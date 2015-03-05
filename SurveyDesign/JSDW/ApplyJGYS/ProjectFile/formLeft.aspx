<%@ Page Language="C#" AutoEventWireup="true" CodeFile="formLeft.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_formLeft" %>

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
        $(document).ready(function () {
            $("#showLeft").click(function () {//显示菜单
                $("#t_show").hide("fast", function () {
                    $("#t_menu").show("slow");
                    var fs = window.parent.document.getElementById("trleft");
                    fs.cols = "191,*";
                });
            });
            $("#getLeft").click(function () {//隐藏菜单
                $("#t_menu").hide("slow", function () {
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
            $(".l_m_a,.l_m_h_a").click(function () {
                $(".l_m_h_a").attr("class", "l_m_a");
                $(this).attr("class", "l_m_h_a");
            });

        });
        function showMenu() {
            $("#t_show").hide("fast", function () { parent.trleft.cols = "191,*"; $("#t_menu").show("slow"); });
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
                        <a id="a61" class="o_m01_1" style="cursor: pointer"><span>项目环节材料</span></a>
                        <div class="o_smenu">
                            <a class="o_m02_1" href='XZYJSForm.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>&audit=1' target="appMain" style="cursor: pointer"><span>选址意见书</span></a>
                            <a class="o_m02_1" href='YDGHForm.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>&audit=1' target="appMain" style="cursor: pointer"><span>建设用地规划许可证</span></a>
                            <a class="o_m02_1" href='GCGHForm.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>&audit=1' target="appMain" style="cursor: pointer"><span>建设工程规划许可证</span></a>
                            <a class="o_m02_1" href='ZTBList.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>&audit=1' target="appMain" style="cursor: pointer"><span>招投标信息</span></a>
                            <a class="o_m02_1" href='HTBAList.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>&audit=1' target="appMain" style="cursor: pointer"><span>合同备案</span></a>
                            <a class="o_m02_1" href='SGTSCXXForm.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>&audit=1' target="appMain" style="cursor: pointer"><span>施工图审查信息</span></a>
                            <a class="o_m02_1" href='SGXKZList.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>&audit=1' target="appMain" style="cursor: pointer"><span>施工许可证</span></a>
                            <a class="o_m02_1" href='QTForm.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>&audit=1' target="appMain" style="cursor: pointer"><span>其他资料</span></a>
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
