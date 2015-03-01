<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZzzlQsdADD.aspx.cs" Inherits="Share_Sys_ZzzlQsdADD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
        function getPageValue(sValue) {
            window.returnValue = sValue;
            self.close();
        }

    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title" id="QueryTable1">
        <tr>
            <th colspan="4">
                纸质资料签收单登记
            </th>
        </tr>
        <tr>
            <td align="right">
                工程名称：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FPrjName"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td align="right">
                工程地址：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FAllAddress" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                建设单位：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FJSDW" Width="70%"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td align="right">
                工程类别：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FType"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td align="right">
                勘察单位：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FKCDW" Width="70%"></asp:TextBox>
            </td>
            <td align="right">
                设计单位：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FSJDW" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                建筑面积：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FArea" onblur="isFloat(this)"></asp:TextBox>
            </td>
            <td align="right">
                合同价格：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FAllMoney" onblur="isFloat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                审查环节：
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="t_FSCHJ" Width="70%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                相关资料情况：
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="t_FXGZLWK" Width="70%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                备注：
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="t_FRemark" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                项目负责人：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FPrjPerson"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td align="right">
                联系电话：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FTel" MaxLength="20" onblur="isInt(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                签收人：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FSignPerson"></asp:TextBox><tt>*</tt>
            </td>
            <td align="right">
                签收日期：
            </td>
            <td>
                <asp:TextBox runat="server" ID="t_FReportDate" onblur="isDate(this);" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click" />
                <asp:Button runat="server" ID="btnReturn" CssClass="m_btn_w2" Text="返回" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
