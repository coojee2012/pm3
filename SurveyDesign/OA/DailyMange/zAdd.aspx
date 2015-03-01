<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="zAdd.aspx.cs"
    Inherits="OA_DailyMange_zAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的日程</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" class="m_title" width="98%">
        <tr>
            <th colspan="3">
                我的日程
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input class="m_btn_w2" type="button" value="返回" onclick="window.close();cl();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" width="60">
                日期：
            </td>
            <td>
                <asp:TextBox ID="t_FDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                内容：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FContent" runat="server" MaxLength="100" TextMode="MultiLine"
                    CssClass="m_txt" Width="90%" Height="160px"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script type="text/javascript">
    function cl() { if (parent.Dialog) parent.Dialog.close(); }
</script>

