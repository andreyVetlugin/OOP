using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void Login_button_Click(object sender, EventArgs e)
        {
            int index = name_comboBox.SelectedIndex;
            if (index < 0)
                return;
            
            DataManager.BranchIndex = index;
            DataManager.BranchName = name_comboBox.Text;
            Close();
        }
    }
}
