﻿using System;
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
        const string result_file_name = "result.dat";

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
            DataManager.BranchIndex = branches_comboBox.SelectedIndex;
            if (DataManager.BranchIndex < 0)
                return;
            DataManager.QuarterIndex = quarters_comboBox.SelectedIndex;
            if (DataManager.QuarterIndex < 0)
                return;
            
            DataManager.BranchName = branches_comboBox.Text;
            Close();
        }

        private void Show_result_button_Click(object sender, EventArgs e)
        {
            DataManager.QuarterIndex = quarters_comboBox.SelectedIndex;
            if (DataManager.QuarterIndex < 0)
            {
                MessageBox.Show(this, "Для начала выберите квартал",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ConnectAndGetResponse(result_file_name);
            Result_Form result_Form = new Result_Form(result_file_name);
            result_Form.ShowDialog();
            File.Delete(result_file_name);
        }

        private void Save_result_button_Click(object sender, EventArgs e)
        {
            DataManager.QuarterIndex = quarters_comboBox.SelectedIndex;
            if (DataManager.QuarterIndex < 0)
            {
                MessageBox.Show(this, "Для начала выберите квартал",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            saveFileDialog.FileName = "";
            saveFileDialog.ShowDialog(this);
            if (saveFileDialog.FileName == "")
                return;
            ConnectAndGetResponse(saveFileDialog.FileName);
        }

        private void ConnectAndGetResponse(string result_path)
        {
            IPEndPoint address = DataManager.ParseIp(Main_Form.ip_info_path);
            if (!DataManager.ConnectToServer(address))
            {
                MessageBox.Show(this, "Не удалось подключиться к серверу\r\nПопробуйте позже",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataManager.SendRequest(DataManager.MessageType.GetResult, null);
            DataManager.GetResponse(result_path);
        }
    }
}
