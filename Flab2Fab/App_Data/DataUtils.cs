using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.CodeDom.Compiler;

namespace Flab2Fab.App_Data
{
    public class DataUtils
    {
        static DataUtils()
        {
            bool onAzure = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ON_AZURE"));
            if (onAzure)
            {
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            }
            else
            {
                connectionString = ConfigurationManager.ConnectionStrings["Flab2FitLocal"].ToString();
            }
        }
        private static string connectionString;

        public static DataTable ExecuteStoredProcedureDataTable(string procedureName, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static object ExecuteStoredProcedureScalar(string procedureName, SqlParameter[] parameters)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    result = cmd.ExecuteScalar();
                }
            }
            return result;
        }
    }
}