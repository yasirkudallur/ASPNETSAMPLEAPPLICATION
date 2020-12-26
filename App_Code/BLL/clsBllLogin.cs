using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class clsBllLogin
{

    clsDalLogin objDalLogin = new clsDalLogin();

    private int empId;
    private string userId;
    private string password;
    private string varCustCode;
    public int EmpId
    {
        get { return empId; }
        set { empId = empId; }
    }
    public string UserId
    {
        get { return userId; }
        set { userId = value; }
    }
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    public string pvarCustCode
    {
        get { return varCustCode; }
        set { varCustCode = value; }
    }
    public DataTable getUserDetails(clsBllLogin obj)
    {
       DataTable dtUser= objDalLogin.getUserDetails(obj);
       return dtUser;
    }


}
