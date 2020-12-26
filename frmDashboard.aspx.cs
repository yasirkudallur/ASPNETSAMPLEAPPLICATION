using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using InfoSoftGlobal;

public partial class frmDashboard : basePage
{

    string sqlQry = "";
    clsSqlHelp objHelp = new clsSqlHelp();
    SqlCommand objCmd;
    SqlDataAdapter objDa;
    SqlDataReader objReader;
    string varSearchCriteria = "";
    string prntQuery = "";
    DataSet objDs;
    int varHour = 0;
    int varMinute = 0;

    string[] color = new string[12];
    DataTable dt = new DataTable("Chart");
    string GraphWidth = "200";
    string GraphHeight = "300";

    //PENDING :: START
    private void ConfigureColorsPending()
    {
        color[0] = "FF0000";
        color[1] = "EBE2D6";
    }
    
    private DataTable LoadGraphDataPending()
    {
        dt = returnTablePending(0);
        return dt;
    }
    
    private DataTable returnTablePending(int varType)
    {
        try
        {
            varSearchCriteria = "";
            varSearchCriteria = buildQuery();
            string sqlQry = "";


            if (Session["sUserType"].ToString() == "1")//System Admin
            {
                sqlQry = "SELECT TOTAL,STATUS,CNT,CASE WHEN TOTAL = 0 THEN 'N/A' ELSE CONVERT(VARCHAR,CONVERT(NUMERIC,CONVERT(float,(CNT))/CONVERT(float,TOTAL)*100,2)) END PERC FROM  " +
                " (SELECT (SELECT ISNULL(COUNT(*),0) FROM dbo.VISITOR_DETAILS_MSTR VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID " +
                " WHERE DAY(TRN_DATE) = " + DateTime.Now.Day + " AND MONTH(TRN_DATE) = " + DateTime.Now.Month + " AND YEAR(TRN_DATE) = " + DateTime.Now.Year + " ) TOTAL, " +
                " 'CHECK IN' STATUS ,ISNULL(COUNT(*),0) CNT FROM  dbo.VISITOR_DETAILS_MSTR  VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID " +
                " WHERE DAY(TRN_DATE) = " + DateTime.Now.Day + " AND MONTH(TRN_DATE) = " + DateTime.Now.Month + " AND YEAR(TRN_DATE) = " + DateTime.Now.Year + " AND STATUS =0 ) " +
                " ROOTA";
            }
            else
            {
                sqlQry = "SELECT TOTAL,STATUS,CNT,CASE WHEN TOTAL = 0 THEN 'N/A' ELSE CONVERT(VARCHAR,CONVERT(NUMERIC,CONVERT(float,(CNT))/CONVERT(float,TOTAL)*100,2)) END PERC FROM  " +
                " (SELECT (SELECT ISNULL(COUNT(*),0) FROM dbo.VISITOR_DETAILS_MSTR VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID " +
                " WHERE EM.BRANCH_ID = " + Session["sBranchId"].ToString() + " AND DAY(TRN_DATE) = " + DateTime.Now.Day + " AND MONTH(TRN_DATE) = " + DateTime.Now.Month + " AND YEAR(TRN_DATE) = " + DateTime.Now.Year + " ) TOTAL, " +
                " 'CHECK IN' STATUS ,ISNULL(COUNT(*),0) CNT FROM  dbo.VISITOR_DETAILS_MSTR  VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID " +
                " WHERE EM.BRANCH_ID = " + Session["sBranchId"].ToString() + " AND DAY(TRN_DATE) = " + DateTime.Now.Day + " AND MONTH(TRN_DATE) = " + DateTime.Now.Month + " AND YEAR(TRN_DATE) = " + DateTime.Now.Year + " AND STATUS =0 ) " +
                " ROOTA";
            }


           


            objCmd = new SqlCommand(sqlQry, objHelp.dbCon);
            objDa = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDa.Fill(objDs);

            this.lblTotalPending.Text = objDs.Tables[0].Rows[0][0].ToString();

            lblDayVisitorsValue.Text = objDs.Tables[0].Rows[0][0].ToString();

            lblPendingCount.Text = objDs.Tables[0].Rows[0][2].ToString();
            //lnkTotalPendingSuggestionsCount.Text = objDs.Tables[0].Rows[0][2].ToString();

            //lblAssessedIdeasValues.Text = (Convert.ToInt32(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToInt32(lblPendingCount.Text)).ToString();

            DataRow objRow = objDs.Tables[0].NewRow();
            objRow[0] = objDs.Tables[0].Rows[0][0];
            objRow[1] = " ";
            objRow[2] = Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString());
            objRow[3] = Math.Round(((Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString())) / (Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()))) * 100, 2);
            objDs.Tables[0].Rows.Add(objRow);
            objDs.Tables[0].AcceptChanges();

            //lblPercPending.Text = "%" + objRow[3].ToString();
            //
            return objDs.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    private void CreateDoughnutGraphPending()
    {
        try
        {
            //string strCaption = "النسب المئوية للحالة";
            //string strSubCaption = "2011 - 2014";
            string strCaption = "  ";
            string strSubCaption = "  ";
            string xAxis = "Status";
            string yAxis = "Count";

            //strXML will be used to store the entire XML document generated
            string strXML = null;

            //Generate the graph element
            strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='1' 
                          pieSliceDepth='30' formatNumberScale='0'
                          xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' rotateNames='1'
                    >";

            int i = 0;

            foreach (DataRow DR in dt.Rows)
            {
                strXML += "<set name='" + DR[1].ToString() + "' value='" + DR[2].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + DR["STATUS"].ToString() + ", " + DR["PERC"].ToString() + "'); &quot;/>";
                i++;
            }

            //Finally, close <graph> element
            strXML += "</graph>";

            string varReturnXml = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_Doughnut2D.swf", // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph1",                      // Unique chart ID
                GraphWidth, GraphHeight,                   // Width & Height of chart
                false
                );

            varReturnXml = varReturnXml.Replace("<embed","<embed wmode='transparent'");

            ltrlPngSug.Text = varReturnXml;

        }
        catch (Exception ex)
        {
            ltrlPngSug.Text = null;
            ltrlPngSug.DataBind();
        }

    }
    //PENDING :: END

    //ONGOING :: START
    
    private void ConfigureColorsOngoing()
    {
        color[0] = "E8952B";
        color[1] = "EBE2D6";
    }
    
    private DataTable LoadGraphDataOngoing()
    {
        dt = returnTableOngoing(0);
        return dt;
    }
    
    private DataTable returnTableOngoing(int varType)
    {
        try
        {
            varSearchCriteria = "";
            varSearchCriteria = buildQuery();
            string sqlQry = "";
            if (Session["sUserType"].ToString() == "1")//System Admin
            {
                sqlQry = "SELECT TOTAL,STATUS,CNT,CASE WHEN TOTAL = 0 THEN 'N/A' ELSE CONVERT(VARCHAR,CONVERT(NUMERIC,CONVERT(float,(CNT))/CONVERT(float,TOTAL)*100,2)) END PERC FROM  " +
                " (SELECT (SELECT ISNULL(COUNT(*),0) FROM dbo.VISITOR_DETAILS_MSTR VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID " +
                " WHERE DAY(TRN_DATE) = " + DateTime.Now.Day + " AND MONTH(TRN_DATE) = " + DateTime.Now.Month + " AND YEAR(TRN_DATE) = " + DateTime.Now.Year + " ) TOTAL, " +
                " 'CHECK IN' STATUS ,ISNULL(COUNT(*),0) CNT FROM  dbo.VISITOR_DETAILS_MSTR  VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID " +
                " WHERE DAY(TRN_DATE) = " + DateTime.Now.Day + " AND MONTH(TRN_DATE) = " + DateTime.Now.Month + " AND YEAR(TRN_DATE) = " + DateTime.Now.Year + " AND STATUS =1 ) " +
                " ROOTA";
            }
            else
            {
                sqlQry = "SELECT TOTAL,STATUS,CNT,CASE WHEN TOTAL = 0 THEN 'N/A' ELSE CONVERT(VARCHAR,CONVERT(NUMERIC,CONVERT(float,(CNT))/CONVERT(float,TOTAL)*100,2)) END PERC FROM  " +
                " (SELECT (SELECT ISNULL(COUNT(*),0) FROM dbo.VISITOR_DETAILS_MSTR VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID " +
                " WHERE EM.BRANCH_ID = " + Session["sBranchId"].ToString() + " AND DAY(TRN_DATE) = " + DateTime.Now.Day + " AND MONTH(TRN_DATE) = " + DateTime.Now.Month + " AND YEAR(TRN_DATE) = " + DateTime.Now.Year + " ) TOTAL, " +
                " 'CHECK IN' STATUS ,ISNULL(COUNT(*),0) CNT FROM  dbo.VISITOR_DETAILS_MSTR  VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID " +
                " WHERE EM.BRANCH_ID = " + Session["sBranchId"].ToString() + " AND DAY(TRN_DATE) = " + DateTime.Now.Day + " AND MONTH(TRN_DATE) = " + DateTime.Now.Month + " AND YEAR(TRN_DATE) = " + DateTime.Now.Year + " AND STATUS =1 ) " +
                " ROOTA";
            }


            objCmd = new SqlCommand(sqlQry, objHelp.dbCon);
            objDa = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDa.Fill(objDs);

            //
            this.lblTotalOutgoing.Text = objDs.Tables[0].Rows[0][0].ToString();
            //lblPercOutgoing.Text = "%" + objDs.Tables[0].Rows[0][3].ToString();
            lblOnProgressCount.Text = objDs.Tables[0].Rows[0][2].ToString();
            //


            DataRow objRow = objDs.Tables[0].NewRow();
            objRow[0] = objDs.Tables[0].Rows[0][0];
            objRow[1] = " ";
            objRow[2] = Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString());
            objRow[3] = Math.Round(((Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString())) / (Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()))) * 100, 2);
            objDs.Tables[0].Rows.Add(objRow);
            objDs.Tables[0].AcceptChanges();
            //
            return objDs.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    private void CreateDoughnutGraphOngoing()
    {
        try
        {
            //string strCaption = "النسب المئوية للحالة";
            //string strSubCaption = "2011 - 2014";
            string strCaption = "  ";
            string strSubCaption = "  ";
            string xAxis = "Status";
            string yAxis = "Count";

            //strXML will be used to store the entire XML document generated
            string strXML = null;

            //Generate the graph element
            strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='1' 
                          pieSliceDepth='30' formatNumberScale='0'
                          xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' rotateNames='1'
                    >";

            int i = 0;

            foreach (DataRow DR in dt.Rows)
            {
                strXML += "<set name='" + DR[1].ToString() + "' value='" + DR[2].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + DR["STATUS"].ToString() + ", " + DR["PERC"].ToString() + "'); &quot;/>";
                i++;
            }

            //Finally, close <graph> element
            strXML += "</graph>";

            string varReturnXml = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_Doughnut2D.swf", // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph1",                      // Unique chart ID
                GraphWidth, GraphHeight,                   // Width & Height of chart
                false
                );

             varReturnXml = varReturnXml.Replace("<embed","<embed wmode='transparent'");
             ltrlFsblSug.Text = varReturnXml;
        }
        catch (Exception ex)
        {
            ltrlFsblSug.Text = null;
            ltrlFsblSug.DataBind();
        }
    }
    //ONGOING :: END
    
    //COMPLETED :: START
    private void ConfigureColorsCompleted()
    {
        color[0] = "008000";
        color[1] = "EBE2D6";
    }
    
    private DataTable LoadGraphDataCompleted()
    {
        dt = returnTableCompleted(0);
        return dt;
    }
    
    private DataTable returnTableCompleted(int varType)
    {
        try
        {
            varSearchCriteria = "";
            varSearchCriteria = buildQuery();
            string sqlQry = "";


            sqlQry = "SELECT TOTAL,STATUS,CNT,CASE WHEN TOTAL = 0 THEN 'N/A' ELSE CONVERT(VARCHAR,CONVERT(NUMERIC,CONVERT(float,(CNT))/CONVERT(float,TOTAL)*100,2)) END PERC FROM ( " +
            " SELECT STATUS,CNT,(" +
            " SELECT ISNULL(COUNT(*),0) FROM VIEW_SUGGESTIONS VS ";

            if (varSearchCriteria != "")
            {
                sqlQry = sqlQry + " WHERE " + varSearchCriteria;
            }

            sqlQry = sqlQry + " ) TOTAL FROM (SELECT (SELECT EXECUTION FROM dbo.tblValues) STATUS,ISNULL(COUNT(*),0) CNT FROM VIEW_SUGGESTIONS VS WHERE VS.EXEC_FLAG = 1 ";
            if (varSearchCriteria != "")
            {
                sqlQry = sqlQry + " AND " + varSearchCriteria;
            }
            sqlQry = sqlQry + " ) ROOT ) ROOTA ";



            objCmd = new SqlCommand(sqlQry, objHelp.dbCon);
            objDa = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDa.Fill(objDs);


            //this.lblTotalCompleted.Text = objDs.Tables[0].Rows[0][0].ToString();
            //lblPercCompleted.Text = "%" + objDs.Tables[0].Rows[0][3].ToString();
            //lblCompletedCount.Text = objDs.Tables[0].Rows[0][2].ToString();


            DataRow objRow = objDs.Tables[0].NewRow();
            objRow[0] = objDs.Tables[0].Rows[0][0];
            objRow[1] = " ";
            objRow[2] = Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString());
            objRow[3] = Math.Round(((Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString())) / (Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()))) * 100, 2);
            objDs.Tables[0].Rows.Add(objRow);
            objDs.Tables[0].AcceptChanges();
            //
            return objDs.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    private void CreateDoughnutGraphCompleted()
    {
        try
        {
            //string strCaption = "النسب المئوية للحالة";
            //string strSubCaption = "2011 - 2014";
            string strCaption = "  ";
            string strSubCaption = "  ";
            string xAxis = "Status";
            string yAxis = "Count";

            //strXML will be used to store the entire XML document generated
            string strXML = null;

            //Generate the graph element
            strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='1' 
                          pieSliceDepth='30' formatNumberScale='0'
                          xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' rotateNames='1'
                    >";

            int i = 0;

            foreach (DataRow DR in dt.Rows)
            {
                strXML += "<set name='" + DR[1].ToString() + "' value='" + DR[2].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + DR["STATUS"].ToString() + ", " + DR["PERC"].ToString() + "'); &quot;/>";
                i++;
            }

            //Finally, close <graph> element
            strXML += "</graph>";

            string varReturnXml = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_Doughnut2D.swf", // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph1",                      // Unique chart ID
                GraphWidth, GraphHeight,                   // Width & Height of chart
                false
                );
            varReturnXml = varReturnXml.Replace("<embed","<embed wmode='transparent'");

            //ltlNfsblSug.Text = varReturnXml;
        }
        catch (Exception ex)
        {
            //ltlNfsblSug.Text = null;
            //ltlNfsblSug.DataBind();
        }
    }
    //COMPLETED :: END

    //PENDING OVERDUED
    private DataTable LoadGraphDataPendingOverdued()
    {
        dt = returnTablePendingOverdued(0);
        return dt;
    }
    
    private DataTable returnTablePendingOverdued(int varType)
    {
        try
        {
            varSearchCriteria = "";
            varSearchCriteria = buildQuery();
            string sqlQry = "";


            if (Session["sUserType"].ToString() == "1")//System Admin
            {
                sqlQry = "SELECT TOTAL,STATUS,CNT,CASE WHEN TOTAL = 0 THEN 'N/A' ELSE CONVERT(VARCHAR,CONVERT(NUMERIC,CONVERT(float,(CNT))/CONVERT(float,TOTAL)*100,2)) END PERC FROM  " +
                " (SELECT (SELECT ISNULL(COUNT(*),0) FROM dbo.VISITOR_DETAILS_MSTR  VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID) TOTAL,'CHECK IN' STATUS ," +
                " ISNULL(COUNT(*),0) CNT FROM  dbo.VISITOR_DETAILS_MSTR VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID WHERE MONTH(TRN_DATE) = " + DateTime.Now.Month + " )ROOTA";

            }
            else
            { 

            sqlQry = "SELECT TOTAL,STATUS,CNT,CASE WHEN TOTAL = 0 THEN 'N/A' ELSE CONVERT(VARCHAR,CONVERT(NUMERIC,CONVERT(float,(CNT))/CONVERT(float,TOTAL)*100,2)) END PERC FROM  "+
            " (SELECT (SELECT ISNULL(COUNT(*),0) FROM dbo.VISITOR_DETAILS_MSTR  VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID WHERE EM.BRANCH_ID = "+Session["sBranchId"].ToString() + " ) TOTAL,'CHECK IN' STATUS ,"+
            " ISNULL(COUNT(*),0) CNT FROM  dbo.VISITOR_DETAILS_MSTR VDM INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID WHERE EM.BRANCH_ID = "+Session["sBranchId"].ToString() + " AND MONTH(TRN_DATE) = " + DateTime.Now.Month + " )ROOTA";

            }

            objCmd = new SqlCommand(sqlQry, objHelp.dbCon);
            objDa = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDa.Fill(objDs);


            this.lblTotalPendingDelayed.Text = objDs.Tables[0].Rows[0][0].ToString();
            //lblPercPendingDelayed.Text = "%" + objDs.Tables[0].Rows[0][3].ToString();
            lblPendingOverdueCount.Text = objDs.Tables[0].Rows[0][2].ToString();


            DataRow objRow = objDs.Tables[0].NewRow();
            objRow[0] = objDs.Tables[0].Rows[0][0];
            objRow[1] = " ";
            objRow[2] = Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString());
            objRow[3] = Math.Round(((Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString())) / (Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()))) * 100, 2);
            objDs.Tables[0].Rows.Add(objRow);
            objDs.Tables[0].AcceptChanges();
            //
            return objDs.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    private void CreateDoughnutGraphPendingOverdued()
    {
        try
        {
            //string strCaption = "النسب المئوية للحالة";
            //string strSubCaption = "2011 - 2014";
            string strCaption = "  ";
            string strSubCaption = "  ";
            string xAxis = "Status";
            string yAxis = "Count";

            //strXML will be used to store the entire XML document generated
            string strXML = null;

            //Generate the graph element
            strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='1' 
                          pieSliceDepth='30' formatNumberScale='0'
                          xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' rotateNames='1'
                    >";

            int i = 0;

            foreach (DataRow DR in dt.Rows)
            {
                strXML += "<set name='" + DR[1].ToString() + "' value='" + DR[2].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + DR["STATUS"].ToString() + ", " + DR["PERC"].ToString() + "'); &quot;/>";
                i++;
            }

            //Finally, close <graph> element
            strXML += "</graph>";

            string varReturnXml = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_Doughnut2D.swf", // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph1",                      // Unique chart ID
                GraphWidth, GraphHeight,                   // Width & Height of chart
                false
                );

            varReturnXml = varReturnXml.Replace("<embed", "<embed wmode='transparent'");


            ltrlImplSug.Text = varReturnXml;


        }
        catch (Exception ex)
        {
            ltrlImplSug.Text = null;
            ltrlImplSug.DataBind();
        }
    }
    //PENDING OVERDUED

    //ONGOING OVERDUED
    private DataTable LoadGraphDataOngoingOverdued()
    {
        dt = returnTableOngoingOverdued(0);
        return dt;
    }
    
    private DataTable returnTableOngoingOverdued(int varType)
    {
        try
        {
            varSearchCriteria = "";
            varSearchCriteria = buildQuery();
            string sqlQry = "";
            sqlQry = "SELECT TOTAL,STATUS,CNT,CASE WHEN TOTAL = 0 THEN 'N/A' ELSE CONVERT(VARCHAR,CONVERT(NUMERIC,CONVERT(float,(CNT))/CONVERT(float,TOTAL)*100,2)) END PERC FROM ( " +
            " SELECT STATUS,CNT,(" +
            " SELECT ISNULL(COUNT(*),0) FROM VIEW_PROJECTS VP WHERE (VOT_COUNT =0 OR VOT_COUNT IS NULL OR VOT_COUNT = 7) AND DEPARTMENTS_INVOLVED=" + Session["DepartmentId"].ToString();

            if (varSearchCriteria != "")
            {
                sqlQry = sqlQry + " WHERE " + varSearchCriteria;
            }
            sqlQry = sqlQry + " ) TOTAL FROM (SELECT (SELECT PEDNING FROM dbo.tblValues) STATUS,ISNULL(COUNT(*),0) CNT FROM VIEW_PROJECTS TT WHERE TASK_STATUS_ID =2 AND " +
            " (EXT_DAYS >0) AND (VOT_COUNT =0 OR VOT_COUNT IS NULL OR VOT_COUNT = 7) AND DEPARTMENTS_INVOLVED=" + Session["DepartmentId"].ToString();

            if (varSearchCriteria != "")
            {
                sqlQry = sqlQry + " AND " + varSearchCriteria;
            }
            sqlQry = sqlQry + " ) ROOT ) ROOTA ";

            objCmd = new SqlCommand(sqlQry, objHelp.dbCon);
            objDa = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDa.Fill(objDs);

            //this.lblTotalOngoingDelayed.Text = objDs.Tables[0].Rows[0][0].ToString();
            //lblPercOngoingDelayed.Text = "%" + objDs.Tables[0].Rows[0][3].ToString();
            //lblOnProgressOverDue.Text = objDs.Tables[0].Rows[0][2].ToString();


            DataRow objRow = objDs.Tables[0].NewRow();
            objRow[0] = objDs.Tables[0].Rows[0][0];
            objRow[1] = " ";
            objRow[2] = Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString());
            objRow[3] = Math.Round(((Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString())) / (Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()))) * 100, 2);
            objDs.Tables[0].Rows.Add(objRow);
            objDs.Tables[0].AcceptChanges();
            //
            return objDs.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    private void CreateDoughnutGraphOngoingOverdued()
    {
        try
        {
            //string strCaption = "النسب المئوية للحالة";
            //string strSubCaption = "2011 - 2014";
            string strCaption = "  ";
            string strSubCaption = "  ";
            string xAxis = "Status";
            string yAxis = "Count";

            //strXML will be used to store the entire XML document generated
            string strXML = null;

            //Generate the graph element
            strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='1' 
                          pieSliceDepth='30' formatNumberScale='0'
                          xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' rotateNames='1'
                    >";

            int i = 0;

            foreach (DataRow DR in dt.Rows)
            {
                strXML += "<set name='" + DR[1].ToString() + "' value='" + DR[2].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + DR["STATUS"].ToString() + ", " + DR["PERC"].ToString() + "'); &quot;/>";
                i++;
            }

            //Finally, close <graph> element
            strXML += "</graph>";

            //fcOngoingDelayed.Text = FusionCharts.RenderChartHTML(
            //    "FusionCharts/FCF_Doughnut2D.swf", // Path to chart's SWF
            //    "",                              // Leave blank when using Data String method
            //    strXML,                          // xmlStr contains the chart data
            //    "mygraph1",                      // Unique chart ID
            //    GraphWidth, GraphHeight,                   // Width & Height of chart
            //    false
            //    );
        }
        catch (Exception ex)
        {
            //fcOngoingDelayed.Text = FusionCharts.RenderChartHTML(
            //    "FusionCharts/FCF_Doughnut2D.swf", // Path to chart's SWF
            //    "",                              // Leave blank when using Data String method
            //    strXML,                          // xmlStr contains the chart data
            //    "mygraph1",                      // Unique chart ID
            //    GraphWidth, GraphHeight,                   // Width & Height of chart
            //    false
        }
    }
    //ONGOING OVERDUED

    //COMPLETED OVERDUED
    private DataTable LoadGraphDataCompletedOverdued()
    {
        dt = returnTableCompletedOverdued(0);
        return dt;
    }
    
    private DataTable returnTableCompletedOverdued(int varType)
    {
        try
        {
            string sqlQry = "";
            if (varSearchCriteria != "")
            {
                sqlQry = sqlQry + " AND " + varSearchCriteria;
            }

            varSearchCriteria = "";
            varSearchCriteria = buildQuery();
            sqlQry = "SELECT TOTAL,STATUS,CNT,CASE WHEN TOTAL = 0 THEN 'N/A' ELSE CONVERT(VARCHAR,CONVERT(NUMERIC,CONVERT(float,(CNT))/CONVERT(float,TOTAL)*100,2)) END PERC FROM ( " +
            " SELECT STATUS,CNT,(" +
            " SELECT ISNULL(COUNT(*),0) FROM VIEW_PROJECTS VP WHERE (VOT_COUNT =0 OR VOT_COUNT IS NULL OR VOT_COUNT = 7) AND DEPARTMENTS_INVOLVED=" + Session["DepartmentId"].ToString();

            if (varSearchCriteria != "")
            {
                sqlQry = sqlQry + " AND " + varSearchCriteria;
            }
            sqlQry = sqlQry + " ) TOTAL FROM (SELECT (SELECT PEDNING FROM dbo.tblValues) STATUS,ISNULL(COUNT(*),0) CNT FROM VIEW_PROJECTS TT WHERE TASK_STATUS_ID =3 AND " +
            " (EXT_DAYS >0) AND (VOT_COUNT =0 OR VOT_COUNT IS NULL OR VOT_COUNT = 7) AND DEPARTMENTS_INVOLVED=" + Session["DepartmentId"].ToString();

            if (varSearchCriteria != "")
            {
                sqlQry = sqlQry + " AND " + varSearchCriteria;
            }
            sqlQry = sqlQry + " ) ROOT ) ROOTA ";

            objCmd = new SqlCommand(sqlQry, objHelp.dbCon);
            objDa = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDa.Fill(objDs);

            //this.lnkTotalPendingSuggestionsCount.Text = objDs.Tables[0].Rows[0][0].ToString();
            //lblFeedbackRequestsCount.Text = "%" + objDs.Tables[0].Rows[0][3].ToString();
            //lblCompletedOverdue.Text = objDs.Tables[0].Rows[0][2].ToString();


            DataRow objRow = objDs.Tables[0].NewRow();
            objRow[0] = objDs.Tables[0].Rows[0][0];
            objRow[1] = " ";
            objRow[2] = Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString());
            objRow[3] = Math.Round(((Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()) - Convert.ToDecimal(objDs.Tables[0].Rows[0][2].ToString())) / (Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString()))) * 100, 2);
            objDs.Tables[0].Rows.Add(objRow);
            objDs.Tables[0].AcceptChanges();
            //
            return objDs.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    private void CreateDoughnutGraphCompletedOverdued()
    {
        try
        {
            //string strCaption = "النسب المئوية للحالة";
            //string strSubCaption = "2011 - 2014";
            string strCaption = "  ";
            string strSubCaption = "  ";
            string xAxis = "Status";
            string yAxis = "Count";

            //strXML will be used to store the entire XML document generated
            string strXML = null;

            //Generate the graph element
            strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='1' 
                          pieSliceDepth='30' formatNumberScale='0'
                          xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' rotateNames='1'
                    >";

            int i = 0;

            foreach (DataRow DR in dt.Rows)
            {
                strXML += "<set name='" + DR[1].ToString() + "' value='" + DR[2].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + DR["STATUS"].ToString() + ", " + DR["PERC"].ToString() + "'); &quot;/>";
                i++;
            }

            //Finally, close <graph> element
            strXML += "</graph>";

            //fcCd.Text = FusionCharts.RenderChartHTML(
            //    "FusionCharts/FCF_Doughnut2D.swf", // Path to chart's SWF
            //    "",                              // Leave blank when using Data String method
            //    strXML,                          // xmlStr contains the chart data
            //    "mygraph1",                      // Unique chart ID
            //    GraphWidth, GraphHeight,                   // Width & Height of chart
            //    false
            //    );
        }
        catch (Exception ex)
        {

        }
    }
    //COMPLETED OVERDUED

    //TASK ANALYSIS
    
    //NOOTIFICATIONS :: START
    private void loadNotifications()
    {
        try
        {
            //sqlQry = "SELECT ROOTA.DEPARTMENT_ID,DEPARTMENT_NAME_AR,CNT FROM (SELECT DEPARTMENT_ID,ISNULL(COUNT(*),0) CNT FROM dbo.VISITOR_DETAILS_MSTR GROUP BY DEPARTMENT_ID "+
            //" ) ROOTA INNER JOIN DEPARTMENT_MSTR DM ON ROOTA.DEPARTMENT_ID =  DM.DEPARTMENT_ID";
            if (Session["sUserType"].ToString() == "1")//System Admin
            {
                sqlQry = "SELECT ROOTA.DEPARTMENT_ID,DEPARTMENT_NAME_AR,CNT FROM (SELECT DEPARTMENT_ID,ISNULL(COUNT(*),0) CNT FROM dbo.VISITOR_DETAILS_MSTR VDM GROUP BY VDM.DEPARTMENT_ID " +
                " ) ROOTA INNER JOIN DEPARTMENT_MSTR DM ON ROOTA.DEPARTMENT_ID =  DM.DEPARTMENT_ID ";
            }
            else
            {
                sqlQry = "SELECT ROOTA.DEPARTMENT_ID,DEPARTMENT_NAME_AR,CNT FROM (SELECT DEPARTMENT_ID,ISNULL(COUNT(*),0) CNT FROM dbo.VISITOR_DETAILS_MSTR VDM " +
                " INNER JOIN ACL_EMPLOYEE_MSTR EM ON VDM.EMPLOYEE_ID = EM.EMPLOYEE_ID GROUP BY DEPARTMENT_ID,BRANCH_ID HAVING EM.BRANCH_ID = " + Session["sBranchId"].ToString() +
                " ) ROOTA INNER JOIN DEPARTMENT_MSTR DM ON ROOTA.DEPARTMENT_ID =  DM.DEPARTMENT_ID ";
            }
            objCmd = new SqlCommand(sqlQry, objHelp.dbCon);

            objDa = new SqlDataAdapter(objCmd);
            objDs = new DataSet();
            objDa.Fill(objDs);

            gvPmAnalysis.DataSource = objDs;
            gvPmAnalysis.DataBind();
            //lnkTotalPendingSuggestionsCount.Text = objDs.Tables[0].Rows[0][0].ToString();
            //lblReassessmentCount.Text = objDs.Tables[1].Rows[0][0].ToString();
            //lblProjectVerificationCounts.Text = objDs.Tables[2].Rows[0][0].ToString();
            //lnkFeedbacksCount.Text = objDs.Tables[3].Rows[0][0].ToString();
            //lnkRejectedProjectsCount.Text = objDs.Tables[4].Rows[0][0].ToString();
            //lnkNotVerifiedProjects.Text = objDs.Tables[5].Rows[0][0].ToString();
        }
        catch (Exception ex)
        {

        }
    }
    //NOTIFICATIONS :: END
    private string buildQuery()
    {
        //string[] fromDate = this.txtFromDate.Text.Trim().Split('/');
        //string[] toDate = this.txtToDate.Text.Trim().Split('/');

        //if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "VS.SUGGESTION_DATE BETWEEN '" + fromDate[2] + "/" + fromDate[1] + "/" + fromDate[0] + "' AND  DATEADD(day,0,'" + toDate[2] + "/" + toDate[1] + "/" + toDate[0].ToString() + "')";
        //}
        //else if (txtFromDate.Text.Trim() != "")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "convert(varchar(10),VS.SUGGESTION_DATE,103) ='" + this.txtFromDate.Text.Trim() + "'";
        //}
        //else if (txtToDate.Text.Trim() != "")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "convert(varchar(10),VS.SUGGESTION_DATE,103) ='" + this.txtToDate.Text.Trim() + "'";
        //}
        //if (cmbMonth.SelectedItem.Value != "0")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "MONTH(VS.SUGGESTION_DATE) =" + cmbMonth.SelectedItem.Value;
        //}
        //if (cmbYear.SelectedItem.Value != "0")
        //{
        //    varSearchCriteria = varSearchCriteria + " AND " + "YEAR(VS.SUGGESTION_DATE) =" + cmbYear.SelectedItem.Value;
        //}
        if (varSearchCriteria != "")
        {
            varSearchCriteria = varSearchCriteria.Remove(0, 4);
        }

        return varSearchCriteria;
    }

    public void yearFill(int varFromYear, int varToYear, DropDownList cmbBox)
    {
        for (varFromYear = varFromYear; varFromYear <= varToYear; varFromYear++)
        {
            cmbBox.Items.Add(varFromYear.ToString());
        }

        if (Session["MyCulture"].ToString() == "ar-SA")
        {
            cmbBox.Items.Insert(0, new ListItem("--السنة--", "0"));
        }
        else
        {
            cmbBox.Items.Insert(0, new ListItem("--Year--", "0"));
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["sEmployeeId"] == null)
        //{
        //    Response.Redirect("~/frmLogout.aspx");
        //}

        //if (!IsPostBack)
        //{
        //    //yearFill(Convert.ToInt32("2010"), DateTime.Now.Year, cmbYear);
        //    //generateDashBoard();


        //    objHelp.openConnection();

        //    ConfigureColorsPending();
        //    LoadGraphDataPending();
        //    CreateDoughnutGraphPending();

        //    LoadGraphDataPendingOverdued();
        //    CreateDoughnutGraphPendingOverdued();


        //    ConfigureColorsOngoing();
        //    LoadGraphDataOngoing();
        //    CreateDoughnutGraphOngoing();


        //    loadNotifications();

        //    objHelp.dbCon.Close();
        //    //
        //}
    }
    
    protected void btnSet_Click(object sender, EventArgs e)
    {
        objHelp.openConnection();
        ConfigureColorsPending();
        LoadGraphDataPending();
        CreateDoughnutGraphPending();
        LoadGraphDataPendingOverdued();
        CreateDoughnutGraphPendingOverdued();

        ConfigureColorsOngoing();
        LoadGraphDataOngoing();
        CreateDoughnutGraphOngoing();
        LoadGraphDataOngoingOverdued();
        CreateDoughnutGraphOngoingOverdued();

        ConfigureColorsCompleted();
        LoadGraphDataCompleted();
        CreateDoughnutGraphCompleted();
        LoadGraphDataCompletedOverdued();
        CreateDoughnutGraphCompletedOverdued();


        //LoadGraphDataTaskAnalysis();
        objHelp.dbCon.Close();
    }
    
    protected void lblPendingCount_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmViewPendingSuggestions.aspx");
    }
    
    protected void lblOnProgressCount_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTaskDetailedReport.aspx?qsStatusId=2");
    }
    
    protected void lblCompletedCount_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTaskDetailedReport.aspx?qsStatusId=1");
    }
    
    protected void lblPendingOverdueCount_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTaskDetailedReport.aspx?qsStatusId=4");
    }
    
    protected void lblOnProgressOverDue_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTaskDetailedReport.aspx?qsStatusId=5");
    }
    
    protected void lblCompletedOverdue_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTaskDetailedReport.aspx?qsStatusId=6");
    }
    
    protected void imgBtnViewProjects_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/EXPERTS/frmExpertsEvaluation.aspx");
    }
    
    protected void imgBtnTasks_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmViewVisitors.aspx");
    }
    
    protected void  gvApprovedRejected_SelectedIndexChanged(object sender, EventArgs e)
{

}
    
    protected void lnkFeedbacksCount_Click(object sender, EventArgs e)
    {
    Response.Redirect("frmViewFeedback.aspx");
    }
    
    protected void lnkTotalPendingSuggestionsCount_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmViewPendingSuggestions.aspx");
    }
    
    protected void lblReassessmentCount_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmProcessDeptVerfnSuggestions.aspx");
    }
    protected void lblProjectVerificationCounts_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmViewProjectsFinalization.aspx");
    }

    protected void imgBtnRejectedProjects_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/EXPERTS/frmViewRejectedProjects.aspx");
    }
    protected void imgBtnNotVerifiedProjects_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/EXPERTS/frmViewNotVerifiedProjects.aspx");
    }
    protected void lnkRejectedProjectsCount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/EXPERTS/frmViewRejectedProjects.aspx");
    }
    protected void lnkNotVerifiedProjects_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/EXPERTS/frmViewNotVerifiedProjects.aspx");
    }
    protected void imgBtnAddVisitor_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ParseData.aspx");
    }
    protected void imgBtnHome_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("frmLogout.aspx");
    }
    protected void gvPmAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }
}