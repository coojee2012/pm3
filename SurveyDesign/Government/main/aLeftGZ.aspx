<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aLeftGZ.aspx.cs" Inherits="GMap_Server_aLeftGZ" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        html, body { height: 100%; overflow: hidden; font-size: 12px; }
        body { padding: 0; margin: 0; filter: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#F7FCFF,endColorStr=#EEF9FF); }
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
                    fs.cols = "192,*";
                });
            });
            $("#getLeft").click(function() {//隐藏菜单
                $("#t_menu").hide("slow", function() {
                    $("#t_show").show("normal");
                    var fs = window.parent.document.getElementById("trleft");
                    fs.cols = "18,*";
                });
            });

            //整理按钮样式
            $(".o_m01_1").each(function() {//一级
                if ($(this).find("+div").find(":parent").length > 0) {
                    $(this).find("+div:parent").hide();
                    $(this).attr("class", "o_m01_3");
                }
                else {
                    $(this).find("+div").hide();
                }
            });
            $(".o_m02_1").each(function() {//二级
                if ($(this).find("+div").find(":parent").length > 0) {
                    $(this).find("+div:parent").hide();
                    $(this).attr("class", "o_m02_3");
                }
            });
            $(".o_m03_1").each(function() {//三级
                if ($(this).find("+div").find(":parent").length > 0) {
                    $(this).find("+div:parent").hide();
                    $(this).attr("class", "o_m03_3");
                }
            });
            //第有子菜单的展开
            $(".o_m01_3:first").find("+div:parent").show();

            //子菜单展开和隐藏
            $(".o_m01_3").click(function() {//一级
                $(this).find("+div:parent").toggle("fast");
                if ($(this).attr("class") == "o_m01_3") {
                    //$(this).find("+div:parent").toggle("fast");
                    $(this).attr("class", "o_m01_4");
                }
                else {
                    $(this).attr("class", "o_m01_3");
                    //$(this).find("+div:parent").show("fast");
                }
                $(this).blur(); //转移焦点
            });
            $(".o_m02_3").click(function() {//二级
                $(this).find("+div:parent").toggle("fast");
                if ($(this).attr("class") == "o_m02_3")
                    $(this).attr("class", "o_m02_4");
                else
                    $(this).attr("class", "o_m02_3");
                $(this).blur(); //转移焦点
            });
            $(".o_m03_3").click(function() {//三级
                $(this).find("+div:parent").toggle("fast");
                if ($(this).attr("class") == "o_m03_3")
                    $(this).attr("class", "o_m03_4");
                else
                    $(this).attr("class", "o_m03_3");
                $(this).blur(); //转移焦点
            });
            //点击没有子菜单
            $(".o_m01_1").click(function() {//一级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(this).attr("class", "o_m01_2");
                $(this).blur(); //转移焦点
            });
            $(".o_m02_1").click(function() {//二级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(this).attr("class", "o_m02_2");
                $(this).blur(); //转移焦点
            });
            $(".o_m03_1").click(function() {//三级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(this).attr("class", "o_m03_2");
                $(this).blur(); //转移焦点
            });
            $(".o_m04_1").click(function() {//四级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(".o_m04_2").attr("class", "o_m04_1");
                $(this).attr("class", "o_m04_2");
                $(this).blur(); //转移焦点
            });
        });

        function showMenu() {
            $("#t_show").hide("fast", function() { parent.trleft.cols = "188,*"; $("#t_menu").show("slow"); });
        }
        function Clear() {
            document.getElementById("dbCol").value = "";
        }
        //打开第一个菜单的连接
        $(function() {
            //            var aFirst = $("a[href*=.]:first");
            //            var l = $(aFirst).attr('href');
            //            $(aFirst).parent().show();
            //            aFirst.click();
            //            parent.frames["main"].document.location.href = l;
        });

        //窗口式选项卡。
        function gotoPage(num, name, m) {
            if (parent.main.addTab) {
                parent.main.location.href = "javascript:addTab('" + m + "','" + num + "','" + name + "');";
            }
            else {
                parent.main.location.href = "Right.aspx?name=" + escape(name) + "&num=" + num + "&#" + m;
            }
        }
        function showTag(fn, vType) {
            var tag = $("a[href*=" + fn + "]:first span");
            var url = $(tag).attr("url");
            if (url != undefined) {
                if (vType != '-')
                    url += "&FType=" + vType;
                var num = $(tag).attr("num");
                gotoPage(num, fn, url);
            }
        }
    </script>

