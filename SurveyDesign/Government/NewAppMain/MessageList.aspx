<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageList.aspx.cs" Inherits="Admin_main_MessageList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/Govdept.js" language="javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
            document.getElementById("drop_FCol").value = "";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                系统通知公告发布
            </th>
        </tr>
        <tr id="QueryTable1">
            <td class="t_r">
                通知公告名称：
            </td>
            <td>
                <asp:TextBox ID="text_FName" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_r">
                发布栏目：
            </td>
            <td>
                <asp:DropDownList ID="drop_FCol" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" style="margin-left: 10px;" class="m_btn_w2"
                    onclick="clearQuery();" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <input id="btnAdd" type="button" value="新增" style="margin-left: 10px;" class="m_btn_w2"
                    onclick="showApproveWindow('NewsAdd.aspx?e=0',896,856);" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
                <%--   <td class="td6 txt3 td12" width="53" onclick="showControl(document.all.QueryTable1);showControl(document.all.QueryTable1);">
                    显示搜索
                </td>
                <td class="td6 txt3 td12" width="53" onclick="hidControl(document.all.QueryTable1);hidControl(document.all.QueryTable1);">
                    隐藏搜索
                </td>--%>
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="News_List" runat="server" HorizontalAlign="Center" Width="98%"
        CssClass="m_dg1" Style="margin-top: 7px; word-break: normal; word-wrap: normal;"
        AutoGenerateColumns="False" OnItemDataBound="News_List_ItemDataBound" OnItemCommand="News_List_ItemCommand">
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
            <asp:BoundColumn DataField="FName" HeaderText="通知公告标题">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FCount" HeaderText="点击率">
                <ItemStyle Width="40px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FPubTime" HeaderText="发布日期" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Width="80px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="已经发布的栏目">
                <ItemStyle Width="85px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="是否发布">
                <ItemStyle Width="80px" />
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="IsPub" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="顺序">
                <ItemStyle Width="88" />
                <ItemTemplate>
                    <asp:TextBox ID="Forder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FOrder") %>'
                        Width="80" CssClass="m_txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="查看评论" Visible="false">
                <ItemStyle Width="60px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="发布栏目">
                <ItemStyle Width="60px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="发布企业" DataField="fpubcount">
                <ItemStyle Width="60px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="保存">
                <ItemStyle Width="60" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnItemSave" CssClass="link3" runat="server" CommandName="Save"
                        Text="保存"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="98%" style="margin-top: 5px;">
        <tr>
            <td style="height: 30px">
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
