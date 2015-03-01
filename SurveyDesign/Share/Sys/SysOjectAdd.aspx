<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysOjectAdd.aspx.cs" ValidateRequest="false"
    Inherits="Share_sys_SysOjectAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统变量维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });

        function CheckInfo() {
            return AutoCheckInfo();
        }

        function ifSaveOk() {
            if ($("#HSaveResult").attr("value") == "1")
                window.returnValue = "1";
            window.close();
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                系统变量维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />&nbsp;
                <input id="btnBack" class="m_btn_w2" type="button" value="返回" onclick="ifSaveOk();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                变量名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="286px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                变量编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" CssClass="m_txt" Width="155px" MaxLength="50"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                变量内容：
            </td>
            <td>
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Height="124px" TextMode="MultiLine"
                    Width="500px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                变量说明：
            </td>
            <td>
                <asp:TextBox ID="t_FMemo" runat="server" CssClass="m_txt" Height="133px" TextMode="MultiLine"
                    MaxLength="2000" Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt" onblur="isInt(this);"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
