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
using System.Collections;

/// <summary>
/// Summary description for clsBllRollToService
/// </summary>
public class clsBllAssignPrivilages:clsSqlHelp
{

    //Coded By Baiju
    public clsBllAssignPrivilages()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    clsDalAssignPrivilages objDalRollToService = new clsDalAssignPrivilages();

    private int var_flag;
    private int var_rts_Id;
    private int var_roll_Id;
    private int var_mnu_Srv_Id;
    private bool var_Stat_Add;
    private bool var_Stat_Edit;
    private bool var_Stat_Delete;
    private bool var_Stat_Report;
    private bool var_Stat_View;
    private Int32 Var_EmployeeId;
    private string var_mnu_Name;

    public Int32 PVar_EmployeeId
    {
        get {return Var_EmployeeId; }
        set { Var_EmployeeId = value; }
    }
    public int flag
    {
        get { return var_flag; }
        set { var_flag = value; }
    }
    public int rtsId
    {
        get { return var_rts_Id; }
        set { var_rts_Id = value; }
    }
    public int rollId
    {
        get { return var_roll_Id; }
        set { var_roll_Id = value; }
    }
    public int menuServiceId
    {
        get { return var_mnu_Srv_Id; }
        set { var_mnu_Srv_Id = value; }
    }
    public bool statAdd
    {
        get { return var_Stat_Add; }
        set { var_Stat_Add = value; }
    }
    public bool statEdit
    {
        get { return var_Stat_Edit; }
        set { var_Stat_Edit = value; }
    }
    public bool statDelete
    {
        get { return var_Stat_Delete; }
        set { var_Stat_Delete = value; }
    }
    public bool statReport
    {
        get { return var_Stat_Report; }
        set { var_Stat_Report = value; }
    }
    public bool statView
    {
        get { return var_Stat_View; }
        set { var_Stat_View = value; }
    }
    public string menuName
    {
        get { return var_mnu_Name; }
        set { var_mnu_Name = value; }
    }

    public void RollIdFill(DropDownList objDdl)
    {
        objDalRollToService.RollIdFill(objDdl);
    }
    public void ServiceGridFill(DataGrid dg)
    {
        objDalRollToService.ServiceGridFill(this, dg);
    }
    public void SaveToRTS()
    {
        objDalRollToService.SaveToRTS(this);
    }
   
    public int chkStatus()
    {
        int ret = 0;

        string mValue = objDalRollToService.chkStatus(this);
        if (mValue != "")
        {
            ret = Convert.ToInt32(mValue);
        }
        return ret;
    }
    public SqlDataReader selectFromRTS()
    {        
        return objDalRollToService.selectFromRTS(this);        
    }
    public void FillComboBox(DropDownList ObjDdl)
    {
        objDalRollToService.FillComboBox(ObjDdl, this);
    }
    
}
