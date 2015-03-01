using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;

public partial class EntApprove_main_Message : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ShowAppList();
        }
    }

    private void ShowAppList()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select  t.fname,t.FPubDepart,c.fpubtime,t.FPicUrl,t.FWebId,t.FFileNote,t.FOperType,t.fid,t.forder  from CF_News_Title t ");
        sb.Append(" inner join CF_News_Col c on t.Fid = c.fnewsid and c.fcolnumber='60803'");
        sb.Append(" and t.fstate=1 ");
        sb.Append(" and t.FValidEnd>='" + DateTime.Now.ToString() + "' ");
       
        sb.Append(" order by t.forder,t.fpubtime desc");
        DataTable dt=rc.GetTable(sb.ToString());
        this.DG_Apply.DataSource=dt;
        this.DG_Apply.DataBind();
    }

    protected void DG_Apply_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string openType = e.Item.Cells[e.Item.Cells.Count - 2].Text;

            string fWebId = e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string fFileNote = e.Item.Cells[e.Item.Cells.Count - 4].Text;

            switch (openType)
            {
                case "1":
                    e.Item.Cells[1].Text = "<a href='" + fWebId + "' target=_blank class=text0>" + e.Item.Cells[1].Text + "</a>";
                    break;

                case "2":
                    e.Item.Cells[1].Text = "<a href='" + fFileNote + "' target=_blank class=text0>" + e.Item.Cells[1].Text + "</a>";
                    break;

                case "3":
                    e.Item.Cells[1].Text = "<a href='MessageDetail.aspx?fid=" + fid + "' target=_blank class=text0>" + e.Item.Cells[1].Text + "</a>";
                    break; 

                default:
                    e.Item.Cells[1].Text = "<a href='MessageDetail.aspx?fid=" + fid + "' target=_blank class=text0>" + e.Item.Cells[1].Text + "</a>";
                    break;
            }
        }
    }
}
