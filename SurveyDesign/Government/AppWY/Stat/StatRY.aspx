<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatRY.aspx.cs" Inherits="Government_AppWY_Stat_StatRY" %>

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
                        <asp:Literal ID="lit_Title" runat="server" Text="人员统计"></asp:Literal>
                    </th>
                </tr>

            </table>
            <div id="div_Info">
                <asp:Repeater runat="server" ID="DG_List" OnItemDataBound="DG_List_ItemDataBound">
                    <HeaderTemplate>
                        <table class="m_dg1" width="98%" align="Center" border="1px">
                            <tr class="m_dg1_h">
                                <td rowspan="2" class="t_c t_bg">序号
                                </td>
                                <td rowspan="2" class="t_c t_bg">地区
                                </td>
                                <td rowspan="2" class="t_c t_bg">人员总数
                                </td>
                                <td rowspan="2" class="t_c t_bg">有职称人数
                                </td>
                                <td colspan="5" class="t_c t_bg">按人员类型分
                                </td>
                                <%--<td rowspan="2" colspan="1" class="t_c">总计
                                </td>--%>
                            </tr>
                            <tr class="m_dg1_h">
                                <td class="t_c">经营人员
                                </td>
                                <td class="t_c">统计人员
                                </td>
                                <td class="t_c">技术人员
                                </td>
                                <td class="t_c">财务人员
                                </td>
                                <td class="t_c">秩序维护
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
                                <%#Eval("Total")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("YZCCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("JYCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("TJCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("JSCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("CWCount")  %>
                            </td>
                            <td class="t_c">
                                <%#Eval("ZXCount")  %>
                            </td>
                            <%-- <td class="t_r">
                                <%#Eval("Total")  %>
                            </td>--%>
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
