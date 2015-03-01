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
                    <asp:Repeater runat="server" ID="DG_List" OnItemDataBound="DG_List_ItemDataBound">
                        <HeaderTemplate>
                            <table class="m_dg1" width="98%">
                                <tr class="m_dg1_h">
                                    <td rowspan="2" class="t_c t_bg">
                                        序号
                                    </td>
                                    <td rowspan="2" class="t_c t_bg">
                                        月份
                                    </td>
                                    <td colspan="2" class="t_c">
                                        参与项目数量
                                    </td>
                                    <%--  <td colspan="2" class="t_c">
                                        人员意见：已完成
                                    </td>
                                    <td colspan="2" class="t_c">
                                        人员意见：未完成
                                    </td>--%>
                                </tr>
                                <tr class="m_dg1_h">
                                    <td colspan="1" class="t_c">
                                        初步设计
                                    </td>
                                    <td colspan="1" class="t_c">
                                        文件编制
                                    </td>
                                    <%--  <td colspan="1" class="t_c">
                                        初步设计
                                    </td>
                                    <td colspan="1" class="t_c">
                                        文件编制
                                    </td>
                                    <td colspan="1" class="t_c">
                                        初步设计
                                    </td>
                                    <td colspan="1" class="t_c">
                                        文件编制
                                    </td>--%>
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
                                    <%#Eval("F")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J")  %>
                                </td>
                                <%--   <td class="t_c">
                                    <%#Eval("F1")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J1")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("F2")  %>
                                </td>
                                <td class="t_c">
                                    <%#Eval("J2")  %>
                                </td>--%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="m_dg1_i">
                                <td class="t_r t_bg" colspan="2">
                                    合计：
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_0" runat="server"></asp:Literal>
                                </td>
                                <td class="t_c">
                                    <asp:Literal ID="l_1" runat="server"></asp:Literal>
                                </td>
                                <%--    <td class="t_c">
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
                                </td>--%>
                            </tr>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </div>
                <div style="width: 98%; margin: 4px auto;">
                    提示：<tt>点击列表中“参与项目数量”可查看参与的工程项目信息(设计成果已经提交的)列表。</tt>
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
