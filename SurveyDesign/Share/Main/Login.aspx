<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Share_Main_Login" %>

<%@ Register Namespace="Tools" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>四川省建设工程监管综合管理信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript">
        function checkInfo() {
            if ($("#C_FName").val() == "") {
                alert('请输入登录名');
                $("#C_FName").focus();
                return false;
            }
            if ($("#C_FPwd").val() == "") {
                alert('请输入密码');
                $("#C_FPwd").focus();
                return false;
            }
            if ($("#YZM").val() == "") {
                alert('请输入验证码');
                $("#YZM").focus();
                return false;
            }
            return true;
        }

        function show() {
            if ($("#C_FType").val() == "2") {
                $("#ta_User").show();
                $("#ta_Lock").hide();
            }
            else {
                $("#ta_Lock").show();
                $("#ta_User").hide();
            }
        }

        $(document).ready(function() {
            $(".log_txtBox2").focus(function() {
                $(this).removeClass("log_txtBox2").removeClass("log_txtBox_hover2").addClass("log_txtBox_focus2");
            });
            $(".log_txtBox2").blur(function() {
                $(this).removeClass("log_txtBox_focus2").removeClass("log_txtBox_hover2").addClass("log_txtBox2");
            });
            $(".log_txtBox2").hover(function() {
                $(this).removeClass("log_txtBox2").addClass("log_txtBox_hover2");
            }, function() {
                $(this).removeClass("log_txtBox_hover2").addClass("log_txtBox2");
            });

            $("#C_FType").change(function() {
                show();
            });

            //登陆按钮
            $("#btnLogin").click(function() {
                if (!checkInfo())
                    return false;
            });
            $("#btnLogin0").click(function() {
                var number = getLockId();
                if (number == undefined) {
                    alert("请插入加密锁");
                    return false;
                }
                else {
                    $("#C_FLockNumber").val(number);
                }
            });

            $("#C_FName").focus(function() {
                $("#C_FType").attr("value", 2);
            });
        });
        
    </script>

    <script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>

    <style type="text/css">
        /*屏蔽输入法 强制半角*/
        .noCH { ime-mode: disabled; }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <table border="0" width="100%" height="90%">
        <tr>
            <td align="center" valign="middle" style="">
                <div class="log_div">
                    <table class="log_img_ent01">
                        <tr>
                            <td style="height: 150px;">
                            </td>
                        </tr>
                        <tr style="height: 100px">
                            <td valign="top" align="center">
                                <table>
                                    <tr>
                                        <td class="log_tdl2">
                                            登陆方式：
                                        </td>
                                        <td class="log_tdr2">
                                            <asp:DropDownList ID="C_FType" CssClass="log_txtBox2" runat="server">
                                                <asp:ListItem Value="2">用户名</asp:ListItem>
                                                <asp:ListItem Value="1">加密锁</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" id="ta_User">
                                    <tr>
                                        <td class="log_tdl2">
                                            用户名：
                                        </td>
                                        <td class="log_tdr2">
                                            <asp:TextBox ID="C_FName" runat="server" class="log_txtBox2" Width="158px" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="log_tdl2">
                                            密&nbsp;&nbsp;码：
                                        </td>
                                        <td class="log_tdr2">
                                            <asp:TextBox ID="C_FPwd" runat="server" class="log_txtBox2" TextMode="Password" Width="158px"
                                                TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="log_tdl2">
                                            验证码：
                                        </td>
                                        <td class="log_tdr2">
                                            <asp:TextBox ID="YZM" runat="server" class="log_txtBox2" MaxLength="4" Width="89px"
                                                TabIndex="1" Style="ime-mode: disabled;"></asp:TextBox>
                                            <cc1:EndyVCode ID="vilideCode" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td class="log_tdr2" style="height: 50px;">
                                            <asp:Button ID="btnLogin" runat="server" Text="登陆" CssClass="log_btn2" OnClick="btnLogin_Click" />
                                            &nbsp;&nbsp;
                                            <input type="reset" value="重置" id="bntExit" class="log_btn2" />
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" id="ta_Lock" style="display: none;">
                                    <tr>
                                        <td class="log_tdr2" style="height: 30px;">
                                            首次登陆，请点击：
                                            <br />
                                            ==>><a href='../../ApproveWeb/lnMain/加密锁万能驱动包.exe' target="_blank"><b>下载加密锁驱动包</b></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="log_tdr2" style="height: 30px;">
                                            插入加密锁后，点击登陆按钮
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="C_FLockNumber" type="hidden" runat="server" />
                                            <asp:Button ID="btnLogin0" runat="server" Text="加密锁登陆" CssClass="log_btn3" OnClick="btnLogin_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="bottom" class="log_txt2">
                                技术支持：
                                <asp:Literal ID="liC_TechSupport" runat="server"></asp:Literal>
                                &nbsp;&nbsp;&nbsp;电话：
                                <asp:Literal ID="liC_webtel" runat="server"></asp:Literal>
                                <br />
                                <br />
                                <asp:Literal ID="liC_Developer" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
