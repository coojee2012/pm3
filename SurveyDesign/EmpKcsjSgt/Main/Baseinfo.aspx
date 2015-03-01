<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Baseinfo.aspx.cs" Inherits="JSDW_QMain_Baseinfo" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid2" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function CheckInfo() {
            var t_FJuridcialCode = document.getElementById("t_FJuridcialCode");
            if (t_FJuridcialCode) {
                var patrn = /^[A-Za-z0-9]{1}[0-9]{7}-[A-Za-z0-9]{1}$/;
                if (!patrn.exec(t_FJuridcialCode.value)) {
                    alert("组织结构代码格式不正确");
                    t_FJuridcialCode.focus();
                    return false
                }
            }
            return AutoCheckInfo();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业基本信息维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                <tt>保存后，请不要随意更改!</tt>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                企业名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="303px" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr style="display: none">
            <td class="t_r t_bg">
                主管部门：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FUpDeptId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                组织机构代码：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FJuridcialCode" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                详细地址：
            </td>
            <td colspan="3">
                <uc2:govdeptid2 ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="t_FAddress" runat="server" CssClass="m_txt" Width="224px" MaxLength="30"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                邮政编码：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FPostcode" runat="server" CssClass="m_txt" onblur="isInt(this);"
                    MaxLength="6"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                EMail：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FEMail" runat="server" CssClass="m_txt" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                联系人电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td align="center" class="t_r t_bg" height="21" nowrap="nowrap">
                法人代表
            </td>
            <td class="txt31">
                <asp:TextBox ID="n_FName" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td align="center" class="t_r t_bg" height="21" nowrap="nowrap">
                联系电话
            </td>
            <td class="txt30">
                <asp:TextBox ID="n_FTel" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
