<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="KC_ApplyKCXXBA_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
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
        function checkInfo() {
            if (AutoCheckInfo()) {
                return confirm('确认要上报吗？');
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
                数据上报
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="2" class="t_bg m_txt_M" style="line-height: 26px;">
                见证人和见证单位郑重声明：
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp; 上述内容经外业见证真实、可靠，如有虚假，我们愿意接受有关行政主管部门依法给予的处罚。
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                见证员：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt17" runat="server" CssClass="m_txt" Width="90px" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审核人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt18" runat="server" CssClass="m_txt" Width="90px" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                单位法人代表或负责人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt19" runat="server" CssClass="m_txt" Width="90px" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" >
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg"  >
                业务名称：
            </td>
            <td>
                <asp:TextBox ID="a_FName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                上报部门：
            </td>
            <td>
                <asp:DropDownList ID="p_FManageDeptId" runat="server" CssClass="m_txt" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="2" class="t_c">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
                <asp:Button ID="btnReport" runat="server" Text="上报" OnClick="btnReport_Click" CssClass="m_btn_w2"
                    Style="margin-left: 10px;" />
                <font color='red'>上报前请确定信息无误，上报后将不能修改。</font>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
