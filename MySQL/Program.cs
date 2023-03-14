using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using MySql.Data.MySqlClient;


namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建一个连接对象
            string connStr = "Database=test007;Data Source=127.0.0.1;port = 3306;User Id = root;Password = 5!!xtjWqq!xT;";
            MySqlConnection conn = new MySqlConnection(connStr);
            
            //打开连接
            conn.Open();
            //MySqlCommand cmd = new MySqlCommand() 

            //CRUD操作
            #region 插入
            //string username = "cwer";string password = "lcker";
            ////容易引发SQL注入的问题，故不采用用下面那种方式
            ////MySqlCommand cmd = new MySqlCommand("insert into user set username =‘" + username + "'" + ",password =‘" + password + "'", conn);

            //MySqlCommand cmd = new MySqlCommand("insert into user set username = @un , password = @pwd",conn);
            //cmd.Parameters.AddWithValue("un", username);
            //cmd.Parameters.AddWithValue("pwd", password);

            ////执行插入
            //cmd.ExecuteNonQuery();
            #endregion

            #region 删除
            //MySqlCommand cmd = new MySqlCommand("delete from user where id = @id", conn);
            //cmd.Parameters.AddWithValue("id",18);
            //cmd.ExecuteNonQuery();
            #endregion

            #region 更新
            //MySqlCommand cmd = new MySqlCommand("update user set password = @pwd where id = 11",conn);
            //cmd.Parameters.AddWithValue("pwd", "sikiedu.com");
            //cmd.ExecuteNonQuery();
            #endregion

            #region 查询
            MySqlCommand cmd = new MySqlCommand("select * from user", conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            //判断所属行是否含有数据
            while (reader.Read())
            {
                string username = reader.GetString("username");
                string password = reader.GetString("password");
                Console.WriteLine(username + ":" + password);
            }
            reader.Close();
            #endregion

            conn.Close();
            Console.ReadKey();
        }
    }
}