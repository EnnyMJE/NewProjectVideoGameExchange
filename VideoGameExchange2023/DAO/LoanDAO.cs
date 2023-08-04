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

        public List<Loan> GetLLoanByPlayer(Player player)
        {
            List<Loan> loanList = new List<Loan>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Loan WHERE borrower = @borrower and ongoing=1", connection);
                cmd.Parameters.AddWithValue("@borrower", player.Pseudo);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan loan = new Loan();
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
    }
}
