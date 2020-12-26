using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
public partial class frmViewUsers : System.Web.UI.Page
{
    string sqlQry = "";
    clsSqlHelp objHelp = new clsSqlHelp();
    clsBllUsers objBllUser = new clsBllUsers();
    SqlCommand objCmd;
    SqlDataAdapter objDa;
    SqlDataReader objReader;
    string varSearchCriteria = "";
    string prntQuery = "";
    DataSet objDs;
    int varHour = 0;
    int varMinute = 0;
    string varDeptIds = "";
    bool varFlag = false;


    private string setDepartmentIds(int varDepartmentId)
    {
        objHelp.openConnection();
        objCmd = new SqlCommand("[SP_VACANCY_MSTR]", objHelp.dbCon);
        objCmd.Parameters.Add("@varDepartmentId", SqlDbType.BigInt).Value = varDepartmentId;
        objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 12;
        objCmd.CommandType = CommandType.StoredProcedure;
        objDa = new SqlDataAdapter(objCmd);
        objDs = new DataSet();
        objDa.Fill(objDs);
        objHelp.dbCon.Close();
        objCmd.Dispose();
        objDa.Dispose();
        for (int varRowIndex = 0; varRowIndex < objDs.Tables[0].Rows.Count; varRowIndex++)
        {
            varDeptIds = varDeptIds + "," + objDs.Tables[0].Rows[varRowIndex][0].ToString();
        }
        if (varDeptIds.Length > 0)
        {
            varDeptIds = varDeptIds.Remove(0, 1);
        }
        objDs.Dispose();
        return varDeptIds;
    }
    private string setDepartmentIdsByEmail()
    {
        objHelp.openConnection();
        objCmd = new SqlCommand("[SP_VACANCY_MSTR]", objHelp.dbCon);
        objCmd.Parameters.Add("@varEmailAddress", SqlDbType.NVarChar).Value = Session["sUserEmail"].ToString();
        objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 13;
        objCmd.CommandType = CommandType.StoredProcedure;
        objDa = new SqlDataAdapter(objCmd);
        objDs = new DataSet();
        objDa.Fill(objDs);
        objHelp.dbCon.Close();
        objCmd.Dispose();
        objDa.Dispose();
        for (int varRowIndex = 0; varRowIndex < objDs.Tables[0].Rows.Count; varRowIndex++)
        {
            varDeptIds = varDeptIds + "," + objDs.Tables[0].Rows[varRowIndex][0].ToString();
        }
        if (varDeptIds.Length > 0)
        {
            varDeptIds = varDeptIds.Remove(0, 1);
        }
        objDs.Dispose();
        return varDeptIds;
    }
    public void yearFill(int varFromYear, int varToYear, DropDownList cmbBox)
    {
        for (varFromYear = varFromYear; varFromYear <= varToYear; varFromYear++)
        {
            cmbBox.Items.Add(varFromYear.ToString());
        }
        cmbBox.Items.Insert(0, new ListItem("--السنة--", "0"));
    }
    private string EncryptString(string Message)
    {
        string Passphrase = "753$#*&^";
        byte[] Results;
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

        // Step 1. We hash the passphrase using MD5 
        // We use the MD5 hash generator as the result is a 128 bit byte array 
        // which is a valid length for the TripleDES encoder we use below
        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
        // Step 2. Create a new TripleDESCryptoServiceProvider object 
        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
        // Step 3. Setup the encoder 
        TDESAlgorithm.Key = TDESKey;
        TDESAlgorithm.Mode = CipherMode.ECB;
        TDESAlgorithm.Padding = PaddingMode.PKCS7;
        // Step 4. Convert the input string to a byte[] 
        byte[] DataToEncrypt = UTF8.GetBytes(Message);

        // Step 5. Attempt to encrypt the string 
        try
        {
            ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
        }
        finally
        {
            // Clear the TripleDes and Hashprovider services of any sensitive information 
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }
        // Step 6. Return the encrypted string as a base64 encoded string 
        return Convert.ToBase64String(Results);
    }
    private void ExportGridView()
    {
        string attachment = "attachment; filename=applications.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        System.Web.UI.HtmlControls.HtmlForm frm = new System.Web.UI.HtmlControls.HtmlForm();
        dgUsers.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(dgUsers);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    private void PrepareGridViewForExport(Control gv)
    {
        LinkButton lb = new LinkButton();
        Literal l = new Literal();
        string name = String.Empty;
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {
                l.Text = (gv.Controls[i] as LinkButton).Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            else if (gv.Controls[i].GetType() == typeof(DropDownList))
            {
                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            else if (gv.Controls[i].GetType() == typeof(CheckBox))
            {
                l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            if (gv.Controls[i].HasControls())
            {
                PrepareGridViewForExport(gv.Controls[i]);
            }
        }
    }

    private string buildQuery()
    {
        if (txtName.Text.Trim() != "")
        {
            varSearchCriteria = varSearchCriteria + " AND " + "UM.NAME LIKE N'%'+@varName+'%'";
        }

        if (txtIdn.Text.Trim() != "")
        {
            varSearchCriteria = varSearchCriteria + " AND " + "UM.IDN = @varIdn";
        }

        if (this.txtContactNumber.Text.Trim() != "")
        {
            varSearchCriteria = varSearchCriteria + " AND " + "(UM.MOB_NO = @varMobile"; 
            varSearchCriteria = varSearchCriteria + " OR " + "UM.LL_NO = @varMobile)";
        }

        return varSearchCriteria;
    }

    private void grid()
    {
        //Method to check Current Page Index of Datagrid while Editing and Deleting records from it.
        if (dgUsers.Items.Count % dgUsers.PageSize == 1 && dgUsers.CurrentPageIndex == dgUsers.PageCount - 1 && dgUsers.CurrentPageIndex != 0)
        {
            dgUsers.CurrentPageIndex = dgUsers.CurrentPageIndex - 1;
        }
    }
    private void gridFill()
    {
        try
        {
            sqlQry = buildQuery();

            objHelp.openConnection();

            string origQuery = "";

            prntQuery = "SELECT row_number() over (order by UM.USER_ID) as SNO,EMAIL_ADDRESS, PASSWORD, "+
            " NAME, GENDER, IDN, MOB_NO, LL_NO, ROLE_ID, ACTIVE,UM.USER_ID " +
            " FROM USER_MSTR UM";

            if (sqlQry != "")
            {
                sqlQry = sqlQry.Remove(0, 4);
                origQuery = prntQuery + " WHERE " + sqlQry + " ORDER BY NAME ASC ";// +" AND AC.APP_MODE_STATUS_ID = 2";
            }
            else
            {
                origQuery = prntQuery + " ORDER BY NAME ASC ";// +" AND AC.APP_MODE_STATUS_ID = 2";
            }

            objCmd = new SqlCommand(origQuery, objHelp.dbCon);

            if (txtName.Text.Trim() != "")
            {
                objCmd.Parameters.Add("@varName", SqlDbType.NVarChar).Value = txtName.Text;
            }

            if (txtIdn.Text.Trim() != "")
            {
                objCmd.Parameters.Add("@varIdn", SqlDbType.NVarChar).Value = txtIdn.Text;
            }

            if (this.txtContactNumber.Text.Trim() != "")
            {
                objCmd.Parameters.Add("@varMobile", SqlDbType.NVarChar).Value = txtContactNumber.Text;
            }


            objDa = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDa.Fill(objDs);
            grid();
            dgUsers.DataSource = objDs;
            try
            {
                dgUsers.DataBind();
            }
            catch (Exception ex)
            {
                dgUsers.CurrentPageIndex = 0;
                dgUsers.DataBind();
                string varMsg = "";
            }
        }
        catch (Exception ex)
        {
            string varMsg = "";
        }
    }
    public void fillComboDatabind(string sqlQry, DropDownList cmbList)
    {
        objHelp.openConnection();
        objCmd = new SqlCommand(sqlQry, objHelp.dbCon);
        objReader = objCmd.ExecuteReader();
        cmbList.DataSource = objReader;
        cmbList.DataTextField = "NAME";
        cmbList.DataValueField = "ID";
        cmbList.DataBind();
        cmbList.Items.Insert(0, new ListItem("--None--", "0"));
        objCmd.Dispose();
        objHelp.dbCon.Close();
    }
    private void fillGrid(int varFlag)
    {
        objHelp.openConnection();
        objCmd = new SqlCommand("[SP_DAILY_TASKS]", objHelp.dbCon);
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.Parameters.Add("@varOwner", SqlDbType.BigInt).Value = Convert.ToInt32(Session["user"].ToString());
        objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = varFlag;
        if (varFlag == 19)
        {
            //objCmd.Parameters.Add("@varClientId", SqlDbType.BigInt).Value = Convert.ToInt32(cmbPositionName.SelectedItem.Value);
        }
        //if (varFlag == 20)
        //{
        //    objCmd.Parameters.Add("@varTaskDate", SqlDbType.VarChar).Value = this.txtToDate.Text.Trim();
        //}
        objDa = new SqlDataAdapter(objCmd);
        DataSet objDs = new DataSet();
        objDa.Fill(objDs);
        dgUsers.DataSource = objDs;
        dgUsers.DataBind();
        objHelp.dbCon.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["sEmployeeId"] = "1";//Danger
        if (Session["sEmployeeId"] == null)
        {
            Response.Redirect("~/frmLogout.aspx");
        }

        if (!IsPostBack)
        {
            //objBllUser.fillCombo(drpSector, 0);
            //objBllUser.fillCombo(drpAccounts, 0);
            btnSearch_Click(null, null);
        }

        if (Session["sFromPage"] != null)
        {
            Session["sFromPage"] = null;
            btnSearch_Click(null, null);
        }
    }

    string varEncryptedString = "";
    protected void dgUsers_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //varEncryptedString = EncryptString("$$%" + e.Item.Cells[13].Text.Trim());
            //((LinkButton)e.Item.FindControl("lnkBtnName")).Attributes.Add("onClick", "javascript:return window.open('frmViewVisitorDetails.aspx?" + "qsVisitorId=" + e.Item.Cells[16].Text.Trim() + "',null,'toolbar=no,location=0,directories=0,status=0,scrollbars=1,resizable=1,width=1200,height=600,menubar=no,Fullscreen=no,titlebar=no,left=0,top=0;help:no'),false;");//NEWLY ADDED

            //if (e.Item.Cells[17].Text.Trim() == "0")//REJECTED
            //{
            //    e.Item.Cells[0].BackColor = System.Drawing.Color.Red;
            //}
            //else
            //{
            //    e.Item.Cells[0].BackColor = System.Drawing.Color.Green;
            //}
            //if (e.Item.Cells[20].Text.Trim() == "1")//SCHEDULE INTERVIEW
            //{
            //    e.Item.Cells[0].BackColor = System.Drawing.Color.Green;
            //    e.Item.Cells[19].Controls.Clear();
            //}
            //else if (e.Item.Cells[20].Text.Trim() == "2")//TRANSFERRED TO ANOTHER PERSON
            //{
            //    e.Item.Cells[0].BackColor = System.Drawing.Color.DarkOrange;
            //}
            //else if (e.Item.Cells[20].Text.Replace("&nbsp", "").Trim() == "")
            //{
            //    e.Item.Cells[0].BackColor = System.Drawing.Color.LightBlue;
            //}

            //
            //if (e.Item.Cells[15].Text.Replace("&nbsp", "").Trim() != "")
            //{
            //    ((System.Web.UI.HtmlControls.HtmlImage)e.Item.Cells[13].FindControl("PhotoBase64")).Src = "data:image/jpeg;base64," + e.Item.Cells[15].Text;
            //}

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gridFill();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        dgUsers.AllowPaging = false;
        btnSearch_Click(null, null);
        PrepareGridViewForExport(dgUsers);
        ExportGridView();
    }
    protected void dgUsers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgUsers_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        //        if (e.CommandName == "Edit")
        //        {
        //            Response.Redirect("frmDailyTaskEntry.aspx?qsTaskId=" + e.Item.Cells[13].Text.Trim());
        //        }
        //        else if (e.CommandName == "Delete")
        //        {
        //            sqlQry = "DELETE FROM TASK_TRN WHERE TASK_ID = " + e.Item.Cells[13].Text.Trim();
        //            objHelp.openConnection();
        //            objCmd = new SqlCommand(sqlQry, objHelp.dbCon);
        //            objCmd.ExecuteNonQuery();
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "btn",
        //"<script type = 'text/javascript'>alert('Task has been deleted successfully.');</script>");
        //            objHelp.dbCon.Close();
        //            gridFill();
        //        } 
    }
    protected void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (cmbDepartment.SelectedItem.Value != "0")
        //{
        //    sqlQry = "SELECT VACANCY_ID ID,POSITION_NAME NAME FROM VACANCY_MSTR WHERE DEPARTMENT_ID = " + cmbDepartment.SelectedItem.Value + " ORDER BY POSITION_NAME";
        //    fillComboDatabind(sqlQry, cmbPositionName);
        //}
        //else
        //{
        //    sqlQry = "SELECT VACANCY_ID ID,POSITION_NAME NAME FROM VACANCY_MSTR ORDER BY POSITION_NAME";
        //    fillComboDatabind(sqlQry, cmbPositionName);
        //}
    }

