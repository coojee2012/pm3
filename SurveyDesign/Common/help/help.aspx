<%@ Page Language="C#" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="Common_help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>帮助</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5" style="background: url(../../Skin/Blue/image/help.gif) 6px 3px no-repeat;">
                帮助信息
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_c t_bg" style="color: #666666; font-weight: bold;">
                <asp:Literal ID="t_FTitle" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="padding: 4px;" valign="top">
                <div style="overflow: auto; height: 380px;">
                    <asp:Literal ID="t_FContent" runat="server"></asp:Literal>
                </div>
            </td>
        </tr>
    </table>
    <div style="text-align: center; margin-top: 4px;">
        <input type="button" class="m_btn_w2" onclick="window.close();" value="关闭" />
    </div>
    </form>
</body>
</html>
