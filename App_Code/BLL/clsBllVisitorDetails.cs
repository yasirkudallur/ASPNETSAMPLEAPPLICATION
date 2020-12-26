using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for clsBllVisitorDetails
/// </summary>
public class clsBllVisitorDetails
{
	public clsBllVisitorDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    clsDalVisitorDetails objDal = new clsDalVisitorDetails();

    public DataTable returnVisitorDetails(int varVisitorId)
    {
        try
        {
            return objDal.returnVisitorDetails(varVisitorId);
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