<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZTBJGLoginAll.aspx.cs" Inherits="Share_WebSide_ZTBJGLoginAll" %>

<%@ Register Namespace="Tools" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�Ĵ�ʡ���蹤����Ŀ�ۺϼ��ϵͳ-���赥λ</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link> 

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript">
        function checkInfo() {
            if (jQuery.trim($("#C_FName").val()) == "") {
                alert('�������¼��');
                $("#C_FName").focus();
                return false;
            }
            if (jQuery.trim($("#C_FPwd").val()) == "") {
                alert('����������');
                $("#C_FPwd").focus();
                return false;
            }
            if ($("#YZM").val() == "") {
                alert('��������֤��');
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

            //��½��ť
            $("#btnLogin").click(function() {
                if (!checkInfo())
                    return false;
            });
            $("#btnLogin0").click(function() {
                var number = getLockId();
                if (number) {
                    $("#C_FLockNumber").val(number);
                }
                else {
                    alert("��ȡ������ʧ�ܣ�\n\r\n\r��ȷ������ȷ��װ������������ȷ�����������");
                    return false;
                }
            });


            $("#d_d").change(function() {
                if ($(this).val() != "") {
                    var checkText = $(this).find("option:selected").text();  //��ȡSelectѡ���Text
                    var checkValue = $(this).val();  //��ȡSelectѡ���Value
                    $("#C_FName").val(checkText);
                    $("#C_FPwd").val(checkValue);
                }
                $(this).val("");
            });

            $(".tab_ut, .tab_ut_h").click(function() {
                var name = $(this).attr("name");
                $("#hiType").val(name);
                show();
            });

            //����ѡ�
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
        .login_in1
        {
            position: absolute;
            left: 70px;
            top: 270px;
            width: 420px;
        }
        .login_in1 div
        {
            height: 36px;
        }
        .login_in1 div tt
        {
            display: block;
            float: left;
            height: 36px;
            line-height: 36px;
            color: #196893;
            filter: Dropshadow(offx=0,offy=1,color=#FFFFFF) Dropshadow(offx=1,offy=1,color=#FFFFFF);
            text-shadow: 0px 2px 1px #FFFFFF,2px 2px 1px #FFFFFF;
        }
        .login_in1 div span
        {
            display: block;
            float: left;
            height: 36px;
        }
        .login_in1 div a
        {
            color: #FFFFFF;
            text-decoration: none;
        }
        .login_in1 div a:hover
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
                <div style="margin: 0 auto; position: relative; width: 780px; height: 540px; background-image: url(../image/JSDWLogin.jpg)">
                    <%--<table style="margin-left: 50px; float: left; width: 350px; line-height: 34px;">
                            <tr>
                                <td align="right">
                                    �û�����
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="C_FName" runat="server" class="login_txt1" TabIndex="1" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    ��&nbsp;&nbsp;�룺
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="C_FPwd" runat="server" class="login_txt1" TextMode="Password" TabIndex="1"
                                        ></asp:TextBox></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    ��֤�룺
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="YZM" runat="server" class="login_txt2" MaxLength="5" TabIndex="1"
                                        ></asp:TextBox>
                                    <cc1:EndyVCode ID="vilideCode" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="m_btn_w2" Text="��½" OnClick="btnLogin_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnReg" runat="server" CssClass="m_btn_w2" Text="ע��" 
                                        onclick="btnReg_Click"  />
                                    &nbsp;
                                    <input type="reset" value="����" id="bntExit" class="m_btn_w2" />
                                </td>
                            </tr>
                        </table>--%>
                    <div id="Ent" class="login_in1">
                        <asp:DropDownList ID="C_FType" CssClass="log_txtBox2" runat="server" Visible="false">
                            <asp:ListItem Value="ta_User">�û���</asp:ListItem>
                            <asp:ListItem Value="ta_lock" Selected="True">������</asp:ListItem>
                        </asp:DropDownList>
                        <div id="Ent_ta_User" runat="server">
                            <div style="height: 30px">
                                <tt style="height: 30px">�û�����</tt><span style="height: 30px">
                                    <asp:TextBox ID="C_FName" runat="server" class="login_txt1" TabIndex="1"></asp:TextBox></span>
                                <asp:DropDownList ID="d_d" runat="server">
                                    <asp:ListItem Text="��ʾ�û���ѡ" Value=""></asp:ListItem>
                                    <asp:ListItem Text="�Ĵ����赥λ" Value="123456"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="height: 30px">
                                <tt style="height: 30px">��&nbsp;&nbsp;�룺</tt><span style="height: 30px">
                                    <asp:TextBox ID="C_FPwd" runat="server" class="login_txt1" TextMode="Password" TabIndex="1"></asp:TextBox></span>
                            </div>
                            <div style="height: 30px">
                                <tt style="height: 30px">��֤�룺</tt><span style="height: 30px">
                                    <asp:TextBox ID="YZM" runat="server" class="login_txt2" MaxLength="5" TabIndex="1"></asp:TextBox>
                                    <cc1:EndyVCode ID="vilideCode" runat="server" />
                                </span>
                            </div>
                            <div style=" height:10px;">
                            
                            </div>
                            <div style="text-align: left; padding-top: 4px; padding-left: 50px;">
                                <asp:Button ID="btnLogin" runat="server" Text="��½" CssClass="m_btn_w4" OnClick="btnLogin_Click" />
                                
                                <input type="reset" value="���" id="bntExit" class="m_btn_w4" />
                                <input type="reset" value="ע��" id="Reset1" class="m_btn_w4"  onclick="window.location='RegJSDWUser.aspx?entsystemid=100&sys=x'"/>
                            </div>
                        </div>
                        <div id="Ent_ta_lock" style="display: none;" runat="server">
                            <div style="padding-top: 14px;">
                                <tt>�״ε�½��������==>><a href='../../����������������.exe' style="color: Red;" target="_blank"><b>���ؼ�����������</b></a></tt><span></span>
                            </div>
                            <div>
                                <tt>������������ȴ�ϵͳʶ��󣬵����½��ť </tt><span></span>
                            </div>
                            <div style="text-align: left; padding-top: 4px; padding-left: 50px;">
                                <input id="C_FLockNumber" type="hidden" runat="server" />
                                <asp:Button ID="btnLogin0" runat="server" Text="��������½" CssClass="m_btn_w6" OnClick="btnLogin_Click" />
                            </div>
                        </div>
                    </div>
                    <%--<div style="position: absolute; top: 264px; left: 366px; width: 300px; height: 30px;">
                        <div class="tab_ut" id="tab_User" name="Ent" title="��λ���¼" runat="server">
                            <img src="../../image/gif_48_057.gif" />
                            ��λ��½
                        </div>
                        <div class="tab_ut" id="tab_Reg" title="�û�ע��" onclick="window.location='RegJSDWUser.aspx?entsystemid=100&sys=x'">
                            <img src="../../image/gif_48_103.gif" />
                            �û�ע��
                        </div>
                    </div>--%>
                    <%--<div style="position: absolute; top: 458px; left: 200px; width: 590px; height: 26px;
                        line-height: 26px; color: #FFFFFF;">
                        ����֧�֣�
                        <asp:Literal ID="liC_TechSupport" runat="server"></asp:Literal>
                        &nbsp;&nbsp;&nbsp;�绰��
                        <asp:Literal ID="liC_webtel" runat="server"></asp:Literal>
                        <asp:Literal ID="liC_Developer" runat="server" Visible="false"></asp:Literal>
                    </div>--%>
                </div>
            </td>
        </tr>
    </table>
    <input type="hidden" value="Ent" id="hiType" />
    </form>
</body>
</html>
