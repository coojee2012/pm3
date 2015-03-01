using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WYDW_ApplyLPB_HouseList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string strBuild = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string buildid = Convert.ToString(Session["WY_LPB_Buildid"]);

            string strSql_Floor = "select distinct RHC=convert(int,RHC) from dbo.YW_WY_XM_FWXX where Buildid='" + buildid + "' order by RHC desc";

            DataTable dt_Floor = rc.GetTable(strSql_Floor);

            string strsql_Unit = "select t.dy as name,count(a.houseid) as PrefloorRoomCount  from( select distinct DY,buildid  from dbo.YW_WY_XM_FWXX ) as t inner join dbo.YW_WY_XM_FWXX as a on t.dy=a.dy ";
            strsql_Unit += "where t.buildid='" + buildid + "' and a.buildid='" + buildid + "' GROUP BY t.dy ";

            DataTable dt_Unit = rc.GetTable(strsql_Unit);


            string strSql_House = "select *  from dbo.YW_WY_XM_FWXX where Buildid='" + buildid + "' order by FH desc";

            DataTable dt_House = rc.GetTable(strSql_House);
            strBuild = CreateLPB3(dt_Floor, dt_Unit, dt_House);

            //string 

        }
    }
    #region 生成楼盘表
    /// <summary>
    /// 生成楼盘表3
    /// </summary>
    /// <param name="dt_Floor">所有楼层的数据集</param>
    /// <param name="Unit">所有单元的数据集</param>
    /// <param name="dt_House">所有房屋的数据集</param>
    private string CreateLPB3(DataTable dt_Floor, DataTable dt_Unit, DataTable dt_House)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<div id='div_LPB' class='div_LPB' style='height: 478px; width: 745px; overflow: auto;'>");
            sb.Append("<table id='tableLPB' width='auto' cellpadding='0' cellspacing='2'>");



            int MaxRoomCount = 0;

            for (int i = 0; i < dt_Floor.Rows.Count; i++)
            {
                sb.Append("<tr>");

                int floorno = Convert.ToInt32(dt_Floor.Rows[i]["RHC"]);

                string strFloorName = Convert.ToString(dt_Floor.Rows[i]["RHC"]);
                sb.Append(" <td class='lb-lc'>" + strFloorName + "层</td>");

                //通过楼层筛选房屋
                DataRow[] dr1 = dt_House.Select(string.Format("RHC='{0}'", floorno));
                if (dr1.Length != 0)
                {
                    int roomCount = 0; //计算房屋具体室数，计算宽度
                    //循环单元，排列有单元的房屋
                    for (int m = 0; m < dt_Unit.Rows.Count; m++)
                    {
                        roomCount++;
                        //通过楼层、单元筛选房屋
                        DataRow[] dr2 = dt_House.Select(string.Format("RHC='{0}' and DY='{1}'", floorno, dt_Unit.Rows[m]["Name"].ToString().Trim()));
                        if (dr2.Length != 0)
                        {
                            for (int n = 0; n < Convert.ToInt32(dt_Unit.Rows[m]["PrefloorRoomCount"]); n++)
                            {
                                //通过楼层、单元和户室号同时筛选房屋
                                DataRow[] dr3 = dt_House.Select(string.Format("RHC='{0}' and DY='{1}' and SH='{2}'", floorno, dt_Unit.Rows[m]["Name"].ToString().Trim(), n + 1));
                                if (dr3.Length != 0)
                                {
                                    //生成并填充房屋信息的单元格
                                    string bgColor = "#06FB0A"; //颜色
                                    //string bgColor = GetColor(dr3[0]["StateCode"].ToString().Trim(), "");
                                    string constructionArea = dr3[0]["JZMJ"].ToString().Trim(); //建筑面积

                                    sb.Append("<td class='lb-fw' id='lmt" + dr3[0]["HouseId"].ToString().Trim() + "' name='" + dr3[0]["HouseId"].ToString().Trim() + "'  bgcolor='" + bgColor + "'; ");
                                    sb.Append("title='面积:" + constructionArea + "平方米 '>");
                                    //sb.Append(dr3[0]["Number"]);
                                    sb.Append("<a href='#' name='" + dr3[0]["HouseId"].ToString().Trim() + "' id='" + dr3[0]["BuildId"].ToString().Trim() + "'  >" + dr3[0]["FH"] + "</a>");

                                    sb.Append("</td>");
                                }
                                else
                                {
                                    //sb.Append("<td class='lb-fw'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
                                }
                            }
                        }
                        else
                        {
                            for (int n = 0; n < Convert.ToInt32(dt_Unit.Rows[m]["PrefloorRoomCount"]); n++)
                            {
                                //sb.Append("<td class='lb-fw'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
                            }
                        }
                    }

                    //在把有单元的房屋排列完之后排列没有单元的
                    for (int p = 0; p < dr1.Length; p++)
                    {
                        roomCount++;
                        //循环单元，排列没有有单元的房屋
                        bool hasUnit = false;
                        for (int m = 0; m < dt_Unit.Rows.Count; m++)
                        {
                            if (dt_Unit.Rows[m]["Name"].ToString().Trim() == dr1[p]["DY"].ToString().Trim())
                            {
                                hasUnit = true;
                                break;
                            }
                        }
                        if (hasUnit == false)
                        {
                            //生成并填充房屋信息的单元格
                            string bgColor = "#06FB0A"; //颜色
                            string constructionArea = dr1[p]["JZMJ"].ToString().Trim(); //建筑面积

                            sb.Append("<td class='lb-fw' id='lmt" + dr1[p]["HouseId"].ToString().Trim() + "' name='" + dr1[p]["HouseId"].ToString().Trim() + "'  bgcolor='" + bgColor + "'; ");
                            sb.Append("title='面积:" + constructionArea + "平方米 '>");
                            //sb.Append(dr1[p]["Number"]);
                            sb.Append("<a href='#' name='" + dr1[p]["HouseId"].ToString().Trim() + "' id='" + dr1[p]["BuildId"].ToString().Trim() + "'  >" + dr1[p]["FH"] + "</a>");

                            sb.Append("</td>");
                        }
                    }
                    if (MaxRoomCount < roomCount)
                    {
                        MaxRoomCount = roomCount;
                    }
                }



            }

            sb.Append("</table>");
            sb.Append("</div>");

            double d = MaxRoomCount * 70;

            if (d > 745)
            {
                sb.Replace("width='auto'", "width='" + d.ToString() + "px");
            }
        }
        catch
        {

        }
        return sb.ToString().Trim();
    }

    #endregion

    #region 返回房屋应该显示的颜色
    /// <summary>
    /// 返回房屋应该显示的颜色
    /// </summary>
    /// <param name="State">房屋的状态码</param>
    /// <param name="TypeCode">房屋分类编码</param>
    /// <param name="type">传入该页面的操作类型代码</param>
    /// <param name="Owner">房屋买受人名称</param>
    private string GetColor(string State, string TypeCode)
    {
        string strColor = "";
        string[] arrayState = new string[12];
        for (int i = 0; i < State.Length; i++)
        {
            arrayState[i] = State.Substring(i, 1);
        }

        //查封
        if (arrayState[5] == "1")
        {
            strColor = "#7F0102";
        }
        //抵押
        else if (arrayState[4] == "1")
        {
            strColor = "#FC7E7F";
        }
        //发证
        else if (arrayState[3] == "1")
        {
            strColor = "#FC0301";
        }
        //抵预
        else if (arrayState[11] == "1")
        {
            strColor = "#FDDFAB";
        }
        //预告
        else if (arrayState[2] == "1")
        {
            strColor = "#FF8354";
        }
        //公用
        else if (arrayState[10] == "1")
        {
            strColor = "#FEA600";
        }
        //自有
        else if (arrayState[9] == "1")
        {
            strColor = "#0601FB";
        }
        //备案（已签合同）
        else if (arrayState[1] == "1")
        {
            strColor = "#FFFC06";
        }
        //签约(拟定合同)
        else if (arrayState[7] == "1")
        {
            strColor = "#02FFFD";
        }
        //认购（订购）
        else if (arrayState[0] == "1")
        {
            strColor = "#2E8B57";
        }
        ////非售
        //else if (arrayState[8] == "1")
        //{
        //    strColor = "#D7D3D3";
        //}
        else
        {
            //异议
            if (arrayState[6] == "1")
            {
                strColor = "#C0C0C0";
            }
            else
            {
                //可售
                if (TypeCode.StartsWith("2002"))
                {
                    strColor = "#06FB0A";
                }
                //不可售
                else
                {
                    strColor = "#D7D3D3";
                }
            }
        }
        return strColor;
    }
    #endregion

    #region 返回房屋已经存在的业务的图片
    /// <summary>
    /// 返回房屋已经存在的业务的图片
    /// </summary>
    /// <param name="State">房屋的状态码</param>
    /// <param name="TypeCode">房屋分类编码</param>
    /// <param name="type">传入该页面的操作类型代码</param>
    /// <param name="Owner">房屋买受人名称</param>
    private string GetImage(string State, string TypeCode)
    {
        string strImage = "";
        string[] arrayState = new string[12];
        for (int i = 0; i < State.Length; i++)
        {
            arrayState[i] = State.Substring(i, 1);
        }

        //认购（订购）
        if (arrayState[0] == "1")
        {
            strImage += "<img alt='' width='16px' height='16px' src=''/>";
        }
        //备案（已签合同）
        if (arrayState[1] == "1")
        {
            strImage += "<img alt='' width='16px' height='16px' src='../Images/State/1.jpg'/>";
        }
        //预告
        if (arrayState[2] == "1")
        {
            strImage += "<img alt='' width='16px' height='16px' src='../Images/State/2.jpg'/>";
        }
        //发证
        if (arrayState[3] == "1")
        {
            strImage += "<img alt='' width='16px' height='16px' src='../Images/State/3.jpg'/>";
        }
        //抵押
        if (arrayState[4] == "1")
        {
            strImage += "<img alt='' width='16px' height='16px' src='../Images/State/4.jpg'/>";
        }
        //查封
        if (arrayState[5] == "1")
        {
            strImage += "<img alt='' width='16px' height='16px' src='../Images/State/5.jpg'/>";
        }
        //异议
        if (arrayState[6] == "1")
        {
            strImage += "<img alt='' width='16px' height='16px' src='../Images/State/6.jpg'/>";
        }
        //签约(拟定合同)
        if (arrayState[7] == "1")
        {
            strImage += "<img alt='' width='16px' height='16px' src='../Images/State/0.jpg'/>";
        }
        strImage += "<br/>";

        return strImage;
    }
    #endregion

    #region 返回建筑面积
    /// <summary>
    /// 返回建筑面积
    /// </summary>
    /// <param name="ActualConstructionArea">实测建筑面积</param>
    /// <param name="PreConstructionArea">预测建筑面积</param>
    private string GetConstructionArea(string ActualConstructionArea, string PreConstructionArea)
    {
        if (ActualConstructionArea != "0" && ActualConstructionArea != "0.00" && ActualConstructionArea != "0.0000")
        {
            return ActualConstructionArea;
        }
        else
        {
            return PreConstructionArea;
        }
    }
    #endregion
}