using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;

public partial class WYDW_ApplyLPB_Lz_edit : System.Web.UI.Page
{
    pageTool tool = null;
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        tool = new pageTool(this.Page);


        if (!IsPostBack)
        {
            
        }
        else 
        {
            string hidoperation = Convert.ToString(Request["hidOperation"]);
            if (hidoperation == "excel")
            {
                DrExcel();
            }
            else
            {
                SetHouseShow();
            }
            
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {

    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }

    private void ShowInfo()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        // 创建 DataTable 中的 DataColumn 列
        dt.Columns.Add(new DataColumn("ZH", typeof(System.String)));
        dt.Columns.Add(new DataColumn("FH", typeof(System.String)));
        dt.Columns.Add(new DataColumn("DY", typeof(System.String)));
        dt.Columns.Add(new DataColumn("RHC", typeof(System.String)));
        dt.Columns.Add(new DataColumn("SH", typeof(System.String)));
        dt.Columns.Add(new DataColumn("JZMJ", typeof(System.String)));

        // 填充数据到 DataTable 中
        for (int i = 1; i < 29; i++)
        {
            dr = dt.NewRow();

            dr[0] = i;
            dr[1] = i.ToString();
            dr[2] = 0;
            dr[3] = 0;
            dr[4] = "";
            dr[5] = "1";
            dt.Rows.Add(dr);
        }

        // 数据绑定代码
        DG_List.DataSource = dt;
        DG_List.DataBind();
    }
    protected void BtHZ_Click(object sender, EventArgs e)
    {
        DrExcel();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(hidZTS.Value) <= 0)
        {
            tool.showMessage("没有房屋信息,请核查!");
            return;
        }
        DataTable dt_House = (DataTable)Session["m_Table_InsertHouse"];
        if (dt_House == null)
        {
            tool.showMessage("没有房屋信息,请核查!");
            return;
        }
        else if (dt_House.Rows.Count <= 0)
        {
            tool.showMessage("没有房屋信息,请核查!");
            return;
        }
        //检查楼幢是否已存在，已存在不可二次保存
        string sql = "select * from YW_WY_XM_LZXX where FAppID='" + (string)Session["FAppId"] + "' and BuildName='" + hidBuildName.Value.ToString().Trim() + "'";
        DataTable dt = rc.GetTable(sql);


        if (dt != null && dt.Rows.Count > 0)
        {
            tool.showMessage("该楼幢已经存在不可再次保存,请核查!");
            return;
        }

        string buildid=Guid.NewGuid().ToString();

        //保存楼幢信息
        string strsql_insertLZ=" insert into YW_WY_XM_LZXX (FAppID,BuildId,BuildName,ZTS,ZJZMJ) VALUES('"+(string)Session["FAppId"]+"','";
        strsql_insertLZ += buildid + "','" + hidBuildName.Value.ToString().Trim() + "','" + hidZTS.Value.ToString() + "','" + hidZJZMJ.Value.ToString() + "')";

        if (rc.PExcute(strsql_insertLZ))
        {
            //保存房屋信息
            for (int i = 0; i < dt_House.Rows.Count; i++)
            {
                string strsql_insertHouse = " insert into YW_WY_XM_FWXX (FAppID,BuildId,HouseId,ZH,FH,DY,RHC,SH,JZMJ) VALUES(";
                strsql_insertHouse += "'" + (string)Session["FAppId"] + "',";
                strsql_insertHouse += "'" + buildid + "',";
                strsql_insertHouse += "'" + Guid.NewGuid().ToString() + "',";
                strsql_insertHouse += "'" + hidBuildName.Value.ToString().Trim() + "',";
                strsql_insertHouse += "'" + dt_House.Rows[i]["房屋编号"] + "',";
                strsql_insertHouse += "'" + dt_House.Rows[i]["单元"] + "',";
                strsql_insertHouse += "'" + dt_House.Rows[i]["入户层"] + "',";
                strsql_insertHouse += "'" + dt_House.Rows[i]["室号"] + "',";
                strsql_insertHouse += "'" + dt_House.Rows[i]["建筑面积"] + "')";
                if (!rc.PExcute(strsql_insertHouse))
                {
                    tool.showMessage("保存房屋信息失败!");
                }
            }

        }
        else
        {
            tool.showMessage("保存楼幢信息失败!");
        }

