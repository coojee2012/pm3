<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppSJBZWT.aspx.cs" Inherits="EvaluateEntApp_main_AppCBSJWT" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>施工图设计文件编制合同</title>
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
        function selPrj(obj) {
            var pid = showWinByReturn('../appmain/PrjRegistSel.aspx?ftype=<%=fMType %>', 700, 500);
            if (pid != null && pid != '') {
                $("#HPid").val(pid);
                __doPostBack(obj.id, '');
            }
        }
        function checkInfo(obj) {
            if ($("#txtFPrjName").val() == '') {
                alert('请先选择项目！');
                return false;
            }
            return true;
        }
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
                    施工图设计文件编制合同
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
                                        <td style="padding-top: 10px">
                                            <table width="100%" id="Table1" runat="server">
                                                <tr>
                                                    <td class="t_c">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="27" class="txt23" style="padding-left: 1px; margin-top: 6px;">
                                                        工程名称：<asp:TextBox ID="ttFPrjName" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
                                                        &nbsp;办理状态：<asp:DropDownList ID="ddlFState" runat="server">
                                                            <asp:ListItem Value="">--全部--</asp:ListItem>
                                                            <asp:ListItem Value="0">未提交</asp:ListItem>
                                                            <asp:ListItem Value="1">已提交</asp:ListItem>
                                                            <asp:ListItem Value="6">已确认</asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;年度：<asp:DropDownList ID="drop_FYear" runat="server">
                                                        </asp:DropDownList>
                                                        &nbsp;<asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                                                        <asp:Button ID="btnPup" runat="server" CssClass="m_btn_w14" CommandArgument="1" OnClick="btn_Click">
                                                        </asp:Button>
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
                                                    <asp:TemplateColumn HeaderText="工程名称">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnItemSee" runat="server" CommandName="See" Text='<%#Eval("FPrjName") %>'>
                                                            </asp:LinkButton>
                                                            <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                                            <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="t_l" />
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn HeaderText="状态"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="FReportDate" DataFormatString="{0:d}" HeaderText="提交时间">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="办理结果">
                                                        <ItemStyle CssClass="lh t_l" />
                                                    </asp:BoundColumn>
                                                    <asp:ButtonColumn CommandName="Delete" Text="删除" HeaderText="操作"></asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
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
                                            <div style="line-height: 20px; text-align: left;">
                                                <tt>提示：点击“办理结果”中各步骤的办理结果，可查看办理详情</tt>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center" width="600" id="applyInfo" runat="server" visible="false" class="m_table">
                                    <tr>
                                        <td class="t_r">
                                            年度：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="t_FYear" runat="server" CssClass="m_txt" onblur="isInt(this);" Width="100px"
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="t_r">
                                            申请业务名称：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="t_r">
                                            工程名称：<tt>*</tt>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox><asp:Button
                                                ID="btnSel" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selPrj(this);"
                                                UseSubmitBehavior="false" OnClick="btnSel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" height="30">
                                            <asp:Button ID="btnOk" runat="server" CssClass="m_btn_w2" OnClick="btnOk_Click" Text="确认"
                                                OnClientClick="return checkInfo(this)" />
                                            <input id="btnCancel" class="m_btn_w2" onclick="window.close();" style="margin-left: 10px"
                                                type="button" value="取消" onserverclick="btnCancel_ServerClick" runat="server" />
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
    <input id="t_FPriItemId" type="hidden" runat="server" />
    <input id="t_OldFAppId" type="hidden" runat="server" />
    <input id="t_FCount" type="hidden" runat="server" />
    <input id="HPid" type="hidden" runat="server" />
    </form>
</body>
</html>
