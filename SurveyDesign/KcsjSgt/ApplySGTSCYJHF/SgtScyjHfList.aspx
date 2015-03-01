<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SgtScyjHfList.aspx.cs" Inherits="KC_ApplySGTSCYJHF_SgtScyjHfList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审查意见回复确认</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        }); 
    </script>

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
                    <asp:Literal ID="lit_Title" runat="server" Text="勘察审查意见回复确认"></asp:Literal>
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td align="left" valign="top">
                                <table width="100%" id="appTab" runat="server">
                                    <tr>
                                        <td class="t_r">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 20px; height: 35px; line-height: 35px;">
                                            工程名称：
                                            <asp:TextBox ID="ttFPrjName" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
                                            &nbsp;回复状态：
                                            <asp:DropDownList ID="ddlFState" runat="server">
                                                <asp:ListItem Value="">--全部--</asp:ListItem>
                                                <asp:ListItem Value="0">未回复</asp:ListItem>
                                                <asp:ListItem Value="1">已回复</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;<asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top" colspan="2">
                                            <div style="width: 98%; margin: auto;">
                                                <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                                    HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
                                                    Width="100%">
                                                    <HeaderStyle CssClass="m_dg1_h" />
                                                    <ItemStyle CssClass="m_dg1_i" />
                                                    <Columns>
                                                        <asp:BoundColumn HeaderText="序号">
                                                            <HeaderStyle Width="40px" />
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="工程名称">
                                                            <ItemStyle CssClass="t_l" />
                                                            <ItemTemplate>
                                                                <%#Eval("FPrjName")%>
                                                                <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                                                <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn HeaderText="勘察单位" DataField="KCSJBaseName">
                                                            <ItemStyle CssClass="t_l" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn HeaderText="建设单位" DataField="FBaseName">
                                                            <ItemStyle CssClass="t_l" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn HeaderText="技术性审查结果">
                                                            <ItemStyle CssClass="t_c lh" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn HeaderText="勘察单位<br/>回复情况">
                                                            <ItemStyle CssClass="t_c lh" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn HeaderText="确认状态">
                                                            <HeaderStyle Width="140" />
                                                            <ItemStyle CssClass="t_c lh" />
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="打印">
                                                            <HeaderStyle Width="80" />
                                                            <ItemTemplate>
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
                                                <div style="line-height: 22px; text-align: left;">
                                                    <tt>提示：以上为技术性审查完的工程，<asp:Literal ID="lit_TS" runat="server" Text="勘察"></asp:Literal>单位对此进行意见回复，您可以对其填写确认意见并打印。</tt>
                                                </div>
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
