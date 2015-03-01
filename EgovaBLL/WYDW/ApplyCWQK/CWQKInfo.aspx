<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CWQKInfo.aspx.cs" Inherits="WYDW_ApplyCWQK_CWQKInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目财务情况</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();

        });
        function checkInfo() {
            return AutoCheckInfo();
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 100%; width: 100%;">
            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">项目财务情况
                    </th>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
                <table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td></td>
                        <td class="t_r">
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                OnClientClick="return checkInfo();" />
                            <%--<input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />--%>
                        </td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>
                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td class="t_r t_bg" colspan="3" width="15%">所属年度：
                        </td>
                        <td width="15%" colspan="1">
                            <asp:Label ID="t_ND" runat="server" Width="195px" MaxLength="40"></asp:Label>
                        </td>
                        <td width="50%"></td>
                    </tr>
                    <tr>
                        <td rowspan="6" class="t_r t_bg">营业收入
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" class="t_r t_bg">物业费
                        </td>
                        <td class="t_r t_bg">总额：
                        </td>
                        <td>
                            <asp:TextBox ID="t_WYFZE" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td>元</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">收费率：
                        </td>
                        <td>
                            <asp:TextBox ID="t_WYFSFL" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td>%</td>

                    </tr>
                    <tr>
                        <td colspan="2" class="t_r t_bg">停车费：
                        </td>
                        <td>
                            <asp:TextBox ID="t_TCF" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td>元</td>
                    </tr>
                    <tr>
                        <td colspan="2" class="t_r t_bg">广告费：
                        </td>
                        <td>
                            <asp:TextBox ID="t_GGF" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td>元</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg" colspan="2">其他：
                        </td>
                        <td>
                            <asp:TextBox ID="t_QT" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td>元</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg" colspan="3">营业成本：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_YYCB" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td>元</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg" colspan="3">营业利润：
                        </td>
                        <td  colspan="1">
                            <asp:TextBox ID="t_YYLR" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td>元</td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
