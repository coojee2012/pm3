<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileList.aspx.cs" Inherits="JSDW_ApplyYDGH_FileList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
     <script type="text/javascript" src="../../script/default.js"> </script>
    <script type="text/javascript">
        function UpadLoadFile(typeId, $obj) {
            var typeName = $($obj).parent("td").prev("td").prev().text();
            var url = "fileUpadload.aspx?typeId=" + typeId + "&typeName=" + typeName + "&FIsApprove=" + $("#hfFIsApprove").val() + "&YD_Id=" + $("#hfYD_Id").val();
            var returnValue = showAddWindow(url, 800, 400);
            location.reload();
        }
    </script>

</head>
<body>
    <form id="Form1" runat="server">
    <asp:HiddenField ID="hfFIsApprove" runat="server" />
    <asp:HiddenField ID="hfYD_Id" runat="server" />
    <table width="98%" align="center" class="m_table">
        <asp:Literal ID="ltrText" runat="server"></asp:Literal>

    </table>
        </form>
</body>
</html>


