using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for clsBllDashboard
/// </summary>
public class clsBllDashboard
{
    public clsBllDashboard()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    clsDalDashbaord objDal = new clsDalDashbaord();

    public string sSqlFilter
    {
        get;set;
    }

    public DataSet loadAnalysis()
    {
        try
        {
            return objDal.loadAnalysis(this);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}