<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zLook.aspx.cs" Inherits="OA_Bulletin_LookBull" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>通知公告</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="Label1" runat="server" Text="公告查看"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right">
                <input class="m_btn_w2" onclick="window.close();cl();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="m_table"
        style="width: 98%; height: 80px">
        <tr>
            <td align="center">
                <b>
                    <asp:Literal ID="t_FTitle" runat="server"></asp:Literal>
                </b>
            </td>
        </tr>
        <tr>
            <td align="center">
                发布时间：<asp:Literal ID="t_FCreateTime" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="height: 350px; vertical-align: top" colspan="2">
                <asp:Literal ID="t_FContent" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script type="text/javascript">
    function cl() { if (parent.Dialog) parent.Dialog.close(); }
</script>