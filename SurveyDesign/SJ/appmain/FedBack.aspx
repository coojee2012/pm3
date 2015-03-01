<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FedBack.aspx.cs" Inherits="SJ_AppMain_FedBack" %>

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
                    图审结果反馈
                </div>
                <div>
                    <table width="100%" id="appTab" runat="server">
                        <tr>
                            <td style="padding-top: 10px">
                                <table width="100%" id="Table1" runat="server">
                                    <tr>
                                        <td class="t_c">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="27" class="txt23" style="padding-left: 50px; margin-top: 6px;">
                                            工程名称：<asp:TextBox ID="t_FPrjName" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
                                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" colspan="2">
                                <div style="width: 98%; margin: 10px auto;">
                                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Width="100%">
                                        <HeaderStyle CssClass="m_dg1_h" />
                                        <ItemStyle CssClass="m_dg1_i" />
                                        <Columns>
                                            <asp:BoundColumn HeaderText="序号">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="工程名称">
                                                <ItemStyle CssClass="t_l" />
                                                <ItemTemplate>
                                                    <%#Eval("FPrjName") %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn HeaderText="类型"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="建设单位" DataField="FBaseName"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="勘察文件审查">
                                                <ItemStyle CssClass="t_l" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="施工图设计文件审查">
                                                <ItemStyle CssClass="t_l" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                                        CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                                        CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                                        NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                                        pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                                        showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-indent: 20px; line-height: 22px; color: Red;">
                                注：“勘察文件审查”和“施工图设计文件审查”为<b>最新</b>办理的业务情况。
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
