<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="KcsjSgt_ApplyKCCXXSC_ApplyBaseInfo" %>

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
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                合同备案基本信息
            </th>
        </tr>
    </table>
    
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                工程编号：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_FPrjNo" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地点：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="p_FAllAddress" runat="server" CssClass="m_txt" Width="224px" MaxLength="30"
                    Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同备案单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FName" runat="server" CssClass="m_txt" ReadOnly="true" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="e_FLinkMan" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="e_FTel" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                Email：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FAddress" runat="server" CssClass="m_txt" ReadOnly="true" Width="250px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" width="250">
                合同备案内容及范围（100字内）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt10" runat="server" CssClass="m_txt" Width="600px" Height="30px"
                    TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同备案承诺和要求（500字内）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt11" runat="server" CssClass="m_txt" Width="600px" Height="80px"
                    TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                受委施工图审查机构信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                单位名称：
            </td>
            <td class="t_l t_bg" colspan="3">
                <asp:TextBox ID="s_FName" runat="server" CssClass="m_txt" Width="300px" ReadOnly="true"></asp:TextBox>
                <input id="s_FBaseInfoId" type="hidden" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="s_FLevelName" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td>
                <asp:TextBox ID="s_FCertiNo" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同金额：
            </td>
            <td>
                <asp:TextBox ID="s_FMoney" runat="server" CssClass="m_txt" MaxLength="15" onblur="isFloat(this);"
                    ReadOnly="true"></asp:TextBox>(万元)
            </td>
            <td class="t_r t_bg">
                要求完成日期：
            </td>
            <td>
                <asp:TextBox ID="s_FPlanDate" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="p_FAddressDept" type="hidden" runat="server" />
    </form>
</body>
</html>
