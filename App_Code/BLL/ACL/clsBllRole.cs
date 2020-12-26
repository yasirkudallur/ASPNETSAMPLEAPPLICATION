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
/// Summary description for ClsBllAclRoll
/// </summary>
public class clsBllRole : clsSqlHelp 
{
    clsDalRole objDalRoll = new clsDalRole();
    public clsBllRole()
	{
		//  
		// TODO: Add constructor logic here
		//
	}
    private string var_Mode;
    private string var_Roll_Code;
    private string var_Roll_Name;
    private string var_Roll_Name_Ar;
    private int var_Flag;
    private Boolean var_Active;
    public SqlCommand objCmd;
    public int pvar_Flag
    {
        get { return var_Flag; }
        set { var_Flag = value; }
    }
    public string pvar_Mode
    {
        get { return var_Mode;}
        set { var_Mode = value;}
    }
    public string pvar_Roll_Code
    {
        get { return var_Roll_Code; }
        set { var_Roll_Code = value; }
    }
    public string pvar_Roll_Name
    {
        get { return var_Roll_Name; }
        set { var_Roll_Name = value; }
    }
    public string pvar_Roll_Name_Ar
    {
        get { return var_Roll_Name_Ar; }
        set { var_Roll_Name_Ar = value; }
    }
    public Boolean  pvar_Active
    {
        get { return var_Active; }
        set { var_Active = value;}
    }
    private Int32 Var_RoleId;
    public Int32 PVar_RoleId
    {
        get { return Var_RoleId; }
        set { Var_RoleId = value; }
    }

    public void saveToDb()
    {
        objDalRoll.saveToDb(this);
    }
    public void gridFill(DataGrid objGrid)
    {
        objDalRoll.GridFill(objGrid);
    }
    public void gridFill(DataGrid objGrid,string Key,string Type)
    {
        objDalRoll.GridFill(objGrid,Key,Type,this);
    }
    public bool deleteData(int roll_id)
    {
      return  objDalRoll.DeleteData(roll_id);
    }
    public string getData(Int32 Flag)
    {
        return objDalRoll.getCode(this,Flag);
    }
}
