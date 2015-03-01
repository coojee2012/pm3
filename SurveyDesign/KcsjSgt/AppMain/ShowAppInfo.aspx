<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowAppInfo.aspx.cs" Inherits="UrbanAndTownPanning_gzmain_ShowAppInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <title>退回意见</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" width="98%" class="m_title">
            <tr>
                <th>
                    退回意见
                </th>
            </tr>
        </table>
        <table align="center" class="m_table" width="98%">
            <tr>
                <td align="center" class="t_l t_bg">
                    退回意见:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="lContent" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
