<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjList.aspx.cs" Inherits="SJ_Statistics_PrjList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <style type="text/css">
        .lh { line-height: 22px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table height="95%" width="98%" align="center" style="margin-top: 10px; margin-bottom: 10px;">
        <tr>
            <td class="wxts_top_l">
            </td>
            <td class="wxts_top">
            </td>
            <td class="wxts_top_r">
            </td>
        </tr>
        <tr>
            <td class="wxts_l">
            </td>
            <td class=" wxts_m">
                <div class="wxts_title">
                    项目查询
                </div>
                <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                    HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 6px;"
                    Width="98%">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <ItemStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:BoundColumn HeaderText="序号">
                            <ItemStyle Width="30px" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="工程名称">
                            <ItemStyle CssClass="t_l" />
                            <ItemTemplate>
                                <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
                                <a href='javascript:showAddWindow("../../JSDW/appmain/AddPrjRegist.aspx?FID=<%#Eval("FID") %>",900,700);'>
                                    <%#Eval("FPrjName")%></a>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn HeaderText="建设单位">
                            <ItemStyle CssClass="t_l" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn HeaderText="项目初步设计">
                            <ItemStyle CssClass="t_l lh" />
                        </asp:ButtonColumn>
                        <asp:ButtonColumn HeaderText="施工图设计文件编制">
                            <ItemStyle CssClass="t_l lh" />
                        </asp:ButtonColumn>
                        <asp:TemplateColumn HeaderText="查看详情">
                            <ItemTemplate>
                                <a href='javascript:showAddWindow("../Statistics/all.aspx?FID=<%#Eval("FID") %>",900,700);'>
                                    查看详情</a>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                <div style="width: 98%; margin: 4px auto;">
                    <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                        CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                        CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                        NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                        pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                        showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
                    </webdiyer:AspNetPager>
                </div>
                <div style="width: 98%; margin: 4px auto;">
                </div>
            </td>
            <td class="wxts_r">
            </td>
        </tr>
        <tr>
            <td class="wxts_bot_l">
            </td>
            <td class="wxts_bot">
            </td>
            <td class="wxts_bot_r">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
