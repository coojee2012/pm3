<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="JSDW_appmain_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>提交</title>
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
            if (AutoCheckInfo()) {
                return confirm('确定提交吗？');
            }
            return false;
        } 
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                提交合同单位
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                年度：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FYear" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                业务名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                勘察单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="k_FName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr style="display:none" >
            <td class="t_r t_bg">
                见证单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="j_FName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="t_c">
                <asp:Button ID="btnSave" runat="server" Text="提交" OnClick="btnSave_Click" CssClass="m_btn_w2"
                    OnClientClick="return checkInfo();" />
                <tt>* 提交前请确定信息无误，提交后将不能修改。</tt>
            </td>
        </tr>
    </table>
    <input id="k_FBaseInfoId" type="hidden" runat="server" />
    <input id="j_FBaseInfoId" type="hidden" runat="server" />
    </form>
</body>
</html>
