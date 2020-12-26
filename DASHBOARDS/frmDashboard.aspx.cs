using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class frmDashboard : basePage
{
    #region FILTERS
    clsBllDashboard objBll = new clsBllDashboard();
    string sqlFilter = "";
    DataSet objDs;
    DataTable objTblChart;
    DataRow objRowChart;
    #endregion

    private void drawChartCheckInCount()
    {
       
    }

    private void drawChartCheckOutCount()
    {
      
    }

    private void drawChartSectorWise()
    {
       

    }

    private void drawChartDepartmentWise()
    {
       

    }
    private void loadAnalysis()
    {
        try
        {
         
            objDs = objBll.loadAnalysis();

            if (objDs.Tables.Count > 0)
            {

                //TOTAL : objDs.Tables[0].Rows[0][0].ToString()
                
                this.lblCheckOutCount.Text = objDs.Tables[0].Rows[0][0].ToString();
                this.lblPendingCount.Text = "100";//    objDs.Tables[0].Rows[0][0].ToString();//PECENTAGE

                this.lblPreRegCount.Text = Convert.ToString(Math.Round(((Convert.ToDecimal(objDs.Tables[1].Rows[0][0].ToString()) / Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString())) * 100)));
                this.lblFitCount.Text = objDs.Tables[1].Rows[0][0].ToString();

                this.lblUnfitCount.Text = Convert.ToString(Math.Round(((Convert.ToDecimal(objDs.Tables[2].Rows[0][0].ToString()) / Convert.ToDecimal(objDs.Tables[0].Rows[0][0].ToString())) * 100)));    //objDs.Tables[2].Rows[0][0].ToString();//PERCENATE
                this.lblCheckInCount.Text = objDs.Tables[2].Rows[0][0].ToString();//
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
        if (!IsPostBack)
        {
            //this.txtFromDate.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            loadAnalysis();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadAnalysis();
    }

    protected void dgSectorWise_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells[11].Text.Trim() == "1")
            {
                e.Item.Cells[11].Text = "الذكر";
            }
            else if (e.Item.Cells[11].Text.Trim() == "0")
            {
                e.Item.Cells[11].Text = "أنثى";
            }
        }
    }

    protected void dgSectorWise_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "INFO")
        {
            Response.Redirect("~/VISITORS_MANAGEMENT/frmTestDetails.aspx?qsVisitId="+e.Item.Cells[0].Text);
        }
    }

    protected void chrtPreRegistered_DataBound(object sender, EventArgs e)
    {
        //chrtPreRegistered.Series[0].Color = Color.Red; //or any other color, maybe from PieColor
        //chrtPreRegistered.Series[1].Color = Color.Red; //or any other color, maybe from PieColor
    }
}
