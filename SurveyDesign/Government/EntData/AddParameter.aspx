<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddParameter.aspx.cs" Inherits="JSDW_appmain_Parameter" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单体工程信息</title>
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
            return AutoCheckInfo();
        }
 
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
            技术指标
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
                指标名称：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="t_FType_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                值：
            </td>
            <td>
                <asp:TextBox ID="t_FValue" runat="server" CssClass="m_txt" ></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                单位：
            </td>
            <td>
                <asp:TextBox ID="t_FUnit" runat="server" ReadOnly="true" CssClass="m_txt" ></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
