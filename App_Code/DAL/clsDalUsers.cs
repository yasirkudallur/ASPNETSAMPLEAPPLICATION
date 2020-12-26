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
public class clsDalUsers:clsSqlHelp
{

	SqlCommand objCmd;
    DataSet objDs;

    public clsDalUsers()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool saveRecords(clsBllUsers objBll)
    {
        try
        {
            objCmd = new SqlCommand("[SP_USER_MSTR]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varEmailAddress", SqlDbType.NVarChar).Value = objBll.sUserName;
            objCmd.Parameters.Add("@varPassword", SqlDbType.NVarChar).Value = objBll.sPassword;
            objCmd.Parameters.Add("@varNameAr", SqlDbType.NVarChar).Value = objBll.sNameAr;
            objCmd.Parameters.Add("@varGender", SqlDbType.BigInt).Value = objBll.iGender;
            objCmd.Parameters.Add("@varIdn", SqlDbType.BigInt).Value = objBll.sIdNumber;
            objCmd.Parameters.Add("@varmobileNumber", SqlDbType.VarChar).Value = objBll.sMobileNumber;
            objCmd.Parameters.Add("@varTel", SqlDbType.VarChar).Value = objBll.sLandLine;
            objCmd.Parameters.Add("@varRoleId", SqlDbType.BigInt).Value = objBll.iRole;
            objCmd.Parameters.Add("@varActive", SqlDbType.BigInt).Value = objBll.iActive;
            objCmd.Parameters.Add("@varTrnOwner", SqlDbType.BigInt).Value = objBll.iTrnOwner;

            if (objBll.iUserId == 0)
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 1;
                objCmd.Parameters.Add("@varUserId", SqlDbType.BigInt).Value = 0;
            }
            else
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 1;
                objCmd.Parameters.Add("@varUserId", SqlDbType.BigInt).Value = objBll.iUserId;
            }

            return  ExecuteTransaction(objCmd);
            
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
    
            gridFill(objCmd,objGrid);
        }
        catch (Exception ex)
        { 
            
        }
    }
    public bool deleteRecords(int varUserId)
    {
        try
        {
            objCmd = new SqlCommand("[SP_USER_MSTR]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varUserId", SqlDbType.BigInt).Value = varUserId;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 3;
            return ExecuteTransaction(objCmd);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public string emailExistance(string varEmail,int varUserId)
    {
        try
        {
            objCmd = new SqlCommand("[SP_USER_MSTR]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varUserId", SqlDbType.BigInt).Value = varUserId;
            objCmd.Parameters.Add("@varEmail", SqlDbType.VarChar).Value = varEmail;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 5;
            return ExecuteScalar(objCmd);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public DataSet loadUserDetails(int varUserId)
    {
        try
        {
            objCmd = new SqlCommand("[SP_USER_MSTR]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value =2;
            objCmd.Parameters.Add("@varUserId", SqlDbType.BigInt).Value = varUserId;
            return ReturnDataSet(objCmd);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public void fillCombo(DropDownList cmb, int varValue)
    {
        try
        {
            objCmd = new SqlCommand("[SP_LOAD_MASTER_DATA]");

            if (cmb.ID.ToString() == "drpSector" || cmb.ID.ToString() == "drpDepartment" || cmb.ID.ToString() == "drpSection" || cmb.ID.ToString() == "drpUnits")
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 1;
                objCmd.Parameters.Add("@varValue", SqlDbType.BigInt).Value = varValue;
            }
            else if (cmb.ID.ToString() == "drpAccounts")
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 2;
            }
            else if (cmb.ID.ToString() == "drpLocations")
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 3;
                objCmd.Parameters.Add("@varValue", SqlDbType.BigInt).Value = varValue;
            }
          
            
            fillComboDatabindInsert(objCmd, cmb, "NAME", "ID");
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

}