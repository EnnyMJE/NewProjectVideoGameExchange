using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.DAO;

namespace VideoGameExchange2023.POCO
{
    internal class Booking
    {
        private int id;
        private DateTime bookingDate;
        private Player futureBorrower;
        private VideoGame game;

        public Booking() { }
        public Booking(DateTime bookingDate, Player futureBorrower, VideoGame game)
        {
            this.BookingDate = bookingDate;
            this.FutureBorrower = futureBorrower;
            this.Game = game;
        }
        public Booking(int id, DateTime bookingDate, Player futureBorrower, VideoGame game)
        {
            this.Id = id;
            this.BookingDate = bookingDate;
            this.FutureBorrower = futureBorrower;
            this.Game = game;
        }

        public int Id { get => id; set => id = value; }
        public DateTime BookingDate { get => bookingDate; set => bookingDate = value; }
        public Player FutureBorrower { get => futureBorrower; set => futureBorrower = value; }
        public VideoGame Game { get => game; set => game = value; }

        public bool AddNewBooking()
        {
            BookingDAO bookingDao = new BookingDAO();
            return bookingDao.AddBooking(this);
        }

        public static List<Booking> GetLBookingByPlayer(Player player)
        {
            BookingDAO bookingDAO = new BookingDAO();
            return (List<Booking>)bookingDAO.GetLBookingByPlayer(player);
        }

        public static List<Booking> GetLBookingByGame(VideoGame videogame)
        {
            BookingDAO bookingDAO = new BookingDAO();
            return (List<Booking>)bookingDAO.GetLBookingByGame(videogame);
        }

        public bool DeleteBooking()
        {
            BookingDAO bookingDAO = new BookingDAO();
            return bookingDAO.DeleteBooking(this);
        }

        public Booking SendPriorityBooking(VideoGame game)
        {
            List<Booking> bookings = GetLBookingByGame(game);
            if (bookings == null || bookings.Count == 0)
            {
                return null;
            }

            List<Booking> sortedBookings = bookings
                .OrderByDescending(booking => booking.FutureBorrower.Credit)
                .ThenBy(booking => booking.BookingDate)
                .ThenBy(booking => booking.FutureBorrower.RegistrationDate)
                .ThenByDescending(booking => booking.FutureBorrower.Age)
                .ThenBy(booking => Guid.NewGuid()) // Randomize
                .ToList();

            //to make sure by the time time a booking object is choosen, the player still have enough credit to afford related game. 
            sortedBookings = sortedBookings.Where(booking => booking.FutureBorrower.Credit > 0 && booking.FutureBorrower.Credit >= booking.Game.CreditCost).ToList(); 

            return sortedBookings.FirstOrDefault();
        }

        public static bool PlayerHasBookedGame(Player player, VideoGame game)
        {
            BookingDAO bookingDAO = new BookingDAO();
            return bookingDAO.DoesBookingExist(player, game);
        }


    }
}
