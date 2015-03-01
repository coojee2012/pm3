<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEmpInfo.aspx.cs" Inherits="JSDW_QMain_AddEmpInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业注册人员信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function checkInfo() {
            setAutoUser();
            if (isIdCard(document.getElementById("t_FIdCard"))) {
                return AutoCheckInfo();
            }
            return false;
        }
        function setAutoUser() {
            if ($("#t_FUserName").val() == '') {
                $("#t_FUserName").val($("#t_FIdCard").val());
            }
            if ($("#txtFPassWord").val() == '') {
                $("#txtFPassWord").val("888888");
            }
        }
        $(function() {
            $("#t_FUserName").focus(function() {
                setAutoUser();
            });
        });
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业注册人员信息
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                    OnClientClick="return checkInfo();" />
                <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                姓名：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                身份证号码：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FIdCard" runat="server" CssClass="m_txt" Width="200px" MaxLength="30"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <td class="t_r t_bg">
            执业类型：
        </td>
        <td colspan="3">
            <asp:DropDownList ID="t_FPersonTypeId" runat="server" CssClass="m_txt">
            </asp:DropDownList>
        </td>
        <tr>
            <td class="t_r t_bg">
                从事专业：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FRegistSpecialId" runat="server" CssClass="m_txt" MaxLength="15"
                    Width="200px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FCertiNo" runat="server" CssClass="m_txt" MaxLength="30" Width="200px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                印章编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FPrintNo" runat="server" CssClass="m_txt" MaxLength="18" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                有效期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FEndTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt"
                    MaxLength="18" Width="200px"></asp:TextBox>
                    <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                职称：
            </td>
            <td>
                <asp:DropDownList ID="t_FTechId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                职称证号：
            </td>
            <td>
                <asp:TextBox ID="t_FSealNo" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="t_FUserName" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                密码：
            </td>
            <td>
                <asp:TextBox ID="txtFPassWord" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
