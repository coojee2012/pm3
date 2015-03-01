<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonInfoList.aspx.cs" Inherits="KC_AppMain_PersonInfoList" %>

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
                    参与项目统计
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <table width="100%" id="appTab" runat="server">
                        <tr>
                            <td class="t_c" colspan="1">
                            </td>
                        </tr>
                        <tr>
                            <td height="27" class="txt23" style="padding-left: 50px; margin-top: 6px;">
                                年度：<asp:DropDownList ID="ddlFYear" runat="server" CssClass="m_txt" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFYear_SelectedIndexChanged">
                                </asp:DropDownList>
                                年
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" EmptyDataText="当前没有数据"
                        ShowFooter="true">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <FooterStyle CssClass="m_dg1_i" />
                        <RowStyle CssClass="m_dg1_i" />
                        <EmptyDataRowStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <HeaderStyle Width="40px" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="月份">
                                <ItemTemplate>
                                    <%#Eval("FMonth")+"月" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="参与项目数量">
                                <ItemTemplate>
                                    <%#Eval("FCount") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="见证合格">
                                <ItemTemplate>
                                    <%#Eval("FCount1") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="见证不合格">
                                <ItemTemplate>
                                    <%#Eval("FCount2") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="width: 98%; margin: 4px auto;">
                    提示：<tt>点击列表中“参与项目数量”可查看参与的工程项目信息(项目见证报告办结的)列表。</tt>
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
