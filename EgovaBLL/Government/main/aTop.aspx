<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aTop.aspx.cs" Inherits="Admin_main_aTop" %>

<%@ Register Src="../../Common/ValidateDFUserId.ascx" TagName="ValidateUserId" TagPrefix="uc1" %>
<%@ Register Src="../../Common/SelectSkin.ascx" TagName="SelectSkin" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $(".top_btn").click(function() {
                var n = $(this).attr("id").replace("a", "");
                if ($("#bar_" + n).length > 0) {
                    $("div[id^=bar_][id!=bar_" + n + "]").hide("500", function() {
                        $("#bar_" + n).show("500");
                    });
                }
                else {
                    $("div[id^=bar_][id!=bar_user]").hide("500", function() {
                        $("#bar_user").show("500");
                    });
                }

                var fs = window.parent.document.getElementById("trleft");
                if ($(this).attr("href") != "#") {
                    //$(".top_btn02").removeClass("top_btn02").addClass("top_btn");
                    //$(this).removeClass("top_btn").addClass("top_btn02");
                    $(".top_btn").css("color", "#000000");
                    $(this).css("color", "#FF0000");
                    $("img").each(function() { $(this).attr("src", $(this).attr("src").replace("-o", "")); $(this).attr("h", "0"); });
                    var img = $(this).find("img");
                    var src = img.attr("src");
                    img.attr("src", getSrc(src));
                    img.attr("h", "1");

                    if ($(this).attr("target") == "left") {
                        aC($(this));
                        fs.cols = "192,*";
                    }
                    else {
                        fs.cols = "0,*";
                    }
                }
            });
            $("a[showID=subA]").click(function() {
                var fs = window.parent.document.getElementById("trleft");
                if ($(this).attr("target") == "left") {
                    aC($(this));
                    fs.cols = "192,*";
                }
                else {
                    fs.cols = "0,*";
                }
            });

//            $("img").hover(function() {
//                var src = $(this).attr("src");
//                if ($(this).attr("h") != "1") {
//                    $(this).attr("src", getSrc(src));
//                }
//            }, function() {
//                var src = $(this).attr("src");
//                if ($(this).attr("h") != "1") {
//                    $(this).attr("src", src.replace("-o", ""));
//                }
//            });


            //初始化时执行第一个主菜单 
            aC($(".top_btn").first(), true);
            //第一个主菜单样式
            $(".top_btn").first().css("color", "#FF0000");
            var firstIMG = $(".top_btn").first().find("img");
            firstIMG.attr("src", getSrc(firstIMG.attr("src")));
            firstIMG.attr("h", "1");
            //$(".top_btn").first().removeClass("top_btn").addClass("top_btn02");

            //            $("a").hover(function() {
            //                if ($(this).attr("href") == "javascript:void(0)") {
            //                    $(".top_btn02").removeClass("top_btn02").addClass("top_btn");
            //                    $(this).removeClass("top_btn").addClass("top_btn02");
            //                    //打开子菜单
            //                    top.OpenSubMenu(document.getElementById($(this).attr("id")), $(this).attr("number"));
            //                }
            //                else {
            //                    $(".top_btn02").removeClass("top_btn02").addClass("top_btn");
            //                    $(this).removeClass("top_btn").addClass("top_btn02");
            //                    //关闭子菜单
            //                    top.hideSubMenu();
            //                }
            //            }, function() {
            //                if ($(this).attr("href") == "javascript:void(0)") {
            //                    $(".top_btn02").removeClass("top_btn02").addClass("top_btn");
            //                    $(this).removeClass("top_btn").addClass("top_btn02");
            //                    //关闭子菜单
            //                    top.hideSubMenu();
            //                }
            //            });
        });

        function getSrc(s) {
            var v = s.replace(".png", "");
            return v.replace("-o", "") + "-o.png";
        }

        //主菜单执行
        function aC(obj, isFirst) {
            //left框架链接
            var l = $(obj).attr("href");
            //main框架链接
            var m = $(obj).attr("qurl");
            //页面number
            var num = $(obj).attr("number");
            //页面name
            var name = $(obj).attr("name");
            //target
            var target = $(obj).attr("target");

            var fs = window.parent.document.getElementById("trleft");
            if (target == "main") {
                fs.cols = "0,*";
                parent.main.location.href = l;
            }
            else {
                fs.cols = "192,*";
                //left框架打开页面
                parent.left.location.href = l;
                //main框架打开页面
                //            if (isFirst) {
                //                parent.main.location.href = "Right.aspx?name=" + escape(name) + "&num=" + num + "&#" + m;
                //            } else {
                //                parent.main.location.href = "javascript:addTab('" + m + "','" + num + "','" + name + "');";
                //            }
                //gotoPage(num, name, m);
                mainURL(m);
            }

        }
        function gotoPage(num, name, m) {
            if (parent.main.addTab) {
                parent.main.location.href = "javascript:addTab('" + m + "','" + num + "','" + name + "');";
            }
            else {
                parent.main.location.href = "Right.aspx?name=" + escape(name) + "&num=" + num + "&#" + m;
            }
        }
        function mainURL(url) {
            if (url != null && url != "")
                parent.main.location.href = url;
        }

        function showTSMSG() {
            top.showMSG("../../Government/main/MSG.aspx", "新待办事项");
        }
    </script>

    <style type="text/css">
        .top_btn
        {
            margin-left: 0px;
            margin-right: 0px;
        }
    </style>
