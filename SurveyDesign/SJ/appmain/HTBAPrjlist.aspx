<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTBAPrjlist.aspx.cs" Inherits="SJ_AppMain_HTBAPrjlist" %>

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
            if ($("#t_FDate1").val() == "") {
                alert("请填合同备案确认时间");
                $("#t_FDate1").focus();
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
                    联合体查询[根据合同备案]<asp:Literal ID="lit_SW" runat="server"></asp:Literal>
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <table width="100%" id="appTab" runat="server">
                        <tr>
                            <td class="t_c">
                            </td>
                        </tr>
                        <tr>
                            <td height="27" class="txt23" style="padding-left: 50px; margin-top: 6px;">
                                工程名称：<asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
                                &nbsp;年度：<asp:DropDownList ID="drop_FYear" runat="server">
                                </asp:DropDownList>
                                &nbsp;<asp:Button ID="Button1" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                                    AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" DataKeyNames="FId"
                                    EmptyDataText="没有该数据" OnRowCommand="DG_List_RowCommand">
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
                                        <asp:BoundField HeaderText="建设单位" DataField="FTxt1">
                                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主勘承包人" DataField="FTxt7">
                                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="上报备案时间" DataField="FReportDate" DataFormatString="{0:d}">
                                            <HeaderStyle Width="80" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="状态">
                                            <HeaderStyle Width="120" />
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
                                <div style="width: 98%; margin: 4px auto;">
                                    提示：<tt>点击“工程名称”进入详细界面。</tt>
                                </div>
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
