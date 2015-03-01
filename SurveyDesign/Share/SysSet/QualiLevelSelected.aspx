<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualiLevelSelected.aspx.cs"
    Inherits="Admin_main_QualiLevelSelected" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程资质等级维护</title>
    <base target="_self"></base>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
             DynamicGrid(); //列表光标移动效果
        }); 
       function selectWindowReturnValue(value) 
       {
         window.returnValue=value;
         window.close();
       }          
    </script>

    <meta http-equiv="x-ua-compatible" content="ie=7" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                流程资质等级维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" OnClick="btnSave_Click"
                    Text="保存" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table align="center" cellpadding="0" cellspacing="0" width="98%">
        <tr>
            <td align="center">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="5" Width="98%">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
