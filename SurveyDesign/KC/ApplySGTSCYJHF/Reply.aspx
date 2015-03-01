<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reply.aspx.cs" Inherits="KC_ApplySGTSCYJHF_Reply" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>审查意见回复</title>
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
            if ($("#t_FContent").val() == "") {
                alert("请填写回复内容");
                $("#t_FContent").focus();
                return false;
            }
            if ($("#t_FTxt1").val() == "") {
                alert("请填写回复人");
                $("#t_FTxt1").focus();
                return false;
            }
            if ($("#t_FTxt2").val() == "") {
                alert("请填写审核人");
                $("#t_FTxt2").focus();
                return false;
            }
            return confirm('确定要提交回复吗？');
        }
 
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                审查意见回复
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" width="140">
                工程名称：
            </td>
            <td>
                <asp:Label ID="GCMC" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审图单位：
            </td>
            <td>
                <asp:Label ID="JSDW" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                技术性审查结果：
            </td>
            <td>
                <asp:Label ID="JG" runat="server"></asp:Label>
                <a id="a_GZS" runat="server" style="margin-left: 30px; text-decoration: underline;"
                    target="_blank"></a>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="4" class="t_r t_bg" style="padding-right: 30px;">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
                <asp:Button ID="btnReport" runat="server" Text="提交" OnClick="btnReport_Click" CssClass="m_btn_w2" />
                <input type="button" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="80">
                回复内容：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Width="99%" TextMode="MultiLine"
                    Height="160"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                日期：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_FDate" runat="server" CssClass="m_txt" Width="100px" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                回复人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt1" runat="server" CssClass="m_txt" Width="100px" MaxLength="10"></asp:TextBox>
            </td>
            <td class="t_r t_bg" width="80">
                审核人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt2" runat="server" CssClass="m_txt" Width="100px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center" id="ta_SGT" runat="server" visible="false">
        <tr>
            <td colspan="4" class="t_l t_bg" style="padding-left: 30px;">
                审图机构意见
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审图报告编号：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:Literal ID="t_FTxt3" runat="server"></asp:Literal> 
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="100">
                意见：
            </td>
            <td colspan="3" class="m_txt_M" style="height: 100px;">
                <asp:Literal ID="t_FTxt" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
