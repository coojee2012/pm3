<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GGJZJNInfo.aspx.cs" Inherits="KcsjSgt_ApplyOther_GGJZJNInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公共建筑节能设计审查备案登记表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function checkInfo() {
            if (!AutoCheckInfo()) {
                return false;
            }
            return true;
        }
        function selPrj(obj) {
            var url = "PrjSelList.aspx?type=jzjn&pType=1"; //公建
            var pid = showWinByReturn(url, 700, 500);
            if (pid != null && pid != '') {
                $("#txtFLinkId").val(pid);
                __doPostBack(obj.id, '');
            }
            else
                return false;
        } 
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                公共建筑节能设计审查备案登记表
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                    OnClientClick="return checkInfo();" />
                <input id="btnReturn" type="button" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FPrjName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                <asp:Button runat="server" ID="btnSel" Text="选择.." CssClass="m_btn_w4" OnClientClick="return selPrj(this)"
                    UseSubmitBehavior="false" OnClick="btnSel_Click" />
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备案编号：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FBackUpNo" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                合格书编号：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FHGCertiNo" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地点：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="t_FAllAddress" runat="server" CssClass="m_txt" Width="150px" MaxLength="30"
                    Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                子项名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FSubName" runat="server" CssClass="m_txt" Width="500px" TextMode="MultiLine"
                    Height="50px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                栋号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FDongHao" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设单位：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Enabled="false" Width="200px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FMobile" runat="server" CssClass="m_txt" Enabled="false" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                设计单位：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_SJDW" runat="server" CssClass="m_txt" Enabled="false" Width="200px"></asp:TextBox>
            </td>
            <td colspan="2">
                资质等级:
                <asp:TextBox ID="t_FSJLevelName" runat="server" CssClass="m_txt" Enabled="false"
                    Width="100px"></asp:TextBox>
                证书编号:
                <asp:TextBox ID="t_FSJCertiNo" runat="server" CssClass="m_txt" Enabled="false" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查机构：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FSCJG" runat="server" CssClass="m_txt" Enabled="false" Width="200px"></asp:TextBox>
            </td>
            <td colspan="2">
                资质等级:
                <asp:TextBox ID="t_FSCLevelName" runat="server" CssClass="m_txt" Enabled="false"
                    Width="100px"></asp:TextBox>
                证书编号:
                <asp:TextBox ID="t_FSCCertiNo" runat="server" CssClass="m_txt" Enabled="false" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                节能计算建筑面积：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FArea" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
                ㎡ <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建筑性质：
            </td>
            <td colspan="3">
                <asp:CheckBoxList ID="t_FNature" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建筑层数：
            </td>
            <td colspan="1">
                地上：
                <asp:TextBox ID="t_FGround" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"
                    MaxLength="9"></asp:TextBox>(层) 地下：
                <asp:TextBox ID="t_FUnderground" runat="server" CssClass="m_txt t_r" Width="70px"
                    onblur="isInt(this)" MaxLength="9"></asp:TextBox>(层)
            </td>
            <td class="t_r t_bg">
                结构形式：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_FStruType" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_c t_bg" rowspan="7" width="12%">
                窗(含玻璃幕墙)的节能设计
            </td>
            <td class="t_c t_bg" rowspan="2">
                朝向
            </td>
            <td class="t_c t_bg" rowspan="2">
                窗墙<br />
                面积比
            </td>
            <td class="t_c t_bg" colspan="2">
                外窗传热系数(K)
            </td>
            <td class="t_c t_bg" rowspan="2">
                遮阳系数SC
            </td>
            <td class="t_c t_bg" rowspan="2">
                可见光透过率
            </td>
            <td class="t_c t_bg" colspan="2">
                外窗（幕墙）选型
            </td>
        </tr>
        <tr>
            <td class="t_c">
                限值
            </td>
            <td class="t_c">
                设计值<br />
                （含凸窗）
            </td>
            <td class="t_c">
                窗框材料
            </td>
            <td class="t_c">
                玻璃
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg">
                东
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt1" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt2" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt3" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt4" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt5" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt6" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt7" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg">
                南
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt8" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt9" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt10" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt11" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt12" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt13" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt14" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg">
                西
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt15" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt16" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt17" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt18" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt19" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt20" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt21" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg">
                北
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt22" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt23" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt24" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt25" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt26" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt27" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:TextBox ID="t_FTxt28" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="5">
                外窗（含阳台门）的气密性等级：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt29" runat="server" CssClass="m_txt" Width="100px" onblur="isFloat(this)"></asp:TextBox>
                (级)
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_c t_bg" colspan="9">
                屋面、墙体、楼地面及户门等的热工节能设计指标
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg" colspan="2">
                部位
            </td>
            <td class="t_c t_bg" colspan="3">
                限值
            </td>
            <td class="t_c t_bg" colspan="1">
                设计值
            </td>
            <td class="t_c t_bg" colspan="3">
                主要节能措施（注明保温层材料及厚度）
            </td>
        </tr>
        <tr>
            <td class="t_c" colspan="2">
                屋顶透明部分
            </td>
            <td class="t_c" colspan="3">
                K≤3.0<br />
                遮阳系数SC≤0.4<br />
                面积S≤20%
            </td>
            <td class="t_c" colspan="1">
                K=<asp:TextBox ID="t_FFloat1" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox><br />
                SC=<asp:TextBox ID="t_FFloat2" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox><br />
                S=<asp:TextBox ID="t_FFloat3" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox>
            </td>
            <td class="t_c" colspan="3">
                <asp:TextBox ID="t_FTxt30" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c" colspan="2">
                屋面
            </td>
            <td class="t_c" colspan="3">
                K≤0.70
            </td>
            <td class="t_c" colspan="1">
                K=<asp:TextBox ID="t_FFloat4" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox>
            </td>
            <td class="t_c" colspan="3">
                <asp:TextBox ID="t_FTxt31" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c" colspan="2">
                外墙
            </td>
            <td class="t_c" colspan="3">
                Km≤1.0
            </td>
            <td class="t_c" colspan="1">
                Km=<asp:TextBox ID="t_FFloat5" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox>
            </td>
            <td class="t_c" colspan="3">
                <asp:TextBox ID="t_FTxt32" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c" colspan="2">
                底面接触室外空气的架空或外挑楼板
            </td>
            <td class="t_c" colspan="3">
                K≤1.0
            </td>
            <td class="t_c" colspan="1">
                K=<asp:TextBox ID="t_FFloat6" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox>
            </td>
            <td class="t_c" colspan="3">
                <asp:TextBox ID="t_FTxt33" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c" colspan="2">
                地面
            </td>
            <td class="t_c" colspan="3">
                热阻R(m2·K/W)≥1.2
            </td>
            <td class="t_c" colspan="1">
                R=<asp:TextBox ID="t_FFloat7" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox>
            </td>
            <td class="t_c" colspan="3">
                <asp:TextBox ID="t_FTxt34" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c" colspan="2">
                地下室外墙
            </td>
            <td class="t_c" colspan="3">
                热阻R(m2·K/W)≥1.2
            </td>
            <td class="t_c" colspan="1">
                R=<asp:TextBox ID="t_FFloat8" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox>
            </td>
            <td class="t_c" colspan="3">
                <asp:TextBox ID="t_FTxt35" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                电气
            </td>
            <td class="t_c">
                照明功率密度
            </td>
            <td class="t_c">
                限值
            </td>
            <td>
                <asp:TextBox ID="t_FTxt36" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c">
                设计值
            </td>
            <td>
                <asp:TextBox ID="t_FFloat9" runat="server" CssClass="m_txt" Width="100px" onblur="isFloat(this)"></asp:TextBox>
            </td>
            <td class="t_c" colspan="2">
                镇流器是否符合能效标准
            </td>
            <td>
                <asp:DropDownList ID="t_FIsHG" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="1">符合</asp:ListItem>
                    <asp:ListItem Value="0">不符合</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" rowspan="2">
                暖通
            </td>
            <td class="t_c" colspan="3">
                空调负荷计算是否符合规定
            </td>
            <td class="t_c" colspan="3">
                热负荷
                <asp:DropDownList ID="t_FIsHG2" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="1">符合</asp:ListItem>
                    <asp:ListItem Value="0">不符合</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_c" colspan="2">
                逐项逐时冷负荷
                <asp:DropDownList ID="t_FIsHG3" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="1">符合</asp:ListItem>
                    <asp:ListItem Value="0">不符合</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_c" colspan="1">
                冷热源选择
            </td>
            <td class="t_l" colspan="7">
                <asp:TextBox ID="t_FTxt37" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg" colspan="2">
                性能性指标计算
            </td>
            <td class="t_c" colspan="2">
                标准规定年耗电量
            </td>
            <td class="t_c" colspan="2">
                <asp:TextBox ID="t_FFloat10" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox>
                Kwh/(m2)
            </td>
            <td class="t_c" colspan="2">
                设计年耗电量
            </td>
            <td class="t_c" colspan="1">
                <asp:TextBox ID="t_FFloat11" runat="server" CssClass="m_txt" Width="100px" onblur="isFloat(this)"></asp:TextBox>
                Kwh/(m2)
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg" colspan="2">
                围护结构热工性能权衡判断
            </td>
            <td class="t_c" colspan="2">
                参照建筑物能耗
            </td>
            <td class="t_c" colspan="2">
                <asp:TextBox ID="t_FFloat12" runat="server" CssClass="m_txt" Width="80px" onblur="isFloat(this)"></asp:TextBox>
                Kwh/(m2)
            </td>
            <td class="t_c" colspan="2">
                设计建筑物能耗
            </td>
            <td class="t_c" colspan="1">
                <asp:TextBox ID="t_FFloat13" runat="server" CssClass="m_txt" Width="100px" onblur="isFloat(this)"></asp:TextBox>
                Kwh/(m2)
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg" colspan="2">
                备注
            </td>
            <td colspan="7">
                <asp:TextBox ID="t_FTxt38" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg" colspan="2">
                审查机构意见
            </td>
            <td>
                <asp:DropDownList ID="t_FIsHG4" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="1">合格</asp:ListItem>
                    <asp:ListItem Value="0">不合格</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_c" colspan="2">
                审核人:
                <asp:TextBox ID="t_FTxt39" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_c" colspan="2">
                负责人:
                <asp:TextBox ID="t_FTxt40" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_c" colspan="2">
                审查日期:
                <asp:TextBox ID="t_FDate1" runat="server" CssClass="m_txt" Width="80px" onfocus="WdatePicker();"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg" colspan="2">
                备案部门意见
            </td>
            <td>
                <asp:TextBox ID="t_FTxt41" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c" colspan="2">
                经办人:
                <asp:TextBox ID="t_FTxt42" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_c" colspan="2">
                负责人:
                <asp:TextBox ID="t_FTxt43" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_c" colspan="2">
                审查日期:
                <asp:TextBox ID="t_FDate2" runat="server" CssClass="m_txt" Width="80px" onfocus="WdatePicker();"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="t_FAddressDept" type="hidden" runat="server" />
    <input id="txtFLinkId" type="hidden" runat="server" />
    </form>
</body>
</html>
