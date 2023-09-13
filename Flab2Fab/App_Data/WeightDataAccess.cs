using System.Data;
using System.Data.SqlClient;
using System;

namespace Flab2Fab.App_Data
{
    public class WeightDataAccess : IWeightDataAccess
    {
        public DataTable GetRecentWeightForAllUsers()
        {
            return DataUtils.ExecuteStoredProcedureDataTable("GetRecentWeightForAllUsers", null);
        }

        public DataTable GetSimpleLeaderBoard()
        {
            return DataUtils.ExecuteStoredProcedureDataTable("GetSimpleLeaderBoard", null);
        }

        public DataTable GetWeightsByUserId(string userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@UserId", userId)
            };

            return DataUtils.ExecuteStoredProcedureDataTable("GetWeightsByUserId", parameters);
        }

        public DataTable InsertWeight(string userId, decimal weight, DateTime date)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@UserId", userId),
            new SqlParameter("@Weight", weight),
            new SqlParameter("@Date", date)
            };

            return DataUtils.ExecuteStoredProcedureDataTable("InsertWeight", parameters);
        }
    }
}