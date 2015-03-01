<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editProduct.aspx.cs" Inherits="JNCLEnt_mangeInfo_editProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <base target="_self"></base>
    <script type="text/javascript">
        function checkInfo() {
            if ($("#t_MC").val() == "") {
                alert("请填写材料和产品名称");
                return false;
            }
            if ($("#t_BZMC").val() == "") {
                alert("请填写产品标准名称");
                return false;
            }
            if ($("#t_BZH").val() == "") {
                alert("请填写产品标准号");
                return false;
            }
            if ($("#t_DJBH").val() == "") {
                alert("请填写备案登记号");
                return false;
            }
            if ($("#t_DJSJ").val() == "") {
                alert("请填写备案登记时间");
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
                <th colspan="5">已备案材料和产品
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r" style="padding-right: 10px;">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">材料和产品名称：
                </td>
                <td>
                    <asp:TextBox ID="t_MC" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">产品标准名称：
                </td>
                <td>
                    <asp:TextBox ID="t_BZMC" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">产品标准号：
                </td>
                <td>
                    <asp:TextBox ID="t_BZH" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">备案登记编号：
                </td>
                <td>
                    <asp:TextBox ID="t_DJBH" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>

                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">备案登记时间：
                </td>
                <td>
                    <asp:TextBox ID="t_DJSJ" Width="150px" onfocus="WdatePicker()" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
        </table>
        <input id="t_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
