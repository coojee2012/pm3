<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dlInfo.aspx.cs" Inherits="JNCLEnt_ApplyInfo_dlInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
    <base target="_self"></base>
    <script type="text/javascript">
        function checkInfo() {
            if ($("#t_MC").val().trim() == "") {
                alert("请填写材料和产品代理商名称");
                return false;
            }
            if ($("#t_QYDZ").val().trim() == "") {
                alert("请填写企业地址");
                return false;
            }
            if ($("#t_YYZZ").val().trim() == "") {
                alert("请填写营业执照号");
                return false;
            }
            if ($("#t_ZCZJ").val().trim() == "") {
                alert("请填写注册资金");
                return false;
            }
            if ($("#t_FR").val().trim() == "") {
                alert("请填写法人");
                return false;
            }
            if ($("#t_SJ").val().trim() == "") {
                alert("请填写手机");
                return false;
            }
            if ($("#t_DH").val().trim() == "") {
                alert("请填写电话");
                return false;
            }
            if ($("#t_SMSQ").val().trim() == "") {
                alert("请选择是否书面授权");
                return false;
            }            
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">代理商信息
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r" style="padding-right: 10px;">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />                    
                    <asp:Button ID="btnQuery" Style="display: none;" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" />                  
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">材料和产品代理商名称：</td>
                <td colspan="3">
                    <asp:TextBox ID="t_MC" runat="server" CssClass="m_txt" Width="350px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">企业地址：</td>
                <td colspan="3">
                    <asp:TextBox ID="t_QYDZ" runat="server" CssClass="m_txt" Width="350px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">营业执照号：</td>
                <td>
                    <asp:TextBox ID="t_YYZZ" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">注册资金：</td>
                <td>
                    <asp:TextBox ID="t_ZCZJ" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">法人代表：</td>
                <td>
                    <asp:TextBox ID="t_FR" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">手机：</td>
                <td>
                    <asp:TextBox ID="t_SJ" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">电话：</td>
                <td>
                    <asp:TextBox ID="t_DH" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">是否书面授权：</td>
                <td>
                    <asp:DropDownList ID="t_SMSQ" CssClass="m_txt" Width="120px" runat="server">
                        <asp:ListItem>是</asp:ListItem>
                        <asp:ListItem>否</asp:ListItem>
                    </asp:DropDownList><tt>*</tt>
                </td>
            </tr>
        </table>
        <input id="t_fappid" runat="server" type="hidden" />
        <input id="t_fid" runat="server" type="hidden" />
    </form>
</body>
</html>
