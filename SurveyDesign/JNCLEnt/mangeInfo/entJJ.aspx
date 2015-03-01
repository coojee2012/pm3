<%@ Page Language="C#" AutoEventWireup="true" CodeFile="entJJ.aspx.cs" Inherits="JNCLEnt_mangeInfo_entJJ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
    <base target="_self"></base>
    <script type="text/javascript">
        function checkInfo() {
            if ($("#t_jianjie").val() == "") {
                alert("请填写企业简介");
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
                <th colspan="5">企业简介
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r" style="padding-right: 10px;">
                    <input type="hidden" id="hidd_FLinkId" runat="server" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">企业简介
                </td>
                <td style="height: 400px; width: 84%">
                    <asp:TextBox ID="t_jianjie" runat="server" CssClass="m_txt" Width="100%" Height="400px" TextMode="MultiLine"></asp:TextBox>
                    <input type="hidden" id="p_FId" runat="server" />
                </td>
            </tr>
        </table>
        <input id="t_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
