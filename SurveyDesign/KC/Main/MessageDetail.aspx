<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageDetail.aspx.cs" Inherits="EntApprove_gzmain_MessageDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/Govdept.js" language="javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>
<style>
.info   td
{
    border:none;
    }
</style>
    <title>企业文件通知</title>
</head>
<body bgcolor="#c7c7c7">
    <form id="form1" runat="server">
    <table width="98%" align="center" height="100%"  class="m_title">
        <tr>
            <td align="center"  height="50">
                <asp:Literal ID="lTitle" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="center" height="30" class="info" >
                <asp:Literal ID="lDesc" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr height="10px">
            <td colspan="1" height="688" valign="top">
                <asp:Literal ID="lContent" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
