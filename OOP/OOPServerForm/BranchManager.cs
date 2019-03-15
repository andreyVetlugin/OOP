using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPServerForm
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

        private void CalculateParametersRaiting(int tableNumber)
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

    }
    public struct Branch
    {
        public DataGridView[] Tables;

        public Branch(DataGridView[] tables)
        {
            Tables = tables;
        }
    }
}
