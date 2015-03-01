<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zWXTS.aspx.cs" Inherits="KC_Main_zWXTS" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
            DynamicGrid(".m_dg1_i");
        });
        function QualiIdear(FAPPID, type) {
            showApproveWindow('../../Goverment/Approve/EntAppIdear.aspx?FAPPID=' + FAPPID + '&type=' + type + '&rid=' + Math.random(), 400, 300);
        }
        function control(FID, obj, msg) {
            if (confirm(msg)) {
                document.getElementById('hidd_FID').value = FID;
                obj.click();
            }
        }
        function toMainPage(sUrl) {
            window.top.location.href = sUrl;
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
            <td class="wxts_m">
                <div class="wxts_title">
                    温馨提示
                </div>
                <div>
                    <div style="text-indent: 30px; line-height: 30px; font-size: 14px;">
                        <b>
                            <asp:Literal ID="lit_EntName" runat="server"></asp:Literal>
                        </b>，您好。
                    </div>
                    <div style="text-indent: 60px; line-height: 30px; font-size: 14px;">
                        <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="wxts_msg">
                    最新系统消息： <font color="black" style="font-weight: normal;">（共有 <b style="color: Green;">
                        <asp:Literal ID="lit_MSGCount" runat="server"></asp:Literal></b> 条，其中 <b style="color: red;">
                            <asp:Literal ID="lit_MSGNoRead" runat="server"></asp:Literal></b> 条未读
                        <asp:Button ID="btnAllRead" runat="server" Text="全部标记为已读" CssClass="m_btn_w8" OnClick="btnAllRead_Click"
                            OnClientClick="return confirm('确认要全部标记为已读吗？');" Visible="false" />
                        <asp:Button ID="btnAll" runat="server" Text="查看全部" CssClass="m_btn_w4" OnClick="btnAll_Click" />
                        <asp:Button ID="btnNoRead" runat="server" Text="查看未读" CssClass="m_btn_w4" OnClick="btnNoRead_Click" />）
                    </font>
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <asp:Literal ID="xtxx" runat="server"></asp:Literal>
                </div>
                <div class="wxts_Ts">
                    您历次办理的业务有：<font color="black"><asp:Literal ID="lit_App" runat="server"></asp:Literal></font>
                    （年度：<asp:DropDownList ID="drop_FYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_FYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    ）
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <asp:DataGrid ID="App_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                        HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Width="100%"
                        OnItemCommand="App_List_ItemCommand">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <ItemStyle CssClass="m_dg1_i" />
                        <%-- <AlternatingItemStyle CssClass="m_dg1_i" />--%>
                        <Columns>
                            <asp:BoundColumn HeaderText="序号">
                                <HeaderStyle Font-Underline="False" Width="30px" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="工程名称">
                                <ItemStyle CssClass="t_l" Wrap="false" />
                                <ItemTemplate>
                                    <a href='javascript:showAddWindow("../<%#Eval("fUrl") %>/ApplyBaseInfo.aspx?FDataID=<%#Eval("FLinkId") %>",850,700);'>
                                        <asp:Literal ID="lFName" runat="server" Text='<%#Eval("FPrjName")%>'></asp:Literal>
                                    </a>
                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                    <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="业务名称">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnItemSee" runat="server" CommandName="See" Text='<%#Eval("FName") %>'
                                        CommandArgument='<%#Eval("FID")+","+Eval("FManageTypeId")+","+Eval("FState")%>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="t_l" Wrap="false" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FReportDate" DataFormatString="{0:d}" HeaderText="办理时间">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FState" HeaderText="状态"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FState" HeaderText="办理结果"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="操作" Visible="false">
                                <HeaderStyle Width="60" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnOp" runat="server" CommandArgument='<%#Eval("FID")%>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    <webdiyer:AspNetPager ID="Pager2" runat="server" AlwaysShow="True" CssClass="pages"
                        CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                        CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                        NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager2_PageChanging"
                        PageIndexBoxType="TextBox" PageSize="15" PrevPageText="上一页" ShowCustomInfoSection="Right"
                        ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
                    </webdiyer:AspNetPager>
                    <div style="margin: 4px auto;">
                        <tt>提示：点击“业务名称”办理或查看业务。</tt>
                    </div>
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
    <input id="hidd_FID" type="hidden" runat="server" />
    <asp:Button ID="btnQuery" runat="server" Text="查询" Style="display: none" OnClick="btnQuery_Click" />
    </form>
</body>
</html>
