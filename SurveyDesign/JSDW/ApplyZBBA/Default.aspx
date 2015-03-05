<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="JSDW_ApplyZBBA_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>        
        &nbsp;&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" OnCommand="Button1_Command" />
        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;
        <br />
    </div>
    </form>
</body>
</html>
