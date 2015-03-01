<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aLeft.aspx.cs" Inherits="WYDW_XMQK_aLeft" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        html, body, form {
            height: 100%;
            overflow: hidden;
            font-size: 12px;
        }

        body {
            padding: 0;
            margin: 0;
        }

        #main {
            height: 100%;
            text-align: center;
        }
    </style>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

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
            $("a[href]").click(function () { //点击切换样式
                if ($(this).attr("href") != "#") {


                    //记录最后点击的菜单
                    if ($(this).attr("id") != "a155")
                        setCookie("_SYS_ENT_SGT_MENUID", $(this).attr("id"));
                }
            });
            $("a").css("cursor", "pointer");
            //跳转到cookie中的对应页面
            $("a[id=" + getCookie("_SYS_ENT_SGT_MENUID") + "]").click();
            var s = $("a[id=" + getCookie("_SYS_ENT_SGT_MENUID") + "]").attr("href");
            if (s) {
                try {
                    parent.main.location.href = s;
                } catch (e) {

                }
            }


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
                <td rowspan="2" width="9px" id="getLeft">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="l_main" valign="top" align="left">
                    <div class="l_main_bg" style="height: 100%; overflow: auto; width: 180px; text-align: center;"
                        id="div_menu">
                        <div id="dv1" runat="server" class="o_menu" style="">
                            <a id="a1" class="o_m01_1" href='JBXXInfo.aspx' target="appMain"><span>项目基本信息</span></a>
                        </div>

                        <div class="o_menu">
                            <a id="a2" class="o_m01_1" href='KZXXInfo.aspx' target="appMain"><span>项目扩展信息</span></a>
                        </div>
                        <div class="o_menu">
                            <a id="a4" class="o_m01_1" href='HTBAInfo.aspx' style="cursor: pointer" target="appMain"><span>项目合同备案</span></a>
                        </div>
                        <div class="o_menu">
                            <a id="a6" class="o_m01_1" href='YWHInfo.aspx' style="cursor: pointer" target="appMain"><span>项目业委会备案</span></a>
                        </div>
                        <div class="o_menu">
                            <a id="a11" class="o_m01_1" href='RYQKList.aspx' style="cursor: pointer" target="appMain"><span>项目人员情况</span></a>
                        </div>
                        <div class="o_menu">
                            <a id="a8" class="o_m01_1" href='SFQKInfo.aspx' style="cursor: pointer" target="appMain"><span>人员配置及收费</span></a>
                        </div>
                        <div class="o_menu">
                            <a id="a3" class="o_m01_1" href='CWQKInfo.aspx' target="appMain"><span>项目财务年报</span></a>
                        </div>
                        <div class="o_menu">
                            <a id="a5" class="o_m01_1" href='LPBList.aspx' style="cursor: pointer" target="appMain"><span>项目楼盘表</span></a>
                        </div>
                        <%--<div class="o_menu">
                            <a id="a9" class="o_m01_1" href='../AppMain/AppFileUpload.aspx' style="cursor: pointer" target="appMain"><span>附件上传</span></a>
                        </div>
                        <div class="o_menu">
                            <a id="a10" class="o_m01_1" href='../AppMain/Report.aspx' style="cursor: pointer" target="appMain"><span>数据上报</span></a>
                        </div>--%>
                        <%--    <div class="o_menu">
                            <a id="a7" class="o_m01_1" style="cursor: pointer" target="appMain"><span>项目市场行为</span></a>
                            <div class="o_smenu">
                                <a id="a71" class="o_m02_1" href='../ApplySGXKZGL/CCBLList.aspx' target="appMain"><span>良好行为</span></a>
                                <div class="o_smenu2">
                                </div>
                                <a id="a72" class="o_m02_1" href='../ApplySGXKZGL/YQBLList.aspx' target="appMain"><span>不良行为</span></a>
                                
                                <div class="o_smenu2">
                                </div>
                            </div>
                        </div>--%>
                        <asp:Literal ID="YDW" runat="server"></asp:Literal>
                    </div>
                </td>
            </tr>
        </table>
        <table id="t_show" border="0" cellpadding="0" cellspacing="0" height="100%">
            <tr>
                <td id="showLeft" width="9px"></td>
            </tr>
        </table>
    </form>
</body>
</html>
