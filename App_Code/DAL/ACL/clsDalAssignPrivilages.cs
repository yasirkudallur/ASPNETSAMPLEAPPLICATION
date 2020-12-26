using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for clsDalRollToService
/// </summary>
public class clsDalAssignPrivilages : clsSqlHelp
{
    //Coded Baiju
    public clsDalAssignPrivilages()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void RollIdFill(DropDownList objDdl)
    {
        objSqlCmd = new SqlCommand();
        objSqlCmd.Parameters.Add("@Flag",SqlDbType.BigInt).Value=1;
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        //comboFill(objDdl, objSqlCmd, "SP_RoleToService");
        comboFill(objDdl, objSqlCmd, "SP_ROLE_SERVICE");
    }
    public void ServiceGridFill(clsBllAssignPrivilages objBllRollToService,DataGrid dg)
    {
        DataTable mTable = new DataTable();

        mTable.Columns.Add("MENU_SERVICE_ID", typeof(int));
        mTable.Columns.Add("MENU_NAME", typeof(string));
        mTable.Columns.Add("ADD_STATUS", typeof(bool));
        mTable.Columns.Add("EDIT_STATUS", typeof(bool));
        mTable.Columns.Add("DELETE_STATUS", typeof(bool));
        mTable.Columns.Add("REPORT_STATUS", typeof(bool));
        mTable.Columns.Add("VIEW_STATUS", typeof(bool));

        openConnection();
        //objSqlCmd = new SqlCommand("SP_RoleToService", dbCon);
        objSqlCmd = new SqlCommand("SP_ROLE_SERVICE", dbCon);
        
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 2;
        objSqlCmd.Parameters.Add("@EmployeeId", SqlDbType.BigInt).Value = objBllRollToService.PVar_EmployeeId;
        objSqlCmd.Parameters.Add("@RoleId", SqlDbType.BigInt).Value = objBllRollToService.rollId;
        objAdapter = new SqlDataAdapter(objSqlCmd);
        objAdapter.Fill(mTable);

        int mRowCnt = mTable.Rows.Count;
        if (objBllRollToService.rollId > 0)
        {
            int cntService = Convert.ToInt32(ExCuteScalar("select count(*) from MENUSERVICE_MASTER"));           
           
            if (mRowCnt < cntService)
            {
                //objSqlCmd = new SqlCommand("SP_RoleToService");
                objSqlCmd = new SqlCommand("SP_ROLE_SERVICE");
                
                objSqlCmd.CommandType = CommandType.StoredProcedure;
                objSqlCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 9;
                objSqlCmd.Parameters.Add("@RoleId", SqlDbType.BigInt).Value = objBllRollToService.rollId;
                objSqlCmd.Parameters.Add("@EmployeeId", SqlDbType.BigInt).Value = objBllRollToService.PVar_EmployeeId;

                DataTable menuTable = fillDataTable(objSqlCmd);
                foreach (DataRow dr in menuTable.Rows)
                    mTable.ImportRow(dr);

                //SqlDataReader mRead = forExecuteReader(objSqlCmd,"SP_RoleToService");
                //    while (mRead.Read())
                //    {
                //        DataRow mRow = mTable.NewRow();
                //        int objServId = Convert.ToInt32(mRead[0]);
                //        mRow[0] = objServId;
                //        string objServName = mRead[1].ToString();
                //        mRow[1] = objServName;
                //        mTable.Rows.Add(mRow);
                //    }
                //    mRead.Close();
            }
        }

        dg.DataSource = mTable;
        dg.DataBind();

    }
    public string chkStatus(clsBllAssignPrivilages objBllRollToService)
    {
        objSqlCmd = new SqlCommand();
        objSqlCmd.Parameters.Add("@Flag", SqlDbType.BigInt).Value = 4;
        objSqlCmd.Parameters.Add("@RoleId", SqlDbType.BigInt).Value = objBllRollToService.rollId;
        objSqlCmd.Parameters.Add("@EmployeeId", SqlDbType.BigInt).Value = objBllRollToService.PVar_EmployeeId;
        objSqlCmd.Parameters.Add("@MenuServiceId", SqlDbType.BigInt).Value = objBllRollToService.menuServiceId;
        //return getSingleCode(objSqlCmd, "SP_RoleToService");   
        return getSingleCode(objSqlCmd, "SP_ROLE_SERVICE");   
        
    }
    public void SaveToRTS(clsBllAssignPrivilages objBllRollToService)
    {
        objSqlCmd = new SqlCommand();
        objSqlCmd.Parameters.Add("@flag",SqlDbType.Int).Value = objBllRollToService.flag;
        objSqlCmd.Parameters.Add("@RoleToServiceId", SqlDbType.BigInt).Value = objBllRollToService.rtsId;
        objSqlCmd.Parameters.Add("@EmployeeId", SqlDbType.BigInt).Value = objBllRollToService.PVar_EmployeeId;
        objSqlCmd.Parameters.Add("@RoleId",SqlDbType.BigInt).Value = objBllRollToService.rollId;
        objSqlCmd.Parameters.Add("@MenuServiceId",SqlDbType.BigInt).Value = objBllRollToService.menuServiceId;
        objSqlCmd.Parameters.Add("@AddStatus",SqlDbType.Bit).Value = objBllRollToService.statAdd;
        objSqlCmd.Parameters.Add("@EditStatus", SqlDbType.Bit).Value = objBllRollToService.statEdit;
        objSqlCmd.Parameters.Add("@DeleteStatus", SqlDbType.Bit).Value = objBllRollToService.statDelete;
        objSqlCmd.Parameters.Add("@ReportStatus", SqlDbType.Bit).Value = objBllRollToService.statReport;
        objSqlCmd.Parameters.Add("@ViewStatus", SqlDbType.Bit).Value = objBllRollToService.statView;
        //executeQuery(objSqlCmd, "SP_RoleToService");
        executeQuery(objSqlCmd, "SP_ROLE_SERVICE");
        
    }

    public SqlDataReader selectFromRTS(clsBllAssignPrivilages objBllRollToService)
    {
        objSqlCmd = new SqlCommand();
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = objBllRollToService.flag;
        objSqlCmd.Parameters.Add("@RoleId", SqlDbType.BigInt).Value = objBllRollToService.rollId;
        //return forExecuteReader(objSqlCmd, "SP_RoleToService");
        return forExecuteReader(objSqlCmd, "SP_ROLE_SERVICE");
        
    }
    //coded Baiju ends
    public void FillComboBox(DropDownList ObjDdl, clsBllAssignPrivilages objBllRollToService)
    {
        //objSqlCmd = new SqlCommand("SP_RoleToService");
        objSqlCmd = new SqlCommand("SP_ROLE_SERVICE");
        
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        objSqlCmd.Parameters.Add("@flag", SqlDbType.Int).Value = 8;
        objSqlCmd.Parameters.Add("@RoleId", SqlDbType.BigInt).Value = objBllRollToService.rollId;
        comboFill(ObjDdl, objSqlCmd);
    }
}
