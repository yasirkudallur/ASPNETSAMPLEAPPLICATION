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
/// Summary description for ClsBllAclModule
/// </summary>
public class cslBllModule
{
    clsDalModule ObjClsdalAcl;
    public cslBllModule()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private int var_id;
    private string var_Name;
    private string var_Name_Ar;
    private int var_Active;
    private DataGrid Var_DataGrid;
    private string Var_SaveOrUpdate;
    public int Pvar_Id
    {
        get { return var_id; }
        set { var_id = value; }
    }
    public string Pvar_Name
    {
        get { return var_Name; }
        set { var_Name = value; }
    }
    public string Pvar_Name_Ar
    {
        get { return var_Name_Ar; }
        set { var_Name_Ar = value; }
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
    public void executeQuery()
    {
        ObjClsdalAcl = new clsDalModule();
        ObjClsdalAcl.executeQuery(this);
    }
    public void gridFill()
    {
        ObjClsdalAcl = new clsDalModule();
        ObjClsdalAcl.gridFill(this);
    }
    public string  IfExists()
    {
        ObjClsdalAcl = new clsDalModule();
        return ObjClsdalAcl.IfExists(this);
    }
    public void Delete()
    {
        ObjClsdalAcl = new clsDalModule();
        ObjClsdalAcl.Delete(this);
    }
  
}
