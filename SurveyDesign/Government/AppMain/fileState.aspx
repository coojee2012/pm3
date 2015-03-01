<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fileState.aspx.cs" Inherits="Government_AppMain_fileState" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">检验状态
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
                <td class="t_r t_bg">状态：</td>
                <td>
                    <asp:DropDownList ID="t_Fstate" runat="server" CssClass="m_txt">
                        <asp:ListItem Text="未核验" Value="未核验"></asp:ListItem>
                        <asp:ListItem Text="待核验" Value="待核验"></asp:ListItem>
                        <asp:ListItem Text="已核验" Value="已核验"></asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t_r t_bg">备注：</td>

                <td>
                    <asp:TextBox ID="t_Fremark" runat="server" Height="160px" TextMode="MultiLine" Width="280px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <input id="t_YWBM" runat="server" type="hidden" />
        <input id="t_ftype" runat="server" type="hidden" />
        <input id="t_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
