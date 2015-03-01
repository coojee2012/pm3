<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppInfoList2.aspx.cs" Inherits="KC_AppMain_AppInfoList2" %>

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
                    项目统计
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
                    <asp:Repeater runat="server" ID="DG_List" OnItemDataBound="DG_List_ItemDataBound">
                        <HeaderTemplate>
                            <table class="m_dg1" width="98%">
                                <tr class="m_dg1_h">
                                    <td rowspan="1" class="t_c t_bg">
                                        序号
                                    </td>
                                    <td rowspan="1" class="t_c t_bg">
                                        月份
                                    </td>
                                    <td colspan="1" class="t_c">
                                        受理项目数
                                    </td>
                                    <td colspan="1" class="t_c">
                                        合同金额(万元)
                                    </td>
                                    <td colspan="1" class="t_c">
                                        成果移交项目数
                                    </td>
                                    <td colspan="1" class="t_c">
                                        合同金额(万元)
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="m_dg1_i">
                                <td class="t_c">
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("FMonth")+"月" %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F1")  %>
                                </td>
                                <td class="t_r">
                                    <%#Eval("F1_1")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F4")  %>
                                </td>
                                <td class="t_r">
                                    <%#Eval("F4_1")  %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="m_dg1_i">
                                <td class="t_r t_bg" colspan="2">
                                    合计：
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_0" runat="server"></asp:Literal>
                                </td>
                                <td class="t_r">
                                    <asp:Literal ID="l_1" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_2" runat="server"></asp:Literal>
                                </td>
                                <td class="t_r">
                                    <asp:Literal ID="l_3" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div style="width: 98%; margin: 4px auto;">
                    提示：<tt>点击列表中“工程数量”可查看工程项目信息(已被见证单位同意受理的)列表。</tt>
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
