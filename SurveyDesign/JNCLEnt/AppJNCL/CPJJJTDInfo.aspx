<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CPJJJTDInfo.aspx.cs" Inherits="JNCLEnt_AppJNCL_CPJJJTDInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
    <base target="_self" />
    <script type="text/javascript">
        function validate() {
            if ($.trim($("#txtJJ").val()).length == 0) {
                alert("内容不能为空");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">产品简介及特点
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r" style="padding-right: 10px;">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick="return validate();" class="m_btn_w2" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td style="height: 400px; width: 84%">
                    <asp:TextBox ID="txtJJ" runat="server" CssClass="m_txt" Width="80%" Height="400px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

