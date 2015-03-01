<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DicList.aspx.cs" Inherits="Share_SysSet_DicList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>协同办公平台</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
            document.getElementById("text_FNumber").value = "";
            document.getElementById("drop_FSystemId").value = "";
        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                字典维护
            </th>
        </tr>
        <tr>
            <td class="t_r">
                字典名称：
            </td>
            <td>
                <asp:TextBox ID="text_FName" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_r">
                字典编码：
            </td>
            <td>
                <asp:TextBox ID="text_FNumber" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_r">
                系统名称：
            </td>
            <td>
                <asp:DropDownList ID="drop_FSystemId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="重置" class="m_btn_w2" onclick="clearQuery();" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('DicAdd.aspx?e=0',378,256,null);"
                    class="m_btn_w2" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Dic_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound" OnItemCommand="Dic_List_ItemCommand">
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
            <asp:BoundColumn DataField="FName" HeaderText="名称">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FNumber" HeaderText="字典编码"></asp:BoundColumn>
            <asp:BoundColumn DataField="FCNumber" HeaderText="国家编码"></asp:BoundColumn>
            <asp:BoundColumn DataField="FSystemName" HeaderText="所属系统">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="顺序">
                <ItemStyle />
                <ItemTemplate>
                    <asp:TextBox ID="FOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FOrder") %>'
                        Width="80" CssClass="m_txt" onblur="isInt(this);"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="保存">
                <ItemStyle />
                <ItemTemplate>
                    <asp:LinkButton ID="btnItemSave" runat="server" CommandName="Save" Text="保存"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="添加子项">
                <ItemStyle />
                <ItemTemplate>
                    <asp:LinkButton ID="btnItemAdd" runat="server" CommandName="Add" Text="添加子项"></asp:LinkButton>
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
