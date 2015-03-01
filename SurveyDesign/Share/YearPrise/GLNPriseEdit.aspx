<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GLNPriseEdit.aspx.cs" Inherits="Admin_mainother_SmsEdit" %>

<%@ Register Namespace="Tools" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信息管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="javascript" src="../../DateSelectNew/WdatePicker.js"></script>

    <script type="text/javascript">
        //验证
        function CheckInfo() {
            if (AutoCheckInfo()) {
                this.disabled = true
                return true
            }
            return false;
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="l_BB" runat="server" Text="短信发送"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar" id="Table1">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" Text="发送" CssClass="m_btn_w2" OnClick="btnAdd_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table align="center" style="width: 98%;" class="m_table">
        <tr>
            <td class="t_r t_bg">
                手机号：
            </td>
            <td>
                <asp:TextBox ID="t_FMobile" runat="server" CssClass="m_txt" MaxLength="11" Width="90"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                内容：
            </td>
            <td style="padding: 3px;">
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Height="141px" TextMode="MultiLine"
                    Width="257px"></asp:TextBox><tt>*</tt>超过60个字时，分多条分送。
            </td>
        </tr>
        <tr id="tr1" visible="false" runat="server">
            <td class="t_r t_bg">
                状态：
            </td>
            <td style="padding-left: 3px;">
                <asp:DropDownList ID="t_FState" runat="server">
                    <asp:ListItem Text="未发送" Value="0"></asp:ListItem>
                    <asp:ListItem Text="已发送" Value="1"></asp:ListItem>
                    <asp:ListItem Text="发送失败" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                计划发送时间：
            </td>
            <td>
                <asp:TextBox ID="t_FPlanTime" runat="server" CssClass="m_txt" Width="90px" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                验证码：
            </td>
            <td>
                <input id="tt_code" runat="server" class="m_txt" maxlength="5" type="text" style="width: 55px;" />
                <tt>*</tt>
                <cc1:EndyVCode ID="vilideCode" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
