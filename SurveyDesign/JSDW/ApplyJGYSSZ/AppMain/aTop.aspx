<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aTop.aspx.cs" Inherits="JSDW_ApplyJGYSSZ_AppMain_aTop" %>

<%@ Register Src="../../../Common/ValidateUserId.ascx" TagName="ValidateUserId" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../script/default.js" type="text/javascript"></script>
    <script src="../../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".top_btn").click(function () {
                if ($(this).attr("href") != "#") {
                    $(".top_btn02").removeClass("top_btn02").addClass("top_btn");
                    $(this).removeClass("top_btn").addClass("top_btn02");
                }
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="top_back02">
            <div id="div_BG" runat="server" class="top_backimg02">
                <div class="top_r">
                    <div class="top_menu" id="adminTd1" runat="server">
                        <div>
                            <a class="top_btn" href="#" id="lit_back" runat="server" onclick='EntWinUnloadEvent("Enforce", "1");' target="_parent">
                                <img alt="返回主页" src="../../../image/top_btn12.gif" /><br />
                                返回主页</a>
                        </div>
                        <div>
                            <a class="top_btn" href="#" id="a7" onclick="if(confirm('确认要退出么?')){$('#bntExit').click();}">
                                <img alt="安全退出" src="../../../image/top_btn07.gif" /><br />
                                安全退出</a>
                        </div>
                    </div>
                </div>
                <uc1:ValidateUserId ID="ValidateUserId1" runat="server" />
            </div>
        </div>
        <div class="top_bar">
            <b>
                <asp:Literal ID="lMsg" runat="server"></asp:Literal>
            </b>
            <div>
            </div>
            <strong style="background: url(../../../image/arrow02.gif) 0px 6px no-repeat; padding-left: 16px;">
                <asp:Literal ID="lDate" runat="server"></asp:Literal>
                <asp:Button ID="bntExit" runat="server" Text="Button" OnClick="bntExit_Click" Style="display: none" />
            </strong>
        </div>
        <input id="HDetpNumber" runat="server" type="hidden" />
    </form>
</body>
</html>