</head>
<body onresize="AUTOoverflow('div_menu');" onload="AUTOoverflow('div_menu');">
    <form id="form1" runat="server">
    <table id="t_menu" border="0" cellpadding="0" cellspacing="0" style="height: 100%;"
        class="l_table">
        <tr>
            <td class="l_top" style="padding-left: 12px;">
                <img src="../../BGZM/img/gif_48_020.gif" style='float: left; margin: 3px 6px 0px 0px;
                    width: 22px; height: 22px;' />
                <div class="f_l">
                    <span>
                        <asp:Literal ID="lTitle" runat="server"></asp:Literal>
                    </span>
                </div>
                <div id="getLeft" class="l_hide f_r">
                </div>
            </td>
        </tr>
        <tr>
            <td class="l_main" valign="top" align="left">
                <div style="height: 100%; overflow: auto; width: 180px; text-align: left;" id="div_menu">
                    <%--<asp:Repeater ID="repMenu" runat="server" OnItemDataBound="repMenu_ItemDataBound">
                        <ItemTemplate>
                            <div class="o_menu">
                                <a class="o_m01_1" href='<%# string.IsNullOrEmpty(Eval("FUrl").ToString())?"javascript:void(0)":Eval("FUrl") %>'
                                    number='<%#Eval("fnumber") %>' name='<%#Eval("fname") %>' target='<%#string.IsNullOrEmpty(Eval("FTarget").ToString())?"main":Eval("FTarget")  %>'>
                                    <span>
                                        <%# Eval("FName")%></span> </a>
                                <div class="o_smenu">
                                    <asp:Repeater ID="repSubMenu" runat="server" OnItemDataBound="repSubMenu_ItemDataBound">
                                        <ItemTemplate>
                                            <a class="o_m02_1" href='<%# string.IsNullOrEmpty(Eval("FUrl").ToString())?"javascript:void(0)":Eval("FUrl") %>'
                                                number='<%#Eval("fnumber") %>' name='<%#Eval("fname") %>' target='<%#string.IsNullOrEmpty(Eval("FTarget").ToString())?"main":Eval("FTarget")  %>'>
                                                <span>
                                                    <%# Eval("fname")%></span> </a>
                                            <div class="o_smenu2">
                                                <asp:Repeater ID="repSubMenu2" runat="server" OnItemDataBound="repSubMenu2_ItemDataBound">
                                                    <ItemTemplate>
                                                        <a class="o_m03_1" href='<%# string.IsNullOrEmpty(Eval("FUrl").ToString())?"javascript:void(0)":Eval("FUrl") %>'
                                                            number='<%#Eval("fnumber") %>' name='<%#Eval("fname") %>' target='<%#string.IsNullOrEmpty(Eval("FTarget").ToString())?"main":Eval("FTarget")  %>'>
                                                            <span>
                                                                <%# Eval("fname")%></span> </a>
                                                        <div class="o_smenu3">
                                                            <asp:Repeater ID="repSubMenu3" runat="server">
                                                                <ItemTemplate>
                                                                    <a class="o_m04_1" href='<%# string.IsNullOrEmpty(Eval("FUrl").ToString())?"javascript:void(0)":Eval("FUrl") %>'
                                                                        number='<%#Eval("fnumber") %>' name='<%#Eval("fname") %>' target='<%#string.IsNullOrEmpty(Eval("FTarget").ToString())?"main":Eval("FTarget")  %>'>
                                                                        <span>
                                                                            <img src="../../skin/blue/image/menu-parent.gif" /><%# Eval("fname")%></span>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>--%>
                    <asp:TreeView ID="TreeView1" runat="server" ShowLines="True" ExpandDepth="1">
                    </asp:TreeView>
                    <br />
                </div>
            </td>
        </tr>
    </table>
    <table id="t_show" border="0" cellpadding="0" cellspacing="0" height="100%" style="display: none;">
        <tr>
            <td colspan="2" class="l_showtop">
                <div id="showLeft" class="l_show f_r" title="显示菜单">
                </div>
            </td>
        </tr>
        <tr>
            <td class="l_showmain">
            </td>
            <td width="15">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
