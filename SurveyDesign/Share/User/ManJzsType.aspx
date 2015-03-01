<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManJzsType.aspx.cs" Inherits="Share_user_ManJzsType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>指定建造师专业</title>
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

    <script language="javascript" src="../script/Lock.js"></script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th>
                <asp:Label ID="lblTitle" runat="server" Text="指定可以审核的建造师专业"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
                <asp:Button ID="btnOk" runat="server" Text="确定" CssClass="m_btn_w2" OnClick="btnOk_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td align="left">
                选择专业：
                <asp:CheckBoxList ID="ckbType" runat="server" RepeatLayout="Flow">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script type="text/javascript">
    var cbkList = document.getElementsByTagName("input");
    var lblList = document.getElementsByTagName("label");
    for (var i = 0; i < cbkList.length; i++) {
        if (cbkList[i].type == "checkbox") {
            if (cbkList[i].checked) {
                cbkList[i].parentNode.style.background = '#eeaffc';
            }
        }
    }
    function changeCheck(obj) {
        obj.parentNode.style.background = obj.checked ? '#eeaffc' : "";
    }
</script>

