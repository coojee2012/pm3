<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualiLevelAdd.aspx.cs" Inherits="Admin_main_QualiLevelAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>四川省勘察设计科技信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });       
    </script>

    <script language="javascript">
        function CheckInfo() {
            if (document.getElementById("t_FName").value.trim() == "") {
                alert("资质级别名称必须填写");
                document.getElementById("t_FName").focus();
                return false;
            }
            if (document.getElementById("t_FNumber").value.trim() == "") {
                alert("编号必须填写");
                document.getElementById("t_FNumber").focus();
                return false;
            }
            if (document.getElementById("t_FSystemId").value.trim() == "") {
                alert("类型编码必须填写");
                document.getElementById("t_FSystemId").focus();
                return false;
            }
            return true;
        }
        function ifSaveOk() {
            var HSaveResult = document.getElementById("HSaveResult");
            if (HSaveResult) {
                window.returnValue = HSaveResult.value;
            }
            window.close();
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                资质级别维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                &nbsp;<asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                &nbsp;<input id="btnBack" class="m_btn_w2" onclick="ifSaveOk();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                资质级别名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                类型编码：
            </td>
            <td>
                <asp:TextBox ID="t_FSystemId" runat="server" CssClass="m_txt" onblur="isInt(this)"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属系统：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemName" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                等级：
            </td>
            <td>
                <asp:TextBox ID="t_FLevel" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备注：
            </td>
            <td>
                <asp:TextBox ID="t_FDescrible" runat="server" CssClass="m_txt" TextMode="MultiLine"
                    Height="70px" Width="350px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
