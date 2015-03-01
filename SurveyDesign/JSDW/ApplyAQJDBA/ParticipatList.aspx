<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParticipatList.aspx.cs" Inherits="JSDW_ApplyAQJDBA_ParticipatList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script>
        $(document).ready(function () {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        function addParticipat() {
            var fid = '<%=ViewState["FAppId"] %>';
            if (fid == null || fid == '') {
                return;
            }
            showAddWindow('Participat.aspx?fAppId=' + fid, 800, 550);
        }
    </script>

</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                参建单位
            </td>
            <td class="t_r">
                <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="addParticipat();" />
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Ent_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" Style="margin-top: 7px" Width="100%" OnItemDataBound="Ent_List_ItemDataBound"
        OnItemCommand="Ent_List_ItemCommand">
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
            <asp:BoundColumn HeaderText="企业名称" DataField="QYMC">
                <ItemStyle Wrap="false"/>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="CJJSStr" HeaderText="参建角色"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="组织机构代码" DataField="ZZJGDM" />
            <asp:BoundColumn HeaderText="资质及等级" DataField="ZZDJ" />
            <asp:BoundColumn HeaderText="资质证书号" DataField="ZZZS" />
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="QYID" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="CJJS" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FAppId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <br />
        <tt>注：</tt>
    </div>
    </form>
</body>
</html>
