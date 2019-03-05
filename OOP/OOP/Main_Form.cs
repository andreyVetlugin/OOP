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
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();

            first_dataGridView.Rows.Add("Промышленность", "0", "0", "0", "0", "0", "0");
            first_dataGridView.Rows.Add("Население", "0", "0", "0", "0", "0", "0");
            first_dataGridView.Rows.Add("Бюджет", "0", "0", "0", "0", "0", "0");
            first_dataGridView.Rows.Add("ОПП, ЖКХ и др.", "0", "0", "0", "0", "0", "0");
            first_dataGridView.Rows.Add("Прочее", "0", "0", "0", "0", "0", "0");
        }

        private void button_recount_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < first_dataGridView.Rows.Count; i++)
            {
                if (!int.TryParse(first_dataGridView[1, i].Value.ToString(), out int first_param))
                    return;
                if (!int.TryParse(first_dataGridView[2, i].Value.ToString(), out int second_param))
                    return;
                if (first_param <= 0 || second_param <= 0)
                    return;
                first_dataGridView[3, i].Value = (double)first_param * 365 / second_param;
            }
        }
    }
}
