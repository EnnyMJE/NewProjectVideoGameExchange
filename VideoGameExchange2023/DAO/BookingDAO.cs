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
    internal class BookingDAO
    {
        private string connectionString;

        public BookingDAO()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["VideoGameExchangeDB"].ConnectionString;
        }

        public bool AddBooking(Booking booking)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.Booking(bookdate, player, game) VALUES(@bookdate, @player, @game)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@bookdate", booking.BookingDate);
                cmd.Parameters.AddWithValue("@player", booking.FutureBorrower.Pseudo);
                cmd.Parameters.AddWithValue("@game", booking.Game.GameName);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public List<Booking> GetLBookingByPlayer(Player pl)
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Booking WHERE player = @player", connection);
                cmd.Parameters.AddWithValue("@player", pl.Pseudo);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking();
                        booking.Id = reader.GetInt32("id");
                        booking.FutureBorrower = pl;
                        string gameName = reader.GetString("game");
                        VideoGame videoGame = new VideoGame().GetGameByName(gameName);
                        booking.Game = videoGame;
                        booking.BookingDate = reader.GetDateTime("bookdate");
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }

        public bool DeleteBooking(Booking book)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "DELETE FROM dbo.Booking WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", book.Id);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        throw new InvalidOperationException("Cannot cancel this booking.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return success;
        }

        public List<Booking> GetLBookingByGame(VideoGame videogame)
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Booking WHERE game = @game", connection);
                cmd.Parameters.AddWithValue("@game", videogame.GameName);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking();
                        booking.Id = reader.GetInt32("id");
                        string playerName = reader.GetString("player");
                        Player player = new Player();
                        booking.FutureBorrower = player.getPlayerByPseudo(playerName);
                        booking.Game = videogame;
                        booking.BookingDate = reader.GetDateTime("bookdate");
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }

        public Booking GetABooking(VideoGame videoGame)
        {
            Booking book = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM dbo.Booking WHERE game = @game", connection);
                cmd.Parameters.AddWithValue("@game", videoGame.GameName);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        book = new Booking();
                        book.Id = reader.GetInt32("id");
                        string borrowerPseudo = reader.GetString("player");
                        Player player = new Player();
                        book.FutureBorrower = player.getPlayerByPseudo(borrowerPseudo);
                        book.BookingDate = reader.GetDateTime("bookdate");
                    }
                }
            }
            return book;
        }

        public bool DoesBookingExist(Player player, VideoGame game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM dbo.Booking WHERE player = @player AND game = @game";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@player", player.Pseudo);
                cmd.Parameters.AddWithValue("@game", game.GameName);
                connection.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

    }
}
