using Approve.RuleCenter;
using EgovaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;

public partial class JSDW_ApplySGXKZGL_BGShiGongEntInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            h_AppId.Value = EConvert.ToString(Session["FAppId"]);
            bindData();
            bindEmpList();
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("btnEnable();");
            }
        }
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    private void bindData()
    {
        ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
        EgovaDB dbContext = new EgovaDB();
        var bgProInfo = dbContext.TC_SGXKZ_BGPrjInfo.Where(t => t.FAppId == h_AppId.Value).FirstOrDefault();
        if (bgProInfo == null)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('变更办理信息获取失败');window.returnValue='1';", true);
            return;
        }
        h_ProjectItemId.Value = bgProInfo.FPrjItemId;

        var oldAppIds = dbContext.CF_App_List.Where(t => t.FLinkId == h_ProjectItemId.Value).OrderByDescending(q => q.FCreateTime).Select(q => q.FId).ToList();

        if (oldAppIds == null || oldAppIds.Count < 2)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('业务信息数据存在问题');window.returnValue='1';", true);
            return;
        }
        h_OldAppId.Value = oldAppIds[1];

        //取这次
        var entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_AppId.Value && t.FEntType == 2).FirstOrDefault();
        if (entInfo == null)
        {
            entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_OldAppId.Value && t.FEntType == 2).FirstOrDefault();
        }
        if (entInfo != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(entInfo);
            t_QYID.Value = entInfo.QYID;  //原来为赋值到 h_selEntId 中
            h_Id.Value = entInfo.FId;    
        }

    }
    private void bindEmpList()
    {
        EgovaDB dbContext = new EgovaDB();
        //取这次
        var entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_AppId.Value && t.FEntType == 2).FirstOrDefault();
        if (entInfo == null)
        {
            entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_OldAppId.Value && t.FEntType == 2).FirstOrDefault();
        }
        if (entInfo != null)
        {
            var v = from t in dbContext.TC_PrjItem_Emp
                    where t.FLinkId == entInfo.FId
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
                        t.FPrjId
                    };
            dg_List.DataSource = v;
            dg_List.DataBind();
        }


    }
    private string getEmpType(string id)
    {
        switch (id)
        {
            default:
                return "项目负责人";
            case "1":
                return "项目负责人";
            case "2":
                return "项目技术负责人";
            case "3":
                return "安全负责人";
            case "4":
                return "施工员";
            case "5":
                return "质量员";
            case "6":
                return "安全员";
            case "7":
                return "材料员";
            case "8":
                return "预算员";
            case "9":
                return "总监理工程师";
            case "10":
                return "专业监理工程师";
            case "11":
                return "监理员";
            case "12":
                return "其他";
        }
    }
    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        var id = h_Id.Value;
        TC_PrjItem_Ent entInfo = new TC_PrjItem_Ent();
        if (string.IsNullOrEmpty(id))
        {
            entInfo.FId = Guid.NewGuid().ToString();
            h_Id.Value = entInfo.FId;
            entInfo.FPrjItemId = h_ProjectItemId.Value;
            entInfo.FAppId = h_AppId.Value;
            entInfo.QYID = h_selEntId.Value;
            entInfo.FEntType = 2;
            entInfo.FName = t_FName.Text;
            entInfo.FOrgCode = t_FOrgCode.Text;
            entInfo.FAddress = t_FAddress.Text;
            entInfo.FLegalPerson = t_FLegalPerson.Text;
            entInfo.FTel = t_FTel.Text;
            entInfo.FLinkMan = t_FLinkMan.Text;
            entInfo.FMobile = t_FMobile.Text;
            entInfo.mZXZZ = t_mZXZZ.Text;
            entInfo.oZXZZ = t_oZXZZ.Text;
            entInfo.Remark = t_Remark.Text;
            entInfo.FTime = DateTime.Now;
            entInfo.FCreateTime = DateTime.Now;
            dbContext.TC_PrjItem_Ent.InsertOnSubmit(entInfo);

            var entity = new TC_SGXKZ_QYBGJG();
            entity.FId = Guid.NewGuid().ToString();
            entity.FAppId = this.h_AppId.Value;
            entity.FPrjItemId = h_ProjectItemId.Value;
            entity.YQLX = lblTitle.InnerText;
            entity.YQMC = t_FName.Text;
            entity.BGTime = DateTime.Now;
            entity.FLinkId = entInfo.FId;
            entity.BGQK = "新增";
            dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);
        }
        else
        {
            entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FId == h_Id.Value).FirstOrDefault();
            if (entInfo == null)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('获取企业信息失败');window.returnValue='1';", true);
                return;
            }
        }
        //如果企业发生了变更
        if (!string.IsNullOrEmpty(t_QYID.Value) && h_selEntId.Value.Trim() != t_QYID.Value.Trim())
        {
            #region 新增一个企业 | 增加一个增加企业 | 把以前企业退出 | 人员全部退出

            if (entInfo.FAppId == h_OldAppId.Value)
            {
                var newEntInfo = new TC_PrjItem_Ent();
                newEntInfo.FId = Guid.NewGuid().ToString();
                h_Id.Value = newEntInfo.FId;
                newEntInfo.FPrjItemId = h_ProjectItemId.Value;
                newEntInfo.FAppId = h_AppId.Value;
                newEntInfo.QYID = h_selEntId.Value;
                newEntInfo.FEntType = 2;
                newEntInfo.FName = t_FName.Text;
                newEntInfo.FOrgCode = t_FOrgCode.Text;
                newEntInfo.FAddress = t_FAddress.Text;
                newEntInfo.FLegalPerson = t_FLegalPerson.Text;
                newEntInfo.FTel = t_FTel.Text;
                newEntInfo.FLinkMan = t_FLinkMan.Text;
                newEntInfo.FMobile = t_FMobile.Text;
                newEntInfo.mZXZZ = t_mZXZZ.Text;
                newEntInfo.oZXZZ = t_oZXZZ.Text;
                newEntInfo.Remark = t_Remark.Text;
                newEntInfo.FTime = DateTime.Now;
                newEntInfo.FCreateTime = DateTime.Now;
                dbContext.TC_PrjItem_Ent.InsertOnSubmit(newEntInfo);


                TC_SGXKZ_QYBGJG entity = new TC_SGXKZ_QYBGJG();
                entity.FId = Guid.NewGuid().ToString();
                entity.FAppId = this.h_AppId.Value;
                entity.FPrjItemId = h_ProjectItemId.Value;
                entity.YQLX = lblTitle.InnerText;
                entity.YQMC = entInfo.FName;
                entity.BGTime = DateTime.Now;
                entity.FLinkId = entInfo.FId;
                entity.BGQK = "退出";
                dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);

                entity = new TC_SGXKZ_QYBGJG();
                entity.FId = Guid.NewGuid().ToString();
                entity.FAppId = this.h_AppId.Value;
                entity.FPrjItemId = h_ProjectItemId.Value;
                entity.YQLX = lblTitle.InnerText;
                entity.YQMC = t_FName.Text;
                entity.BGTime = DateTime.Now;
                entity.FLinkId = newEntInfo.FId;
                entity.BGQK = "新增";
                dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);


                //当前人员全部退出
                var oldEmpList = dbContext.TC_PrjItem_Emp.Where(t => t.FLinkId == entInfo.FId).ToList();
                if (oldEmpList != null && oldEmpList.Count > 0)
                {
                    oldEmpList.ForEach(q =>
                    {
                        TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();
                        sr.FId = Guid.NewGuid().ToString();
                        sr.FAppId = h_AppId.Value;
                        sr.FPrjItemId = h_ProjectItemId.Value;
                        sr.RYLX = getEmpType(sr.RYLX.ToString());
                        sr.XM = q.FHumanName;
                        sr.ZSBH = q.ZSBH;
                        sr.QYMC = q.FEntName;
                        sr.BGQK = "退出";
                        sr.BGTime = DateTime.Now;

                        //sr.FLinkId = q.FLinkId;
                        sr.FLinkId = q.FEmpId; ;
                        dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
                    });
                }

            }
            #endregion
            #region 更新当前企业 | 更新增加企业 | 当前新增人员全部删除
            else if (entInfo.FAppId == h_AppId.Value)
            {
                pageTool tool = new pageTool(this.Page);
                entInfo = tool.getPageValue(entInfo);
                entInfo.QYID = h_selEntId.Value;

                var bgEntInfo = dbContext.TC_SGXKZ_QYBGJG.Where(t => t.FLinkId == entInfo.FId && t.BGQK == "新增").FirstOrDefault();
                bgEntInfo.YQLX = lblTitle.InnerText;
                bgEntInfo.YQMC = t_FName.Text;

                var addEmpList = dbContext.TC_PrjItem_Emp.Where(t => t.FLinkId == entInfo.FId);
                dbContext.TC_PrjItem_Emp.DeleteAllOnSubmit(addEmpList);


                //是否有问题？如何解锁的问题
                //var addEmpList1 = dbContext.TC_SGXKZ_RYBGJG.Where(t => t.FLinkId == entInfo.FId && t.BGQK == "新增");
                //dbContext.TC_SGXKZ_RYBGJG.DeleteAllOnSubmit(addEmpList1);
            }
            #endregion
        }
        else
        {
            pageTool tool = new pageTool(this.Page);
            entInfo = tool.getPageValue(entInfo);
            entInfo.QYID = h_selEntId.Value;
        }
        dbContext.SubmitChanges();
        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    }

    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
        bindEmpList();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_PrjItem_Emp, tool_Deleting);
        bindEmpList();
    }

    //删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        EgovaDB dbContext = new EgovaDB();

        var empList = dbContext.TC_PrjItem_Emp.Where(t => FIdList.ToArray().Contains(t.FId)).ToList();

        if (empList != null && empList.Count > 0)
        {
            empList.ForEach(q =>
            {
                dbContext.TC_PrjItem_Emp.DeleteOnSubmit(q);
                if (q.FAppId == h_OldAppId.Value)
                {
                    TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();
                    sr.FId = Guid.NewGuid().ToString();
                    sr.FAppId = h_AppId.Value;
                    sr.FPrjItemId = h_ProjectItemId.Value;
                    sr.RYLX = getEmpType(q.EmpType.ToString());
                    sr.XM = q.FHumanName;
                    sr.ZSBH = q.ZSBH;
                    sr.QYMC = q.FEntName;
                    sr.BGQK = "退出";
                    sr.FLinkId = q.FEmpId;  //by zyd 5.29
                    sr.BGTime = DateTime.Now;
                    dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
                }
                else
                {
                    var info = dbContext.TC_SGXKZ_RYBGJG.Where(m => m.FAppId == q.FAppId && m.FLinkId == h_Id.Value && m.XM == q.FHumanName && m.BGQK == "增加");
                    dbContext.TC_SGXKZ_RYBGJG.DeleteAllOnSubmit(info);
                }
            });
            dbContext.SubmitChanges();
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('删除成功');window.returnValue='1';", true);
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        bindEmpList();
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
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('BGEmpInfo.aspx?FId=" + fId + "&FAppId=" + fAppId + "&FEntId=" + fEntId + "&FPrjItemId=" + fPrjItemId + "&FprjId=" + fPrjId + "',900,700);\">" + e.Item.Cells[2].Text + "</a>";
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
        var v = db.QY_JBXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v != null)
        {
            t_FName.Text = v.QYMC;
            t_FAddress.Text = v.QYXXDZ;
            t_FLegalPerson.Text = v.FRDB;
            t_FLinkMan.Text = v.LXR;
            t_FMobile.Text = v.FRDBSJH;
            t_FTel.Text = v.LXDH;
            t_FOrgCode.Text = v.JGDM;
        }


        var v1 = db.QY_QYZZXX.Where(t => t.QYBM == selEntId).FirstOrDefault();
        if (v1 != null)
            t_mZXZZ.Text = v1.ZZLB + v1.ZZMC + v1.ZZDJ;

        ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");

    }
    protected void btnAddEnt_Click(object sender, EventArgs e)
    {
        selEnt();
    }

}