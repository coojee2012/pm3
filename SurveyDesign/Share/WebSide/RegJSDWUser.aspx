<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegJSDWUser.aspx.cs" Inherits="Share_WebSide_RegJSDWUser" %>

<%@ Register Namespace="Tools" TagPrefix="cc1" %>
<%@ Register Src="../../Common/govdeptid3.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>[建设单位用户注册]四川省勘察设计科技信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../style/reg.js"></script>

    <style type="text/css">
        .nospan span { display: inline-table; width: auto; height: auto; text-align: left; line-height: 20px; }
    </style>

    <script type="text/javascript">
        function load() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }
        function EndRequestHandler() {
            /*===单位类型======*/
            $("#t_FSystemId").focus(function() {
                $("#tip_FSystemId").attr("class", "tip");
                $("#tip_FSystemId").text("选择您的单位类型");
            });
            $("#t_FSystemId").change(function() {
                if ($(this).val() == "") {
                    $("#tip_FSystemId").attr("class", "error");
                    $("#tip_FSystemId").text("请选择单位类型。");
                }
                else {
                    $("#tip_FSystemId").attr("class", "success");
                    $("#tip_FSystemId").text("");


                    /*单位名称AJAX验证*/
                    check_FCompany();
                    /*组织机构代码AJAX验证*/
                    check_FJuridcialCode();
                    /*营业执照号AJAX验证*/
                    check_FLicence();
                }
            });

            if ($("#t_FSystemId").val() == "") {
                $("#tip_FSystemId").attr("class", "error");
                $("#tip_FSystemId").text("请选择单位类型。");
            }
            else {
                $("#tip_FSystemId").attr("class", "success");
                $("#tip_FSystemId").text("");
            }

        }
        function doJsdw(obj) {
            if (obj.value != "" && obj.value != "100")
                location.href = "RegEntUser.aspx?sys=" + obj.value;
        }
        function showDiv(v) {
            $(".c").hide();
            $(".c" + v).show();

            $(".b").show();
            $(".b" + v).hide();

            $(".c").not($(".c" + v)).find(":text").val('');
            $(".c").not($(".c" + v)).find("b[id^=tip_]").text('');
            $(".c").not($(".c" + v)).find("b[id^=tip_]").removeAttr("class");
            $(".b" + v).find(":text").val('');
            $(".b" + v).find("b[id^=tip_]").text('');
            $(".b" + v).find("b[id^=tip_]").removeAttr("class");
        }
        function checkInfo() {
            var msg = "请将信息填写完整！";
            var ok = true;
            if ($('#t_FCompany').val().trim().length < 2) {
                alert('单位名称的字符太短！');
                $('#t_FCompany').focus();
                return false;
            }
            var v = $('#t_FEntTypeId').val();
            if ($('#t_FEntTypeId').val() == '') {
                alert('请选择单位性质！');
                $('#t_FEntTypeId').focus();
                return false;
            }
            if ($('#t_FCompany').val() == '')
                ok = false;
            else if ($('#t_FRegistAddress').val() == '')
                ok = false;
            else if ($('#t_FRegistAddress').val() == '')
                ok = false;
            else if ($("#t_FSystemId").val() == "")
                ok = false;
            else if ($("#t_FName").val() == "")
                ok = false;
            else if ($("#t_FPassWord").val() == "")
                ok = false;
            else if ($("#tt_FPassWord").val() == "")
                ok = false;
            else if ($("#YZM").val() == "")
                ok = false;
            else {
                $.each($(".c" + v).find(":text"), function() {
                    if (this.value == '') {
                        ok = false;
                        return false;
                    }
                });
                $.each($(".b").not($(".b" + v)).find(":text"), function() {
                    if (this.value == '') {
                        ok = false;
                        return false;
                    }
                });
                if ($('b.error').size() > 0) {
                    msg = "请将信息填写正确！";
                    ok = false;
                }
            }
            if (!ok) {
                alert(msg);
            } return ok;
        }
        $(function() {
            $('#t_FEntTypeId').change(function() {
                showDiv(this.value);
            });
            showDiv($('#t_FEntTypeId').val());
        });
    </script>

