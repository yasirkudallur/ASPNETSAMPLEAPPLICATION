using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for clsBllDepartment
/// </summary>
public class clsBllUsers
{
    clsDalUsers objDal = new clsDalUsers();
    public clsBllUsers()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int iUserId
    {
        get;
        set;
    }
    public string sUserName
    {
        get;
        set;
    }

    public string sNameAr
    {
        get;
        set;
    }

    public string sPassword
    {
        get;
        set;
    }

    public string sIdNumber
    {
        get;
        set;
    }

    public string sMobileNumber
    {
        get;
        set;
    }

    public string sLandLine
    {
        get;
        set;
    }

  

    public int iTrnOwner
    {
        get;
        set;
    }

    public int iActive
    {
        get;
        set;
    }

    public int iGender
    {
        get; set;
    }

    public int iRole
    {
        get; set;
    }

    public bool saveRecords()
    {
        try
        {
            return objDal.saveRecords(this);
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
            objDal.fillGrid(objGrid, sqlQry);
        }
        catch (Exception ex)
        {

        }
    }
    public bool deleteRecords(int varUserId)
    {
        try
        {
            return objDal.deleteRecords(varUserId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public string userExistance(string varUserName, int varUserId)
    {
        try
        {
            return objDal.emailExistance(varUserName, varUserId);
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
            return objDal.loadUserDetails(varUserId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public void fillCombo(DropDownList cmbBox, int varValue)
    {
        objDal.fillCombo(cmbBox, varValue);
    }

}