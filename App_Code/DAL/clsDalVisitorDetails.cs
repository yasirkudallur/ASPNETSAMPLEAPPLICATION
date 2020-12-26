using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for clsDalVisitorDetails
/// </summary>
public class clsDalVisitorDetails:clsSqlHelp
{
	public clsDalVisitorDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    SqlCommand objCmd;

    public DataTable returnVisitorDetails(int varVisitorId)
    {
        try
        {
            objCmd = new SqlCommand("[SP_VISITOR_DETAILS]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 1;
            objCmd.Parameters.Add("@varVisitorId", SqlDbType.BigInt).Value = varVisitorId;
            return ReturnDataTable(objCmd);
        }
        catch (Exception ex)
        {
            return null;
        }

    }
    public void fillCombo(DropDownList cmb, int varValue)
    {
        try
        {
            objCmd = new SqlCommand("[SP_APPLICANT_REGN]");
            objCmd.CommandType = CommandType.StoredProcedure;
            if (cmb.ID.ToString() == "cmbDepartment")
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 2;
            }

            fillComboDatabindInsert(objCmd, cmb, "NAME", "ID", "-1", "--إختر--");
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }
}