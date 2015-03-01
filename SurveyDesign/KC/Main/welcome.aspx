<%@ Page Language="C#" AutoEventWireup="true" CodeFile="welcome.aspx.cs" Inherits="Enterprise_main_welcome" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <asp:Link id="skin1" runat="server">
    </asp:Link>
    
</head>
<body>
    <form id="form1" runat="server">
 <ext:ResourceManager ID="ResourceManager1" runat="server" DirectMethodNamespace="X"
        IDMode="Explicit" />
    <ext:Viewport ID="Viewport1" runat="server">
        <Items>
            <ext:RowLayout ID="RowLayout1" runat="server">
                <Rows>
                    <ext:LayoutRow RowHeight="1">
                        <ext:TabPanel ID="TabPanel1" im Border="false" IconCls="icon-application" TabCls="TabCls"
                            Cls="TabCls"  BodyCssClass="blank_04" EnableTabScroll="true" MinTabWidth="85" runat="server" Region="Center">
                            <Defaults>
                            <ext:Parameter> </ext:Parameter>
                            </Defaults> 
                            <Plugins>
                                <ext:TabCloseMenu ID="TabCloseMenu1" runat="server" />
                            </Plugins>
                        </ext:TabPanel>
                    </ext:LayoutRow>
                </Rows>
            </ext:RowLayout>
        </Items>
    </ext:Viewport>
    <input id="firstName" runat="server" type="hidden" />
    <script type="text/javascript">
        if (window.location.href.indexOf("#") > 0) {
            var directLink = window.location.href.substr(window.location.href.indexOf("#") + 1);
            Ext.onReady(function() {
                if (!Ext.isEmpty(directLink, false)) {
                    addTab(directLink, getRequest(window.location.href, "num"), document.getElementById("firstName").value);
                }
            }, window, { delay: 100 });
        }
    </script>
    </form>
</body>
</html>
