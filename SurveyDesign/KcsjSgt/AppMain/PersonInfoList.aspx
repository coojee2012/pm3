<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonInfoList.aspx.cs" Inherits="SJ_AppMain_PersonInfoList" %>

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
                    人员参与项目统计
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <table width="100%" id="appTab" runat="server">
                        <tr>
                            <td class="t_c" colspan="1">
                            </td>
                        </tr>
                        <tr>
                            <td height="27" class="txt23" style="padding-left: 50px; margin-top: 6px;">
                                年度：<asp:DropDownList ID="ddlFYear" runat="server" CssClass="m_txt">
                                </asp:DropDownList>
                                年<asp:DropDownList ID="ddlFMonth" runat="server" CssClass="m_txt">
                                </asp:DropDownList>
                                月 &nbsp; 时间段：
                                <asp:TextBox ID="txtFBTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                                    Width="80px"></asp:TextBox>
                                --
                                <asp:TextBox ID="txtFETime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                                    Width="80px"></asp:TextBox>
                                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                                <input id="btnClear" type="button" value="重置" class="m_btn_w2" onclick="clearPage();" />
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
                            <asp:TemplateField HeaderText="序号">
                                <HeaderStyle Width="40px" />
                                <ItemTemplate>
                                    <asp:Label ID="lbautoid" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemStyle CssClass="t_c" Wrap="false" />
                                <ItemTemplate>
                                    <a href='javascript:showAddWindow("../main/AddEmpInfo.aspx?FId=<%#Eval("FId") %>&IsView=",600,450);'>
                                        <%#Eval("FName")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="身份证" DataField="FIdCard"></asp:BoundField>
                            <asp:BoundField HeaderText="从事专业" DataField="FRegistSpecialId"></asp:BoundField>
                            <asp:BoundField HeaderText="参与勘察文件审查数量" >
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="参与施工图设计文件审查数量" >
                                <HeaderStyle Width="120px" />
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
                </div>
                <div style="width: 98%; margin: 4px auto;">
                    提示：<tt>点击列表中“参与项目数量”可查看参与的工程项目信息(技术性审查办结的)列表。</tt>
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
