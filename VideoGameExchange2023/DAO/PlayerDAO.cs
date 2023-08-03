using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using VideoGameExchange2023.POCO;

namespace VideoGameExchange2023.DAO
{
    internal class PlayerDAO
    {
        private string connectionString;

        public PlayerDAO()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["VideoGameExchangeDB"].ConnectionString;
        }

        public Player GetPlayer(string usr, string psw)
        {
            Player ply = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Player WHERE username=@username AND password=@password", connection);
                cmd.Parameters.AddWithValue("username", usr);
                cmd.Parameters.AddWithValue("password", psw);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ply = new Player();
                        ply.Username = reader.GetString(0);
                        ply.Password = reader.GetString(1);
                        ply.Credit = reader.GetInt32(2);
                        ply.Pseudo = reader.GetString(3);
                        ply.RegistrationDate = reader.GetDateTime(4);
                        ply.DateOfBirth = reader.GetDateTime(5);
                        ply.BirthdayGiftGivenYear = reader.GetInt32(6);
                    }
                }
            }
            return ply;
        }
        
        public Player GetPlayerbyPseudo(string pseudo)
        {
            Player ply = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Player WHERE pseudo=@pseudo", connection);
                cmd.Parameters.AddWithValue("pseudo", pseudo);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ply = new Player();
                        ply.Username = reader.GetString(0);
                        ply.Credit = reader.GetInt32(2);
                        ply.Pseudo = reader.GetString(3);
                    }
                }
            }
            return ply;
        }

        public bool UpdatePlayerCreditAndGiftYear(string pseudo, int newCredit, int newGiftYear)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Player SET credit = @credit, BirthdayGiftGivenYear = @giftYear WHERE pseudo = @pseudo", connection);
                cmd.Parameters.AddWithValue("@credit", newCredit);
                cmd.Parameters.AddWithValue("@giftYear", newGiftYear);
                cmd.Parameters.AddWithValue("@pseudo", pseudo);
                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        
        public bool UpdatePlayerCredit(string pseudo, int newCredit)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Player SET credit = @credit WHERE pseudo = @pseudo", connection);
                cmd.Parameters.AddWithValue("@credit", newCredit);
                cmd.Parameters.AddWithValue("@pseudo", pseudo);
                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool checkPseudo(string pseudo)
        {
            bool pseudoExisted = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Player WHERE pseudo=@pseudo", connection);
                cmd.Parameters.AddWithValue("pseudo", pseudo);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) { pseudoExisted = true; }
                }
            }
            return pseudoExisted;
        }

        public bool AddPlayer(Player pl)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.Player(username, password, credit, pseudo, registrationdate, dateofbirth, birthdaygiftgivenyear) " +
                               "VALUES(@username, @password, @credit, @pseudo, @registrationdate, @dateofbirth, @birthdaygiftgivenyear)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", pl.Username);
                cmd.Parameters.AddWithValue("@password", pl.Password);
                cmd.Parameters.AddWithValue("@credit", pl.Credit);
                cmd.Parameters.AddWithValue("@pseudo", pl.Pseudo);
                cmd.Parameters.AddWithValue("@registrationdate", pl.RegistrationDate);
                cmd.Parameters.AddWithValue("@dateofbirth", pl.DateOfBirth);
                cmd.Parameters.AddWithValue("@birthdaygiftgivenyear", pl.BirthdayGiftGivenYear);

                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }
    }
}
