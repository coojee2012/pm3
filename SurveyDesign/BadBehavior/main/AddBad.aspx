<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBad.aspx.cs" Inherits="BadBehavior_main_AddBad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>举报</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function checkInfo() {
            if (!AutoCheckInfo()) {
                return false;
            }
            if (!getLength(document.getElementById("t_FContent"), 50, '“举报内容”')) {
                return false;
            }

            return true;
        }
        
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                市场不良行为举报
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="暂存" CssClass="m_btn_w2" CommandArgument="0"
                    OnClientClick="return checkInfo();" OnCommand="btnSave_Click" />
                <asp:Button ID="btnReport" runat="server" Text="提交" CssClass="m_btn_w2" CommandArgument="1"
                    OnClientClick="return checkInfo();" OnCommand="btnSave_Click" />
                <input type="button" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                举报对象：
            </td>
            <td>
                <asp:TextBox ID="t_FSubject" runat="server" CssClass="m_txt" Width="285px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                举报内容：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Width="287px" MaxLength="30"
                    Height="91px" TextMode="MultiLine"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                处理结果：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FResult" runat="server" CssClass="m_txt" Width="287px" MaxLength="30"
                    Height="60px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                举报时间：
            </td>
            <td>
                <asp:TextBox ID="t_FReportTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt"
                    MaxLength="18" Width="90px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                状态：
            </td>
            <td>
                <asp:Label ID="liState" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
