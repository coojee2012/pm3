<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntselectSysTem.aspx.cs"
    Inherits="ApproveWeb_Main_EntselectSysTem" %>

<html>
<head>
    <title>四川省勘察设计科技信息系统</title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>

    <script src="../script/jquery.js" type="text/javascript"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table border="0" width="100%" height="100%">
        <tr>
            <td align="center" valign="middle">
                <table class="e_log_t">
                    <tr style="height: 255px;">
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr style="height: 145px;">
                        <td style="width: 230px;">
                        </td>
                        <td valign="top" align="left">
                            <table>
                                <tr>
                                    <td height="60px">
                                        请选择系统：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dbSystem" runat="server" Width="170px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="height: 39px">
                                        <input id="btnLogin" type="button" class="m_btn_b1" value="登 陆" runat="server" onserverclick="btnPost_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">
                            技术支持：
                            <asp:Literal ID="liC_TechSupport" runat="server"></asp:Literal>
                            电话：
                            <asp:Literal ID="liC_webtel" runat="server"></asp:Literal>
                            <br />
                            <br />
                            <asp:Literal ID="liC_Developer" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnPost" runat="server" Text="登录" Style="display: none" />
    <input id="C_FType" type="hidden" runat="server" value="3" />
    </form>
</body>
</html>
