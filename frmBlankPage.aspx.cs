using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmBlankPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["sEmpType"].ToString() == "1")//ADMIN
            {
                Response.Redirect("~/DASHBOARDS/frmDashboard.aspx");
            }
        }
    }
}