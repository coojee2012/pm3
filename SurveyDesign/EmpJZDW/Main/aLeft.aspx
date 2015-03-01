<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aLeft.aspx.cs" Inherits="Admin_main_aLeft" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业备案管理系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        html, body, form
        {
            height: 100%;
            overflow: hidden;
            font-size: 12px;
        }
        body
        {
            padding: 0;
            margin: 0;
        }
        #main
        {
            height: 100%;
            text-align: center;
        }
    </style>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#showLeft").click(function() {//隐藏菜单
                $("#t_show").hide("fast", function() { parent.trleft.cols = "191,*"; $("#t_menu").show("slow"); });
            });
            $("#getLeft").click(function() {//显示菜单
                $("#t_menu").hide("slow", function() { parent.trleft.cols = "12,*"; $("#t_show").show("normal"); });
            });
            $("a").click(function() { //点击切换样式
                if ($(this).attr("href") != "#") {
                    $(".l_m_h_a").removeClass("l_m_h_a").addClass("l_m_a");
                    $(this).removeClass("l_m_a").addClass("l_m_h_a");
                }
                setCookie("_SYS_ENT_MENUINDEX", $(this).attr("id"));
            });

        });
        function showMenu() {
            $("#t_show").hide("fast", function() { parent.trleft.cols = "191,*"; $("#t_menu").show("slow"); });
        }
        
    </script>

</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" class="body">
    <form id="form1" runat="server">
    <table id="t_menu" border="0" cellpadding="0" cellspacing="0" height="100%" class="l_table">
        <tr>
            <td class="l_top">
                <div class="f_l" style="width: 125px;">
                    <asp:Label ID="l_title" runat="server">系统菜单</asp:Label>
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
                    <div class="l_menu">
                        <a id="a1" class="l_m_a" href='../Main/zWXTS.aspx' target="main"><span>温馨提示</span></a>
                    </div>
                    <div class="l_menu">
                        <a id="a2" class="l_m_a" href='../../KC/main/Message.aspx' target="main"><span>文件通知</span></a>
                    </div>
                    <div class="l_menu">
                        <a id="a3" class="l_m_a" href='../AppMain/Business.aspx' target="main"><span>见证管理</span></a>
                    </div>
                    <div class="l_menu">
                        <a id="a5" class="l_m_a" href='../AppMain/PersonInfoList.aspx' target="main"><span>参与项目统计</span></a>
                    </div>
                    <div class="l_menu">
                        <a id="a6" class="l_m_a" href='../../OA/Talk/TalkList.aspx' target="main"><span>审查交流</span></a>
                    </div>
                    <div class="l_menu">
                        <a id="a4" class="l_m_a" href='../../Common/EmpInfo.aspx?fUserId=<%=Session["FEmpID"] %>'
                            target="main"><span>个人信息维护</span></a>
                    </div>
                    <div class="l_menu">
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
    <input id="Hidden1" runat="server" type="hidden" value="155001" />
    </form>
</body>
</html>
