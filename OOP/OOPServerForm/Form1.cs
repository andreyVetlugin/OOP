using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPServerForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DataGridView[] Tables = new DataGridView[] {
                first_dataGridView, second_dataGridView, third_dataGridView,
                fourth_dataGridView, fifth_dataGridView, sixth_dataGridView,
                seventh_dataGridView, eighth_dataGridView, ninth_dataGridView,
                tenth_dataGridView };

            InitializeTables(Tables);
        }

        public static void InitializeTables(DataGridView[] Tables)
        {
            for (int i = 1; i < 7; i++)
            {
                Tables[0].Rows.Add(i, "Промышленность", "0", "0", "0", "0", "0", "0");
                Tables[0].Rows.Add(i, "Население", "0", "0", "0", "0", "0", "0");
                Tables[0].Rows.Add(i, "Бюджет", "0", "0", "0", "0", "0", "0");
                Tables[0].Rows.Add(i, "ОПП, ЖКХ и др.", "0", "0", "0", "0", "0", "0");
                Tables[0].Rows.Add(i, "Прочее", "0", "0", "0", "0", "0", "0");
                Tables[1].Rows.Add(i, "0", "0", "0", "0");
                Tables[2].Rows.Add(i, "Физическое", "0", "0", "0", "0", "0", "0");
                Tables[2].Rows.Add(i, "Юридическое", "0", "0", "0", "0", "0", "0");
                Tables[3].Rows.Add(i, "Физическое", "0", "0", "1", "0", "0", "0", "0");
                Tables[3].Rows.Add(i, "Юридическое", "0", "0", "1", "0", "0", "0", "0");
            }
            Tables[4].Rows.Add("0", "0", "0", "0", "0");
            Tables[5].Rows.Add("0");
            Tables[6].Rows.Add("0", "0", "0");
            Tables[7].Rows.Add("Инциденты", "0", "0", "0");
            Tables[7].Rows.Add("Техника безопасности", "0", "0", "0");
            Tables[8].Rows.Add("0", "0", "0");
            Tables[9].Rows.Add("0", "0", "0");
        }

        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView dataGrid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 || 
                e.ColumnIndex == dataGrid.ColumnCount - 1 || 
                e.ColumnIndex == dataGrid.ColumnCount - 2) && 
                (e.RowIndex < dataGrid.RowCount - 1 && e.RowIndex >= 0))
            {
                if (dataGrid[0, e.RowIndex].Value.ToString() == dataGrid[0, e.RowIndex + 1].Value.ToString())
                    e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                else
                    e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            }
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGrid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 ||
                   e.ColumnIndex == dataGrid.ColumnCount - 1 ||
                   e.ColumnIndex == dataGrid.ColumnCount - 2) &&
                   e.RowIndex > 0)
            {
                if (dataGrid[0, e.RowIndex].Value.ToString() == dataGrid[0, e.RowIndex - 1].Value.ToString())
                {
                    e.Value = "";
                }
            }
        }
    }
}
