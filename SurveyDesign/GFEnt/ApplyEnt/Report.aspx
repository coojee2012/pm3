<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="GFEnt_ApplyEnt_Report" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function checkInfo() {
            if (AutoCheckInfo()) {
                return confirm('确认要提交此工法信息吗？');
            }
            return false;
        }
    </script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="5">提交工法
                    </th>
                </tr>
            </table>
            <table class="m_table" width="98%" align="center">
                <tr>
                    <td class="t_r t_bg">年度：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="t_FYear" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">业务名称：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">工法名称：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="t_GFMC" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">申报单位：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="t_DW" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                        <tt>*</tt>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">上报部门：
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="t_FUpDeptName" Width="250px" CssClass="m_txt" runat="server" AutoPostBack="True" Enabled="false">
                        </asp:DropDownList>
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
        </div>
    </form>
</body>
</html>
