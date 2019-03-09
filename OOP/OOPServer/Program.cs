using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace OOPServer
{
    class Program
    {
        const string _ip = "127.0.0.1";
        const int _port = 3123;

        static void Main(string[] args)
        {
            IPEndPoint address = new IPEndPoint(IPAddress.Parse(_ip), _port);
            UdpClient client = new UdpClient(address);
            while(true)
            {
                byte[] data = client.Receive(ref address);
                int filiation_id = BitConverter.ToInt32(data, 0);
                using (FileStream file = File.Create("filiation" + filiation_id + ".dat"))
                {
                    data = client.Receive(ref address);
                    file.Write(data, 0, data.Length);
                }
                Console.WriteLine("filiation" + filiation_id + ".dat recorded correctly");
            }
        }
    }
}
