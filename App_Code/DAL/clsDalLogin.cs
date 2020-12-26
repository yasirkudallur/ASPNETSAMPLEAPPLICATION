using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public class clsDalLogin : clsSqlHelp
{
    public DataTable getUserDetails(clsBllLogin objLogin)
    {
        objSqlCmd = new SqlCommand("SP_ACL_LOGIN");
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = objLogin.UserId;
        objSqlCmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = objLogin.Password;
        DataTable dtUserDetails = fillDataTable(objSqlCmd);
        return dtUserDetails;
    }
}
