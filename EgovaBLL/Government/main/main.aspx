<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="Government_main_main"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首页</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../script/mytable.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../zDialogNew/zDialog.js"></script>

    <script src="../chart/FusionCharts.js" type="text/javascript"></script>

    <script type="text/javascript">
        var MUSTVIEW = "<%=mustView%>"; //必需板块
        var IsR = false;
        $(document).ready(function() {
            _upc();
        });

        function mytalbeRest() {
            if (confirm('确认恢复桌面缺省设置么？')) {
                window.location = "../maintable/dragconfig.aspx?sysId=" + $("#hidd_sysId").val();
            }
        }
        function setmytable() {
            var diag = new Dialog(); diag.Modal = false;

            diag.Width = 600;
            diag.Height = 400;
            //diag.CancelEvent = function() { show(); diag.close(); };
            diag.Title = "自义定首页";
            diag.URL = "../maintable/setMyTable.aspx?sysId=" + $("#hidd_sysId").val();
            diag.show();
        }

        function show() {
            if (IsR) {
                window.location.href = window.location.href;
            }
        }
        //调用ajax时，每次 UpdatePanel局部刷新之后执行一次jQuery初始化代码
        function load() {
            try {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(_upc);
            } catch (e) { }
        }
    </script>

    <style type="text/css">
        body
        {
            height: 100%;
            width: 100%;
        }
        .d
        {
            height: 100%;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="d  bodycolor" <%=tablePhoto!=String.Empty ? "style=\"background-image:url(" + tablePhoto + ")\"" : "" %>>
                <tr>
                    <td style="vertical-align: top;">
                        <table width="98%" height="100%" border="0" cellpadding="1" cellspacing="0" align="center">
                            <tr>
                                <td colspan="2" style="text-align: right;">
                                    <a href="javascript:mytalbeRest();" style="font-size: 12px">缺省设置</a> &nbsp; <a href="javascript:setmytable();"
                                        style="font-size: 12px">自定义首页</a>
                                </td>
                            </tr>
                            <tr>
                                <td id="col_l" style="width: 100%" valign="top">
                                    <asp:PlaceHolder ID="plhLeft" runat="server"></asp:PlaceHolder>
                                    <br />
                                </td>
                                <td id="col_r" style="width: 0px" valign="top">
                                    <asp:PlaceHolder ID="plhRight" runat="server"></asp:PlaceHolder>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <input id="hidd_sysId" runat="server" type="hidden" />
            <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Style="display: none;" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnQuery" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
