using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
public partial class JSDW_QMain_Baseinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "return CheckInfo();");
            govd_FRegistDeptId.FNumber = ComFunction.GetDefaultCityDept();
            //govd_FRegistDeptId.isEnbel(1);
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        ProjectDB db = new ProjectDB();
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == CurrentEmpUser.EmpId).FirstOrDefault();
        if (Ent != null)
        {
            tool.fillPageControl(Ent);
            govd_FUpDeptId.FNumber = Ent.FUpDeptId.ToString();
            if (Ent.FRegistDeptId.ToString() != "")
                govd_FRegistDeptId.FNumber = Ent.FRegistDeptId.ToString();

        }
        else
        {
            CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == CurrentEmpUser.EmpId).FirstOrDefault();
            if (user != null)
            {
                t_FName.Text = user.FCompany;
                govd_FUpDeptId.FNumber = user.FManageDeptId.ToString();
                t_FJuridcialCode.Text = user.FJuridcialCode;
                t_FLinkMan.Text = user.FLinkMan;
                t_FTel.Text = user.FTel;
            }
        }

        //CF_Ent_Leader T = db.CF_Ent_Leader.Where(d => d.FPersonTypeId == 7021 && d.FBaseInfoId == CurrentEmpUser.EmpId).FirstOrDefault();

        //if (T != null)
        //{
        //    tool = new pageTool(this.Page, "n_");
        //    tool.fillPageControl(T);
        //}
    }

    //保存
    private void saveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        ProjectDB db = new ProjectDB();
        bool IsInsert = false;
        CF_Ent_BaseInfo Ent = db.CF_Ent_BaseInfo.Where(t => t.FId == CurrentEmpUser.EmpId).FirstOrDefault();
        if (Ent == null)
        {
            IsInsert = true;
            Ent = new CF_Ent_BaseInfo();
            Ent.FId = CurrentEmpUser.EmpId;
            Ent.FIsDeleted = false;
            Ent.FCreateTime = dTime;
            Ent.FSystemId = EConvert.ToInt(CurrentEntUser.SystemId);
        }
        Ent = tool.getPageValue(Ent);
        Ent.FTime = dTime;
        Ent.FUpDeptId = EConvert.ToInt(govd_FUpDeptId.FNumber);
        Ent.FRegistDeptId = EConvert.ToInt(govd_FRegistDeptId.FNumber);

        if (IsInsert)
            db.CF_Ent_BaseInfo.InsertOnSubmit(Ent);

        //更新CF_Sys_User表中的FCompany
        CF_Sys_User User = db.CF_Sys_User.Where(t => t.FBaseInfoId == CurrentEmpUser.EmpId).FirstOrDefault();
        if (User != null)
        {
            User.FCompany = t_FName.Text;
            User.FLinkMan = t_FLinkMan.Text;
            User.FTel = t_FTel.Text;
            User.FManageDeptId = EConvert.ToInt(govd_FUpDeptId.FNumber);
            User.FJuridcialCode = t_FJuridcialCode.Text;
        }
        SaveLeader();
        db.SubmitChanges();
        tool.showMessage("保存成功");
    }

    private void SaveLeader()
    {
        ProjectDB qdb = new ProjectDB();
    

        string BaseId = CurrentEmpUser.EmpId;
        pageTool tool = new pageTool(this.Page, "n_");





        //DateTime dTime = DateTime.Now;
        //SaveOptionEnum so = SaveOptionEnum.Insert;

        //CF_Ent_Leader T = qdb.CF_Ent_Leader.Where(d => d.FPersonTypeId == 7021 && d.FBaseInfoId == BaseId).FirstOrDefault();

        //if (T == null)
        //{
        //    T = new CF_Ent_Leader();
        //}
        //else
        //{

        //    so = SaveOptionEnum.Update;
        //}
        //if (T != null)
        //{

        //    T = tool.getPageValue(T);



        //    if (so == SaveOptionEnum.Insert)
        //    {
        //        string FId = Guid.NewGuid().ToString();
        //        T.FPersonTypeId = 7021;
        //        T.FBaseInfoId = BaseId;
        //        T.FIsDeleted = false;
        //        T.FTime = dTime;
        //        T.FCreateTime = dTime;
        //        T.FId = FId;

        //        qdb.CF_Ent_Leader.InsertOnSubmit(T);

        //    }
        //}
        //qdb.SubmitChanges();

    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
