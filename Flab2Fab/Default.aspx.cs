using Flab2Fab.App_Data;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Flab2Fab
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable leaderDt = LoadLeaderboardTable();
            LoadMoneyInThePot(leaderDt);
        }

        private void LoadMoneyInThePot(DataTable leaderDt)
        {
            //total participants * 100 - earnings of all participants
            decimal totalParticipants = leaderDt.Rows.Count;
            decimal totalEarnings = 0;
            foreach (DataRow row in leaderDt.Rows)
            {
                totalEarnings += Convert.ToDecimal(row["Earnings"].ToString().Replace("$", ""));
            }
            decimal moneyInThePot = totalParticipants * 100 - totalEarnings;
            phMoneyInPot.Controls.Add(new LiteralControl(moneyInThePot.ToString("C")));
        }

        private DataTable LoadLeaderboardTable()
        {
            IWeightDataAccess _weightDataAccess = new WeightDataAccess();
            DataTable dt = _weightDataAccess.GetRecentWeightForAllUsers();
            AddPositionColumn(dt);
            FormatLeaderboard(dt);
            AddEarningsColumn(dt);
            Leaderboard.DataSource = dt;
            Leaderboard.DataBind();
            return dt;
            //TODO: resize the position column to be smaller
        }

        private void AddPositionColumn(DataTable dt)
        {
            dt.Columns.Add("Position", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Assign the row number (starting from 1) to the "Position" column
                dt.Rows[i]["Position"] = i + 1;
            }
            dt.Columns["Position"].SetOrdinal(0); // Move the "Position" column to the first column in the table
        }

        private void AddEarningsColumn(DataTable dt)
        {
            DataColumn earnings = new DataColumn("Earnings", typeof(string));
            dt.Columns.Add(earnings);

            foreach (DataRow row in dt.Rows)
            {

                string pattern = @"[^0-9\.\-]"; // Regular expression pattern to match characters that are not numbers, a decimal point, or a negative sign
                decimal earningRate = 10.0M; // $10 per percent lost
                decimal percentChange = Convert.ToDecimal(Regex.Replace(row["Percent Change"].ToString(), pattern, ""));

                //Calculate earnings
                decimal earningsAmount = Math.Round(percentChange * earningRate, 2) < 0 ? Math.Round(percentChange * -1 * earningRate, 2) : 0;

                //if earnings are above $100 set to $100
                earningsAmount = earningsAmount > 100 ? 100 : earningsAmount;

                // Set the "Earnings" value in the new column
                row["Earnings"] = earningsAmount.ToString("C");
            }
        }

        private void FormatLeaderboard(DataTable dt)
        {
            FormatWeightChangeColumn(dt);
            FormatPercentChangeColumn(dt);
            UpdateAnonymousUsersWeights(dt);
        }

        private void UpdateAnonymousUsersWeights(DataTable dt)
        {

            //Hide weight change column values if user is anonymous
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i]["Anonymous"]))
                {
                    dt.Rows[i]["Weight Change"] = "N/A";
                }
            }
            dt.Columns.Remove("Anonymous");
        }

        private void FormatPercentChangeColumn(DataTable dt)
        {
            // Step 1: Copy the values of the "PercentChange" column into a temporary object
            decimal[] percentChangeValues = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                percentChangeValues[i] = Convert.ToDecimal(dt.Rows[i]["PercentChange"]);
            }

            // Step 2: Clear the data from the "PercentChange" column in the DataTable
            dt.Columns.Remove("PercentChange");
            dt.Columns.Add("Percent Change", typeof(string)); // Change the data type to string

            // Step 3: Round values to nearest hundredth and add the values back to the DataTable column as strings
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Percent Change"] = Math.Round(percentChangeValues[i], 2).ToString() + "%";
            }
        }

        private void FormatWeightChangeColumn(DataTable dt)
        {
            // Step 1: Copy the values of the "WeightChange" column into a temporary object
            decimal[] weightChangeValues = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                weightChangeValues[i] = Convert.ToDecimal(dt.Rows[i]["WeightChange"]);
            }

            // Step 2: Clear the data from the "WeightChange" column in the DataTable
            dt.Columns.Remove("WeightChange");
            dt.Columns.Add("Weight Change", typeof(string)); // Change the data type to string

            // Step 3: Round values to nearest hundredth and add the values back to the DataTable column as strings
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Weight Change"] = Math.Round(weightChangeValues[i], 1).ToString() + "lbs";
            }
        }
    }
}