using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class User
    {
        public User(int id, string username, string password) 
        {
            this.Id = id;
            this.Password = password;
            this.UserName = username;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
