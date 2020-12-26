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
/// Summary description for clsBllfrmproperties
/// </summary>
public class clsBllfrmproperties
{
   string lblfrmName;
   string lblPropertyName;
   string lblValue;

	public clsBllfrmproperties()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string lblForm
    {
    get
    {
        return lblfrmName; 
    }
set
{
    lblfrmName = value;
}

}

    public string lblProperty
    {
        get
        {
           return  lblProperty; 
        }
        set
        {
            lblProperty = value;  
        }

    }
    public string lblvalues
    {
        get
        {
            return lblValue; 
        }
        set
        {
            lblValue = value;  
        }
    }

}
