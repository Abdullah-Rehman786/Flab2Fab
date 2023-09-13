using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Flab2Fab.App_Data
{
    public class UserDataAccess : IUserDataAccess
    {
        public decimal GetStartingWeight(string userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@UserId", userId)
            };

            return Convert.ToDecimal(DataUtils.ExecuteStoredProcedureScalar("GetUserStartWeightById", parameters));
        }

        public DataTable GetUpdateableUserInfo(string userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@UserId", userId)
            };

            return DataUtils.ExecuteStoredProcedureDataTable("GetUpdateableUserInfo", parameters);
        }

        public DataTable UpdateUserInfo(string userId,string name, string phoneNumber, bool isAnonymous, bool isPaid)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@UserId", userId),
            new SqlParameter("@Name", name),
            new SqlParameter("@PhoneNumber", phoneNumber),
            new SqlParameter("@isAnonymous", isAnonymous),
            new SqlParameter("@isPaid", isPaid),

            };

            return DataUtils.ExecuteStoredProcedureDataTable("UpdateUserInfo", parameters);
        }
    }
}