<%@ Page Language="C#" AutoEventWireup="true" CodeFile="onlineServices.aspx.cs" Inherits="Common_onlineServices" %>

<%@ Register Src="online.ascx" TagName="online" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线客服</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:online ID="online1" runat="server" />
    </form>
</body>
</html>