</head>
<body onload="load()">
    <form id="form1" runat="server">
    <div class="reg_top">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <div class="reg_mid">
        <div style="height: 40px; border-bottom: solid 1px #CCCCCC; width: 800px; margin: 0 auto;
            color: #676767; line-height: 40px; font-size: 14px; font-weight: bold; padding-left: 4px;">
            <div style="width: 200px; float: left;">
                建设单位用户注册
            </div>
            <span style="display: block; float: right; font-size: 12px; font-weight: normal;">请准确填写您的注册信息</span>
        </div>
        <div class="reg_m_top">
            <span></span>
            <div>
            </div>
            <strong></strong>
        </div>
        <div class="reg_m_mid">
            如果您已经拥有帐号，请从这里<a href="JSDWJGLogin.aspx">登陆</a>
        </div>
        <div class="reg_m_bot">
            <span></span>
            <div>
            </div>
            <strong></strong>
        </div>
        <div class="reg_txtline">
            <span>单位类型：</span>
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="t_FSystemId" runat="server" AutoPostBack="true" OnSelectedIndexChanged="t_FSystemId_SelectedIndexChanged"
                        onchange="doJsdw(this)">
                    </asp:DropDownList>
                    <tt>*</tt>
                </ContentTemplate>
            </asp:UpdatePanel>
            <strong><b id="tip_FSystemId" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline">
            <span>单位名称：</span>
            <div>
                <asp:TextBox ID="t_FCompany" runat="server" CssClass="reg_txt" Width="210px" MaxLength="40"> </asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FCompany" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline">
            <span>单位地址：</span>
            <div>
                <asp:TextBox ID="t_FRegistAddress" runat="server" CssClass="reg_txt" Width="210px"
                    MaxLength="40"> </asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FRegistAddress" style="display: block; font-weight: normal;"></b>
            </strong>
        </div>
        <div class="reg_txtline">
            <span>单位性质：</span>
            <div>
                <asp:DropDownList ID="t_FEntTypeId" runat="server" Width="210px">
                </asp:DropDownList>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FEntTypeId" style="display: block; font-weight: normal;"></b>
            </strong>
        </div>
        <div class="reg_txtline" style="height: auto;">
            <table style="color: #666666;">
                <tr>
                    <td style="width: 90px; line-height: 20px;" align="right" valign="middle">
                        <asp:Literal ID="lit_Name" runat="server" Text="所属地："></asp:Literal>
                    </td>
                    <td style="width: 330px;" class="nospan">
                        <uc1:Govdept ID="Govdept1" runat="server" />
                    </td>
                    <td valign="middle">
                        <strong><b id="B1" style="display: block; font-weight: normal;"></b></strong>
                    </td>
                </tr>
            </table>
        </div>
        <div class="reg_txtline c c18102 c18103">
            <span>法人代表：</span>
            <div>
                <asp:TextBox ID="t_FOTxt5" runat="server" CssClass="reg_txt" Width="210px" MaxLength="6"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FOTxt5" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline c c18102 c18103">
            <span>法人手机号：</span>
            <div>
                <asp:TextBox ID="t_FMobile" runat="server" CssClass="reg_txt" Width="210px" MaxLength="12"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FMobile" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline b b18106">
            <span>联系人：</span>
            <div>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="reg_txt" Width="210px" MaxLength="6"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FLinkMan" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline b b18106">
            <span>联系人电话：</span>
            <div>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="reg_txt" Width="210px" MaxLength="12"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FTel" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline c c18102 c18103">
            <span>组织机构代码：</span>
            <div>
                <asp:TextBox ID="t_FJuridcialCode" runat="server" CssClass="reg_txt" Width="210px"
                    MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FJuridcialCode" style="display: block; font-weight: normal;"></b>
            </strong>
        </div>
        <div class="reg_txtline c c18103">
            <span>营业执照号：</span>
            <div>
                <asp:TextBox ID="t_FLicence" runat="server" CssClass="reg_txt" Width="210px" MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FLicence" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline c c18106">
            <span>身份证：</span>
            <div>
                <asp:TextBox ID="t_FIdCard" runat="server" CssClass="reg_txt" Width="210px" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FIdCard" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline c c18106">
            <span>联系电话：</span>
            <div>
                <asp:TextBox ID="t_FCall" runat="server" CssClass="reg_txt" Width="210px" MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FCall" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline">
            <span>电子邮箱：</span>
            <div>
                <asp:TextBox ID="t_FEmail" runat="server" CssClass="reg_txt" Width="210px" MaxLength="50"></asp:TextBox>
            </div>
        </div>
        <div class="reg_txtline">
            <span>备注：</span>
            <div>
                <asp:TextBox ID="t_FRemark" runat="server" CssClass="reg_txt" Width="210px" MaxLength="50"></asp:TextBox>
            </div>
        </div>
        <div class="reg_txtline" style="display:none;">
            <span>用户名：</span>
            <div>
                <asp:TextBox ID="t_FName" runat="server" CssClass="reg_txt" Width="210px" MaxLength="18"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FName" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline">
            <span>密码：</span>
            <div>
                <input id="t_FPassWord" runat="server" type="password" class="reg_txt" style="width: 210px;"
                    maxlength="16" />
                <tt>*</tt>
            </div>
            <strong><b id="tip_FPassWord" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div id="pwdDIV" style="height: 12px; width: 360px; margin: 0 auto; display: none;">
            <div class="f_l" style="width: auto;">
                <table id="pwdStrong_color">
                    <tr>
                        <td id="pwdStrong_1">
                        </td>
                        <td id="pwdStrong_2">
                        </td>
                        <td id="pwdStrong_3">
                        </td>
                        <td id="pwdStrong_4">
                        </td>
                    </tr>
                </table>
            </div>
            <span id="pwdStrong_text"></span>
        </div>
        <div class="reg_txtline">
            <span>确认密码：</span>
            <div>
                <input id="tt_FPassWord" runat="server" type="password" class="reg_txt" style="width: 210px;"
                    maxlength="16" />
                <tt>*</tt>
            </div>
            <strong><b id="ttip_FPassWord" style="display: block; font-weight: normal;"></b>
            </strong>
        </div>
        <div class="reg_txtline" style="height: auto;">
            <table style="color: #666666;">
                <tr>
                    <td style="width: 90px; line-height: 20px;" align="right" valign="middle">
                        选择要开通的&nbsp;<br />
                        系统权限：
                    </td>
                    <td style="width: 230px;" class="nospan">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="r_FSysList" runat="server">
                                </asp:CheckBoxList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td valign="middle">
                        <strong><b id="tip_CheckSys" style="display: block; font-weight: normal;"></b></strong>
                    </td>
                </tr>
            </table>
        </div>
        <div class="reg_txtline">
            <span>验证码：</span>
            <div>
                <asp:TextBox ID="YZM" runat="server" class="reg_txt" MaxLength="5" Width="132px"
                    TabIndex="1"></asp:TextBox>
                <tt>*</tt>
                <cc1:EndyVCode ID="vilideCode" runat="server" />
            </div>
            <strong><b id="tip_YZM" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div style="margin: 20px auto 0 auto; width: 400px; height: 50px;">
            <div class="f_l">
                <asp:Button ID="btnREGJSDW" runat="server" CssClass="reg_btn0" Text="点击注册" OnClick="btnREG_Click"
                    OnClientClick="return checkInfo(this);" />
            </div>
            
        </div>
    </div>
    <div class="reg_bot">
    </div>
    <div class="log_txt2" style="margin-bottom: 50px; text-align: center;">
        技术支持：
        <asp:Literal ID="liC_TechSupport" runat="server"></asp:Literal>
        电话：
        <asp:Literal ID="liC_webtel" runat="server"></asp:Literal>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Literal ID="liC_Developer" runat="server"></asp:Literal>
    </div>
    <input id="t_FManageDeptId" runat="server" type="hidden" />
    </form>
</body>
</html>
