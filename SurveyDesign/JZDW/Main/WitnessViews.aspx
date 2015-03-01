<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WitnessViews.aspx.cs" Inherits="JZDW_main_WitnessViews" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员安排</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        }); 
    </script>

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
                    见证人员意见
                </div>
                <table width="100%" id="Table1" runat="server">
                    <tr>
                        <td class="t_c">
                        </td>
                    </tr>
                    <tr>
                        <td height="27" class="txt23" style="padding-left: 50px; margin-top: 6px;">
                            工程名称：<asp:TextBox ID="ttFPrjName" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                            &nbsp; 办理状态：<asp:DropDownList ID="ddlFState" runat="server">
                                <asp:ListItem Value="">--全部--</asp:ListItem>
                                <asp:ListItem Value="0">未办理</asp:ListItem>
                                <asp:ListItem Value="6">已办理</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp; 年度：<asp:DropDownList ID="drop_FYear" runat="server">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                            <input id="btnClear" type="button" value="重置" class="m_btn_w2" onclick="clearPage();" />
                        </td>
                    </tr>
                </table>
                <div style="width: 98%; margin: 0 auto;">
                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
                        Width="100%">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <ItemStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:BoundColumn HeaderText="序号">
                                <ItemStyle Width="40px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="工程名称">
                                <ItemStyle CssClass="t_l" />
                                <ItemTemplate>
                                    <a href='javascript:showAddWindow("../ApplyKCXMWT/ApplyBaseInfo.aspx?FDataID=<%#Eval("FLinkId") %>",800,700);'>
                                        <%#Eval("FPrjName")%>
                                    </a>
                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                    <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FBaseName" HeaderText="建设单位">
                                <ItemStyle CssClass="t_l" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FCreateTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="提交到本步骤时间">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="本步骤完成时间">
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="状态"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="操作">
                                <HeaderStyle Width="100" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FPrjId" Visible="False"></asp:BoundColumn>
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
                </div>
                <div style="width: 98%; margin: 4px auto;">
                    <tt>提示：见证提交见证报告之前还可以重新见证。</tt>
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
    <input id="HPid" type="hidden" runat="server" />
    </form>
</body>
</html>
