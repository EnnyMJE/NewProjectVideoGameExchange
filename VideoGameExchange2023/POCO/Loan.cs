using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.DAO;

namespace VideoGameExchange2023.POCO
{
    internal class Loan
    {
        private int id;
        private DateTime startTime;
        private DateTime endTime;
        private bool ongoing;
        private Copy copy;
        private Player owner;
        private Player borower;

        public Loan() { }

        public Loan(DateTime startTime, DateTime endTime, bool ongoing, Copy copy, Player owner, Player borower)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Ongoing = ongoing;
            this.Copy = copy;
            this.Owner = owner;
            this.Borower = borower;
        }

        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime EndTime { get => endTime; set => endTime = value; }
        public bool Ongoing { get => ongoing; set => ongoing = value; }
        public Player Owner { get => owner; set => owner = value; }
        public Player Borower { get => borower; set => borower = value; }
        public int Id { get => id; set => id = value; }
        internal Copy Copy { get => copy; set => copy = value; }

        //todo: to bevelopped.
        public bool CalculateBalance()
        {
            return true;
        }

        public bool AddNewLoan()
        {
            LoanDAO loanDAO = new LoanDAO();
            return loanDAO.AddLoan(this);
        }

    }
}
