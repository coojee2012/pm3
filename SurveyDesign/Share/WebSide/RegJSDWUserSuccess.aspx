<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegJSDWUserSuccess.aspx.cs"
    Inherits="Share_WebSide_RegJSDWUserSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>[注册成功]四川省勘察设计科技信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <style type="text/css">
        .tt_l { width: 100px; text-align: right; padding-right: 10px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="reg_top">
    </div>
    <div class="reg_mid">
        <div style="height: 40px; border-bottom: solid 1px #CCCCCC; width: 800px; margin: 0 auto;
            color: #676767; line-height: 40px; font-size: 14px; font-weight: bold; padding-left: 4px;">
            <div class="f_l">
                企业用户注册
            </div>
            <span style="float: right; display: block; font-size: 12px; font-weight: normal;">
            </span>
        </div>
        <div class="reg_m_top">
            <span></span>
            <div>
            </div>
            <strong></strong>
        </div>
        <div class="reg_m_mid">
            恭喜，注册成功。<tt>请牢记您的用户名和密码以免丢失</tt>
        </div>
        <div class="reg_m_bot">
            <span></span>
            <div>
            </div>
            <strong></strong>
        </div>
        <div class="reg_txtline">
            <div class="tt_l" style="width: 170px;">
                单位名称：
            </div>
            <samp>
                <asp:Literal ID="t_FCompany" runat="server"></asp:Literal>
            </samp>
        </div>
        <div class="reg_txtline" style="display: none">
            <div class="tt_l" style="width: 170px;">
                组织机构代码：
            </div>
            <samp>
                <asp:Literal ID="t_FJuridcialCode" runat="server"></asp:Literal>
            </samp>
        </div>
        <div class="reg_txtline" style="display: none">
            <div class="tt_l" style="width: 170px;">
                营业执照号：
            </div>
            <samp>
                <asp:Literal ID="t_FLicence" runat="server"></asp:Literal>
            </samp>
        </div>
        <div class="reg_txtline">
            <div class="tt_l" style="width: 170px;">
                用户名：</div>
            <samp>
                <asp:Literal ID="t_FName" runat="server"></asp:Literal></samp>
        </div>
        <div class="reg_txtline">
            <div class="tt_l" style="width: 170px;">
                密码：</div>
            <samp>
                <asp:Literal ID="t_FPassWord" runat="server"></asp:Literal></samp>
        </div>
        <div class="reg_txtline">
            <div class="tt_l" style="width: 170px;">
                主管部门：</div>
            <samp>
                <asp:Literal ID="t_FManageDeptName" runat="server"></asp:Literal></samp>
        </div>
        <div class="reg_txtline">
            <div class="tt_l" style="width: 170px;">
                联系人：</div>
            <samp>
                <asp:Literal ID="t_FLinkMan" runat="server"></asp:Literal></samp>
        </div>
        <div class="reg_txtline">
            <div class="tt_l" style="width: 170px;">
                联系电话：</div>
            <samp>
                <asp:Literal ID="t_FTel" runat="server"></asp:Literal></samp>
        </div>
        <table style="width: 570px;" align="center">
            <tr>
                <td style="width: 170px; height: 40px; line-height: 40px; padding-right: 10px;" align="right"
                    valign="top">
                    <asp:Literal ID="lit_Name" runat="server" Text="申请开通的系统权限："></asp:Literal>
                </td>
                <td style="line-height: 20px; color: #02376A; font-weight: bold;">
                    <samp>
                        <asp:Literal ID="t_FSysList" runat="server"></asp:Literal>
                    </samp>
                </td>
            </tr>
        </table>
        <div style="margin: 20px auto 0 auto; width: 500px; height: 20px;">
            <tt>提示：系统已经接收到您的注册信息，待管理员审核成功后，你便可登陆系统</tt>
        </div>
        <div style="margin: 20px auto 0 auto; width: 400px; height: 50px;">
            <asp:Button ID="btnREG" runat="server" CssClass="reg_btn0" Text="返回注册页" OnClick="btnREG_Click" />
            <asp:Button ID="btnLogin" runat="server" CssClass="reg_btn0" Style="margin-left: 15px;"
                Text="返回登陆页" OnClick="btnLogin_Click" />
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
    <input id="t_FSystemId" runat="server" type="hidden" value="8001" />
    </form>
</body>
</html>
