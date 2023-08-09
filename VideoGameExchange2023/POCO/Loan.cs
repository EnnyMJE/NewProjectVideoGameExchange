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
        private Player borrower;

        public Loan() { }

        public Loan(DateTime startTime, DateTime endTime, bool ongoing, Copy copy, Player owner, Player borower)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Ongoing = ongoing;
            this.Copy = copy;
            this.Owner = owner;
            this.Borrower = borower;
        }

        public Loan(int id, DateTime startTime, DateTime endTime, bool ongoing, Copy copy, Player owner, Player borrower)
        {
            this.id = id;
            this.startTime = startTime;
            this.endTime = endTime;
            this.ongoing = ongoing;
            this.copy = copy;
            this.owner = owner;
            this.borrower = borrower;
        }

        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime EndTime { get => endTime; set => endTime = value; }
        public bool Ongoing { get => ongoing; set => ongoing = value; }
        public Player Owner { get => owner; set => owner = value; }
        public Player Borrower { get => borrower; set => borrower = value; }
        public int Id { get => id; set => id = value; }
        public Copy Copy { get => copy; set => copy = value; }

        //todo: to bevelopped.
        public bool CalculateBalance()
        {
            return true;
        }

        public bool AddNewLoan()
        {
            LoanDAO loanDAO = new LoanDAO();
            bool borrowed = loanDAO.AddLoan(this);
            if (borrowed)
            {
                this.Owner.AddCredit(this.Copy.Game.CreditCost);
                this.borrower.UpdateCredit(this.Copy.Game.CreditCost);
            }
            return borrowed;
        }

        public static List<Loan> GetLLoansByBorrower(Player borrower)
        {
            LoanDAO loanDAO = new LoanDAO();
            return loanDAO.GetLLoanByPlayer(borrower, true);
        }

        public static List<Loan> GetLOldLoansByBorrower(Player borrower)
        {
            LoanDAO loanDAO = new LoanDAO();
            return loanDAO.GetLLoanByPlayer(borrower, false);
        }

        public void UpdateLoanToNonActive()
        {
            this.Ongoing = false;
            LoanDAO loanDAO = new LoanDAO();
            loanDAO.UpdateLoanStatus(this);
        }

        public bool DeleteLoan()
        {
            LoanDAO loanDAO = new LoanDAO();
            return loanDAO.DeleteLoan(this);
        }

    }
}
