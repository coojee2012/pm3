<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aTop.aspx.cs" Inherits="Admin_main_aTop" %>

<%@ Register Src="../../Common/ValidateUserId.ascx" TagName="ValidateUserId" TagPrefix="uc1" %>
<%@ Register Src="../../Common/SelectSkin.ascx" TagName="SelectSkin" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        var date;
        $(document).ready(function () {
            date = new Date(Date.parse($("#hfCurrentTime").val().replace(/-/g, "/")));
            $(".top_btn").click(function () {
                if ($(this).attr("href") != "#") {
                    $(".top_btn02").removeClass("top_btn02").addClass("top_btn");
                    $(this).removeClass("top_btn").addClass("top_btn02");
                }
            });

            var n = 1;
            $("#a6").mouseover(function () {
                $("#div_out").attr("class", "serhover");
            });
            $("#a6").mouseout(function () {
                if (n == 1)
                    $("#div_out").attr("class", "ser");
            });
            $("#a5").click(function () {//变更系统在链接
                var url = $(this).attr("href");
                window.parent.parent.location.replace(url);
            });
            $("#a6").click(function () {
                $(this).blur();
                top.openOnLine();
            });
            $("#time").text(GetDate(date.getDay()));
            setInterval(function () { $("#currentDate").text(FormatDate("yyyy年MM月dd日 hh:mm:ss")); }, 1000);
        });
        function FormatDate(format) {
            date = new Date(date.valueOf() + 1000); 
            var o = {
                "M+": date.getMonth() + 1, //month 
                "d+": date.getDate(), //day 
                "h+": date.getHours(), //hour 
                "m+": date.getMinutes(), //minute 
                "s+": date.getSeconds(), //second 
                "q+": Math.floor((date.getMonth() + 3) / 3), //quarter 
                "S": date.getMilliseconds() //millisecond 
            }

            if (/(y+)/.test(format)) {
                format = format.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
            }

            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
                }
            }
            return format;
        }
        function GetDate(day) {
            var result = "";
            if (day == 1) result = "星期一";
            else if (day == 2) result = "星期二";
            else if (day == 3) result = "星期三";
            else if (day == 4) result = "星期四";
            else if (day == 5) result = "星期五";
            else if (day == 6) result = "星期六";
            else if (day == 0) result = "星期日";
            return result;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfCurrentTime" runat="server" />
    <uc1:ValidateUserId ID="ValidateUserId1" runat="server" />
    <div class="top_back02">
        <div id="div_BG" runat="server" class="top_backimg02">
            <div class="top_r02" style="display:none;">
                <div class="top_menu" id="adminTd1" runat="server">
                    <div>
                        <a class="top_btn" href="#" id="a5" runat="server" target="_top">
                            <img alt="返回门户" src="../../image/top_btn12.gif" /><br />
                            返回门户</a>
                    </div>
                    <div>
                        <a class="top_btn" href="#" id="a6" runat="server">
                            <img alt="在线服务" src="../../image/help.gif" /><br />
                            在线服务</a>
                    </div>
                    <div>
                        <a class="top_btn" href="#" id="a7" onclick="if(confirm('确认要退出么?')){$('#bntExit').click();}">
                            <img alt="安全退出" src="../../image/top_btn07.gif" /><br />
                            安全退出</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="top_bar">
        <div style="width:120px;height:28px;background-color:Orange;font-weight:bold;text-align:center;">建设单位版</div>
        <b>登录单位：<asp:Label ID="liter_FBaseName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;<span id="currentDate"></span>&nbsp;&nbsp;&nbsp;<span id="time"></span></b>
        <div>
            <asp:Literal ID="li_UserType" runat="server"></asp:Literal>
        </div>
        <div>
            <asp:Literal ID="li_Logs" runat="server"></asp:Literal>
        </div>
        <strong style="background: url(../../image/arrow02.gif) 0px 6px no-repeat; padding-left: 16px;">
            <asp:Literal ID="lDate" runat="server"></asp:Literal>
            <uc2:SelectSkin ID="SelectSkin" runat="server" />
        </strong>
        <strong style="background: url(../../image/icon3.gif) 0px 6px no-repeat; padding-left: 20px;">
            <asp:Button ID="bntExit" runat="server" Text="Button" OnClick="bntExit_Click" Style="display: none" />
            <asp:Literal ID="lLockInfo" runat="server"></asp:Literal>
        </strong>
        <strong style="background: url(../../image/icon2.gif) 0px 6px no-repeat; padding-left: 20px;">
         <a href="#" onclick="if(confirm('确认要退出么?')){$('#bntExit').click();}"> 安全退出</a>
        </strong>
       <%-- <strong style="background: url(../../image/icon1.gif) 0px 6px no-repeat; padding-left: 20px;">
        <a href="#">返回门户</a>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </strong>--%>
    </div>
    </form>
</body>
</html>
