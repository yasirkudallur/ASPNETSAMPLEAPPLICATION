using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for clsDalDepartment
/// </summary>
public class clsDalDepartment
{
	SqlCommand objCmd;
    DataSet objDs;
    clsSqlHelp objHelp = new clsSqlHelp();
    public clsDalDepartment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public bool saveRecords(clsBllDepartment objBllDepartment)
    {
        try
        {
            objCmd = new SqlCommand("[SP_DEPARTMENT_MSTR]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = objBllDepartment.DepartmentNameAr;
            objCmd.Parameters.Add("@DepartmentAr", SqlDbType.NVarChar).Value = objBllDepartment.DepartmentNameAr;
            objCmd.Parameters.Add("@varActive", SqlDbType.BigInt).Value = objBllDepartment.iActive;

            if (objBllDepartment.DepartmentId == 0)
            {
                objCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 1;
            }
            else
            {
                objCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 1;
                objCmd.Parameters.Add("@DepartmentId", SqlDbType.BigInt).Value = objBllDepartment.DepartmentId;
            }
            return  objHelp.ExecuteTransaction(objCmd);
            
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public void fillGrid(DataGrid objGrid,string sqlQry)
    {
        try
        {
            objCmd = new SqlCommand("[SP_DEPARTMENT_MSTR]");
            objCmd.CommandType = CommandType.StoredProcedure;
        
            objCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 2;
    
            objHelp.gridFill(objCmd,objGrid);
        }
        catch (Exception ex)
        { 
            
        }
    }
    public bool deleteRecords(int varDepartmentId)
    {
        try
        {
            objCmd = new SqlCommand("[SP_DEPARTMENT]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varDepartmentId", SqlDbType.BigInt).Value = varDepartmentId;
            objCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 4;
            return objHelp.ExecuteTransaction(objCmd);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}