<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResUserAdd.aspx.cs" Inherits="Admin_User_UserAdd"
    EnableEventValidation="false" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公共资源交换平台用户管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            //文本框样式
            txtCss();
        });
        function readLock() {
            document.getElementById("t_FLockNumber").value = getLockId();
        }
        function CheckInfo() {
            return AutoCheckInfo();
        }
        function exitt() {
            if ($("#HSaveResult").val() == "1")
                window.returnValue = "1";
            window.close();
        }
    </script>

    <base target="_self" />

    <script language='vbScript'>Function  ToHex(str)  ToHex= Hex(str)  End function</script>

</head>
<body style="margin-left: 5px; margin-right: 18px;" class="nox">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                公共资源交换平台用户维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="exitt();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" MaxLength="20" Style="ime-mode: disabled"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                密码：
            </td>
            <td>
                <asp:TextBox ID="txt_FPassWord" runat="server" CssClass="m_txt" MaxLength="20" Style="ime-mode: disabled"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                菜单角色：
            </td>
            <td>
                <asp:DropDownList ID="t_FMenuRoleId" runat="server">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                加密锁标签编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockLabelNumber" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                加密锁硬件编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
                <input id="btnRead" class="m_btn_w2" type="button" value="读锁" onclick="readLock('t_FLockNumber')" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属管理部门：
            </td>
            <td>
                <uc1:Govdept ID="Govdept1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                有效开始日期：
            </td>
            <td>
                <asp:TextBox ID="t_FBeginTime" runat="server" CssClass="m_txt" onfocus="new WdatePicker()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                有效结束日期：
            </td>
            <td>
                <asp:TextBox ID="t_FEndTime" runat="server" CssClass="m_txt" onfocus="new WdatePicker()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                管理人：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                职务：
            </td>
            <td>
                <asp:TextBox ID="t_FFunction" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所在单位：
            </td>
            <td>
                <asp:TextBox ID="t_FCompany" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="t_FManageDeptId" runat="server" type="hidden" />
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
