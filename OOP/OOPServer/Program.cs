using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace OOPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint address = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788);
            UdpClient client = new UdpClient(address);
            byte[] data = client.Receive(ref address);
            Console.WriteLine(Encoding.ASCII.GetString(data));
            Console.WriteLine(address.ToString());
            Console.ReadKey(true);
        }
    }
}
