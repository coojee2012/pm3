<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchAdd.aspx.cs" Inherits="Share_Sys_BatchAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批次管理</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
            DynamicGrid(".m_dg1_i");
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
        function exitt() { //返回
            if ($("#HSaveResult").val() == "1")
                window.returnValue = "1";
            window.close();
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                批次管理
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="exitt();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                批次名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="225px" MaxLength="30"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                类型ID：
            </td>
            <td>
                <asp:TextBox ID="t_FTypeId" runat="server" CssClass="m_txt" MaxLength="8" onblur="isInt(this);"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                添加时间：
            </td>
            <td>
                <asp:TextBox ID="t_FJoinTime" runat="server" CssClass="m_txt" Width="118px" MaxLength="3"
                    onfocus="WdatePicker();"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备注：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FRemark" runat="server" CssClass="m_txt" MaxLength="25" Width="231px"
                    TextMode="MultiLine" Height="61px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
