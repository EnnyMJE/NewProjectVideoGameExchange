using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using VideoGameExchange2023.POCO;
using Console = VideoGameExchange2023.POCO.Console;
using System.Data;

namespace VideoGameExchange2023.DAO
{
    internal class ConsoleDAO
    {
        private string connectionString;

        public ConsoleDAO()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["VideoGameExchangeDB"].ConnectionString;
        }

        public List<Console> GetLConsoles()
        {
            List<Console> lConsoles = new List<Console>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Console", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console console = new Console();
                        console.ConsoleName = reader.GetString("consolename");
                        lConsoles.Add(console);
                    }
                }
            }
            return lConsoles;
        }

        public bool IsConsoleExisted(Console cons)
        {
            bool consExisted = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Console WHERE consolename=@name", connection);
                cmd.Parameters.AddWithValue("name", cons.ConsoleName);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) { consExisted = true; }
                }
            }
            return consExisted;
        }

        public bool AddConsole(Console cons)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.Console(consolename) VALUES(@consolename)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@consolename", cons.ConsoleName);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public bool DeleteConsole(Console cons)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM dbo.Console WHERE consolename = @consolename";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@consolename", cons.ConsoleName);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

    }
}
