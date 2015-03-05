<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditTSForm.aspx.cs" Inherits="Share_Main_EditTSForm" %>

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
    <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <input id="Button1" class="m_btn_w2" type="button" value="保存" />
                    <input id="btnGetBack" class="m_btn_w2" onclick="javascript:window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td><%--parent.document.frames('fileupload').parent.location.reload(); --%>
            </tr>
        </table>
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">投诉
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">投诉人</td>
            <td><asp:TextBox ID="txtJGLXMC" runat="server">王成</asp:TextBox></td>
            <td class="t_r t_bg">投诉时间</td>
            <td><asp:TextBox ID="txtJZMJ" CssClass="m_txt Wdate" Text="2014-11-15" runat="server"></asp:TextBox></td>
        </tr>
         <tr>
            <td class="t_r t_bg">所在单位</td>
            <td><asp:TextBox ID="txtYDXZMC" runat="server">川西南基建工程总公司</asp:TextBox></td>
            <td class="t_r t_bg">职务</td>
            <td><asp:TextBox ID="txtGCZJ" runat="server">会计</asp:TextBox></td>
        </tr>
         <tr>
            <td class="t_r t_bg">联系电话</td>
            <td><asp:TextBox ID="txtKGRQ" runat="server">18156940323</asp:TextBox></td>
            <td class="t_r t_bg">邮箱</td>
            <td><asp:TextBox ID="txtJGYSRQ" runat="server">1195830254@qq.com</asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">被投诉企业</td>
            <td colspan="3"><asp:TextBox ID="txtGCMC" runat="server" Width="500">四川彭州瑞信混凝土有限公司</asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">投诉事项</td>
            <td colspan="3"><asp:TextBox ID="txtJSYJ" runat="server" Width="500" CssClass="m_txt" Height="60" TextMode="MultiLine">投标作假</asp:TextBox></td>
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
                    <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt">李峰</asp:TextBox>
                </td>
                <td class="t_r">回复时间</td>
                <td>
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt Wdate">2014-11-17</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">所在部门
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="m_txt">审查中心</asp:TextBox>
                </td>
                <td class="t_r">职务
                </td>
                <td>
                    <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt">主任</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">处理意见
                </td>
                <td colspan="2">
                    <asp:TextBox ID="TextBox1" runat="server" Width="500" CssClass="m_txt" Height="60" TextMode="MultiLine">已处理</asp:TextBox>
                </td>
                <td><input type="checkbox" checked="checked" />是否发布
                </td>
            </tr>
        </table>
    </form>
</body>
</html>


