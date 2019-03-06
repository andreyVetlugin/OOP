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

            first_dataGridView.Rows.Add("Промышленность", "0", "0", "0");
            first_dataGridView.Rows.Add("Население", "0", "0", "0");
            first_dataGridView.Rows.Add("Бюджет", "0", "0", "0");
            first_dataGridView.Rows.Add("ОПП, ЖКХ и др.", "0", "0", "0");
            first_dataGridView.Rows.Add("Прочее", "0", "0", "0");
            second_dataGridView.Rows.Add("0", "0", "0", "0");
        }

        private void button_recount_Click(object sender, EventArgs e)
        {
            int first_param, second_param;
            for (int i = 0; i < first_dataGridView.Rows.Count; i++)
            {
                if (!int.TryParse(first_dataGridView[1, i].Value.ToString(), out first_param))
                    continue;
                if (!int.TryParse(first_dataGridView[2, i].Value.ToString(), out second_param))
                    continue;
                if (first_param <= 0 || second_param <= 0)
                    continue;
                first_dataGridView[3, i].Value = (double)first_param * 365 / second_param;
            }

            if (!int.TryParse(second_dataGridView[0, 0].Value.ToString(), out first_param))
                return;
            if (!int.TryParse(second_dataGridView[1, 0].Value.ToString(), out second_param))
                return;
            if (first_param <= 0 || second_param <= 0)
                return;
            second_dataGridView[2, 0].Value = (double)first_param / second_param * 100;
        }
    }
}