</head>
<body bottommargin="0" topmargin="0" rightmargin="0">
    <form id="form1" runat="server">
    <div class="top_back02">
        <div class="top_backimgGLDW">
            <div class="top_r">
                <div class="top_menu" id="adminTd1" runat="server" >
                    <asp:Repeater ID="re_Menu" runat="server">
                        <ItemTemplate>
                            <div style="margin-right: 6px;display:none">
                                <a id='a<%#Eval("fnumber") %>' class="top_btn" href='<%# string.IsNullOrEmpty(Eval("FUrl").ToString())?"aleft.aspx?HKINDID="+Eval("fnumber"):Eval("FUrl") %>'
                                    qurl='<%#Eval("FQurl") %>' number='<%#Eval("fnumber") %>' name='<%#Eval("fname") %>'
                                    target='<%#string.IsNullOrEmpty(Eval("FTarget").ToString())?"left":Eval("FTarget")  %>'
                                    id="a2">
                                    <img alt='<%#Eval("FName") %>' src='<%#Eval("FPicName") %>' /><br />
                                    <%#Eval("FName") %></a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div style="margin-right: 40px;">
                        <a class="top_btn" href="#" id="a7" onclick="if(confirm('确认要退出么?')){document.getElementById('bntExit').click();}">
                                  <img alt="安全退出" src="../../image/top_btn07.gif" /><br />
                            安全退出</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="top_bar" style="margin-top: 1px;">
        <div id="bar_user">
            <b>
                <asp:Literal ID="lab_User" runat="server" Text=""></asp:Literal></b>
        </div>
        <asp:Literal ID="lit_Bar" runat="server"></asp:Literal>
        <div style="float: right;">
            <span>
                <uc1:ValidateUserId ID="ValidateUserId1" runat="server" />
            </span>
            <div>
             <a href="../../../help/四川工程勘察设计基督平台管理版操作说明.pdf" id="linkHelp" runat="server" target="_blank"  class="txt4">操作说明下载</a>
                <asp:Literal ID="lDate" runat="server"></asp:Literal>
                <uc1:SelectSkin ID="SelectSkin1" runat="server" />
            </div>
        </div>
    </div>
    <asp:Button ID="bntExit" runat="server" Text="Button" OnClick="bntExit_Click" Style="display: none" />
    <input id="HCsCradNo" runat="server" type="text" style="display: none;" />
    </form>
</body>
</html>
