using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPServerForm
{
    public enum TableType { Extended, ReverseExtended, Ordinary, ReverseOrdinary, NotFilled }

    public partial class Form1 : Form
    {
        const string tables_data_path = "tables.dat";
        public Form1()
        {
            InitializeComponent();

            DataGridView[] Tables = new DataGridView[] {
                first_dataGridView, second_dataGridView, third_dataGridView,
                fourth_dataGridView, fifth_dataGridView, sixth_dataGridView,
                seventh_dataGridView, eighth_dataGridView, ninth_dataGridView,
                tenth_dataGridView };

            var branchTables = JustForTests.DeserializeToNewtables(tables_data_path);
            Branch[] branches = new Branch[1];
            branches[0] = new Branch(branchTables);
            BranchManager branchManager = new BranchManager(branches);
            branchManager.CalcualateBranchesRating();

            InitializeTables(Tables);
        }

        public void InitializeTables(DataGridView[] Tables)
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
                Tables[4].Rows.Add(i, "0", "0", "0", "0", "0", "0");
                Tables[5].Rows.Add(i, "0", "0");
                Tables[6].Rows.Add(i, "0", "0", "0", "0");
                Tables[7].Rows.Add(i, "Инциденты", "0", "0", "0", "0", "0", "0");
                Tables[7].Rows.Add(i, "Техника безопасности", "0", "0", "0", "0", "0", "0");
                Tables[8].Rows.Add(i, "Коффициент текучести кадров", "0", "0", "0", "0", "0", "0");
                Tables[8].Rows.Add(i, "Качество обучения", "0", "0", "0", "0", "0", "0");
            }

            string[] parametrs_text = new string[9];
            foreach (Control control in Controls)
            {
                if (control is Label)
                {
                    string name = control.Name;
                    if (name.Length != 6)
                        continue;
                    if (!int.TryParse(name.Substring(5), out int index))
                        continue;
                    parametrs_text[index - 1] = control.Text.Remove(control.Text.Length - 1);
                }
            }
            for (int i = 1; i < parametrs_text.Length + 1; i++)
                Tables[9].Rows.Add(i, parametrs_text[i - 1], "0", "0", "0", "0", "0", "0", "0");

            Tables[9].Rows.Add("", "Сумма баллов с учетом веса", "", "0", "0", "0", "0", "0", "0");
            Tables[9].Rows.Add("", "Итоговое местов рейтинге", "", "0", "0", "0", "0", "0", "0");
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
            Common_dataGridView_CellFormatting(sender, e);
            DataGridView dataGrid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 ||
                   e.ColumnIndex == dataGrid.ColumnCount - 1 ||
                   e.ColumnIndex == dataGrid.ColumnCount - 2) &&
                   e.RowIndex > 0)
            {
                if (dataGrid[0, e.RowIndex].Value.ToString() == dataGrid[0, e.RowIndex - 1].Value.ToString())
                    e.CellStyle.ForeColor = e.CellStyle.BackColor;
            }
        }

        private void Common_dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGrid = (DataGridView)sender;
            if (e.RowIndex >= 0)
            {
                if (int.TryParse(dataGrid[0, e.RowIndex].Value.ToString(), out int value))
                {
                    if (value % 2 == 0)
                        e.CellStyle.BackColor = Color.Lavender;
                }
                else
                    e.CellStyle.BackColor = Color.MistyRose;
            }
        }
    }
    public class JustForTests
    {
        public static DataGridView[] DeserializeToNewtables(string file_path)
        {
            DataGridView[] Tables = new DataGridView[10];
            if (!File.Exists(file_path))
                return Tables; // ПЕРЕДЕЛать
            using (FileStream file = File.OpenRead(file_path))
            {
                for (int i = 0; i < 10; i++)
                {
                    Tables[i] = new DataGridView();
                    List<byte> buffer = new List<byte>(byte.MaxValue);
                    byte[] bytes = new byte[4];
                    file.Read(bytes, 0, 4);
                    buffer.AddRange(bytes);
                    Tables[i].ColumnCount = (int)bytes[0] + ((int)bytes[1] << 8) + ((int)bytes[2] << 16) + ((int)bytes[3] << 24);
                    file.Read(bytes, 0, 4);
                    buffer.AddRange(bytes);
                    Tables[i].RowCount = (int)bytes[0] + ((int)bytes[1] << 8) + ((int)bytes[2] << 16) + ((int)bytes[3] << 24);
                    for (int y = 0; y < Tables[i].RowCount; y++)
                    {
                        for (int x = 0; x < Tables[i].ColumnCount; x++)
                        {
                            buffer = new List<byte>(byte.MaxValue);
                            bytes = new byte[2];
                            while (file.Read(bytes, 0, 2) == 2)
                            {
                                if (bytes[0] == 0x02 && bytes[1] == 0xA8)
                                    break;
                                buffer.AddRange(bytes);
                            }
                            Tables[i][x, y].Value = Encoding.Unicode.GetString(buffer.ToArray());
                        }
                    }
                }
            }
            return Tables;
        }
    }
    public class BranchManager
    {
        private Branch[] branches;
        private static TableType[] TableTypes = {
            TableType.ReverseExtended,
            TableType.NotFilled,
            TableType.Extended,
            TableType.ReverseExtended,
            TableType.Ordinary,
            TableType.Ordinary,
            TableType.ReverseOrdinary,
            TableType.ReverseExtended,
            TableType.ReverseExtended };
        public BranchManager(Branch[] branches)
        {
            this.branches = branches;
        }
        public void CalcualateBranchesRating()
        {
            for (int i = 0; i < TableTypes.Length; i++)
            {
                if ((int)TableTypes[i] == 4)
                    continue;
                else if ((int)TableTypes[i] == 0 || (int)TableTypes[i] == 1)
                    CalculateParametersRaiting(i);
                CalculateFinallRating(i);
            }
        }
        private void CalculateFinallRating(int tableNumber)
        {
            int parameterColumn = branches[0].Tables[tableNumber].ColumnCount - 1;
            var distributionRaiting = GetDistributionRaiting(tableNumber, parameterColumn);
            foreach (var branch in branches)
            {
                var paramValue = GetCellValue(branch.Tables[tableNumber][parameterColumn, 0]);
                branch.Tables[tableNumber].ColumnCount++;// можно просто инкременить count?
                branch.Tables[tableNumber][parameterColumn + 1, 0].Value = distributionRaiting[paramValue];
            }
        }
        private void CalculateParametersRaiting(int tableNumber)// table number от 0
        {
            int parameterColumn = branches[0].Tables[tableNumber].ColumnCount - 1;
            Dictionary<Branch, int> summRatingForBranches = new Dictionary<Branch, int>();

            foreach (var branch in branches)
                summRatingForBranches[branch] = 0;
            for (var i = 0; i < branches[0].Tables[tableNumber].RowCount; i++) //строки с различными показателями
            {
                var distributionRaiting = GetDistributionRaiting(tableNumber, parameterColumn, i);
                foreach (var branch in branches)
                {
                    var paramValue = GetCellValue(branch.Tables[tableNumber][parameterColumn, i]);
                    branch.Tables[tableNumber].ColumnCount++;
                    branch.Tables[tableNumber][parameterColumn + 1, i].Value = distributionRaiting[paramValue];
                    summRatingForBranches[branch] += distributionRaiting[paramValue];
                }
            }
            foreach (var branch in branches)
            {
                branch.Tables[tableNumber].ColumnCount++;
                branch.Tables[tableNumber][parameterColumn + 2, 0].Value = summRatingForBranches[branch];
            }
        }
        private double GetCellValue(DataGridViewCell cell)
        {
            Double.TryParse(cell.Value.ToString(), out double value);
            return value;
        }
        private Dictionary<double, int> GetDistributionRaiting(int tableNumber, int parameterColumn, int parameterRow = 0)// какая
        {
            double[] possibleValues = new double[branches.Length];
            for (var j = 0; j < branches.Length; j++)
                possibleValues[j] = GetCellValue(branches[j].Tables[tableNumber][parameterColumn, parameterRow]);
            Array.Sort(possibleValues);
            if ((int)TableTypes[tableNumber] % 2 == 1)
                Array.Reverse(possibleValues);
            return GetDistributionRaiting(possibleValues);
        }
        private Dictionary<double, int> GetDistributionRaiting(double[] possibleValues)
        {
            Dictionary<double, int> distribution = new Dictionary<double, int>();
            for (var i = 0; i < possibleValues.Length; i++)
            {
                try
                {
                    distribution.Add(possibleValues[i], distribution.Count + 1);
                }
                catch { }
            }
            return distribution;
        }
    }
    public class Branch //сделать структурой
    {
        public readonly DataGridView[] Tables;
        public Branch(DataGridView[] tables)
        {
            Tables = tables;
        }
        public Branch()
        { }
    }
}

