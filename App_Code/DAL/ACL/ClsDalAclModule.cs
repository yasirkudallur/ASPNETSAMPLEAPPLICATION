using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsDalAclModule
/// </summary>
public class clsDalModule:clsSqlHelp
{
    public clsDalModule()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void executeQuery(cslBllModule ObjClsBll)
    {
        objSqlCmd = new SqlCommand("SP_MODULE_MSTR");
        if (ObjClsBll.PVar_SaveOrUpdate == "Save")
        {
            objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 1;
            objSqlCmd.Parameters.Add("@ModuleId", SqlDbType.Int).Value = 0;
        }
        else
        {
            objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 2;
            objSqlCmd.Parameters.Add("@ModuleId", SqlDbType.Int).Value = ObjClsBll.Pvar_Id;
        }
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@ModuleName", SqlDbType.VarChar).Value = ObjClsBll.Pvar_Name;
        objSqlCmd.Parameters.Add("@ModuleNameAr", SqlDbType.NVarChar).Value = ObjClsBll.Pvar_Name_Ar;
        executeQuery(objSqlCmd);
    }
    public void gridFill(cslBllModule ObjClsBll)
    {
        //objSqlCmd = new SqlCommand("SP_Acl_Module_Master");
        objSqlCmd = new SqlCommand("SP_MODULE_MSTR");
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 3;
        objSqlCmd.Parameters.Add("@ModuleId", SqlDbType.Int).Value = 0;
        objSqlCmd.Parameters.Add("@ModuleName", SqlDbType.VarChar).Value = "";
        gridFill(objSqlCmd,ObjClsBll.PVar_DataGrid);
    }
    public string IfExists(cslBllModule ObjClsBll)
    {
        objSqlCmd = new SqlCommand("SP_MODULE_MSTR");
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 4;
        objSqlCmd.Parameters.Add("@ModuleId", SqlDbType.Int).Value = ObjClsBll.Pvar_Id;
        objSqlCmd.Parameters.Add("@ModuleName", SqlDbType.VarChar).Value = ObjClsBll.Pvar_Name;
        openConnection();
        objSqlCmd.Connection = dbCon;
        var_Temp_Code = Convert.ToString(objSqlCmd.ExecuteScalar());
        dbCon.Close();
        return var_Temp_Code;
        
    }
    public void Delete(cslBllModule ObjClsBll)
    {
        objSqlCmd = new SqlCommand("SP_MODULE_MSTR");
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 6;
        objSqlCmd.Parameters.Add("@ModuleId", SqlDbType.Int).Value = ObjClsBll.Pvar_Id;
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        executeQuery(objSqlCmd);
    }

}
