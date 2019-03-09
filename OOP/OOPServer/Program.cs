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
        const string info_ini_path = "info.ini";

        static void Main(string[] args)
        {
            IPEndPoint address = ParseIp(info_ini_path);
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

        private static IPEndPoint ParseIp(string file_path)
        {
            IPEndPoint default_address = new IPEndPoint(IPAddress.Parse(_ip), _port);
            if (!File.Exists(file_path))
            {
                File.WriteAllText(file_path, _ip + ":" + _port.ToString());
                return default_address;
            }
            string[] ip_port = File.ReadAllText(file_path).Split(':');

            if (IPAddress.TryParse(ip_port[0], out IPAddress ip) && int.TryParse(ip_port[1], out int port))
                return new IPEndPoint(ip, port);
            return default_address;
        }
    }
}
