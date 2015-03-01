<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KCWTSLlist.aspx.cs" Inherits="KC_AppMain_KCWTSLlist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table height="95%" width="98%" align="center">
        <tr>
            <td colspan="3" height="10px;">
            </td>
        </tr>
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
            <td class="wxts_m" valign="top">
                <div class="wxts_title">
                    勘察合同确认
                    <asp:Button ID="btnQuery" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                        Style="display: none;" />
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <table width="100%" id="appTab" runat="server">
                        <tr>
                            <td class="t_c">
                            </td>
                        </tr>
                        <tr>
                            <td height="27" class="txt23" style="padding-left: 50px; margin-top: 6px;">
                                工程名称：<asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                                &nbsp; 办理状态：<asp:DropDownList ID="ddlFState" runat="server">
                                    <asp:ListItem Value="">--全部--</asp:ListItem>
                                    <asp:ListItem Value="1">未确认</asp:ListItem>
                                    <asp:ListItem Value="2">已退回</asp:ListItem>
                                    <asp:ListItem Value="6">已确认</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; 年度：<asp:DropDownList ID="drop_FYear" runat="server">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:Button ID="Button1" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                                <input id="btnClear" type="button" value="重置" class="m_btn_w2" onclick="clearPage();" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" DataKeyNames="FId"
                        EmptyDataText="当前没有勘察合同申请" OnRowCommand="DG_List_RowCommand">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <RowStyle CssClass="m_dg1_i" />
                        <EmptyDataRowStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <HeaderStyle Width="40px" />
                                <ItemTemplate>
                                    <asp:Label ID="lbautoid" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="工程名称">
                                <ItemStyle CssClass="t_l" Wrap="false" />
                                <ItemTemplate>
                                    <a href='javascript:showAddWindow("../ApplyKCXMWT/ApplyBaseInfo.aspx?FDataID=<%#Eval("FLinkId") %>",800,700);'>
                                        <%#Eval("FPrjName")%>
                                    </a>
                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                    <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="建设单位" DataField="FBaseName" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="提交合同时间" DataField="FReportDate" DataFormatString="{0:d}">
                                <HeaderStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="确认时间" DataField="FAppDate" DataFormatString="{0:d}">
                                <HeaderStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="结果" DataField="FState">
                                <HeaderStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="操作">
                                <HeaderStyle Width="80" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="打印">
                                <HeaderStyle Width="88" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                        CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                        CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                        NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                        pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                        showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
                    </webdiyer:AspNetPager>
                </div>
                <div style="width: 98%; margin: 4px auto;">
                    提示：<tt>点击“工程名称”查看合同基本信息</tt>
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
