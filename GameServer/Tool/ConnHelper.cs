using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//处理每个客户端和数据库的连接和关闭
namespace GameServer.Tool
{
    class ConnHelper
    {
        public const string CONNECTIONSTRING = "datasource = 127.0.0.1;port =3306;database =game01;user = root; pwd = root;";
        /// <summary>
        /// 建立连接
        /// </summary>
        public static MySqlConnection Connect()
        {
            //创建连接
            MySqlConnection conn = new MySqlConnection(CONNECTIONSTRING);
            //打开连接
            try
            {
                conn.Open();
                return conn;
            }
            catch(Exception e)
            {
                Console.WriteLine("连接数据库异常"+e);
                return null;
            }
        }
        //关闭连接
        public static void CloseConnection(MySqlConnection conn) 
        {
            if (conn != null)
            {
                conn.Close();
            }
            else 
            {
                Console.WriteLine("MySqlConnection不能为空");
            }
        }
    }
}
