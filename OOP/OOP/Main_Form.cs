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
                seventh_dataGridView, eighth_dataGridView, ninth_dataGridView,
                tenth_dataGridView };

            TableManager.InitializeTables(Tables);
            tableManager = new TableManager(Tables);
        }

        //private void sendTables()// сюда пока не смотреть 
        //{
        //    WebRequest request = WebRequest.Create("");
        //    request.Method = "POST";
        //    Stream dataStream = request.GetRequestStream();
        //    // dataStream.Write();
        //    dataStream.Close();
        //    WebResponse response = request.GetResponse();
        //}

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
            Tables[8].Rows.Add("0", "0", "0");
            Tables[9].Rows.Add("0", "0", "0");
        }

        public void FillTable(DataGridView table)
        {
            tablesFillers[table]();
        }

        private bool tryParseCells(DataGridViewCell[] cells, out int[] tableParams)
        {
            tableParams = new int[cells.Length];            
            for (int i = 0; i < cells.Length; i++)
            {
                if (!int.TryParse(cells[i].Value.ToString(), out tableParams[i]) || tableParams[i] < 0) // нет проверки равенста с нгулем!(есть случаи, где 0 допустимо)
                    return false;
            }
            return true;
        }
        private bool tryParseCells(DataGridViewCell[] cells, out double[] tableParams)
        {
            tableParams = new double[cells.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                if (!double.TryParse(cells[i].Value.ToString(), out tableParams[i]) || tableParams[i] < 0)// нет проверки равенста с нгулем!(есть случаи, где 0 допустимо)
                    return false;
            }
            return true;
        }        
        private TableFiller[] getTableFillers(DataGridView[] tables)
        {           
            TableFiller[] tablesFillers = new TableFiller[tables.Length];
            tablesFillers[0] = () =>
            {
                double[] parameters;
                DataGridViewCell[] cells;                
                for (int i = 0; i < tables[0].Rows.Count; i++)
                {
                    cells = new DataGridViewCell[]{ tables[0][1, i],tables[0][2, i]};
                    if (!tryParseCells(cells,out parameters)||parameters[1]<=0)
                        continue;                    
                    tables[0][3, i].Value = parameters[0] * 365 / parameters[1];
                }
            };

            tablesFillers[1] = () =>
            {
                double[] parameters;
                var cells= new DataGridViewCell[]{ tables[1][0, 0] , tables[1][1, 0] };
                if(!tryParseCells(cells,out parameters)|| parameters[1] <= 0)
                    return;
                tables[1][2, 0].Value = parameters[0] / parameters[1]* 100;
                var cellValue = (double)tables[1][2, 0].Value;
                if (cellValue >= 120)
                    tables[1][3, 0].Value = 6;
                else if (cellValue >= 110)
                    tables[1][3, 0].Value = 5;
                else if (cellValue >= 100)
                    tables[1][3, 0].Value = 4;
                else if (cellValue >= 95)
                    tables[1][3, 0].Value = 3;
                else if (cellValue >= 90)
                    tables[1][3, 0].Value = 2;
                else
                    tables[1][3, 0].Value = 1;
            };

            tablesFillers[2] = () =>
            {
                int documentsCount=0;
                int[] parameters;
                DataGridViewCell[] cells;
                for (var i = 0; i < tables[2].Rows.Count; i++)
                {
                    cells = new DataGridViewCell[] {tables[2][1, i], tables[2][2, i]};
                    if (!tryParseCells(cells, out parameters)|| parameters[1] <= 0)
                        continue;
                    tables[2][3, i].Value = (double)parameters[0] / parameters[1];
                    documentsCount += parameters[1];
                }
                tables[6][1, 0].Value = documentsCount;
                FillTable(tables[6]);
            };

            tablesFillers[3] = () =>
            {
                int[] parameters;
                DataGridViewCell[] cells;                
                for (var i = 0; i < tables[3].Rows.Count; i++)
                {
                    cells = new DataGridViewCell[] { tables[3][1, i], tables[3][2, i], tables[3][3, i] };
                    if (!tryParseCells(cells, out parameters) || parameters[1]<=0)
                        continue;                                            
                    tables[3][4, i].Value = (double)parameters[0] / parameters[1]* parameters[2] * 100;
                }
                tables[4][0, 0].Value = tables[3][2, 0].Value;
                tables[4][1, 0].Value = tables[3][2, 1].Value;
                FillTable(tables[4]);
            };

            tablesFillers[4] = () => 
            {
                int[] parameters;
                var cells = new DataGridViewCell[] { tables[4][0, 0], tables[4][1, 0], tables[4][3, 0] };
                if (!tryParseCells(cells, out parameters))
                    return;
                if (parameters[2]> 0)
                    tables[4][4, 0].Value = (double)(parameters[0] + parameters[1]) / parameters[2];
                //tables[6][1, 0].Value = parameters[0]+ parameters[1]; проверить! перенес в filler[2]
                FillTable(tables[6]);
            };

            tablesFillers[5] = () => { };

            tablesFillers[6] = () =>
            {
                int[] parameters;
                DataGridViewCell[] cells = new DataGridViewCell[] { tables[6][0, 0], tables[6][1, 0] };
                if (!tryParseCells(cells, out parameters) || parameters[1] <= 0)
                    return;
                tables[6][2, 0].Value = (double)parameters[0] / parameters[1];
            };

            tablesFillers[7] = () => 
            {
                int[] parameters;
                for(int i = 0; i < tables[7].Rows.Count; i++)
                {
                    var cells = new DataGridViewCell[] { tables[7][1, i], tables[7][2, i] };
                    if (!tryParseCells(cells, out parameters) || parameters[1] <= 0)
                        return;
                    tables[7][3, i].Value = (double)parameters[0] / parameters[1];
                }
                
            };

            tablesFillers[8] = () =>
            {

            };

            tablesFillers[9] = () =>
            {

            };

            return tablesFillers;
        }
    }
}
