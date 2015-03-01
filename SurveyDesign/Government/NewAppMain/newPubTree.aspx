<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newPubTree.aspx.cs" Inherits="Admin_main_newPubTree" %>

 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择栏目</title>
    <base target="_self">
    </base>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>

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
<body onclick="NoScrool()" onunload="SetVal();" onload="GetVal();">
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_bar" id="Table3">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <asp:Button ID="btnOk" runat="server" CssClass="m_btn_w2" Text="确定" OnClick="btnOk_Click" /><input
                    id="Button1" type="button" value="取消" onclick="window.close();" class="m_btn_w2" />
                <input id="selectvalue" type="hidden" runat="server" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table bgcolor="aliceblue" border="0" cellpadding="0" cellspacing="0" height="100%"
        width="240">
        <tr>
            <td align="left" valign="top" height="100%">
               
                <asp:TreeView ID="Tree" runat="server" ShowCheckBoxes="Leaf">
                </asp:TreeView>
            </td>
        </tr>
    </table>
    <a id="aNoScroll" style="display: none" onclick="NoScrool()"></a></div>
    </form>
</body>
</html>

<script>
    document.getElementById("aNoScroll").click();
</script>

