using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.DAO;

namespace VideoGameExchange2023.POCO
{
    internal class Console
    {
        private string consoleName;

        public Console() { }

        public Console(string consoleName)
        {
            this.consoleName = consoleName;
        }

        public string ConsoleName { get => consoleName; set => consoleName = value; }

        public static List<Console> GetLConsoles()
        {
            ConsoleDAO consoleDAO = new ConsoleDAO();
            return consoleDAO.GetLConsoles();
        }

        public bool AddNewConsole()
        {
            ConsoleDAO consoleDAO = new ConsoleDAO();
            if (consoleDAO.IsConsoleExisted(this)) return false;
            else return consoleDAO.AddConsole(this);
        }
        
        public bool DeleteConsole()
        {
            ConsoleDAO consoleDAO = new ConsoleDAO();
            return consoleDAO.DeleteConsole(this);
        }
    }
}
