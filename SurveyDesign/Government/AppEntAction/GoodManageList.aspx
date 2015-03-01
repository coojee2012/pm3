<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodManageList.aspx.cs" Inherits="Government_AppEntAction_GoodManageList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')
            if (ret == "1") {
                form1.btnQuery.click();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                良好行为发布管理
            </th>
        </tr>
        <tr>
            <td class="t_r">
                企业名称：
            </td>
            <td width="230">
                <asp:TextBox ID="txtFEntName" runat="server" CssClass="m_txt" Width="175px"></asp:TextBox>
            </td>
            <td class="t_r">
                企业类别：
            </td>
            <td width="150">
                <asp:DropDownList ID="dbFSystemId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td rowspan="2" class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Style="margin-left: 6px;"
                    Text="查询" OnClick="btnQuery_Click" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                颁发机关：
            </td>
            <td>
                <asp:TextBox ID="txtFDeptIdName" runat="server" CssClass="m_txt" Width="175px"></asp:TextBox>
            </td>
            <td class="t_r">
                是否发布：
            </td>
            <td width="150">
                <asp:DropDownList ID="dbFState" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="">--请选择--</asp:ListItem>
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_l">
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="发布" OnClick="btnDel_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="m_btn_w2" Text="撤销" OnClick="btnCancel_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
        Width="98%" OnItemCommand="DG_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn Visible="True">
                <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="40px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="企业名称" DataField="FProjectName">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="证书文件名称" DataField="FWay">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="颁发部门" DataField="FDeptIdname"></asp:BoundColumn>
            <asp:BoundColumn DataField="FHTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="颁发时间">
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="状态"></asp:BoundColumn>
            <asp:ButtonColumn HeaderText="操作" Text="操作" Visible="false"></asp:ButtonColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
        CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
        CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
        NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
        PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
        ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
    </webdiyer:AspNetPager>
    </form>
</body>
</html>
