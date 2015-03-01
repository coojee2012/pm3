<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegQuaEntUser.aspx.cs" Inherits="Share_WebSide_RegEntUser" %>

<%@ Register Namespace="Tools" TagPrefix="cc1" %>
<%@ Register Src="../../Common/govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业用户注册</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>


    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script src="../style/reg.js" type="text/javascript"></script>

    <style type="text/css">
        .reg_top
        {
        	background: url(../image/regTop3.gif) no-repeat;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="reg_top">
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
            如果您已经拥有帐号，请从这里<a href="#">登陆</a>
        </div>
        <div class="reg_m_bot">
            <span></span>
            <div>
            </div>
            <strong></strong>
        </div>
        <div class="reg_txtline">
            <span>企业名称：</span>
            <div>
                <asp:TextBox ID="t_FCompany" runat="server" CssClass="reg_txt" Width="210px"></asp:TextBox>
                <tt>*</tt>
            </div>
            <strong><b id="tip_FCompany" style="display: block; font-weight: normal;"></b></strong>
        </div>
        <div class="reg_txtline">
            <span>用户名：</span>
            <div>
                <asp:TextBox ID="t_FName" runat="server" CssClass="reg_txt" Width="210px"></asp:TextBox>
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
        <div class="reg_txtline">
            <span>主管部门：</span>
            <div class="divGovdept">
                <uc1:Govdept ID="Govdept1" runat="server" />
            </div>
        </div>
        <div class="reg_txtline">
            <span>联系人：</span>
            <div>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="reg_txt" Width="210px"></asp:TextBox>
            </div>
            <strong></strong>
        </div>
        <div class="reg_txtline">
            <span>联系电话：</span>
            <div>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="reg_txt" Width="210px"></asp:TextBox>
            </div>
            <strong></strong>
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
            <asp:Button ID="btnREG" runat="server" CssClass="reg_btn0" Text="点击注册" OnClick="btnREG_Click" />
        </div>
    </div>
    <div class="reg_bot">
    </div>
    <input id="t_FManageDeptId" runat="server" type="hidden" />
    <input id="t_FSystemId" runat="server" type="hidden" value="8002" />
    </form>
</body>
</html>
