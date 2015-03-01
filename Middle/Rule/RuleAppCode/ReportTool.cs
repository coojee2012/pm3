using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Approve.RuleCenter;
using System.Text;
namespace Approve.Common
{
    /// <summary>
    /// ReportTool 的摘要说明
    /// </summary>
    public class ReportTool
    {
        /// <summary>
        /// 对上报建设部的文件进行格式转化工具
        /// </summary>
        public ReportTool()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 转化企业类型
        /// </summary>
        /// <param name="fsystemId"></param>
        /// <returns></returns>
        public static string GetFSystemId(string fsystemId)
        {
            switch (fsystemId)
            {
                case "101"://施工
                    fsystemId = "4";
                    break;
                case "155"://勘察
                    fsystemId = "1";
                    break;
                case "120"://招标代理
                    fsystemId = "3";
                    break;
                case "125"://监理
                    fsystemId = "5";
                    break;
                case "185"://工程造价咨询
                    fsystemId = "6";
                    break;
                case "202"://城乡规划
                    fsystemId = "7";
                    break;
                case "130"://房地产开发
                    fsystemId = "8";
                    break;
                case "187"://物业管理
                    fsystemId = "9";
                    break;
                case "186"://房地产估价
                    fsystemId = "10";
                    break;
                case "135"://园林绿化
                    fsystemId = "12";
                    break;
                case "196"://一体化
                    fsystemId = "13";
                    break;
                case "180"://外商施工
                    fsystemId = "16";
                    break;
                default:
                    fsystemId = string.Empty;
                    break;
            }
            return fsystemId;
        }
        /// <summary>
        /// 转换企业经济性质
        /// </summary>
        /// <param name="fentTypeId"></param>
        /// <returns></returns>
        public static string GetEntTypeId(string fentTypeId)
        {
            RCenter rc = new RCenter();
            if (!string.IsNullOrEmpty(fentTypeId))
            {
                fentTypeId = rc.GetSignValue("select top 1 FCnumber from cf_sys_dic where fnumber='" + fentTypeId + "'");
            }
            return fentTypeId;
        }


        public static string GetDefaultListIDByEntType(string fenttype)
        {
            RCenter rc = new RCenter();
            if (!string.IsNullOrEmpty(fenttype))
            {
                fenttype = rc.GetSignValue("select top 1 tradetypenum from tb_TRADETYPEDIC where AptitudeKind=" + fenttype);
            }
            return fenttype;
        }

        public static string GetDefaultTypeIDByEntType(string TRADETYPENUM)
        {
            RCenter rc = new RCenter();
            if (!string.IsNullOrEmpty(TRADETYPENUM))
            {
                TRADETYPENUM = rc.GetSignValue("select top 1 tradeboundnum from tb_TRADETYPEBOUNDDIC  where TRADETYPENUM=" + TRADETYPENUM);
            }
            return TRADETYPENUM;
        }

        /// <summary>
        /// 转换币种
        /// </summary>
        /// <param name="fentTypeId"></param>
        /// <returns></returns>
        public static string GetRegistBound(string fRegistFound)
        {
            RCenter rc = new RCenter();
            if (string.IsNullOrEmpty(fRegistFound))
            {
                fRegistFound = "1";//人民币
            }
            else
            {
                string ftype = rc.GetSignValue("select top 1 FName from cf_Sys_Dic where fnumber='" + fRegistFound + "'");
                if (string.IsNullOrEmpty(ftype))
                {
                    fRegistFound = "1";//人民币
                }
                else
                {
                    switch (ftype)
                    {
                        case "人民币":
                            fRegistFound = "1";//人民币
                            break;
                        case "美元":
                            fRegistFound = "2";//美元
                            break;
                        case "日元":
                            fRegistFound = "3";//日元
                            break;
                        case "欧元":
                            fRegistFound = "4";//欧元
                            break;
                        case "港元":
                            fRegistFound = "5";//港元
                            break;
                        default:
                            fRegistFound = "1";//人民币
                            break;
                    }
                }
            }
            return fRegistFound;
        }

        /// <summary>
        /// 转换资质的序列
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static string GetFListId(string FListName, string fsystemId)
        {
            RCenter rc = new RCenter();
            string flag = rc.GetSignValue("select top 1 TRADETYPENUM from tb_TRADETYPEDIC where TRADETYPENAME like '" + FListName + "' and AptitudeKind='" + GetFSystemId(fsystemId) + "'");
            //if (!string.IsNullOrEmpty(flag))
            //    FListName = flag;
            return flag;
        }
        /// <summary>
        /// 转换资质的专业
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static string GetFTypeId(string FTypeName, string fsystemId)
        {
            RCenter rc = new RCenter();
            string flag = rc.GetSignValue("select top 1 TRADEBOUNDNUM from tb_TRADETYPEBOUNDDIC b inner join tb_TRADETYPEDIC t on t.TRADETYPENUM=b.TRADETYPENUM where b.TRADEBOUNDNAME like '" + FTypeName + "' and t.AptitudeKind='" + GetFSystemId(fsystemId) + "'");
            //if (!string.IsNullOrEmpty(flag))
            //    FTypeName = flag;
            return flag;
        }
        /// <summary>
        /// 转换资质的等级
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static string GetFLevelId(string FLevelName)
        {
            switch (FLevelName)
            {
                case "甲级":
                    FLevelName = "88";
                    break;
                case "乙级":
                    FLevelName = "86";
                    break;
                case "丙级":
                    FLevelName = "85";
                    break;
                case "丁级":
                    FLevelName = "84";
                    break;
                case "不分等级":
                    FLevelName = "93";
                    break;
                case "一级":
                    FLevelName = "77";
                    break;
                case "二级":
                    FLevelName = "76";
                    break;
                case "三级":
                    FLevelName = "75";
                    break;
                case "四级":
                    FLevelName = "74";
                    break;
                case "暂定级":
                    FLevelName = "80";
                    break;
                default:
                    FLevelName = "93";
                    break;
            }
            return FLevelName;
        }
        /// <summary>
        /// 根据省份获取审核部门的编号
        /// 返回的格式是：AreaId,AreaDeptId(如："21,1")
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static string GetDeptCode(string deptId)
        {
            if (string.IsNullOrEmpty(deptId))
                deptId = ComFunction.GetDefaultDept();
            switch (deptId)
            {
                case "21"://辽宁
                    deptId = "辽宁省住房和城乡建设厅";
                    break;
                case "36"://江西
                    deptId = "江西省住房和城乡建设厅";
                    break;
                case "51"://四川
                    deptId = "四川省住房和城乡建设厅";
                    break;
                case "52"://贵州
                    deptId = "贵州省住房和城乡建设厅";
                    break;
                case "61"://陕西
                    deptId = "陕西省住房和城乡建设厅";
                    break;
                default:
                    deptId = "";
                    break;
            }
            return deptId;
        }

        public static string GetListIDByFNumber(string fnumber)
        {
            RCenter rc = new RCenter();
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("select top 1 tradetypenum from tb_TRADETYPEDIC where weid=" + fnumber);

            string result = rc.GetSignValue(sqlBuilder.ToString());
            if (String.IsNullOrEmpty(result))
            {
                return string.Empty;
            }
            return result;
        }

        public static string GetTypeIDByFNumber(string fnumber)
        {
            RCenter rc = new RCenter();
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("select top 1 tradeboundnum from tb_TRADETYPEBOUNDDIC where weid='" + fnumber + "'");

            string result = rc.GetSignValue(sqlBuilder.ToString());
            if (String.IsNullOrEmpty(result))
            {
                return string.Empty;
            }
            return result;
        }
    }
}