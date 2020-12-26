using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;

/// <summary>
/// Summary description for basePage
/// </summary>
public class basePage:System.Web.UI.Page
{
	public basePage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    protected override void InitializeCulture()
    {
        try
        {
            if (Session["MyCulture"] != null)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["MyCulture"].ToString());
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["MyCulture"].ToString());
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-SA");
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ar-SA");
            }
        }
        catch (Exception ex)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-SA");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ar-SA");
        }
    }

    
}