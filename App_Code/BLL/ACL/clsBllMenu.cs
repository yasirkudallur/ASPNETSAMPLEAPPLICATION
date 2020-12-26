using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsBllAclMenuService
/// </summary>
public class clsBllMenu
{
    clsDalMenu ObjDalAclMenuServ ;
    
    public clsBllMenu()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private int var_Mnu_Srv_id;
    private int var_Module_Id;
    private string  var_Menu_Name;
    private int var_Active;
    private DataGrid Var_DataGrid;
    private string Var_SaveOrUpdate;
    private DropDownList Var_DDlModulName;
    private string var_Menu_Name_Ar;
   
    public DropDownList PVar_DDlModulName
    {
        get { return Var_DDlModulName; }
        set { Var_DDlModulName = value; }
    }

    //Baiju
    private DropDownList Var_DDlFrmName;
    public DropDownList PVar_DDlFormName
    {
        get { return Var_DDlFrmName; }
        set { Var_DDlFrmName = value; }
    }
    private int var_Form_URL_Id;
    public int PVar_Form_URL_ID
    {
        get { return var_Form_URL_Id; }
        set { var_Form_URL_Id = value; }
    }
    private string var_MS_Flag;
    public string PVar_MS_Flag
    {
        get { return var_MS_Flag; }
        set { var_MS_Flag = value; }
    }
    //Baiju

    public int Pvar_Mnu_Srv_id
    {
        get { return var_Mnu_Srv_id; }
        set { var_Mnu_Srv_id = value; }
    }
    public int Pvar_Module_Id
    {
        get { return var_Module_Id; }
        set { var_Module_Id = value; }
    }
    public string Pvar_Menu_Name
    {
        get { return var_Menu_Name; }
        set { var_Menu_Name = value; }
    }
    public int Pvar_Active
    {
        get { return var_Active; }
        set { var_Active = value; }
    }
    public DataGrid PVar_DataGrid
    {
        get { return Var_DataGrid; }
        set { Var_DataGrid = value; }
    }
    public string PVar_SaveOrUpdate
    {
        get { return Var_SaveOrUpdate; }
        set { Var_SaveOrUpdate = value; }
    }
    public string Pvar_Menu_Name_Ar
    {
        get { return var_Menu_Name_Ar; }
        set { var_Menu_Name_Ar = value; }
    }
    

    public void ComboFill()
    {
        ObjDalAclMenuServ = new clsDalMenu();
        ObjDalAclMenuServ.ComboFill(this);
    }

    //Baiju
    public void ComboFrmFill()
    {
        ObjDalAclMenuServ = new clsDalMenu();
        ObjDalAclMenuServ.ComboFrmFill(this);
    }
    //Baiju

    public void executeQuery()
    {
        ObjDalAclMenuServ = new clsDalMenu();
        ObjDalAclMenuServ.executeQuery(this);
    }
    public void gridFill(string varCulture,DataGrid objGrid)
    {
        ObjDalAclMenuServ = new clsDalMenu();
        ObjDalAclMenuServ.gridFill(varCulture, objGrid);
    }
    public void Delete()
    {
        ObjDalAclMenuServ = new clsDalMenu();
        ObjDalAclMenuServ.Delete(this);
    }
    public string IfExists()
    {
        ObjDalAclMenuServ = new clsDalMenu();
        return ObjDalAclMenuServ.IfExists(this);
    }
}
