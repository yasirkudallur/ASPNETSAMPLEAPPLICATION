using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

public partial class frmViewVisitors : System.Web.UI.Page
{

    string sqlQry = "";
    clsSqlHelp objHelp = new clsSqlHelp();
    clsBllVisitorInfo objBll = new clsBllVisitorInfo();
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
        string attachment = "attachment; filename=VISITORS.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        System.Web.UI.HtmlControls.HtmlForm frm = new System.Web.UI.HtmlControls.HtmlForm();
        dgMyTaskDetails.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(dgMyTaskDetails);
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
        if (txtIdn.Text.Trim() != "")
        {
            varSearchCriteria = varSearchCriteria + " AND " + "(VHT.IDN = @varIdn)";
        }

        if (txtDate.Text.Trim() != "")
        {
            varSearchCriteria = varSearchCriteria + " AND " + "convert(varchar(10),VHT.VISITED_DATE,103) ='" + this.txtDate.Text.Trim() + "'";
        }

        //if (txtVisitorName.Text.Trim() != "")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "VHT.NAME_AR LIKE N'%'+@varVisitorName+'%'";
        //    varSearchCriteria = varSearchCriteria + " OR " + "VHT.NAME_EN LIKE N'%'+@varVisitorName+'%'";
        //}

        //if (txtContactNumber.Text.Trim() != "")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "(VHT.MOB_NUMBER = @varMobileNo)";
        //}

        //if (txtEmailAddress.Text.Trim() != "")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "VHT.EMAIL LIKE N'%'+@varVisitorEmail+'%'";
        //}

        //string[] fromDate = this.txtFromDate.Text.Trim().Split('/');
        //string[] toDate = this.txtToDate.Text.Trim().Split('/');

        //if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "VHT.VISITED_DATE BETWEEN '" + fromDate[2] + "/" + fromDate[1] + "/" + fromDate[0] + "' AND  DATEADD(day,0,'" + toDate[2] + "/" + toDate[1] + "/" + toDate[0].ToString() + "')";
        //}
        //else if (txtFromDate.Text.Trim() != "")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "convert(varchar(10),VHT.VISITED_DATE,103) ='" + this.txtFromDate.Text.Trim() + "'";
        //}
        //else if (txtToDate.Text.Trim() != "")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "convert(varchar(10),VHT.VISITED_DATE,103) ='" + this.txtToDate.Text.Trim() + "'";
        //}
        //if (cmbMonth.SelectedItem.Value != "0")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "MONTH(VHT.VISITED_DATE) =" + cmbMonth.SelectedItem.Value;
        //}
        //if (cmbYear.SelectedItem.Value != "0")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "YEAR(VHT.VISITED_DATE) =" + cmbYear.SelectedItem.Value;
        //}

        //if (drpCategory.SelectedIndex != 0)
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "VHT.CATEGORY_ID =" + drpCategory.SelectedItem.Value;
        //}

