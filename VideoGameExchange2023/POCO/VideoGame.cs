using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.DAO;

namespace VideoGameExchange2023.POCO
{
    internal class VideoGame
    {
        private string gameName;
        private int creditCost;
        private string console;

        public VideoGame() { }

        public VideoGame(string gameName, int creditCost, string console)
        {
            this.gameName = gameName;
            this.creditCost = creditCost;
            this.console = console;
        }

        public string GameName { get => gameName; set => gameName = value; }
        public int CreditCost { get => creditCost; set => creditCost = value; }
        public string Console { get => console; set => console = value; }

        public static List<VideoGame> GetLGames()
        {
            GameDAO gameDAO = new GameDAO();   
            return gameDAO.GetLGames();
        }

        public bool AddNewGame()
        {
            GameDAO gameDAO = new GameDAO();
            if (gameDAO.IsGameExisted(this)) return false;
            else return gameDAO.AddGame(this);
        }

        public bool DeleteGame()
        {
            GameDAO gameDAO = new GameDAO();
            return gameDAO.DeleteGame(this);
        }

        public bool UpdateCost(int newCost)
        {
            GameDAO gameDAO = new GameDAO();
            return gameDAO.EditCredit(newCost, this);
        }

        public List<Copy> CopyAvailable()
        {
            CopyDAO copyDAO = new CopyDAO();
            return copyDAO.GetLCopybyGame(this);
        }

        public Copy GetACopyAvailable()
        {
            CopyDAO copyDAO = new CopyDAO();
            return copyDAO.GetACopyAvailable(this);
        }

        public int nbrCopyAvailable()
        {
            Copy cp = new Copy();
            return cp.copyAvailable(this);
        }

        public bool AlreadyRented(Player player)
        {
            LoanDAO loanDAO = new LoanDAO();
            return loanDAO.IsGameRentedByPlayer(this, player);
        }

        public VideoGame GetGameByName(string gamename)
        {
            GameDAO gameDAO = new GameDAO();
            return gameDAO.GetGameByName(gamename);
        }
    }
}
