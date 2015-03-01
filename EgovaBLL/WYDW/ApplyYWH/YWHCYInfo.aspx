<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YWHCYInfo.aspx.cs" Inherits="WYDW_ApplyYWH_YWHCYInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="height: 100%; width: 100%;">
            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">业主委员会成员信息
                    </th>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
                <table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td></td>
                        <td class="t_r">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click1" OnClientClick="return checkInfo()" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />

                        </td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>
                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td class="t_r t_bg" width="15%">姓名：
                        </td>
                        <td width="35%">
                            <asp:TextBox ID="t_XM" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td class="t_r t_bg" width="15%">性别：
                        </td>
                        <td width="35%">
                            <asp:DropDownList ID="t_XB" runat="server" CssClass="m_txt" Width="195px">
                                <asp:ListItem Value="1" Selected="True">男</asp:ListItem>
                                <asp:ListItem Value="0">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">年龄：
                        </td>
                        <td>
                            <asp:TextBox ID="t_NL" onblur="isInt(this)" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox><tt>*</tt>
                        </td>
                        <td class="t_r t_bg">身份证件号：
                        </td>
                        <td>
                            <asp:TextBox ID="t_SFZH" runat="server" onblur="isIdCard(this)" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox><tt>*</tt>
                        </td>

                    </tr>
                    <tr>
                        <td class="t_r t_bg">政治面貌：
                        </td>
                        <td>
                            <asp:TextBox ID="t_ZZMM" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">业主委员会职务：
                        </td>
                        <td>
                            <asp:TextBox ID="t_YZWYHZW" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="t_r t_bg">联系电话：
                        </td>
                        <td>
                            <asp:TextBox ID="t_LXDH" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">工作单位：
                        </td>
                        <td>
                            <asp:TextBox ID="t_GZDW" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="t_r t_bg">家庭地址：
                        </td>
                        <td>
                            <asp:TextBox ID="t_JTDZ" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                            <span id="myspan"></span>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
    <input type="hidden" id="mytest" runat="server" />
</body>
</html>
