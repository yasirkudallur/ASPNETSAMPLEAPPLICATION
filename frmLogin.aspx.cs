using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using log4net;

public partial class frmLogin : System.Web.UI.Page
{
    string sqlQry = "";
    clsBllLogin objBllLogin = new clsBllLogin();
    ILog logger = log4net.LogManager.GetLogger("DETAILS");

    //public bool AuthenticateActiveDirectory(string Domain, string UserName, string Password)
    //{
    //    try
    //    {
    //        DirectoryEntry entry = new DirectoryEntry("LDAP://" + Domain, UserName, Password);
    //        object nativeObject = entry.NativeObject;
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}

    //public bool authenticateUser()
    //{
    //    bool varFlag = false;
    //    try
    //    {
    //        clsSqlHelp objHelp = new clsSqlHelp();
    //        objHelp.openConnection();
    //        sqlQry = "SELECT ROLE_ID,NAME,EMAIL,EMPLOYEE_ID,DEPT_ID,ACTIVE,BRANCH_ID FROM ACL_EMPLOYEE_MSTR  " +
    //        " WHERE EMAIL = @varUserName and PASSWORD =  @varPassword";
    //        System.Data.SqlClient.SqlCommand objCmd = new System.Data.SqlClient.SqlCommand(sqlQry, objHelp.dbCon);

    //        objCmd.Parameters.Add("@varUserName", System.Data.SqlDbType.NVarChar).Value = this.txtUserName.Text;
    //        byte[] buffer = clsEncryption.Encryption(txtPassword.Text.Trim(), "753$#*&^");
    //        objCmd.Parameters.Add("@varPassword", System.Data.SqlDbType.NVarChar).Value = Convert.ToBase64String(buffer);
    //        System.Data.SqlClient.SqlDataReader objReader = objCmd.ExecuteReader();
    //        if (objReader.Read())
    //        {
    //            Session["sUserType"] = objReader.GetValue(0).ToString();
    //            Session["sEmployeeName"] = objReader.GetValue(1).ToString();
    //            Session["sUserEmail"] = objReader.GetValue(2).ToString();
    //            Session["sEmployeeId"] = objReader.GetValue(3).ToString();
    //            Session["sDepartmentId"] = objReader.GetValue(4).ToString();
    //            Session["sActive"] = objReader.GetValue(5).ToString();
    //            Session["sBranchId"] = objReader.GetValue(6).ToString();
    //            //Session["sUserName"] = this.txtUserName.Text.Trim().Replace("@emiratesid.ae","");
    //            if (Session["sActive"].ToString() == "True")
    //            {
    //                varFlag = true;
    //            }
    //            else
    //            {
    //                varFlag = false;
    //            }
    //        }
    //        else
    //        {
    //            varFlag = false;
    //        }
    //        objReader.Close();
    //        objHelp.dbCon.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message.ToString());
    //    }
    //    finally
    //    {
    //    }
    //    return varFlag;
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //imgBtnReadPublicData.Attributes.Add("onClick", "javascript:return validateControls();");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
      
    }
    protected void imgBtnReadPublicData_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click1(object sender, EventArgs e)
    {
        logger.Info(System.IO.Path.GetFileName(Request.PhysicalPath) + ":" + "Login Button Clicked");

        objBllLogin.UserId = txtUserName.Text;
        byte[] buffer = clsEncryption.Encryption(txtPassword.Text.Trim(), "753$#*&^");

        objBllLogin.Password = Convert.ToBase64String(buffer);

        DataTable dtUserDetails = objBllLogin.getUserDetails(objBllLogin);

        if (dtUserDetails.Rows.Count == 0)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Login')</script>");
        }
        else
        {


            Session["sUserName"] = dtUserDetails.Rows[0]["NAME"].ToString();
            Session["sUserId"] = dtUserDetails.Rows[0]["user_id"].ToString();
            Session["sEmpType"] = dtUserDetails.Rows[0]["role_id"].ToString();

            logger.Info(System.IO.Path.GetFileName(Request.PhysicalPath) + ":" + "Login Success" + ":" + Session["sUserName"].ToString());
            Response.Redirect("frmIndexVisitor.aspx");
        }
    }
}