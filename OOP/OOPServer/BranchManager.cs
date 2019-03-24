using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPServer
{
    public class BranchManager
    {    
        public Branch[] branches;

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
                branch.Tables[tableNumber].ColumnCount++;
                branch.Tables[tableNumber][parameterColumn + 1, 0].Value = distributionRaiting[paramValue];
            }
        }

        private void CalculateParametersRaiting(int tableNumber)//это не будет норм работать
        {
            int parameterColumn = branches[0].Tables[tableNumber].ColumnCount - 1;
            Dictionary<Branch, int> summRatingForBranches = new Dictionary<Branch, int>();

            foreach (var branch in branches)
            {
                summRatingForBranches[branch] = 0;
                branch.Tables[tableNumber].ColumnCount += 2; // место под балл параметра и балл суммарный
            }
            for (var i = 0; i < branches[0].Tables[tableNumber].RowCount; i++)
            {
                var distributionRaiting = GetDistributionRaiting(tableNumber, parameterColumn,true, i);
                foreach (var branch in branches)
                {
                    var paramValue = GetCellValue(branch.Tables[tableNumber][parameterColumn, i]);
                    branch.Tables[tableNumber][parameterColumn + 1, i].Value = distributionRaiting[paramValue];
                    summRatingForBranches[branch] += distributionRaiting[paramValue];
                }
            }
            foreach (var branch in branches)
                branch.Tables[tableNumber][parameterColumn + 2, 0].Value = summRatingForBranches[branch];

        }

        private double GetCellValue(DataGridViewCell cell)
        {
            Double.TryParse(cell.Value.ToString(), out double value);
            return value;
        }

        private Dictionary<double, int> GetDistributionRaiting(int tableNumber, int parameterColumn, bool reverseRaitingCheck = false, int parameterRow = 0)// какая
        {
            double[] possibleValues = new double[branches.Length];
            for (var j = 0; j < branches.Length; j++)
                possibleValues[j] = GetCellValue(branches[j].Tables[tableNumber][parameterColumn, parameterRow]);
            Array.Sort(possibleValues);            
            return GetDistributionRaiting(possibleValues, reverseRaitingCheck && (int)TableTypes[tableNumber] % 2 == 1);
        }

        private Dictionary<double, int> GetDistributionRaiting(double[] possibleValues,bool reverseRaiting = false)
        {            
            Array.Sort(possibleValues);
            if (reverseRaiting)
                Array.Reverse(possibleValues);
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

        public void FillTables(DataGridView[] Tables)
        {
            for (int tableNum = 0; tableNum < Tables.Length - 1; tableNum++)
                for (int branchNum = 0; branchNum < branches.Length; branchNum++)
                    for (var i = 1; i < Tables[tableNum].ColumnCount; i++)
                        for (var j = 0; j < branches[branchNum].Tables[tableNum].RowCount; j++)
                        {
                            if (((int)TableTypes[tableNum] == 0 || (int)TableTypes[tableNum] == 1) && i == 1)
                                continue;
                            var rowIndex = j + branchNum * branches[branchNum].Tables[tableNum].RowCount;
                            Tables[tableNum][i, rowIndex].Value = branches[branchNum].Tables[tableNum][i - 1, j].Value;
                        }
            FillFinallTable(Tables[Tables.Length - 1]);
        }

        private void FillFinallTable(DataGridView table)
        {
            double[] ratingsSums = new double[branches.Length];
            for (var i = 0; i < branches.Length; i++)
            {
                double summOfRatingScores = 0;
                for (var j = 0; j < branches[i].Tables.Length; j++)
                {
                    var columnCount = branches[i].Tables[j].ColumnCount;
                    table[3 + i, j].Value = branches[i].Tables[j][columnCount - 1, 0].Value;
                    summOfRatingScores += GetCellValue(table[3 + i, j]) * GetCellValue(table[2, j]);
                }
                table[3 + i, branches[i].Tables.Length].Value = summOfRatingScores;
                ratingsSums[i] = summOfRatingScores;
            }
            var disributionRating = GetDistributionRaiting(ratingsSums,true);
            for (var i = 0; i < branches.Length; i++)
                table[i + 3, branches[i].Tables.Length + 1].Value = disributionRating[GetCellValue(table[i + 3, branches[i].Tables.Length])];
        }



        public void FillAnnualTables(Branch[][] branches, DataGridView[] tables)// первое измерение - количество реальных филиалов, второе - кваратл (т.е.[6][4] )
        {            
            for (var i = 0; i < branches.Length; i++)
                this.branches[i]= Branch.GetSumTables(branches[i]);
            FillNewResults(tables);
            //после этого вызов того что и для обычной процедуры расчета рейтинга
        }

        private void FillNewResults(DataGridView[] tables)// передавать функцию или проверять внутри что делать с параметрами
        {// или так же сделать getFillers?
            double[] parameters;
            for (int i = 0; i < tables[0].RowCount; i++)
            {
                parameters = new double[] { GetCellValue(tables[0][1, i]), GetCellValue(tables[0][2, i]) };
                tables[0][3, i].Value = parameters[0] * 365 / parameters[1];
            }

            for (int i = 0; i < tables[1].RowCount; i++)
            {
                parameters = new double[] { GetCellValue(tables[1][0, i]), GetCellValue(tables[1][1, i]) };
                tables[1][2, i].Value = parameters[0] / parameters[1] * 100;
                var cellValue = (double)tables[1][2, i].Value;
                if (cellValue >= 120)
                    tables[1][3, i].Value = 6;
                else if (cellValue >= 110)
                    tables[1][3, i].Value = 5;
                else if (cellValue >= 100)
                    tables[1][3, i].Value = 4;
                else if (cellValue >= 95)
                    tables[1][3, i].Value = 3;
                else if (cellValue >= 90)
                    tables[1][3, i].Value = 2;
                else
                    tables[1][3, i].Value = 1;
            }


            for (var i = 0; i < tables[2].RowCount; i++)
            {
                parameters = new double[] { GetCellValue(tables[2][1, i]), GetCellValue(tables[2][2, i]) };
                tables[2][3, i].Value = parameters[0] / parameters[1];
            }

            for (var i = 0; i < tables[3].RowCount; i++)
            {
                parameters = new double[] { GetCellValue(tables[3][1, i]), GetCellValue(tables[3][2, i]), GetCellValue(tables[3][3, i]) };
                tables[3][4, i].Value = parameters[0] / parameters[1] * parameters[2] * 100;
            }
            //tables[4][0, 0].Value = tables[3][2, 0].Value;// ???
            //tables[4][1, 0].Value = tables[3][2, 1].Value;//   ????        

            for (var i = 0; i < tables[4].RowCount; i++)
            {
                parameters= new double[]{ GetCellValue(tables[4][0, i]), GetCellValue(tables[4][1, i]), GetCellValue(tables[4][3, i])};                
                if (parameters[2] > 0)
                    tables[4][4, i].Value = (parameters[0] + parameters[1]) / parameters[2];
            }

            for (var i = 0; i < tables[5].RowCount; i++)
            {
                // !!!здесь навреное в заголовке таблицы убрать полугодие  и просто суммировать
            }

            for (var i = 0; i < tables[6].RowCount; i++)
            {
                parameters = new double[]{GetCellValue(tables[6][0, i]), GetCellValue(tables[6][1, i])};                
                tables[6][2, i].Value = parameters[0] / parameters[1];
            }

            for (int i = 0; i < tables[7].Rows.Count; i++)
            {
                parameters = new double[] {GetCellValue(tables[7][1, i]), GetCellValue(tables[7][2, i])};                
                tables[7][3, i].Value = parameters[0] / parameters[1];
            }

            for (int i = 0; i < tables[8].Rows.Count; i++)
            {
                parameters =new double[] {GetCellValue(tables[8][0, i]),GetCellValue(tables[8][1, i])};               
                tables[8][2, 0].Value = parameters[0] / parameters[1] * 100;
            }
        }
    }
    public class Branch
    {
        public DataGridView[] Tables;

        public Branch(DataGridView[] tables)
        {
            Tables = tables;
        }

        static public Branch GetSumTables(Branch[] branches)
        {
            for (var branchN = 1; branchN < branches.Length; branchN++)
                for (var tableN = 0; tableN < branches[0].Tables.Length; tableN++)
                    for (var i = 0; i < branches[0].Tables[tableN].RowCount; i++)
                        for (var j = 0; j < branches[0].Tables[tableN].ColumnCount; j++)
                        {
                            var currentCell = branches[0].Tables[tableN][j, i];
                            if (double.TryParse(currentCell.Value.ToString(), out double param))
                                currentCell.Value = (double)branches[branchN].Tables[tableN][j, i].Value + (double)currentCell.Value;
                        }
            return branches[0];
        }
    }
}
