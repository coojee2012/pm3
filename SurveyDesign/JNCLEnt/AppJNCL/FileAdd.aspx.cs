using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Approve.Common;
using System.Text;
using System.Data;
using Approve.RuleCenter;

public partial class JNCLEnt_AppJNCL_FileAdd : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(FIsApprove))
            {
                if (FIsApprove == "1")
                {
                    btnSave.Enabled = false;
                    btnUpLoad.Enabled = false;
                }
            }
            ShowInfo();
        }
    }
    private void ShowInfo()
    {
        string typeName = TypeName;
        ltrtypeName.Text = typeName;
        if (!string.IsNullOrEmpty(FileId) && !string.IsNullOrEmpty(YWBM))
        {
            string sql = string.Format("select * from YW_FILE_DETAIL where FileId={0} and YWBM='{1}'", FileId, YWBM);
            DataTable table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                StringBuilder _builder = new StringBuilder();
                foreach (DataRow row in table.Rows)
                {
                    _builder.Append("<tr>");
                    //string url = "../ApplyXZYJS/DownLoad.aspx?filePath=" + row["DownLoadPath"] + "&fileName=" + row["FILE_NAME"];
                    string url = row["DownLoadPath"].ToString();
                    if (FIsApprove == "1")
                        _builder.AppendFormat("<td>{0}</td><td>{2}</td><td width='100'><a href='{1}' target='_blank'>查看附件</a></td>", row["FILE_NAME"], url, row["CreateTime"]);
                    else
                        _builder.AppendFormat("<td>{0}</td><td>{2}</td><td width='100'><a href='javascript:void(0)' onclick=\"DelFile('{1}',this)\">删 除</a>&nbsp;&nbsp;<a href='" + url + "' target='_blank'>查看附件</a></td>", row["FILE_NAME"], row["ID"], row["CreateTime"]);
                    _builder.Append("</tr>");
                }
                ltrFile.Text = _builder.ToString();
            }
            else
                ltrFile.Text = string.Empty;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "txt");
        string sql = "";
        if (!string.IsNullOrEmpty(hfUpadFile.Value))
        {
            string[] items = hfUpadFile.Value.Split(',');
            foreach (var item in items)
            {
                string downLoadPath = item.Split('|')[0];
                string fileSize = item.Split('|')[1];
                string fileName = Path.GetFileName(downLoadPath);
                sql += string.Format("INSERT INTO YW_FILE_DETAIL(ID,FileId,YWBM,[FILE_NAME],FILE_SIZE,DownLoadPath)VALUES(NEWID(),{0},'{1}','{2}','{3}','{4}')", FileId, YWBM, fileName, fileSize, downLoadPath);
            }
            if (!string.IsNullOrEmpty(sql))
            {
                bool success = rc.PExcute(sql);
                if (success)
                {
                    hfUpadFile.Value = "";
                    tool.showMessage("保存成功");
                    ShowInfo();
                }
                else
                    tool.showMessage("保存失败");
            }
        }
        else
            tool.showMessage("未上传文件或文件未上传完成");
    }
    private string FileId
    {
        get
        {
            return Request.QueryString["typeId"];
        }
    }
    private string TypeName
    {
        get
        {
            return HttpUtility.UrlDecode(Request.QueryString["typeName"], Encoding.Default);
        }
    }
    private string FIsApprove
    {
        get
        {
            string value = Request.QueryString["FIsApprove"];
            if (string.IsNullOrEmpty(value))
                return "0";
            return value;
        }
    }
    private string YWBM
    {
        get
        {
            return Request.QueryString["YWBM"];
        }
    }
}