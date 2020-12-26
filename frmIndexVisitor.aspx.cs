using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmIndexVisitor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetAllowResponseInBrowserHistory(false);

        if (Session["sUserId"] == null)
        {
            Response.Redirect("~/frmLogout.aspx");
        }
    }
}