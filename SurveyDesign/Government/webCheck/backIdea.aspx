<%@ Page Language="C#" AutoEventWireup="true" CodeFile="backIdea.aspx.cs" Inherits="Government_webCheck_backIdea" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../script/Govdept.js" language="javascript"></script>

    <script src="../script/default.js" language="javascript"></script>

    <script type="text/javascript">
        dialogHeight = "500px";
        dialogWidth = "600px";
    </script>

    <title>打回意见</title>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                打回意见
            </th>
        </tr>
    </table>
    <table align="center" width="98%" class="marTop table1">
        <tr>
            <td style="padding-top: 10px">
                <asp:Literal ID="lBackIdea" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
