using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using GameServer.Controller;
using Common;
namespace GameServer.Servers
{
    class Server
    {
        private IPEndPoint ipEndPoint;
        private Socket serverSocket;
        // 所有连接的客户端的列表
        private List<Client> clientList;

        private ControllerManger controllerManger;


        public Server() 
        {
        }
        public Server(string ipStr,int port) 
        {
            controllerManger = new ControllerManger(this);
            SetIpAndPort(ipStr, port);
        }
        public void SetIpAndPort(string ipStr, int port) 
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }
        public void Start() 
        {
            //创建游戏服务器
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定端口和IP
            serverSocket.Bind(ipEndPoint);
            //监听客户端连接(0是设置队列的长度不受限制)
            serverSocket.Listen(0);
            //异步的方式接受客户端的连接
            serverSocket.BeginAccept(AcceptCallBack,null);
        }
        private void AcceptCallBack(IAsyncResult ar) 
        {
            //处理客户端的连接
            Console.WriteLine("客户端连接成功");

            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket,this);
            clientList.Add(client);
        }
        public void RemoveClient(Client client) 
        {
            //由于是异步的回调,所以线程是不安全的，
            //多线程环境下，如果一个线程锁定了共享资源，需要访问该资源的其他线程则会处于阻塞状态，并等待直到该共享资源解除锁定
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }


        public void SendResponse(Client client,ActionCode actionCode, string data) 
        {
            //TODO给客户端响应
            client.Send(actionCode, data);
        }
        public void HandlerRequest(RequestCode requestCode,ActionCode actionCode,string data,Client client) 
        {
            //客户端解析成功数据后不直接与Server进行交互，而是转发给ControllerManger让其进行消息的处理以减少耦合
            //ControllerManger和Server进行交互，Server和Client交互，Client和ControllerManger交互，Server相当与“中介”
            controllerManger.HandleRequest(requestCode,actionCode,data,client);
        }
    }
}
