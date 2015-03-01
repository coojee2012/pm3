<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppSJGMList.aspx.cs" Inherits="KC_AppMain_AppSJGMList" %>

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
                    审查项目业务统计--按工程规模统计
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
                                    <td rowspan="2" class="t_c t_bg">
                                        月份
                                    </td>
                                    <td colspan="3" class="t_c">
                                        合同备案项目
                                    </td>
                                    <td colspan="3" class="t_c">
                                        受理项目
                                    </td>
                                    <td colspan="3" class="t_c">
                                        受理项目合同金额(万元)
                                    </td>
                                    <td colspan="3" class="t_c">
                                        程序性审查
                                    </td>
                                    <td colspan="3" class="t_c">
                                        技术性审查
                                    </td>
                                    <td colspan="3" class="t_c">
                                        备案
                                    </td>
                                    <td colspan="3" class="t_c">
                                        打印合格证
                                    </td>
                                    <td colspan="3" class="t_c">
                                        打印告知书
                                    </td>
                                </tr>
                                <tr class="m_dg1_h" style="white-space: nowrap">
                                    <td colspan="1" class="t_c">
                                        大型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        中型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        小型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        大型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        中型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        小型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        大型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        中型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        小型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        大型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        中型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        小型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        大型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        中型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        小型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        大型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        中型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        小型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        大型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        中型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        小型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        大型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        中型
                                    </td>
                                    <td colspan="1" class="t_c">
                                        小型
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="m_dg1_i">
                                <td class="t_c" style="white-space: nowrap">
                                    <%#Eval("FMonth")+"月" %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F1")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J1")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("X1")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F1_1")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J1_1")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("X1_1")  %>
                                </td>
                                <td class="t_r">
                                    <%#Eval("F1_2")  %>
                                </td>
                                <td class="t_r">
                                    <%#Eval("J1_2")  %>
                                </td>
                                <td class="t_r">
                                    <%#Eval("X1_2")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F4")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J4")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("X4")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F5")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J5")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("X5")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F6")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J6")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("X6")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F7")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J7")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("X7")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F8")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J8")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("X8")  %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="m_dg1_i">
                                <td class="t_r t_bg" style="white-space: nowrap">
                                    合计
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_0" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_1" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_2" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_3" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_4" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_5" runat="server"></asp:Literal>
                                </td>
                                <td class="t_r">
                                    <asp:Literal ID="l_6" runat="server"></asp:Literal>
                                </td>
                                <td class="t_r">
                                    <asp:Literal ID="l_7" runat="server"></asp:Literal>
                                </td>
                                <td class="t_r">
                                    <asp:Literal ID="l_8" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_9" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_10" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_11" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_12" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_13" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_14" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_15" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_16" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_17" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_18" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_19" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_20" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_21" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_22" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_23" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </div>
                <div style="width: 98%; margin: 4px auto;">
                    提示：<tt>点击列表中“工程数量”可查看工程信息列表。</tt>
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
