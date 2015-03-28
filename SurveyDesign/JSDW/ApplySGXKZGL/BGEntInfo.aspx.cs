using System;
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

public partial class JSDW_ApplySGXKZGL_EntInfoForBG : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    pageTool tool;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFId.Value = EConvert.ToString(Request["FId"]);
            t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            h_AppId.Value = EConvert.ToString(Session["FAppId"]);
            t_FEntType.Value = EConvert.ToString(Request["FEntType"]);
            tool = new pageTool(this.Page);
            showTitle();
            showInfo();
            showEmpList();

            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }

    /// <summary>
    /// 显示标题
    /// </summary>
    private void showTitle()
    {
        switch (t_FEntType.Value)
        {
            case "2":
                lblTitle.InnerText = "施工总承包单位";
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
    /// <summary>
    /// 绑定企业信息
    /// </summary>
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Ent ent = null;
        if (string.IsNullOrEmpty(txtFId.Value))
        {
            h_IsAdd.Value = "1";//新增
        }
        else
        {
            h_IsAdd.Value = "0";//修改
        }

        TC_SGXKZ_BGPrjInfo sp = dbContext.TC_SGXKZ_BGPrjInfo.Where(t => t.FAppId == h_AppId.Value).FirstOrDefault();
        t_FPrjItemId.Value = sp.FPrjItemId;


        var entList = dbContext.TC_PrjItem_Ent.Where(t => t.FPrjItemId == t_FPrjItemId.Value && t.FEntType.Equals(t_FEntType.Value)).OrderByDescending(q => q.FCreateTime).ToList();
        if (entList == null || entList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('获取工程信息失败');window.returnValue='1';", true);
            return;
        }
        if (t_FEntType.Value == "2") //如果是施工总承包
        {
            if (entList.Count == 1)
            {
                ent = entList[0];
                h_OldAppId.Value = entList[0].FAppId;
            }
            else if (entList.Count > 1)
            {
                ent = entList[0];
                h_OldAppId.Value = entList[1].FAppId;
            }
        }
        else
        {
            var entList1 = entList.Where(q => q.FAppId == h_AppId.Value).ToList();
            if (entList1 == null || entList1.Count == 0)
            {
                h_OldAppId.Value = entList[0].FAppId;
            }
            else
            {
                var appIds = new List<string>();
                entList.ForEach(q =>
                {
                    if (!appIds.Contains(q.FAppId))
                    {
                        appIds.Add(q.FAppId);
                    }
                });
                h_OldAppId.Value = appIds[1];
            }
            if (!string.IsNullOrEmpty(txtFId.Value))
            {
                ent = dbContext.TC_PrjItem_Ent.Where(t => t.FId == txtFId.Value).FirstOrDefault();
            }
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
            h_selEntId.Value = ent.QYID;
            h_OldQYID.Value = ent.QYID;
            h_OldQYName.Value = ent.FName;
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
    /// <summary>
    /// 显示绑定人员
    /// </summary>
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
                where (t.FAppId == h_AppId.Value || t.FAppId == h_OldAppId.Value) && t.FEntId == h_selEntId.Value
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
    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        var entType = Convert.ToInt32(t_FEntType.Value);


        if (entType == 2)//施工总承包
        {
            var shigongEnt = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_AppId.Value && t.FEntType == entType).OrderByDescending(q => q.FCreateTime).FirstOrDefault();
            var oldEnt = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_OldAppId.Value && t.FEntType == entType).FirstOrDefault();
            //如果无当前这个施工企业，则证明没有变更过
            if (shigongEnt == null)
            {
                if (h_selEntId.Value != h_OldQYID.Value)//企业变更了
                {
                    TC_SGXKZ_QYBGJG entity = new TC_SGXKZ_QYBGJG();
                    entity.FId = Guid.NewGuid().ToString();
                    entity.FAppId = this.h_AppId.Value;
                    entity.FPrjItemId = t_FPrjItemId.Value;
                    entity.YQLX = lblTitle.InnerText;
                    entity.YQMC = oldEnt.FName;
                    entity.BGTime = DateTime.Now;
                    entity.BGQK = "退出";
                    dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);

                    entity = new TC_SGXKZ_QYBGJG();
                    entity.FId = Guid.NewGuid().ToString();
                    entity.FAppId = this.h_AppId.Value;
                    entity.FPrjItemId = t_FPrjItemId.Value;
                    entity.YQLX = lblTitle.InnerText;
                    entity.YQMC = t_FName.Text;
                    entity.BGTime = DateTime.Now;
                    entity.BGQK = "新增";
                    dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);

                    shigongEnt = new TC_PrjItem_Ent();
                    shigongEnt.FId = Guid.NewGuid().ToString();
                    shigongEnt.QYID = h_selEntId.Value;
                    shigongEnt.FAppId = h_AppId.Value;
                    shigongEnt.FPrjItemId = t_FPrjItemId.Value;
                    shigongEnt.FEntType = EConvert.ToInt(t_FEntType.Value);
                    shigongEnt.FTime = DateTime.Now;
                    shigongEnt.FCreateTime = DateTime.Now;
                    pageTool tool = new pageTool(this.Page);
                    shigongEnt = tool.getPageValue(shigongEnt);
                    dbContext.TC_PrjItem_Ent.InsertOnSubmit(shigongEnt);
                    dbContext.SubmitChanges();
                    //当前人员全部退出
                    //..
                }
                else
                {
                    //以前的企业 也要发生信息变更
                    pageTool tool = new pageTool(this.Page);
                    oldEnt = tool.getPageValue(oldEnt);
                }
            }
            //如果当前有这个施工企业，则证明已变更过
            else
            {
                if (h_selEntId.Value != h_OldQYID.Value)//企业变更了
                {
                    //已退出的企业不要管，把以前新增的那条信息更新了。
                    var yibiangengEnt = dbContext.TC_SGXKZ_QYBGJG.Where(q => q.FAppId == h_AppId.Value && q.YQLX == lblTitle.InnerText && q.YQMC == h_OldQYName.Value).FirstOrDefault();
                    if (yibiangengEnt != null)
                    {
                        yibiangengEnt.YQMC = t_FName.Text;
                        yibiangengEnt.BGTime = DateTime.Now;
                    }
                    //当前人员全部直接删除
                    //..
                }
                else
                {
                    pageTool tool = new pageTool(this.Page);
                    shigongEnt = tool.getPageValue(shigongEnt);
                }

            }
            dbContext.SubmitChanges();
        }
        else
        {
            if (h_IsAdd.Value == "1")
            {
                //新增
                var entity = new TC_SGXKZ_QYBGJG();
                entity.FId = Guid.NewGuid().ToString();
                entity.FAppId = this.h_AppId.Value;
                entity.FPrjItemId = t_FPrjItemId.Value;
                entity.YQLX = lblTitle.InnerText;
                entity.YQMC = t_FName.Text;
                entity.BGTime = DateTime.Now;
                entity.BGQK = "新增";
                dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);
            }
            else
            {
                var dangqianEnt = dbContext.TC_PrjItem_Ent.Where(t => t.FId == txtFId.Value).FirstOrDefault();
                if (h_selEntId.Value != h_OldQYID.Value)//企业变更了
                {
                    //判断是否是修改上一次的数据
                    var shangyiciEnt = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_AppId.Value && t.FId == txtFId.Value).FirstOrDefault();
                    if (shangyiciEnt == null)
                    {
                        //是修改的上一次:上一次的企业退出，新增一个这次的企业
                        TC_SGXKZ_QYBGJG entity = new TC_SGXKZ_QYBGJG();
                        entity.FId = Guid.NewGuid().ToString();
                        entity.FAppId = this.h_AppId.Value;
                        entity.FPrjItemId = t_FPrjItemId.Value;
                        entity.YQLX = lblTitle.InnerText;
                        entity.YQMC = dangqianEnt.FName;
                        entity.BGTime = DateTime.Now;
                        entity.BGQK = "退出";
                        dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);

                        entity = new TC_SGXKZ_QYBGJG();
                        entity.FId = Guid.NewGuid().ToString();
                        entity.FAppId = this.h_AppId.Value;
                        entity.FPrjItemId = t_FPrjItemId.Value;
                        entity.YQLX = lblTitle.InnerText;
                        entity.YQMC = t_FName.Text;
                        entity.BGTime = DateTime.Now;
                        entity.BGQK = "新增";
                        dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);

                        shangyiciEnt = new TC_PrjItem_Ent();
                        shangyiciEnt.FId = Guid.NewGuid().ToString();
                        shangyiciEnt.QYID = h_selEntId.Value;
                        shangyiciEnt.FAppId = h_AppId.Value;
                        shangyiciEnt.FPrjItemId = t_FPrjItemId.Value;
                        shangyiciEnt.FEntType = EConvert.ToInt(t_FEntType.Value);
                        shangyiciEnt.FTime = DateTime.Now;
                        shangyiciEnt.FCreateTime = DateTime.Now;
                        pageTool tool = new pageTool(this.Page);
                        shangyiciEnt = tool.getPageValue(shangyiciEnt);
                        dbContext.TC_PrjItem_Ent.InsertOnSubmit(shangyiciEnt);

                        //当前人员全部退出
                        //..
                    }
                    else
                    {
                        //不是修改上一次,则上一次的退出不管

                        pageTool tool = new pageTool(this.Page);
                        shangyiciEnt = tool.getPageValue(shangyiciEnt);

                        var yibiangengEnt = dbContext.TC_SGXKZ_QYBGJG.Where(q => q.FAppId == h_AppId.Value && q.YQLX == lblTitle.InnerText && q.YQMC == h_OldQYName.Value).FirstOrDefault();
                        if (yibiangengEnt != null)
                        {
                            yibiangengEnt.YQMC = t_FName.Text;
                            yibiangengEnt.BGTime = DateTime.Now;
                        }
                    }
                }
                else
                {
                    pageTool tool = new pageTool(this.Page);
                    dangqianEnt = tool.getPageValue(dangqianEnt);
                }
            }
            dbContext.SubmitChanges();
        }

        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        showEmpList();
    }

    private void DelEmp()
    {
        EgovaDB dbContext = new EgovaDB();
        var para = dbContext.TC_PrjItem_Emp.Where(t => t.FEntId == h_OldQYID.Value);
        dbContext.TC_PrjItem_Emp.DeleteAllOnSubmit(para);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_PrjItem_Emp, tool_Deleting);
        showEmpList();
    }

    //删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        EgovaDB dbContext = new EgovaDB();
        if (dbContext != null)
        {
            var para = dbContext.TC_PrjItem_Emp.Where(t => FIdList.ToArray().Contains(t.FId));
            foreach (TC_PrjItem_Emp pe in para)
            {
                TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();
                sr.FId = Guid.NewGuid().ToString();
                sr.FAppId = t_FAppId.Value;
                sr.FPrjItemId = t_FPrjItemId.Value;
                sr.RYLX = getEmpType(pe.EmpType.ToString());
                sr.XM = pe.FHumanName;
                sr.ZSBH = pe.ZSBH;
                sr.QYMC = pe.FEntName;
                sr.BGQK = "退出";
                sr.BGTime = DateTime.Now;
                dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
                dbContext.SubmitChanges();
            }
        }
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
            t_QYID.Value = v.QYBM;
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

        if (t_FEntType.Value == "2" || t_FEntType.Value == "3" || t_FEntType.Value == "4")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr2", "<script>showTr2();</script>");
        }

    }
    protected void btnAddEnt_Click(object sender, EventArgs e)
    {
        selEnt();
    }




}