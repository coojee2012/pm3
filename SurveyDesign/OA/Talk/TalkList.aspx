<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TalkList.aspx.cs" Inherits="OA_Talk_TalkList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");

            $(".a_a").click(function() {
                $(".a_a_h").attr("class", "a_a");
                $(this).attr("class", "a_a_h");
            });
        });

        function unLock(FID) {
            var rv = showAddWindow_rv("Lock.aspx?FID=" + FID + "", 400, 250);
            if (rv != null && rv == 1) {
                var FState = '<%=ViewState["FState"] %>';
                window.location = "TalkDisp.aspx?FID=" + FID + "&FState=" + FState;
            }
        }
        
    </script>

    <style type="text/css">
        .a_a { color: #333333; font-weight: bold; margin-right: 10px; margin-left: 2px; }
        .a_a_h, a_a:hover { color: #FF0000; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="t_BB" runat="server" Text="讨论区"></asp:Label>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                板块：
            </td>
            <td>
                <asp:DropDownList ID="t_Fproject" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                提交时间：
            </td>
            <td>
                <asp:TextBox ID="t_appTime1" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="75px"></asp:TextBox>
                至<asp:TextBox ID="t_appTime2" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="75px"></asp:TextBox>
            </td>
            <td align="center" rowspan="3">
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                <br />
                <br />
                <input id="btnClear" class="m_btn_w2" onclick="clearPage();" type="button" value="重置" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                标题：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="202px"></asp:TextBox>
            </td>
            <td class="t_r">
                发起者：
            </td>
            <td>
                <asp:TextBox ID="t_createName" runat="server" CssClass="m_txt" Width="166px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                按回复数排序：
            </td>
            <td>
                <asp:DropDownList CssClass="m_txt" ID="Drop_order" runat="server" Width="132px">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                按提交时间排序：
            </td>
            <td>
                <asp:DropDownList CssClass="m_txt" ID="Drop_order1" runat="server" Width="132px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                <img src="../../image/question1.gif" />
                <asp:LinkButton ID="lb2" runat="server" CssClass="a_a" OnClick="lb_Click" CommandArgument="2">讨论进行中</asp:LinkButton>
                <img src="../../image/question2.gif" />
                <asp:LinkButton ID="lb3" runat="server" CssClass="a_a" OnClick="lb_Click" CommandArgument="3">已中止</asp:LinkButton>
                <img src="../../image/question7.gif" />
                <asp:LinkButton ID="lb1" runat="server" CssClass="a_a" OnClick="lb_Click" CommandArgument="1">草稿箱</asp:LinkButton>
            </td>
            <td class="t_r" style="padding-right: 20px;">
                <img src="../../image/question9.gif" />
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="link2" Font-Bold="True"
                    OnClick="btnQuery_Click">刷 新</asp:LinkButton>
                <span style="color: #2a586f; font-family: Verdana">&nbsp;
                    <img src="../../image/question8.gif" /></span> <a href="javascript:showAddWindow('TalkEdit.aspx?',700,650);">
                        <b>添加新讨论 </b></a>
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:GridView ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        EmptyDataText="没有此类讨论的话题" HorizontalAlign="Center" OnRowDataBound="DG_List_RowDataBound"
        Width="98%" OnRowCommand="DG_List_RowCommand">
        <RowStyle CssClass="m_dg1_i" />
        <EmptyDataRowStyle CssClass="m_dg1_i" />
        <HeaderStyle CssClass="m_dg1_h" />
        <Columns>
            <asp:BoundField HeaderText="序号">
                <ItemStyle Width="30px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="标题">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FProjectID" HeaderText="板块">
                <ItemStyle HorizontalAlign="Left" Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="FSubmitPerson" HeaderText="发起者">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="FSubmitTime" HeaderText="日期">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="状态" Visible="false">
                <ItemStyle Width="8%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="参讨人" Visible="false">
                <ItemStyle Width="6%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="操作">
                <ItemStyle Width="15%" />
                <ItemTemplate>
                    <asp:LinkButton ID="lbuIsOK" runat="server" CausesValidation="false" CommandArgument='<%# Eval("FId")+","+Eval("FTalkState")  %>'
                        CommandName="cnIsOK" Text="终止该问题">
                    </asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lbuDel" runat="server" CausesValidation="false" CommandArgument='<%# Eval("FId") %>'
                        CommandName="cnDel" Text="删除">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FCreateTime" Visible="False" />
            <asp:BoundField DataField="FTalkState" Visible="False" />
            <asp:BoundField DataField="FTalkName" Visible="False" />
            <asp:BoundField DataField="FId" Visible="False" />
        </Columns>
    </asp:GridView>
    <table align="center" width="98%">
        <tr>
            <td>
                <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
                </webdiyer:AspNetPager>
            </td>
        </tr>
        <tr>
            <td style="line-height: 24px; height: 24px;">
                <img src="../../image/question5.gif" />
                <span style="color: #2A586F">提示：若讨论话题已上锁，则需输入口令解锁才能进入；已解锁的在口令未变更前无需再次输入便可进入。 </span>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
