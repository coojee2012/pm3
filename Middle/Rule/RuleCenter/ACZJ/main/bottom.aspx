<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bottom.aspx.cs" Inherits="Goverment_main_bottom" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>四川省施工企业系统--安全自检单位版</title>
 <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css"></asp:Link>
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
