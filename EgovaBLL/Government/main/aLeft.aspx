<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aLeft.aspx.cs" Inherits="Government_AppMain_aLeft" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        $(document).ready(function () {
            var one = document.getElementById("div_menu"); //需要滚动条的DIV高
            one.style.height = window.document.documentElement.offsetHeight - 30; //-40：可根据需要时调整

            $("#t_show").click(function() {//显示菜单
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
                    fs.cols = "9,*";
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
            //第有子菜单的展开
            $(".o_m01_3").find("+div:parent").first().show();
            //$(".o_m02_3").find("+div:parent").show();
            //子菜单展开和隐藏
            $(".o_m01_3").click(function() {//一级
                $(this).find("+div:parent").first().slideToggle("fast");
                if ($(this).attr("class") == "o_m01_3")
                    $(this).attr("class", "o_m01_4");
                else
                    $(this).attr("class", "o_m01_3");
                $(this).blur(); //转移焦点
            });
            $(".o_m02_3").click(function() {//二级
                $(this).find("+div:parent").first().slideToggle("fast");
                if ($(this).attr("class") == "o_m02_3")
                    $(this).attr("class", "o_m02_4");
                else
                    $(this).attr("class", "o_m02_3");
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
        });
        //        function showMenu() {
        //            alert(1);
        //            $("#t_show").hide("fast", function() { parent.trleft.cols = "188,*"; $("#t_menu").show("slow"); });
        //        }

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
            var url = $(tag).attr("url");.0
            if (url != undefined) {
                if (vType != '-')
                    url += "&FType=" + vType;
                var num = $(tag).attr("num");
                //parent.main.location.href = "Right.aspx?name=" + escape(fn) + "&num=" + num + "&#" + url;
                gotoPage(num, fn, url);
            }
        }
        window.onresize = function() { change(); }
        window.onload = function() { change(); }
        function change() {
            var one = document.getElementById("div_menu");
          //  one.style.height = window.document.body.clientHeight - 36;
        } 
    </script>

</head>
<body style="height: 100%;" >
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
                    <asp:Repeater ID="repMenu" runat="server" OnItemDataBound="repMenu_ItemDataBound">
                        <ItemTemplate>
                            <div class="o_menu">
                                <a class="o_m01_1" href="<%# Eval("FUrl").ToString()==""?"javascript:void(0);":string.Format("javascript:gotoPage('{0}','{1}','{2}')" ,Eval("Fnumber"),Eval("fname"),Eval("FUrl"))%>">
                                    <span>
                                        <%# Eval("FName")%></span> </a>
                                <div class="o_smenu">
                                    <asp:Repeater ID="repSubMenu" runat="server" OnItemDataBound="repSubMenu_ItemDataBound">
                                        <ItemTemplate>
                                            <a class="o_m02_1" href="<%# Eval("FUrl").ToString()==""?"javascript:void(0);":string.Format("javascript:gotoPage('{0}','{1}','{2}')",Eval("Fnumber"),Eval("fname"),Eval("FUrl") ) %>">
                                                <span>
                                                    <%# Eval("fname")%></span> </a>
                                            <div class="o_smenu2">
                                                <asp:Repeater ID="repSubMenu2" runat="server">
                                                    <ItemTemplate>
                                                        <a class="o_m03_1" href="<%# Eval("FUrl").ToString()==""?"javascript:void(0);":string.Format("javascript:gotoPage('{0}','{1}','{2}')" ,Eval("Fnumber"),Eval("fname"),Eval("FUrl")) %>">
                                                            <span>
                                                                <%# Eval("fname")%></span> </a>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
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
    <input id="HKINDID" runat="server" type="hidden" value="YWBL" />
    <input id="HCardNo" runat="server" type="hidden" />
    </form>
</body>
</html>
