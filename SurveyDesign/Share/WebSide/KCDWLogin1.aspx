<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KCDWLogin1.aspx.cs" Inherits="Share_WebSide_KCDWLogin1" %>

<%@ Register Namespace="Tools" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>四川省勘察设计科技信息系统-勘察单位</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript">
        function checkInfo() {
            if (jQuery.trim($("#C_FName").val()) == "") {
                alert('请输入登录名');
                $("#C_FName").focus();
                return false;
            }
            if (jQuery.trim($("#C_FPwd").val()) == "") {
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

        $(document).ready(function() {
            $(".log_txtBox2").focus(function() {
                $(this).removeClass("log_txtBox2").removeClass("log_txtBox_hover2").addClass("log_txtBox_focus2");
            });
            $(".log_txtBox2").blur(function() {
                $(this).removeClass("log_txtBox_focus2").removeClass("log_txtBox_hover2").addClass("log_txtBox2");
            });
            $(".log_txtBox2").mouseover(function() {
                $(this).removeClass("log_txtBox2").addClass("log_txtBox_hover2");
            });
            $(".log_txtBox2").mouseout(function() {
                $(this).removeClass("log_txtBox_hover2").addClass("log_txtBox2");
            });

            $("#C_FType").change(function() {
                var name = $("#C_FType").val();
                $("div[id*=Ent_]").hide();
                $("div[id*=Ent_" + name + "]").show();
            });

            //登陆按钮
            $("#btnLogin").click(function() {
                if (!checkInfo())
                    return false;
                var ckCA = '<%=IsCheckCA() %>';
                if (ckCA == "True")
                    return fnQYLogin();
                return true;
            });
            $("#btnEmpLogin").click(function() {
                if (!checkInfo())
                    return false;
                var ckCA = '<%=IsCheckEmpCA() %>';
                if (ckCA == "True")
                    return fnQYLogin();
                return true;
            });
            $("#btnLogin0").click(function() {
                var number = getLockId();
                if (number) {
                    $("#C_FLockNumber").val(number);
                    var ckCA = '<%=IsCheckCA() %>';
                    if (ckCA == "True")
                        return fnQYLogin();
                    return true;
                }
                else {
                    alert("读取加密锁失败！\n\r\n\r请确保已正确安装驱动，并已正确插入加密锁。");
                    return false;
                }
            });


            $("#d_d").change(function() {
                if ($(this).val() != "") {
                    var checkText = $(this).find("option:selected").text();  //获取Select选择的Text
                    var checkValue = $(this).val();  //获取Select选择的Value
                    $("#C_FName").val(checkText);
                    $("#C_FPwd").val(checkValue);
                }
                $(this).val("");
            });

            $("#d_dEmp").change(function() {
                if ($(this).val() != "") {
                    var checkText = $(this).find("option:selected").text();  //获取Select选择的Text
                    var checkValue = $(this).val();  //获取Select选择的Value
                    $("#e_Name").val(checkText);
                    $("#e_Pwd").val(checkValue);
                }
                $(this).val("");
            });
            $(".tab_ut, .tab_ut_h").click(function() {
                var name = $(this).attr("name");
                $("#hiType").val(name);
                show();
            });

            //控制选项卡
            show();
        });

        function show() {
            var name = $("#hiType").val();
            $(".tab_ut_h").attr("class", "tab_ut");
            $("div[name=" + name + "]").attr("class", "tab_ut_h");
            $(".login_in").hide();
            $("div[id=" + name + "]").show();
            $("#C_FType").change();
        }
        function DowCAQD() {
            window.open("../../upload/BJCA证书应用环境安装程序.rar");
        }
    </script>

    <script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>

    <style type="text/css">
        html, body, form
        {
            width: 100%;
            height: 100%;
        }
        .tab_ut, .tab_ut_h
        {
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
        .tab_ut img, .tab_ut_h img
        {
            width: 20px;
            height: 20px;
            position: absolute;
            top: 2px;
            left: 2px;
        }
        .tab_ut
        {
            border-bottom: 1px solid #1EB2F0;
            height: 25px;
            line-height: 25px;
            margin-top: 4px;
            color: #2773C7;
        }
        .tab_ut_h
        {
            height: 30px;
            line-height: 30px;
            color: #FF0000;
        }
        .dic_list div
        {
            height: 22px;
            line-height: 22px;
        }
        .dic_list div a
        {
            color: Red;
        }
        .dic_list div a:hover
        {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" width="100%" height="100%">
        <tr>
            <td align="center" valign="middle">
                <div style="margin: 0 auto; position: relative; width: 780px; height: 540px; background-image: url(../image/KCDWLogin.jpg)">
                    <div style="height: 282px;">
                    </div>
                    <div style="height: 174px">
                        <table style="margin-left: 50px; float: left; width: 350px; line-height: 34px;">
                            <tr>
                                <td align="right">
                                    用户名：
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="C_FName" runat="server" class="login_txt1" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    密&nbsp;&nbsp;码：
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="C_FPwd" runat="server" class="login_txt1" TextMode="Password" TabIndex="1"></asp:TextBox></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    验证码：
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="YZM" runat="server" class="login_txt2" MaxLength="5" TabIndex="1"></asp:TextBox>
                                    <cc1:EndyVCode ID="vilideCode" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="m_btn_w4" Text="企业登陆" OnClick="btnLogin_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnEmpLogin" runat="server" CssClass="m_btn_w4" Text="个人登陆" OnClick="btnReg_Click" />
                                    &nbsp;
                                    <input type="reset" value="CA证书驱动下载" id="dowQD" onclick="DowCAQD();" class="m_btn_w6" />
                                </td>
                            </tr>
                        </table>
                        <%--<div id="Ent_ta_User" runat="server">
                            <div style="text-align: left; padding-top: 4px; padding-left: 50px;">
                                <asp:Button ID="btnLogin" runat="server" Text="" CssClass="login_btn_in" OnClick="btnLogin_Click" />
                                &nbsp;&nbsp;
                                
                            </div>
                        </div>--%>
                    </div>
                    <%--<div style="position: absolute; top: 264px; left: 366px; width: 300px; height: 30px;">
                        <div class="tab_ut" id="tab_User" name="Ent" title="单位版登录" runat="server">
                            <img src="../../image/gif_48_057.gif" />
                            单位登陆
                        </div>
                        <div class="tab_ut" id="tab_Reg" title="用户注册" onclick="window.location='RegJSDWUser.aspx?entsystemid=100&sys=x'">
                            <img src="../../image/gif_48_103.gif" />
                            用户注册
                        </div>
                    </div>--%>
                    <%--<div style="position: absolute; top: 458px; left: 200px; width: 590px; height: 26px;
                        line-height: 26px; color: #FFFFFF;">
                        技术支持：
                        <asp:Literal ID="liC_TechSupport" runat="server"></asp:Literal>
                        &nbsp;&nbsp;&nbsp;电话：
                        <asp:Literal ID="liC_webtel" runat="server"></asp:Literal>
                        <asp:Literal ID="liC_Developer" runat="server" Visible="false"></asp:Literal>
                    </div>--%>
                </div>
            </td>
        </tr>
    </table>
    <input type="hidden" value="Ent" id="Hidden1" runat="server" />
    <input type="hidden" id="CaCerti" name="CaCerti" value="" runat="server" />
    </form>
</body>
</html>
