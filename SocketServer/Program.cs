using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace SocketServer 
{
    class Program
    {
        public static byte[] dataBuffer = new byte[1024];
        static Message message = new Message();
        static void Main(string[] args) 
        {
            StartServerAsync();
            Console.ReadKey();
        }

        static void StartServerAsync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            # region 绑定ip和端口号
            IPAddress iPAddress = IPAddress.Parse("192.168.59.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 88);
            serverSocket.Bind(iPEndPoint);
            #endregion
            //开始监听端口号
            serverSocket.Listen(0);
            ////接收一个客户端链接同步
            //Socket clientSocket = serverSocket.Accept();
            //接收一个客户端链接异步
            serverSocket.BeginAccept(AcceptCallback,serverSocket);

        }
        static void AcceptCallback(IAsyncResult ar) 
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket =  serverSocket.EndAccept(ar);
            //客户端异步链接回调
            //向客户端发送一条消息
            string msg = "Hello client 你好！...";
            byte[] data = Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(data);
            //接受客户端的一条消息
            clientSocket.BeginReceive(message.Data, message.StartIndex, message.RemainSize, SocketFlags.None, ReceiveCallback, clientSocket);
            serverSocket.BeginAccept(AcceptCallback, serverSocket);
        }

        static void ReceiveCallback(IAsyncResult ar) 
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);
                if (count == 0) 
                {
                    //空数据
                    clientSocket.Close();
                    return;
                }
                message.AddCount(count);
                //string msg = Encoding.UTF8.GetString(message.Data, 0, count);
                //Console.WriteLine("ReceiveCallback执行从客户端异步收到数据" + msg);
                message.ReadMessage();


                clientSocket.BeginReceive(message.Data, message.StartIndex, message.RemainSize, SocketFlags.None, ReceiveCallback, clientSocket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
        }
        void StartServerSync() 
        {

        }
    }
}