using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace DataServices
{
    public class DbConnectionSvc : IDbConnectionSvc
    {
        private string GetConnectionString()
        {
            string ConnectionStringsKey = "ConnectionString";
            string strConnString = ConfigurationManager.AppSettings[ConnectionStringsKey];
            return strConnString;
        }

        public SqlConnection GetConnection()
        {
            SqlConnection cnDB = default(SqlConnection);
            cnDB = new SqlConnection(GetConnectionString());
            return cnDB;
        }
    }
}
