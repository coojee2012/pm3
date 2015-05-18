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
using Approve.Common;

public partial class JSDW_project_EmpListSelAqjd: System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    
    //显示 
     void showInfo()
    {

        EgovaDB1 db = new EgovaDB1();    
    
        
        //var v = from a in db.RY_RYJBXX
        //        join c in db.RY_RYZSXX
        //        on a.RYBH equals c.RYBH
        //        into temp

        //        join d in dbContext.TC_AJBA_CJDW
        //        on a.QYBM equals d.QYID                             
        //        where d.FAppId == EConvert.ToString(Session["FAppId"])
        //        from tt in temp.DefaultIfEmpty()          
        //        select new
        //        {
        //            a.QYBM,
        //            //t.ZSLX,
        //            tt.ZSLX,
        //            a.RYBH,
        //            a.XM,
        //            a.SFZH,
        //            XBStr = a.XB == "0" ? "男" : "女",
        //            tt.ZCZSH,
        //            tt.ZCZY,
        //            tt.ZSYXQKSSJ,
        //            tt.ZSYXQJSSJ,
        //            //c.ZCZSH,
        //            //c.ZCZY,
        //            //c.ZSYXQKSSJ,
        //            //c.ZSYXQJSSJ,
        //            ZSYXQJSSJStr = string.Format("{0:d}", tt.ZSYXQJSSJ),
        //            FZSJStr = string.Format("{0:d}", tt.FZSJ)
        //        };
        //if (!string.IsNullOrEmpty(this.txtIDCard.Text.Trim()))
        //{
        //    v = v.Where(t => t.SFZH.Contains(this.txtIDCard.Text.Trim()));
        //}
        //if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
        //{
        //    v = v.Where(t => t.XM.Contains(this.t_FName.Text.Trim()));
        //}
        //if (this.ddlEmpType.SelectedIndex > 0)
        //{
        //    string sel = this.ddlEmpType.SelectedValue;
        //    if (sel == "1" || sel == "2" || sel == "3")
        //    {
        //        v = v.Where(t => t.ZSLX == "1" || t.ZSLX == "2" || t.ZSLX == "3" || t.ZSLX == "4" || t.ZSLX == "5");
        //    }
        //    else if (sel == "4")
        //    {
        //        v = v.Where(t => t.ZSLX == "1" || t.ZSLX == "2" || t.ZSLX == "3" || t.ZSLX == "4");
        //    }
        //    else
        //    {
        //        v = v.Where(t => t.ZSLX == sel);
        //    }

        //}
        //Pager1.RecordCount = v.Count();
        //dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        //dg_List.DataBind();

       
        string sql = @"select  b.QYBM,
                    b.ZSLX,
                    b.RYBH,
                    b.XM,
                    b.SFZH,
                    b.xb  as XBStr,
                    b.ZCZSH,
                    b.ZCZY,
                    b.ZSYXQKSSJ,
                    b.ZSYXQJSSJ,
                   convert(date,b.ZSYXQJSSJ) ZSYXQJSSJStr,
                   convert(date,b.FZSJ) FZSJStr         
                       from  JST_XZSPBaseInfo.dbo.RY_RYJBXX a,JST_XZSPBaseInfo.dbo.RY_RYZSXX b
                        where a.RYBH = b.RYBH
                        and b.QYBM in (select  QYID   from  TC_AJBA_CJDW  where FAppId = '" + Session["FAppId"].ToString()+"')";
        if (!string.IsNullOrEmpty(this.txtIDCard.Text.Trim()))
        {
            sql += " and b.SFZH  like '%" + this.txtIDCard.Text.Trim()+"%'";           
        }
        if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
        {
            sql += " and  b.xm  like '%" + this.t_FName.Text.Trim() + "%'";
        }
        if (this.ddlEmpType.SelectedIndex > 0)
        {
            string sel = this.ddlEmpType.SelectedValue;
            if (sel == "1" || sel == "2" || sel == "3")
            {
                sql += " and  (b.ZSLX  = '407') and (b.ZSJB  = '115'  or b.ZSJB  = '1151'  or b.ZSJB  = '116' or b.ZSJB  = '1161' )";              
                
            }
            else if (sel == "4")
            {
                sql += " and  (b.ZSLX  = '403') and (b.ZSJB  = '40304'  or b.ZSJB  = '40306'  or b.ZSJB  = '40301' or b.ZSJB  = '40302'   or b.ZSJB  = '40305')";   
            }
            else
            {
                sql += " and  b.ZSLX  = '"+sel+"'";                  
            }
          
        }  
        this.Pager2.sql = sql.ToString();
        this.Pager2.controltype = "Repeater";
        this.Pager2.controltopage = "dg_List";
        this.Pager2.className = "dbCenter";
        this.Pager2.pagecount = 10;
        this.Pager2.dataBind();             

    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        //Pager1.CurrentPageIndex = e.NewPageIndex;       
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {       
        showInfo();
    }
    protected void dg_List_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                HiddenField hfEmpId = e.Item.FindControl("hfEmpId") as HiddenField;
                string fid = hfEmpId.Value;
                Tools.pageTool tool = new Tools.pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }
       
    }

    protected void dg_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        EgovaDB db = new EgovaDB();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //MODIFY:ytb 修改锁定按钮显示
            //LinkButton lblLock = e.Item.Controls[0].FindControl("lkb_Lock") as LinkButton;
            //HiddenField hLock = e.Item.Controls[0].FindControl("h_lock") as HiddenField;           
            //string idCard = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "SFZH"));
            //var v = db.TC_PrjItem_Emp_Lock.FirstOrDefault(t => t.FIdCard == idCard);
            //if (v != null && EConvert.ToBool(LockBusiness(v)))
            //{
            //    lblLock.Text = "锁定";
            //    hLock.Value = "1";
            //    //MODIFY:YTB 为锁定按钮注册点击事件
            //    lblLock.Attributes.Add("onclick", "showEmpinfo('" + idCard + "');");
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
    private bool LockBusiness(TC_PrjItem_Emp_Lock empLock) {
        if (empLock == null) return false;
        EgovaDB db = new EgovaDB();
        //是否锁定
        if (!EConvert.ToBool(empLock.IsLock)) return false;
        //已锁定的项目地区
        var lockArea = db.TC_PrjItem_Info.FirstOrDefault(item => item.FId == empLock.FPrjItemId).AddressDept;   
        //当前项目地区
        var prjItemid = EConvert.ToString(ViewState["FPrjItemId"]);
        var area = db.TC_PrjItem_Info.FirstOrDefault(item => item.FId == prjItemid).AddressDept;

        return ((lockArea!=area)||empLock.SelectedCount>=3);
    }
    protected void Pager1_PageChanged(object sender, EventArgs e)
    {

    }
}