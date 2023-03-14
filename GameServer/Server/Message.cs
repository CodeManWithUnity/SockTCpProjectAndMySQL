using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Server
{
    class Message
    {
        private byte[] data = new byte[1024];
        //我们存储了多少个字节的数据在里面
        private int startIndex = 0;
        public byte[] Data
        {
            get { return data; }
            private set { }
        }
        public int StartIndex
        {
            get { return startIndex; }
            private set { }
        }
        public int RemainSize 
        {
            get { return data.Length - startIndex; }
        }
        public void AddCount(int count) 
        {
            startIndex += count;
        }
        /// <summary>
        /// 解析数据
        /// </summary>
        public void ReadMessage (int cunt)
        {
            AddCount(cunt);
            while (true) 
            {
                if (startIndex <= 4) 
                { 
                    return;
                }
                int count = BitConverter.ToInt32(data, 0);
                if (startIndex - 4 > 0)
                {
                    string s = Encoding.UTF8.GetString(data, 4, count);
                    Console.WriteLine("解析出来一条数据：" + s);
                    Array.Copy(data, count + 4, data, 0, startIndex - count);
                    startIndex -= count;
                }
                else 
                {
                    break;
                }
            }
        }
    }
}
