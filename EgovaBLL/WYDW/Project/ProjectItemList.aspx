<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectItemList.aspx.cs" Inherits="WYDW_Project_ProjectItemList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                项目列表
            </th>
        </tr>
        <tr>               
            <td class="t_r">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="txtProjectItemName" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
            </td>
            <td class="t_r">
                项目名称：
            </td>
            <td>
                <asp:TextBox ID="txtProjectName" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>                                     
            </td>
            <td  rowspan="2" style="text-align: center; padding-right: 10px">
                <asp:Button ID="Button2" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                项目类别：
            </td>
            <td>
                <asp:DropDownList ID="t_PrjItemType" runat="server"  Width="156px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r">
                结构类型：
            </td>
            <td>
                <asp:DropDownList ID="t_ConstrType" runat="server"  Width="156px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>                                   
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                单项工程情况
            </td>
            <td class="t_r">
                
                <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px;
        margin-bottom: 1px;" Width="98%" onitemcommand="dg_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
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
            <asp:BoundColumn DataField="PrjItemName" HeaderText="项目名称">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="PrjItemTypeStr" HeaderText="项目属地">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="Cost" HeaderText="工程造价（万元）">
                <ItemStyle Wrap="false" HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ConstrTypeStr" HeaderText="结构类型">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ProjectName" HeaderText="项目名称" ItemStyle-HorizontalAlign="Left">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="PrjItemType" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="ConstrType" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
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
