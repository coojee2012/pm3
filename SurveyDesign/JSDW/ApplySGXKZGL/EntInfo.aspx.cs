﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using System.Data;
using EgovaDAO;
using Tools;
using System.Text;
using System.Web.Services;

public partial class JSDW_APPLYSGXKZGL_EntInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //应用编号
            t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            //企业类型
            t_FEntType.Value = EConvert.ToString(Request["FEntType"]);
            //企业编号主键
            txtFId.Value = EConvert.ToString(Request["FId"]);

            //t_FPrjId.Value = EConvert.ToString(Request["FPrjId"]);
            //t_FPrjItemId.Value = EConvert.ToString(Request["FPrjItemId"]);
            showTitle();
            showInfo();
            showEmpList();
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {

                tool.ExecuteScript("btnEnable();");
                //同时屏蔽添加企业的按钮
                btnAddEnt.Visible = false;
            }
        }
        else
        {
            //企业编号
            
        }
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Ent ent = null;
        if (t_FEntType.Value == "2")
        {
            ent = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == t_FAppId.Value && t.FEntType.Equals(t_FEntType.Value)).FirstOrDefault();

        }
        else
        {
            ent = dbContext.TC_PrjItem_Ent.Where(t => t.FId == txtFId.Value).FirstOrDefault();
        }
        if (t_FEntType.Value == "2" || t_FEntType.Value == "3" || t_FEntType.Value == "4")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr2", "<script>showTr2();</script>");
        }
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(ent);
            txtFId.Value = ent.FId;
            h_selEntId.Value = ent.QYID;
            hf_oldqyid.Value = ent.QYID;
            
        }
        TC_SGXKZ_PrjInfo sp = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == t_FAppId.Value).FirstOrDefault();
        if(sp!=null)
            t_FPrjItemId.Value = sp.FPrjItemId;

    }
    //显示
    private void showTitle()
    {
        switch (t_FEntType.Value)
        {
            case "2":
                lblTitle.InnerText = "施工总承包商单位";
                break;
            case "3":
                lblTitle.InnerText = "专业承包单位";
                break;
            case "4":
                lblTitle.InnerText = "劳务分包单位";
                break;
            case "5":
                lblTitle.InnerText = "勘察单位";
                break;
            case "6":
                lblTitle.InnerText = "设计单位";
                break;
            case "7":
                lblTitle.InnerText = "监理单位";
                break;
        }


    }
    private void showEmpList()
    {
        if (t_FEntType.Value == "2" || t_FEntType.Value == "3" || t_FEntType.Value == "4")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr2", "<script>showTr2();</script>");
        }
        EgovaDB dbContext = new EgovaDB();
        var v = from t in dbContext.TC_PrjItem_Emp 
                where t.FAppId == t_FAppId.Value && t.FEntId == h_selEntId.Value  && t.FEntType==EConvert.ToInt(t_FEntType.Value)                
                orderby t.FId
                select new
                {
                    t.FHumanName,
                    t.ZCZY,
                    EmpTypeStr = dbContext.CF_Sys_Dic.Where(d => d.FNumber == Convert.ToInt32(t.EmpType)).Select(d => d.FName).FirstOrDefault(),
                    t.ZCRQ,
                    t.ZCBH,
                    t.FId,
                    t.FAppId,
                    t.FEntId,
                    t.FPrjItemId,
                    t.FPrjId,
                    t.FEntType
                };

        dg_List.DataSource = v;
        dg_List.DataBind();
    }
    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        string fId = txtFId.Value;
        string oldId = fId;
        TC_PrjItem_Ent ent = new TC_PrjItem_Ent();
        //修改单位
        if (!string.IsNullOrEmpty(fId))
        {
            ent = dbContext.TC_PrjItem_Ent.Where(t => t.FId == fId).FirstOrDefault();
            //如果选择的单位和之前的单位不同，则删除之前单位所有人员

            if (h_selEntId.Value != ent.QYID)           
            {
                var para = dbContext.TC_PrjItem_Emp.Where(t => t.FEntId == ent.QYID && t.FAppId == t_FAppId.Value && t.FEntType == EConvert.ToInt(t_FEntType.Value));
                foreach(TC_PrjItem_Emp v in para)
                {
                    dbContext.TC_PrjItem_Emp.DeleteOnSubmit(v);
                    dbContext.SubmitChanges();
                }
                //dbContext.TC_PrjItem_Emp.DeleteAllOnSubmit(para);
            }
        }
        //新增单位
        else
        {
            fId = Guid.NewGuid().ToString();
            ent.FId = fId;
            ent.QYID = h_selEntId.Value;
            ent.FAppId = t_FAppId.Value;
            ent.FPrjItemId = t_FPrjItemId.Value;
            ent.FEntType = EConvert.ToInt(t_FEntType.Value);
            ent.FTime = DateTime.Now;
            ent.FCreateTime = DateTime.Now;
            dbContext.TC_PrjItem_Ent.InsertOnSubmit(ent);
        }
        pageTool tool = new pageTool(this.Page);
        //ytb 修改了企业需要为企业ID赋值
        ent.QYID = h_selEntId.Value;
        ent = tool.getPageValue(ent);
        dbContext.SubmitChanges();
        hf_FId.Value = fId;
        txtFId.Value = fId;

        
    }


    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "reloadEmpList();alert('保存成功');window.returnValue='1';", true);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_PrjItem_Emp, tool_Deleting);

        showEmpList();
    }
    //单项工程删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        EgovaDB dbContext = new EgovaDB();
        var para = dbContext.TC_PrjItem_Emp.Where(t => FIdList.ToArray().Contains(t.FId));
        dbContext.TC_PrjItem_Emp.DeleteAllOnSubmit(para);
        showEmpList();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showEmpList();

    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string fEntId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntId"));
            string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjItemId"));
            string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));

            string fentType = EConvert.ToString(t_FEntType.Value);
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('EmpInfo.aspx?FId=" + fId + "&FAppId=" + fAppId + "&FEntId=" + fEntId + "&FPrjItemId=" + fPrjItemId + "&FprjId=" + fPrjId + "&t_FEntType="+fentType+"',900,700);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;

    }

    private void selEnt()
    {
        string selEntId = h_selEntId.Value;
        EgovaDB1 db = new EgovaDB1();

        //如果是监理选择单位返回的是企业资质的主键，通过主键再找到企业编号  modify by psq 20150421
        if (t_FEntType.Value == "7" || t_FEntType.Value == "5" || t_FEntType.Value == "6")
        {
            var vqyzz = db.QY_QYZZXX.Where(t => t.QYZZID == selEntId).FirstOrDefault();
            if (vqyzz != null)
            {
                //绑定企业基本信息
                var v2 = db.QY_JBXX.Where(t => t.QYBM == vqyzz.QYBM).FirstOrDefault();
                if (v2 != null)
                {
                    t_QYID.Value = v2.QYBM;
                    h_selEntId.Value = v2.QYBM;
                    t_FName.Text = v2.QYMC;
                    t_FAddress.Text = v2.QYXXDZ;
                    t_FLegalPerson.Text = v2.FRDB;
                    t_FLinkMan.Text = v2.LXR;
                    t_FMobile.Text = v2.FRDBSJH;
                    t_FTel.Text = v2.LXDH;
                    t_FOrgCode.Text = v2.JGDM;
                }
                //绑定企业资质信息
                t_mZXZZ.Text = vqyzz.ZZLB + vqyzz.ZZMC + vqyzz.ZZDJ;
            }
        }
        else
        {

            var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
            if (v != null)
            {
                t_QYID.Value = v.QYBM;
                t_FName.Text = v.QYMC;
                t_FAddress.Text = v.QYXXDZ;
                t_FLegalPerson.Text = v.FRDB;
                t_FLinkMan.Text = v.LXR;
                t_FMobile.Text = v.FRDBSJH;
                t_FTel.Text = v.LXDH;
                t_FOrgCode.Text = v.JGDM;
            }

            var v1 = db.QY_QYZZXX.Where(t => t.QYBM == selEntId && t.SFZX == 1).FirstOrDefault();
            if (v1 != null)
                t_mZXZZ.Text = v1.ZZLB + v1.ZZMC + v1.ZZDJ;

            //获取增项资质
            var _v2 = db.QY_QYZZXX.Where(t => t.QYBM == selEntId && t.SFZX == 0);
            var _oZXZZ = new StringBuilder();
            foreach (var i in _v2)
            {
                _oZXZZ.Append(i.ZZLB + i.ZZMC + i.ZZDJ + "\r\n");
            }
            t_oZXZZ.Text = _oZXZZ.ToString();
        }

        if (t_FEntType.Value == "2" || t_FEntType.Value == "3" || t_FEntType.Value == "4")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr2", "<script>showTr2();</script>");
        }
        saveInfo();
        //刷新人员
        showEmpList();
    }
    protected void btnAddEnt_Click(object sender, EventArgs e)
    {
        selEnt();
    }

    /// <summary>
    /// 当重新选择企业后，需要删除以前的企业与人员
    /// </summary>
    private void DeltePrjEntAndEmp() {
        EgovaDB dbContext = new EgovaDB();

        var entid = h_selEntId.Value;
        var appId = t_FAppId.Value;
        var prjItemId = t_FPrjItemId.Value;
        var entType = EConvert.ToInt(t_FEntType.Value);


        var para = dbContext.TC_PrjItem_Emp.Where(t => t.FEntId !=entid&& t.FAppId==appId&&t.FEntType==entType);
        dbContext.TC_PrjItem_Emp.DeleteAllOnSubmit(para);
        dbContext.SubmitChanges();
    }
    protected void btntest_click(object sender, EventArgs e)
    {
        string selEntId = h_selEntId.Value;
        Response.Write(selEntId);
    }


}