using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
using GameServer.DAO;
using GameServer.Model;

namespace GameServer.Controller
{
    class UserController : BaseController
    {
        private UserDAO userDao = new UserDAO();

        public UserController()
        {
            requestCode = RequestCode.User;
        }
        public void Login(string data, Client client, Server server) 
        {
            string[] strs = data.Split(',');
            User user = userDao.VerifyUser(client.MySqlConnection,strs[0], strs[1]);
            if (user == null) 
            {

            }
        }
    }
}
