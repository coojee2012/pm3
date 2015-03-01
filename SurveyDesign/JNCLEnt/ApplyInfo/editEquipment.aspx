<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editEquipment.aspx.cs" Inherits="JNCLEnt_mangeInfo_editWquipment" %>

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
            if ($("#t_SBMC").val() == "") {
                alert("请填写设备名称");
                return false;
            }
            if ($("#t_XH").val() == "") {
                alert("请填写型号/产地/出厂日期");
                return false;
            }
            if ($("#t_GL").val() == "") {
                alert("请填写功率");
                return false;
            }
            if ($("#t_NU").val() == "") {
                alert("请填写数量");
                return false;
            }
            if ($("#t_DW").val() == "") {
                alert("请填写单位");
                return false;
            }
            if ($("#t_YZ").val() == "") {
                alert("请填写原值");
                return false;
            }
            if ($("#t_JZ").val() == "") {
                alert("请填写净值");
                return false;
            }
            if ($("#t_SFZY").val() == "") {
                alert("请选择是否自有设备");
                return false;
            }
            if ($("#t_ZT").val() == "") {
                alert("请选择状态");
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
                <th colspan="5">主要生产设备编辑
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
                <td class="t_r t_bg">设备名称：
                </td>
                <td>
                    <asp:TextBox ID="t_SBMC" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">型号/产地/出厂日期：
                </td>
                <td>
                    <asp:TextBox ID="t_XH" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">功率：
                </td>
                <td>
                    <asp:TextBox ID="t_GL" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                    千瓦
                </td>
                <td class="t_r t_bg">数量：
                </td>
                <td>
                    <asp:TextBox ID="t_NU" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                    单位
                    <asp:TextBox ID="t_DW" Width="60px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">是否自由设备：
                </td>
                <td>
                    <asp:DropDownList ID="t_SFZY" Width="150px" runat="server">
                        <asp:ListItem>是</asp:ListItem>
                        <asp:ListItem>否</asp:ListItem>
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">原值：
                </td>
                <td>
                    <asp:TextBox ID="t_YZ" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">净值：
                </td>
                <td>
                    <asp:TextBox ID="t_JZ" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">状态：
                </td>
                <td>
                    <asp:DropDownList ID="t_ZT" Width="150px" runat="server">
                        <asp:ListItem>正常运行</asp:ListItem>
                        <asp:ListItem>退出生产</asp:ListItem>
                    </asp:DropDownList><tt>*</tt>
                </td>
            </tr>
        </table>
        <input id="t_FID" runat="server" type="hidden" />
        <input id="t_fappid" runat="server" type="hidden" />
    </form>
</body>
</html>
