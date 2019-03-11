using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace OOP
{
    public static class DataManager
    {
        const string default_ip = "127.0.0.1";
        const int default_port = 3123;

        public enum MessageType { GetResult, SendFile };

        private static IPEndPoint Address = new IPEndPoint(IPAddress.Parse(default_ip), default_port);
        public static string BranchName;
        public static int BranchIndex = -1;
        
        public static void SendRequest(MessageType messageType, string file_path)
        {
            using (UdpClient client = new UdpClient())
            {
                byte[] type = BitConverter.GetBytes((int)messageType);
                client.Send(type, type.Length, Address);

                if (messageType == MessageType.SendFile)
                {
                    byte[] id = BitConverter.GetBytes(BranchIndex);
                    client.Send(id, id.Length, Address);

                    byte[] file = File.ReadAllBytes(file_path);
                    client.Send(file, file.Length, Address);
                }
            }
        }

        public static string GetResponse()
        {
            using (UdpClient client = new UdpClient(Address))
            {
                IPEndPoint addr = null;
                byte[] data = client.Receive(ref addr);
                return Encoding.Unicode.GetString(data);
            }
        }

        public static void ParseIp(string file_path)
        {
            if (!File.Exists(file_path))
            {
                File.WriteAllText(file_path, default_ip + ":" + default_port.ToString());
                return;
            }
            string[] ip_port = File.ReadAllText(file_path).Split(':');

            if (IPAddress.TryParse(ip_port[0], out IPAddress ip) && int.TryParse(ip_port[1], out int port))
                Address = new IPEndPoint(ip, port);
        }

        public static void Serialize(DataGridView[] Tables, string file_path)
        {
            using (FileStream file = File.Create(file_path))
            {
                for (int i = 0; i < Tables.Length; i++)
                {
                    for (int y = 0; y < Tables[i].RowCount; y++)
                    {
                        for (int x = 0; x < Tables[i].ColumnCount; x++)
                        {
                            byte[] buffer = Encoding.Unicode.GetBytes(Tables[i][x, y].Value.ToString());
                            file.Write(buffer, 0, buffer.Length);
                            file.WriteByte(0x02);
                            file.WriteByte(0xA8);
                        }
                    }
                }
            }
        }

        public static void Deserialize(DataGridView[] Tables, string file_path)
        {
            if (!File.Exists(file_path))
                return;
            using (FileStream file = File.OpenRead(file_path))
            {
                for (int i = 0; i < Tables.Length; i++)
                {
                    for (int y = 0; y < Tables[i].RowCount; y++)
                    {
                        for (int x = 0; x < Tables[i].ColumnCount; x++)
                        {
                            List<byte> buffer = new List<byte>(byte.MaxValue);
                            byte[] bytes = new byte[2];
                            while (file.Read(bytes, 0, 2) == 2)
                            {
                                if (bytes[0] == 0x02 && bytes[1] == 0xA8)
                                    break;
                                buffer.AddRange(bytes);
                            }
                            Tables[i][x, y].Value = Encoding.Unicode.GetString(buffer.ToArray());
                        }
                    }
                }
            }
        }
    }
}
