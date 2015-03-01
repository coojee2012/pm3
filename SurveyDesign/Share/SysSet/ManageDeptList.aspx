<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageDeptList.aspx.cs" Inherits="Share_SysSet_ManageDeptList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>协同办公平台</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
            document.getElementById("text_FNumber").value = "";
            document.getElementById("drop_Flevel").value = "";
            document.getElementById("drop_FClassNumber").value = "";
            document.getElementById("text_FCNumber").value = "";

        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                政府部门维护
            </th>
        </tr>
        <tr>
            <td class="t_r">
                部门名称：
            </td>
            <td>
                <asp:TextBox ID="text_FName" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_r">
                部门编码：
            </td>
            <td>
                <asp:TextBox ID="text_FNumber" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_r">
                部门级别：
            </td>
            <td>
                <asp:DropDownList ID="drop_Flevel" runat="server" Width="100px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                部门类别：
            </td>
            <td>
                <asp:DropDownList ID="drop_FClassNumber" runat="server" Width="100px">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                国家编码：
            </td>
            <td>
                <asp:TextBox ID="text_FCNumber" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_c" colspan="2">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Style="margin-left: 10px"
                    Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" style="margin-left: 10px;" class="m_btn_w2"
                    onclick="clearQuery();" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="新增" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
                <asp:Button ID="btnReturn" runat="server" CssClass="m_btn_w2" Text="返回" OnClick="btnReturn_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="ManageDept_List" runat="server" HorizontalAlign="Center" Width="98%"
        CssClass="m_dg1" Style="margin-top: 4px;" AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound"
        OnItemCommand="Dic_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="30" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="50" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="部门名称"></asp:BoundColumn>
            <asp:BoundColumn DataField="FNumber" HeaderText="部门编码"></asp:BoundColumn>
            <asp:BoundColumn DataField="FLevel" HeaderText="部门级别"></asp:BoundColumn>
            <asp:BoundColumn DataField="FClassNumberName" HeaderText="部门类别"></asp:BoundColumn>
            <asp:BoundColumn DataField="FFullName" HeaderText="部门全称"></asp:BoundColumn>
            <asp:BoundColumn DataField="FCNumber" HeaderText="国家编码"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="添加子项">
                <ItemTemplate>
                    <asp:LinkButton ID="btnItemAdd" CssClass="link3" runat="server" CommandName="Add"
                        Text="添加子项"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
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
