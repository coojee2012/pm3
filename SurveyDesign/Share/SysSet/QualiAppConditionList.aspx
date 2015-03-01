<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualiAppConditionList.aspx.cs"
    Inherits="Admin_main_QualiAppConditionList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>协同办公平台</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
            document.getElementById("dbSystem").value = "";
            document.getElementById("dbQualiLevel").value = "";

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
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                资质审核认定条件维护
            </th>
        </tr>
        <tr>
            <td class="t_r">
                名称：
            </td>
            <td>
                <asp:TextBox ID="text_FName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
            <td class="t_r">
                所属系统：
            </td>
            <td>
                <asp:DropDownList ID="dbSystem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dbSystem_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                所属资质等级：
            </td>
            <td>
                <asp:DropDownList ID="dbQualiLevel" runat="server">
                </asp:DropDownList>
            </td>
            <td class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" class="m_btn_w2" style="margin-left: 10px"
                    onclick="clearQuery();" />
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="btnAdd" class="m_btn_w2" type="button" value="新增" onclick="showApproveWindow('QualiAppConditionAdd.aspx?e=0',557,360);" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="QualiLevel_List" runat="server" HorizontalAlign="Center" Width="100%"
        CssClass="m_dg1" Style="margin-top: 7px;" AutoGenerateColumns="False" OnItemDataBound="QualiLevel_List_ItemDataBound"
        OnItemCommand="QualiLevel_List_ItemCommand">
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
            <asp:BoundColumn DataField="FName" HeaderText="名称"></asp:BoundColumn>
            <asp:BoundColumn DataField="FQualiLevel" HeaderText="资质级别名称">
                <ItemStyle Width="20%" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="fsystemname" HeaderText="所属系统"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="添加子项">
                <ItemStyle Width="100" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnItemAdd" runat="server" CommandName="Add" Text="添加子项"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FId" Visible="false" HeaderText="Id"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="100%" style="margin-top: 5px;">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
