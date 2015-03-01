<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KZSFInfo.aspx.cs" Inherits="KcsjSgt_ApplyOther_KZSFInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>抗震设防审查表</title>
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
            var url = "PrjSelList.aspx?type=kzsf";
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
                抗震设防审查表
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
            <td colspan="1">
                <asp:TextBox ID="t_FPrjName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                <asp:Button runat="server" ID="btnSel" Text="选择.." CssClass="m_btn_w4" OnClientClick="return selPrj(this)"
                    UseSubmitBehavior="false" OnClick="btnSel_Click" />
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                工程编号：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FPrjNo" runat="server" CssClass="m_txt" Width="150px" Enabled="false"></asp:TextBox>
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
                建设单位：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Enabled="false" Width="200px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                设计单位：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_SJDW" runat="server" CssClass="m_txt" Enabled="false" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                抗震设防分类标准：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_FAntiSeismic" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                结构类型：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_FStruType" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                总建筑面积：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FArea" runat="server" CssClass="m_txt t_r" onblur="isFloat(this)"
                    Width="70px" MaxLength="8"></asp:TextBox>(㎡)
            </td>
            <td class="t_r t_bg">
                层数：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FLayers" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"
                    MaxLength="9"></asp:TextBox>
            (层)
        </tr>
        <tr>
            <td class="t_r t_bg">
                设防烈度：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FIntensity" runat="server" CssClass="m_txt t_r" MaxLength="30"
                    Width="70px" onblue="isFloat(this)"></asp:TextBox>(度)
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                结构设计负责人：
            </td>
            <td>
                <asp:TextBox ID="t_FLeader1" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                建筑设计负责人：
            </td>
            <td>
                <asp:TextBox ID="t_FLeader2" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查意见：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FAppIdea" runat="server" CssClass="m_txt" Width="500px" Height="100px"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查结论：
            </td>
            <td colspan="3">
                <asp:CheckBox runat="server" ID="t_FInt1" Text="符合抗震要求" />
                <asp:CheckBox runat="server" ID="t_FInt2" Text="基本符合抗震要求" /><asp:CheckBox runat="server"
                    ID="t_FInt3" Text="修改后符合抗震要求" />
                <asp:CheckBox runat="server" ID="t_FInt4" Text="修改后基本符合抗震要求" />
                <asp:CheckBox runat="server" ID="t_FInt5" Text="不符合抗震要求" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查专家组：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt1" runat="server" CssClass="m_txt" Width="120px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt2" runat="server" CssClass="m_txt" Width="120px" MaxLength="10"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                审查日期：
            </td>
            <td>
                <asp:TextBox ID="t_FDate1" runat="server" CssClass="m_txt" Width="120px" onfocus="WdatePicker();"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审核人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt3" runat="server" CssClass="m_txt" Width="120px" MaxLength="10"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                备注：
            </td>
            <td>
                <asp:TextBox ID="t_FRemark" runat="server" CssClass="m_txt" Width="120px" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="t_FAddressDept" type="hidden" runat="server" />
    <input id="txtFLinkId" type="hidden" runat="server" />
    </form>
</body>
</html>
