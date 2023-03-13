using System.Text;

namespace 数据转成字节数组
{
    class Program 
    {
        static void Main(string[] args) 
        {
            //byte[] data  = Encoding.UTF8.GetBytes("  ");
            uint count = 256;
            byte[] data = BitConverter.GetBytes(count);
            foreach (byte b in data) 
            {
                Console.Write(b+":");
            }
            Console.ReadKey();
        }
    }
}