<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSForm.aspx.cs" Inherits="Share_Main_TSForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
     <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">投诉
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">投诉人</td>
            <td><asp:TextBox ID="txtJGLXMC" runat="server"></asp:TextBox></td>
            <td class="t_r t_bg">投诉时间</td>
            <td><asp:TextBox ID="txtJZMJ" CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" runat="server"></asp:TextBox></td>
        </tr>
         <tr>
            <td class="t_r t_bg">所在单位</td>
            <td><asp:TextBox ID="txtYDXZMC" runat="server"></asp:TextBox></td>
            <td class="t_r t_bg">职务</td>
            <td><asp:TextBox ID="txtGCZJ" runat="server"></asp:TextBox></td>
        </tr>
         <tr>
            <td class="t_r t_bg">联系电话</td>
            <td><asp:TextBox ID="txtKGRQ" runat="server"></asp:TextBox></td>
            <td class="t_r t_bg">邮箱</td>
            <td><asp:TextBox ID="txtJGYSRQ" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">被投诉企业</td>
            <td colspan="3"><asp:TextBox ID="txtGCMC" Enabled="false" runat="server" Width="500"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">投诉事项</td>
            <td colspan="3"><asp:TextBox ID="txtJSYJ" runat="server" Width="500" CssClass="m_txt required" Height="60" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
    </table>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="4" style="text-align: left;">处理意见
                </th>
            </tr>
            <tr>
                <td class="t_r">回复人</td>
                <td>
                    <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">回复时间</td>
                <td>
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">所在部门
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">职务
                </td>
                <td>
                    <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">处理意见
                </td>
                <td colspan="2">
                    <asp:TextBox ID="TextBox1" runat="server" Width="500" CssClass="m_txt required" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td><input type="checkbox" />是否发布</td>
            </tr>
        </table>
    </form>
</body>
</html>

