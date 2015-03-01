<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gfzsEdit.aspx.cs" Inherits="Government_AppMain_gfzsEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">工法证书编辑
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
                <td class="t_r t_bg">工法编号：
                </td>
                <td>
                    <asp:TextBox ID="t_FNu" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r t_bg">省级工法批准文号：
                </td>
                <td>
                    <asp:TextBox ID="t_WH" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">批准时间：
                </td>
                <td>
                    <asp:TextBox ID="t_Fpztime" onfocus="WdatePicker()" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r t_bg">颁发部门：
                </td>
                <td>
                    <asp:TextBox ID="t_FDep" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">主要完成单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_DW" Width="450px" runat="server" CssClass="m_txt" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">主要完成人：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_RY" Width="450px" runat="server" CssClass="m_txt" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <input id="t_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
