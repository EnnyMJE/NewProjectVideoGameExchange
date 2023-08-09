using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.POCO;

namespace VideoGameExchange2023.DAO
{
    internal class CopyDAO
    {
        private string connectionString;

        public CopyDAO()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["VideoGameExchangeDB"].ConnectionString;
        }

        public bool IsCopyAlreadyOwned(Copy copy)
        {
            bool copyOwned = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Copy WHERE videogame=@game AND owner=@owner", connection);
                cmd.Parameters.AddWithValue("@game", copy.Game.GameName);
                cmd.Parameters.AddWithValue("@owner", copy.Owner.Pseudo);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) { copyOwned = true; }
                }
            }
            return copyOwned;
        }

        public bool AddCopy(Copy copy)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.Copy(videogame, owner, available) VALUES(@game, @owner, @available)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@game", copy.Game.GameName);
                cmd.Parameters.AddWithValue("@owner", copy.Owner.Pseudo);
                cmd.Parameters.AddWithValue("@available", copy.Available);

                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public List<Copy> GetLCopybyGame(VideoGame game)
        {
            List<Copy> copyList = new List<Copy>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy WHERE videogame = @game", connection);
                cmd.Parameters.AddWithValue("@game", game.GameName);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Copy copy = new Copy();
                        copy.Id = reader.GetInt32("id");
                        copy.Game = game;
                        string ownerPseudo = reader.GetString("owner");
                        Player player = new Player();
                        copy.Owner = player.getPlayerByPseudo(ownerPseudo);
                        copyList.Add(copy);
                    }
                }
            }
            return copyList;
        }

        public int NumberofCopybyGameAvailable(VideoGame game)
        {
            int numberOfCopiesAvailable = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Copy WHERE videogame = @game AND available=1", connection);
                cmd.Parameters.AddWithValue("@game", game.GameName);
                connection.Open();
                numberOfCopiesAvailable = (int)cmd.ExecuteScalar();
            }

            return numberOfCopiesAvailable;
        }


        public Copy GetACopyAvailable(VideoGame game)
        {
            Copy cp = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy WHERE videogame = @game ORDER BY recording_time_column LIMIT 1", connection);
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM dbo.Copy WHERE videogame = @game and available=1", connection);
                cmd.Parameters.AddWithValue("@game", game.GameName);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cp = new Copy();
                        cp.Id = reader.GetInt32("id");
                        cp.Game = game;
                        string ownerPseudo = reader.GetString("owner");
                        Player player = new Player();
                        cp.Owner = player.getPlayerByPseudo(ownerPseudo);
                    }
                }
            }
            return cp;
        }

        public void UpdateAvailability(Copy cp)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Copy SET available = @available WHERE id=@id", connection);
                cmd.Parameters.AddWithValue("@id", cp.Id);
                cmd.Parameters.AddWithValue("@available", cp.Available);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Copy> GetLCopybyPlayer(Player pl)
        {
            List<Copy> copyList = new List<Copy>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy WHERE owner = @owner", connection);
                cmd.Parameters.AddWithValue("@owner", pl.Pseudo);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Copy copy = new Copy();
                        copy.Id = reader.GetInt32("id");
                        string gameName = reader.GetString("videogame");
                        VideoGame videogame = new VideoGame().GetGameByName(gameName);
                        copy.Game = videogame;
                        copy.Available = reader.GetBoolean("available");
                        copy.Owner = pl;
                        copyList.Add(copy);
                    }
                }
            }
            return copyList;
        }

        public List<Copy> GetCopybyBorrower(Player pl)
        {
            List <Copy> copyList = new List<Copy>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy WHERE id IN (SELECT copy FROM dbo.Loan WHERE borrower = @player)", connection);
                cmd.Parameters.AddWithValue("@player", pl.Pseudo);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Copy copy = new Copy();
                        copy.Id = reader.GetInt32("id");
                        string gameName = reader.GetString("videogame");
                        VideoGame videogame = new VideoGame().GetGameByName(gameName);
                        copy.Game = videogame;
                        copy.Available = reader.GetBoolean("available");
                        string ownerPseudo = reader.GetString("owner");
                        Player player = new Player().getPlayerByPseudo(ownerPseudo);
                        copy.Owner = player;
                        copyList.Add(copy);
                    }
                }
            }
            return copyList;
        }

        public Copy GetCopyById(int idReceived)
        {
            Copy cp = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Copy WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@id", idReceived);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cp = new Copy();
                        cp.Id = reader.GetInt32("id");
                        string gameName = reader.GetString("videogame");
                        VideoGame videogame = new VideoGame().GetGameByName(gameName);
                        cp.Game = videogame;
                        string ownerPseudo = reader.GetString("owner");
                        Player player = new Player().getPlayerByPseudo(ownerPseudo);
                        cp.Owner = player;
                        cp.Available = reader.GetBoolean("available");
                    }
                }
            }
            return cp;
        }

        public bool DeleteCopy(Copy copy)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "DELETE FROM dbo.Copy WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", copy.Id);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
                catch (SqlException ex)
                {
                    // Check if the exception is related to a foreign key constraint violation
                    if (ex.Number == 547) // Error number for foreign key violation
                    {
                        throw new InvalidOperationException("Cannot delete this console as it is referenced by game objects.");
                    }
                    else
                    {
                        // For other database-related exceptions, you can log or handle them differently.
                        throw; // Re-throw the exception to indicate an unexpected error.
                    }
                }
            }
            return success;
        }

    }
}
