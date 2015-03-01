<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogDetail.aspx.cs" Inherits="Admin_main_LogDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>操作日志详情</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });       
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                操作日志详情
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <input id="btnClose" type="button" value="返回" class="m_btn_w2" language="javascript"
                    onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                记录编号：
            </td>
            <td>
                <asp:TextBox ID="t_FId" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                记录日期：
            </td>
            <td>
                <asp:TextBox ID="t_Title" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                数据库名称：
            </td>
            <td>
                <asp:TextBox ID="t_FDbName" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                操作人员：
            </td>
            <td>
                <asp:TextBox ID="t_FUserId" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                记录内容：
            </td>
            <td style="height: 3px">
                <asp:TextBox ID="t_Content" runat="server" CssClass="m_txt" Height="265px" ReadOnly="True"
                    TextMode="MultiLine" Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                错误信息：
            </td>
            <td>
                <asp:TextBox ID="t_errmsg" runat="server" CssClass="m_txt" ReadOnly="True" Height="243px"
                    TextMode="MultiLine" Width="500px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
