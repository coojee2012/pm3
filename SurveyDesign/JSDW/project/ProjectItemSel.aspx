<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectItemSel.aspx.cs" Inherits="JSDW_project_ProjectItemSel" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="../../Common/pager.ascx" tagname="pager" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目登记列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        
        function CheckInfo() {
            return AutoCheckInfo();
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                项目登记列表
            </th>
        </tr>
        <tr>
            <td colspan="1" class="t_r">
                工程名称
            </td>
            <td colspan="4" class="t_l">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="250px" 
                    MaxLength="30"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                <input type="button" id="Button1" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px;
        margin-bottom: 1px;" Width="98%" OnItemCommand="dg_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn Visible="false">
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="PrjItemName" HeaderText="工程名称" ItemStyle-HorizontalAlign="Left">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="PrjItemType" HeaderText="工程类别"></asp:BoundColumn>
            <asp:BoundColumn DataField="JSDW" HeaderText="建设单位"></asp:BoundColumn>
            <asp:ButtonColumn HeaderText="选择" CommandName="Sel"></asp:ButtonColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="padding-left: 1%">
        <uc1:pager ID="pager1" runat="server" />
    </div>
    </form>
</body>
</html>
