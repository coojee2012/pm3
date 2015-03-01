<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newPubTree.aspx.cs" Inherits="Admin_main_newPubTree" %>

<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>选择栏目</title>
    <base target="_self">
    </base>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript">
        function SetVal() {
            window.parent.returnValue = document.forms[0].selectvalue.value;
            close();
        }
        function GetVal() {
            if (document.forms[0].selectvalue.value == "")
                document.forms[0].selectvalue.value = window.dialogArguments;

        }
        function NoScrool() {
            Tree.click();

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Button ID="btnOk" runat="server" CssClass="m_btn_w2" Text="确定" OnClick="btnOk_Click" />
        <input id="Button1" type="button" value="取消" onclick="window.close();" class="m_btn_w2" />
        <input id="selectvalue" type="hidden" runat="server" />
        <table border="0" cellpadding="0" cellspacing="0" height="100%" width="240">
            <tr>
                <td align="left" valign="top" height="100%">
                    <iewc:TreeView ID="Tree" runat="server" SelectExpands="True"></iewc:TreeView>
                </td>
            </tr>
        </table>
        <a id="aNoScroll" style="display: none" onclick="NoScrool()"></a>
    </div>
    </form>
</body>
</html>

<script>
    document.getElementById("aNoScroll").click();
</script>

