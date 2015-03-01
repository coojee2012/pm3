<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zWXTS.aspx.cs" Inherits="WYDW_main_zWXTS" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table height="95%" width="98%" align="center">
            <tr>
                <td colspan="3" height="10px;"></td>
            </tr>
            <tr>
                <td class="wxts_top_l"></td>
                <td class="wxts_top"></td>
                <td class="wxts_top_r"></td>
            </tr>
            <tr>
                <td class="wxts_l"></td>
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
                        <%--<div style="text-indent: 60px; line-height: 30px; font-size: 14px;">
                            <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
                        </div>--%>
                    </div>
                    <%--<div class="wxts_msg">
                        最新系统消息： <font color="black" style="font-weight: normal;">（共有 <b style="color: Green;">
                        <asp:Literal ID="lit_MSGCount" runat="server"></asp:Literal></b> 条，其中 <b style="color: red;">
                            <asp:Literal ID="lit_MSGNoRead" runat="server"></asp:Literal></b> 条未读
                        <asp:Button ID="btnAllRead" runat="server" Text="全部标记为已读" CssClass="m_btn_w8" OnClick="btnAllRead_Click"
                            OnClientClick="return confirm('确认要全部标记为已读吗？')" Visible="false" />
                        <asp:Button ID="btnAll" runat="server" Text="查看全部" CssClass="m_btn_w4" OnClick="btnAll_Click" />
                        <asp:Button ID="btnNoRead" runat="server" Text="查看未读" CssClass="m_btn_w4" OnClick="btnNoRead_Click" />）
                    </font>
                    </div>--%>
                    <div style="width: 98%; margin: 0 auto;">
                        <asp:Literal ID="xtxx" runat="server"></asp:Literal>
                        <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
                    </div>
                    <div class="wxts_Ts">
                        您正在办理的业务有：<font color="black"><asp:Literal ID="lit_App" runat="server"></asp:Literal></font>
                        <%--（年度：<asp:DropDownList ID="drop_FYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_FYear_SelectedIndexChanged">
                        </asp:DropDownList>
                        ）--%>
                    </div>
                    <div style="width: 98%; margin: 0 auto;">
                        <asp:DataGrid ID="App_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" OnItemCommand="App_List_ItemCommand"
                            Style="margin-top: 7px" Width="100%">
                            <HeaderStyle CssClass="m_dg1_h" />
                            <ItemStyle CssClass="m_dg1_i" />
                            <Columns>
                                <asp:BoundColumn HeaderText="序号">
                                    <ItemStyle Width="40px" />
                                </asp:BoundColumn>
                                <%--<asp:TemplateColumn HeaderText="业务名称" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnYW" runat="server" CommandName="See" Text='<%#Eval("FName") %>'>
                                        </asp:LinkButton>                                      
                                    </ItemTemplate>
                                    <ItemStyle CssClass="t_l" />
                                </asp:TemplateColumn>--%>
                                <asp:TemplateColumn HeaderText="项目名称" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnItemSee" runat="server" CommandName="See" Text='<%#Eval("XMMC") %>'>
                                        </asp:LinkButton>
                                        <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                        <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="t_l" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn HeaderText="业务年度" HeaderStyle-Width="100px" DataField="FYear">
                                    <ItemStyle CssClass="lh t_c" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="类别" DataField="LeiB" HeaderStyle-Width="90px"></asp:BoundColumn>
                                <%--<asp:BoundColumn HeaderText="专业分类" DataField="FTypeName" HeaderStyle-Width="90px"></asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="FReportDate" DataFormatString="{0:d}" HeaderText="上报时间" HeaderStyle-Width="80px"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="状态" HeaderStyle-Width="80px">
                                    <ItemStyle CssClass="lh t_c" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="办理结果" HeaderStyle-Width="80px">
                                    <ItemStyle CssClass="lh t_c" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="审批详情" Visible="False" HeaderStyle-Width="100px" DataField="FResult">
                                    <ItemStyle CssClass="lh t_l" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="操作">
                                    <HeaderStyle Width="70" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnOp" runat="server" CommandArgument='<%#Eval("FId")%>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="XMBH" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FManageTypeId" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                        <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                        <div style="margin: 4px auto;">
                            <tt>提示：点击“业务名称”办理或查看业务。</tt>
                        </div>
                    </div>
                </td>
                <td class="wxts_r"></td>
            </tr>
            <tr>
                <td class="wxts_bot_l"></td>
                <td class="wxts_bot"></td>
                <td class="wxts_bot_r"></td>
            </tr>
        </table>
        <input id="hidd_FID" type="hidden" runat="server" />
        <asp:Button ID="btnQuery" runat="server" Text="查询" Style="display: none" OnClick="btnQuery_Click" />
    </form>
</body>
</html>
