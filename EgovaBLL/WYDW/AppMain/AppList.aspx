<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppList.aspx.cs" Inherits="WYDW_AppMain_ProjectSB" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <base target="_self" />
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function ShowAppPage(sUrl, width, height, evt) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret == "ok") {
                location.reload();
            }
            else {
                evt.returnValue = false;
                return false;
            }
        }
    </script>
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
                <td class="wxts_m" valign="top">
                    <div class="wxts_title">
                        <asp:Label runat="server" ID="lblManageType"></asp:Label>
                    </div>
                    <div style="width: 98%; margin: 0 auto;">
                        <table width="100%" id="appTab" runat="server">
                            <tr>
                                <td class="t_r"></td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px">
                                    <table width="100%" id="Table1" runat="server">
                                        <tr>
                                            <td class="t_c"></td>
                                        </tr>
                                        <tr>
                                            <td height="27" class="txt23" style="padding-left: 1px; margin-top: 6px;">业务名称：<asp:TextBox ID="t_YWFname" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                                                &nbsp; 办理状态：<asp:DropDownList ID="ddlFState" runat="server">
                                                    <asp:ListItem Value="">--全部--</asp:ListItem>
                                                    <asp:ListItem Value="0">未提交</asp:ListItem>
                                                    <asp:ListItem Value="1">已提交</asp:ListItem>
                                                    <asp:ListItem Value="6">已确认</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; 年度：<asp:DropDownList ID="drop_FYear" runat="server">
                                                </asp:DropDownList>
                                                &nbsp;
                                                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                                                <asp:Button ID="btnAdd" Text="项目在管申请" runat="server" CssClass="m_btn_w8" CommandArgument="1"></asp:Button>
                                                <%--    <asp:Button ID="btnXMBG" Text="项目变更申请" OnClientClick="ShowAppPage('AppAdd.aspx?ManageType=14403',600,140,event);" runat="server" CssClass="m_btn_w8" CommandArgument="1" Visible="False"></asp:Button>
                                                <asp:Button ID="btnXMSQ" Text="项目失去申请" OnClientClick="ShowAppPage('AppLostAdd.aspx?ManageType=14402',600,140,event);" runat="server" CssClass="m_btn_w8" CommandArgument="1" Visible="False"></asp:Button>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" colspan="2">
                                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                        HorizontalAlign="Center" OnItemCommand="DG_List_ItemCommand" OnItemDataBound="DG_List_ItemDataBound"
                                        Style="margin-top: 7px" Width="100%">
                                        <HeaderStyle CssClass="m_dg1_h" />
                                        <ItemStyle CssClass="m_dg1_i" />
                                        <Columns>
                                            <asp:BoundColumn HeaderText="序号">
                                                <ItemStyle Width="40px" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="业务名称">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnItemSee" runat="server" CommandName="See" Text='<%#Eval("FName") %>'>
                                                    </asp:LinkButton>
                                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                                    <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="t_l" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="项目名称">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnMC" runat="server" CommandName="See" Text='<%#Eval("XMMC") %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="t_l" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="FReportDate" DataFormatString="{0:d}" HeaderText="上报时间" HeaderStyle-Width="80px">
                                                <HeaderStyle Width="80px"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="状态"></asp:BoundColumn>

                                            <asp:BoundColumn HeaderText="办理结果">
                                                <ItemStyle CssClass="lh t_l" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="审批详情" HeaderStyle-Width="100px" DataField="FResult">
                                                <HeaderStyle Width="100px"></HeaderStyle>

                                                <ItemStyle CssClass="lh t_l" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="操作">
                                                <HeaderStyle Width="70" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnOp" runat="server" CommandArgument='<%#Eval("FId")%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FManageTypeID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="XMBH" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                                    <div style="line-height: 20px; text-align: left;">
                                        <tt>提示：点击“办理结果”中各步骤的办理结果，可查看办理详情</tt>
                                    </div>
                                </td>
                            </tr>
                        </table>
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
    </form>
</body>
</html>
