<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LxrSysType.aspx.cs" Inherits="Admin_User_LxrSysType" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
    <title>客服维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" language="javascript">
        function clearQuery() {
            document.getElementById("txtFName").value = "";
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                <asp:Label ID="Label1" runat="server" Text="在线客服设置"></asp:Label>
            </th>
        </tr>
        <tr>
            <td align="right">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right">
                <input id="btnAdd" type="button" value="新增" onclick="showAddWindow('LxrSysTypeAdd.aspx?e=0',400,400);"
                    class="m_btn_w2" /><asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除"
                        OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        Style="margin-top: 4px;" AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="50px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FLinkName" HeaderText="联系人"></asp:BoundColumn>
            <asp:BoundColumn DataField="FType" HeaderText="用户类型"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="系统类型"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="97%" style="margin-top: 5px;">
        <tr>
            <td style="height: 30px">
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    &nbsp;&nbsp;
    </form>
</body>
</html>
