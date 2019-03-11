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

namespace OOP
{
    public partial class Login_Form : Form
    {
        public Login_Form(string branches_info_path)
        {
            InitializeComponent();
            if (!File.Exists(branches_info_path))
                File.WriteAllText(branches_info_path, "Филиал");
            string[] branches = File.ReadAllLines(branches_info_path);
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
            //Не работает
            //DataManager.SendRequest(DataManager.MessageType.GetResult, null);
            //MessageBox.Show(this, DataManager.GetResponse(), "Результат");
        }
    }
}
