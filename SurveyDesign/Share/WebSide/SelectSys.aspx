<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectSys.aspx.cs" Inherits="Share_WebSide_SelectSys" %>

<%@ Register Namespace="Tools" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业登陆</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../style/openother.js"></script>

    <style type="text/css">
        form
        {
            text-align: center;
            vertical-align: middle;
        }
        /*密码验证提示CSS */.tip
        {
            width: 140px;
        }
        .error
        {
            width: 140px;
        }
        .success
        {
            width: 140px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table height="100%" width="100%">
        <tr>
            <td align="center" valign="middle">
                <div class="reg_top">
                </div>
                <div class="reg_mid" style="padding: 20px 0; height: 460px;">
                    <div class="f_l l_bg" style="width: 200px; margin-right: 5px; height: 470px; padding-top: 5px;">
                        <div class="l_info_top">
                        </div>
                        <div class="l_info_mid">
                            <div class="l_name">
                                <asp:Literal ID="lit_EntName" runat="server"></asp:Literal>
                            </div>
                            <hr style="margin: 2px 8px; width: 150px;" />
                            <div class="l_helo">
                                <asp:Literal ID="lit_helo" runat="server"></asp:Literal>
                            </div>
                            <div class="l_helo t_r">
                                <asp:Literal ID="lit_Time" runat="server"></asp:Literal>
                            </div>
                            <div class="l_other">
                                联系人：<b><asp:Literal ID="lit_LinkMan" runat="server"></asp:Literal></b>
                            </div>
                            <div class="l_other">
                                联系电话：<b><asp:Literal ID="lit_Tel" runat="server"></asp:Literal></b>
                            </div>
                            <div class="l_other">
                                主帐户状态：<b><asp:Literal ID="lit_State" runat="server"></asp:Literal></b>
                            </div>
                            <div class="l_other">
                                有效结束日期：<b><asp:Literal ID="lit_EndTime" runat="server"></asp:Literal></b>
                            </div>
                        </div>
                        <div class="l_info_bot">
                        </div>
                        <div class="l_select">
                            <ul>
                                <li><a id="a1" href="#" class="li1_link">选择系统登陆</a></li>
                                <li><a id="a2" href="#" class="li2">开通其它系统</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="f_l " style="height: auto; width: 675px;">
                        <div id="div1" style="margin: 0 auto; width: 640px; display: none;">
                            <div style="height: 40px; border-bottom: solid 1px #CCCCCC; width: 640px; margin: 0 auto;
                                color: #676767; line-height: 40px; font-size: 14px; font-weight: bold; padding-left: 4px;">
                                <div style="width: 380px; float: left; text-align: left; margin-left: 20px;">
                                    请选择您要登陆的系统
                                </div>
                                <span style="display: block; float: right; font-size: 12px; font-weight: normal;">欢迎您，&nbsp;&nbsp;<a
                                    href="Login.aspx">［返回登陆页］</a> </span>
                            </div>
                            <div class="select_div2" runat="server" id="sys_List" style="margin-top: 20px;">
                                <a href="#"><span></span><strong>
                                    <samp>
                                        
                                    </samp>
                                    <b></b> <big>您该子帐户的状态：</big> </strong>
                                </a>
                            </div>
                        </div>
                        <div id="div2" style="margin: 0 auto; width: 640px;">
                            <div style="height: 40px; border-bottom: solid 1px #CCCCCC; width: 640px; margin: 0 auto;
                                color: #676767; line-height: 40px; font-size: 14px; font-weight: bold; padding-left: 4px;">
                                <div style="width: 380px; float: left; text-align: left; margin-left: 20px;">
                                    开通其它系统权限
                                </div>
                                <span style="display: block; float: right; font-size: 12px; font-weight: normal;"><a
                                    href="Login.aspx">［返回登陆页］</a> </span>
                            </div>
                            <div class="reg_m_top">
                                <span></span>
                                <div>
                                </div>
                                <strong></strong>
                            </div>
                            <div class="reg_m_mid" style="font-size: 12px; font-weight: normal; line-height: 18px;
                                text-align: left;">
                                <img src="../image/ts.gif" />&nbsp;提示：以下是您暂时没有开通的系统权限， 如果有需要的情况下您可以填写以下表单申请您所要开通的系统权限，待管理员审核后您便可使用该系统
                            </div>
                            <div class="reg_m_bot">
                                <span></span>
                                <div>
                                </div>
                                <strong></strong>
                            </div>
                            <div id="Other_reg" runat="server" style="width: 640px;">
                                <div class="reg_txtline" style="height: auto; text-align: left;">
                                    <table style="color: #666666;">
                                        <tr>
                                            <td style="width: 180px; line-height: 20px;" align="right" valign="middle">
                                                选择要开通的系统<tt>*</tt>：
                                            </td>
                                            <td style="width: 230px;" align="left">
                                                <asp:CheckBoxList ID="t_FSysList" runat="server">
                                                </asp:CheckBoxList>
                                            </td>
                                            <td valign="middle">
                                                <strong><b id="tip_CheckSys" style="display: block; font-weight: normal;"></b></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="reg_txtline" style="height: auto; text-align: left;">
                                    <table style="color: #666666;">
                                        <tr>
                                            <td style="width: 180px; line-height: 20px;" align="right" valign="middle">
                                                填写您的开通原由<tt>*</tt>：
                                            </td>
                                            <td style="width: 230px;" align="left">
                                                <asp:TextBox ID="t_FYY" runat="server" TextMode="MultiLine" CssClass="reg_txt" Height="93px"
                                                    Width="217px"></asp:TextBox>
                                            </td>
                                            <td valign="middle">
                                                <strong><b id="tip_FYY" style="display: block; font-weight: normal;"></b></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="reg_txtline" style="height: auto; text-align: left;">
                                    <table style="color: #666666;">
                                        <tr>
                                            <td style="width: 180px; line-height: 20px;" align="right" valign="middle">
                                                验证码<tt>*</tt>：
                                            </td>
                                            <td style="width: 230px;" align="left">
                                                <asp:TextBox ID="YZM" runat="server" class="reg_txt" MaxLength="5" Width="132px"
                                                    TabIndex="1"></asp:TextBox>
                                                <cc1:EndyVCode ID="vilideCode" runat="server" />
                                            </td>
                                            <td valign="middle">
                                                <strong><b id="tip_YZM" style="display: block; font-weight: normal;"></b></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin: 20px auto 0 auto; width: 400px; height: 50px;">
                                    <asp:Button ID="btnREG" runat="server" CssClass="reg_btn0" Text="提交申请" OnClick="btnREG_Click"
                                        UseSubmitBehavior="false" />
                                </div>
                            </div>
                            <div id="Other_NoReg" runat="server" style="width: 640px; height: 40px; line-height: 40px;">
                                <asp:Literal ID="lit_NoOther" runat="server"></asp:Literal>
                            </div>
                            <div id="Other_IsReg" runat="server" style="width: 640px;">
                                <div class="reg_txtline" style="height: auto; text-align: left;">
                                    <table style="color: #666666;">
                                        <tr>
                                            <td style="width: 180px; line-height: 30px;" align="right" valign="middle">
                                                要开通的系统：
                                            </td>
                                            <td style="width: 230px; color: #000000;" align="left">
                                                <asp:Literal ID="IsReg_SysList" runat="server">
                                                </asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="reg_txtline" style="height: auto; text-align: left;">
                                    <table style="color: #666666;">
                                        <tr>
                                            <td style="width: 180px; line-height: 30px; height: 40px;" align="right" valign="middle">
                                                开通原由 ：
                                            </td>
                                            <td style="width: 230px; color: #000000;" align="left">
                                                <asp:Literal ID="IsReg_YY" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="reg_txtline" style="height: auto; text-align: left;">
                                    <table style="color: #666666;">
                                        <tr>
                                            <td style="width: 180px; line-height: 30px;" align="right" valign="middle">
                                                状态 ：
                                            </td>
                                            <td style="width: 230px;" align="left">
                                                <asp:Literal ID="IsReg_State" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="reg_bot">
                </div>
                <div class="log_txt2" style="margin-bottom: 50px;">
                    技术支持：
                    <asp:Literal ID="liC_TechSupport" runat="server"></asp:Literal>
                    电话：
                    <asp:Literal ID="liC_webtel" runat="server"></asp:Literal>
                    &nbsp;
                    <asp:Literal ID="liC_Developer" runat="server"></asp:Literal>
                </div>
            </td>
        </tr>
    </table>
    <input id="hidd_n" runat="server" type="hidden" value="1" />
    <asp:Button ID="btn_n" runat="server" Text="" OnClick="btn_n_Click" Style="display: none;" />
    <input id="hidd_sysId" runat="server" type="hidden" />
    <input id="hidd_FID" runat="server" type="hidden" />
    <asp:Button ID="btnLogin" runat="server" Text="" OnClick="btnLogin_Click" Style="display: none;" />
    </form>
</body>
</html>
