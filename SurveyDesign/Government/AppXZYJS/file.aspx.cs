using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_AuditMain_file : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo();
            ltrName.Text = TypeName;
        }
    }
    private void ShowInfo()
    {
        if (!string.IsNullOrEmpty(FileId) && !string.IsNullOrEmpty(YJS_ID))
        {
            string sql = string.Format("select * from YW_FILE_DETAIL where FileId={0} and YWBM='{1}'", FileId, YJS_ID);
            DataTable table = rc.GetTable(sql);
            if (table != null && table.Rows.Count > 0)
            {
                StringBuilder _builder = new StringBuilder();
                foreach (DataRow row in table.Rows)
                {
                    _builder.Append("<tr>");
                    _builder.AppendFormat("<td>{0}</td>", row["FILE_NAME"]);
                    _builder.AppendFormat("<td><a href='{0}'>查看附件</a></td>", row["DownLoadPath"]);
                    _builder.Append("</tr>");
                }
                ltrFile.Text = _builder.ToString();
            }
        }
    }
    private string FileId {
        get {
            return Request.QueryString["typeId"];
        }
    }
    private string YJS_ID {
        get {
            return Request.QueryString["YJS_ID"];
        }
    }
    private string TypeName {
        get {
            return Request.QueryString["typeName"];
        }
    }
}