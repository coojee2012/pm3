<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BadList.aspx.cs" Inherits="JSDW_appmain_BadList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

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
                    市场不良行为举报
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td align="left" valign="top">
                                <table width="100%" id="appTab" runat="server">
                                    <tr>
                                        <td style="padding-top: 10px">
                                            <input id="btnAdd" class="m_btn_w2" type="button" value="举报" onclick="showAddWindow('AddBad.aspx?e=1',600,400);" />
                                            <asp:Button ID="btnQuery" runat="server" OnClick="btn_Click" Style="display: none;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top">
                                            <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                                HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
                                                Width="100%" OnItemCommand="DG_List_ItemCommand">
                                                <HeaderStyle CssClass="m_dg1_h" />
                                                <ItemStyle CssClass="m_dg1_i" />
                                                <Columns>
                                                    <asp:BoundColumn HeaderText="序号">
                                                        <ItemStyle Width="40px" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="举报对象" DataField="FSubject">
                                                        <ItemStyle CssClass="t_l" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="举报内容" DataField="FContent">
                                                        <ItemStyle CssClass="t_l" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="FReportTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="举报时间">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="状态"></asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="办理结果" Visible="false">
                                                        <ItemStyle CssClass="lh t_l" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="操作">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbReport" runat="server" CausesValidation="false" OnClientClick="return confirm('确定要提交吗？')"
                                                                CommandArgument='<%# Eval("FID") %>' CommandName="Report" Text="提交"></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lbDel" CausesValidation="false" OnClientClick="return confirm('确定要删除吗？')"
                                                                CommandArgument='<%# Eval("FID") %>' CommandName="Delete" Text="删除"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
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
                                            <div style="line-height: 20px; text-align: left;">
                                                <tt></tt>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
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
