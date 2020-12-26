using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for clsDalAclRoll
/// </summary>
public class clsDalRole : clsSqlHelp 
{
    public clsDalRole()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void saveToDb(clsBllRole objBllRoll)
    {

        objSqlCmd = new SqlCommand();

        if (objBllRoll.PVar_RoleId == 0)
        {
            objSqlCmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 1;
        }
        else
        {
            objSqlCmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 2;
        }

        objSqlCmd.Parameters.Add("@RoleCode", SqlDbType.VarChar).Value = objBllRoll.pvar_Roll_Code;
        objSqlCmd.Parameters.Add("@RoleName", SqlDbType.VarChar).Value = objBllRoll.pvar_Roll_Name;
        objSqlCmd.Parameters.Add("@RoleNameAr", SqlDbType.NVarChar).Value = objBllRoll.pvar_Roll_Name_Ar;
        objSqlCmd.Parameters.Add("@RoleId", SqlDbType.BigInt).Value = objBllRoll.PVar_RoleId;
        executeQuery(objSqlCmd, "SP_ROLE_MSTR");
    }
    public string getCode(clsBllRole objBllRoll,Int32 Flag)
    {
        objSqlCmd = new SqlCommand();
        if(Flag==5)  objSqlCmd.Parameters.Add("@RoleCode", SqlDbType.VarChar).Value = objBllRoll.pvar_Roll_Code;
        else if (Flag==4)    objSqlCmd.Parameters.Add("@RoleName", SqlDbType.VarChar).Value = objBllRoll.pvar_Roll_Name;
        objSqlCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = Flag;
        objSqlCmd.Parameters.Add("@RoleId", SqlDbType.BigInt).Value = objBllRoll.PVar_RoleId;

        var_Temp_Code = getSingleCode(objSqlCmd, "SP_ROLE_MSTR");
        return var_Temp_Code;

    }
    public void GridFill(DataGrid objGrid)
    {
           SqlString objString;
            objString = SqlString.Null;
            SqlCommand objCmd = new SqlCommand();
            //objCmd = new SqlCommand("SP_Acl_Role_Master");
            objCmd = new SqlCommand("SP_ROLE_MSTR");
            objCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 3;
            objCmd.CommandType = CommandType.StoredProcedure;
            gridFill(objCmd, objGrid);

    }
    public void GridFill(DataGrid objGrid,string Key ,string Type, clsBllRole ObjBll)
    {
        SqlString objString;
        objString = SqlString.Null;
        SqlCommand objCmd = new SqlCommand();
        //objCmd = new SqlCommand("SP_Acl_Role_Master");
        objCmd = new SqlCommand("SP_ROLE_MSTR");
        if (Type == "Code")
        {
            objCmd.Parameters.Add("@RoleCode", SqlDbType.VarChar).Value = Key;
            objCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 8;
        }
        else
        {
            objCmd.Parameters.Add("@RoleName", SqlDbType.VarChar).Value = Key;
            objCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 7;
        }
        objCmd.CommandType = CommandType.StoredProcedure;
        gridFill(objCmd, objGrid);

    }
    public bool DeleteData(Int32 RoleId)
    {
        try
        {
            //SqlCommand objCmd = new SqlCommand("SP_Acl_Role_Master");
            SqlCommand objCmd = new SqlCommand("SP_ROLE_MSTR");
            

            objCmd.Parameters.Add("@RoleId", SqlDbType.BigInt).Value = RoleId;
            objCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 6;
            objCmd.CommandType = CommandType.StoredProcedure;
           return ExecuteQuery(objCmd);
            
        }
        catch (Exception Ex)
        {
            return false;
        }
    }
}
