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
    internal class LoanDAO
    {
        private string connectionString;

        public LoanDAO()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["VideoGameExchangeDB"].ConnectionString;
        }

        public bool AddLoan(Loan loan)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.Loan(startdate, enddate, ongoing, copy, lender, borrower) VALUES(@startdate, @enddate, @ongoing, @copy, @lender, @borrower)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@startdate", loan.StartTime);
                cmd.Parameters.AddWithValue("@enddate", loan.EndTime);
                cmd.Parameters.AddWithValue("@ongoing", loan.Ongoing);
                cmd.Parameters.AddWithValue("@copy", loan.Copy.Id);
                cmd.Parameters.AddWithValue("@lender", loan.Owner.Pseudo);
                cmd.Parameters.AddWithValue("@borrower", loan.Borower.Pseudo);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public List<Loan> GetLLoanByPlayer(Player player, bool ongoing)
        {
            List<Loan> loanList = new List<Loan>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Loan WHERE borrower = @borrower and ongoing=@ongoing", connection);
                cmd.Parameters.AddWithValue("@borrower", player.Pseudo);
                cmd.Parameters.AddWithValue("@ongoing", ongoing);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan loan = new Loan();
                        loan.Id = reader.GetInt32("id");
                        int idCopy = reader.GetInt32("copy");
                        Copy cp = new Copy().GetCopyById(idCopy);
                        loan.Copy = cp;
                        loan.StartTime = reader.GetDateTime("startdate");
                        loan.EndTime = reader.GetDateTime("enddate");
                        loanList.Add(loan);
                    }
                }
            }
            return loanList;
        }

        public void UpdateLoanStatus(Copy copy)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Loan SET ongoing = 0 WHERE copy=@copy AND ongoing=1", connection);
                cmd.Parameters.AddWithValue("@copy", copy.Id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool DeleteLoan(Loan loan)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "DELETE FROM dbo.Loan WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", loan.Id);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        throw new InvalidOperationException("Cannot delete this console as it is referenced by game objects.");
                    }
                    else
                    {
                        throw; 
                    }
                }
            }
            return success;
        }


    }
}
