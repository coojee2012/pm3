<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addEmp.aspx.cs" Inherits="GFEnt_ApplyEnt_addEmp" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">主要完成人维护
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">姓名：
                </td>
                <td>
                    <asp:TextBox ID="t_XM" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">电话：
                </td>
                <td>
                    <asp:TextBox ID="t_SJ" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">工作单位：
                </td>
                <td>
                    <asp:TextBox ID="t_GZDW" runat="server" Width="150px" CssClass="m_txt"></asp:TextBox>

                </td>
                <td class="t_r t_bg">职务：
                </td>
                <td>
                    <asp:TextBox ID="t_ZW" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>

                </td>
            </tr>
        </table>
        <input id="t_RYID" runat="server" type="hidden" />
        <input id="t_YWBM" runat="server" type="hidden" />
        <input id="t_FBaseInfoId" runat="server" type="hidden" />
        <input id="t_FSystemId" runat="server" type="hidden" />
    </form>
</body>
</html>
