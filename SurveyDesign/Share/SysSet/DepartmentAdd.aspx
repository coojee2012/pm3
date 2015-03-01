<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentAdd.aspx.cs" Inherits="Share_SysSet_DepartmentAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EGBP基础数据平台</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <meta http-equiv="x-ua-compatible" content="ie=7" />

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        function CheckInfo() {
            if (document.getElementById("t_FName").value.trim() == "") {
                alert("部门名称必须填写");
                document.getElementById("t_FName").focus();
                return false;
            }
            if (document.getElementById("t_FNumber").value.trim() == "") {
                alert("部门编码必须填写");
                document.getElementById("t_FNumber").focus();
                return false;
            }
            return true;
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                政府部门维护 </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <%--<asp:Button ID="btnNew" runat="server" CssClass="cBtn3" Text="新增" OnClick="btnNew_Click" />--%>
                <input id="btnBack" class="m_btn_w2" onclick="javascript:window.close();" type="button"
                    value="返回" />&nbsp;&nbsp;&nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr id="trParentName" runat="server">
            <td class="t_r t_bg">
                上级部门名称：
            </td>
            <td>
                <asp:TextBox ID="text_FParentName" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr id="trParentNumber" runat="server">
            <td class="t_r t_bg">
                上级部门编码：
            </td>
            <td>
                <asp:TextBox ID="text_FParentNumber" runat="server" CssClass="m_txt" onblur="isInt(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                <span style="color: #ff0000">*</span>部门名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                <span style="color: #ff0000">*</span>部门编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" onblur="isInt(this);" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                国家编码：
            </td>
            <td>
                <asp:TextBox ID="t_FCNumber" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                管理部门级别：
            </td>
            <td>
                <asp:DropDownList ID="t_FLevel" runat="server" CssClass="m_txt">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                管理部门类别：
            </td>
            <td>
                <asp:DropDownList ID="t_FClassNumber" runat="server" CssClass="m_txt">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否扩权县：
            </td>
            <td>
                <asp:DropDownList ID="t_FIsTown" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="false">不是</asp:ListItem>
                    <asp:ListItem Value="true">是</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                全称：
            </td>
            <td>
                <asp:TextBox ID="t_FFullName" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
