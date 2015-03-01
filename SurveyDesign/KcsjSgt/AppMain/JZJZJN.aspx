<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JZJZJN.aspx.cs" Inherits="KcsjSgt_AppMain_JZJZJN" %>

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
        function addKZSF(obj) {
            showAddWindow("../ApplyOther/JZJZJNInfo.aspx?e=1", 850, 700);
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
                    居住建筑节能设计审查备案登记表
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
                                <input id="btnAdd" type="button" runat="server" value="新增" class="m_btn_w2" onclick="addKZSF(this)" />
                                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClick="btnDel_Click"
                                    OnClientClick="return confirm('确认要删除吗？')" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" DataKeyNames="FId"
                        EmptyDataText="当前没有数据">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <RowStyle CssClass="m_dg1_i" />
                        <EmptyDataRowStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="30px" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckItem" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序号">
                                <HeaderStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lbautoid" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="工程名称">
                                <ItemStyle CssClass="t_l" Wrap="false" />
                                <ItemTemplate>
                                    <a href='javascript:showAddWindow("../ApplyOther/JZJZJNInfo.aspx?fId=<%#Eval("FId") %>",850,700);'>
                                        <%#Eval("FPrjName")%>
                                    </a>
                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="建设单位" DataField="JSDW">
                                <ItemStyle CssClass="t_l" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="设计单位" DataField="SJDW">
                                <ItemStyle CssClass="t_l" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="备案编号" DataField="FBackUpNo">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="节能计算建筑面积(㎡)" DataField="FArea">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField Visible="false">
                                <ItemStyle Wrap="false" />
                                <ItemTemplate>
                                    <a href="<%=ReportServer %>SC-KZSFInfo.cpt&FAppId=<%#Eval("FID") %>" target="_blank">
                                        打印</a>
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
                    提示：<tt>点击列表中“工程名称”查看备案登记表信息</tt>
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
