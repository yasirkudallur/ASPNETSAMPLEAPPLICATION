using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for clsBllDepartment
/// </summary>
public class clsBllDepartment
{
clsDalDepartment objDalDepartment = new clsDalDepartment();
public clsBllDepartment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private int varDepartmentId;
    public int DepartmentId
    {
        get { return varDepartmentId; }
        set { varDepartmentId = value; }
    }
    private string varDepartmentNameAr;
    public string DepartmentNameAr
    {
        get { return varDepartmentNameAr; }
        set { varDepartmentNameAr = value; }
    }
    private string varDepartmentNameEn;
    public string DepartmentNameEn
    {
        get { return varDepartmentNameEn; }
        set { varDepartmentNameEn = value; }
    }

    public int iActive
    {
        get;
        set;
    }

    public bool saveRecords()
    {
        try
        {
            return objDalDepartment.saveRecords(this);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public void fillGrid(DataGrid objGrid, string sqlQry)
    {
        try
        {
            objDalDepartment.fillGrid(objGrid, sqlQry);
        }
        catch (Exception ex)
        {

        }
    }
    public bool deleteRecords(int varDepartmentId)
    {
        try
        {
            return objDalDepartment.deleteRecords(varDepartmentId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}