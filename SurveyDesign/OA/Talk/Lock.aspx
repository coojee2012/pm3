<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lock.aspx.cs" Inherits="OA_Talk_Lock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>解锁</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" class="m_title" width="98%">
        <tr>
            <th colspan="2">
                解锁
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg" width="80">
                标题：
            </td>
            <td>
                <asp:Literal ID="t_FTalkName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                发起者：
            </td>
            <td>
                <asp:Literal ID="t_userName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系方式：
            </td>
            <td>
                <asp:Literal ID="t_FLinkWay" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                口令：
            </td>
            <td>
                <asp:TextBox ID="t_FKey" runat="server" CssClass="m_txt" Width="110px" MaxLength="8"
                    TextMode="Password"></asp:TextBox>
                <tt>* 您可以联系发起者索要口令</tt>
            </td>
        </tr>
        <tr>
            <td class="t_c t_bg" colspan="2">
                <asp:Button ID="btnOK" runat="server" CssClass="m_btn_w4" OnClick="btnOK_Click" Text="解锁" />
                <input type="button" value="放弃" class="m_btn_w4" onclick="window.close();" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
