﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Share_WebSide_Login" %>

<%@ Register Namespace="Tools" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>四川省勘察设计科技信息系统</title>
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
            //if ($("#YZM").val() == "") {
            //    alert('请输入验证码');
            //    $("#YZM").focus();
            //    return false;
            //}
            return true;
        }

        $(document).ready(function () {
            $(".log_txtBox2").focus(function () {
                $(this).removeClass("log_txtBox2").removeClass("log_txtBox_hover2").addClass("log_txtBox_focus2");
            });
            $(".log_txtBox2").blur(function () {
                $(this).removeClass("log_txtBox_focus2").removeClass("log_txtBox_hover2").addClass("log_txtBox2");
            });
            $(".log_txtBox2").mouseover(function () {
                $(this).removeClass("log_txtBox2").addClass("log_txtBox_hover2");
            });
            $(".log_txtBox2").mouseout(function () {
                $(this).removeClass("log_txtBox_hover2").addClass("log_txtBox2");
            });

            $("#C_FType").change(function () {
                show();
            });

            //登陆按钮
            $("#btnLogin").click(function () {
                if (!checkInfo())
                    return false;
            });
            $("#btnLogin0").click(function () {
                var number = getLockId();
                if (number) {
                    $("#C_FLockNumber").val(number);
                }
                else {
                    alert("读取加密锁失败！\n\r\n\r请确保已正确安装驱动，并已正确插入加密锁。");
                    return false;
                }
            });


            $("#d_d").change(function () {
                if ($(this).val() != "") {
                    var checkText = $(this).find("option:selected").text();  //获取Select选择的Text
                    var checkValue = $(this).val();  //获取Select选择的Value
                    $("#C_FName").val(checkText);
                    $("#C_FPwd").val(checkValue);
                }
                $(this).val("");
            });

            $(".tab_ut, .tab_ut_h").click(function () {
                var name = $(this).attr("name");
                $("#C_FType").val(name);
                show();
            });

            //控制选项卡
            show();
        });

        function show() {
            var name = $("#C_FType").val();
            $(".tab_ut_h").attr("class", "tab_ut");
            $("div[name=" + name + "]").attr("class", "tab_ut_h");

            $(".login_in").hide();
            $("div[id=" + name + "]").show();
        }

    </script>

    <script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>

    <style type="text/css">
        html, body, form {
            width: 100%;
            height: 100%;
        }

        .tab_ut, .tab_ut_h {
            cursor: pointer;
            float: left;
            text-align: left;
            padding: 0px 6px 0px 25px;
            position: relative;
            border-left: 1px solid #1EB2F0;
            border-top: 1px solid #1EB2F0;
            border-right: 1px solid #1EB2F0;
            background: #EEF7FF;
        }

            .tab_ut img, .tab_ut_h img {
                width: 20px;
                height: 20px;
                position: absolute;
                top: 2px;
                left: 2px;
            }

        .tab_ut {
            border-bottom: 1px solid #1EB2F0;
            height: 25px;
            line-height: 25px;
            margin-top: 4px;
            color: #2773C7;
        }

        .tab_ut_h {
            height: 30px;
            line-height: 30px;
            color: #FF0000;
        }

        .dic_list div {
            height: 22px;
            line-height: 22px;
        }

            .dic_list div a {
                color: Red;
            }

                .dic_list div a:hover {
                    text-decoration: underline;
                }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" width="100%" height="100%">
            <tr>
                <td align="center" valign="middle">
                    <div class="login_bg_m" style="margin: 0 auto; position: relative;border:1pt;">
                        <div id="ta_Doc" style="position: absolute; left: 620px; position: absolute; top: 300px; width: 200px; z-index: 999; text-align: left; display: none">
                            <div style="padding: 14px 0px 6px 0px; height: 20px;">
                                <img src="../../image/gif_48_050.gif" style="width: 20px; height: 20px; float: left;" />
                                <b style="display: block; float: left; width: 150px; height: 20px; line-height: 20px;">操作说明下载：</b>
                            </div>
                            <div class="dic_list">
                            </div>
                        </div>
                        <div id="ta_User" style="float:left;left: 100px; margin-top:10px;" class="login_in" runat="server">
                            <div>
                                <tt>用户名：</tt><span>
                                    <asp:TextBox ID="C_FName" runat="server" class="login_txt1" TabIndex="1"></asp:TextBox></span>
                                <asp:DropDownList ID="d_d" runat="server">
                                    <asp:ListItem Text="演示用户备选" Value=""></asp:ListItem>
                                    <asp:ListItem Text="四川见证单位" Value="123456"></asp:ListItem>
                                    <asp:ListItem Text="四川设计单位" Value="123456"></asp:ListItem>
                                    <asp:ListItem Text="四川勘察单位" Value="123456"></asp:ListItem>
                                    <asp:ListItem Text="四川图审机构" Value="123456"></asp:ListItem>
                                    <asp:ListItem Text="四川建设单位" Value="123456"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div>
                                <tt>密&nbsp;&nbsp;码：</tt><span>
                                    <asp:TextBox ID="C_FPwd" runat="server" class="login_txt1" TextMode="Password" TabIndex="1"></asp:TextBox></span>
                            </div>
                            <div style="display: none;">
                                <tt>验证码：</tt><span>
                                    <asp:TextBox ID="YZM" runat="server" class="login_txt2" MaxLength="5" TabIndex="1"></asp:TextBox>
                                    <cc1:EndyVCode ID="vilideCode" runat="server" />
                                </span>
                            </div>
                            <div style="text-align: left; padding-top: 4px; padding-left: 50px;">
                                <asp:Button ID="btnLogin" runat="server" Text="" CssClass="login_btn_in" OnClick="btnLogin_Click" />
                                &nbsp;&nbsp;
                            <input type="reset" value="" id="bntExit" class="login_btn_reset" />
                            </div>
                        </div>
                        <div id="ta_lock" class="login_in" style="display: none;left: 100px; " runat="server">
                            <div style="padding-top: 14px;">
                                <tt>首次登陆，请点击：==>><a href='../../加密锁万能驱动包.exe' style="color: Red;" target="_blank"><b>下载加密锁驱动包</b></a></tt><span></span>
                            </div>
                            <div>
                                <tt>插入加密锁，等待系统识别后，点击登陆按钮 </tt><span></span>
                            </div>
                            <div style="text-align: left; padding-top: 4px; padding-left: 50px;">
                                <input id="C_FLockNumber" type="hidden" runat="server" />
                                <asp:Button ID="btnLogin0" runat="server" Text="" CssClass="login_btn_in" OnClick="btnLogin_Click" />
                            </div>
                        </div>
                        <div style="position: absolute; top: 275px; left: 100px; width: 300px; height: 30px;">
                            <div class="tab_ut" id="tab_lock" name="ta_lock" title="使用加密锁登陆系统" runat="server">
                                <img src="../../image/gif_48_057.gif" />
                                加密锁登陆
                            </div>
                            <div class="tab_ut_h" id="tab_User" name="ta_User" title="使用用户名登陆系统" runat="server">
                                <img src="../../image/gif_48_103.gif" />
                                用户名登陆
                            </div>
                            <div class="tab_ut" id="tab_Reg" style="display: none;" title="用户注册" onclick="window.location='RegEntUser.aspx?entsystemid=100&sys=x'">
                                <img src="../../image/gif_48_103.gif" />
                                用户注册
                            </div>
                        </div>
                        <div style="position: absolute; top: 458px; left: 200px; width: 590px; height: 26px; line-height: 26px; color: #FFFFFF;">
                            技术支持：
                        <asp:Literal ID="liC_TechSupport" runat="server"></asp:Literal>
                            &nbsp;&nbsp;&nbsp;电话：
                        <asp:Literal ID="liC_webtel" runat="server"></asp:Literal>
                            <asp:Literal ID="liC_Developer" runat="server" Visible="false"></asp:Literal>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div class="log_div" style="display: none;">
            <asp:DropDownList ID="C_FType" CssClass="log_txtBox2" runat="server">
                <asp:ListItem Value="ta_User">用户名</asp:ListItem>
                <asp:ListItem Value="ta_lock">加密锁</asp:ListItem>
            </asp:DropDownList>
        </div>
    </form>
</body>
</html>
