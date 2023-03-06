using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataServices
{
    public class DbSqlFunctionSvc : IDbSqlFunctionSvc
    {
        private readonly IDbConnectionSvc _dbConnectionSvc;
        public DbSqlFunctionSvc(IDbConnectionSvc dbConnectionSvc)
        {
            _dbConnectionSvc = dbConnectionSvc;
        }


        public DataTable ExecProcDataTable(string ProcName, ref SqlParameter[] Parameters)
        {
            var sqlConnection = _dbConnectionSvc.GetConnection();
            DataTable Result = new DataTable();

            try
            {
                sqlConnection.Open();               
                // Define a command to call stored procedure show_cities_multiple
                SqlCommand command = new SqlCommand(ProcName, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                // Parameter
                if (Parameters != null)
                {
                    AddParameter(command, Parameters);//add parameters to command
                }
                // Output the rows of the first result set
                SqlDataAdapter dsAdp = default(SqlDataAdapter);
                dsAdp = new SqlDataAdapter(command);
                DataSet ds = new DataSet();

                dsAdp.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    Result = ds.Tables[0];
                }
                //tran.Commit();
                sqlConnection.Close();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
        }

        public DataSet ExecProcDataSet(string ProcName, ref SqlParameter[] Parameters)
        {
            var sqlConnection = _dbConnectionSvc.GetConnection();
            DataSet Result = new DataSet();
            
            try
            {
                sqlConnection.Open();
                // Start a transaction as it is required to work with cursors in PostgreSQL
                SqlCommand command = new SqlCommand(ProcName, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                // Parameter
                if (Parameters != null)
                {
                    AddParameter(command, Parameters);//add parameters to command
                }
                // Output the rows of the first result set
                SqlDataAdapter dsAdp = default(SqlDataAdapter);
                dsAdp = new SqlDataAdapter(command);
                dsAdp.Fill(Result);
                sqlConnection.Close();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }

                sqlConnection.Dispose();
            }
        }

        public string ExecStrSingleValue(string sqlStr)
        {
            var sqlConnection = _dbConnectionSvc.GetConnection();
            string Result = string.Empty;            
            try
            {
                sqlConnection.Open();

                // Define a command to call stored procedure show_cities_multiple
                SqlCommand command = new SqlCommand(sqlStr, sqlConnection);
                command.CommandType = CommandType.Text;
                // Execute the stored procedure and obtain the first result set
                SqlDataReader dr = command.ExecuteReader();
                // Output the rows of the first result set
                while (dr.Read())
                    Result = dr[0].ToString();

                sqlConnection.Close();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
        }

        private SqlCommand AddParameter(SqlCommand Command, SqlParameter[] Parameters)
        {
            List<SqlParameter> distinctParm = Parameters.GroupBy(x => x.ParameterName).Select(g => g.First()).ToList();
            for (int i = 0; i < distinctParm.Count; i++)//add parameters to command
            {
                Command.Parameters.Add(distinctParm[i]);
            }
            return Command;
        }
    }
}
