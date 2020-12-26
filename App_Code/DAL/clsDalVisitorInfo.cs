using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for clsDalDepartment
/// </summary>
public class clsDalVisitorInfo : clsSqlHelp
{

	SqlCommand objCmd;
    DataSet objDs;

    public clsDalVisitorInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable loadDiagnosisDetails(int iVisitorId)
    {
        try
        {
            objCmd = new SqlCommand("[SP_VISITOR_MGMT]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varVisitorId", SqlDbType.BigInt).Value = iVisitorId;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 6;
            objCmd.Parameters.Add("@varReturnVisitorId", SqlDbType.VarChar).Direction = ParameterDirection.Output;
            objCmd.Parameters["@varReturnVisitorId"].Size = 400;
            return ReturnDataTable(objCmd);
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {

        }
    }

    public DataSet loadTestCompleteDetails(int iVisitorId)
    {
        try
        {
            objCmd = new SqlCommand("[SP_VISITOR_MGMT]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varVisitorId", SqlDbType.BigInt).Value = iVisitorId;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 7;
            objCmd.Parameters.Add("@varReturnVisitorId", SqlDbType.VarChar).Direction = ParameterDirection.Output;
            objCmd.Parameters["@varReturnVisitorId"].Size = 400;
            return ReturnDataSet(objCmd);
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {

        }
    }
    
    public DataTable loadUserInfo(int iUserId)
    {
        try
        {
            objCmd = new SqlCommand("[SP_VISITOR_MGMT]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 5;
            objCmd.Parameters.Add("@varValue", SqlDbType.BigInt).Value = iUserId;
            return ReturnDataTable(objCmd);
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {

        }
    }

    
    public string updateTestStatus(bool bTestStatus,int iTestType,int iVisitorId)
    {
        try
        {
            objCmd = new SqlCommand("[SP_VISITOR_MGMT]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 2;
            objCmd.Parameters.Add("@varVisitorId", SqlDbType.BigInt).Value = iVisitorId;
            objCmd.Parameters.Add("@varTestResult", SqlDbType.Bit).Value = bTestStatus;
            objCmd.Parameters.Add("@varTestType", SqlDbType.BigInt).Value = iTestType;
            
            objCmd.Parameters.Add("@varReturnVisitorId", SqlDbType.VarChar).Direction = ParameterDirection.Output;
            objCmd.Parameters["@varReturnVisitorId"].Size = 400;

            

            return ExecuteTransactionReturnValue(objCmd);

        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string manageAttachments(int iVisitorId,string sAttachmentName,int iTestType)
    {
        try
        {
            objCmd = new SqlCommand("[SP_VISITOR_MGMT]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 3;
            objCmd.Parameters.Add("@varVisitorId", SqlDbType.BigInt).Value = iVisitorId;
            objCmd.Parameters.Add("@varAttachmentName", SqlDbType.NVarChar).Value = sAttachmentName;
            objCmd.Parameters.Add("@varTestType", SqlDbType.BigInt).Value = iTestType;
            objCmd.Parameters.Add("@varReturnVisitorId", SqlDbType.VarChar).Direction = ParameterDirection.Output;
            objCmd.Parameters["@varReturnVisitorId"].Size = 400;
            return ExecuteTransactionReturnValue(objCmd);
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    
    public string saveRecords(clsBllVisitorInfo objBll)
    {
        try
        {
            objCmd = new SqlCommand("[SP_VISITOR_MGMT]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varIdn", SqlDbType.BigInt).Value = objBll.sIdn;
            objCmd.Parameters.Add("@varNameAr", SqlDbType.NVarChar).Value = objBll.sNameAr;
            objCmd.Parameters.Add("@varNameEn", SqlDbType.NVarChar).Value = objBll.sNameEn;
            if (objBll.dtDob.Year != 1)
            {
                objCmd.Parameters.Add("@varDob", SqlDbType.DateTime).Value = objBll.dtDob;
            }

            if (objBll.dtExpiryDate.Year != 1)
            {
                objCmd.Parameters.Add("@varCardExpiryDate", SqlDbType.DateTime).Value = objBll.dtExpiryDate;
            }
            


            objCmd.Parameters.Add("@varNationality", SqlDbType.NVarChar).Value = objBll.sNationality;


           

           
            objCmd.Parameters.Add("@varMobNumber", SqlDbType.VarChar).Value = objBll.sMobileVisitor;

            objCmd.Parameters.Add("@varClinicId", SqlDbType.BigInt).Value = objBll.iClinicId;
            objCmd.Parameters.Add("@varClinicText", SqlDbType.NVarChar).Value = objBll.sClinicName;


            objCmd.Parameters.Add("@varCategoryId", SqlDbType.BigInt).Value = objBll.iAge;
            objCmd.Parameters.Add("@varTrnOwner", SqlDbType.BigInt).Value = objBll.iTrnOwner;
            objCmd.Parameters.Add("@varGender", SqlDbType.VarChar).Value = objBll.sGender;

            if (objBll.iVisitorId == 0)
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 1;
            }
            else
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 2;
                objCmd.Parameters.Add("@varVisitorId", SqlDbType.BigInt).Value = objBll.iVisitorId;
            }
            objCmd.Parameters.Add("@varReturnVisitorId", SqlDbType.VarChar).Direction = ParameterDirection.Output;
            objCmd.Parameters["@varReturnVisitorId"].Size = 400;

            ExecuteTransactionReturnValue(objCmd);

            return objCmd.Parameters["@varReturnVisitorId"].Value.ToString();

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
            objCmd = new SqlCommand("[SP_VISITOR_MGMT]");
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 4;
            objCmd.Parameters.Add("@varVisitorId", SqlDbType.BigInt).Value = iVisitorId;
            objCmd.Parameters.Add("@varAttachmentName", SqlDbType.NVarChar).Value = sAttachmentName;
            objCmd.Parameters.Add("@varResultStatus", SqlDbType.BigInt).Value = iResultStatus;
            objCmd.Parameters.Add("@varReturnVisitorId", SqlDbType.VarChar).Direction = ParameterDirection.Output;
            objCmd.Parameters["@varReturnVisitorId"].Size = 400;
            return ExecuteTransactionReturnValue(objCmd);

        }
        catch (Exception ex)
        {
            return "";
        }
    }
    public void fillCombo(DropDownList cmb, int varValue,int varOperator)
    {
        try
        {
            objCmd = new SqlCommand("[SP_LOAD_MASTER_DATA]");
            objCmd.CommandType = CommandType.StoredProcedure;

            if (cmb.ID.ToString() == "drpClicnics")//NO
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 4;
            }
            else if (cmb.ID.ToString() == "drpNationality")
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 2;
            }
            else if (cmb.ID.ToString() == "drpCategory")
            { 
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 3;
            }
            
            else if (cmb.ID.ToString() == "drpSector" || cmb.ID.ToString() == "drpDepartment" || cmb.ID.ToString() == "drpSection" || cmb.ID.ToString() == "drpUnits")
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 1;
                objCmd.Parameters.Add("@varValue", SqlDbType.BigInt).Value = varValue;
            }
            else if (cmb.ID.ToString() == "drpVisitingPlace")
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 6;
            }
            else if (cmb.ID.ToString() == "drpUsers")
            {
                if (varOperator == 0)//SECTOR
                {
                    objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 7;
                }
                else if (varOperator == 1)//SECTOR
                {
                    objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 8;
                }
                else if (varOperator == 2)//DEPARTMENT
                {
                    objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 9;
                }
                else if (varOperator == 3)//SECTION
                {
                    objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 10;
                }
                else if (varOperator == 4)//UNIT
                {
                    objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 11;
                }

                objCmd.Parameters.Add("@varValue", SqlDbType.BigInt).Value = varValue;
            }
            else if (cmb.ID.ToString() == "drpAccounts")
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 2;
            }
            else if (cmb.ID.ToString() == "drpLocations")
            {
                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 3;
                objCmd.Parameters.Add("@varValue", SqlDbType.BigInt).Value = varValue;
            }

            fillComboDatabindInsert(objCmd,cmb,"NAME","ID","-1","--إختر--");
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }


    public string checkPendingTest(string strIdn)
    {
        objCmd = new SqlCommand("[SP_VISITOR_MGMT]");
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 5;
        objCmd.Parameters.Add("@varIdn", SqlDbType.NVarChar).Value = strIdn;
        objCmd.Parameters.Add("@varReturnVisitorId", SqlDbType.VarChar).Direction = ParameterDirection.Output;
        objCmd.Parameters["@varReturnVisitorId"].Size = 400;
        return ExecuteScalar(objCmd);
        //return getSingleCode(objCmd).ToString();

    }

    //public bool updateTaskStatus(clsBllTasks objBll)
    //{
    //    try
    //    {
    //        objCmd = new SqlCommand("[SP_TASK_TRN]");
    //        objCmd.CommandType = CommandType.StoredProcedure;
    //        objCmd.Parameters.Add("@varTaskStatus", SqlDbType.BigInt).Value = objBll.TaskStatus;
    //        objCmd.Parameters.Add("@varComments", SqlDbType.NVarChar).Value = objBll.Comments;
    //        objCmd.Parameters.Add("@varTaskId", SqlDbType.BigInt).Value = objBll.TaskId;
    //        objCmd.Parameters.Add("@varInitiatives", SqlDbType.Xml).Value = objBll.Initiatives;
    //        objCmd.Parameters.Add("@ApprovalStatus", SqlDbType.BigInt).Value = objBll.ApprovalStatus;
    //        //
    //        objCmd.Parameters.Add("@varLogTypeEn", SqlDbType.NVarChar).Value = objBll.LogTypeEn;
    //        objCmd.Parameters.Add("@varLogTypeAr", SqlDbType.NVarChar).Value = objBll.LogTypeAr;
    //        objCmd.Parameters.Add("@varTrnOwner", SqlDbType.BigInt).Value = objBll.TrnOwner;
    //        objCmd.Parameters.Add("@varLogTextAr", SqlDbType.NVarChar).Value = objBll.LogTextAr;
    //        objCmd.Parameters.Add("@varLogTextEn", SqlDbType.NVarChar).Value = objBll.LogTextEn;
    //        //
    //        if (objBll.TaskStatus == 3)//IF COMPLETED.
    //        {
    //            objCmd.Parameters.Add("@varCompletionDate", SqlDbType.DateTime).Value = objBll.CompletionDate;
    //        }

    //        objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 8;

    //        return  ExecuteTransaction(objCmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}

    //public void fillCombo(DropDownList cmb, string varCulture)
    //{
    //    try
    //    {
    //        objCmd = new SqlCommand("[SP_TASK_TRN]");

    //        if (cmb.ID.ToString() == "cmbTaskStatus")
    //        {
    //            if (varCulture == "ar-SA")
    //            {
    //                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 6;
    //            }
    //            else
    //            {
    //                objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 5;
    //            }
    //        }
    //        fillComboDatabind(objCmd, cmb, "NAME", "ID");
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally
    //    {

    //    }
    //}
    //public void fillGrid(DataGrid objGrid,string sqlQry)
    //{
    //    try
    //    {
    //        objCmd = new SqlCommand("[SP_DEPARTMENT]");
    //        objCmd.CommandType = CommandType.StoredProcedure;

    //            objCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 3;

    //        gridFill(objCmd,objGrid);
    //    }
    //    catch (Exception ex)
    //    { 

    //    }
    //}
    //public bool deleteRecords(int varOpFlag,int varRecordId)//i
    //{
    //    try
    //    {
    //        objCmd = new SqlCommand("[SP_TASK_TRN]");
    //        objCmd.CommandType = CommandType.StoredProcedure;

    //        if (varOpFlag == 0)
    //        {
    //            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 3;
    //            objCmd.Parameters.Add("@varTaskId", SqlDbType.BigInt).Value = varRecordId;
    //        }
    //        else if (varOpFlag == 1)//Delete initiative
    //        {
    //            objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 7;
    //            objCmd.Parameters.Add("@varInitiativeId", SqlDbType.BigInt).Value = varRecordId;
    //        }
    //        return ExecuteTransaction(objCmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}
    //public bool deleteTask(clsBllTasks objBll)//i
    //{
    //    try
    //    {
    //        objCmd = new SqlCommand("[SP_TASK_TRN]");
    //        objCmd.CommandType = CommandType.StoredProcedure;
    //        objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 3;
    //        objCmd.Parameters.Add("@varTaskId", SqlDbType.BigInt).Value = objBll.TaskId;
    //        objCmd.Parameters.Add("@varLogTypeEn", SqlDbType.NVarChar).Value = objBll.LogTypeEn;
    //        objCmd.Parameters.Add("@varLogTypeAr", SqlDbType.NVarChar).Value = objBll.LogTypeAr;
    //        objCmd.Parameters.Add("@varTrnOwner", SqlDbType.BigInt).Value = objBll.TrnOwner;
    //        objCmd.Parameters.Add("@varLogTextAr", SqlDbType.NVarChar).Value = objBll.LogTextAr;
    //        objCmd.Parameters.Add("@varLogTextEn", SqlDbType.NVarChar).Value = objBll.LogTextEn;

    //        return ExecuteTransaction(objCmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}

    //public string getEmailSuggester(string varEmailSuggester)
    //{
    //    objCmd = new SqlCommand("[SP_TASK_TRN]");
    //    objCmd.CommandType = CommandType.StoredProcedure;
    //    objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 9;
    //    objCmd.Parameters.Add("@varAssignedToEmail", SqlDbType.NVarChar).Value = varEmailSuggester;
    //    return getSingleCode(objCmd).ToString();

    //}
    //public string loadProjectBasicDetails(int varProjectId, int varOpFlag)//if varOpFlag =1 then, load basic project details of project start date and project end date.
    //{

    //    objCmd = new SqlCommand("[SP_TASK_TRN]");
    //    objCmd.CommandType = CommandType.StoredProcedure;
    //    objCmd.Parameters.Add("@varFlag", SqlDbType.BigInt).Value = 10;

    //    if (varOpFlag == 1)//load basic project details of start date and end date of project .
    //    {
    //        objCmd.Parameters.Add("@varSuggId", SqlDbType.BigInt).Value = varProjectId;
    //    }

    //    return getSingleCode(objCmd).ToString();

    //}

}