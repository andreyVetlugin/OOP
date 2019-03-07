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
    public class TableManager
    {
        private delegate void TableFiller(DataGridView table);
        //private List<DataGridView> tables;
        //private Dictionary<int, string> tablesNames;
        private Dictionary<DataGridView, TableFiller> tablesFillers = new Dictionary<DataGridView, TableFiller>();
        //хранить места ошибок, чтобы потом их вывести + имена для описания () передавать не лист а dict с именами-таблицами
        public TableManager(List<DataGridView> tables)
        {
            var numericTableFillers = getNumericTableFillers();
            for (var i = 0; i < tables.Count; i++)
                tablesFillers[tables[i]] = numericTableFillers[i];
        }
        public void FillTables()
        {
            foreach (var filler in tablesFillers)
                filler.Value(filler.Key);
        }
        private List<TableFiller> getNumericTableFillers()//!!! заменить все проверки данных на функцию
        {
            var tablesFillers = new List<TableFiller>();
            // МОжно ли расчитывать на порядок с add?
            //double\int где что 
            tablesFillers.Add((DataGridView currentTable) =>
            {
                int first_param, second_param;
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    if (!int.TryParse(currentTable[1, i].Value.ToString(), out first_param))
                        continue;
                    if (!int.TryParse(currentTable[2, i].Value.ToString(), out second_param))
                        continue;
                    if (first_param <= 0 || second_param <= 0)
                        continue;
                    currentTable[3, i].Value = (double)first_param * 365 / second_param;
                }
            });

            tablesFillers.Add((DataGridView currentTable) =>
            {
                int first_param, second_param;
                if (!int.TryParse(currentTable[0, 0].Value.ToString(), out first_param))
                    return;
                if (!int.TryParse(currentTable[1, 0].Value.ToString(), out second_param))
                    return;
                if (first_param <= 0 || second_param <= 0)
                    return;
                currentTable[2, 0].Value = (double)first_param / second_param * 100;
            });

            tablesFillers.Add((DataGridView currentTable) =>
            {
                int first_param, second_param;
                for (var i = 0; i < 2; i++)
                {
                    if (!int.TryParse(currentTable[1, i].Value.ToString(), out first_param))
                        return;
                    if (!int.TryParse(currentTable[2, i].Value.ToString(), out second_param))
                        return;
                    if (first_param <= 0 || second_param <= 0)
                        return;
                    currentTable[3, i].Value = (double)first_param / second_param;
                }
            });
            return tablesFillers;
        }
    }
    public partial class Main_Form : Form
    {
        private TableManager tableManager;
        public Main_Form()
        {
            // убрать балл в третьеь таблице
            InitializeComponent();
             // вынести в отдельный метод в мэнеджере?
            first_dataGridView.Rows.Add("Промышленность", "0", "0", "0");
            first_dataGridView.Rows.Add("Население", "0", "0", "0");
            first_dataGridView.Rows.Add("Бюджет", "0", "0", "0");
            first_dataGridView.Rows.Add("ОПП, ЖКХ и др.", "0", "0", "0");
            first_dataGridView.Rows.Add("Прочее", "0", "0", "0");
            second_dataGridView.Rows.Add("0", "0", "0", "0");
            third_dataGridView.Rows.Add("Физическое", "0", "0", "0");
            third_dataGridView.Rows.Add("Юридическое", "0", "0", "0");
            fourth_dataGridView.Rows.Add("Физическое", "0", "0", "0", "0");
            fourth_dataGridView.Rows.Add("Юридическое", "0", "0", "0", "0");

            tableManager = new TableManager(new List<DataGridView> { first_dataGridView, second_dataGridView, third_dataGridView });
        }

        private void button_recount_Click(object sender, EventArgs e)
        {
            tableManager.FillTables();            
        }
    }
}
