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
    EgovaDB db = new EgovaDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //主键编号
            txtFId.Value = EConvert.ToString(Request["FId"]);
            //当前app编号
            t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            h_AppId.Value = EConvert.ToString(Session["FAppId"]);
            //企业类型
            t_FEntType.Value = EConvert.ToString(Request["FEntType"]);
            tool = new pageTool(this.Page);
            showTitle();
            showInfo();
            bindEmpList();
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool.ExecuteScript("btnEnable();");
                btnAddEnt.Visible = false;
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
    /// 绑定数据
    /// </summary>
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_PrjItem_Ent entInfo = null;
        var entType = Convert.ToInt32(t_FEntType.Value);
        if (string.IsNullOrEmpty(txtFId.Value))
        {
            h_IsAdd.Value = "1";//新增
        }
        else
        {
            h_IsAdd.Value = "0";//修改
        }
        var bgProInfo = dbContext.TC_SGXKZ_BGPrjInfo.Where(t => t.FAppId == h_AppId.Value).FirstOrDefault();

        if (bgProInfo == null)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('变更办理信息获取失败');window.returnValue='1';", true);
            return;
        }
        //获取工程ID
        h_ProjectItemId.Value = bgProInfo.FPrjItemId;

        //修改数据来源，ly不要修改，从TC_SGXKZ_BGPrjInfo的flinkid中获取 modify by psq 20150501     
        //从变更表中获取历史的fappid
        h_OldAppId.Value = bgProInfo.FLinkId;
        //
        if (!string.IsNullOrEmpty(txtFId.Value))
        {
            entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FId == txtFId.Value).FirstOrDefault();
        }
        else
        {
            if (entType == 2)
            {
                entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_AppId.Value && t.FEntType == 2).FirstOrDefault();
                if (entInfo == null)
                {
                    entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_OldAppId.Value && t.FEntType == 2).FirstOrDefault();
                }
                if (entInfo != null)
                {
                    pageTool tool = new pageTool(this.Page, "t_");
                    tool.fillPageControl(entInfo);
                    h_selEntId.Value = entInfo.QYID;
                    txtFId.Value = entInfo.FId;
                }
            }
        }

        //根据企业类型隐藏或显示部分数据
        if (t_FEntType.Value == "2" || t_FEntType.Value == "3" || t_FEntType.Value == "4")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr1", "<script>showTr1();</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showTr2", "<script>showTr2();</script>");
        }

        //如果企业不为空
        if (entInfo != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(entInfo);
            h_selEntId.Value = entInfo.QYID;
            //保存老的企业编号和企业名称
            h_OldQYID.Value   = entInfo.QYID;
            h_OldQYName.Value = entInfo.FName;
            var v = from t in dbContext.TC_PrjItem_Emp
                    where t.FAppId == h_AppId.Value && t.FEntId == entInfo.QYID && t.FEntType == Convert.ToInt16(t_FEntType.Value)
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

    private void bindEmpList()
    {
        EgovaDB dbContext = new EgovaDB();
        var entType = Convert.ToInt32(t_FEntType.Value);
        TC_PrjItem_Ent entInfo = null;
        //不是新添加的企业
        if (!string.IsNullOrEmpty(txtFId.Value))
        {
            entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FId == txtFId.Value).FirstOrDefault();
        }
        else
        {
            if (entType == 2)
            {
                entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_AppId.Value && t.FEntType == entType).FirstOrDefault();
                if (entInfo == null)
                {
                    entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FAppId == h_OldAppId.Value && t.FEntType == entType).FirstOrDefault();
                }

            }
        }
        //绑定人员信息
        if (entInfo != null)
        {           
            var v = from t in dbContext.TC_PrjItem_Emp
                    where t.FEntId == entInfo.QYID
                    && t.FAppId == h_AppId.Value
                    && t.FEntType == entType
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
    private string getEmpType(int id)
    {
        
        CF_Sys_Dic csd = db.CF_Sys_Dic.Where(t => t.FParentId == 112202 && t.FNumber == id).FirstOrDefault();
        if (csd != null)
        {
            switch (csd.FNumber)
            {
                default:
                    return "项目负责人";
                case 11220201:
                    return "项目负责人";
                case 11220202:
                    return "项目技术负责人";
                case 11220203:
                    return "安全负责人";
                case 11220204:
                    return "施工员";
                case 11220205:
                    return "质量员";
                case 11220206:
                    return "安全员";
                case 11220207:
                    return "材料员";
                case 11220208:
                    return "预算员";
                case 11220209:
                    return "总监理工程师";
                case 11220210:
                    return "专业监理工程师";
                case 11220211:
                    return "监理员";
                case 11220212:
                    return "其他";
                case 11220213:
                    return "建造师";	
            }
        }
        return "";
    }

    /*
    施工许可证变更办理思路整理:
    1、从上次办结业务中导入所有基本信息，包括基本情况、参与企业、参与人员。
    2、企业变更
        2.1、新增企业，增加企业的同时，记录增加企业的日志。
        2.2、退出企业，删除企业，并记录企业退出记录，删除对应参与人员，并记录人员退出日志。
        2.3、审批部分办结时，需要对退出的人员解锁。
    3、人员变更
         3.1、人员新增，添加人员信息，并记录人员新增记录。
         3.2、人员退出，删除人员信息，并记录人员退出记录。
    */


    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        var entType = Convert.ToInt32(t_FEntType.Value);
        var fid = txtFId.Value;  //参与企业主键
        TC_PrjItem_Ent entInfo = new TC_PrjItem_Ent();
        //新增企业的情况
        if (string.IsNullOrEmpty(fid))
        {
            entInfo.FId = Guid.NewGuid().ToString();
            txtFId.Value = entInfo.FId;
            entInfo.FPrjItemId = h_ProjectItemId.Value;
            entInfo.FAppId = h_AppId.Value;
            entInfo.QYID = h_selEntId.Value;
            entInfo.FEntType = entType;
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
            //记录企业的新增记录
            var entity = new TC_SGXKZ_QYBGJG();
            entity.FId = Guid.NewGuid().ToString();
            entity.FAppId = this.h_AppId.Value;
            entity.FPrjItemId = h_ProjectItemId.Value;
            entity.YQLX = lblTitle.InnerText;
            entity.YQMC = t_FName.Text;
            entity.BGTime = DateTime.Now;
            //entity.FLinkId = entInfo.FId;
            entity.FLinkId = entInfo.QYID;
            entity.BGQK = "新增";
            dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);
        }
        else  //修改一个参选企业的情况
        {
            entInfo = dbContext.TC_PrjItem_Ent.Where(t => t.FId == txtFId.Value).FirstOrDefault();                     
            entInfo.FPrjItemId = h_ProjectItemId.Value;
            entInfo.FAppId = h_AppId.Value;
            entInfo.QYID = h_selEntId.Value;
            entInfo.FEntType = entType;
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
            if (entInfo == null)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('获取企业信息失败');window.returnValue='1';", true);
                return;
            }
        }

        //如果企业发生了变更,历史的企业记录退出，新的企业记录新增
        if (!string.IsNullOrEmpty(h_OldQYID.Value) && h_selEntId.Value.Trim() != h_OldQYID.Value.Trim())
        {  
           #region   多余的
            /*
                //删除历史企业
                TC_PrjItem_Ent oldent = dbContext.TC_PrjItem_Ent.Where(t =>t.FAppId == h_AppId.Value && t.QYID == h_OldQYID.Value && t.FEntType == entType).FirstOrDefault();
                dbContext.TC_PrjItem_Ent.DeleteOnSubmit(oldent);
                //添加新的企业
                var newEntInfo = new TC_PrjItem_Ent();
                newEntInfo.FId = Guid.NewGuid().ToString();
                txtFId.Value = newEntInfo.FId;
                newEntInfo.FPrjItemId = h_ProjectItemId.Value;
                newEntInfo.FAppId = h_AppId.Value;
                newEntInfo.QYID = h_selEntId.Value;
                newEntInfo.FEntType = entType;
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
                pageTool tool = new pageTool(this.Page);              
                newEntInfo = tool.getPageValue(newEntInfo);*/
           #endregion
            //记录历史企业退出的记录
                TC_SGXKZ_QYBGJG entity = new TC_SGXKZ_QYBGJG();
                entity.FId = Guid.NewGuid().ToString();
                entity.FAppId = this.h_AppId.Value;
                entity.FPrjItemId = h_ProjectItemId.Value;
                entity.YQLX = lblTitle.InnerText;
                entity.YQMC = h_OldQYName.Value;
                entity.BGTime = DateTime.Now;
                //entity.FLinkId = entInfo.FId;
                entity.FLinkId = h_OldQYID.Value;
                entity.BGQK = "退出";
                dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);
                //记录新的企业新增的记录
                entity = new TC_SGXKZ_QYBGJG();
                entity.FId = Guid.NewGuid().ToString();
                entity.FAppId = this.h_AppId.Value;
                entity.FPrjItemId = h_ProjectItemId.Value;
                entity.YQLX = lblTitle.InnerText;
                entity.YQMC = t_FName.Text;
                entity.BGTime = DateTime.Now;
                entity.FLinkId = h_selEntId.Value;
                entity.BGQK = "新增";
                dbContext.TC_SGXKZ_QYBGJG.InsertOnSubmit(entity);

                //历史企业的人员全部退出，历史企业的筛选条件包括appid,fentid,fenttype,并删除历史企业人员                
                var oldEmpList = dbContext.TC_PrjItem_Emp.Where(t => t.FEntId == h_OldQYID.Value && t.FAppId == h_AppId.Value && t.FPrjItemId == h_ProjectItemId.Value && t.FEntType == Convert.ToInt16(t_FEntType.Value)).ToList();                
                if (oldEmpList != null && oldEmpList.Count > 0)
                {
                    oldEmpList.ForEach(q =>
                    {
                        //删除历史企业人员
                        TC_PrjItem_Emp temp = dbContext.TC_PrjItem_Emp.Where(t => t.FId == q.FId).FirstOrDefault();
                        dbContext.TC_PrjItem_Emp.DeleteOnSubmit(temp);
                        //记录退出日志
                        TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();
                        sr.FId = Guid.NewGuid().ToString();
                        sr.FAppId = h_AppId.Value;
                        sr.FPrjItemId = h_ProjectItemId.Value;
                        sr.RYLX = getEmpType(Convert.ToInt32(q.EmpType));
                        sr.XM = q.FHumanName;
                        sr.ZSBH = q.ZSBH;
                        sr.QYMC = q.FEntName;
                        sr.BGQK = "退出";
                        sr.BGTime = DateTime.Now;
                        sr.FLinkId = q.FEmpId;
                        dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
                    });
                }               

            }   
            //else if (h_OldQYID.Value == h_selEntId.Value)
            //{             

            //    pageTool tool = new pageTool(this.Page);
            //    entInfo = tool.getPageValue(entInfo);
            //    entInfo.QYID = h_selEntId.Value;             
            //    var addEmpList = dbContext.TC_PrjItem_Emp.Where(t => t.FEntId == entInfo.QYID && t.FAppId == entInfo.FAppId && t.FEntType==Convert.ToInt16(t_FEntType.Value));
            //    dbContext.TC_PrjItem_Emp.DeleteAllOnSubmit(addEmpList);
            //    var addEmpList1 = dbContext.TC_SGXKZ_RYBGJG.Where(t => t.FLinkId == entInfo.FId && t.BGQK == "新增");
            //    dbContext.TC_SGXKZ_RYBGJG.DeleteAllOnSubmit(addEmpList1);
            //}       
        dbContext.SubmitChanges();       
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
        showInfo();
    }

    //删除
    //ly 此问题我已经按照新的思路调整，不要调整
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        EgovaDB dbContext = new EgovaDB();

        var empList = dbContext.TC_PrjItem_Emp.Where(t => FIdList.ToArray().Contains(t.FId)).ToList();

        if (empList != null && empList.Count > 0)
        {
            empList.ForEach(q =>
            {
                //删除人员
                dbContext.TC_PrjItem_Emp.DeleteOnSubmit(q);
                //记录人员删除日志
                TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();
                sr.FId = Guid.NewGuid().ToString();
                sr.FAppId = h_AppId.Value;
                sr.FPrjItemId = h_ProjectItemId.Value;
                sr.RYLX = getEmpType(Convert.ToInt32(q.EmpType));
                sr.XM = q.FHumanName;
                sr.ZSBH = q.ZSBH;
                sr.QYMC = q.FEntName;
                sr.BGQK = "退出";
                sr.FLinkId = q.FEmpId;
                sr.BGTime = DateTime.Now;
                dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
                //if (q.FAppId == h_OldAppId.Value)
                //{
                //    TC_SGXKZ_RYBGJG sr = new TC_SGXKZ_RYBGJG();
                //    sr.FId = Guid.NewGuid().ToString();
                //    sr.FAppId = h_AppId.Value;
                //    sr.FPrjItemId = h_ProjectItemId.Value;
                //    sr.RYLX = getEmpType(q.EmpType.ToString());
                //    sr.XM = q.FHumanName;
                //    sr.ZSBH = q.ZSBH;
                //    sr.QYMC = q.FEntName;
                //    sr.BGQK = "退出";
                //    sr.FLinkId = txtFId.Value;
                //    sr.BGTime = DateTime.Now;
                //    dbContext.TC_SGXKZ_RYBGJG.InsertOnSubmit(sr);
                //}
                //else
                //{
                //    var info = dbContext.TC_SGXKZ_RYBGJG.Where(m => m.FAppId == q.FAppId && m.FLinkId == txtFId.Value && m.XM == q.FHumanName && m.BGQK == "增加");
                //    dbContext.TC_SGXKZ_RYBGJG.DeleteAllOnSubmit(info);
                //}
            });
            dbContext.SubmitChanges();
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('删除成功');window.returnValue='1';", true);
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
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

        //如果是监理选择单位返回的是企业资质的主键，通过主键再找到企业编号  modify by psq 20150421
        if (t_FEntType.Value == "7")
        {
            var vqyzz = db.QY_QYZZXX.Where(t => t.QYZZID == selEntId).FirstOrDefault();
            if (vqyzz != null)
            {
                //绑定企业基本信息
                var v2 = db.QY_JBXX.Where(t => t.QYBM == vqyzz.QYBM).FirstOrDefault();
                if (v2 != null)
                {
                    t_QYID.Value = v2.QYBM;
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
                ClientScript.RegisterStartupScript(this.GetType(), "showTr2", "<script>showTr2();</script>");
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

        this.saveInfo();
        this.bindEmpList();
   
    }
    protected void btnAddEnt_Click(object sender, EventArgs e)
    {
        selEnt();
    }




}