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
    public partial class Form1 : Form
    {
        const string tables_data_path = "tables.dat";
        public Form1()
        {
            InitializeComponent();

            // получение данных и десириализация ..в branches запихиваем массив таблиц для каждого филиала

            //Branch[] branches = new Branch[6];
            //BranchManager branchManager = new BranchManager(branches);
            //branchManager.CalcualateBranchesRating();


            DataGridView[] Tables = new DataGridView[] {
                first_dataGridView, second_dataGridView, third_dataGridView,
                fourth_dataGridView, fifth_dataGridView, sixth_dataGridView,
                seventh_dataGridView, eighth_dataGridView, ninth_dataGridView,
                tenth_dataGridView };

            JustForTests.Deserialize(Tables, tables_data_path);

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
                Tables[4].Rows.Add(i, "0", "0", "0", "0", "0", "0");
                Tables[5].Rows.Add(i, "0", "0");
                Tables[6].Rows.Add(i, "0", "0", "0", "0");
                Tables[7].Rows.Add(i, "Инциденты", "0", "0", "0", "0", "0", "0");
                Tables[7].Rows.Add(i, "Техника безопасности", "0", "0", "0", "0", "0", "0");
                Tables[8].Rows.Add(i, "Коффициент текучести кадров", "0", "0", "0", "0", "0", "0");
                Tables[8].Rows.Add(i, "Качество обучения", "0", "0", "0", "0", "0", "0");
            }
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
                if ((int)dataGrid[0, e.RowIndex].Value % 2 == 0)
                    e.CellStyle.BackColor = Color.Lavender;
            }
        }
    }
}





public class JustForTests
{
    public static void Deserialize(DataGridView[] Tables, string file_path)
    {
        if (!File.Exists(file_path))
            return;
        using (FileStream file = File.OpenRead(file_path))
        {
            for (int i = 0; i < Tables.Length; i++)
            {
                for (int y = 0; y < Tables[i].RowCount; y++)
                {
                    for (int x = 0; x < Tables[i].ColumnCount; x++)
                    {
                        List<byte> buffer = new List<byte>(byte.MaxValue);
                        byte[] bytes = new byte[2];
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
    }
}

public class BranchManager
{
    private Branch[] branches;
    public BranchManager(Branch[] branches)
    {
        this.branches = branches;
    }

    public void CalcualateBranchesRating()
    {
        // для всех таблиц вызов CalcualateBranchesRatingForTable
    }
    private void CalcualateBranchesRatingForTable(int tableNumber/*, int finallyScoreColumn = 5*/)
    {

        //var distributionRaiting = GetDistributionRaiting(); туда кинуть данные суммарные баллы по параметрам

        for (int i = 0; i < branches.Length; i++)
        {

        }
    }


    private void CalculateParametersRaiting(int tableNumber, int parameterColumn = 3)
    {
        double[] possibleValues = new double[branches.Length];
        for (var i = 0; i < branches[0].Tables[tableNumber].RowCount; i++)
        {
            var distributionRaiting = GetDistributionRaiting(tableNumber, parameterColumn, i);
            foreach (var branch in branches)
            {
                var paramValue = GetCellValue(branch.Tables[tableNumber][3, i]);
                branch.TableParametersRating[tableNumber][i] = distributionRaiting[paramValue];
            }
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
        Dictionary<double, int> distribution = new Dictionary<double, int>();
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
    public int[] TablesRatings;
    public Dictionary<int, int[]> TableParametersRating; //заменить на dictionary<DataGridView,int[]>;?
    public Branch(DataGridView[] tables)
    {
        Tables = tables;
    }
    public Branch()
    { }
}

