<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleAdd.aspx.cs" Inherits="Share_Sys_RoleAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>菜单角色</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
        function exitt() { //返回
            if ($("#HSaveResult").val() == "1")
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
                角色维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="exitt();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                所属平台：
            </td>
            <td>
                <asp:DropDownList ID="t_FPlatId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FPlatId_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属系统：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="trParentName" runat="server">
            <td class="t_r t_bg">
                父角色名称：
            </td>
            <td>
                <asp:TextBox ID="text_FParentName" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr id="trParentNumber" runat="server">
            <td class="t_r t_bg">
                父角色编码：
            </td>
            <td>
                <asp:TextBox ID="text_FParentNumber" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                <span style="color: #ff0000">*</span>角色名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                <span style="color: #ff0000">*</span>角色编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                角色类别：
            </td>
            <td>
                <asp:TextBox ID="t_FMTypeId" runat="server" CssClass="m_txt" onblur="isInt(this);"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                显示顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt" onblur="isInt(this);"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
