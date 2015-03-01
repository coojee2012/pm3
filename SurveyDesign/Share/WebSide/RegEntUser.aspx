<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegEntUser.aspx.cs" Inherits="Share_WebSide_RegEntUser" %>

<%@ Register Namespace="Tools" TagPrefix="cc1" %>
<%@ Register Src="../../Common/govdeptid3.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>[企业用户注册]四川省勘察设计科技信息系统</title>
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
            if (obj.value == "100")
                location.href = "RegJsdwUser.aspx";
        }
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
                企业用户注册
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
            如果您已经拥有帐号，请从这里<a href="Default.aspx">登陆</a>
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
                        onchange="doJsdw(this);">
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
            <span>组织机构代码：</span>
            <div>
                <asp:TextBox ID="t_FJuridcialCode" runat="server" CssClass="reg_txt" Width="210px"
                    MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FJuridcialCode" style="display: block; font-weight: normal;"></b>
            </strong>
        </div>
        <div class="reg_txtline">
            <span>营业执照号：</span>
            <div>
                <asp:TextBox ID="t_FLicence" runat="server" CssClass="reg_txt" Width="210px" MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FLicence" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline" style="height: auto;">
            <table style="color: #666666;">
                <tr>
                    <td style="width: 90px; line-height: 20px;" align="right" valign="middle">
                        <asp:Literal ID="lit_Name" runat="server" Text="主管部门："></asp:Literal>
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
        <div class="reg_txtline">
            <span>联系人：</span>
            <div>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="reg_txt" Width="210px" MaxLength="6"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FLinkMan" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline">
            <span>联系电话：</span>
            <div>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="reg_txt" Width="210px" MaxLength="12"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FTel" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline">
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
                <asp:Button ID="btnREG" runat="server" CssClass="reg_btn0" Text="点击注册" OnClick="btnREG_Click" />
            </div>
            <div class="f_l" style="height: 50px; line-height: 25px; margin-left: 20px;">
                提示：注册后请等待管理员审核。
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
