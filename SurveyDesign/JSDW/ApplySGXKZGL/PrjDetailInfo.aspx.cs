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

public partial class JSDW_APPLYSGXKZGL_PrjDetailInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //应用编号
            t_FAppId.Value = EConvert.ToString(Session["FAppId"]);

            //编号
            txtFId.Value = EConvert.ToString(Request["FId"]);

            showInfo();

            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {

                tool.ExecuteScript("btnEnable();");
            }
        }
        else
        {
            
            
        }
    }
    //显示
    private void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_SGXKZ_PrjDetail ent = null;
        ent = dbContext.TC_SGXKZ_PrjDetail.Where(t => t.FId == txtFId.Value).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(ent);
            txtFId.Value = ent.FId;
            t_FPrjItemId.Value = ent.PrjItemId;
        }


    }

    //保存
    private void saveInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        string fId = txtFId.Value;
        string oldId = fId;
        TC_SGXKZ_PrjDetail ent = new TC_SGXKZ_PrjDetail();
        if (!string.IsNullOrEmpty(fId))
        {
            ent = dbContext.TC_SGXKZ_PrjDetail.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            TC_SGXKZ_PrjInfo Prj = new TC_SGXKZ_PrjInfo();
            string appid = t_FAppId.Value;
            if (!string.IsNullOrEmpty(appid))
            {
                Prj = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == appid).FirstOrDefault();
                //施工许可证ID
                t_SgxkzId.Value    = Prj.FId;
                t_FPrjId.Value     = Prj.PrjId;
                t_FPrjItemId.Value = Prj.FPrjItemId;
            }
            fId = Guid.NewGuid().ToString();
            ent.FId = fId;
            ent.JSDW = Prj.JSDW;
            ent.AddressDept = Prj.JSDWAddressDept;
            ent.FAppId    = t_FAppId.Value;
            ent.PrjId     = t_FPrjId.Value;
            ent.PrjItemId = t_FPrjItemId.Value;
            ent.SgxkzInfoID = t_SgxkzId.Value;
            ent.DetailName = t_DetailName.Text ;
            ent.Scale   = t_Scale.Text;
            ent.UpScale = t_UpScale.Text;
            ent.DoScale = t_DoScale.Text;
            ent.AbLayerNum = t_AbLayerNum.Text;
            ent.UnLayerNum = t_UnLayerNum.Text;
            ent.ReMark = t_Remark.Text;
            dbContext.TC_SGXKZ_PrjDetail.InsertOnSubmit(ent);
        }
        pageTool tool = new pageTool(this.Page);

        ent = tool.getPageValue(ent);
        dbContext.SubmitChanges();
        hf_FId.Value = fId;
        txtFId.Value = fId;

        
    }


    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    }


    protected void btnReload_Click(object sender, EventArgs e)
    {

    }





}