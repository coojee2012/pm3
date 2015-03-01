<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Right.aspx.cs" Inherits="Government_AppMain_Right" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <ext:ResourcePlaceHolder ID="ResourcePlaceHolder1" runat="server" Mode="Script" />
    <ext:ResourcePlaceHolder ID="ResourcePlaceHolder2" runat="server" Mode="Style" />

    <script type="text/javascript" src="../../zDialogNew/zDialog.js"></script>

    <link rel="stylesheet" type="text/css" href="../../resources/css/main.css" />

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../zDialogNew/zDialog.js" type="text/javascript"></script>

    <script src="../../zDialogNew/zDrag.js" type="text/javascript"></script>

    <script type="text/javascript">
        Ext.ns("X");
        var addTab = function(url, id, title) {
            if (url) {

                if (id == "-") {

                    X.GetHashCode(url, {
                        success: function(result) {
                            addTab(url, "e" + result, title);
                        }
                    });

                    return;
                }
                var tab = TabPanel1.getComponent(id);

                if (!tab) {
                    tab = TabPanel1.add({
                        id: id,
                        title: title,
                        iconCls: "icon-application",
                        closable: true,
                        autoLoad: {
                            showMask: true,
                            url: url,
                            mode: "iframe",
                            maskMsg: "正在加载 ..."
                        }
                    });
                    tab.on("activate", function() {

                    }, this);
                }
                else {
                    tab.load(url);
                }
                TabPanel1.setActiveTab(tab);

            }
        }
  
    </script>

    <asp:Link id="skin1" runat="server">
    </asp:Link>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" DirectMethodNamespace="X"
        IDMode="Explicit" />
    <ext:Viewport ID="Viewport1" runat="server" >
        <Items>
            <ext:RowLayout ID="RowLayout1" runat="server">
                <Rows>
                    <ext:LayoutRow RowHeight="1">
                        <ext:TabPanel ID="TabPanel1" imBorder="false" IconCls="icon-application" TabCls="TabCls"
                            Cls="TabCls" EnableTabScroll="true" runat="server" Region="Center" BodyCssClass="blank_04" >
                            <Defaults>
                                <ext:Parameter>
                                </ext:Parameter>
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
    </form>

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

</body>
</html>
