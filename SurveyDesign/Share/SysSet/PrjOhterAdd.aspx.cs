using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using ProjectData;

public partial class Admin_yamain_PrjOhterAdd : Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("showMType();");
        if (!Page.IsPostBack)
        {
            BindControl();
            if (Request["FManageId"] == null && Request["FManageId"] == "")
            {
                return;
            }
            if (Request["FManageType"] == null && Request["FManageType"] == "")
            {
                return;
            }
            ShowParentInfo();
            this.btnAdd.Attributes.Add("onclick", "if(CheckInfo()){return true;}else{return false;}");
            if (Request["fid"] != null && Request["fid"] != "")
            {
                ViewState["FID"] = Request["fid"];
                ShowInfo();
            }
        }
    }

    private void BindControl()
    {
        DataTable dt = rc.GetTable(EntityTypeEnum.EsDic, "fname,fnumber", "fparentid='20001' order by forder ");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            check_FIsPrjType.Items.Add(new ListItem(dt.Rows[i]["FName"].ToString(), dt.Rows[i]["FNumber"].ToString()));
        }
    }

    private void ShowParentInfo()
    {
        DataTable dt = rc.GetTable(EntityTypeEnum.EsManageType, "FName,FNumber", "fid='" + Request["FManageId"] + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["FName"] != DBNull.Value)
            {
                this.txtFManageName.Text = dt.Rows[0]["FName"].ToString();

            }
            if (dt.Rows[0]["FNumber"] != DBNull.Value)
            {
                this.txtFNumber.Text = dt.Rows[0]["FNumber"].ToString();

            }
        }
    }

    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_PrjList where fid='" + ViewState["FID"].ToString() + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);

            //关联业务编号
            ProjectDB db = new ProjectDB();
            string FMType = dt.Rows[0]["FMType"].ToString();
            lit_FMType.Text = string.Join("、", (from t in db.CF_Sys_ManageType
                                                where FMType.Split(',').ToList().Contains(t.FNumber.ToString())
                                                select t.FName).ToArray());



            setPrjTypeList(dt.Rows[0]["FIsPrjType"].ToString());
            showPrjType();
        }
    }

    private void SaveInfo()
    {
        if (Request["FManageId"] == null && Request["FManageId"] == "")
        {
            return;
        }

        pageTool tool = new pageTool(this.Page);
        SaveOptionEnum so = SaveOptionEnum.Insert;
        SortedList sl = tool.getPageValue();
        sl.Add("FIsPrjType", getPrjTypeList());
        if (this.ViewState["FID"] != null)
        {
            so = SaveOptionEnum.Update;
            sl.Add("FID", this.ViewState["FID"].ToString());
        }
        else
        {
            sl.Add("FManageId", Request["FManageId"]);
            sl.Add("FManageType", Request["FManageType"]);
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
        }
        if (!string.IsNullOrEmpty(this.hid_fileID.Value))
        {
            sl.Add("FFileId", this.hid_fileID.Value);
        }
        if (rc.SaveEBase(EntityTypeEnum.EsPrjList, sl, "FID", so))
        {
            ViewState["FID"] = sl["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.ViewState["FID"] = null;
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("clearPage()");
    }

    protected void btnReload_ServerClick(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.hid_fileID.Value))
        {
            this.t_FFileName.Text = rc.GetSignValue(EntityTypeEnum.EsDic, "fname", "fid='" + this.hid_fileID.Value + "'");
        }
    }


    //选择关联业务后更新
    protected void btnMType_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void t_FIsMust_SelectedIndexChanged(object sender, EventArgs e)
    {
        showPrjType();
    }

    /// <summary>
    /// 显示/隐藏 工程类别
    /// </summary>
    private void showPrjType()
    {
        if (t_FIsMust.SelectedValue == "1")
            tr_prjType.Visible = true;
        else
            tr_prjType.Visible = true;
    }

    /// <summary>
    /// 得到所选的 工程类别编号
    /// </summary>
    /// <returns></returns>
    private string getPrjTypeList()
    {
        string str = "";
        int n = check_FIsPrjType.Items.Count;
        for (int i = 0; i < n; i++)
        {
            if (check_FIsPrjType.Items[i].Selected)
            {
                if (!string.IsNullOrEmpty(str))
                    str += ",";
                str += "'" + check_FIsPrjType.Items[i].Value + "'";
            }
        }
        return str;
    }

    /// <summary>
    /// 选中相应的复选框
    /// </summary>
    /// <param name="str"></param>
    private void setPrjTypeList(string strlist)
    {
        int n = check_FIsPrjType.Items.Count;
        for (int i = 0; i < n; i++)
        {
            foreach (string str in strlist.Replace("'", "").Split(new char[] { ',' }))
            {
                if (str == check_FIsPrjType.Items[i].Value)
                {
                    check_FIsPrjType.Items[i].Selected = true;
                }
            }
        }
    }

}
