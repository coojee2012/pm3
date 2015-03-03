using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyYDGH_FileList : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo();
            hfJG_Id.Value = JG_Id;
            hfFIsApprove.Value = FIsApprove;
        }
    }
    private void ShowInfo()
    {
        string sql = string.Format("select A.ID,A.[FILE_NAME],COUNT(B.ID) TOTAL,A.Number FROM YW_FILE A LEFT JOIN YW_FILE_DETAIL B ON A.ID=B.[FileId] WHERE A.YWBM='{0}'  GROUP BY A.[FILE_NAME],a.ID,A.Number", JG_Id);
        DataTable table = rc.GetTable(sql);
        if (table != null && table.Rows.Count > 0)
        {
            System.Text.StringBuilder _builder = new System.Text.StringBuilder();
            _builder.Append("<tr>");
            _builder.Append("<td class=\"t_r t_bg\" style=\"width: 50px; text-align: center;\">序号</td>");
            _builder.Append("<td class=\"t_r t_bg\" style=\"width: 280px; text-align: center;\">材料名称</td>");
            _builder.Append("<td class=\"t_r t_bg\" style=\"width: 50px; text-align: center;\">份数</td>");
            _builder.Append("<td class=\"t_r t_bg\" style=\"text-align: left;\">电子件</td>");
            _builder.Append("</tr>");
            int num = 0;
            foreach (DataRow row in table.Rows)
            {
                num++;
                _builder.Append("<tr>");
                _builder.AppendFormat("<td class=\"t_r t_bg\" style=\"width: 50px; text-align: center;\">{0}</td>", num);
                _builder.AppendFormat("<td class=\"t_r t_bg\" style=\"width: 280px; text-align: left;\">{0}：</td>", row["FILE_NAME"]);
                _builder.AppendFormat("<td class=\"t_r t_bg\" style=\"width: 50px; text-align: center;\">{0}</td>", row["Number"]);
                _builder.AppendFormat("<td><input type=\"button\" class=\"m_btn_w6\" value=\"文件上传({0})\" onclick=\"UpadLoadFile('{1}',this)\" /></td>", row["TOTAL"], row["ID"]);
                _builder.Append("</tr>");
            }
            ltrText.Text = _builder.ToString();
        }
    }
    private string FIsApprove
    {
        get
        {
            string value = Session["FIsApprove"] == null ? "" : Session["FIsApprove"].ToString();
            if (string.IsNullOrEmpty(value))
                return "0";
            return value;
        }
    }
    private string JG_Id
    {
        get
        {
            return Request.QueryString["JG_Id"];
        }
    }
}

