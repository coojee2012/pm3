<%@ Control Language="c#" Inherits="Approve.Common.Pager" CodeFile="Pager.ascx.cs" %>
<meta name="vs_snapToGrid" content="False">
<table name="Table1" id="Table1" cellspacing="0" cellpadding="0" border="0" width="100%"
    align="center" runat="server" class="pages">
    <tr>
        <td class="pagescount" valign="bottom" nowrap="true" align="notset">
            <asp:LinkButton ID="lb_First" runat="server" OnClick="lb_First_Click">��ҳ</asp:LinkButton>
            <asp:LinkButton ID="lb_Prev" runat="server" OnClick="lb_Prev_Click">��һҳ</asp:LinkButton>
            <asp:LinkButton ID="lb_Next" runat="server" OnClick="lb_Next_Click">��һҳ</asp:LinkButton>
            <asp:LinkButton ID="lb_Last" runat="server" OnClick="lb_Last_Click">ĩҳ</asp:LinkButton>
            <asp:LinkButton ID="btnOut" runat="server" OnClick="btnOut_Click" Visible="False">����</asp:LinkButton>
            <span style="padding: 3px 4px 0px 4px; margin: 1px; float: left; display: block;">ת����
            </span>
            <asp:TextBox ID="NavPage" runat="server" Width="30px" Style="float: left; margin: 1px;"
                AutoPostBack="True" OnTextChanged="NavPage_TextChanged"></asp:TextBox>
            <span style="padding: 3px 4px 0px 4px; margin: 1px; float: left; display: block;">ҳ
            </span>
            <asp:LinkButton ID="btn_Go" runat="server" OnClick="btn_Go_Click">GO</asp:LinkButton>
        </td>
        <td class="pagescount" valign="bottom" align="right" style="width: 250px; white-space: nowrap;">
            <b>��<asp:Label ID="CurPage" runat="server"></asp:Label>ҳ ��<asp:Label ID="Pages" runat="server"></asp:Label>ҳ&nbsp;ÿҳ<asp:Label
                ID="Counts" runat="server"></asp:Label>�� ��<asp:Label ID="TotleCount" runat="server"></asp:Label>��</b>
        </td>
    </tr>
</table>
