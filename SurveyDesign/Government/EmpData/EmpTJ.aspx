<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpTJ.aspx.cs" Inherits="Government_EmpData_EmpTJ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            if (AutoCheckInfo()) {
                if (isIdCard(document.getElementById("t_FIdCard"))) {
                    return true;
                }
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

    <base target="_self"></base>
</head>
<body scroll="no">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业注册人员信息
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_c t_bg">
                <b>执业类型</b>
            </td>
            <td class="t_c t_bg">
                <b>人员数量</b>
            </td>
            <td class="t_c t_bg">
                <b>执业类型</b>
            </td>
            <td class="t_c t_bg">
                <b>人员数量</b>
            </td>
        </tr>
        <asp:Literal ID="litEmp" runat="server"></asp:Literal>
    </table>
    </form>
</body>
</html>
