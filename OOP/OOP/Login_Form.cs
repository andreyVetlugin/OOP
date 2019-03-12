using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace OOP
{
    public partial class Login_Form : Form
    {
        private string ip_path;

        public Login_Form()
        {
            InitializeComponent();
            if (!File.Exists(Main_Form.branches_info_path))
                File.WriteAllText(Main_Form.branches_info_path, "Филиал");
            string[] branches = File.ReadAllLines(Main_Form.branches_info_path);
            branches_comboBox.Items.AddRange(branches);
        }

        private void Login_button_Click(object sender, EventArgs e)
        {
            int index = branches_comboBox.SelectedIndex;
            if (index < 0)
                return;
            
            DataManager.BranchIndex = index;
            DataManager.BranchName = branches_comboBox.Text;
            Close();
        }

        private void Show_result_button_Click(object sender, EventArgs e)
        {
            IPEndPoint address = DataManager.ParseIp(Main_Form.ip_info_path);
            if (!DataManager.ConnectToServer(address))
            {
                MessageBox.Show(this, "Не удалось подключиться к серверу\n\rПопробуйте позже",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataManager.SendRequest(DataManager.MessageType.GetResult, null);
            DataManager.GetResponse("result.dat");
            DataManager.DisconnectFromServer();
        }
    }
}
