<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aLeft.aspx.cs" Inherits="Share_Main_aLeft" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#showLeft").click(function() {//隐藏菜单
                $("#t_show").hide("fast", function() { parent.trleft.cols = "192,*"; $("#t_menu").show("slow"); });
            });
            $("#getLeft").click(function() {//显示菜单
                $("#t_menu").hide("slow", function() { parent.trleft.cols = "18,*"; $("#t_show").show("normal"); });
            });
        });
        function showMenu() {
            $("#t_show").hide("fast", function() { parent.trleft.cols = "192,*"; $("#t_menu").show("slow"); });
        }


        //火狐、IE滚动条，主要用于左则菜单 
        //<body style="height: 100%" onresize="AUTOoverflow('div_menu');" onload="AUTOoverflow('div_menu');">
        function AUTOoverflow(objId) {
            var one = document.getElementById(objId); //需要滚动条的DIV高
            one.style.height = window.document.body.clientHeight - 30; //-40：可根据需要时调整
        }
    </script>

</head>
<body style="height: 100%" onload="AUTOoverflow('div_menu');" onresize="AUTOoverflow('div_menu');">
    <form id="form1" runat="server">
    <table id="t_menu" border="0" cellpadding="0" cellspacing="0" height="100%" class="l_table">
        <tr>
            <td class="l_top">
                <div class="f_l">
                    <asp:Label ID="l_title" runat="server"></asp:Label>
                </div>
            </td>
            <td rowspan="2" width="9px" id="getLeft">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="l_main" valign="top" align="left">
                <div style="height: 100%; overflow: auto; width: 180px;" id="div_menu">
                    <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
                    </asp:TreeView>
                </div>
            </td>
        </tr>
    </table>
    <table id="t_show" border="0" cellpadding="0" cellspacing="0" height="100%">
        <tr>
            <td id="showLeft" width="9px">
            </td>
        </tr>
    </table>
    <input id="HKINDID" runat="server" type="hidden" value="155001" />
    </form>
</body>
</html>
