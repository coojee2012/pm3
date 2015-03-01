<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="KcsjSgt_ApplyKCWJBA_ApplyBaseInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            if (!getLength(document.getElementById("t_FTxt10"), 100, '“合同备案内容及范围”')) {
                return false;
            }
            if (!getLength(document.getElementById("t_FTxt11"), 500, '“合同备案承诺和要求”')) {
                return false;
            }
            return true;
        }
        function selEnt(tagId, fsysid, obj, hasCerti) {
            var url = "EntListSel.aspx";
            if (hasCerti == "1")
                url = "EntListSelCerti.aspx";
            var pid = showWinByReturn('../appmain/' + url + '?fsysid=' + fsysid, 700, 500);
            if (pid != null && pid != '') {
                var ps = pid.split('|');
                if (ps.length > 0)
                    $("#" + tagId).val(ps[0]);
                if (ps.length > 1)
                    $("#" + tagId + "_c").val(ps[1]);
                __doPostBack(obj.id, '');
            }
        }
        function showSZ() {
            var t = $("#p_FType").val();
            if (t == "2000101") {//房屋建筑
                $("#sz_1").hide();
                $("#sz_2").show();
            }
            else {//市政
                $("#sz_1").show();
                $("#sz_2").hide();
            }
        }
        function seePrj() {
            var fid = $('#p_FId').val();
            showAddWindow('../../JSDW/appmain/AddPrjRegist.aspx?fid=' + fid, 900, 700);
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
                基本信息
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
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                工程项目：
            </td>
            <td colspan="3">
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                <input type="button" id="btnLook" value="查看工程基本信息" class="m_btn_w8" onclick="seePrj()" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="12%">
                建设单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FName" runat="server" CssClass="m_txt" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="e_FLinkMan" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="e_FTel" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                子项名称：
            </td>
            <td>
                <asp:TextBox ID="txtFPrjItemList" runat="server" CssClass="m_txt" Width="250px" Enabled="false"
                    Height="50px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                送审日期：
            </td>
            <td>
                <asp:TextBox ID="txtSSDate" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地点：
            </td>
            <td colspan="1">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="p_FAllAddress" runat="server" CssClass="m_txt" Width="150px" MaxLength="30"
                    Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                完成日期：
            </td>
            <td>
                <asp:TextBox ID="txtFSCDate" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                勘察单位：
            </td>
            <td>
                <asp:TextBox ID="k_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="k_FLevelName" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                设计单位：
            </td>
            <td>
                <asp:TextBox ID="sj_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="sj_FLevelName" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查机构：
            </td>
            <td>
                <asp:TextBox ID="s_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="false"></asp:TextBox>
                <input id="s_FBaseInfoId" type="hidden" runat="server" />
            </td>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="s_FLevelName" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_c t_bg" colspan="6">
                工程概况
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="12%">
                建设性质：
            </td>
            <td colspan="5">
                <asp:DropDownList ID="p_FKind" runat="server" CssClass="m_txt" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建筑使用性质：
            </td>
            <td colspan="5">
                <asp:CheckBoxList ID="p_FNature" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                    RepeatColumns="8" Enabled="false">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr id="sz_1" style="display: none">
            <td class="t_r t_bg">
                市政行业类别：
            </td>
            <td colspan="5">
                <asp:CheckBoxList ID="p_FSectors" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                    RepeatColumns="8" Enabled="false">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程等级：
            </td>
            <td width="12%">
                <asp:DropDownList ID="p_FLevel" runat="server" CssClass="m_txt" Width="90px" Enabled="false">
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                抗震设防烈度：
            </td>
            <td width="12%">
                <asp:TextBox ID="p_FIntensity" runat="server" CssClass="m_txt" Width="80px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                结构类型：
            </td>
            <td width="15%">
                <asp:DropDownList ID="p_FStruType" runat="server" CssClass="m_txt" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="sz_2">
            <td class="t_r t_bg">
                建筑面积：
            </td>
            <td>
                <asp:TextBox ID="p_FArea" runat="server" CssClass="m_txt" Enabled="false" Width="60px"></asp:TextBox>(m2)
            </td>
            <td class="t_r t_bg">
                建筑高度：
            </td>
            <td>
                <asp:TextBox ID="p_FHeight" runat="server" CssClass="m_txt" Enabled="false" Width="60px"></asp:TextBox>(m)
            </td>
            <td class="t_r t_bg">
                建筑层数<br />
                (地上/地下)：
            </td>
            <td>
                <asp:TextBox ID="p_FLayers" runat="server" CssClass="m_txt" Width="70px" Enabled="false"></asp:TextBox><br />
                <asp:TextBox ID="p_FGround" runat="server" Width="30px" CssClass="m_txt" Enabled="false"></asp:TextBox>
                <asp:TextBox ID="p_FUnderground" runat="server" Width="30px" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_c t_bg" colspan="8">
                审查情况
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg">
                审查专业
            </td>
            <td class="t_c t_bg">
                审查结论
            </td>
            <td class="t_c t_bg">
                复审次数
            </td>
            <td class="t_c t_bg">
                违强条数
            </td>
            <td class="t_c t_bg">
                审查人
            </td>
            <td class="t_c t_bg">
                审核人
            </td>
            <td class="t_c t_bg" colspan="2">
                违反工程建设强制性<br />
                标准编号及条文编号
            </td>
        </tr>
        <asp:Repeater ID="rep_Emp" runat="server" OnItemDataBound="rep_File_ItemDataBound">
            <ItemTemplate>
                <tr class="m_dg1_i">
                    <td class="t_c">
                        <%#Eval("FAppMajor")%>
                    </td>
                    <td class="t_c">
                        <%#Eval("FResult")%>
                    </td>
                    <td class="t_c">
                        <%#Eval("FCount")%>
                    </td>
                    <td class="t_c">
                        <%#Eval("FOrder")%>
                    </td>
                    <td class="t_c">
                        <%#Eval("FName")%>
                    </td>
                    <td class="t_c">
                        <%#Eval("FTxt4")%>
                    </td>
                    <td class="t_l" colspan="2">
                        <%#Eval("FTxt3")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="2" class="t_r t_bg" style='color: Red'>
                审查合格书编号：
            </td>
            <td colspan="4">
                <asp:TextBox ID="t_Ftxt1" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td colspan="1" class="t_r t_bg" style='color: Red'>
                审查报告编号：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_Ftxt2" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="t_r t_bg" style='color: Red'>
                勘察设计单位整改情况：
            </td>
            <td colspan="6">
                <asp:DropDownList ID="t_FInt1" runat="server">
                    <asp:ListItem Value="1">已整改</asp:ListItem>
                    <asp:ListItem Value="0">未整改</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_c t_bg" colspan="2">
                备案情况
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                审查结论：
            </td>
            <td class="t_l">
                <asp:TextBox ID="txtFResult" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                审查受理时间：
            </td>
            <td class="t_l">
                <asp:TextBox ID="t_FDate1" runat="server" CssClass="m_txt" onfocus="WdatePicker();"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                出具审查合格书时间：
            </td>
            <td class="t_l">
                <asp:TextBox ID="t_FDate2" runat="server" CssClass="m_txt" onfocus="WdatePicker();"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查中发现的重大问题：
            </td>
            <td class="t_l">
                <asp:TextBox ID="t_FTxt16" runat="server" CssClass="m_txt" TextMode="MultiLine" Height="60"
                    Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                其他问题：
            </td>
            <td class="t_l">
                <asp:TextBox ID="t_FTxt17" runat="server" CssClass="m_txt" TextMode="MultiLine" Height="60"
                    Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                处理意见：
            </td>
            <td class="t_l">
                <asp:TextBox ID="txtFContent" runat="server" CssClass="m_txt" Enabled="false" TextMode="MultiLine"
                    Height="60" Width="500px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="p_FAddressDept" type="hidden" runat="server" />
    <input id="p_FType" type="hidden" runat="server" />
    <input id="p_FId" type="hidden" runat="server" />
    </form>
</body>
</html>
