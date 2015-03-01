<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FastLogon.aspx.cs" Inherits="WYDW_FastLogon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ul>
                <li>UerID：<asp:RadioButtonList runat="server" ID="rblUsers" AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rblUsers_SelectedIndexChanged">
                    <asp:ListItem Value="C8F99506-EF9C-479C-A9D5-F86004520B4B">超级用户</asp:ListItem>
                    <asp:ListItem  Value="ad67d397-ef4f-4573-83fc-5f09b93ad6b2">四川宏德建设有限公司（物业企业）</asp:ListItem>
                    <asp:ListItem Value="509feaa4-79f4-40b6-9d74-daf71f26ce80">成都房管局（陈婷婷）</asp:ListItem>
                    <asp:ListItem Value="d5d6706e-5232-48cc-856a-3a50721d24d4">蒲江房管局（01510131）</asp:ListItem>
                    <asp:ListItem Value="c0749a2a-9b23-480a-8b4e-b1cf167c882d">唐宁（唐宁）</asp:ListItem>
                    <asp:ListItem Value="0">单独录入</asp:ListItem>
                </asp:RadioButtonList>
                    <asp:TextBox runat="server" Visible="false" ID="txtUserID"></asp:TextBox>
                </li>
                <li>登陆默认页：<asp:RadioButtonList runat="server" ID="rblGoTo" RepeatDirection="Horizontal" RepeatLayout="Flow">

                    <asp:ListItem Value="0" Selected="True">主页</asp:ListItem>
                </asp:RadioButtonList></li>
                <li>
                    <asp:Button Text="快速登陆" ID="btnLogon" runat="server" OnClick="btnLogon_Click" /></li>
            </ul>

        </div>
    </form>
</body>
</html>
