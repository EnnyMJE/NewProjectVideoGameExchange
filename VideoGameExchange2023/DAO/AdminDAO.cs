using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.POCO;

namespace VideoGameExchange2023.DAO
{
    internal class AdminDAO
    {
        private string connectionString;

        public AdminDAO()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["VideoGameExchangeDB"].ConnectionString;
        }

        public Admin GetAdmin(string usr, string psw)
        {
            Admin adm = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Admin WHERE username=@username AND password=@password", connection);
                cmd.Parameters.AddWithValue("username", usr );
                cmd.Parameters.AddWithValue("password", psw );
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        adm = new Admin();
                        adm.Username = reader.GetString("username");
                        adm.Password = reader.GetString("password");
                    }
                }
            }
            return adm;
        }

        public int nbrAdm()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM dbo.Admin", connection);
                connection.Open();
                int result = (int)cmd.ExecuteScalar();
                return result;
            }
        }
    }
}
