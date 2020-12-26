using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmDepartmentMaster : System.Web.UI.Page
{

    clsBllDepartment objBll = new clsBllDepartment();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sEmployeeId"] == null)
        {
            Response.Redirect("~/frmLogout.aspx");
        }

        btnSubmit.Attributes.Add("onClick", "javascript:return validateControls();");

        if (!IsPostBack)
        {
            radActive.Items[0].Selected = true;
            //objBll.fillCombo(drpDepartments, 0);
            objBll.fillGrid(dgDepartments,"");

            //DISABLING FOR AUDITOR :: START
            if (Session["sUserType"].ToString() == "14")//IF AUDITOR
            {
                btnSubmit.Visible = false;
            }
            //DISABLING FOR AUDITOR :: END
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";
        try
        {
            objBll.DepartmentNameAr = this.txtDepartment.Text;

            objBll.iActive = Convert.ToInt32(radActive.SelectedItem.Value);

            if (ViewState["vsEditValue"] != null)
            {
                objBll.DepartmentId = Convert.ToInt32(ViewState["vsEditValue"].ToString());
                ViewState["vsEditValue"] = null;
            }
            else
            {
                objBll.DepartmentId = 0;
            }
            objBll.saveRecords();

            this.txtDepartment.Text = "";
            this.radActive.Items[0].Selected = true;
            this.radActive.Items[1].Selected = false;
            this.txtDepartment.Focus();
            objBll.fillGrid(dgDepartments, "");
            this.lblMessage.Text = "تم حفظ بنجاح.";
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
            this.txtDepartment.Text = e.Item.Cells[1].Text.Trim().Replace("&nbsp;", "");

            ViewState["vsEditValue"] = e.Item.Cells[2].Text.Trim();

            if (e.Item.Cells[6].Text.Trim() == "1")
            {
                radActive.Items[0].Selected = true;
                radActive.Items[1].Selected = false;
            }
            else if (e.Item.Cells[6].Text.Trim() == "0")
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
        dgDepartments.CurrentPageIndex = e.NewPageIndex;
        objBll.fillGrid(dgDepartments, "");
    }
}