<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTBAlist.aspx.cs" Inherits="KC_AppMain_HTBAlist" %>

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
        function checkInfo() {
            if ($("#t_FPrjName").val() == "") {
                alert("请填写工程名称");
                $("#t_FPrjName").focus();
                return false;
            }
            if ($("#t_FTxt1").val() == "") {
                alert("请填写建设单位");
                $("#t_FTxt1").focus();
                return false;
            }
            if ($("#t_FInt3").val() == "") {
                alert("请填合同类型");
                $("#t_FInt3").focus();
                return false;
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
                    合同备案<asp:Literal ID="lit_SW" runat="server"></asp:Literal>
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <table width="100%" id="appTab" runat="server">
                        <tr>
                            <td class="t_c">
                            </td>
                        </tr>
                        <tr>
                            <td height="27" class="txt23" style="padding-left: 10px; margin-top: 6px;">
                                工程名称：<asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
                                &nbsp;办理状态：<asp:DropDownList ID="ddlFState" runat="server">
                                    <asp:ListItem Value="">--全部--</asp:ListItem>
                                    <asp:ListItem Value="0">未上报</asp:ListItem>
                                    <asp:ListItem Value="1">已上报</asp:ListItem>
                                    <asp:ListItem Value="2">退回</asp:ListItem>
                                    <asp:ListItem Value="6">已办结</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;年度：<asp:DropDownList ID="drop_FYear" runat="server">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:Button ID="Button1" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                                <asp:Button ID="btnPup" runat="server" CssClass="m_btn_w12" OnClick="btn_Click">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                                    AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" DataKeyNames="FId"
                                    EmptyDataText="当前没有待备案合同" OnRowCommand="DG_List_RowCommand">
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
                                            <ItemTemplate>
                                                <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
                                                <asp:LinkButton ID="btnItemSee" runat="server" CommandName="See" Text='<%#Eval("FPrjName") %>'
                                                    CommandArgument='<%#Eval("FID")+","+Eval("FManageTypeId")+","+Eval("FState")%>'>
                                                </asp:LinkButton>
                                                <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                                <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="合同类型" />
                                        <asp:BoundField HeaderText="建设单位" DataField="FTxt1">
                                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="合同备案确认时间" DataField="FDate1" DataFormatString="{0:d}">
                                            <HeaderStyle Width="80" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="上报备案时间" DataField="FReportDate" DataFormatString="{0:d}">
                                            <HeaderStyle Width="80" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="状态">
                                            <HeaderStyle Width="120" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="结果" Visible="false">
                                            <HeaderStyle Width="120" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="操作">
                                            <HeaderStyle Width="100" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnOp" runat="server" CommandArgument='<%#Eval("FPrjId")%>'>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnBack" CommandName="Back" runat="server" CommandArgument='<%#Eval("FID")%>'
                                                    Visible="false" Style="margin-left: 6px;">
                                                </asp:LinkButton>
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
                                <div style="width: 98%; margin: 4px auto;">
                                    <tt>提示：点击“工程名称”进入业务办理界面。</tt>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table align="center" width="600" id="applyInfo" runat="server" visible="false" class="m_table">
                        <tr>
                            <td class="t_r t_bg">
                                年度：
                            </td>
                            <td>
                                <asp:TextBox ID="t_FYear" runat="server" CssClass="m_txt" onblur="isInt(this);" Width="100px"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg">
                                申请业务名称：
                            </td>
                            <td>
                                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg">
                                工程名称：<tt>*</tt>
                            </td>
                            <td>
                                <asp:TextBox ID="t_FPrjName" runat="server" CssClass="m_txt" Width="300px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg">
                                建设单位：<tt>*</tt>
                            </td>
                            <td>
                                <asp:TextBox ID="t_FTxt1" runat="server" CssClass="m_txt" Width="300px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg">
                                合同类型：<tt>*</tt>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="t_FInt3" runat="server" CssClass="m_txt">
                                    <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                                    <asp:ListItem Value="291" Text="初步设计"></asp:ListItem>
                                    <asp:ListItem Value="296" Text="施工图设计文件编制"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" height="30">
                                <asp:Button ID="btnOk" runat="server" CssClass="m_btn_w2" OnClick="btnOk_Click" Text="确认"
                                    OnClientClick="return checkInfo(this);" />
                                <input id="btnCancel" class="m_btn_w2" onclick="window.close();" style="margin-left: 10px"
                                    type="button" value="取消" onserverclick="btnCancel_ServerClick" runat="server" />
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
