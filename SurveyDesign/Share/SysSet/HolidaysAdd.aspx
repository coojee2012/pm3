<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidaysAdd.aspx.cs" Inherits="Admin_main_HolidaysAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>节假日维护</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript">
            function CheckInfo() {
            if (document.getElementById("t_FDate").value.trim() == "") {
                alert("日期必须填写");
                document.getElementById("t_FDate").focus();
                return false;
            }
            return true;
        }
   function ifSaveOk()
{
    if(document.getElementById("HSaveResult")!=null&&document.getElementById("HSaveResult").value=="1")
    {
        window.returnValue="1";
        window.close();
    }
    else
    {
        window.close();
    }
}
    </script>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
        <tr>
            <th>
                节假日维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="ifSaveOk();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
        <table class="m_table" width="98%" cellspacing="0" align="center">
        <tr>
            <td class="t_r t_bg">
                日期：
            </td>
            <td>
                <asp:TextBox ID="t_FDate" onfocus="WdatePicker()" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        </table>
            <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
