<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lift_CZRY.aspx.cs" Inherits="JSDW_ApplyAQJDBA_Lift_CZRY" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>起重设备操作人员</title>
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
            return AutoCheckInfo();
        }
    </script>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click"
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
                <td colspan="1">
                    <asp:TextBox ID="t_Name" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工种：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Trades" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    操作证号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_CZZH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>      
        </table>
    </div>
    </form>
</body>
</html>
