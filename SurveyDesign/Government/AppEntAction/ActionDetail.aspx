<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActionDetail.aspx.cs" Inherits="Government_AppEntAction_ActionDetail" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>处罚标准</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                处罚标准
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r">
                <input id="btnRetrun" class="m_btn_w2" type="button" value="返回" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td width="15%" class="t_r">
                行为代码：
            </td>
            <td style="padding-left: 5px;">
                <asp:Label ID="t_FNumber" runat="server" CssClass="cLabel1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                单位类型：
            </td>
            <td style="padding-left: 5px;">
                <asp:Label ID="txtPPFName" runat="server" CssClass="cLabel1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                行为类别：
            </td>
            <td style="padding-left: 5px;">
                <asp:Label ID="txtPFName" runat="server" CssClass="cLabel1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                分数：
            </td>
            <td style="padding-left: 5px;">
                <asp:Label ID="t_FScore" runat="server" CssClass="cLabel1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                行为描述：
            </td>
            <td style="padding-left: 5px;">
                <asp:Label ID="t_FDesc" runat="server" CssClass="cLabel1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                法律依据：
            </td>
            <td style="padding-left: 5px;">
                <asp:Label ID="t_FLawGist" runat="server" CssClass="cLabel1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                处罚依据：
            </td>
            <td style="padding-left: 5px;">
                <asp:Label ID="t_FPunishGist" runat="server" CssClass="cLabel1"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
