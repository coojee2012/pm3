<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aTop.aspx.cs" Inherits="Admin_main_aTop" %>

<%@ Register Src="../../Common/SelectSkin.ascx" TagName="SelectSkin" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $(".top_btn,.top_btn02").click(function() {
                if ($(this).attr("href") != "#") {
                    $(".top_btn02").removeClass("top_btn02").addClass("top_btn");
                    $(this).removeClass("top_btn").addClass("top_btn02");
                    $(window.parent.document.frames["main"].document.location).attr("href", "blank.aspx");
                }
            });


        }); 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="top_back">
        <div class="top_backimg01">
            <div class="top_r">
                <div style="height: 21px; margin-top: 2px;">
                    <div class="top_time">
                        <span></span>
                        <div>
                            <asp:Literal ID="lDate" runat="server"></asp:Literal>
                        </div>
                        <strong>
                            <uc1:SelectSkin ID="SelectSkin1" runat="server" />
                        </strong>
                    </div>
                </div>
                <div class="top_menu" id="adminTd1" runat="server">
                    <div runat="server" id="div_155001">
                        <a class="top_btn02" href="aleft.aspx?HKINDID=155001" target="left" id="a2">
                            <img alt="用户管理" src="../../image/top_btn09.gif" /><br />
                            用户管理 </a>
                    </div>
                    <div runat="server" id="div_155005">
                        <a class="top_btn" href="aleft.aspx?HKINDID=155005" target="left" id="a3">
                            <img alt="编码管理" src="../../image/top_btn03.gif" /><br />
                            编码管理</a>
                    </div>
                    <div runat="server" id="div_155006">
                        <a class="top_btn" href="aleft.aspx?HKINDID=155006" target="left" id="a4">
                            <img alt="标准管理" src="../../image/top_btn04.gif" /><br />
                            标准管理</a>
                    </div>
                    <div runat="server" id="div_155007">
                        <a class="top_btn" href="aleft.aspx?HKINDID=155007" target="left" id="a5">
                            <img alt="流程管理" src="../../image/top_btn11.gif" /><br />
                            流程管理</a>
                    </div>
                    <div runat="server" id="div_155003">
                        <a class="top_btn" href="aleft.aspx?HKINDID=155003" target="left" id="a8">
                            <img alt="建设资讯" src="../../image/top_btn08.gif" /><br />
                            建设资讯</a>
                    </div>
                    <div runat="server" id="div_xtcd">
                        <a class="top_btn" href="aleft.aspx?HKINDID=xtcd" target="left" id="a9">
                            <img alt="系统菜单" src="../../image/top_btn05.gif" /><br />
                            系统菜单</a>
                    </div>
                    <div runat="server" id="div_lmcd">
                        <a class="top_btn" href="aleft.aspx?HKINDID=lmcd" target="left" id="a1">
                            <img alt="菜单管理" src="../../image/top_btn06.gif" /><br />
                            栏目菜单</a>
                    </div>
                    <div runat="server" id="div_155008">
                        <a class="top_btn" href="aleft.aspx?HKINDID=155008" target="left" id="a6">
                            <img alt="日志管理" src="../../image/top_btn02.gif" /><br />
                            日志管理</a>
                    </div>
                    <div>
                        <a class="top_btn" href="#" id="a7" onclick="if(confirm('确认要退出么?')){document.getElementById('bntExit').click();}">
                            <img alt="安全退出" src="../../image/top_btn07.gif" /><br />
                            安全退出</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="top_bar">
        <b>当前用户是：<asp:Label ID="lUserName" runat="server" Text=""></asp:Label></b>
        <div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </div>
    <asp:Button ID="bntExit" runat="server" Text="Button" OnClick="bntExit_Click" Style="display: none" />
    </form>
</body>
</html>
