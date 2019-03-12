using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace OOPServer
{
    class Program
    {
        const string default_ip = "127.0.0.1";
        const int default_port = 3344;
        const string ip_info_path = "ip.ini";

        public enum MessageType { GetResult, SendFile };

        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, default_port);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer_for_int = new byte[4];
                    stream.Read(buffer_for_int, 0, 4);
                    MessageType messageType = (MessageType)BitConverter.ToInt32(buffer_for_int, 0);

                    if (messageType == MessageType.SendFile)
                    {
                        stream.Read(buffer_for_int, 0, 4);
                        int branch_id = BitConverter.ToInt32(buffer_for_int, 0);

                        List<byte> data = new List<byte>();
                        do
                        {
                            byte[] buffer = new byte[256];
                            int bytes_count = stream.Read(buffer, 0, buffer.Length);
                            for (int i = 0; i < bytes_count; i++)
                                data.Add(buffer[i]);
                        } while (stream.DataAvailable);

                        File.WriteAllBytes("branch" + branch_id + ".dat", data.ToArray());
                        Console.WriteLine("File branch" + branch_id + ".dat was received");
                    }
                    else
                    {
                        //Код создания итоговой таблицы из имеющихся данных
                        //Итоговая таблица - result.dat
                        byte[] file = File.ReadAllBytes("result.dat");
                        stream.Write(file, 0, file.Length);
                        Thread.Sleep(200);
                        Console.WriteLine("File result.dat was sent");
                    }
                }
                client.Close();
            }
        }
    }
}
