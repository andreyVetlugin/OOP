using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace OOP
{
    public partial class Main_Form : Form
    {
        private DataGridView[] Tables;
        private TableManager tableManager;

        public Main_Form()
        {
            InitializeComponent();

            Tables = new DataGridView[] {
                first_dataGridView, second_dataGridView, third_dataGridView,
                fourth_dataGridView, fifth_dataGridView, sixth_dataGridView,
                seventh_dataGridView, eighth_dataGridView };

            TableManager.InitializeTables(Tables);
            tableManager = new TableManager(Tables);
        }

        private void sendTables()// сюда пока не смотреть 
        {
            WebRequest request = WebRequest.Create("HERE SERVER");
            request.Method = "POST";
            Stream dataStream = request.GetRequestStream();
            // dataStream.Write();
            dataStream.Close();
            WebResponse response = request.GetResponse();
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tableManager.FillTable((DataGridView)sender);
        }
    }

    public class TableManager
    {
        private delegate void TableFiller();
        private Dictionary<DataGridView, TableFiller> tablesFillers = new Dictionary<DataGridView, TableFiller>();

        public TableManager(DataGridView[] tables)
        {
            TableFiller[] TableFillers = getTableFillers(tables);
            for (int i = 0; i < tables.Length; i++)
                tablesFillers[tables[i]] = TableFillers[i];
        }

        public static void InitializeTables(DataGridView[] Tables)
        {
            Tables[0].Rows.Add("Промышленность", "0", "0", "0");
            Tables[0].Rows.Add("Население", "0", "0", "0");
            Tables[0].Rows.Add("Бюджет", "0", "0", "0");
            Tables[0].Rows.Add("ОПП, ЖКХ и др.", "0", "0", "0");
            Tables[0].Rows.Add("Прочее", "0", "0", "0");
            Tables[1].Rows.Add("0", "0", "0", "0");
            Tables[2].Rows.Add("Физическое", "0", "0", "0");
            Tables[2].Rows.Add("Юридическое", "0", "0", "0");
            Tables[3].Rows.Add("Физическое", "0", "0", "1", "0");
            Tables[3].Rows.Add("Юридическое", "0", "0", "1", "0");
            Tables[4].Rows.Add("0", "0", "0", "0", "0");
            Tables[5].Rows.Add("0");
            Tables[6].Rows.Add("0", "0", "0");
            Tables[7].Rows.Add("Инциденты", "0", "0", "0");
            Tables[7].Rows.Add("Техника безопасности", "0", "0", "0");
        }

        public void FillTable(DataGridView table)
        {
            tablesFillers[table]();
        }
        
        private TableFiller[] getTableFillers(DataGridView[] tables)
        {
            TableFiller[] tablesFillers = new TableFiller[tables.Length];
            tablesFillers[0] = () =>
            {
                int first_param, second_param;
                for (int i = 0; i < tables[0].Rows.Count; i++)
                {
                    if (!int.TryParse(tables[0][1, i].Value.ToString(), out first_param))
                        continue;
                    if (!int.TryParse(tables[0][2, i].Value.ToString(), out second_param))
                        continue;
                    if (first_param <= 0 || second_param <= 0)
                        continue;
                    tables[0][3, i].Value = (double)first_param * 365 / second_param;
                }
            };

            tablesFillers[1] = () =>
            {
                int first_param, second_param;
                if (!int.TryParse(tables[1][0, 0].Value.ToString(), out first_param))
                    return;
                if (!int.TryParse(tables[1][1, 0].Value.ToString(), out second_param))
                    return;
                if (first_param <= 0 || second_param <= 0)
                    return;
                tables[1][2, 0].Value = (double)first_param / second_param * 100;

                if ((double)tables[1][2, 0].Value >= 120)
                    tables[1][3, 0].Value = 6;
                else if ((double)tables[1][2, 0].Value >= 110)
                    tables[1][3, 0].Value = 5;
                else if ((double)tables[1][2, 0].Value >= 100)
                    tables[1][3, 0].Value = 4;
                else if ((double)tables[1][2, 0].Value >= 95)
                    tables[1][3, 0].Value = 3;
                else if ((double)tables[1][2, 0].Value >= 90)
                    tables[1][3, 0].Value = 2;
                else
                    tables[1][3, 0].Value = 1;
            };

            tablesFillers[2] = () =>
            {
                int first_param, second_param;
                for (var i = 0; i < tables[2].Rows.Count; i++)
                {
                    if (!int.TryParse(tables[2][1, i].Value.ToString(), out first_param))
                        continue;
                    if (!int.TryParse(tables[2][2, i].Value.ToString(), out second_param))
                        continue;
                    if (first_param <= 0 || second_param <= 0)
                        continue;
                    tables[2][3, i].Value = (double)first_param / second_param;
                }
            };

            tablesFillers[3] = () =>
            {                
                int first_param, second_param, third_param;
                for (var i = 0; i < tables[3].Rows.Count; i++)
                {
                    if (!int.TryParse(tables[3][1, i].Value.ToString(), out first_param))
                        continue;
                    if (!int.TryParse(tables[3][2, i].Value.ToString(), out second_param))
                        continue;
                    if (!int.TryParse(tables[3][3, i].Value.ToString(), out third_param))
                        continue;
                    if (first_param <= 0 || second_param <= 0)
                        continue;
                    if (third_param != 1)
                        tables[3][4, i].Value = 0;
                    else
                        tables[3][4, i].Value = (double)first_param / second_param * third_param * 100;
                }
                tables[4][0, 0].Value = tables[3][2, 0].Value;
                tables[4][1, 0].Value = tables[3][2, 1].Value;
                FillTable(tables[4]);
            };

            tablesFillers[4] = () => 
            {
                int first_param, second_param, third_param;
                if (!int.TryParse(tables[4][1, 0].Value.ToString(), out first_param))
                    return;
                if (!int.TryParse(tables[4][0, 0].Value.ToString(), out second_param))
                    return;
                if (!int.TryParse(tables[4][3, 0].Value.ToString(), out third_param))
                    return;
                if (third_param > 0)
                    tables[4][4, 0].Value = (double)(first_param + second_param) / third_param;

                tables[6][1, 0].Value = first_param + second_param;
                FillTable(tables[6]);
            };

            tablesFillers[5] = () => { };

            tablesFillers[6] = () => { };

            tablesFillers[7] = () => { };

            return tablesFillers;
        }
    }
}
