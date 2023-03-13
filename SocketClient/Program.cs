using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using SocketClient;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //和服务端建立链接
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.59.1"), 88));

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            string msg = Encoding.UTF8.GetString(data, 0, count);
            Console.WriteLine(msg);
            //while (true) 
            //{
            //    string s = Console.ReadLine();
            //    Console.WriteLine(s);
            //    clientSocket.Send(Encoding.UTF8.GetBytes(s));
            //}
            for (int i = 0; i < 100; i++)
            {
                clientSocket.Send(Message.GetBytes(i.ToString()));
            }
            //clientSocket.Send(Encoding.UTF8.GetBytes(""));

            Console.ReadKey();
            clientSocket.Close();
        }
        //异步
        void StartServerASync()
        {

        }
        //同步
        void StartServerSync() 
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //和服务端建立链接
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.59.1"), 88));

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            string msg = Encoding.UTF8.GetString(data, 0, count);
            Console.WriteLine(msg);

            string s = Console.ReadLine();
            clientSocket.Send(Encoding.UTF8.GetBytes(s));
            Console.ReadKey();
            clientSocket.Close();
        }
    }
}