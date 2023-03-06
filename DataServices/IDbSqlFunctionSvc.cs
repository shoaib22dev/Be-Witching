using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataServices
{
    public interface IDbSqlFunctionSvc
    {
        DataTable ExecProcDataTable(string ProcName, ref SqlParameter[] Parameters);
        DataSet ExecProcDataSet(string ProcName, ref SqlParameter[] Parameters);
        string ExecStrSingleValue(string sqlStr);
    }
}
