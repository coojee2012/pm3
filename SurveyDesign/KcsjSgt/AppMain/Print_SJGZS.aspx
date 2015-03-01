<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_SJGZS.aspx.cs" Inherits="KcsjSgt_AppMain_Print_SJGZS" %>

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

    <script type="text/javascript">
        function checkLSH(obj) {
            var lsh = $(':text[id*=t_LSH]', $(obj).parent().parent()).val();
            if (lsh == null || lsh == '') {
                alert('请填写编号！');
                return false;
            }
            $('#h_LSH').val(lsh);
            var state = $(':checkbox[id*=ck_State]', $(obj).parent().parent()).attr("checked");
            $('#h_State').val(state ? "1" : "0");
            if (state) {
                return confirm('设置为已打印后，不可再修编号,确认要保存设置吗?');
            }
            return true;
        }
    </script>

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
                    施工图设计文件审查告知书打印
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <table width="100%" id="appTab" runat="server">
                        <tr>
                            <td class="t_c">
                            </td>
                        </tr>
                        <tr>
                            <td height="27" class="txt23" style="padding-left: 50px; margin-top: 6px;">
                                工程名称：<asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" DataKeyNames="FId"
                        EmptyDataText="当前没有数据" OnRowCommand="DG_List_RowCommand">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <RowStyle CssClass="m_dg1_i" />
                        <EmptyDataRowStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <HeaderStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lbautoid" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="工程名称">
                                <ItemStyle CssClass="t_l" Wrap="false" />
                                <ItemTemplate>
                                    <a href='javascript:showAddWindow("../ApplySGTSJWJSCWTSL/ApplyBaseInfo.aspx?FDataID=<%#Eval("FLinkId") %>",800,700);'>
                                        <%#Eval("FPrjName")%>
                                    </a>
                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="建设单位" DataField="FJSEnt">
                                <ItemStyle CssClass="t_l" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="勘察单位" DataField="FKCEnt">
                                <ItemStyle CssClass="t_l" Wrap="false" />
                                <HeaderStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="设计单位" DataField="FSJEnt">
                                <ItemStyle CssClass="t_l" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="审查日期" DataField="FAppDate" DataFormatString="{0:d}">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="打印日期" DataField="FPrintDate" DataFormatString="{0:d}">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="备案结果">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="编号">
                                <ItemStyle CssClass="t_c" Wrap="false" />
                                <ItemTemplate>
                                    <asp:TextBox ID="t_LSH" runat="server" CssClass="m_txt" Text='<%#Eval("FResult") %>'
                                        Width="100px"></asp:TextBox>
                                    <tt>*</tt>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是/否打印">
                                <HeaderStyle CssClass="t_c" Wrap="false" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="ck_State" runat="server" ToolTip="选中状态表示已打印"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSave" runat="server" Text="保存" CommandName="DoSave" CommandArgument='<%#Eval("FID") %>'></asp:LinkButton>
                                    |
                                    <asp:HyperLink ID="print_A" runat="server" Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                    提示：<tt>点击列表中“工程名称”查看合同备案基本信息</tt>
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
    <input id="h_LSH" type="hidden" runat="server" />
    <input id="h_State" type="hidden" runat="server" />
    </form>
</body>
</html>
