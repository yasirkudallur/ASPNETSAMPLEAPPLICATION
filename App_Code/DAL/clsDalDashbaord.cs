using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for clsDalDashbaord
/// </summary>
public class clsDalDashbaord:clsSqlHelp
{


    public clsDalDashbaord()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet loadAnalysis(clsBllDashboard objBll)
    {
        try
        {
               
            objSqlCmd = new SqlCommand("[SP_DASHBOARD]");
            objSqlCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 1;
            objSqlCmd.Parameters.Add("@varFilterCriteria", SqlDbType.NVarChar).Value = objBll.sSqlFilter;
            return ReturnDataSet(objSqlCmd);
        }
        catch (Exception ex)
        {
            return null;
        }

    }
}