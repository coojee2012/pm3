<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Clear.aspx.cs" Inherits="Clear" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <asp:link runat="server"  href="a" id="a1"></asp:link>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="清除缓存" /></div>
        <div id="divDt">
           <asp:Button ID="Button2" runat="server" Text="同步建设单位" onclick="Button2_Click" />
        </div>
        
    </form>
</body>
</html>
