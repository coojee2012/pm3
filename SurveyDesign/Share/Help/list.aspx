<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="Share_Help_list" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>帮助信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
     
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                帮助信息维护
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                标题：
            </td>
            <td>
                <asp:TextBox ID="t_FTitle" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                栏目编码：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkNumber" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input type="reset" value="重置" class="m_btn_w2" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r">
                <input type="button" value="新增" class="m_btn_w2" onclick="showAddWindow('edit.aspx?FLinkNumber='+$('#t_FLinkNumber').val(),600,600);" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" Width="98%" OnItemDataBound="DG_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <HeaderStyle Width="30" />
                <ItemStyle Width="30" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" />
                <HeaderStyle Width="30px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="标题" DataField="FTitle">
                <ItemStyle CssClass="t_l" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="栏目编码" DataField="FLinkNumber">
                <ItemStyle CssClass="t_l" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="帮助信息" Visible="false">
                <ItemStyle CssClass="t_l" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" HeaderText="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="98%">
        <tr>
            <td>
                <webdiyer:AspNetPager ID="Pager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                    PageSize="14" NextPageText="下一页" PrevPageText="上一页" AlwaysShow="True" ShowCustomInfoSection="Right"
                    CustomInfoClass="pagescount" PageIndexBoxType="TextBox" ShowPageIndexBox="Always"
                    SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="<b>共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页</b>"
                    OnPageChanging="Pager1_PageChanging" LayoutType="Table" CssClass="pages" CurrentPageButtonClass="cpb"
                    CustomInfoSectionWidth="150px" NumericButtonCount="6">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
