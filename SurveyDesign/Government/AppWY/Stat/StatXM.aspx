<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatXM.aspx.cs" Inherits="Government_AppWY_Stat_StatGS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../script/default.js"> </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="7">
                        <asp:Literal ID="lit_Title" runat="server" Text="项目统计"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td class="t_l t_bg" colspan="1">&nbsp;&nbsp;当前部门：&nbsp;&nbsp;<asp:Label ID="t_DeptName" runat="server" Text="Label"></asp:Label>
                    </td>

                </tr>
            </table>
            <div id="div_Info">
                <asp:Repeater runat="server" ID="DG_List" OnItemDataBound="DG_List_ItemDataBound">
                    <HeaderTemplate>
                        <table class="m_dg1" width="98%" align="Center" border="1px">
                            <tr class="m_dg1_h">
                                <td rowspan="3" class="t_c t_bg">序号
                                </td>
                                <td rowspan="3" class="t_c t_bg">地区
                                </td>
                                <td colspan="8" class="t_c" style="text-align: center">项目个数
                                </td>
                                <td rowspan="3" colspan="1" class="t_c">建筑面积
                                </td>
                                <td rowspan="3" colspan="1" class="t_c">占地面积
                                </td>
                            </tr>
                            <tr class="m_dg1_h">
                                <td rowspan="2" class="t_c">项目总计
                                </td>
                                <td colspan="3" class="t_c">按项目类型分
                                </td>
                                <td colspan="4" class="t_c">按住保房类型分
                                </td>
                            </tr>
                            <tr class="m_dg1_h">
                                <td colspan="1" class="t_c">住宅
                                </td>
                                <td colspan="1" class="t_c">商住
                                </td>
                                <td colspan="1" class="t_c">办公
                                </td>
                                <td colspan="1" class="t_c">廉租房
                                </td>
                                <td colspan="1" class="t_c">公租房
                                </td>
                                <td colspan="1" class="t_c">限价房
                                </td>
                                <td colspan="1" class="t_c">经适房
                                </td>


                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="m_dg1_i">
                            <td class="t_c">
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td class="t_c">
                                <%#Eval("DeptName")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("XMCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("ZZCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("SZCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("BGCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("LZFCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("GZFCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("XJFCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("JSFCount")  %>
                            </td>

                            <td class="t_c">
                                <%#Eval("JZMJSum")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("ZDMJSum")  %>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class="m_dg1_i">
                            <td class="t_c t_bg" colspan="2">合计：
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
                            <td class="t_c">
                                <asp:Literal ID="l_6" runat="server"></asp:Literal>
                            </td>
                            <td class="t_c">
                                <asp:Literal ID="l_7" runat="server"></asp:Literal>
                            </td>
                            <td class="t_c">
                                <asp:Literal ID="l_8" runat="server"></asp:Literal>
                            </td>
                            <td class="t_c">
                                <asp:Literal ID="l_9" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <input type="hidden" id="hidCurDeptLevel" value="1" runat="server" />
    </form>
</body>
</html>

