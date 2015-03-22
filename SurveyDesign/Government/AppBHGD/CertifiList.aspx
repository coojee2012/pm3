﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertifiList.aspx.cs" Inherits="Government_AppBHGD_FZList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发证审核</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">发证审核</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">工程名称
                </td>
                <td>
                    <input runat="server" cssclass="m_txt" width="130px" />
                </td>
                <td class="t_r">工程所属地
                </td>
                <td>
                    <input runat="server" />
                </td>
                <td align="center" rowspan="3" colspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" />
                </td>
            </tr>
            <tr>
                <td class="t_r">申报单位
                </td>
                <td>
                    <input runat="server" cssclass="m_txt" width="130px" />
                </td>
                <td class="t_r">年度
                </td>
                <td>
                    <input runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r">批次
                </td>
                <td></td>
                <td class="t_r">办理状态
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddl_state">
                        <asp:ListItem runat="server" Value="-1">--全部--</asp:ListItem>
                        <asp:ListItem runat="server" Value="0">未审核</asp:ListItem>
                        <asp:ListItem runat="server" Value="1">已审核</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:Button ID="btnAccept" runat="server" CssClass="m_btn_w2" Text="审核" />
                    <asp:Button runat="server" CssClass="m_btn_w4" Text="分配批次" />
                    <asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w4"
                        Text="导出Excel" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:DataGrid ID="gv_list" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                    HorizontalAlign="Center" Width="98%">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <ItemStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemStyle Width="20px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn HeaderText="序号">
                            <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="工程名称" DataField="ProjectName">
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="工程所属地" DataField="GCSSD">
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="申报单位" DataField="SBDW">
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="申报日期" DataField="SBRQ" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="不良行为记录" DataField="BLJL">
                            <ItemStyle Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="上报时间" DataField="FReportTime">
                            <ItemStyle Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="办理状态" DataField="BLZT">

                            <ItemStyle Font-Underline="False" Wrap="False"></ItemStyle>
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="FId" DataField="FId" Visible="False">
                            <ItemStyle Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:DataGrid>
                <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
                </webdiyer:AspNetPager>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
