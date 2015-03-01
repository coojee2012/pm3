<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLockInfoAdd.aspx.cs"
    Inherits="Admin_main_UserLockInfoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/Govdept.js" language="javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>

    <script language="javascript">
        function CheckInfo() {
            if (document.getElementById("t_FNo").value.trim() == "") {
                alert("批次必须填写");
                document.getElementById("t_FNo").focus();
                return false;
            }
            if (document.getElementById("t_FDate").value.trim() == "") {
                alert("日期必须填写");
                document.getElementById("t_FDate").focus();
                return false;
            }
            if (document.getElementById("t_FCount").value.trim == "") {
                alert("数量必须填写");
                document.getElementById("t_FCount").focus();
                return false;
            }
            return true;
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th>
                加密锁批次维护
            </th>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="window.close()" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r">
                批次：
            </td>
            <td>
                <asp:TextBox ID="t_FNo" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                日期：
            </td>
            <td>
                <asp:TextBox ID="t_FDate" runat="server" CssClass="m_txt" onblur="isDate(this);"
                    onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                型号：
            </td>
            <td>
                <asp:TextBox ID="t_FMode" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                数量：
            </td>
            <td>
                <asp:TextBox ID="t_FCount" runat="server" CssClass="m_txt" onblur="isInt(this)"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                单价：
            </td>
            <td>
                <asp:TextBox ID="t_FPrice" runat="server" CssClass="m_txt" onblur="isFloat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                购买人：
            </td>
            <td>
                <asp:TextBox ID="t_FBuyPerson" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                厂家：
            </td>
            <td>
                <asp:TextBox ID="t_FFactory" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                厂家联系人：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                厂家联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkTel" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
