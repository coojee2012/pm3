<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aleft.aspx.cs" Inherits="EvaluateEntApp_main_left1" %>

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

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

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


            //if ($(".l_m_a,.l_m_h_a").length > 0) {
            //    var href = $(".l_m_a,.l_m_h_a").first().attr("href");
            //    var fs = window.parent.document.getElementsByName("main")[0].src = href;
            //    $(".l_m_a,.l_m_h_a").first().attr("class", "l_m_h_a");
            //}
            //$(".l_m_a,.l_m_h_a").click(function() {
            //    $(".l_m_h_a").attr("class", "l_m_a");
            //    $(this).attr("class", "l_m_h_a");
            //});
            //$(".l_menu").click(function () {
            //    var smenu = $(this).children(".l_smenu");
            //    if (smenu.length>0)
            //    {
            //        smenu.show();
            //    }
            //});
            $("a[href]").click(function () { //点击切换样式
                if ($(this).attr("href") != "#") {


                    //记录最后点击的菜单
                    if ($(this).attr("id") != "a155")
                        setCookie("_SYS_ENT_SGT_MENUID", $(this).attr("id"));
                }
            });
            $("a").css("cursor", "pointer");
            //跳转到cookie中的对应页面
            // $("a[id=" + getCookie("_SYS_ENT_SGT_MENUID") + "]").click();
            var ck = getCookie("_SYS_ENT_SGT_MENUID");
            if (!ck) {
                ck = "";
            }
            var s = $("a[id=" + ck + "]").attr("href");
            if (s)
                parent.main.location.href = s;


            //整理按钮样式
            $(".o_m01_1").each(function () {//一级
                if ($(this).find("+div").find(":parent").length > 0) {
                    $(this).find("+div:parent").hide();
                    $(this).attr("class", "o_m01_3");
                }
                else {
                    $(this).find("+div").hide();
                }
            });
            $(".o_m02_1").each(function () {//二级
                if ($(this).find("+div").find(":parent").length > 0) {
                    $(this).find("+div:parent").hide();
                    $(this).attr("class", "o_m02_3");
                }
            });
            //第有子菜单的展开
            $(".o_m01_3").find("+div:parent").first().show();
            //$(".o_m02_3").find("+div:parent").show();
            //子菜单展开和隐藏
            $(".o_m01_3").click(function () {//一级
                $(this).find("+div:parent").first().slideToggle("fast");
                if ($(this).attr("class") == "o_m01_3")
                    $(this).attr("class", "o_m01_4");
                else
                    $(this).attr("class", "o_m01_3");
                $(this).blur(); //转移焦点
            });
            $(".o_m02_3").click(function () {//二级
                $(this).find("+div:parent").first().slideToggle("fast");
                if ($(this).attr("class") == "o_m02_3")
                    $(this).attr("class", "o_m02_4");
                else
                    $(this).attr("class", "o_m02_3");
                $(this).blur(); //转移焦点
            });
            //点击没有子菜单
            $(".o_m01_1").click(function () {//一级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(this).attr("class", "o_m01_2");
                $(this).blur(); //转移焦点
            });
            $(".o_m02_1").click(function () {//二级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(this).attr("class", "o_m02_2");
                $(this).blur(); //转移焦点
            });
            $(".o_m03_1").click(function () {//三级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(this).attr("class", "o_m03_2");
                $(this).blur(); //转移焦点
            });
            //去除有子菜单的节点的href属性,防止点击一级菜单开关二级菜单时也访问空url，导致右边main页面的错误
            $(".o_smenu").each(function () {
                var a = $(this).prev();
                if (a.length > 0) {
                    a.removeAttr("href");
                }
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
                    <asp:Repeater ID="rep_Menu" runat="server" OnItemDataBound="rep_Menu_ItemDataBound">
                        <ItemTemplate>
                            <%--<div class="l_menu">
                                <a class="l_m_a" href='<%#Eval("FQUrl") %>' target='<%#Eval("FTarget") %>'><span>
                                    <%#Eval("FName")%></span></a>--%>
                            <div class="o_menu" >
                                <a  class="o_m01_1" href='<%#Eval("FQUrl") %>' target='<%#Eval("FTarget") %>'>
                                    <span><%#Eval("FName")%></span>
                                </a>
                                <asp:Repeater ID="rep_SubMenu" runat="server">
                                    <HeaderTemplate>
                                        <div class="o_smenu">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--<div class="l_smenu">
                                            <a class="l_m_a" href='<%#Eval("FQUrl") %>' target='<%#Eval("FTarget") %>'><span>
                                                <%#Eval("FName")%></span></a>
                                        </div>--%>
                                        <a  class="o_m02_1" href='<%#Eval("FQUrl") %>' target='<%#Eval("FTarget") %>'>
                                            <span><%#Eval("FName")%></span>
                                        </a>
                                        <div class="o_smenu2">
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>                                                                
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
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
