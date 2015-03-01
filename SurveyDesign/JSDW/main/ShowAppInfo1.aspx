<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowAppInfo1.aspx.cs" Inherits="Government_AppQualiInfo_ShowAppInfo" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>各级审核意见</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table align="center" width="98%" class="m_title">
        <tr>
            <th colspan="8">
                各级审核意见
            </th>
        </tr>
        <tr>
            <td class="t_r">
                企业名称：
            </td>
            <td>
                <asp:Literal ID="liter_FBaseName" runat="server"></asp:Literal>
            </td>
            <td class="t_r">
                上报日期：
            </td>
            <td>
                <asp:Literal ID="liter_FReportDate" runat="server"></asp:Literal>
            </td>
            <td class="t_r">
                业务类型：
            </td>
            <td>
                <asp:Literal ID="liter_FManageType" runat="server"></asp:Literal>
            </td>
            <td class="t_r">
                上报年度：
            </td>
            <td>
                <asp:Literal ID="liter_FYear" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="AppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" Width="98%" OnItemDataBound="AppInfo_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="业务类型" DataField="fmanagetypename">
                <ItemStyle CssClass="padLeft" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核岗位" DataField="FRoleDesc">
                <ItemStyle CssClass="padLeft" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核人" DataField="FAppPerson">
                <ItemStyle CssClass="padLeft" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核人单位" DataField="FCompany">
                <ItemStyle CssClass="padLeft" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核人职务" DataField="FFunction">
                <ItemStyle CssClass="padLeft" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核日期" DataField="FAppTime" DataFormatString="{0:yyyy-MM-dd}">
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核结果" DataField="FResult"></asp:BoundColumn>
            <asp:BoundColumn DataField="flistid" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="ftypeid" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="flevelid" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
       <asp:DataGrid ID="DG_FileList" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Width="98%" OnItemDataBound="DG_FileList_ItemDataBound"
             >
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="50px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="文件名称" ItemStyle-HorizontalAlign="Left">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="附件类型" Visible="false">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:ButtonColumn></asp:ButtonColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    <input id="hidden_Sql" type="hidden" runat="server" />
    </form>
</body>
</html>
