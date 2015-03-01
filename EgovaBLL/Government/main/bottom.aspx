<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bottom.aspx.cs" Inherits="Government_AppMain_bottom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server"></asp:Link>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bot_b">
        技术支持：
        <asp:Literal ID="liC_TechSupport" runat="server"></asp:Literal>
        电话：
        <asp:Literal ID="liC_webtel" runat="server"></asp:Literal>
        &nbsp;
        <asp:Literal ID="liC_Developer" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
