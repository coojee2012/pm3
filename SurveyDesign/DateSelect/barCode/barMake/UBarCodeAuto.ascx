<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UBarCodeAuto.ascx.cs"
    Inherits="barCode_barMake_UBarCodeAuto" %>
<table width="500" id='barcode' height="50" style="border: none; font-size: 14px;
    font-weight: normal">
    <tr>
        <td align="center" style="border: none" class="noborder">
            <img alt="" src="../../barcode/barmake/BarCodeCom.aspx" id="IMG1" runat="server">
        </td>
    </tr>
    <tr>
        <td align="center" style="border: none" class="noborder">
            <asp:Label ID="sBarCode" runat="server"></asp:Label>
        </td>
    </tr>
</table>
