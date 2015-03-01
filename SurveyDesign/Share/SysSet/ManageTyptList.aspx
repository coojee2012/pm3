<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageTyptList.aspx.cs" Inherits="Admin_main_ManageTyptList" %>

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
            document.getElementById("text_FOperDeptName").value = "";
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
            <th colspan="5">
                业务类型维护
            </th>
        </tr>
        <tr>
            <td class="t_r">
                业务名称：
            </td>
            <td>
                <asp:TextBox ID="text_FName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
            <td class="t_r">
                编码：
            </td>
            <td>
                <asp:TextBox ID="text_FNumber" runat="server" CssClass="m_txt" Width="60"></asp:TextBox>
                类型编码：
                <asp:TextBox ID="text_FMTypeId" runat="server" CssClass="m_txt" Width="60"></asp:TextBox>
            </td>
            <td class="t_c" rowspan="2">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Style="margin-left: 10px"
                    Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" style="margin-left: 10px;" class="m_btn_w2"
                    onclick="clearQuery();" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                系统名称：
            </td>
            <td>
                <asp:DropDownList ID="drop_FSystemId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                承办处室：
            </td>
            <td>
                <asp:TextBox ID="text_FOperDeptName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('ManageTypeAdd.aspx?e=0',970,800);"
                    class="m_btn_w2" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" OnClick="btnSave_Click"
                    Text="保存" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="ManageType_List" runat="server" HorizontalAlign="Center" Width="100%"
        CssClass="m_dg1" Style="margin-top: 4px;" AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound"
        OnItemCommand="ManageType_List_ItemCommand">
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
            <asp:BoundColumn DataField="FName" HeaderText="业务名称">
                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FNumber" HeaderText="编号">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FMTypeId" HeaderText="类型编号">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSystemName" HeaderText="所属业务系统">
                <ItemStyle Width="120px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FOperDeptName" HeaderText="承办处室">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FDesc" HeaderText="说明">
                <ItemStyle Width="150px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="显示顺序">
                <ItemStyle Width="90" />
                <ItemTemplate>
                    <asp:TextBox ID="FOrder" runat="server" Text='<%# Eval("FOrder") %>' Width="80"
                        CssClass="m_txt" onblur="isInt(this);"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="保存">
                <ItemStyle Width="60" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnItemSave" runat="server" CommandName="Save" Text="保存"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="附件">
                <ItemStyle Width="40px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="100%">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html> 