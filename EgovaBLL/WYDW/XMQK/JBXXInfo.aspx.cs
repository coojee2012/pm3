using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_JBXXInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showinfo();
        }
        BindControl();
    }
    void BindControl()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("FName");
        dt.Columns.Add("FNumber");
        DataRow dr = dt.NewRow();
        dr["FName"] = "房屋建筑";
        dr["FNumber"] = "20001";
        dt.Rows.Add(dr);
        t_XMLX.DataSource = dt;
        t_XMLX.DataTextField = "FName";
        t_XMLX.DataValueField = "FNumber";
        t_XMLX.DataBind();

        DataTable dt2 = rc.getDicTbByFNumber("2000101");
        t_XMZLX.DataSource = dt2;
        t_XMZLX.DataTextField = "FName";
        t_XMZLX.DataValueField = "FNumber";
        t_XMZLX.DataBind();

        DataTable dt3 = rc.getDicTbByFNumber("20005");
        t_JSXZ.DataSource = dt3;
        t_JSXZ.DataTextField = "FName";
        t_JSXZ.DataValueField = "FNumber";
        t_JSXZ.DataBind();

        DataTable dt4 = rc.getDicTbByFNumber("20009");
        t_JSMS.DataSource = dt4;
        t_JSMS.DataTextField = "FName";
        t_JSMS.DataValueField = "FNumber";
        t_JSMS.DataBind();

    }
    private void showinfo()
    {
        if (Session["GovLinkID"] != null)
        {
            Session["XMQK_XMBH"] = Session["GovLinkID"];
            Session["GovLinkID"] = "1";
        }
        if (Session["XMQK_XMBH"] != null)
        {
            string sql = "select * from WY_XM_JBXX where XMBH='" + (string)Session["XMQK_XMBH"] +"' ";
            DataTable dt = rc.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                t_ProjectName.Text = dt.Rows[0]["XMMC"].ToString();
                t_JSDW.Text = dt.Rows[0]["JSDW"].ToString();
                if (dt.Rows[0]["JSDW"].ToString() != "")
                {
                    t_JSDW.Enabled = false;
                }
                govd_FRegistDeptId.fNumber = dt.Rows[0]["XMSD"].ToString();
                t_Address.Text = dt.Rows[0]["XMDZ"].ToString();
                t_JSDWDM.Text = dt.Rows[0]["JSDWZZJFDM"].ToString();
                if (dt.Rows[0]["JSDWZZJFDM"].ToString() != "")
                {
                    t_JSDWDM.Enabled = false;
                }
                t_JSDWDZ.Text = dt.Rows[0]["JSDWDZ"].ToString();
                if (dt.Rows[0]["JSDWDZ"].ToString() != "")
                {
                    t_JSDWDZ.Enabled = false;
                }
                t_Contacts.Text = dt.Rows[0]["JSDWFR"].ToString();
                t_Mobile.Text = dt.Rows[0]["JSDWFRDH"].ToString();
                t_JSDWJSFZR.Text = dt.Rows[0]["JSDWJSFZR"].ToString();
                t_JSDWJSFZRZC.Text = dt.Rows[0]["JSDWJSFZRZC"].ToString();
                t_JSDWJSFZRDH.Text = dt.Rows[0]["JSDWJSFZRDH"].ToString();
                t_XMLX.SelectedValue = dt.Rows[0]["XMLX"].ToString();
                t_XMZLX.SelectedValue = dt.Rows[0]["XMZLX"].ToString();
                t_JSXZ.SelectedValue = dt.Rows[0]["JSXZ"].ToString();
                t_JSMS.SelectedValue = dt.Rows[0]["JSMS"].ToString();
                t_XMZTZ.Text = dt.Rows[0]["XMZTZ"].ToString();
                t_JSGM.Text = dt.Rows[0]["JSGM"].ToString();
                t_JSNR.Text = dt.Rows[0]["JSNR"].ToString();
                //待用数据
                //Session["_JBXX"] = dt;
                Session["_XMBH"] = dt.Rows[0]["XMBH"].ToString();

            }
            else
            {
                if (Session["GovLinkID"] != null && Session["GovLinkID"].ToString() == "1")
                {
                    this.RegisterStartupScript(new Guid().ToString(),
                        "<script>alert('数据加载失败！');parent.location.href='../../Government/Main/aIndex.aspx';</script>");
                }
                else
                {
                    this.RegisterStartupScript(new Guid().ToString(),
                        "<script>alert('数据加载失败！');parent.location.href='../main/Index.aspx';</script>");
                }
            }
        }
        else
        {
            if (Session["GovLinkID"] != null && Session["GovLinkID"].ToString() == "1")
            {
                this.RegisterStartupScript(new Guid().ToString(),
                    "<script>alert('数据加载失败！');parent.location.href='../../Government/Main/aIndex.aspx';</script>");
            }
            else
            {
                this.RegisterStartupScript(new Guid().ToString(),
                    "<script>alert('数据加载失败！');parent.location.href='../main/Index.aspx';</script>");
            }
        }
    }
}