<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysInfo.aspx.cs" Inherits="Admin_main_SysInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function CheckInfo() {
            if (document.getElementById("t_FName").value.trim() == "") {
                alert("用户必须填写");
                document.getElementById("t_FName").focus();
                return false;
            }
            if (document.getElementById("t_FPassWord").value.trim() == "") {
                alert("用户密码必须填写");
                document.getElementById("t_FPassWord").focus();
                return false;
            }

            return true;
        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

    <base target="_self"></base>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="600px" align="center" style="margin-top: 10px;">
        <tr>
            <td class="wxts_top_l">
            </td>
            <td class="wxts_top">
            </td>
            <td class="wxts_top_r">
            </td>
        </tr>
        <tr>
            <td class="wxts_l">
            </td>
            <td class="wxts_m">
                <div class="wxts_title" style="width: 80%; border-bottom: solid 1px #CBCBCB;">
                    用户信息
                </div>
                <div>
                    <table align="center" class="cp_t">
                        <tr>
                            <td class="t_r">
                                用户名：
                            </td>
                            <td>
                                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="184px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r">
                                密码：
                            </td>
                            <td>
                                <asp:TextBox ID="t_FPassWord" runat="server" CssClass="m_txt" MaxLength="20" Style="ime-mode: disabled;"
                                    Width="184px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td class="wxts_r">
            </td>
        </tr>
        <tr>
            <td class="wxts_bot_l">
            </td>
            <td class="wxts_bot">
            </td>
            <td class="wxts_bot_r">
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
