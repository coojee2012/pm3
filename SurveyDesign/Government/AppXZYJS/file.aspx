<%@ Page Language="C#" AutoEventWireup="true" CodeFile="file.aspx.cs" Inherits="JSDW_ApplyXZYJS_AuditMain_file" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="../../../script/jquery.js" type="text/javascript"></script>
     <script type="text/javascript" src="../../script/default.js"> </script>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <title></title>
    <script type="text/javascript">
        function downLoad(filePath,fileName)
        {
            window.location.href = '../../JSDW/ApplyXZYJS/DownLoad.aspx?filePath=' + filePath + "&fileName=" + fileName;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="t_r"></td>
            <td class="t_r">
                <input id="btnGetBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
        </tr>
    </table>
        <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="t_r" style="text-align:left">
                <asp:Literal ID="ltrName" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
     <table width="98%" align="center" class="m_title">
        <tr id="firstFile">
            <td class="t_r" style="text-align:center;">附件名称</td>
            <td></td>
        </tr>
        <asp:Literal ID="ltrFile" runat="server"></asp:Literal>
    </table>
    </form>
</body>
</html>
