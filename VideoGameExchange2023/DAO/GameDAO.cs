using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.POCO;
using System.Data;
using System.Data.SqlClient;

namespace VideoGameExchange2023.DAO
{
    internal class GameDAO
    {
        private string connectionString;

        public GameDAO()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["VideoGameExchangeDB"].ConnectionString;
        }

        public List<VideoGame> GetLGames()
        {
            List<VideoGame> lGames = new List<VideoGame>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Videogames", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoGame game = new VideoGame();
                        game.GameName = reader.GetString("name");
                        game.CreditCost = reader.GetInt32("creditcost");
                        game.Console = reader.GetString("console");
                        lGames.Add(game);
                    }
                }
            }
            return lGames;
        }

        public bool IsGameExisted(VideoGame game)
        {
            bool isExisted = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Videogames WHERE name=@name", connection);
                cmd.Parameters.AddWithValue("name", game.GameName);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) { isExisted = true; }
                }
            }
            return isExisted;   
        }

        public bool AddGame(VideoGame game)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.Videogames(name, creditcost, console) VALUES(@name, @creditcost, @console)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", game.GameName);
                cmd.Parameters.AddWithValue("@creditcost", game.CreditCost);
                cmd.Parameters.AddWithValue("@console", game.Console);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public bool DeleteGame(VideoGame game)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "DELETE FROM dbo.Videogames WHERE name = @name and console = @console";
                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@name", game.GameName);
                    cmd.Parameters.AddWithValue("@console", game.Console);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        throw new InvalidOperationException("Cannot delete this video game as it is referenced by copy objects.");
                    }
                    else
                    {
                        throw; 
                    }
                }


            }
            return success;
        }

        public bool EditCredit(int newCost, VideoGame game)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE dbo.Videogames SET creditcost = @newcost WHERE name = @name ";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@newcost", newCost);
                cmd.Parameters.AddWithValue("@name", game.GameName);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public VideoGame GetGameByName(string name)
        {
            VideoGame game = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Videogames WHERE name = @name", connection);
                cmd.Parameters.AddWithValue("@name", name);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        game = new VideoGame();
                        game.CreditCost = reader.GetInt32("creditcost");
                        game.Console = reader.GetString("console");
                        game.GameName = name;
                    }
                }
            }
            return game;
        }
    }
}
