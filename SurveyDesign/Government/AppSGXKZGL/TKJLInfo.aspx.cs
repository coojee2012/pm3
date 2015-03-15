using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaBLL;
using System.Text;
using Approve.PersistBase;
using System.Data.SqlClient;

public partial class JSDW_APPSGXKZGL_YZInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    string n = "Byte";
    private IConnection Cn_e;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            h_FAppId.Value = EConvert.ToString(Request["FAppId"]);
            t_FId.Value = EConvert.ToString(Request["FId"]);
            t_FEntId.Value = EConvert.ToString(Request["FEntId"]);
            t_FPrjId.Value = EConvert.ToString(Request["FPrjId"]);
            t_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);
            showInfo();
        }
    }
    void BindControl()
    {

       
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_Prj_XCTKJL emp = dbContext.TC_Prj_XCTKJL.Where(t => t.FId == t_FId.Value).FirstOrDefault();
        if (emp != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(emp);
        }
        string sql = "SELECT * FROM TC_Prj_XCTKJL_File WHERE FLinkId='" + t_FId.Value + "'";
        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            DataSet ds = new DataSet();
            conn.Open();       
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(ds, "ds");
            DataTable dt = ds.Tables[0];
            string filepaths = "";
            for (int i = 0; i < dt.Rows.Count; i++) {
                //if (i > 0)
                //    filepaths += "|"+dt.Rows[i]["FFilePath"].ToString();
               // else
                    filepaths += dt.Rows[i]["FFilePath"].ToString()+"|";
            }
            t_FilePath.Value = filepaths;

        }
    }
    //保存
    private void saveInfo()
    {
        string fId = t_FId.Value;
        TC_Prj_XCTKJL Emp = new TC_Prj_XCTKJL();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_Prj_XCTKJL.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = h_FAppId.Value;
            t_FId.Value = fId;
            dbContext.TC_Prj_XCTKJL.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        t_FId.Value = fId;

        addFj();
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
    //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }

    private void addFj() { 
           
        string fId = Guid.NewGuid().ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM TC_Prj_XCTKJL_File WHERE FlinkId='" + t_FId.Value + "';");
       
        string[] filePaths = t_FilePath.Value.Split('|');
        if (filePaths.Count() > 0 && !string.IsNullOrEmpty(filePaths[0]))
        {
            sb.Append("INSERT INTO  TC_Prj_XCTKJL_File (FId,FAppId,FlinkId,FFileName,FNum,FReportor,FFilePath) VALUES ");
            for (int i = 0; i < filePaths.Count(); i++)
            {
                if (!string.IsNullOrEmpty(filePaths[i]))
                {
                    string filename = filePaths[i].Split('/').ToList().Last();
                    if (i > 0)
                        sb.Append(",");
                    sb.Append("('" + Guid.NewGuid().ToString() + "','" + h_FAppId.Value + "','" + t_FId.Value + "','");
                    sb.Append(filename + "',0,'root','" + filePaths[i] + "')");
                }

            }
        }
        
        string sql = sb.ToString();

        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            int a = cmd.ExecuteNonQuery();

            //SqlDataAdapter da = new SqlDataAdapter(sql, conn);
           
        }

          
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnFileUpload_Click(object sender, EventArgs e) {

        //附件url
        string str = t_FilePath.Value;
        //文件大小
        float s = EConvert.ToInt(t_Size.Value);
        if (s > 1024) { s = s / 1024; n = "KB"; }
        if (s > 1024) { s = s / 1024; n = "MB"; }
        //附件类型
        t_FileType.Value = str.Split('.').ToList().Last();
        //附件文件名
        string m = str.Split('/').ToList().Last();
        name.Text += m + "（大小：" + s.ToString("0.0") + n + "）<br/>";
        //if (string.IsNullOrEmpty(t_FileName.Text))
        //{
        //    t_FileName.Text = m.Replace("." + m.Split('.').ToList().Last(), "");
        //}
    }
}