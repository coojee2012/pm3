<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductInfo.aspx.cs" Inherits="JNCLEnt_AppJNCL_ProductInfo" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../script/messages_zh.js"></script>

    <script type="text/javascript">
        function SaveValidate() {
            var success = $("#form1").valid();
            if (success)
                return true;
            return false;
        }
        function SearchProject() {
            var result = showWinByReturn("../ApplyXZYJS/ChooseBuildUnit.aspx?", 800, 500);
            if (result != undefined) {
                $("#hfXMBM").val(result);
                return true;
            }
            return false;
        }
        $(function () {
            $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
            $("#ProductType").find("input[type=checkbox]").live("click", function () {
                if ($(this).attr("checked") == false) {
                    $(this).removeAttr("checked");
                } else {
                    $("#ProductType").find("input[type=checkbox]").removeAttr("checked");
                    $(this).attr("checked", "checked");
                }
            });
        });
    </script>
    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                本次申请产品
            </th>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" 
                        OnClientClick="return SaveValidate()" onclick="btnSave_Click" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
           
            <tr>
                <td class="t_r t_bg">
                    标识等级：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlBSDJ" runat="server" CssClass="m_txt required" Width="200px"></asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    申请产品名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtSQCPMC" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    规格型号：
                </td>
                <td>
                    <asp:TextBox ID="txtGGXH" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    生产能力：
                </td>
                <td><asp:TextBox ID="txtSCNL" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    产品类型：
                </td>
                <td colspan="3" id="ProductType">
                    <asp:CheckBoxList ID="cbCPLX" runat="server" RepeatColumns="4"></asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    产品执行标准：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtCPZXBZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    主要材质：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtZYCZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    辅助材质：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtFZCZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>

