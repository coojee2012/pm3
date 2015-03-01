<%@ Page Language="C#" AutoEventWireup="true" CodeFile="statisByGCLB.aspx.cs" Inherits="Government_statis_statisByGCLB"
    ValidateRequest="false" %>

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
        $(function() {
            $("#drop_CountWay").change(function() {
                tab();
            });
        });
        function tab() {
            $("#dr_Year,#dr_Quarter,#dr_Month,#span_zdy").hide();
            if ($("#drop_CountWay").val() == "year")
                $("#dr_Year").show();
            else if ($("#drop_CountWay").val() == "quarter")
                $("#dr_Year,#dr_Quarter").show();
            else if ($("#drop_CountWay").val() == "month")
                $("#dr_Year,#dr_Month").show();
            else if ($("#drop_CountWay").val() == "zdy")
                $("#span_zdy").show();
        }
        function doOut(obj) {
            $('#h_div').val($('#div_Info').html());
            __doPostBack(obj.id, '');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                <asp:Literal ID="lit_Title" runat="server" Text="按工程类别统计"></asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                统计方式：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="drop_CountWay" runat="server">
                    <asp:ListItem Text="按年" Value="year"></asp:ListItem>
                    <asp:ListItem Text="按季度" Value="quarter"></asp:ListItem>
                    <asp:ListItem Text="按月" Value="month"></asp:ListItem>
                    <asp:ListItem Text="自定义" Value="zdy"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="dr_Year" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="dr_Quarter" runat="server">
                    <asp:ListItem Text="全部季度" Value=""></asp:ListItem>
                    <asp:ListItem Text="一季度" Value="1"></asp:ListItem>
                    <asp:ListItem Text="二季度" Value="2"></asp:ListItem>
                    <asp:ListItem Text="三季度" Value="3"></asp:ListItem>
                    <asp:ListItem Text="四季度" Value="4"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="dr_Month" runat="server">
                    <asp:ListItem Text="全部月" Value=""></asp:ListItem>
                    <asp:ListItem Text="一月" Value="1"></asp:ListItem>
                    <asp:ListItem Text="二月" Value="2"></asp:ListItem>
                    <asp:ListItem Text="三月" Value="3"></asp:ListItem>
                    <asp:ListItem Text="四月" Value="4"></asp:ListItem>
                    <asp:ListItem Text="五月" Value="5"></asp:ListItem>
                    <asp:ListItem Text="六月" Value="6"></asp:ListItem>
                    <asp:ListItem Text="七月" Value="7"></asp:ListItem>
                    <asp:ListItem Text="八月" Value="8"></asp:ListItem>
                    <asp:ListItem Text="九月" Value="9"></asp:ListItem>
                    <asp:ListItem Text="十月" Value="10"></asp:ListItem>
                    <asp:ListItem Text="十一月" Value="11"></asp:ListItem>
                    <asp:ListItem Text="十二月" Value="12"></asp:ListItem>
                </asp:DropDownList>
                <span id="span_zdy" style="display: none">
                    <asp:TextBox ID="txtFBTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                        Width="70px"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtFETime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                        Width="70px"></asp:TextBox>
                </span>
            </td>
            <td class="t_r t_bg">
                审查类别：
            </td>
            <td colspan="1">
                <asp:RadioButtonList ID="ddlFType" runat="server" CssClass="m_txt" BorderStyle="None"
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="287" Text="勘察" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="300" Text="设计"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td rowspan="1" class="t_c t_bg">
                <asp:Button ID="btnQuery" runat="server" Text="统计" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                &nbsp;
                <input id="btnClear" type="button" value="重置" class="m_btn_w2" onclick="clearPage();tab();"
                    style="margin: 6px auto;" />
                &nbsp;
                <asp:Button ID="btnOut" runat="server" Text="导出" CssClass="m_btn_w2" OnClick="btnOut_Click"
                    OnClientClick="return doOut(this);" UseSubmitBehavior="false" />
                &nbsp;
                <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="m_btn_w2" OnClick="btnReturn_Click"
                    Visible="false" />
            </td>
        </tr>
    </table>
    <div id="div_Info">
        <asp:Repeater runat="server" ID="DG_List" OnItemDataBound="DG_List_ItemDataBound"
            OnItemCommand="DG_List_ItemCommand">
            <HeaderTemplate>
                <table class="m_dg1" width="98%" align="Center" border="1px">
                    <tr class="m_dg1_h">
                        <td rowspan="2" class="t_c t_bg">
                            序号
                        </td>
                        <td rowspan="2" class="t_c t_bg">
                            地区
                        </td>
                        <td colspan="2" class="t_c">
                            审查项目数
                        </td>
                        <td colspan="2" class="t_c">
                            项目合同金额(万元)
                        </td>
                        <td colspan="2" class="t_c">
                            一次性通过审查项目数
                        </td>
                        <td colspan="2" class="t_c">
                            违反工程建设标准强制性条文数
                        </td>
                    </tr>
                    <tr class="m_dg1_h">
                        <td colspan="1" class="t_c">
                            房屋建筑
                        </td>
                        <td colspan="1" class="t_c">
                            市政基础
                        </td>
                        <td colspan="1" class="t_c">
                            房屋建筑
                        </td>
                        <td colspan="1" class="t_c">
                            市政基础
                        </td>
                        <td colspan="1" class="t_c">
                            房屋建筑
                        </td>
                        <td colspan="1" class="t_c">
                            市政基础
                        </td>
                        <td colspan="1" class="t_c">
                            房屋建筑
                        </td>
                        <td colspan="1" class="t_c">
                            市政基础
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="m_dg1_i">
                    <td class="t_c">
                        <%#Container.ItemIndex+1 %>
                    </td>
                    <td class="t_c">
                        <asp:LinkButton ID="ltName" runat="server" Text='<%#Eval("FName")  %>' CommandArgument='<%#Eval("FNumber") %>'
                            CommandName="Sel"></asp:LinkButton>
                    </td>
                    <td class="t_c">
                        <%#Eval("F1")  %>
                    </td>
                    <td class="t_c">
                        <%#Eval("J1")  %>
                    </td>
                    <td class="t_r">
                        <%#Eval("F1_1")  %>
                    </td>
                    <td class="t_r">
                        <%#Eval("J1_1")  %>
                    </td>
                    <td class="t_c">
                        <%#Eval("F4")  %>
                    </td>
                    <td class="t_c">
                        <%#Eval("J4")  %>
                    </td>
                    <td class="t_c">
                        <%#Eval("F5")  %>
                    </td>
                    <td class="t_c">
                        <%#Eval("J5")  %>
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
                    <td class="t_c">
                        <asp:Literal ID="l_1" runat="server"></asp:Literal>
                    </td>
                    <td class="t_r">
                        <asp:Literal ID="l_2" runat="server"></asp:Literal>
                    </td>
                    <td class="t_r">
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
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <input id="h_div" type="hidden" runat="server" />
    </form>
</body>
</html>
