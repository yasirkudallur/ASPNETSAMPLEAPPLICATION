using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VISITORS_MANAGEMENT_frmDiagnosisDetails : basePage
{
    clsBllVisitorInfo objBll = new clsBllVisitorInfo();
    string varAppender = "";
    DataTable objTbl;

    public void loadDiagnosisDetails(int iVisitorId)
    {
        try
        {
            objTbl = objBll.loadDiagnosisDetails(iVisitorId);

            if (objTbl.Rows.Count > 0)
            {
                if (objTbl.Rows[0]["TEST_ONE_EYE"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestOneEye.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_ONE_HEIGHT"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestOneHeight.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_ONE_WEIGHT"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestOneWeight.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_TWO_BLOOD"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestTwoBlood.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_TWO_URINE"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestTwoUrine.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_THREE_XRAY"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestThreeXray.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_FOUR_EYE"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourEye.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_FOUR_BONE"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourBone.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_FOUR_SKIN"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourSkin.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_FOUR_PSYCH"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourPsych.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_FOUR_SURGERY"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourSurgery.Checked = true;
                }

                if (objTbl.Rows[0]["TEST_FOUR_GP"].ToString().ToUpper() == "TRUE")
                {
                    this.chkTestFourGp.Checked = true;
                }


                if (objTbl.Rows[0]["RESULT_STATUS"].ToString().ToUpper() == "1")
                {
                    this.radFitnessSelection.Items[0].Selected = true;
                }
                else if (objTbl.Rows[0]["RESULT_STATUS"].ToString().ToUpper() == "0")
                {
                    this.radFitnessSelection.Items[1].Selected = true;
                }

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

            this.tblMessage.Visible = false;

            this.tblMainControls.Visible = true;

            ViewState["vsVisitorId"] = Request.QueryString["qsVisitorId"].ToString();

            this.lblAttachFitnessCertificate.Text = "إضافة شهادة اللياقة : ";

            loadDiagnosisDetails(Convert.ToInt32(ViewState["vsVisitorId"].ToString()));

        }
    }

    protected void chkTestOneEye_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestOneEye.Checked, 1);
    }

    protected void chkTestOneHeight_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestOneHeight.Checked, 2);
    }

    protected void chkTestOneWeight_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestOneWeight.Checked, 3);
    }

    protected void chkTestTwoBlood_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestTwoBlood.Checked, 4);
    }

    protected void chkTestTwoUrine_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestTwoUrine.Checked, 5);
    }

    protected void chkTestThreeXray_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestThreeXray.Checked, 6);
    }

    protected void chkTestFourEye_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestFourEye.Checked, 7);
    }

    protected void chkTestFourBone_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestFourBone.Checked, 8);
    }

    protected void chkTestFourSkin_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestFourSkin.Checked, 9);
    }

    protected void chkTestFourPsych_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestFourPsych.Checked, 10);
    }

    protected void chkTestFourSurgery_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestFourSurgery.Checked, 11);
    }

    protected void chkTestFourGp_CheckedChanged(object sender, EventArgs e)
    {
        updateTestStatus(chkTestFourGp.Checked, 12);
    }

    protected void btnAddFileTestOne_Click(object sender, EventArgs e)
    {
        manageAttachments(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), 1, fuTestOne);
    }

    protected void btnAddFileTestTwo_Click(object sender, EventArgs e)
    {
        manageAttachments(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), 2, fuTestOne);
    }

    protected void btnAddFileTestThree_Click(object sender, EventArgs e)
    {
        manageAttachments(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), 3, fuTestOne);
    }

    protected void btnAddFileTestFour_Click(object sender, EventArgs e)
    {
        manageAttachments(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), 1, fuTestFour);
    }

    protected void radFitnessSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radFitnessSelection.Items[0].Selected)//FIT
        {
            this.lblAttachFitnessCertificate.Text = "إضافة شهادة اللياقة : ";
        }
        else if (radFitnessSelection.Items[1].Selected)//NOT FIT
        {
            this.lblAttachFitnessCertificate.Text = "تقرير من جهة خارجية : ";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objBll.manageResult(Convert.ToInt32(ViewState["vsVisitorId"].ToString()), this.fuFitCertificate.PostedFile.FileName, Convert.ToInt32(radFitnessSelection.SelectedItem.Value));

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