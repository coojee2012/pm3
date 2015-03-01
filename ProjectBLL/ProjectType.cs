using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Approve.Common
{
    public class ProjectType
    {
        public ProjectType()
        { }
        /*项目信息类型*/
        public string getProjectType(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "项目审核信息");
            ht.Add("2", "项目核准信息");
            ht.Add("3", "项目备案信息");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";


        }
        public string pType(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "勘察设计企业");
            ht.Add("2", "造价咨询企业");
            ht.Add("3", "招标代理企业");
            ht.Add("4", "工程监理企业");
            ht.Add("5", "工程施工企业");
            ht.Add("6", "工程检测企业");
            ht.Add("7", "材料及设备供应企业");
            ht.Add("8", "其他相关机构");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }
        public string PersonType(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "法人代表");
            ht.Add("2", "建造师(项目负责人)");
            ht.Add("3", "建筑师");
            ht.Add("4", "监理工程师");
            ht.Add("5", "城市规划师");
            ht.Add("6", "招标师");
            ht.Add("7", "造价工程师");
            ht.Add("8", "项目相关人员");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }
        public string QYType(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "企业基本信息");
            ht.Add("2", "企业资质信息");
            ht.Add("3", "项目业绩信息");
            ht.Add("4", "信用评价信息");
            ht.Add("5", "良好行为记录信息");
            ht.Add("6", "不良行为记录信息");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }

        public string PPType(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "人员基本信息");
            ht.Add("2", "执业资格信息");
            ht.Add("3", "参与项目信息");
            ht.Add("4", "良好行为记录信息");
            ht.Add("5", "不良行为记录信息");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }
        public string GoodAction(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "企业良好行为");
            ht.Add("2", "企业不良行为");
            ht.Add("3", "个人良好行为");
            ht.Add("4", "个人不良行为");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }

        public string ProjectTp(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "项目审核信息");
            ht.Add("2", "项目核准信息");
            ht.Add("3", "项目备案信息");
            ht.Add("4", "项目建设管理信息");
            ht.Add("5", "从业单位信息");
            ht.Add("6", "从业人员信息");
            ht.Add("7", "土地使用权审核出让信息");
            ht.Add("8", "矿业权审核和出让信息");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }
        public string ProjectSP(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "项目建议书批复结果");
            ht.Add("2", "可研报告批复结果");
            ht.Add("3", "招标实施方案批复结果");
            ht.Add("4", "初步设计方案批复结果");
            ht.Add("5", "节能评估审查批复结果");
            ht.Add("6", "规划选址意见批复结果");
            ht.Add("7", "用地批复文件结果");
            ht.Add("8", " 环境影响评价审核结果");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }
        public string ProjectHZ(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "项目申请报告核准结果");
            ht.Add("2", "招标实施方案核准结果");
            ht.Add("3", "节能评估审查核准结果");
            ht.Add("4", "规划选址意见核准结果");
            ht.Add("5", "用地批复文件结果");
            ht.Add("6", "环境影响评价核准结果");

            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }

        public string ProjectBA(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "项目备案申请结果");
            ht.Add("2", "招标实施方案核准结果");
            ht.Add("3", "施工图核准信息");
            ht.Add("4", "节能评估审查备案结果");
            ht.Add("5", "规划选址意见备案结果");
            ht.Add("6", "用地批复文件结果");
            ht.Add("7", "环境影响评价备案结果");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }

        public string ProjectMananger(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "项目基本信息");
            ht.Add("2", "项目招投标信息");
            ht.Add("3", "征地拆迁信息");
            ht.Add("4", "重大设计变更信息");
            ht.Add("5", "施工管理信息");
            ht.Add("6", "合同履约信息");
            ht.Add("7", "质量检查信息 ");
            ht.Add("8", "安全检查信息");
            ht.Add("9", "资金管理信息");
            ht.Add("10", "交工验收信息");
            ht.Add("11", "竣工验收信息");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }
        public string UnitInfo(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "从业单位基本信息");
            ht.Add("2", "从业单位良好行为记录");
            ht.Add("3", "从业单位不良行为记录");
            ht.Add("4", "资质信息");
            ht.Add("5", "项目业绩信息");
            ht.Add("6", "信用评价信息");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }

        public string pInfo(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "从业人员基本信息");
            ht.Add("2", "从业人员良好行为记录");
            ht.Add("3", "从业人员不良行为记录");
            ht.Add("4", "执业资格信息");
            ht.Add("5", "参与项目信息");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }
        public string dInfo(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "土地招标拍卖出让信息");
            ht.Add("2", "国土使用权出让合同信息");
            ht.Add("3", "建设用地审核信息");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }
        public string kInfo(string code)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();

            ht.Add("1", "探矿权审核信息");
            ht.Add("2", "探矿权出让信息");
            ht.Add("3", "采矿权审核信息");
            ht.Add("4", "采矿权出让信息 ");
            if (ht.ContainsKey(code))
            {
                return ht[code];
            }
            return "";
        }
    }
}
