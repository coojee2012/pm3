<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLockInfoList.aspx.cs"
    Inherits="Admin_main_UserLockInfoList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
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
            document.getElementById("text_FNo").value = "";
            document.getElementById("text_FMode").value = "";
            document.getElementById("text_FFactory").value = "";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                加密锁批次维护
            </th>
        </tr>
        <tr id="QueryTable1">
            <td class="t_r">
                批次：
            </td>
            <td>
                <asp:TextBox ID="text_FNo" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_r">
                厂家：
            </td>
            <td>
                &nbsp;<asp:TextBox ID="text_FFactory" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_r">
                型号：
            </td>
            <td>
                <asp:TextBox ID="text_FMode" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td align="center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                &nbsp;<input id="Button2" type="button" value="清空" class="m_btn_w2" onclick="clearQuery();" />
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="left">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('UserLockInfoAdd.aspx?e=0',378,400);"
                    class="m_btn_w2" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <%--  <td class="td6 txt3 td12" width="53" onclick="showControl(document.all.QueryTable1);showControl(document.all.QueryTable1);">
                显示搜索
            </td>
            <td class="td6 txt3 td12" width="53" onclick="hidControl(document.all.QueryTable1);hidControl(document.all.QueryTable1);">
                隐藏搜索
            </td>--%>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="UserLockInfo_List" runat="server" HorizontalAlign="Center" Width="100%"
        CssClass="m_dg1" Style="margin-top: 7px;" AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound">
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
            <asp:BoundColumn DataField="FNo" HeaderText="批次"></asp:BoundColumn>
            <asp:BoundColumn DataField="FDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FMode" HeaderText="型号"></asp:BoundColumn>
            <asp:BoundColumn DataField="FCount" HeaderText="数量"></asp:BoundColumn>
            <asp:BoundColumn DataField="FFactory" HeaderText="厂家"></asp:BoundColumn>
            <asp:BoundColumn DataField="FBuyPerson" HeaderText="购买人"></asp:BoundColumn>
            <asp:BoundColumn DataField="FPrice" HeaderText="单价"></asp:BoundColumn>
            <asp:BoundColumn DataField="FLinkMan" HeaderText="厂家联系人"></asp:BoundColumn>
            <asp:BoundColumn DataField="FLinkTel" HeaderText="厂家联系电话"></asp:BoundColumn>
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
