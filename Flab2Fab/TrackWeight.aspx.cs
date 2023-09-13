using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;

using System.Web.UI.WebControls;
using System.Data;
using Microsoft.AspNet.Identity;
using Flab2Fab.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Flab2Fab.App_Data;
using Autofac;
using System.Web.DynamicData;
using System.Text.RegularExpressions;
using Flab2Fab.Helper;
using Microsoft.AspNet.Identity.Owin;

namespace Flab2Fab
{
    public partial class TrackWeight : Page

    {
        //TODO: Use dependency injection to inject implementation of IWeightDataAccess
        //private readonly IWeightDataAccess _weightDataAccess;

        //public TrackWeight(IWeightDataAccess weightDataAccess)
        //{
        //    this._weightDataAccess = weightDataAccess;
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.Url.LocalPath));

            }

            if (!IsPostBack)
            {
                LoadUserStartWeight();
                LoadWeightsTable();
            }
        }

        protected void SaveWeight(object sender, EventArgs e)
        {
            IWeightDataAccess _weightDataAccess = new WeightDataAccess();
            DataTable leaderBoardBeforeUserNewWeight = _weightDataAccess.GetSimpleLeaderBoard();
            _weightDataAccess.InsertWeight(User.Identity.GetUserId(), Convert.ToDecimal(TxtWeight.Text), TimezoneHelper.GetCentralTimeNow());
            TxtWeight.Text = string.Empty;
            CalculateUserPositionChangesAndNotifyUsers(leaderBoardBeforeUserNewWeight);
            LoadWeightsTable();
            
        }

        private void CalculateUserPositionChangesAndNotifyUsers(DataTable leaderBoardBeforeUserNewWeight)
        {
            IWeightDataAccess _weightDataAccess = new WeightDataAccess();
            DataTable leaderBoardAfterUserNewWeight = _weightDataAccess.GetSimpleLeaderBoard();
            if(leaderBoardAfterUserNewWeight.Rows.Count > 1 && leaderBoardBeforeUserNewWeight.Rows.Count.Equals(leaderBoardAfterUserNewWeight.Rows.Count))
            {
                for (int i = 0; i < leaderBoardAfterUserNewWeight.Rows.Count; i++)
                {
                    string previousUserIdAtPosition = Convert.ToString(leaderBoardBeforeUserNewWeight.Rows[i]["Id"]);
                    //string phoneNumber = Convert.ToString(leaderBoardBeforeUserNewWeight.Rows[i]["PhoneNumber"]);
                    string userName = Convert.ToString(leaderBoardBeforeUserNewWeight.Rows[i]["Name"]);

                    if (previousUserIdAtPosition != Convert.ToString(leaderBoardAfterUserNewWeight.Rows[i]["Id"])
                        && previousUserIdAtPosition != User.Identity.GetUserId().ToString())
                    {
                        //leaderboard user position has changed and is not the current user
                        //send sms to user mentioning changed position and link to leaderboard

                        var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        manager.SendSms(previousUserIdAtPosition, userName + ", your position on the Flab2Fab leaderboard has changed! Log in to see your new position and update your weight. https://Flab2fab.azurewebsites.net");
                    }
                }
            }
            
        }

        private void LoadWeightsTable()
        {
            IWeightDataAccess _weightDataAccess = new WeightDataAccess();
            DataTable dt = _weightDataAccess.GetWeightsByUserId(User.Identity.GetUserId());
            AddPercentChangeColumn(dt, Convert.ToDecimal(lblStartingWeightPlaceholder.Text));
            AddEarningsColumn(dt);
            FormatDate(dt);
            userWeights.DataSource = dt;
            userWeights.DataBind();
        }

        private void FormatDate(DataTable dt)
        {

            DateTime[] dates = new DateTime[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dates[i] = Convert.ToDateTime(dt.Rows[i]["Date"]);
            }

            dt.Columns.Remove("Date");
            dt.Columns.Add("Date", typeof(string)).SetOrdinal(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Date"] = dates[i].ToString("MM/dd hh:mm tt");
            }
        }

        private void AddPercentChangeColumn(DataTable dt, decimal startingWeight)
        {
            // Add a new DataColumn for "Percent Change"
            DataColumn percentChangeColumn = new DataColumn("Percent Change", typeof(string));

            // Add the new column to the DataTable
            dt.Columns.Add(percentChangeColumn);

            // Now, you can calculate and populate the "Percent Change" values
            foreach (DataRow row in dt.Rows)
            {

                // Calculate the "Percent Change" using some arithmetic
                decimal weight = Convert.ToDecimal(row["Weight"]);
                decimal percentChange = Math.Round((startingWeight - weight) / startingWeight * 100 * -1, 2);
                string percentChangeString = percentChange > 0 ? "+"+percentChange.ToString() : percentChange.ToString();


                //// Set the "Percent Change" value in the new column
                row["Percent Change"] = percentChangeString + "%";
            }
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
                decimal earningsAmount = Math.Round(percentChange * earningRate, 2) < 0 ? Math.Round((percentChange*-1) * earningRate, 2) : 0;

                //if earnings are above $100 set to $100
                earningsAmount = earningsAmount > 100 ? 100 : earningsAmount;

                // Set the "Earnings" value in the new column
                row["Earnings"] = earningsAmount.ToString("C");
            }
        }

        private void LoadUserStartWeight()
        {
            IUserDataAccess _userDataAccess = new UserDataAccess();
            decimal startWeight = _userDataAccess.GetStartingWeight(User.Identity.GetUserId());
            lblStartingWeightPlaceholder.Text = startWeight.ToString();
        }  
    }
}