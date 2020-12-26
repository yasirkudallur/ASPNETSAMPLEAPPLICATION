using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class frmDiagnosisDetailsTemp : basePage
{
    clsBllVisitorInfo objBll = new clsBllVisitorInfo();
    string varAppender = "";
    DataTable objTbl;
    DataSet objDs;

    public void loadDiagnosisDetails(int iVisitorId)
    {
        try
        {
            objDs = objBll.loadTestCompleteDetails(iVisitorId);
            //objTbl = objBll.loadDiagnosisDetails(iVisitorId);

            if (objDs.Tables[0].Rows.Count > 0)
            {
                this.txtDateSampleTaken.Text = objDs.Tables[0].Rows[0]["VISITED_DATE"].ToString();
                this.lblSampleTaken.Text = this.txtDateSampleTaken.Text;
                this.idn.Text = objDs.Tables[0].Rows[0]["IDN"].ToString();
                this.lblIdn.Text = this.idn.Text;
                this.cardHolderNameEn.Text = objDs.Tables[0].Rows[0]["NAME_EN"].ToString();
                this.lblNameEn.Text = this.cardHolderNameEn.Text;
                if (objDs.Tables[0].Rows[0]["GENDER"].ToString() == "1")
                {
                    this.radGender.Items[0].Selected = true;
                    this.lblSex.Text = "Male";
                }
                else
                {
                    this.radGender.Items[1].Selected = true;
                    this.lblSex.Text = "Female";
                }
                this.txtNationality.Text = objDs.Tables[0].Rows[0]["NATIONALITY"].ToString();
                this.lblNationality.Text = this.txtNationality.Text;
                this.txtAge.Text = objDs.Tables[0].Rows[0]["CATEGORY_ID"].ToString();
                this.lblAge.Text = this.txtAge.Text;
                this.txtTelNumber.Text = objDs.Tables[0].Rows[0]["MOB_NUMBER"].ToString();
                //this.lblMobile.Text = this.txtTelNumber.Text;
                this.txtClinicNae.Text = objDs.Tables[0].Rows[0]["CATEGORY_NAME_AR"].ToString();
                this.txtExpiryDate  .Text = objDs.Tables[0].Rows[0]["CARD_EXPIRY_DATE"].ToString();
                this.txtDob.Text = objDs.Tables[0].Rows[0]["DOB"].ToString();

            }
        }
        catch (Exception ex)
        {
        }
    }

    public void updateTestStatus(bool bTestStatus,int iTestType)
    {
        objBll.updateTestStaus(bTestStatus, iTestType, Convert.ToInt32(ViewState["vsVisitorId"].ToString()) );
    }

    public void manageAttachments(int iVisitorId, int iTestType,FileUpload fuName)
    {
        
        if (fuName.PostedFile.FileName != "")
        {

            fuName.SaveAs(Server.MapPath(".").Replace("\\VISITORS_MANAGEMENT", "") + "\\Attachments\\" + "EC_" + varAppender + fuName.FileName.Replace(fuName.FileName, "") + System.IO.Path.GetExtension(fuName.PostedFile.FileName));
            objBll.manageAttachments(iVisitorId, fuName.PostedFile.FileName, iTestType);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //objBll.fillCombo(drpCategory, 0, 0);

            this.tblMessage.Visible = false;

            this.tblMainControls.Visible = true;

            ViewState["vsVisitorId"] = Request.QueryString["qsVisitorId"].ToString();

            //this.lblAttachFitnessCertificate.Text = "إضافة شهادة اللياقة : ";

            loadDiagnosisDetails(Convert.ToInt32(ViewState["vsVisitorId"].ToString()));

        }
    }

    protected void chkTestOneEye_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestOneEye.Checked, 1);
    }

    protected void chkTestOneHeight_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestOneHeight.Checked, 2);
    }

    protected void chkTestOneWeight_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestOneWeight.Checked, 3);
    }

    protected void chkTestTwoBlood_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestTwoBlood.Checked, 4);
    }

    protected void chkTestTwoUrine_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestTwoUrine.Checked, 5);
    }

    protected void chkTestThreeXray_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestThreeXray.Checked, 6);
    }

    protected void chkTestFourEye_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestFourEye.Checked, 7);
    }

    protected void chkTestFourBone_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestFourBone.Checked, 8);
    }

    protected void chkTestFourSkin_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestFourSkin.Checked, 9);
    }

    protected void chkTestFourPsych_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestFourPsych.Checked, 10);
    }

    protected void chkTestFourSurgery_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestFourSurgery.Checked, 11);
    }

    protected void chkTestFourGp_CheckedChanged(object sender, EventArgs e)
    {
        //updateTestStatus(chkTestFourGp.Checked, 12);
    }

    protected void btnAddFileTestOne_Click(object sender, EventArgs e)
    {
        //manageAttachments(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), 1, fuTestOne);
    }

    protected void btnAddFileTestTwo_Click(object sender, EventArgs e)
    {
        //manageAttachments(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), 2, fuTestOne);
    }

    protected void btnAddFileTestThree_Click(object sender, EventArgs e)
    {
        //manageAttachments(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), 3, fuTestOne);
    }

    protected void btnAddFileTestFour_Click(object sender, EventArgs e)
    {
        //manageAttachments(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), 1, fuTestFour);
    }

    protected void radFitnessSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (radFitnessSelection.Items[0].Selected)//FIT
        //{
        //    this.lblAttachFitnessCertificate.Text = "إضافة شهادة اللياقة : ";
        //}
        //else if (radFitnessSelection.Items[1].Selected)//NOT FIT
        //{
        //    this.lblAttachFitnessCertificate.Text = "تقرير من جهة خارجية : ";
        //}
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //objBll.manageResult(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), this.fuFitCertificate.PostedFile.FileName, Convert.ToInt32(radFitnessSelection.SelectedItem.Value));

            this.lblMessage.Text = "Diagnosis results have been submitted successfully.";

            this.tblMessage.Visible = true;

            this.tblMainControls.Visible = false;
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }
}