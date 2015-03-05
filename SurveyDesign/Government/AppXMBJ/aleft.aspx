﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aleft.aspx.cs" Inherits="EvaluateEntApp_main_left1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        html, body, form { height: 100%; overflow: hidden; font-size: 12px; }
        body { padding: 0; margin: 0; }
        #main { height: 100%; text-align: center; }
    </style>

    <script src="../../../script/jquery.js" type="text/javascript"></script>

    <script src="../../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#showLeft").click(function() {//显示菜单
                $("#t_show").hide("fast", function() {
                    $("#t_menu").show("slow");
                    var fs = window.parent.document.getElementById("trleft");
                    fs.cols = "191,*";
                });
            });
            $("#getLeft").click(function() {//隐藏菜单
                $("#t_menu").hide("slow", function() {
                    $("#t_show").show("normal");
                    var fs = window.parent.document.getElementById("trleft");
                    fs.cols = "12,*";
                });
            });


            if ($(".l_m_a,.l_m_h_a").length > 0) {
                var href = $(".l_m_a,.l_m_h_a").first().attr("href");
                var fs = window.parent.document.getElementsByName("appMain")[0].src = href;
                $(".l_m_a,.l_m_h_a").first().attr("class", "l_m_h_a");
            }
            $(".l_m_a,.l_m_h_a").click(function() {
                $(".l_m_h_a").attr("class", "l_m_a");
                $(this).attr("class", "l_m_h_a");
            });

        });
        function showMenu() {
            $("#t_show").hide("fast", function() { parent.trleft.cols = "191,*"; $("#t_menu").show("slow"); });
        }
         
    </script>

</head>
<body style="height: 100%" onload="AUTOoverflow('div_menu');" onresize="AUTOoverflow('div_menu');">
    <form id="form1" runat="server">
    <table id="t_menu" border="0" cellpadding="0" cellspacing="0" height="100%" class="l_table">
        <tr>
            <td class="l_top">
                <div class="f_l">
                    <asp:Label ID="l_title" runat="server">业务菜单</asp:Label>
                </div>
            </td>
            <td rowspan="2" width="9px" id="getLeft">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="l_main" valign="top" align="left">
                <div class="l_main_bg" style="height: 100%; overflow: auto; width: 180px; text-align: center;"
                    id="div_menu">
                    <div class="o_menu">
                        <a id="a61" class="o_m01_1" style="cursor: pointer"><span>接件审批</span></a>
                        <div class="o_smenu">
                            <asp:Literal ID="ltrText" runat="server"></asp:Literal>
                            <%--<a id="a7" class="o_m02_1" href='XZAcceptList.aspx' target="appMain"><span>待接件</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a15" class="o_m02_1" href='FirstAuditAccept.aspx' target="appMain"><span>初审</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a1" class="o_m02_1" href='SecondAuditAccept.aspx' target="appMain"><span>复审</span></a>--%>
                        </div>
                    </div>
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
    </form>
</body>
</html>