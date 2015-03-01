<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowAppIdea.aspx.cs" Inherits="Government_AppQualiInfo_ShowAppIdea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>打回意见</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" class="m_title" width="500">
            <tr>
                <th colspan="6">
                    打回意见
                </th>
            </tr>
            <tr>
                <td width="70">
                    打回时间：
                </td>
                <td colspan="5">
                    <asp:Label ID="t_FAppTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="70">
                    打回意见：
                </td>
                <td colspan="5">
                    <asp:Label ID="t_FIdea" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
