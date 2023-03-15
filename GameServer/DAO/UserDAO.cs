using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GameServer.Model;

namespace GameServer.DAO
{
    class UserDAO
    {
        //验证用户名和密码
        public User VerifyUser(MySqlConnection conn,string username, string password) 
        {
            MySqlDataReader Reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user where username = @username and password = @password", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                Reader = cmd.ExecuteReader();
                if (Reader.Read())
                {
                    int id = Reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("验证用户名和密码错误请检查" + e);
            }
            finally 
            {
                if (Reader != null) 
                {
                    Reader.Close();
                }
            }
            return null;
        }
    }
}
