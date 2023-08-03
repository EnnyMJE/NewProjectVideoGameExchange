using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameExchange2023.DAO;

namespace VideoGameExchange2023.POCO
{
    public abstract class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public abstract User Login();

    }


}
