<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="JSDW_ApplyKCWJSCWT_ApplyBaseInfo" %>

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
            return AutoCheckInfo();
        }
        function selEnt(tagId, fsysid, obj, hasCerti) {
            var url = "EntListSel.aspx";
            if (hasCerti == "1")
                url = "EntListSelCerti.aspx";
            var pid = showWinByReturn('../appmain/' + url + '?fsysid=' + fsysid + '&fadd=' + $("#p_FAddressDept").val(), 700, 500);
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

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                合同基本信息
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
                工程名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                工程编号：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_FPrjNo" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
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
                合同提交单位：
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
                Email：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FAddress" runat="server" CssClass="m_txt" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" width="22%">
                合同内容及范围（100字内）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt10" runat="server" CssClass="m_txt" Width="500px" Height="30px"
                    TextMode="MultiLine"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同承诺和要求（500字内）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt11" runat="server" CssClass="m_txt" Width="500px" Height="80px"
                    TextMode="MultiLine"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                施工图审查机构信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                单位名称：
            </td>
            <td class="t_l t_bg" colspan="3">
                <asp:TextBox ID="s_FName" runat="server" CssClass="m_txt" Width="300px" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
                <input id="s_FBaseInfoId" type="hidden" runat="server" />
                <asp:Button ID="btnMod" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEnt('s_FBaseInfoId',1451,this,'0');"
                    UseSubmitBehavior="false" CommandName="SGT" OnClick="btnSel_Click" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="s_FLevelName" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td>
                <asp:TextBox ID="s_FCertiNo" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同金额：
            </td>
            <td>
                <asp:TextBox ID="s_FMoney" runat="server" CssClass="m_txt" MaxLength="15" onblur="isFloat(this);"></asp:TextBox>(万元)
                
            </td>
            <td class="t_r t_bg">
                要求完成日期：
            </td>
            <td>
                <asp:TextBox ID="s_FPlanDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <input id="p_FAddressDept" type="hidden" runat="server" />
    </form>
</body>
</html>
