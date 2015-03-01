<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSList.aspx.cs" Inherits="Share_Main_TSList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript">
        function showWindow() {
            showAddWindow("EditTSForm.aspx?", 800, 600);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%--<table width="98%" align="center" class="m_title">
            <tr>
                <td width="100">企业名称：</td>
                <td width="150"><asp:TextBox ID="txtQYName" runat="server"></asp:TextBox></td>
                <td width="auto"><asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="m_btn_w2" OnClick="btnQuery_Click"  /></td>
            </tr>
        </table>--%>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" onitemdatabound="DG_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="40px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="投诉人">
                <ItemTemplate>
                    <a href="#" onclick="showWindow()"><%#Eval("TSR") %></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="TSSJ" HeaderText="投诉时间"></asp:BoundColumn>
            <asp:BoundColumn DataField="SZDW" HeaderText="所在单位">
                <ItemStyle CssClass="t_l" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="BTSQY" HeaderText="被投诉企业"></asp:BoundColumn>
            <asp:BoundColumn DataField="LXDH" HeaderText="联系电话"></asp:BoundColumn>
            <asp:BoundColumn DataField="Email" HeaderText="邮箱"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="98%">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>


