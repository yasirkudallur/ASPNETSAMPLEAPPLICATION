using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for clsBllApplicantDetails
/// </summary>
public class clsBllVisitorInfo
{
	public clsBllVisitorInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    clsDalVisitorInfo objDal = new clsDalVisitorInfo();

    public int iVisitorId
    {
        get;
        set;
    }

    public string sNameAr
    {
        get;
        set;
    }

    public string sNameEn
    {
        get;
        set;
    }

    public int iCategoryId
    {
        get;
        set;
    }

    public string sIdn
    {
        get;
        set;
    }

    public string sMobileVisitor
    {
        get;
        set;
    }



    public string sNationality
    {
        get;
        set;
    }
    public int iAge
    {
        get;
        set;
    }
    public string sGender
    {
        get;
        set;
    }

      public DateTime dtDob
    {
        get;
        set;
    }

    
    public int iTrnOwner
    {
        get;
        set;
    }

    public int iClinicId
    {
        get; set;
    }

    public string sClinicName
    {
        get;set;
    }
    public DateTime dtExpiryDate
    {
        get;set;
    }
    public string saveRecords()
    {
        try
        {
            return objDal.saveRecords(this);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string updateTestStaus(bool bTestStatus, int iTestType, int iVisitorId)//istatus = 1->Pre-Registered, 2->Registered, 3->Exit
    {
        try
        {
            return objDal.updateTestStatus(bTestStatus, iTestType, iVisitorId);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string manageAttachments(int iVisitorId, string sAttachmentName, int iTestType)
    {
        try
        {
            return objDal.manageAttachments(iVisitorId, sAttachmentName, iTestType);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string manageResult(int iVisitorId, string sAttachmentName, int iResultStatus)
    {
        try
        {
            return objDal.manageResult(iVisitorId, sAttachmentName, iResultStatus);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

   
    public void fillCombo(DropDownList cmbBox, int varValue, int varOperator)
    {
        objDal.fillCombo(cmbBox, varValue, varOperator);
    }

    public DataTable loadDiagnosisDetails(int iVisitorId)
    {
        try
        {
            return objDal.loadDiagnosisDetails(iVisitorId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataSet loadTestCompleteDetails(int iVisitorId)
    {
        try
        {
            return objDal.loadTestCompleteDetails(iVisitorId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable loadUserInfo(int iUserId)
    {
        try
        {
            return objDal.loadUserInfo(iUserId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string checkPendingTest(string strIdn)
    {
        try
        {
            return objDal.checkPendingTest(strIdn);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

}