        tool.showMessage("保存成功!");

    }

    private void DrExcel()
    {
        hidOperation.Value = "";
        if (fileLoad.HasFile)
        {
            string SavePath = "";
            //若不存在楼盘文件夹，添加该文件夹
            string fileExt = System.IO.Path.GetExtension(fileLoad.FileName);
            if (fileExt == ".xls")
            {
                //if (!System.IO.Directory.Exists(HttpRuntime.AppDomainAppPath + "LPB"))
                //{
                //    System.IO.Directory.CreateDirectory(HttpRuntime.AppDomainAppPath + "LPB");
                //}
                //SavePath = "LPB\\new.xlsx";

                string path = Server.MapPath("") + "\\new" + Guid.NewGuid().ToString() + ".xls";
                //Function.ShowMessageBox(path, Page);
                fileLoad.SaveAs(path);
                ExcelToDataTable(path);
            }
            else if (fileExt == ".xlsx")
            {
                string path = Server.MapPath("") + "\\new" + Guid.NewGuid().ToString() + ".xlsx"; 
                //Function.ShowMessageBox(path, Page);
                fileLoad.SaveAs(path);
                ExcelToDataTable(path);
            }
            else
            {
                tool.showMessage("请上传EXCEL文件！");
            }
        }
    }

    private DataTable ExcelToDataTable(string strPath)
    {

        DataTable dt = new DataTable();

        string strSheetName = "sheet1"; //默认sheet1 
        DataSet m_ds = new DataSet();
        //string strConn = @"Provider = Microsoft.Jet.OLEDB.4.0;Data Source = " +  strPath + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'";
        string strConn = @"Provider = Microsoft.ACE.OLEDB.12.0;Data Source = " + strPath + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1;'";
        using (OleDbConnection conn = new OleDbConnection(strConn))
        {
            try
            {
                conn.Open();
                DataTable SDataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                strSheetName = SDataTable.Rows[0][2].ToString().Trim();

                string strExcel = "select * from [" + strSheetName + "]";

                OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
                adapter.Fill(m_ds, strSheetName);
                DataTable dt_ds = m_ds.Tables[0];
                for (int i = 0; i < dt_ds.Columns.Count; i++)
                {
                    dt_ds.Columns[i].ColumnName = Convert.ToString(dt_ds.Rows[0][i]);
                }
                dt_ds.Rows.RemoveAt(0);
                if (!CheckExcelFile(m_ds))
                {
                    tool.showMessage("EXCEL表数据结构不正确!");
                    return dt;
                }


            }
            catch (Exception ex)
            {

                string str = "EXCEL表数据错误，请联系系统管理员";

                tool.showMessage(str);
                return dt;
            }
            finally
            {
                conn.Close();
            }
        }

        CreateLB(m_ds.Tables[0].Copy());


        return dt;
    }

    private string CreateLB(DataTable dt_House)
    {
        Session["m_Table_InsertHouse"] = dt_House;
        StringBuilder sb = new StringBuilder();
        SetHouseShow();
        return sb.ToString();
    }

    private void SetHouseShow()
    {
        DataTable dt_House = new DataTable();
        try
        {
            dt_House = (DataTable)Session["m_Table_InsertHouse"];
            if (dt_House == null)
            {
                return;
            }

        }
        catch
        {
            return;
        }
        

        DataTable dt = new DataTable();
        DataRow dr;

        // 创建 DataTable 中的 DataColumn 列
        dt.Columns.Add(new DataColumn("ZH", typeof(System.String)));
        dt.Columns.Add(new DataColumn("FH", typeof(System.String)));
        dt.Columns.Add(new DataColumn("DY", typeof(System.String)));
        dt.Columns.Add(new DataColumn("RHC", typeof(System.String)));
        dt.Columns.Add(new DataColumn("SH", typeof(System.String)));
        dt.Columns.Add(new DataColumn("JZMJ", typeof(System.String)));

        double sum_jzmj = 0;

        // 填充数据到 DataTable 中
        for (int i = 0; i < dt_House.Rows.Count; i++)
        {
            dr = dt.NewRow();

            dr["ZH"] = hidBuildName.Value;
            dr["FH"] = dt_House.Rows[i]["房屋编号"];
            dr["DY"] = dt_House.Rows[i]["单元"];
            dr["RHC"] = dt_House.Rows[i]["入户层"];
            dr["SH"] = dt_House.Rows[i]["室号"];
            dr["JZMJ"] = dt_House.Rows[i]["建筑面积"];
            sum_jzmj += Convert.ToDouble(dt_House.Rows[i]["建筑面积"]);
            dt.Rows.Add(dr);
        }

        // 数据绑定代码
        DG_List.DataSource = dt;
        DG_List.DataBind();


        txtCount.Text = "共有 ：" + dt_House.Rows.Count + " 户，总面积为 : " + sum_jzmj.ToString("0.00") + " 平方米";

        hidZJZMJ.Value = sum_jzmj.ToString();
        hidZTS.Value = dt_House.Rows.Count.ToString();
        hidBuildName.Value =Convert.ToString(dt_House.Rows[0]["幢号"]);
    }

    /// <summary>
    /// 对要导入的EXCEL文件进行错误判断
    /// </summary>
    /// <param name="ds"></param>
    private bool CheckExcelFile(DataSet ds)
    {
        //List<string> houseNumber_List = DAL.SqlGetClass.GetAllHouseNumberByBuildAN(buildAN);

        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count < 1)
        {
            tool.showMessage("没有房屋信息!");
            return false;
        }

        string strLJZH = Convert.ToString(dt.Rows[0]["幢号"]);
        hidBuildName.Value = strLJZH;

        //房屋编号必须有，如果重复则警告
        List<string> list_houseName = new List<string>();
        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {
            string houseName = Convert.ToString(dt.Rows[i]["房屋编号"]).Trim();
            string floorNo = Convert.ToString(dt.Rows[i]["入户层"]).Trim();
            if (houseName == "" && floorNo == "")
            {                
                dt.Rows.RemoveAt(i);
                continue;
            }
            if (houseName == "")
            {
                break;
            }

            string strLJZH_I = Convert.ToString(dt.Rows[i]["幢号"]);
            if (strLJZH != strLJZH_I)
            {

                tool.showMessage("不能将多个幢号房屋放入同一个Excel中导入，请核查!");
                return false;
            }
            
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            #region 单元如果填写了则必须为数字

            string unitNo = Convert.ToString(dt.Rows[i]["单元"]).Trim();
            if (unitNo != "")
            {
                try
                {
                    int intUnitNo = Convert.ToInt32(unitNo);
                }
                catch
                {
                    tool.showMessage((i + 2) + "行单元必须为整数!");
                    return false;
                }
            }

            #endregion

            #region 实际层必须填写且必须为数字

            string floorNo = Convert.ToString(dt.Rows[i]["入户层"]).Trim();
            if (floorNo == "")
            {
                tool.showMessage((i + 2) + "行楼层未填写!");
                return false;
            }
            else
            {
                try
                {
                    int intfloorNo = Convert.ToInt32(floorNo);
                }
                catch
                {
                    tool.showMessage((i + 2) + "行楼层不为整数!");
                    return false;
                }
            }

            #endregion

            #region 实际室号必须填写且必须为数字

            string roomNo = Convert.ToString(dt.Rows[i]["室号"]).Trim();
            if (roomNo == "")
            {
                tool.showMessage((i + 2) + "行室号未填写!");
                return false;
            }
            else
            {
                try
                {
                    int intRoomNo = Convert.ToInt32(roomNo);
                }
                catch
                {
                    tool.showMessage((i + 2) + "行室号不为整数!");
                    return false;
                }
            }

            #endregion


            #region 房屋编号必须有，如果重复且单元，层，室号不一致则警告

            string houseName = Convert.ToString(dt.Rows[i]["房屋编号"]).Trim();
            if (houseName == "")
            {
                //double i_han = i + 2;
                tool.showMessage((i + 2) + "行房屋编号未填写!");
                return false;
            }
            else
            {
                //if (houseNumber_List.Contains(houseName))
                //{
                //    Function.ShowMessageBox((i + 2) + "行房屋编号已经存在系统中，不需追加!", this.Page);
                //    return false;
                //}
                if (list_houseName.Contains(houseName))
                {
                    tool.showMessage((i + 2) + "行房屋编号发生重复!");
                    return false;

                }
                else
                {
                    list_houseName.Add(houseName);
                }
            }

            #endregion

            


            #region 面积如果填写则必须为数字

            string strjzmj = Convert.ToString(dt.Rows[i]["建筑面积"]).Trim();

            if (strjzmj == "")
            {
                dt.Rows[i]["建筑面积"] = 0;
            }
            strjzmj = Convert.ToString(dt.Rows[i]["建筑面积"]).Trim();
            try
            {
                double dTNMJ = Convert.ToDouble(strjzmj);
            }
            catch
            {
                tool.showMessage((i + 2) + "行建筑面积不是数字!");
                return false;
            }


            #endregion

        }

        return true;
    }
}