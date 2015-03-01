<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveIdea.aspx.cs" Inherits="KC_ApplyKCXXBA_SaveIdea" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>见证单位审核意见</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();


            //选择是否同意时切换要填写的内容
            $("#t_FInt1").change(function() {
                tab();
            });
        });


        function checkInfo() {
            function checkInfo() {
                return AutoCheckInfo();
            }
            if (!getLength(document.getElementById("t_FDeptName"), 50, '“审核意见”')) {
                return false;
            }

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
                见证单位审核意见
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
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" style="width: 20%">
                见证单位：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt5" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <%--<tr>
            <td class="t_r t_bg">
                见证人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt6" runat="server" CssClass="m_txt" Width="90"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审核时间：
            </td>
            <td>
                <asp:TextBox ID="t_FDate6" runat="server" CssClass="m_txt" Width="90" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>--%>
        <tr>
            <td class="t_r t_bg">
                审核意见：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FDeptName" runat="server" CssClass="m_txt" Width="320px" MaxLength="50"
                    TextMode="MultiLine" Height="60"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr runat="server" id="tr_tip" visible="false">
            <td colspan="2" class="t_bg" style="color: Red">
                注：该工程项目没有合同备案给相关的见证单位，不用填写该页的内容。
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
