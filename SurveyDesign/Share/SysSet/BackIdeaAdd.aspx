<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackIdeaAdd.aspx.cs" Inherits="Admin_main_BackIdeaAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>打回意见维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript"> 
            function CheckInfo() {
                return AutoCheckInfo(); //自动验证
            } 
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th>
                打回意见维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" cellspacing="0" align="center">
        <tr>
            <td class="t_r">
                所属平台：
            </td>
            <td>
                <asp:DropDownList ID="t_FPlatId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FPlatId_SelectedIndexChanged">
                </asp:DropDownList><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                所属系统：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server">
                </asp:DropDownList><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server">
                    <asp:ListItem Text="公开" Value="0"></asp:ListItem>
                    <asp:ListItem Text="个人" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" onblur="isInt(this)" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                内容：
            </td>
            <td>
                <asp:TextBox ID="t_FContent" TextMode="MultiLine" runat="server" CssClass="m_txt"
                    Height="69px" Width="241px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
