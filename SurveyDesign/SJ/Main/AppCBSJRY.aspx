﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppCBSJRY.aspx.cs" Inherits="EvaluateEntApp_main_AppCBSJRY" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员安排</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../DateSelect/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
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
                    初步设计人员安排
                    <asp:Button ID="Button1" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnQuery_Click"
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
                                工程名称：
                                <asp:TextBox ID="t_FPrjName" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                                &nbsp; &nbsp;办理状态：
                                <asp:DropDownList ID="t_FState" runat="server">
                                    <asp:ListItem Value="">--全部--</asp:ListItem>
                                    <asp:ListItem Value="0">未安排</asp:ListItem>
                                    <asp:ListItem Value="6">已安排</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; 年度：<asp:DropDownList ID="drop_FYear" runat="server">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                                <input id="btnClear" type="button" value="重置" class="m_btn_w2" onclick="clearPage();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" DataKeyNames="FId"
                        EmptyDataText="没有符合条件的数据">
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
                                <ItemStyle CssClass="t_l" />
                                <ItemTemplate>
                                    <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
                                    <a href='javascript:showAddWindow("../applycbsjwt/ApplyBaseInfo.aspx?FDataID=<%#Eval("FLinkId") %>",800,700);'>
                                        <%#Eval("FPrjName")%>
                                    </a>
                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                    <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="建设单位" DataField="FBaseName"></asp:BoundField>
                            <asp:BoundField HeaderText="提交到该步骤时间" DataField="FCreateTime" DataFormatString="{0:d}">
                                <HeaderStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="办理时间" DataField="FAppDate" DataFormatString="{0:d}">
                                <HeaderStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="状态" DataField="FState">
                                <HeaderStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="操作">
                                <HeaderStyle Width="100" />
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
                    提示：<tt>点击“工程名称”查看合同备案详情</tt>，<tt>安排完成后在未开始办“成果移交”前人员可以变更</tt>
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
