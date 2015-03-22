using EgovaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_project_EmpLockInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack){
            //锝到URL传入的人员信息ID
            var idCard = EConvert.ToString(Request.QueryString["idCard"]);
            //调用信息加载页面
            ShowLockEmpInfo(idCard);
        }


    }


    private void ShowLockEmpInfo(string idCard)
    {
        //MODIFY:YTB 查询该专家锁定信息
        EgovaDB db = new EgovaDB();
        //人员选择列表中锁定详情字段：如果有人员被锁定后，锁定详情字段应显示【查看】按钮，点击可查看此人被锁定的项目详情（以列表形式展现）列表字段如下：人员姓名、相关项目、工程所在地、锁定日期、工程状态、合同开工日期、合同竣工日期、实际开工日期、实际竣工日期       
        var p = from emp in db.TC_PrjItem_Emp_Lock join projiteminfo in db.TC_PrjItem_Info on emp.FPrjItemId equals projiteminfo.FId 
                join prj in db.TC_Prj_Info on projiteminfo.FPrjId equals prj.FId
                join sgxkzprj in db.TC_SGXKZ_PrjInfo on projiteminfo.FId equals sgxkzprj.FPrjItemId
                select new {
                    emp.FIdCard,
                    emp.FHumanName,
                    projiteminfo.ProjectName,
                    AddressDept = AreaName( projiteminfo.AddressDept),
                    SDRQ = emp.FCreateTime,
                    SDZT= PrjState(prj.StartDate,prj.EndDate),
                    SJKGRQ= prj.StartDate,
                    SJJGRQ = prj.EndDate,
                    HTKGRQ = sgxkzprj.StartDate,
                    HTJGRQ = sgxkzprj.EndDate
                };

        p = p.Where(item => item.FIdCard == idCard);
        dg_List.DataSource = p;
        dg_List.DataBind();
    }
    
    
    /// <summary>
    /// 未开工：实际开工时间为空
    /// 已开工：实际竣工时间为空或大于now
    /// 已竣工：实际竣工时间小于now 
    /// </summary>
    /// <param name="sjkgsj"></param>
    /// <param name="sjjgsj"></param>
    /// <returns></returns>
    private string PrjState(object sjkgsj,object sjjgsj)
    {

        if (sjkgsj==null)
        {
            return "未开工";
        }
        else if (sjjgsj == null || (EConvert.ToDateTime(sjjgsj) > DateTime.Now))
        {
            return "已开工";
        }else if (EConvert.ToDateTime(sjjgsj) < DateTime.Now)
        {
            return "已竣工";
        }
        else
        {
            return "未知";
        }
    }


    private string AreaName(string areaCode)
    {
        StringBuilder areaName = new StringBuilder();

        if (string.IsNullOrEmpty(areaCode)) return "未知";
        if (areaCode.Length >= 2)
        {
            areaName = areaName.Append(GetAreaId(areaCode.Substring(0, 2)));
        }

        if (areaCode.Length>=4)
        {
            areaName = areaName.Append(GetAreaId(areaCode.Substring(0, 4)));
        }

        if (areaCode.Length>=6)
        {
            areaName = areaName.Append(GetAreaId(areaCode));
        }
        return areaName.ToString();
    }

   

    private string GetAreaId(string areaId)
    {
        EgovaDB db = new EgovaDB();
        var manageDept = db.CF_Sys_ManageDept.FirstOrDefault(item => item.FNumber == EConvert.ToInt(areaId));

        return manageDept != null ? manageDept.FName : "未知";
    }
}