<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlatformAdd.aspx.cs" Inherits="Share_SysSet_PlatformAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>应用平台维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        function CheckInfo() {
            return AutoCheckInfo(); //自动验证
        }
        function exitt() { //返回
            if ($("#HSaveResult").val() == "1")
                window.returnValue = "1";
            window.close();
        }
        $(document).ready(function() {
            txtCss(); //文本框样式
        });
        function ifSaveOk() {
            var HSaveResult = document.getElementById("HSaveResult");
            if (HSaveResult) {
                window.returnValue = HSaveResult.value;
            }
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
                应用平台维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="ifSaveOk();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="98%" class="m_table" align="center">
        <tr>
            <td class="t_r t_bg">
                平台名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                平台编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" onblur="isInt(this)" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" onblur="isInt(this)"  runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                说明：
            </td>
            <td>
                <asp:TextBox ID="t_FDesc" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                查询地址：
            </td>
            <td>
                <asp:TextBox ID="t_FQurl" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                状态：
            </td>
            <td>
                <asp:DropDownList ID="t_FState" runat="server">
                    <asp:ListItem Value="1" Text="有效"></asp:ListItem>
                    <asp:ListItem Value="0" Text="无效"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
