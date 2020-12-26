using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmiratesId.AE.PublicData;
using System.Data;
using System.Data.SqlClient;

public partial class frmLot : System.Web.UI.Page
{

    
    clsSqlHelp objHelp = new clsSqlHelp();
    SqlCommand objCmd;
    int i = 0;
    private void randomSelection()
    {
        DataTable dt = new DataTable("YourDataTable");
        objHelp.openConnection();
        string sqlQry = "SELECT VISITOR_ID, NAME_AR, IMAGE_BYTE, IDN FROM     VISITOR_DETAILS_MSTR WHERE AWARDED IS NULL AND EVENT_ID IS NOT NULL ";
        SqlDataAdapter objDa = new SqlDataAdapter(sqlQry, objHelp.dbCon);
        objDa.Fill(dt);
        dt.AcceptChanges();
        objHelp.dbCon.Close();
        if (dt.Rows.Count > 0)
        {
            DataTable dtRandomRows = new DataTable("RandomTable");
            dtRandomRows.Columns.Add("VISITOR_ID", typeof(Int32));
            dtRandomRows.Columns.Add("NAME_AR", typeof(string));
            dtRandomRows.Columns.Add("IMAGE_BYTE", typeof(string));
            dtRandomRows.Columns.Add("IDN", typeof(string));

            DataRow objRow = dtRandomRows.NewRow();
            //dtRandomRows = dt.Clone();

            Random rDom = new Random();
            int i = 0;
            for (int ctr = 0; ctr < dt.Rows.Count; ctr++)
            {
                if (dt.Rows.Count > 30)
                {
                    i = rDom.Next(0, dt.Rows.Count - 1);
                }
                else
                { 
                
                }
                dtRandomRows.Rows.Add(dt.Rows[i].ItemArray);
                i = i + 1;
            }
            dtRandomRows.AcceptChanges();
            ViewState["vsRandomTable"] = dtRandomRows;
        }
        else
        {
            Timer1.Enabled = false;
            this.lblMessage.Text = "";
            imgGift.Visible = false;
            imgGift1.Visible = false;
            Image1.Visible = false;
            Button1.BackColor = System.Drawing.Color.Red;
            Button1.Visible = false;
            ViewState["vsActiveId"] = null;
        }
    }


    protected void GetTime(object sender, EventArgs e)
    {
        try
        {
            Timer1.Enabled = false;
            i = Convert.ToInt32(ViewState["vsIndex"].ToString()) + 1;
            ViewState["vsIndex"] = i;

            //lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");

            lblTime.Text = ViewState["vsIndex"].ToString();

            //i = Convert.ToInt32(ViewState["vsIndex"].ToString()) + 1;

            this.txtFullNameAr.Text = ((DataTable)ViewState["vsRandomTable"]).Rows[Convert.ToInt32(ViewState["vsIndex"].ToString())-1][1].ToString();

            if (((DataTable)ViewState["vsRandomTable"]).Rows[Convert.ToInt32(ViewState["vsIndex"].ToString()) - 1][2].ToString() != "")
            {
                PhotoBase64.Src = "data:image/jpeg;base64," + ((DataTable)ViewState["vsRandomTable"]).Rows[Convert.ToInt32(ViewState["vsIndex"].ToString())-1][2].ToString();
            }
            Timer1.Enabled = true;

            if (lblTime.Text == "30" || ((DataTable)ViewState["vsRandomTable"]).Rows.Count-1 == Convert.ToInt32(ViewState["vsIndex"].ToString())-1)//if 30 seconds finished
            {
                Timer1.Enabled = false;
                this.lblMessage.Text = "مبروك";
                imgGift.Visible = true;
                imgGift1.Visible = true;
                Image1.Visible = false;
                this.imgStableCube.Visible = true;
                Button1.BackColor = System.Drawing.Color.Red;
                Button1.Visible = false;
                Button1.BackColor = System.Drawing.Color.Red;
                ViewState["vsActiveId"] = ((DataTable)ViewState["vsRandomTable"]).Rows[Convert.ToInt32(ViewState["vsIndex"].ToString())-1][0].ToString();

            }
        }
        catch (Exception ex)
        { 
        
        }
        //

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            imgGift.Visible = false;
            imgGift1.Visible = false;
            ViewState["vsIndex"] = "0";

            imgStableCube.Visible = true;

            PhotoBase64.Visible = false;

            //randomSelection();

            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");

            //Timer1.Enabled = true;

            Image1.Visible = false;

            Button1.BackColor = System.Drawing.Color.Red;


        }
    }

    string sqlQry = "";
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Button1.BackColor.Name.ToLower() == "red")
        {
            Button1.BackColor = System.Drawing.Color.Green;
            Button1.Visible = false;

            if (ViewState["vsActiveId"] != null)
            {
                try
                {
                    objHelp.openConnection();
                    sqlQry = "UPDATE VISITOR_DETAILS_MSTR SET AWARDED = 1 WHERE VISITOR_ID= " + ViewState["vsActiveId"].ToString();
                    objCmd = new SqlCommand(sqlQry, objHelp.dbCon);
                    objCmd.ExecuteNonQuery();
                    objCmd.Dispose();
                }
                catch (Exception ex)
                {
                    string varMsg = ex.Message;
                }
                finally
                {
                    objHelp.dbCon.Close();
                }
            }
        }
        else
        {
            Button1.BackColor = System.Drawing.Color.Red;
        }

        ViewState["vsActiveId"] = null;

        imgGift.Visible = false;
        imgGift1.Visible = false;
        this.lblMessage.Text = "";
        this.Image1.Visible = true;
        this.imgStableCube.Visible = false;
        PhotoBase64.Visible = true;
        randomSelection();
        ViewState["vsIndex"] = "0";
        if (Timer1.Enabled == false)
        {
            randomSelection();
            Timer1.Enabled = true;
        }
        else
        {
            Timer1.Enabled = false;
        }
    }
    protected void imgStableCube_Click(object sender, ImageClickEventArgs e)
    {
        Button1_Click(null, null);
    }
}