<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Baseinfo1.aspx.cs" Inherits="SJ_Main_Baseinfo1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                单位基本信息管理
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
            </td>
            <td class="t_r">
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                单位名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="303px" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                注册地址：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="t_FRegistAddress" runat="server" CssClass="m_txt" Width="224px"
                    MaxLength="75" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                组织机构代码：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FJuridcialCode" runat="server" CssClass="m_txt" MaxLength="10"
                    ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td align="center" class="t_r t_bg" height="21" nowrap="nowrap">
                法定代表人
            </td>
            <td class="txt31">
                <asp:TextBox ID="t_FOTxt5" runat="server" CssClass="m_txt" MaxLength="20" ReadOnly="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                单位联系人：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                联系人手机：
            </td>
            <td>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt" MaxLength="15" onblur="isInt(this);"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                EMail：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FEMail" runat="server" CssClass="m_txt" MaxLength="30" Width="302px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
