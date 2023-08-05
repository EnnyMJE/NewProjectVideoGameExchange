using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.DAO;

namespace VideoGameExchange2023.POCO
{
    internal class Copy
    {
        private int id;
        private VideoGame game;
        private Player owner;
        private bool available;

        public Copy() { }
        public Copy(VideoGame game, Player owner)
        {
            this.game = game;
            this.owner = owner;
            available = true;
        }
        public Copy(int id, VideoGame game) { this.id = id; this.game = game; available = true; }
        public Copy(int id, VideoGame game, Player owner)
        {
            this.id = id;
            this.game = game;
            this.owner = owner;
            available = true;
        }

        public int Id { get => id; set => id = value; }
        public Player Owner { get => owner; set => owner = value; }
        public bool Available { get => available; set => available = value; }
        public VideoGame Game { get => game; set => game = value; }

        public string AvailabilityStatus
        {
            get { return Available ? "available" : "is rented"; }
        }
        public bool AddNewCopy()
        {
            CopyDAO copyDAO = new CopyDAO();
            if (copyDAO.IsCopyAlreadyOwned(this)) return false;
            return copyDAO.AddCopy(this);
        }

        public bool IsOwner()
        {
            CopyDAO copyDAO = new CopyDAO();
            return copyDAO.IsCopyAlreadyOwned(this);
        }

        public void borrow(Loan loan)
        {
            bool borrowed = loan.AddNewLoan();
            if (borrowed)
            {
                CopyDAO copyDAO = new CopyDAO();
                this.Available = false;
                copyDAO.UpdateAvailability(this);
            }
        }

        public int copyAvailable (VideoGame game)
        {
            CopyDAO copyDAO = new CopyDAO();
            return copyDAO.NumberofCopybyGameAvailable(game);
        }
       
        public static List<Copy> GetLCopiesByPlayer(Player player)
        {
            CopyDAO copyDAO = new CopyDAO();
            return copyDAO.GetLCopybyPlayer(player);
        }
        public Copy GetCopyById(int id)
        {
            CopyDAO copyDAO = new CopyDAO();
            return copyDAO.GetCopyById(id);
        }

        public bool DeleteCopy()
        {
            CopyDAO copyDAO = new CopyDAO();
            return copyDAO.DeleteCopy(this);
        }

        public void ReleaseCopy()
        {
            Loan loan = new Loan();
            loan.UpdateLoanStatus(this);
            CopyDAO copyDAO = new CopyDAO();
            this.Available = true;
            copyDAO.UpdateAvailability(this);

        }
    }
}
