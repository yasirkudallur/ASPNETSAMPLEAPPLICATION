using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["MyCulture"] = "en-US";//Danger
        clsSqlHelp ObjHelp = new clsSqlHelp();

        if (!IsPostBack)
        {

            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {

                Request.Browser.Adapters.Clear();

            }
            if (Session["MyCulture"].ToString() == "ar-SA")
            {

                htmlHead.Attributes.Add("dir", "rtl");
            }
            else
            {

                htmlHead.Attributes.Add("dir", "ltr");
            }

            MenuItem newMenuNewUser;
            if (Session["sEmpType"].ToString() == "1")//ADMIN
            {
                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("الصفحة الرئيسية");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Home");
                }

                newMenuNewUser.ToolTip = "";
                newMenuNewUser.Value = "0";
                newMenuNewUser.NavigateUrl = "~/DASHBOARDS/frmDashboard.aspx";
                newMenuNewUser.Target = "dispFrame";
                menuNames.Items.AddAt(0, newMenuNewUser);

                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("إضافة طلب جديد");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Add New Customer");
                }
                newMenuNewUser.ToolTip = "";
                newMenuNewUser.Value = "1";
                newMenuNewUser.NavigateUrl = "~/VISITORS_MANAGEMENT/frmReadVisitorInfoAPI.aspx";
                newMenuNewUser.Target = "dispFrame";
                menuNames.Items.AddAt(1, newMenuNewUser);


                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("تفارير");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Reports");
                }
                newMenuNewUser.ToolTip = "";
                newMenuNewUser.Value = "2";
                newMenuNewUser.NavigateUrl = "~/VISITORS_MANAGEMENT/frmViewVisitors.aspx";
                newMenuNewUser.Target = "dispFrame";
                menuNames.Items.AddAt(2, newMenuNewUser);

      
                

                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("خروج");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Logout");
                }
                newMenuNewUser.Value = "3";
                newMenuNewUser.NavigateUrl = "~/frmLogout.aspx";
                newMenuNewUser.Target = "_parent";
                menuNames.Items.AddAt(3, newMenuNewUser);
            }
            else if (Session["sEmpType"].ToString() == "2")//USER
            {
                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("الصفحة الرئيسية");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Home");
                }

                newMenuNewUser.ToolTip = "";
                newMenuNewUser.Value = "0";
                newMenuNewUser.NavigateUrl = "~/DASHBOARDS/frmDashboard.aspx";
                newMenuNewUser.Target = "dispFrame";
                menuNames.Items.AddAt(0, newMenuNewUser);

                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("إضافة متعامل جديد");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Add New Customer");
                }
                newMenuNewUser.ToolTip = "";
                newMenuNewUser.Value = "1";
                newMenuNewUser.NavigateUrl = "~/VISITORS_MANAGEMENT/frmReadVisitorInfoAPI.aspx";
                newMenuNewUser.Target = "dispFrame";
                menuNames.Items.AddAt(1, newMenuNewUser);


                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("متابعة الطلب");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Track Requests");
                }
                newMenuNewUser.ToolTip = "";
                newMenuNewUser.Value = "1";
                newMenuNewUser.NavigateUrl = "~/VISITORS_MANAGEMENT/frmViewVisitors.aspx";
                newMenuNewUser.Target = "dispFrame";
                menuNames.Items.AddAt(2, newMenuNewUser);

                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("تفارير");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Reports");
                }
                newMenuNewUser.ToolTip = "";
                newMenuNewUser.Value = "3";
                newMenuNewUser.NavigateUrl = "~/VISITORS_MANAGEMENT/frmViewVisitors.aspx";
                newMenuNewUser.Target = "dispFrame";
                menuNames.Items.AddAt(3, newMenuNewUser);

                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("تغيير كلمة المرور");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Change Password");
                }
                newMenuNewUser.ToolTip = "تغيير كلمة المرور";
                newMenuNewUser.Value = "4";
                newMenuNewUser.NavigateUrl = "~/ACL/frmResetPassword.aspx";
                newMenuNewUser.Target = "dispFrame";
                menuNames.Items.AddAt(4, newMenuNewUser);



                if (Session["MyCulture"].ToString() == "ar-SA")
                {
                    newMenuNewUser = new MenuItem("خروج");
                }
                else
                {
                    newMenuNewUser = new MenuItem("Logout");
                }
                newMenuNewUser.Value = "5";
                newMenuNewUser.NavigateUrl = "~/frmLogout.aspx";
                newMenuNewUser.Target = "_parent";
                menuNames.Items.AddAt(5, newMenuNewUser);
            }

        

        }
     
    }
    protected void imgBtnTasks_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmDashboard.aspx");
    }

    protected void imgBtnAddVisitor_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmReadVisitorInfo.aspx");
    }
    protected void imgBtnViewVisitors_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmViewVisitors.aspx");
    }
    protected void imgBtnHome_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmLogout.aspx");
    }
}
