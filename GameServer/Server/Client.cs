using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Server
{
    //这个类是每个客户端具体处理通信的一个类
    class Client
    {
        private Socket ClientSocket;
        private Server Server;
        private Message message = new Message();
        public Client() { }
        public Client(Socket clientSocket, Server server)
        {
            this.ClientSocket = clientSocket;
            this.Server = server;
        }
        public void Start() 
        {
            //开始接收
            ClientSocket.BeginReceive(message.Data, message.StartIndex,message.RemainSize,SocketFlags.None,ReceiveCallBack,null);
        }
        private void ReceiveCallBack(IAsyncResult ar) 
        {
            try
            {
                int count = ClientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                }
                //TODO 处理接收到的数据
                message.ReadMessage(count);

                Start();
                ClientSocket.BeginReceive(null,0,0,SocketFlags.None,ReceiveCallBack,null);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
            finally { }
        }
        private void Close()
        {
            if (ClientSocket != null) 
            {
                ClientSocket.Close();
                this.Server.RemoveClient(this);
            };
        }
    }
}
