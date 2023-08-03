using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.DAO;

namespace VideoGameExchange2023.POCO
{
    public class Player : User
    {
        int credit;
        string pseudo;
        DateTime dateOfBirth;
        DateTime registrationDate;
        private int birthdayGiftGivenYear;

        public int Credit { get => credit; set => credit = value; }
        public string Pseudo { get => pseudo; set => pseudo = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public int BirthdayGiftGivenYear { get => birthdayGiftGivenYear; set => birthdayGiftGivenYear = value; }

        public bool LoanAllowed() //todo to be developed 
        {
            return true;
        }

        public Player() { }

        public Player(string username, string password)
        {
            base.Username = username;
            base.Password = password;
        }


        public Player(string username, string password, string pseudo, DateTime dateOfBirth, DateTime registrationDate)
        {
            base.Username = username;
            base.Password = password;
            this.Credit = 10;
            this.Pseudo = pseudo;
            this.DateOfBirth = dateOfBirth;
            this.RegistrationDate = registrationDate;
            this.BirthdayGiftGivenYear = 0;
        }

        public override User Login()
        {
            PlayerDAO playerDAO = new PlayerDAO();
            Player pl = playerDAO.GetPlayer(this.Username, this.Password);
            return pl;
        }

        public bool PseudoExisted()
        {
            PlayerDAO plDAO = new PlayerDAO();
            bool existed = plDAO.checkPseudo(this.Pseudo);
            return existed;
        }

        public bool AddNewPlayer(int givenYear)
        {
            this.BirthdayGiftGivenYear = givenYear;
            PlayerDAO playerDAO = new PlayerDAO();
            return playerDAO.AddPlayer(this);
        }

        public bool BirthdayGiftGiven()
        {
            DateTime currentDate = DateTime.Today;
            int curyear = currentDate.Year;
            if(curyear > this.BirthdayGiftGivenYear)
            {
                if(currentDate.Month > this.DateOfBirth.Month  || (currentDate.Month == this.DateOfBirth.Month && currentDate.Day >= this.DateOfBirth.Day) )
                {
                    this.Credit += 2;
                    PlayerDAO playerDAO = new PlayerDAO();
                    return playerDAO.UpdatePlayerCreditAndGiftYear(this.Pseudo, this.Credit, curyear);
                }
            }
            return false;
        }

        public bool IsBirthday()
        {
            DateTime currentDate = DateTime.Today;
            int curdate = currentDate.Day;
            int curmonth = currentDate.Month;
            int birthdate = this.DateOfBirth.Day;
            int birthmonth = this.DateOfBirth.Month;
            
            return curdate==birthdate && curmonth==birthmonth;
        }

        public int YearGivenGift()
        {
            
            if(this.DateOfBirth.Month > DateTime.Today.Month || (this.DateOfBirth.Month == DateTime.Today.Month && this.DateOfBirth.Day >= DateTime.Today.Day))
            {
                return 0;
            }
            return DateTime.Today.Year;
        }

        public Player getPlayerByPseudo(string pseudo)
        {
            PlayerDAO playerDAO = new PlayerDAO();
            return playerDAO.GetPlayerbyPseudo(pseudo);
        }

        public bool UpdateCredit(int numberToDeduct)
        {
            if (numberToDeduct > this.Credit)
            {
                return false;
            }
            PlayerDAO playerDAO1 = new PlayerDAO();
            this.credit -= numberToDeduct;
            return playerDAO1.UpdatePlayerCredit(this.pseudo, this.Credit);
        }


    }
}