        return varSearchCriteria;
    }

    private void grid()
    {
        //Method to check Current Page Index of Datagrid while Editing and Deleting records from it.
        if (dgMyTaskDetails.Items.Count % dgMyTaskDetails.PageSize == 1 && dgMyTaskDetails.CurrentPageIndex == dgMyTaskDetails.PageCount - 1 && dgMyTaskDetails.CurrentPageIndex != 0)
        {
            dgMyTaskDetails.CurrentPageIndex = dgMyTaskDetails.CurrentPageIndex - 1;
        }
    }

    private void gridFill()
    {
        try
        {
            sqlQry = buildQuery();

            objHelp.openConnection();
            
            string origQuery = "";

            prntQuery = "SELECT row_number() over (order by VHT.VISITOR_ID  DESC) as SNO,VHT.VISITOR_ID,CONVERT(VARCHAR(10),VHT.VISITED_DATE,103) VISITED_DATE, VHT.IDN, VHT.NAME_EN," +
            " VHT.NATIONALITY,VHT.GENDER," +
            " VHT.CATEGORY_ID,CASE WHEN CM.CATEGORY_NAME_AR IS NULL THEN [LOCATION_TEXT] ELSE CM.CATEGORY_NAME_AR END  CATEGORY,VHT.MOB_NUMBER,CONVERT(VARCHAR(10),VHT.DOB,103) DOB,  " +
            " convert(varchar(10), CARD_EXPIRY_DATE,13) CARD_EXPIRY_DATE" +
            " FROM " +
            " VISITOR_HEADER_TRN VHT "+
            " LEFT OUTER JOIN CATEGORY_MSTR CM ON VHT.CARD_NUMBER = CM.CATEGORY_ID ";

            if (sqlQry != "")
            {
                sqlQry = sqlQry.Remove(0, 4);
                origQuery = prntQuery + " WHERE " + sqlQry + " ORDER BY VHT.VISITOR_ID DESC ";
            }
            else
            {
                origQuery = prntQuery+ " ORDER BY VHT.VISITOR_ID DESC ";// +" AND AC.APP_MODE_STATUS_ID = 2";
            }

            objCmd = new SqlCommand(origQuery, objHelp.dbCon);

            if (txtIdn.Text.Trim() != "")
            {
                objCmd.Parameters.Add("@varIdn", SqlDbType.NVarChar).Value = txtIdn.Text;
            }
            //if (txtVisitorName.Text.Trim() != "")
            //{
            //    objCmd.Parameters.Add("@varVisitorName", SqlDbType.NVarChar).Value = txtVisitorName.Text;
            //}

            //if (txtContactNumber.Text.Trim() != "")
            //{
            //    objCmd.Parameters.Add("@varMobileNo", SqlDbType.NVarChar).Value = txtContactNumber.Text;
            //}


            //if (txtEmailAddress.Text.Trim() != "")
            //{
            //    objCmd.Parameters.Add("@varVisitorEmail", SqlDbType.NVarChar).Value = txtEmailAddress.Text;
            //}

            objDa = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDa.Fill(objDs);
            grid();
            dgMyTaskDetails.DataSource = objDs;

            //this.lblMessage.Text = "تم إيجاد   " + objDs.Tables[0].Rows.Count.ToString() + "   زائر  ";

            try
            {
                dgMyTaskDetails.DataBind();
            }
            catch (Exception ex)
            {
                dgMyTaskDetails.CurrentPageIndex = 0;
                dgMyTaskDetails.DataBind();
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
        dgMyTaskDetails.DataSource = objDs;
        dgMyTaskDetails.DataBind();
        objHelp.dbCon.Close();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["sUserId"] = "1";
        if (Session["sUserId"] == null)
        {
            Response.Redirect("~/frmLogout.aspx");
        }
        if (!IsPostBack)
        {
            //cmbStatus.SelectedIndex = 1;

            //objBll.fillCombo(drpSector, 0,0);
            //objBll.fillCombo(drpAccounts, 0, 0);

            //objBll.fillCombo(drpEvents, 0);


            
            //yearFill(Convert.ToInt32("2010"), DateTime.Now.Year, cmbYear);

            if (Request.QueryString["qsEmiratesId"] != null)
            {
                this.txtIdn.Text = Request.QueryString["qsEmiratesId"].ToString();
            }


            btnSearch_Click(null, null);
        }

    }
    string varEncryptedString = "";
    protected void dgMyTaskDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (exportFlag == 1)
        {
            e.Item.Cells[12].Visible = false;
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells[9].Text.Trim() == "1")//MALE
            {
                e.Item.Cells[9].Text = "Male";
            }
            else
            {
                e.Item.Cells[9].Text = "Female";
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gridFill();
    }
    int exportFlag = 0;
    protected void btnExport_Click(object sender, EventArgs e)
    {
        dgMyTaskDetails.AllowPaging = false;
        exportFlag = 1;
        btnSearch_Click(null, null);
        PrepareGridViewForExport(dgMyTaskDetails);
        ExportGridView();
    }
    protected void dgMyTaskDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void dgMyTaskDetails_ItemCommand(object source, DataGridCommandEventArgs e)
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
    protected void dgMyTaskDetails_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //ImageButton imgBtn = new ImageButton();
            //imgBtn = (ImageButton)e.Item.FindControl("imgBtnAcceptReject");

            //imgBtn.Attributes.Add("onClick", "javaScript:return confirm('Are you sure you want to checkout this visitor?');");
        }
            
     }
    bool varSucceed = false;
    protected void dgMyTaskDetails_ItemCommand1(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "EDIT")
        {
            Response.Redirect("frmDiagnosisDetailsTemp.aspx?qsVisitorId=" + e.Item.Cells[0].Text);
            //if (e.Item.Cells[0].Text == "1")//PRE-REGISTERED -> MARKED AS REGISTERED
            //{
            //objBll.dtCheckInTime = DateTime.Parse(DateTime.Now.Day.ToString()+"/"+DateTime.Now.Month.ToString()+"/"+DateTime.Now.Year.ToString()  , System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat);
            //objBll.strCheckInHHMMSS = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            //objBll.iStatus = 2;
            //objBll.iVisitorId = Convert.ToInt32(e.Item.Cells[1].Text);

            //objBll.updateVisitStatus();
            //}
            //else if (e.Item.Cells[0].Text == "2")//REGISTERED
            //{
            //objBll.dtCheckOutTime = DateTime.Parse(DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat);
            //objBll.strCheckOutHHMMSS = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            //objBll.iStatus = 3;
            //objBll.iVisitorId = Convert.ToInt32(e.Item.Cells[1].Text);

            //objBll.updateVisitStatus();
            //}
            //else if (e.Item.Cells[0].Text == "3")//EXIT
            //{
            //    Response.Redirect("~/VISITORS_MANAGEMENT/frmReadVisitorInfoAPI.aspx?qsVisitId=" + e.Item.Cells[1].Text);
            //}

            //btnSearch_Click(null, null);
        }
        else if (e.CommandName == "INFO")
        {
            Response.Redirect("frmDiagnosisDetailsTemp.aspx?qsVisitorId=" + e.Item.Cells[0].Text);
        }
      
      
    }
    protected void dgMyTaskDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgMyTaskDetails.CurrentPageIndex = e.NewPageIndex;
        btnSearch_Click(null, null);
    }

    protected void drpSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objBll.fillCombo(drpDepartment, Convert.ToInt32(drpSector.SelectedItem.Value), 0);
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objBll.fillCombo(drpSection, Convert.ToInt32(drpDepartment.SelectedItem.Value), 0);
    }

    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void drpAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objBll.fillCombo(drpLocations, Convert.ToInt32(drpAccounts.SelectedItem.Value),0);
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {

    }
}