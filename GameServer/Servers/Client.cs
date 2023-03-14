using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace GameServer.Servers
{
    //这个类是每个客户端具体处理通信的一个类
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message message = new Message();
        public Client() { }
        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
        }
        public void Start() 
        {
            //开始接收
            clientSocket.BeginReceive(message.Data, message.StartIndex,message.RemainSize,SocketFlags.None,ReceiveCallBack,null);
        }
        private void ReceiveCallBack(IAsyncResult ar) 
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                }
                //TODO 处理接收到的数据
                message.ReadMessage(count,OnProcessMessage);

                Start();
                clientSocket.BeginReceive(null,0,0,SocketFlags.None,ReceiveCallBack,null);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
            finally { }
        }
        private void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            //消息解析成功后回调
            server.HandlerRequest(requestCode, actionCode, data, this);
        }
        public void Send(RequestCode requestCode,string data) 
        {
            byte[] bytes = Message.PackData(requestCode,data);
            //发送数据
            clientSocket.Send(bytes);
        }


        private void Close()
        {
            if (clientSocket != null) 
            {
                clientSocket.Close();
                this.server.RemoveClient(this);
            };
        }
    }
}
