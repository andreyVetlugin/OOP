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
        const string default_ip = "127.0.0.1";
        const int default_port = 3123;
        const string ip_info_path = "ip.ini";

        public enum MessageType { GetResult, SendFile };

        static void Main(string[] args)
        {
            IPEndPoint address = ParseIp(ip_info_path);
            UdpClient client = new UdpClient(address);

            while(true)
            {
                byte[] data = client.Receive(ref address);
                MessageType messageType = (MessageType)BitConverter.ToInt32(data, 0);

                if (messageType == MessageType.SendFile)
                {
                    data = client.Receive(ref address);
                    int branch_index = BitConverter.ToInt32(data, 0);
                    using (FileStream file = File.Create("branch" + branch_index + ".dat"))
                    {
                        data = client.Receive(ref address);
                        file.Write(data, 0, data.Length);
                    }
                    Console.WriteLine("branch" + branch_index + "send data correctly");
                }
                else
                {
                    //Здесь код для подсчета баллов по всем таблицам
                    //Последующая отправка пользователю
                    byte[] message = Encoding.Unicode.GetBytes("Тестовое сообщение123");
                    client.Send(message, message.Length, address);
                }
            }
        }

        private static IPEndPoint ParseIp(string file_path)
        {
            IPEndPoint default_address = new IPEndPoint(IPAddress.Parse(default_ip), default_port);
            if (!File.Exists(file_path))
            {
                File.WriteAllText(file_path, default_ip + ":" + default_port.ToString());
                return default_address;
            }
            string[] ip_port = File.ReadAllText(file_path).Split(':');

            if (IPAddress.TryParse(ip_port[0], out IPAddress ip) && int.TryParse(ip_port[1], out int port))
                return new IPEndPoint(ip, port);
            return default_address;
        }
    }
}
