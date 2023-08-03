using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.DAO;

namespace VideoGameExchange2023.POCO
{
    public class Admin : User
    {
        public Admin() { }
        public Admin(string username, string password)
        {
            base.Username = username;
            base.Password = password;
        }

        public override User Login()
        {
            AdminDAO adminDAO = new AdminDAO();
            Admin ad = adminDAO.GetAdmin(this.Username, this.Password);
            return ad;
        }

        public int NbrAdmin()
        {
            AdminDAO db = new AdminDAO();
            return db.nbrAdm();
        }
    }
}
