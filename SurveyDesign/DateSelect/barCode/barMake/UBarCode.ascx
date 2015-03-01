<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UBarCode.ascx.cs" Inherits="barCode_barMake_UBarCode" %>
<table  width="500" id='barcode' height="50"  style="border: none;font-size:14px;font-weight:normal">
	<tr>
		<td align="center"  style="border: none" class="noborder"><IMG alt="" src="../../barcode/barmake/barcode.aspx" id="IMG1" runat="server"></td>
	</tr>
	<tr>
		<td align="center"  style="border: none" class="noborder">
			<asp:Label id="sBarCode" runat="server"></asp:Label></td>
	</tr>
</table>
