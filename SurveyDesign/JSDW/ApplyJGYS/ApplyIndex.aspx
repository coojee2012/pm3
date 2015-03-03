﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyIndex.aspx.cs" Inherits="JSDW_ApplyYDGH_ApplyIndex" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                        竣工验收备案申报
                    </div>
                    <div style="width: 98%; margin: 0 auto;">
                        <table width="100%" id="appTab">
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
                                            <td height="27" class="txt23" style="padding-left: 1px; margin-top: 6px;">工程名称：<asp:TextBox ID="t_YWFname" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                                                &nbsp; 办理状态：<asp:DropDownList ID="ddlFState" runat="server">
                                                    <asp:ListItem Value="">--全部--</asp:ListItem>
                                                    <asp:ListItem Value="0">未提交</asp:ListItem>
                                                    <asp:ListItem Value="1">已上报</asp:ListItem>
                                                    <asp:ListItem Value="6">已办理</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; 年度：<asp:DropDownList ID="drop_FYear" runat="server">
                                                </asp:DropDownList>
                                                &nbsp;
                                                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                                                <input type="button"  class="m_btn_w12" onclick="javascript: window.location.href = 'ApplyFor.aspx'" value="新增竣工验收备案申请" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" colspan="2">
                                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                        HorizontalAlign="Center"
                                        Style="margin-top: 7px" Width="100%" OnItemDataBound="DG_List_ItemDataBound" OnItemCommand="DG_List_ItemCommand">
                                        <HeaderStyle CssClass="m_dg1_h" />
                                        <ItemStyle CssClass="m_dg1_i" />
                                        <Columns>
                                            <asp:BoundColumn HeaderText="序号">
                                                <ItemStyle Width="40px" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="业务名称">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnItemSee" runat="server" CommandName="See" CommandArgument='<%#Eval("FState") %>' Text='<%#Eval("FName") %>'>
                                                    </asp:LinkButton>
                                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                                    <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="t_l" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="GCMC" HeaderText="工程名称"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TypeName" HeaderText="类别"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="状态"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FReportDate" DataFormatString="{0:d}" HeaderText="提交时间"></asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="办理结果">
                                                <ItemStyle CssClass="lh t_l" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="审批详情" HeaderStyle-Width="100px" DataField="FResult">
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
                                            <asp:BoundColumn DataField="ProjectType" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                                             <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
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