    protected void dgUsers_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ImageButton imgBtn = new ImageButton();
            imgBtn = (ImageButton)e.Item.FindControl("imgBtnDelete");

            imgBtn.Attributes.Add("onClick", "javaScript:return confirm('Are you sure you want to delete this user?');");
        }
    }

    bool varSucceed = false;
    protected void dgUsers_ItemCommand1(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "EDIT")
        {
            Response.Redirect("frmUsers.aspx?qsUserId=" + e.Item.Cells[0].Text);
        }
        else if (e.CommandName == "DELETE")
        {
            if (objBllUser.deleteRecords(Convert.ToInt32(e.Item.Cells[0].Text)) == true)
            {
                this.lblMessage.Text = "Deleted successfully.";
                this.btnSearch_Click(null, null);
            }
            else
            {
                this.lblMessage.Text = "Cant delete this user!!! Please check with System Admin.";
            }
        }
        else if (e.CommandName == "CP")
        {
            Response.Redirect("~/ACL/frmResetPassword.aspx?qsUserId="+ e.Item.Cells[0].Text);
        }
    }
    protected void btnNewUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmUsers.aspx");
    }
    protected void dgUsers_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgUsers.CurrentPageIndex = e.NewPageIndex;
        btnSearch_Click(null, null);
    }


  

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmUsers.aspx");
    }
}