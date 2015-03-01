<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addProject.aspx.cs" Inherits="GFEnt_ApplyEnt_addProject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <base target="_self"></base>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">工法应用工程
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">工程名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_GCMC" Width="350px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">工程所在地：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_SZD" Width="350px" runat="server" CssClass="m_txt"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">开工时间：
                </td>
                <td>
                    <asp:TextBox ID="t_KSSJ" runat="server" Width="150px" onfocus="WdatePicker()" CssClass="m_txt"></asp:TextBox>

                </td>
                <td class="t_r t_bg">竣工时间：
                </td>
                <td>
                    <asp:TextBox ID="t_JSSJ" Width="150px" runat="server" onfocus="WdatePicker()" CssClass="m_txt"></asp:TextBox>

                </td>
            </tr>
        </table>
        <input id="t_YWBM" runat="server" type="hidden" />
        <input id="t_FBaseInfoId" runat="server" type="hidden" />
        <input id="t_FSystemId" runat="server" type="hidden" />
        <input id="t_GCID" runat="server" type="hidden" />
    </form>
</body>
</html>
