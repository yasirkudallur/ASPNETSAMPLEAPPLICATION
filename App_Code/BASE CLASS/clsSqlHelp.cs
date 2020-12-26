using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Web.Mail;


/// <summary>
/// Summary description for clsSqlHelp
/// </summary>
public class clsSqlHelp
{

    #region Enumerator

    public enum ActionType
    {
        Insert = 0,
        Update = 1,
        Delete = 2
    }

    #endregion

    public SqlConnection dbCon;
    public SqlTransaction objTrans;
    public SqlCommand objSqlCmd;
    
    public SqlDataReader objReader;
    public SqlDataAdapter objAdapter;
    public DataSet objDs = new DataSet();
    public string var_Temp_Code;
    CLsDataGridProperties ObjDgProp = new CLsDataGridProperties();
    public clsSqlHelp()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void openConnection()
    {
        string varCon = System.Configuration.ConfigurationManager.AppSettings["dbCon"];
        dbCon = new SqlConnection(varCon);
        if (dbCon.State == ConnectionState.Closed)
        {
            dbCon.Open();
        }
        else
        {
            dbCon.Close();
        }
    }
    public SqlDataReader ReturnReader(SqlCommand cmd, string ProcedureName)
    {
        openConnection();
        cmd.Connection = dbCon;
        cmd.CommandType = CommandType.StoredProcedure;
        objReader = cmd.ExecuteReader();
        return objReader;
        dbCon.Close();

    }
    public void ListboxFill(ListBox lstBox, SqlCommand cmd) //yunus 21-11-08
    {
        openConnection();

        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Connection = dbCon;


        objReader = objSqlCmd.ExecuteReader();
        while (objReader.Read())
        {
            lstBox.Items.Add(new ListItem(objReader.GetValue(1).ToString(), objReader.GetValue(0).ToString()));
        }
        objReader.Close();
        dbCon.Close();

    }
    public DataSet ReturnDataSet(SqlCommand cmd, string ProcedureName)
    {
        openConnection();
        cmd.Connection = dbCon;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcedureName;
        objAdapter = new SqlDataAdapter(cmd);
        objDs = new DataSet();
        objAdapter.Fill(objDs, ProcedureName);
        dbCon.Close();
        return objDs;
    }
    // Written By Rasheed :Function to retun a dataTable//when using with SP
    public static bool sendMail(string fromEmail, string toEmail, string bodyMessage)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To = toEmail;
            mail.From = fromEmail;
            mail.Subject = "LPO";
            mail.Body = bodyMessage;
            //mail.Attachments.Add(new MailAttachment("D:\\Zoom Application\\Travel Pos-02-april_2011\\TravelPos\\Test.pdf"));
            //mail.Attachments.Add(new MailAttachment("D:\\Zoom Application\\Travel Pos-02-april_2011\\TravelPos\\Test.pdf"));
            mail.Attachments.Add(new MailAttachment("E:\\TRAVEL POS PUBLISH 28-MAY-2011\\Payment.pdf"));
            //MailAttachment obj = new MailAttachment();
            //obj.Filename = "Test.pdf";
            //mail.Attachments.Add(obj);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "mail.sacrosys.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", false);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", 60);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "yasir@sacrosys.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "ummusalma");
            System.Web.Mail.SmtpMail.SmtpServer = "mail.sacrosys.com";  //your real server goes here
            System.Web.Mail.SmtpMail.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }
    public static bool sendMailLpo(string fromEmail, string toEmail, string bodyMessage, string varSubject)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To = toEmail;
            mail.From = fromEmail;
            mail.Subject = varSubject;
            mail.Body = "LPO";
            mail.Attachments.Add(new MailAttachment("E:\\TRAVEL POS PUBLISH 28-MAY-2011\\Test.pdf"));
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "mail.sacrosys.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", false);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", 60);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "yasir@sacrosys.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "ummusalma");
            System.Web.Mail.SmtpMail.SmtpServer = "mail.sacrosys.com";  //your real server goes here
            System.Web.Mail.SmtpMail.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }
    public static bool sendMailReceipt(string fromEmail, string toEmail, string bodyMessage, string varSubject)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To = "info@zoomtravel.ae";
            //mail.To = "yasirkudallur@gmail.com";
            mail.From = fromEmail;
            mail.Subject = toEmail + "-" + varSubject;
            mail.Body = bodyMessage;
            mail.Attachments.Add(new MailAttachment("E:\\TRAVEL POS PUBLISH 28-MAY-2011\\Receipt.pdf"));
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "mail.sacrosys.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", false);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", 60);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "yasir@sacrosys.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "ummusalma");
            System.Web.Mail.SmtpMail.SmtpServer = "mail.sacrosys.com";  //your real server goes here
            System.Web.Mail.SmtpMail.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }
    public static bool sendMail(string fromEmail, string toEmail, string bodyMessage, string Subject, string CC)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To = toEmail;
            mail.From = fromEmail;
            mail.Subject = Subject;
            mail.Body = bodyMessage;
            mail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-1256");

            if (CC != "")
            {
                mail.Cc = CC;
            }
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "mail.ipefoundation.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", false);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", 60);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "noreply@ipefoundation.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "ipeadmin29");
            System.Web.Mail.SmtpMail.SmtpServer = "mail.ipefoundation.com";  //your real server goes here
            System.Web.Mail.SmtpMail.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }
    public static bool sendMailHtml(string fromEmail, string toEmail, string bodyMessage, string Subject, string CC)
    {

        try
        {

            MailMessage mail = new MailMessage();
            mail.To = toEmail;
            mail.From = fromEmail;
            mail.Subject = Subject;
            mail.Body = bodyMessage;
            mail.BodyFormat = MailFormat.Html;
            mail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-1256");

            if (CC != "")
            {
                mail.Cc = CC;
            }
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "172.16.11.204");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", false);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", 60);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "emiratesid\\employees.suggestion");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "Eida@#0876");
            System.Web.Mail.SmtpMail.SmtpServer = "172.16.11.204";  //your real server goes here
            System.Web.Mail.SmtpMail.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }
    public static bool sendMailAr(string fromEmail, string toEmail, string bodyMessage, string Subject, string CC)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.BodyFormat = MailFormat.Html;
            mail.To = toEmail;
            mail.From = fromEmail;
            mail.Subject = Subject;
            mail.Body = bodyMessage;
            mail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-1256");
            //if (CC != "")
            //{
            //    mail.Cc = CC;
            //}
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "mail.ipefoundation.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", false);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", 60);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "noreply@ipefoundation.com");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "ipeadmin29");
            System.Web.Mail.SmtpMail.SmtpServer = "mail.ipefoundation.com";  //your real server goes here
            System.Web.Mail.SmtpMail.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {

        }
    }
    public DataTable fillDataTable(SqlCommand cmd)
    {
        openConnection();

        DataSet DS = new DataSet();
        cmd.Connection = dbCon;
        objAdapter = new SqlDataAdapter(cmd);
        objAdapter.Fill(DS);
        dbCon.Close();
        return DS.Tables[0];
    }
    public void fillComboDatabind(SqlCommand objCmd,DropDownList cmbList,string displayField,string valueField)
    {
        openConnection();
        objCmd.Connection = dbCon;
        objCmd.CommandType = CommandType.StoredProcedure;
        objReader = objCmd.ExecuteReader();
        cmbList.DataSource = objReader;
        cmbList.DataTextField = displayField;
        cmbList.DataValueField = valueField;
        cmbList.DataBind();
        objReader.Close();
        dbCon.Close();
    }
    public void fillComboDatabindInsert(SqlCommand objCmd, DropDownList cmbList, string displayField, string valueField)
    {
        openConnection();
        objCmd.Connection = dbCon;
        objCmd.CommandType = CommandType.StoredProcedure;
        objReader = objCmd.ExecuteReader();
        cmbList.DataSource = objReader;
        cmbList.DataTextField = displayField;
        cmbList.DataValueField = valueField;
        cmbList.DataBind();
        cmbList.Items.Insert(0, new ListItem("--اختر--", "0"));
        objReader.Close();
        dbCon.Close();
    }
    public void fillComboDatabindInsert(SqlCommand objCmd, DropDownList cmbList, string displayField, string valueField,string varFirstValue,string varFirstText)
    {
        openConnection();
        objCmd.Connection = dbCon;
        objCmd.CommandType = CommandType.StoredProcedure;
        objReader = objCmd.ExecuteReader();
        cmbList.DataSource = objReader;
        cmbList.DataTextField = displayField;
        cmbList.DataValueField = valueField;
        cmbList.DataBind();
        cmbList.Items.Insert(0, new ListItem("--" + varFirstText + "--", varFirstValue));
        objReader.Close();
        dbCon.Close();
    }
    public void fillCheckboxlistDatabind(SqlCommand objCmd, CheckBoxList cmbList, string displayField, string valueField)
    {
        openConnection();
        objCmd.Connection = dbCon;
        objCmd.CommandType = CommandType.StoredProcedure;
        objReader = objCmd.ExecuteReader();
        cmbList.DataSource = objReader;
        cmbList.DataTextField = displayField;
        cmbList.DataValueField = valueField;
        cmbList.DataBind();
        objReader.Close();
        dbCon.Close();
    }
    public void FillMenu(Menu Menu1, string UserSession, int varLanguage, string varBrowser)
    {
        if (UserSession != null)
        {
            SqlCommand ObjCmd = new SqlCommand("Edms_MenuCreation");
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.Parameters.Add("@rollid", SqlDbType.Int).Value = Convert.ToInt32(UserSession);
            ObjCmd.Parameters.Add("@varFlag", SqlDbType.Int).Value = varLanguage;
            DataSet dsMenu = ReturnDataSet(ObjCmd, "Edms_MenuCreation");
            Menu1.Items.Clear();
            for (int rowcount = 0; rowcount < dsMenu.Tables[0].Rows.Count; rowcount++)
            {
                bool ExistingModule = false;
                for (int itemCnt = 0; itemCnt < Menu1.Items.Count; itemCnt++)
                {
                    if (Menu1.Items[itemCnt].Value == dsMenu.Tables[0].Rows[rowcount][3].ToString())
                    {
                        ExistingModule = true;
                        break;
                    }
                    else
                    {
                        ExistingModule = false;
                    }
                }
                if (ExistingModule == false)
                {
                    MenuItem newMenuItem = new MenuItem(dsMenu.Tables[0].Rows[rowcount][1].ToString());
                    //newMenuItem.Width = 100; //YASIR COMMENTED ON 09-FEB-2012
                    newMenuItem.Value = dsMenu.Tables[0].Rows[rowcount][3].ToString();
                    if (varBrowser.ToString() != "IE")
                    {
                        newMenuItem.NavigateUrl = "frmIndex.aspx";
                    }
                    else
                    {
                        newMenuItem.NavigateUrl = "frmIndexIE.aspx";
                    }
                    string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
                    Menu1.Items.Add(newMenuItem);
                    for (int RowCnt = 0; RowCnt < dsMenu.Tables[0].Rows.Count; RowCnt++)
                    {
                        if (dsMenu.Tables[0].Rows[RowCnt][3].ToString() == menuItemValue)
                        {
                            MenuItem newSubMenu = new MenuItem(dsMenu.Tables[0].Rows[RowCnt][0].ToString());
                            //newMenuItem.Width = 100; //YASIR COMMENTED ON 09-FEB-2012
                            newSubMenu.Value = dsMenu.Tables[0].Rows[RowCnt][4].ToString();
                            string Privilage = ObjDgProp.EncryPt(dsMenu.Tables[0].Rows[RowCnt][5].ToString());
                            newSubMenu.Target = "dispFrame";
                            newSubMenu.NavigateUrl = dsMenu.Tables[0].Rows[RowCnt][2].ToString() + "?Event=" + Privilage;
                            Menu1.Items[Menu1.Items.Count - 1].ChildItems.Add(newSubMenu);
                        }
                    }
                }
            }

            if (Convert.ToInt32(UserSession) == 10)
            {
                //MenuItem newMenuNewUser;
                //if (varLanguage == 1)
                //{
                //    newMenuNewUser = new MenuItem("CREATE USER");
                //}
                //else
                //{
                //    newMenuNewUser = new MenuItem("إنشاء حساب مستخدم جديد");
                //}
                //newMenuNewUser.Value = "13";
                //newMenuNewUser.NavigateUrl = "~/SUGGESTION SYSTEM/frmUserRegn.aspx";
                //newMenuNewUser.Target = "dispFrame";
                //Menu1.Items.Add(newMenuNewUser);

                //--------------------------
                MenuItem newMenuEditUser;
                if (varLanguage == 1)
                {
                    //newMenuEditUser = new MenuItem("EDIT / DELETE USERS");
                    newMenuEditUser = new MenuItem("USERS");
                }
                else
                {
                    //newMenuEditUser = new MenuItem("تحرير / حذف المستخدمين");
                    newMenuEditUser = new MenuItem("المستخدمين");
                }
                newMenuEditUser.Value = "20";
                newMenuEditUser.NavigateUrl = "~/SUGGESTION SYSTEM/frmViewUsers.aspx";
                newMenuEditUser.Target = "dispFrame";
                Menu1.Items.Add(newMenuEditUser);
                //--------------------------
            }




            //EDIT PROFILE : START
            //MenuItem newMenuEditProfile;
            //MenuItem newMenuContactUs;
            //if (varLanguage == 1)
            //{
            //    newMenuEditProfile = new MenuItem("EDIT YOUR PROFILE");
            //    newMenuContactUs = new MenuItem("CONTACT US");
            //}
            //else
            //{
            //    newMenuEditProfile = new MenuItem("تعديل الملف الشخصي");
            //    newMenuContactUs = new MenuItem("الاتصال بنا");
            //}
            //newMenuEditProfile.Value = "111";
            //newMenuEditProfile.NavigateUrl = "~/SUGGESTION SYSTEM/frmEditProfile.aspx";
            //newMenuEditProfile.Target = "dispFrame";
            //Menu1.Items.Add(newMenuEditProfile);

            //newMenuContactUs.Value = "112";
            //newMenuContactUs.NavigateUrl = "frmContactUs.aspx";
            //newMenuContactUs.Target = "dispFrame";
            //Menu1.Items.Add(newMenuContactUs);
            //EDIT PROFILE : END

            //MenuItem newMenuItemd;
            //if(varLanguage == 1)
            //{
            //    newMenuItemd = new MenuItem("SIGN OUT");
            //}
            //else
            //{
            //    newMenuItemd = new MenuItem("خروج");
            //}

            //newMenuItemd.Value = "0";
            //newMenuItemd.NavigateUrl = "frmLogout.aspx";
            //Menu1.Items.Add(newMenuItemd);
        }

    }
     public void gridViewFill(SqlCommand objCmd, GridView objGridview)//Rasheed
    {
        try
        {
            openConnection();
            objCmd.Connection = dbCon;
            objAdapter = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDs.Clear();
            //objAdapter.Fill(objDs.Tables[0]);
            objAdapter.Fill(objDs, "TableName");
            objGridview.DataSource = objDs;
            objGridview.DataBind();
            dbCon.Close();
        }
        catch(Exception ex)
        {


        }
    }
    //public string getData(clsBllCountry objBllCountry)
    //{
    //    openConnection();
    //    objSqlCmd = new SqlCommand("select_Countr_Code", dbCon);
    //    objSqlCmd.CommandType = CommandType.StoredProcedure;
    //    objSqlCmd.Parameters.Add("@countr_Code", SqlDbType.VarChar).Value = objBllCountry.pvar_Countr_Code;
    //    objSqlCmd.Parameters.Add("@countr_Name", SqlDbType.VarChar).Value = objBllCountry.pvar_Countr_Name;
    //    string var = Convert.ToString(objSqlCmd.ExecuteScalar());
    //    dbCon.Close();
    //    return var;


    //}
    public SqlDataReader selectData(string sqlQry)
    {
        objSqlCmd = new SqlCommand(sqlQry, dbCon);
        objReader = objSqlCmd.ExecuteReader();
        return objReader;
    }
    public DataSet comboFill(string str)
    {
        objAdapter = new SqlDataAdapter(str, dbCon);
        objAdapter.Fill(objDs);
        string check = "";
        dbCon.Close();
        return objDs;

    }
    //By Yunus. : This methos is used for filling combo if a dropdown list object and stored procedure name(With Parametres) 
    //is passed.
    public void comboFill(DropDownList cmb, SqlCommand objCmd, string sp)
    {
        openConnection();
        cmb.Items.Clear();
        objCmd.Connection = dbCon;
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.CommandText = sp;
        objReader = objCmd.ExecuteReader();
        while (objReader.Read())
        {
            cmb.Items.Add(new ListItem(objReader.GetValue(1).ToString(), objReader.GetValue(0).ToString()));
            
        }
        cmb.Items.Insert(0, new ListItem("--Select One--", "0"));
        objReader.Close();
        dbCon.Close();
    }
    //By Yunus.

     // To fill combo by passing object of combo and a command name
    public void comboFill(DropDownList cmb, SqlCommand objCmd)
    {
        openConnection();
        cmb.Items.Clear();
        objCmd.Connection = dbCon;
        objCmd.CommandType = CommandType.StoredProcedure;
        objReader = objCmd.ExecuteReader();
        while (objReader.Read())
        {

            cmb.Items.Add(new ListItem(objReader.GetValue(1).ToString(), objReader.GetValue(0).ToString()));

        }
        //if (cmb.Items.Count > 0)
        //{
            cmb.Items.Insert(0, new ListItem("--Select One--", "0"));
        //}
        objReader.Close();
        dbCon.Close();
        objCmd.Dispose();
    }

    public void FillCheckedListBox(CheckBoxList ObjChkList, SqlCommand objCmd, string sp)
    {
        openConnection();
        ObjChkList.Items.Clear();
        objCmd.Connection = dbCon;
        objCmd.CommandText = sp;
        objReader = objCmd.ExecuteReader();
        while (objReader.Read())
        {
            ObjChkList.Items.Add(new ListItem(objReader.GetValue(1).ToString(), objReader.GetValue(0).ToString()));
        }
        objReader.Close();
        dbCon.Close();

    }

    //Coded by Baiju
    public SqlDataReader forExecuteReader(SqlCommand objCmd, string sp)
    {
        openConnection();
        objCmd.Connection = dbCon;
        objCmd.CommandText = sp;
        objCmd.CommandType = CommandType.StoredProcedure;
        objReader = objCmd.ExecuteReader();
        dbCon.Close();
        return objReader;
        
    }
    //Stops Baiju
    public void executeQuery(SqlCommand objCmd,string sp)
    {
        try
        {
            openConnection();
            objTrans = dbCon.BeginTransaction();
            objCmd.Connection = dbCon;
            objCmd.Transaction = objTrans;
            objCmd.CommandText = sp;
            objCmd.CommandType = CommandType.StoredProcedure;
            int id = objCmd.ExecuteNonQuery();
            objTrans.Commit();
        }
        catch (Exception ex)
        {
            objTrans.Rollback();
        }
        finally
        {
            dbCon.Close();
        }

    }
    public bool ExecuteTransaction(SqlCommand objCmd)
    {
        bool ReturnValue = false;
        openConnection();
        SqlTransaction ObjTrans = dbCon.BeginTransaction();
        try
        {
            objCmd.Transaction = ObjTrans;
            objCmd.Connection = dbCon;
            objCmd.ExecuteNonQuery();
            ObjTrans.Commit();
            dbCon.Close();
            ReturnValue= true;
            objCmd.Dispose();
        }
        catch (Exception Ex)
        {
            ObjTrans.Rollback();
            ReturnValue= false;
        }
        return ReturnValue;
    }
    public string ExecuteTransactionReturnValue(SqlCommand objCmd)
    {
        string  ReturnValue = "";
        openConnection();
        SqlTransaction ObjTrans = dbCon.BeginTransaction();
        try
        {
            objCmd.Transaction = ObjTrans;
            objCmd.Connection = dbCon;
            int id = objCmd.ExecuteNonQuery();
            ObjTrans.Commit();
            dbCon.Close();
            ReturnValue = id.ToString();
            objCmd.Dispose();
        }
        catch (Exception Ex)
        {
            ObjTrans.Rollback();
            ReturnValue = "";
        }
        return ReturnValue;
    }
    public void executeQuery(SqlCommand objCmd)  //By:YUNUS  Function to Execute a Sqlcommand Object
    {

        openConnection();
        objCmd.Connection = dbCon;
        objCmd.ExecuteNonQuery();
        dbCon.Close();
    }
    public object ExCuteScalar(string sql)
    {
        openConnection();
        objSqlCmd = new SqlCommand(sql, dbCon);
        object objct = objSqlCmd.ExecuteScalar();
        if (objct == null)
        {

            dbCon.Close();
            return "null";
        }
        else
        {
            dbCon.Close();
            return objct;
        }
    }
    public string getSingleCode(SqlCommand objCmd,string sp)
    {
        openConnection();
        objCmd.Connection = dbCon;
        objCmd.CommandText = sp;
        objCmd.CommandType = CommandType.StoredProcedure;
        var_Temp_Code = Convert.ToString(objCmd.ExecuteScalar());
        dbCon.Close();
        return var_Temp_Code;
    }
    public string getSingleCode(SqlCommand objCmd)
    {
        openConnection();
        objCmd.Connection = dbCon;
        var_Temp_Code = Convert.ToString(objCmd.ExecuteScalar());
        dbCon.Close();
        return var_Temp_Code;
    }
    public void comboFill(DropDownList cmb, string sp_name)
    {
        openConnection();
        cmb.Items.Clear();
        objSqlCmd = new SqlCommand(sp_name, dbCon);
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objReader = objSqlCmd.ExecuteReader();
        while (objReader.Read()  )
        {
            cmb.Items.Add(new ListItem(objReader.GetValue(1).ToString(), objReader.GetValue(0).ToString ()));
            
        }
        cmb.Items.Insert(0, new ListItem("--Select One--", "0"));
        objReader.Close();
        dbCon.Close();
    }
    public void ListboxFill(ListBox lstBox, string sp_name)
    {
        if (lstBox.ID == "lstFinCountry") //Yunus  :starts
        {
            openConnection();
            objSqlCmd = new SqlCommand();
            objSqlCmd.CommandType = CommandType.StoredProcedure;
            objSqlCmd.Connection = dbCon;
            objSqlCmd.CommandText = "Get_Countries_of_Mrkt";
            objSqlCmd.Parameters.AddWithValue("@mrkt", Convert.ToInt32(sp_name));
            objReader = objSqlCmd.ExecuteReader();
            while (objReader.Read())
            {
                lstBox.Items.Add(new ListItem(objReader.GetValue(1).ToString(), objReader.GetValue(0).ToString()));
            }
            objReader.Close();
            dbCon.Close();
        }
        else
        {
            openConnection();
            objSqlCmd.CommandType = CommandType.StoredProcedure;
            objSqlCmd.Parameters.Clear();   
            objSqlCmd.Connection = dbCon;
            objSqlCmd.CommandText = sp_name;
            objReader = objSqlCmd.ExecuteReader();
            while (objReader.Read())
            {
                lstBox.Items.Add(new ListItem(objReader.GetValue(1).ToString(), objReader.GetValue(0).ToString()));
            }
            objReader.Close();
            dbCon.Close();
        }
    }
    //Yasir
    //The method gridFill is used for filling a datagrid 
    //(when a command object (with command text and command type) and a datagrid object) is passed.
    public void gridFill(SqlCommand objCmd, DataGrid objGrid)
    {
        try
        {
            openConnection();
            objCmd.Connection = dbCon;
            objAdapter = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            //objDs.Clear();
            objAdapter.Fill(objDs,"TableName");
            if (objDs.Tables.Count > 0)
            {
                objGrid.DataSource = objDs.Tables[0];
                objGrid.DataBind();
            }
            dbCon.Close();
        }
        catch(Exception ex)
        { 
            

        }
    }
    public void datalistFill(SqlCommand objCmd, DataList objList)
    {
        try
        {
            openConnection();
            objCmd.Connection = dbCon;
            objAdapter = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            //objDs.Clear();
            objAdapter.Fill(objDs, "TableName");
            if (objDs.Tables.Count > 0)
            {
                objList.DataSource = objDs;
                objList.DataBind();
            }
            dbCon.Close();
        }
        catch (Exception ex)
        {


        }
    }
    //YUNUS:Method used to check for a value within a table.Arguments are Table name
    //field name and value.
    public string getSingleCode(string table, string field, string value)
    {
        string str;
        try
        {
            openConnection();
            string st = "select " + field + " from " + table + " where " + field + " =@val1";
            objSqlCmd = new SqlCommand(st,dbCon);
            objSqlCmd.Parameters.AddWithValue("@val1",value );
            if (objSqlCmd.ExecuteScalar() == null)
            {
                str = "";

                dbCon.Close();
                return str;
            }
            else
            {
              
                str = objSqlCmd.ExecuteScalar().ToString();
                dbCon.Close();
                return str;
            }
        }
        catch
        {
            return null;

        }
 
    }
    //YUNUS::Function to Fill PlaceHolder Dynamically  
    public void PlaceHolderFill(string procedure, PlaceHolder plchldr)
    {
        int i = 0;
        openConnection();
        objSqlCmd = new SqlCommand(procedure, dbCon);
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objReader = objSqlCmd.ExecuteReader();
        while (objReader.Read())
        {
            i = i + 1;
            CheckBox objCheckBox = new CheckBox();
            objCheckBox.Text = objReader.GetValue(1).ToString();
            objCheckBox.ID = objReader.GetValue(0).ToString();
            
            plchldr.Controls.Add(objCheckBox);
            if (i % 4 == 0)
            {
                plchldr.Controls.Add(new LiteralControl("<br>"));
            }
        }
        objReader.Close();
        dbCon.Close();
    }
    public void PlaceHolderFill(string procedure, PlaceHolder plchldr, int rowCount)
    {
        int i = 0;
        openConnection();
        objSqlCmd = new SqlCommand(procedure, dbCon);
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objReader = objSqlCmd.ExecuteReader();
        while (objReader.Read())
        {
            i = i + 1;
            CheckBox objCheckBox = new CheckBox();
            objCheckBox.Text = objReader.GetValue(1).ToString();
            objCheckBox.ID = objReader.GetValue(0).ToString();
            
            plchldr.Controls.Add(objCheckBox);
            if (i % rowCount == 0)
            {
                plchldr.Controls.Add(new LiteralControl("<br>"));
            }
        }
        objReader.Close();
        dbCon.Close();
    }
    public void PlaceHolderFill(string procedure, PlaceHolder plchldr, SqlCommand ObjCmd)
    {
        int i = 0;
        openConnection();
        ObjCmd.Connection = dbCon;
        plchldr.Controls.Clear();
        objReader = ObjCmd.ExecuteReader();
        while (objReader.Read())
        {
            i = i + 1;
            CheckBox objCheckBox = new CheckBox();
            objCheckBox.Text = objReader.GetValue(1).ToString();
            objCheckBox.ID = objReader.GetValue(0).ToString();
            plchldr.Controls.Add(objCheckBox);
            if (i % 2 == 0)
            {
                plchldr.Controls.Add(new LiteralControl("<br>"));
            }
        }
        objReader.Close();
        dbCon.Close();
    }
     //Yunus Function for deleting from any table:when table Name, field name and A value.
    public void Delete_A_Record(string table,string field, string code)
    {
        string str;
        try
        {
            openConnection();
            string st = "Delete from " + table + " where " + field + " =@val1";
            objSqlCmd = new SqlCommand(st, dbCon);
            objSqlCmd.Parameters.AddWithValue("@val1", code);
            objSqlCmd.ExecuteNonQuery();
            dbCon.Close();
        }
        catch
        {
            
        }
    }
    public static void emptyControls(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if ((c.Controls.Count > 0))
            {
                emptyControls(c);
            }
            else
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
                else if (c is DropDownList)
                {
                    ((DropDownList)c).SelectedIndex = 0;
                }
            }
        }
    }
     //faslu............

    //public void CreateMenu(Menu Menu1,string UserSession)
    //{
    //    if (UserSession != null)
    //    {
    //        SqlCommand ObjCmd = new SqlCommand("Edms_MenuCreation");
    //        ObjCmd.CommandType = CommandType.StoredProcedure;
    //        ObjCmd.Parameters.Add("@rollid", SqlDbType.Int).Value = Convert.ToInt32(UserSession);
    //        DataSet dsMenu = ReturnDataSet(ObjCmd, "Edms_MenuCreation");
    //        Menu1.Items.Clear();
    //        for (int rowcount = 0; rowcount < dsMenu.Tables[0].Rows.Count; rowcount++)
    //        {
    //            bool ExistingModule = false;
    //            for (int itemCnt = 0; itemCnt < Menu1.Items.Count; itemCnt++)
    //            {
    //                if (Menu1.Items[itemCnt].Value == dsMenu.Tables[0].Rows[rowcount][3].ToString())
    //                {
    //                    ExistingModule = true;
    //                    break;
    //                }
    //                else
    //                {
    //                    ExistingModule = false;
    //                }
    //            }
    //            if (ExistingModule == false)
    //            {
    //                MenuItem newMenuItem = new MenuItem();
    //                newMenuItem.Text = dsMenu.Tables[0].Rows[rowcount][1].ToString();
    //                newMenuItem.Value = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                //newMenuItem.NavigateUrl=dsMenu.Tables[0].Rows[rowcount][2].ToString();
    //                Menu1.Items.Add(newMenuItem);
    //                for (int RowCnt = 0; RowCnt < dsMenu.Tables[0].Rows.Count; RowCnt++)
    //                {
    //                    if (dsMenu.Tables[0].Rows[RowCnt][3].ToString() == menuItemValue)
    //                    {
    //                        MenuItem newSubMenu = new MenuItem();
    //                        newSubMenu.Text = dsMenu.Tables[0].Rows[RowCnt][0].ToString();
    //                        newSubMenu.Value = dsMenu.Tables[0].Rows[RowCnt][4].ToString();
    //                        newSubMenu.NavigateUrl = dsMenu.Tables[0].Rows[RowCnt][2].ToString()+"?Privilage="+dsMenu.Tables[0].Rows[RowCnt][5].ToString();
    //                        Menu1.Items[Menu1.Items.Count - 1].ChildItems.Add(newSubMenu);
    //                    }
    //                }
    //            }
    //        }
    //        MenuItem newMenuItemd = new MenuItem();
    //        newMenuItemd.Text = "Sign Out";
    //        newMenuItemd.Value = "0";
    //        newMenuItemd.NavigateUrl = "FrmLogin.aspx";
            
    //        //string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //        //newMenuItem.NavigateUrl=dsMenu.Tables[0].Rows[rowcount][2].ToString();
    //        Menu1.Items.Add(newMenuItemd);
    //    }
    //}

    //after modification of privilage settings

    //public void CreateMenu(Menu Menu1, string UserSession)
    //{
    //    if (UserSession != null)
    //    {
    //        SqlCommand ObjCmd = new SqlCommand("Edms_MenuCreation");
    //        ObjCmd.CommandType = CommandType.StoredProcedure;
    //        ObjCmd.Parameters.Add("@rollid", SqlDbType.Int).Value = Convert.ToInt32(UserSession);
    //        DataSet dsMenu = ReturnDataSet(ObjCmd, "Edms_MenuCreation");
    //        Menu1.Items.Clear();
    //        for (int rowcount = 0; rowcount < dsMenu.Tables[0].Rows.Count; rowcount++)
    //        {
    //            bool ExistingModule = false;
    //            for (int itemCnt = 0; itemCnt < Menu1.Items.Count; itemCnt++)
    //            {
    //                if (Menu1.Items[itemCnt].Value == dsMenu.Tables[0].Rows[rowcount][3].ToString())
    //                {
    //                    ExistingModule = true;
    //                    break;
    //                }
    //                else
    //                {
    //                    ExistingModule = false;
    //                }
    //            }
    //            if (ExistingModule == false)
    //            {
    //                MenuItem newMenuItem = new MenuItem();
    //                newMenuItem.Text = dsMenu.Tables[0].Rows[rowcount][1].ToString();
    //                newMenuItem.Value = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                //newMenuItem.NavigateUrl=dsMenu.Tables[0].Rows[rowcount][2].ToString();
    //                Menu1.Items.Add(newMenuItem);
    //                for (int RowCnt = 0; RowCnt < dsMenu.Tables[0].Rows.Count; RowCnt++)
    //                {
    //                    if (dsMenu.Tables[0].Rows[RowCnt][3].ToString() == menuItemValue)
    //                    {
    //                        MenuItem newSubMenu = new MenuItem();
    //                        newSubMenu.Text = dsMenu.Tables[0].Rows[RowCnt][0].ToString();
    //                        newSubMenu.Value = dsMenu.Tables[0].Rows[RowCnt][4].ToString();
    //                        string Privilage = ObjDgProp.EncryPt(dsMenu.Tables[0].Rows[RowCnt][5].ToString());
    //                        newSubMenu.NavigateUrl = dsMenu.Tables[0].Rows[RowCnt][2].ToString() + "?Event="+Privilage;
    //                        Menu1.Items[Menu1.Items.Count - 1].ChildItems.Add(newSubMenu);
    //                    }
    //                }
    //            }
    //        }
    //        MenuItem newMenuItemd = new MenuItem();
    //        newMenuItemd.Text = "Sign Out";
    //        newMenuItemd.Value = "0";
    //        newMenuItemd.NavigateUrl = "FrmLogin.aspx";

    //        //string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //        //newMenuItem.NavigateUrl=dsMenu.Tables[0].Rows[rowcount][2].ToString();
    //        Menu1.Items.Add(newMenuItemd);
    //    }
    //}

    //faslu...........end.......
    //public void CreateMenuForAclCustomer(Menu Menu1, string UserSession)
    //{
    //    if (UserSession != null)
    //    {
    //        SqlCommand ObjCmd = new SqlCommand("Edms_MenuCreation_For_Acl_Customer");
    //        ObjCmd.CommandType = CommandType.StoredProcedure;
    //        ObjCmd.Parameters.Add("@rollid", SqlDbType.Int).Value = Convert.ToInt32(UserSession);
    //        DataSet dsMenu = ReturnDataSet(ObjCmd, "Edms_MenuCreation_For_Acl_Customer");
    //        Menu1.Items.Clear();
    //        for (int rowcount = 0; rowcount < dsMenu.Tables[0].Rows.Count; rowcount++)
    //        {
    //            bool ExistingModule = false;
    //            for (int itemCnt = 0; itemCnt < Menu1.Items.Count; itemCnt++)
    //            {
    //                if (Menu1.Items[itemCnt].Value == dsMenu.Tables[0].Rows[rowcount][3].ToString())
    //                {
    //                    ExistingModule = true;
    //                    break;
    //                }
    //                else
    //                {
    //                    ExistingModule = false;
    //                }
    //            }
    //            if (ExistingModule == false)
    //            {
    //                MenuItem newMenuItem = new MenuItem();
    //                newMenuItem.Text = dsMenu.Tables[0].Rows[rowcount][1].ToString();
    //                newMenuItem.Value = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                //newMenuItem.NavigateUrl=dsMenu.Tables[0].Rows[rowcount][2].ToString();
    //                Menu1.Items.Add(newMenuItem);
    //                for (int RowCnt = 0; RowCnt < dsMenu.Tables[0].Rows.Count; RowCnt++)
    //                {
    //                    if (dsMenu.Tables[0].Rows[RowCnt][3].ToString() == menuItemValue)
    //                    {
    //                        MenuItem newSubMenu = new MenuItem();
    //                        newSubMenu.Text = dsMenu.Tables[0].Rows[RowCnt][0].ToString();
    //                        newSubMenu.Value = dsMenu.Tables[0].Rows[RowCnt][4].ToString();
    //                        newSubMenu.NavigateUrl = dsMenu.Tables[0].Rows[RowCnt][2].ToString() + "?Event=" + dsMenu.Tables[0].Rows[RowCnt][5].ToString();
    //                        Menu1.Items[Menu1.Items.Count - 1].ChildItems.Add(newSubMenu);
    //                    }
    //                }
    //            }
    //        }
    //        MenuItem newMenuItemd = new MenuItem();
    //        newMenuItemd.Text = "Sign Out";
    //        newMenuItemd.Value = "0";
    //        newMenuItemd.NavigateUrl = "FrmLogin.aspx";

    //        //string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //        //newMenuItem.NavigateUrl=dsMenu.Tables[0].Rows[rowcount][2].ToString();
    //        Menu1.Items.Add(newMenuItemd);
    //    }
    //}

    //public void FillMenu(EO.Web.Menu Menu1, string UserSession)
    //{
    //    if (UserSession != null)
    //    {
    //        SqlCommand ObjCmd = new SqlCommand("Edms_MenuCreation");
    //        ObjCmd.CommandType = CommandType.StoredProcedure;
    //        ObjCmd.Parameters.Add("@rollid", SqlDbType.Int).Value = Convert.ToInt32(UserSession);
    //        DataSet dsMenu = ReturnDataSet(ObjCmd, "Edms_MenuCreation");
    //        Menu1.Items.Clear();
    //        for (int rowcount = 0; rowcount < dsMenu.Tables[0].Rows.Count; rowcount++)
    //        {
    //            bool ExistingModule = false;
    //            for (int itemCnt = 0; itemCnt < Menu1.Items.Count; itemCnt++)
    //            {
    //                if (Menu1.Items[itemCnt].Value == dsMenu.Tables[0].Rows[rowcount][3].ToString())
    //                {
    //                    ExistingModule = true;
    //                    break;
    //                }
    //                else
    //                {
    //                    ExistingModule = false;
    //                }
    //            }
    //            if (ExistingModule == false)
    //            {
    //                EO.Web.MenuItem newMenuItem = new EO.Web.MenuItem(dsMenu.Tables[0].Rows[rowcount][1].ToString());
    //                newMenuItem.Width = 100;
    //                newMenuItem.Value = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                Menu1.Items.Add(newMenuItem);
    //                for (int RowCnt = 0; RowCnt < dsMenu.Tables[0].Rows.Count; RowCnt++)
    //                {
    //                    if (dsMenu.Tables[0].Rows[RowCnt][3].ToString() == menuItemValue)
    //                    {
    //                        EO.Web.MenuItem newSubMenu = new EO.Web.MenuItem(dsMenu.Tables[0].Rows[RowCnt][0].ToString());
    //                        newSubMenu.Width = 100;
    //                        newSubMenu.Value = dsMenu.Tables[0].Rows[RowCnt][4].ToString();
    //                        string Privilage = ObjDgProp.EncryPt(dsMenu.Tables[0].Rows[RowCnt][5].ToString());
    //                        newSubMenu.NavigateUrl = dsMenu.Tables[0].Rows[RowCnt][2].ToString() + "?Event=" + Privilage;
    //                        Menu1.Items[Menu1.Items.Count - 1].SubMenu.Items.Add(newSubMenu);
    //                    }
    //                }
    //            }
    //        }
    //        EO.Web.MenuItem newMenuItemd = new EO.Web.MenuItem("SIGN OUT");
    //        newMenuItemd.Width = 100;
    //        newMenuItemd.Value = "0";
    //        newMenuItemd.NavigateUrl = "FrmLogin.aspx";
    //        Menu1.Items.Add(newMenuItemd);
    //    }

    //}

    //public void FillMenuForAcl(EO.Web.Menu Menu1, string UserSession)
    //{
    //    if (UserSession != null)
    //    {
    //        SqlCommand ObjCmd = new SqlCommand("Edms_MenuCreation_For_Acl_Customer");
    //        ObjCmd.CommandType = CommandType.StoredProcedure;
    //        ObjCmd.Parameters.Add("@rollid", SqlDbType.Int).Value = Convert.ToInt32(UserSession);
    //        DataSet dsMenu = ReturnDataSet(ObjCmd, "Edms_MenuCreation_For_Acl_Customer");
    //        Menu1.Items.Clear();
    //        for (int rowcount = 0; rowcount < dsMenu.Tables[0].Rows.Count; rowcount++)
    //        {
    //            bool ExistingModule = false;
    //            for (int itemCnt = 0; itemCnt < Menu1.Items.Count; itemCnt++)
    //            {
    //                if (Menu1.Items[itemCnt].Value == dsMenu.Tables[0].Rows[rowcount][3].ToString())
    //                {
    //                    ExistingModule = true;
    //                    break;
    //                }
    //                else
    //                {
    //                    ExistingModule = false;
    //                }
    //            }
    //            if (ExistingModule == false)
    //            {
    //                EO.Web.MenuItem newMenuItem = new EO.Web.MenuItem(dsMenu.Tables[0].Rows[rowcount][1].ToString());
    //                newMenuItem.Value = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //                //newMenuItem.NavigateUrl=dsMenu.Tables[0].Rows[rowcount][2].ToString();
    //                Menu1.Items.Add(newMenuItem);
    //                for (int RowCnt = 0; RowCnt < dsMenu.Tables[0].Rows.Count; RowCnt++)
    //                {
    //                    if (dsMenu.Tables[0].Rows[RowCnt][3].ToString() == menuItemValue)
    //                    {
    //                        EO.Web.MenuItem newSubMenu = new EO.Web.MenuItem(dsMenu.Tables[0].Rows[RowCnt][0].ToString());
    //                        newSubMenu.Value = dsMenu.Tables[0].Rows[RowCnt][4].ToString();
    //                        newSubMenu.NavigateUrl = dsMenu.Tables[0].Rows[RowCnt][2].ToString() + "?Event=" + dsMenu.Tables[0].Rows[RowCnt][5].ToString();
    //                        Menu1.Items[Menu1.Items.Count - 1].SubMenu.Items.Add(newSubMenu);
    //                    }
    //                }
    //            }
    //        }
    //        EO.Web.MenuItem newMenuItemd = new EO.Web.MenuItem("Sign Out");
    //        newMenuItemd.Value = "0";
    //        newMenuItemd.NavigateUrl = "FrmLogin.aspx";

    //        //string menuItemValue = dsMenu.Tables[0].Rows[rowcount][3].ToString();
    //        //newMenuItem.NavigateUrl=dsMenu.Tables[0].Rows[rowcount][2].ToString();
    //        Menu1.Items.Add(newMenuItemd);
    //    }
    //}


   public DataSet ReturnDataSet(SqlCommand cmd)
    {
        openConnection();
        cmd.Connection = dbCon;
        cmd.CommandType = CommandType.StoredProcedure;
        objAdapter = new SqlDataAdapter(cmd);
        objDs = new DataSet();
        objAdapter.Fill(objDs, cmd.CommandText);
        dbCon.Close();
        cmd.Dispose();
        objAdapter.Dispose();
        return objDs;
    }
   public bool ExecuteQuery(SqlCommand objCmd, string sp)
    {
        try
        {
            openConnection();
            objTrans = dbCon.BeginTransaction();
            objCmd.Connection = dbCon;
            objCmd.Transaction = objTrans;
            objCmd.CommandText = sp;
            objCmd.CommandType = CommandType.StoredProcedure;
            int id = objCmd.ExecuteNonQuery();
            objTrans.Commit();
            return true;
        }
        catch (Exception ex)
        {
            objTrans.Rollback();
            return false;
        }
        finally
        {
            dbCon.Close();
        }

    }
   public bool ExecuteQuery(SqlCommand objCmd)
    {
        try
        {
            openConnection();
            objTrans = dbCon.BeginTransaction();
            objCmd.Connection = dbCon;
            objCmd.Transaction = objTrans;
            objCmd.CommandType = CommandType.StoredProcedure;
            int id = objCmd.ExecuteNonQuery();
            objTrans.Commit();
            return true;
        }
        catch (Exception ex)
        {
            objTrans.Rollback();
            return false;
        }
        finally
        {
            dbCon.Close();
        }

    }
    //public void CreateMenu(Menu Menu1, DataTable DtPrivilages)
    // {
    //     if (DtPrivilages.Rows.Count >0)
    //     {
    //         CLsDataGridProperties ObjDgProp = new CLsDataGridProperties();
    //        Menu1.Items.Clear();
    //         for (int rowcount = 0; rowcount < DtPrivilages.Rows.Count; rowcount++)
    //         {

    //                 MenuItem newMenuItem = new MenuItem();
    //                 newMenuItem.Text = DtPrivilages.Rows[rowcount][0].ToString();
    //                 newMenuItem.Value = DtPrivilages.Rows[rowcount][3].ToString();
    //                 string menuItemValue = DtPrivilages.Rows[rowcount][3].ToString();
    //                 newMenuItem.NavigateUrl = DtPrivilages.Rows[rowcount][4].ToString() + "?Event=" + ObjDgProp.EncryPt(DtPrivilages.Rows[rowcount][6].ToString());
    //                 newMenuItem.Target = "dispFrame";
    //                 Menu1.Items.Add(newMenuItem);

    //         }
    //         MenuItem newMenuItemd = new MenuItem();
    //         newMenuItemd.Text = "LOG OUT";
    //         newMenuItemd.Value = "0";
    //         newMenuItemd.NavigateUrl = "FrmLogin.aspx";


    //         //string menuItemValue = DtPrivilages.Rows[rowcount][3].ToString();
    //         //newMenuItem.NavigateUrl=DtPrivilages.Rows[rowcount][2].ToString();
    //         Menu1.Items.Add(newMenuItemd);
    //     }
    // }
    public void CreateMenu(Menu Menu1, DataTable DtForPrivilages, string varLanguage)
    {
        CLsDataGridProperties ObjDgProp = new CLsDataGridProperties();
        if (DtForPrivilages.Rows.Count > 0)
        {
            Menu1.Items.Clear();
            for (int rowcount = 0; rowcount < DtForPrivilages.Rows.Count; rowcount++)
            {
                bool ExistingModule = false;
                for (int itemCnt = 0; itemCnt < Menu1.Items.Count; itemCnt++)
                {
                    if (Menu1.Items[itemCnt].Value == DtForPrivilages.Rows[rowcount][2].ToString())
                    {
                        ExistingModule = true;
                        break;
                    }
                    else
                    {
                        ExistingModule = false;
                    }
                }
                if (ExistingModule == false)
                {
                    MenuItem newMenuItem = new MenuItem();
                    newMenuItem.Text = DtForPrivilages.Rows[rowcount][1].ToString();
                    newMenuItem.Value = DtForPrivilages.Rows[rowcount][2].ToString();
                    string menuItemValue = DtForPrivilages.Rows[rowcount][2].ToString();
                    newMenuItem.NavigateUrl = "frmBlankPage.aspx";
                    Menu1.Items.Add(newMenuItem);
                    for (int RowCnt = 0; RowCnt < DtForPrivilages.Rows.Count; RowCnt++)
                    {
                        if (DtForPrivilages.Rows[RowCnt][2].ToString() == menuItemValue)
                        {
                            MenuItem newSubMenu = new MenuItem();
                            newSubMenu.Text = DtForPrivilages.Rows[RowCnt][0].ToString();
                            newSubMenu.Value = DtForPrivilages.Rows[RowCnt][3].ToString();
                            string Privilage = ObjDgProp.EncryPt(DtForPrivilages.Rows[RowCnt][6].ToString());
                            newSubMenu.NavigateUrl = DtForPrivilages.Rows[RowCnt][4].ToString() + "?Event=" + Privilage;
                            newSubMenu.Target = "dispFrame";
                            Menu1.Items[Menu1.Items.Count - 1].ChildItems.Add(newSubMenu);
                        }
                    }
                }
            }
            MenuItem newMenuItemd = new MenuItem();

            if (varLanguage == "ar-SA")
            {
                newMenuItemd.Text = "تسجيل الخروج";
            }
            else
            {
                newMenuItemd.Text = "SIGNOUT";
            }

            newMenuItemd.Value = "0";
            newMenuItemd.NavigateUrl = "frmLogout.aspx";
            Menu1.Items.Add(newMenuItemd);
        }
    }
    /// <summary>
    /// This method is used to return dataTable by passing the SQLCommand object
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="ProcedureName"></param>
    /// <returns></returns>
    public DataTable ReturnDataTable(SqlCommand cmd)
   {
       try
       {
           openConnection();
           cmd.Connection = dbCon;
           cmd.CommandType = CommandType.StoredProcedure;
           //cmd.CommandText = ProcedureName;
           objAdapter = new SqlDataAdapter(cmd);
           DataTable objDataTable = new DataTable();
           objAdapter.Fill(objDataTable);
           dbCon.Close();
           cmd.Dispose();
           return objDataTable;
       }
       catch (Exception Ex)
       {
           throw Ex;
       }
       finally
       {
           dbCon.Close();
       }
   }
   /// <summary>
   /// This method is used to return dataTable with a specified Table Name.
   /// </summary>
   /// <param name="cmd"></param>
   /// <param name="ReturningTableName">Table Name using which the returning table could be accessed</param>
   /// <returns></returns>
   public DataTable ReturnDataTable(SqlCommand cmd, string ReturningTableName)
   {
       try
       {
           openConnection();
           cmd.Connection = dbCon;
           cmd.CommandType = CommandType.StoredProcedure;
           objAdapter = new SqlDataAdapter(cmd);
           DataTable objDataTable = new DataTable(ReturningTableName);
           objAdapter.Fill(objDataTable);
           dbCon.Close();
           return objDataTable;
       }
       catch (Exception Ex)
       {
           throw Ex;
       }
       finally
       {
           dbCon.Close();
       }
   }
   public String changeToWords(String numb, bool isCurrency)
   {

       String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";

       String endStr = (isCurrency) ? ("Only") : ("");

       try
       {

           int decimalPlace = numb.IndexOf(".");

           if (decimalPlace > 0)
           {

               wholeNo = numb.Substring(0, decimalPlace);

               points = numb.Substring(decimalPlace + 1);

               if (Convert.ToInt32(points) > 0)
               {

                   andStr = (isCurrency) ? ("and") : ("point");// just to separate whole numbers from points/cents

                   endStr = (isCurrency) ? ("Fills " + endStr) : ("");

                   pointStr = translateCents(points);

               }

           }

           val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);

       }

       catch { ;}

       return val;

   }
   private String tens(String digit)
   {

       int digt = Convert.ToInt32(digit);

       String name = null;

       switch (digt)
       {

           case 10:

               name = "Ten";

               break;

           case 11:

               name = "Eleven";

               break;

           case 12:

               name = "Twelve";

               break;

           case 13:

               name = "Thirteen";

               break;

           case 14:

               name = "Fourteen";

               break;

           case 15:

               name = "Fifteen";

               break;

           case 16:

               name = "Sixteen";

               break;

           case 17:

               name = "Seventeen";

               break;

           case 18:

               name = "Eighteen";

               break;

           case 19:

               name = "Nineteen";

               break;

           case 20:

               name = "Twenty";

               break;

           case 30:

               name = "Thirty";

               break;

           case 40:

               name = "Fourty";

               break;

           case 50:

               name = "Fifty";

               break;

           case 60:

               name = "Sixty";

               break;

           case 70:

               name = "Seventy";

               break;

           case 80:

               name = "Eighty";

               break;

           case 90:

               name = "Ninety";

               break;

           default:

               if (digt > 0)
               {

                   name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));

               }

               break;

       }

       return name;

   }
   private String ones(String digit)
   {

       int digt = Convert.ToInt32(digit);

       String name = "";

       switch (digt)
       {

           case 1:

               name = "One";

               break;

           case 2:

               name = "Two";

               break;

           case 3:

               name = "Three";

               break;

           case 4:

               name = "Four";

               break;

           case 5:

               name = "Five";

               break;

           case 6:

               name = "Six";

               break;

           case 7:

               name = "Seven";

               break;

           case 8:

               name = "Eight";

               break;

           case 9:

               name = "Nine";

               break;

       }

       return name;

   }
   private String translateCents(String cents)
   {

       String cts = "", digit = "", engOne = "";

       for (int i = 0; i < cents.Length; i++)
       {

           digit = cents[i].ToString();

           if (digit.Equals("0"))
           {

               engOne = "Zero";

           }

           else
           {

               engOne = ones(digit);

           }

           cts += " " + engOne;

       }

       return cts;

   }
   private String translateWholeNumber(String number)
   {

       string word = "";

       try
       {

           bool beginsZero = false;//tests for 0XX

           bool isDone = false;//test if already translated

           double dblAmt = (Convert.ToDouble(number));

           //if ((dblAmt > 0) && number.StartsWith("0"))

           if (dblAmt > 0)
           {//test for zero or digit zero in a nuemric

               beginsZero = number.StartsWith("0");

               int numDigits = number.Length;

               int pos = 0;//store digit grouping

               String place = "";//digit grouping name:hundres,thousand,etc...

               switch (numDigits)
               {

                   case 1://ones' range

                       word = ones(number);

                       isDone = true;

                       break;

                   case 2://tens' range

                       word = tens(number);

                       isDone = true;

                       break;

                   case 3://hundreds' range

                       pos = (numDigits % 3) + 1;

                       place = " Hundred ";

                       break;

                   case 4://thousands' range

                   case 5:

                   case 6:

                       pos = (numDigits % 4) + 1;

                       place = " Thousand ";

                       break;

                   case 7://millions' range

                   case 8:

                   case 9:

                       pos = (numDigits % 7) + 1;

                       place = " Million ";

                       break;

                   case 10://Billions's range

                       pos = (numDigits % 10) + 1;

                       place = " Billion ";

                       break;

                   //add extra case options for anything above Billion...

                   default:

                       isDone = true;

                       break;

               }

               if (!isDone)
               {//if transalation is not done, continue...(Recursion comes in now!!)

                   word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));

                   //check for trailing zeros

                   if (beginsZero) word = " and " + word.Trim();

               }

               //ignore digit grouping names

               if (word.Trim().Equals(place.Trim())) word = "";

           }

       }

       catch { ;}

       return word.Trim();

   }
   public static bool isMobileBrowser()
   {
       //GETS THE CURRENT USER CONTEXT
       HttpContext context = HttpContext.Current;

       //FIRST TRY BUILT IN ASP.NT CHECK
       if (context.Request.Browser.IsMobileDevice)
       {
           return true;
       }
       //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
       if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
       {
           return true;
       }
       //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
       if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
           context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
       {
           return true;
       }
       //AND FINALLY CHECK THE HTTP_USER_AGENT 
       //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
       if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
       {
           //Create a list of all mobile types
           string[] mobiles =
               new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

           //Loop through each item in the list created above 
           //and check if the header contains that text
           foreach (string s in mobiles)
           {
               if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                   ToLower().Contains(s.ToLower()))
               {
                   return true;
               }
           }
       }

       return false;
   }
 public static string returnRoleName(int varRoleId,string varCulture)
   {
       string varReturnString = "";
       if (varCulture.ToString().ToUpper() == "AR-SA")
       {
           if (varRoleId == 2)
           {
               varReturnString = "موظف";
           }
           else if (varRoleId == 3)
           {
               varReturnString = "ضابط الأفكار";
           }
           else if (varRoleId == 4)
           {
               varReturnString = "مدير مشاريع";
           }
           else if (varRoleId == 5)
           {
               varReturnString = "لجنة التظلمات";
           }
           else if (varRoleId == 8)
           {
               varReturnString = " مدير الجودة";
           }
           else if (varRoleId == 9)
           {
               varReturnString = "مقيّم";
           }
           else if (varRoleId == 10)
           {
               varReturnString = "إداري النظام";
           }
           else if (varRoleId == 11)
           {
               varReturnString = "زائر";
           }
           else if (varRoleId == 12)
           {
               varReturnString = "منفذ";
           }
           else if (varRoleId == 13)
           {
               varReturnString = "رئيس لجنة المقيّمين";
           }
           else if (varRoleId == 14)
           {
               varReturnString = "مدير مركز";
           }
       }
       else
       {
           if (varRoleId == 2)
           {
               varReturnString = "USER";
           }
           else if (varRoleId == 3)
           {
               varReturnString = "IDEATION OFFICER";
           }
           else if (varRoleId == 4)
           {
               varReturnString = "PROJECTS MANAGER";
           }
           else if (varRoleId == 5)
           {
               varReturnString = "APPEAL COMMITTEE";
           }
           else if (varRoleId == 8)
           {
               varReturnString = "QUALITY HEAD";
           }
           else if (varRoleId == 9)
           {
               varReturnString = "EVALUATOR";
           }
           else if (varRoleId == 10)
           {
               varReturnString = "SYSTEM ADMIN";
           }
           else if (varRoleId == 11)
           {
               varReturnString = "VISITOR";
           }
           else if (varRoleId == 12)
           {
               varReturnString = "IMPLEMENTER";
           }
           else if (varRoleId == 13)
           {
               varReturnString = "EVALUATORS COMMITTE LEADER";
           }
           else if (varRoleId == 14)
           {
               varReturnString = "REGISTRATION CENTRE MANAGER";
           }
       }
       return varReturnString;
   }
   public string ExecuteScalar(SqlCommand objCmd)
   {
     openConnection();
     objCmd.Connection = dbCon;
     var_Temp_Code = Convert.ToString(objCmd.ExecuteScalar());
     dbCon.Close();
     objCmd.Dispose();
     return var_Temp_Code;
   }
    
}
