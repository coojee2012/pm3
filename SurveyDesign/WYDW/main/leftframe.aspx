﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="leftframe.aspx.cs" Inherits="WYDW_leftframe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            $("#showLeft").click(function () {//隐藏菜单
                $("#t_show").hide("fast", function () { parent.trleft.cols = "191,*"; $("#t_menu").show("slow"); });
            });
            $("#getLeft").click(function () {//显示菜单
                $("#t_menu").hide("slow", function () { parent.trleft.cols = "12,*"; $("#t_show").show("normal"); });
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
                        <asp:Label ID="l_title" runat="server">系统菜单</asp:Label>
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
                            <a id="a1" class="o_m01_1" href='zWXTS.aspx' target="main"><span>温馨提示</span></a>
                        </div>

                        <div class="o_menu">
                            <a id="a2" class="o_m01_1" href='../ZGXM/ZGXMList.aspx' target="main"><span>在管项目</span></a>
                        </div>
                        <%--
                        <div class="o_menu">
                            <a id="a3" class="o_m01_1" href='../JSDW/project/ProjectList.aspx' target="main"><span>单位信息</span></a>
                        </div>--%>
                        <div class="o_menu">
                            <div class="o_smenu">
                                <a id="a4" class="o_m01_1" style="cursor: pointer" target="main"><span>业务办理</span></a>
                                <a id="a41" class="o_m02_1" href='../AppMain/AppList.aspx?ManageTypeID=14401' target="main"><span>项目在管申请</span></a>
                                <div class="o_smenu2">
                                </div>
                                <a id="a42" class="o_m02_1" href='../AppMain/AppList.aspx?ManageTypeID=14403' target="main"><span>项目变更备案</span></a>
                                <div class="o_smenu2">
                                </div>
                                <a id="a43" class="o_m02_1" href='../AppMain/AppList.aspx?ManageTypeID=14402' target="main"><span>项目失去申请</span></a>
                                <div class="o_smenu2">
                                </div>
                            </div>
                        </div>
                    <div class="o_menu">
                        <a id="a6" class="o_m01_1" href='../AppMain/AppList.aspx?ManageTypeID=14404' target="main"><span>项目合同备案</span></a>
                    </div>
                    <div class="o_menu">
                        <a id="a7" class="o_m01_1" href='../AppMain/AppList.aspx?ManageTypeID=14405' target="main"><span>项目业委会备案</span></a>
                    </div>
                    <div class="o_menu">
                        <a id="a8" class="o_m01_1" href='../AppMain/AppList.aspx?ManageTypeID=14406' target="main"><span>项目财务年报</span></a>
                    </div>
                    <div class="o_menu">
                        <a id="a5" class="o_m01_1" style="cursor: pointer" target="main"><span>查询</span></a>
                        <div class="o_smenu">
                            <a id="a52" class="o_m02_1" href='../LSXM/LSXMList.aspx' target="main"><span>历史项目查询</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
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
        <input id="Hidden1" runat="server" type="hidden" value="155001" />
    </form>
</body>
</html>
