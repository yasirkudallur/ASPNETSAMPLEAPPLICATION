using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class frmUsers : System.Web.UI.Page
{

    clsBllUsers objBll = new clsBllUsers();

    public void loadUserDetails()
    {
        try
        {
            DataSet objDs= objBll.loadUserDetails(Convert.ToInt32(ViewState["vsEditValue"].ToString()));

            if (objDs.Tables[0].Rows.Count > 0)
            {
                this.txtName.Text = objDs.Tables[0].Rows[0]["name_ar"].ToString();
                this.txtUserName.Text = objDs.Tables[0].Rows[0]["user_name"].ToString();
                hfPassword.Value = clsEncryption.Decryption(objDs.Tables[0].Rows[0]["password"].ToString().ToString(), "753$#*&^");
                //this.txtIdn.Text = objDs.Tables[0].Rows[0]["id_number"].ToString();
                this.txtMobileNumber.Text = objDs.Tables[0].Rows[0]["mobile_number"].ToString();
                //this.txtTel.Text = objDs.Tables[0].Rows[0]["land_line"].ToString();

              
                if (objDs.Tables[0].Rows[0]["is_active"].ToString() == "1")
                {
                    radActive.Items[0].Selected = true;
                }
                else
                {
                    radActive.Items[1].Selected = true;
                }
            }

        }
        catch (Exception ex)
        {

        }
        finally
        { 
        
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Session["sUserId"] = "1";//DANGER
        if (Session["sUserId"] == null)
        {
            Response.Redirect("~/frmLogout.aspx");
        }


        this.txtName.Attributes.Add("onkeypress", "javascript:return hideMessage();");

        btnSubmit.Attributes.Add("onClick", "javascript:return validateControls();");

        if (!IsPostBack)
        {

            this.tblMessage.Visible = false;

            radActive.Items[0].Selected = true;

            if (Request.QueryString["qsUserId"] != null)
            {
                btnSubmit.Text = "تحديث";
                ViewState["vsEditValue"] = Request.QueryString["qsUserId"].ToString();
                loadUserDetails();
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(750);
        this.lblMessage.Text = "";
        try
        {
            if (objBll.userExistance(txtUserName.Text, objBll.iUserId) == "")
            {
                if (ViewState["vsEditValue"] != null)
                {
                objBll.iUserId = Convert.ToInt32(ViewState["vsEditValue"].ToString());
                }
                else
                {
                objBll.iUserId = 0;
                }
                objBll.sNameAr = this.txtName.Text;
                //objBll.iGender = Convert.ToInt32(cmbGender.SelectedItem.Value);
                objBll.sUserName = this.txtUserName.Text;
                //objBll.sIdNumber = this.txtIdn.Text;
                objBll.sMobileNumber = this.txtMobileNumber.Text;
                //objBll.sLandLine = this.txtTel.Text;

                if (ViewState["vsEditValue"] == null)
                {
                    byte[] buffer = clsEncryption.Encryption(txtPassword.Text.Trim(), "753$#*&^");
                    objBll.sPassword = Convert.ToBase64String(buffer);
                }
                else
                {
                    if (this.txtPassword.Text.Trim() != "")
                    {
                        byte[] buffer = clsEncryption.Encryption(txtPassword.Text.Trim(), "753$#*&^");
                        objBll.sPassword = Convert.ToBase64String(buffer);
                    }
                    else
                    {
                        byte[] buffer = clsEncryption.Encryption(hfPassword.Value, "753$#*&^");
                        objBll.sPassword = Convert.ToBase64String(buffer);
                    }
                }
                objBll.iTrnOwner = Convert.ToInt32(Session["sUserId"].ToString());
                objBll.iActive = Convert.ToInt32(radActive.SelectedItem.Value);
                objBll.iRole = Convert.ToInt32(drpRoles.SelectedItem.Value);

                objBll.saveRecords();

                ViewState["vsEditValue"] = null;

                this.radActive.Items[0].Selected = true;
                this.radActive.Items[1].Selected = false;
                this.txtName.Focus();
                //objBll.fillGrid(dgDepartments, "");

                clsSqlHelp.emptyControls(this);

                this.lblMessage.Text = "تم حفظ بنجاح.";

                this.tblMessage.Visible = true;


                btnSubmit.Text = "حفظ";
                this.hfPassword.Value = "";

            }
            else
            {
                this.lblMessage.Text = "Email Already Registered!!!";
            }
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = "خطأ في حفظ السجل";
        }
    }



    protected void dgDepartments_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "EDIT")
        {
            this.txtName.Text = e.Item.Cells[1].Text.Trim().Replace("&nbsp;", "");

            ViewState["vsEditValue"] = e.Item.Cells[2].Text.Trim();

            if (e.Item.Cells[6].Text.Trim() == "1")
            {
                radActive.Items[0].Selected = true;
                radActive.Items[1].Selected = false;
            }
            else if (e.Item.Cells[7].Text.Trim() == "0")
            {
                radActive.Items[1].Selected = true;
                radActive.Items[0].Selected = false;
            }
        }
        else if (e.CommandName == "DELETE")
        {
            //objBll.DeleteRecord(Convert.ToInt32(e.Item.Cells[7].Text));
            //objBll.fillGrid(dgAssignments, "");
        }
    }
    protected void dgDepartments_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton delBtn = new LinkButton();
            delBtn = (LinkButton)e.Item.FindControl("lnkBtnDelete");
            delBtn.Attributes.Add("onClick", "javaScript:return confirm('هل تريد بالتأكيد حذف هذا السجل؟');");
        }
    }
    protected void dgDepartments_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells[6].Text.Trim() == "1")
            {
                e.Item.Cells[3].Text = "نعم";
            }
            else if (e.Item.Cells[6].Text.Trim() == "0")
            {
                e.Item.Cells[3].Text = "لا";
            }
        }
    }
    protected void dgDepartments_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        //dgDepartments.CurrentPageIndex = e.NewPageIndex;
        //objBll.fillGrid(dgDepartments, "");
    }
    protected void btnViewUsers_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmViewUsers.aspx");
    }
    protected void btnCLear_Click(object sender, EventArgs e)
    {
        ViewState["vsEditValue"] = null;
        clsSqlHelp.emptyControls(this);
    }

  
}