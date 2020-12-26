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
/// Summary description for ClsDalAclMenuService
/// </summary>
public class clsDalMenu : clsSqlHelp 
{
	public clsDalMenu()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void executeQuery(clsBllMenu objnew)
    {
        //objSqlCmd = new SqlCommand("SP_Acl_MenuService_Master");

        objSqlCmd = new SqlCommand("SP_MENUSERVICE_MASTER");
        if (objnew.PVar_SaveOrUpdate == "Save")
        {
            objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 1;
            objSqlCmd.Parameters.Add("@MenuServiceId", SqlDbType.Int).Value = 0;
        }
        else
        {
            objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 2;
            objSqlCmd.Parameters.Add("@MenuServiceId", SqlDbType.Int).Value = objnew.Pvar_Mnu_Srv_id;
        }
        objSqlCmd.Parameters.Add("@ModuleId", SqlDbType.Int).Value = objnew.Pvar_Module_Id;
        objSqlCmd.Parameters.Add("@FormId", SqlDbType.Int).Value = objnew.PVar_Form_URL_ID;
        objSqlCmd.Parameters.Add("@MenuName", SqlDbType.VarChar).Value = objnew.Pvar_Menu_Name;
        objSqlCmd.Parameters.Add("@MenuNameAr", SqlDbType.NVarChar).Value = objnew.Pvar_Menu_Name_Ar;
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        executeQuery(objSqlCmd);
    }
    public void gridFill(string varCulture,DataGrid objGrid)
    {
        //objSqlCmd = new SqlCommand("SP_Acl_MenuService_Master");
        objSqlCmd = new SqlCommand("SP_MENUSERVICE_MASTER");
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 4;
        gridFill(objSqlCmd, objGrid);
    }
    public string IfExists(clsBllMenu objnew)
    {
        //objSqlCmd = new SqlCommand("SP_Acl_MenuService_Master");
        objSqlCmd = new SqlCommand("SP_MENUSERVICE_MASTER");
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 5;
        objSqlCmd.Parameters.Add("@MenuServiceid", SqlDbType.Int).Value = objnew.Pvar_Mnu_Srv_id;
        objSqlCmd.Parameters.Add("@MenuName", SqlDbType.VarChar).Value = objnew.Pvar_Menu_Name;
        openConnection();
        objSqlCmd.Connection = dbCon;
        var_Temp_Code = Convert.ToString(objSqlCmd.ExecuteScalar());
        dbCon.Close();
        return var_Temp_Code;

    }
    public void ComboFill(clsBllMenu objnew)
    {
        objnew.PVar_DDlModulName.Items.Clear();
        //objSqlCmd = new SqlCommand("SP_Acl_MenuService_Master");
        objSqlCmd = new SqlCommand("SP_MENUSERVICE_MASTER");
        objSqlCmd.Connection = dbCon;
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 7;
        comboFill(objnew.PVar_DDlModulName, objSqlCmd);
    }
    //Baiju
    public void ComboFrmFill(clsBllMenu objnew)
    {
        objnew.PVar_DDlFormName.Items.Clear();
        //objSqlCmd = new SqlCommand("SP_Acl_MenuService_Master");
        objSqlCmd = new SqlCommand("SP_MENUSERVICE_MASTER");
        objSqlCmd.Connection = dbCon;
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 8;
        
        comboFill(objnew.PVar_DDlFormName, objSqlCmd);
    }
    //Baiju
    public void Delete(clsBllMenu objnew)
    {
        //objSqlCmd = new SqlCommand("SP_Acl_MenuService_Master");
        objSqlCmd = new SqlCommand("SP_MENUSERVICE_MASTER");
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 9;
        objSqlCmd.Parameters.Add("@MenuServiceId", SqlDbType.Int).Value = objnew.Pvar_Mnu_Srv_id;
        executeQuery(objSqlCmd);
    }
}
