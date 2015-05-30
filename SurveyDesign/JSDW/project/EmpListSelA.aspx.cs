using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using Approve.RuleCenter;
using System.Data;
using Approve.EntityBase;
using System.Collections;
using EgovaDAO;

public partial class JSDW_project_EmpListSelA : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string qybm = EConvert.ToString(Request.QueryString["qybm"]);
            string prjItemid = EConvert.ToString(Request.QueryString["FPrjItemId"]);
            lblRylx.Value = EConvert.ToString(Request.QueryString["rylx"]);
            if (lblRylx.Value == "t_SGRYId")
                ddlEmpType.SelectedValue = "-1";
            if (lblRylx.Value == "t_JLRYId")
                ddlEmpType.SelectedValue = "-1";
            //是否是九大员
            string isjdy = EConvert.ToString(Request.QueryString["isjdy"]);
            ViewState["isjdy"] = isjdy;
            //
            ViewState["qybm"] = qybm;
            ViewState["FPrjItemId"] = prjItemid;
            BindControl();
            showInfo(qybm);
        }
    }
    void BindControl()
    {
        int iMType = EConvert.ToInt(Request.QueryString["fsysId"]);
        if (iMType > 0)
        {
            //   lTitle.Text = db.CF_Sys_SystemName.Where(t => t.FNumber == iMType).Select(t => t.FName).FirstOrDefault();
        }
    }
    string GetQYLX()
    {
        string sys = Request.QueryString["fsysId"];


        return sys;
    }

    //显示 
    void showInfo(string qybm)
    {
        EgovaDB1 db = new EgovaDB1();
        //如果是九大员就不用区分企业，直接选zslx为403的所有人员
        if (ViewState["isjdy"].ToString() == "true")
        {
            var v = from a in db.RY_RYJBXX
                    join c in db.RY_RYZSXX
                    on a.RYBH equals c.RYBH
                    where c.ZSLX == 403

                    select new
                    {
                        c.QYBM,
                        ZSLX = c.ZSLX,
                        c.RYZSXXID,
                        c.RYBH,
                        c.XM,
                        c.SFZH,
                        XBStr = c.XB,
                        c.ZCZSH,
                        c.ZCZY,
                        c.ZSYXQKSSJ,
                        c.ZSYXQJSSJ,
                        ZSYXQJSSJStr = string.Format("{0:d}", c.ZSYXQJSSJ),
                        FZSJStr = string.Format("{0:d}", c.FZSJ),
                        c.ZSJB
                    };
            if (!string.IsNullOrEmpty(this.txtIDCard.Text.Trim()))
            {
                v = v.Where(t => t.SFZH.Contains(this.txtIDCard.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
            {
                v = v.Where(t => t.XM.Contains(this.t_FName.Text.Trim()));
            }
            //if (this.ddlEmpType.SelectedIndex > 0)
            //{
            //    string sel = this.ddlEmpType.SelectedValue;
            //    if (sel == "1" || sel == "2" || sel == "3")
            //    {
            //        v = v.Where(t => t.ZSLX == 407 && (t.ZSJB == "115" || t.ZSJB == "1151" || t.ZSJB == "116" || t.ZSJB == "1161"));
            //    }
            //    else if (sel == "4")
            //    {
            //        v = v.Where(t => t.ZSLX == 403 && (t.ZSJB == "40304" || t.ZSJB == "40306" || t.ZSJB == "40301" || t.ZSJB == "40302" || t.ZSJB == "40305"));
            //    }
            //    else
            //    {
            //        v = v.Where(t => t.ZSLX.ToString() == sel);
            //    }

            //}
            Pager1.RecordCount = v.Count();
            dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else
        {
            
            var qy = from t in db.QY_JBXX
                     where t.QYBM == qybm
                     select new { t.FCompanyId };
            string qybmnew = "";
            if (qy == null)
            {
                qybmnew = qybm;
            }
            else
            {
                qybmnew = qy.FirstOrDefault().FCompanyId;
            }

            var v = from a in db.RY_RYJBXX
                    join c in db.RY_RYZSXX
                    on a.RYBH equals c.RYBH
                    //where a.QYBM == qybm
                    where a.QYBM == qybmnew
                    select new
                    {
                        c.QYBM,
                        c.ZSLX,
                        c.RYBH,
                        c.XM,
                        c.SFZH,
                        XBStr = c.XB,
                        c.ZCZSH,
                        c.ZCZY,
                        c.ZSYXQKSSJ,
                        c.ZSYXQJSSJ,
                        ZSYXQJSSJStr = string.Format("{0:d}", c.ZSYXQJSSJ),
                        FZSJStr = string.Format("{0:d}", c.FZSJ),
                        c.ZSJB
                    };
            if (!string.IsNullOrEmpty(this.txtIDCard.Text.Trim()))
            {
                v = v.Where(t => t.SFZH.Contains(this.txtIDCard.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
            {
                v = v.Where(t => t.XM.Contains(this.t_FName.Text.Trim()));
            }
            if (this.ddlEmpType.SelectedIndex > 0)
            {
                string sel = this.ddlEmpType.SelectedValue;
                if (sel == "1" || sel == "2" || sel == "3")
                {
                    v = v.Where(t => t.ZSLX == 407 && (t.ZSJB == "115" || t.ZSJB == "1151" || t.ZSJB == "116" || t.ZSJB == "1161"));
                }
                else if (sel == "4")
                {
                    v = v.Where(t => t.ZSLX == 403 && (t.ZSJB == "40304" || t.ZSJB == "40306" || t.ZSJB == "40301" || t.ZSJB == "40302" || t.ZSJB == "40305"));
                }
                else
                {
                    v = v.Where(t => t.ZSLX.ToString() == sel);
                }

            }
            Pager1.RecordCount = v.Count();
            dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        string qybm = EConvert.ToString(ViewState["qybm"]);
        showInfo(qybm);
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        string qybm = EConvert.ToString(ViewState["qybm"]);
        showInfo(qybm);
    }
    protected void dg_List_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                HiddenField hfEmpId = e.Item.FindControl("hfEmpId") as HiddenField;
                string fid = hfEmpId.Value;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }

    }

    protected void dg_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        EgovaDB db = new EgovaDB();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //Label lblLock = e.Item.Controls[0].FindControl("lblLock") as Label;
            //HiddenField hLock = e.Item.Controls[0].FindControl("h_lock") as HiddenField;           
            //string idCard = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "SFZH"));
            //var v = db.TC_PrjItem_Emp_Lock.FirstOrDefault(t => t.FIdCard == idCard);
            //if (v != null && EConvert.ToBool(LockBusiness(v)))
            //{
            //    lblLock.Text = "锁定";
            //    hLock.Value = "1";
            //}
            //else
            //{
            //    lblLock.Text = "";
            //    hLock.Value = "0";
            //}
            LinkButton lb = e.Item.FindControl("btnSelect") as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return selEmp(this);");
        }
    }

    /// <summary>
    /// 是否满足锁定业务
    /// true:已锁定
    /// false:未锁定
    /// </summary>
    /// 如果人员状态是已锁定（以身份证号为准进行判断，15位18位自动识别）
    /// 则当前业务的工程所在地与已锁定的工程所在地完全一致才允许选入（且只能允许选入3次），否则不允许选入
    /// <param name="empLock"></param>
    /// <returns></returns>
    private bool LockBusiness(TC_PrjItem_Emp_Lock empLock)
    {
        if (empLock == null) return false;
        EgovaDB db = new EgovaDB();
        //是否锁定
        if (!EConvert.ToBool(empLock.IsLock)) return false;
        //已锁定的项目地区
        var lockArea = db.TC_PrjItem_Info.FirstOrDefault(item => item.FId == empLock.FPrjItemId).AddressDept;
        //当前项目地区

        var prjItemid = EConvert.ToString(ViewState["FPrjItemId"]);
        string  area ="";
        if (prjItemid != null && prjItemid != "")
        {
            area = db.TC_PrjItem_Info.FirstOrDefault(item => item.FId == prjItemid).AddressDept;
        }
        return ((lockArea != area) || empLock.SelectedCount >= 3);
    }
}