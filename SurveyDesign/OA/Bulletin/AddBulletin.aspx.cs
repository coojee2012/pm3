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

public partial class OA_Bulletin_AddBulletin : System.Web.UI.Page
{
    bool temp = false;
    OA oa = new OA();
    string userId = "";
    SaveOptionEnum so = SaveOptionEnum.Insert;
    string mag = "公告发布成功！";
    string FID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append(@"select bt.FID,bt.TypeName from CF_OA_BulType bt,CF_OA_Emp u where bt.FUserID=u.FID and bt.FIsDeleted = 'false'");
            //DataTable dt = oa.GetTable(sb.ToString());
            //this.t_FBulTypeId.Items.Add(new ListItem(" ", " "));
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        this.t_FBulTypeId.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
            //    }
            //}
            if (Request.QueryString["fid"] != null && Request.QueryString["fid"].ToString() != "")
            {
                this.ViewState["FID"] = Request.QueryString["fid"]; ShowInfo();
            }

        }
        this.OFFDuty.Attributes.Add("onclick", "return onchick()");
        if (Session["FEmpID"] != null)
        {
            userId = Session["FEmpID"].ToString();

        }

    }

    protected void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        DataTable dt = oa.GetTable("select * from CF_OA_Bulletin where FID ='" + this.ViewState["FID"].ToString() + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            if ((int)dt.Rows[0][11] < 5000)
            {
                this.RadioButton1.Checked = true;
            }
            if ((int)dt.Rows[0]["FIsApp"] == 1)
            {
                this.CheckBox1.Checked = true;
            }
        }
        string MsgTo = "";
        string bullid = this.ViewState["FID"].ToString();
        dt = oa.GetTable("select RoleID from CF_OA_BullReal where BullID='" + this.ViewState["FID"].ToString() + "' and fisdeleted = 'false'");
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    MsgTo += ",";
                }
                MsgTo += "'" + dt.Rows[i][0].ToString() + "'";
            }
        }
        this.presonFID.Value = MsgTo;

        ShowName();


    }

    protected void OFFDuty_Click(object sender, EventArgs e)
    {


        SaveInfo();
    }

    private void RealInfo()
    {
        IList presFID = new ArrayList();
        IList orgFIDs = new ArrayList();
        string tos = this.presonFID.Value.Trim().ToString();
        tos = tos.Replace("^", "'");
        int m = (tos.Trim().Length + 1) / 11;

        temp = false;
        string str = "update CF_OA_BullReal set fisdeleted='true' where BullID='" + this.ViewState["FID"].ToString() + "'";
        oa.PExcute(str);

        SortedList[] sl = new SortedList[m];
        EntityTypeEnum[] en = new EntityTypeEnum[m];
        SaveOptionEnum[] so1 = new SaveOptionEnum[m];
        string[] key = new string[m];
        //IList pres = new ArrayList();//注意这里需要修改。
        for (int i = 0; i < m; i++)
        {
            sl[i] = new SortedList();
            en[i] = EntityTypeEnum.EOABullReal;
            so1[i] = SaveOptionEnum.Insert;
            key[i] = "FID";
            string FMsgTO = tos.Substring(i * 11, 10);
            FMsgTO = FMsgTO.Replace("'", "");
            string FID1 = Guid.NewGuid().ToString();
            sl[i].Add("FID", FID1);
            sl[i].Add("BullID", this.ViewState["FID"].ToString());
            sl[i].Add("RoleID", FMsgTO);
            orgFIDs.Add(FMsgTO);
            sl[i].Add("FIsDeleted", false);
            sl[i].Add("FCratetime", DateTime.Now);

        }
        if (oa.SaveEBaseM(en, sl, key, so1))
        {
            temp = true;
            SaveDevelopment.SaveDevel("发了一条新的公告", "../images/ggfb.jpg", userId, this.t_FTitle.Text, null, orgFIDs);
        }

    }

    private void SaveInfo()
    {

        pageTool tool = new pageTool(this.Page);
        if (this.t_FTitle.Text == null || this.t_FTitle.Text.Trim() == "")
        {
            tool.showMessage("公告标题必须填写！");
            return;
        }
        if (this.t_FContent.Value == null || this.t_FContent.Value.ToString().Trim() == "")
        {
            tool.showMessage("公告内容必须填写！");
            return;
        }
        if (this.t_FContent.Value.ToString().Length > 254)
        {
            tool.showMessage("内容字数超过限制，必须小于255字!");
            return;
        }
        //if (this.t_FBulTypeId.Text == null || this.t_FBulTypeId.Text.Trim() == "")
        //{
        //    tool.showMessage("请选择公告类别！");
        //    return;
        //}
        if (this.presonFID.Value == null || this.presonFID.Value.ToString().Trim() == "")
        {
            tool.showMessage("请添加发布范围！");
            return;
        }
        if (this.t_FDateOn.Text == null || this.t_FDateOn.Text.Trim() == "")
        {
            tool.showMessage("请选择公告起始时间！");
            return;
        }
        if (this.t_FDateOff.Text == null || this.t_FDateOff.Text.Trim() == "")
        {
            tool.showMessage("请选择公告结束时间！");
            return;
        }
        DateTime dton = DateTime.Parse(this.t_FDateOn.Text);
        DateTime dtoff = DateTime.Parse(this.t_FDateOff.Text);
        if (dtoff < dton)
        {
            tool.showMessage("公告结束时间不能小于起始时间！");
            return;
        }
        SortedList sl = new SortedList();
        if (this.ViewState["FID"] == null)
        {
            this.ViewState["FID"] = Guid.NewGuid().ToString();
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        sl.Add("FTitle", this.t_FTitle.Text);
        if (this.RadioButton1.Checked)
        {
            sl.Add("FState", 3000);
        }
        else
        {
            sl.Add("FState", 5000);
        }
        if (this.CheckBox1.Checked)
        {
            sl.Add("FIsApp", 1);
        }
        else
        {
            sl.Add("FIsApp", 0);
        }
        sl.Add("FuserID", userId);
        sl.Add("FContent", this.t_FContent.Value.ToString());
        //sl.Add("FBulTypeId", this.t_FBulTypeId.Text);
        sl.Add("FIsDeleted", false);
        sl.Add("FDateOn", this.t_FDateOn.Text);
        sl.Add("FDateOff", this.t_FDateOff.Text + " 23:59:59");
        sl.Add("FCratetime", DateTime.Now);

        if (oa.SaveEBase(EntityTypeEnum.EOABulletin, sl, "FID", so))
        {
            RealInfo();
        }
        if (temp)
        {
            tool.showMessage(mag);
        }
        else
        {
            tool.showMessage("发送失败");
        }

    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowName();
    }

    private void ShowName()
    {
        this.presonFID.Value = this.presonFID.Value.ToString().Replace("^", "'");
        string str = "";
        StringBuilder sb = new StringBuilder();
        if (this.presonFID.Value.Length > 8)
        {
            sb.Append("select fname from CF_OA_Organization where Fnumber in(" + this.presonFID.Value + ")");
            DataTable dt = oa.GetTable(sb.ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (str.Length > 0)
                {
                    str += ",";
                }
                str += dt.Rows[i]["fname"].ToString();
            }
            this.presonList.Text = str;
        }
        this.presonList.Text = str;

        this.presonFID.Value = this.presonFID.Value.ToString().Replace("'", "^");
    }
    protected void btnReload_ServerClick(object sender, EventArgs e)
    {
        ShowName();
    }
}
