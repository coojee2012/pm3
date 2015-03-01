<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCXMBB_KC.aspx.cs" Inherits="KcsjSgt_Statistics_SCXMBB_KC" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(function() {
            $("#drop_CountWay").change(function() { tab(); });
            tab();
        });


        function tab() {
            $("#dr_Year,#dr_Quarter,#dr_Month,#div_autoDate").hide();
            if ($("#drop_CountWay").val() == "year") {
                $("#dr_Year").show();
            }
            if ($("#drop_CountWay").val() == "quarter") {
                $("#dr_Year,#dr_Quarter").show();
            }
            else if ($("#drop_CountWay").val() == "month") {
                $("#dr_Year,#dr_Month").show();
            }
            else if ($("#drop_CountWay").val() == "auto") {
                $("#div_autoDate").show();
            }
        }
    </script>

    <style type="text/css">
        span { color: Red; font-weight: bold; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                <asp:Literal ID="lit_Title" runat="server" Text="勘察文件审查项目报表统计"></asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                时间：
            </td>
            <td colspan="5">
                <div class="f_l">
                    <asp:DropDownList ID="drop_CountWay" runat="server">
                        <asp:ListItem Text="按年" Value="year"></asp:ListItem>
                        <asp:ListItem Text="按季度" Value="quarter"></asp:ListItem>
                        <asp:ListItem Text="按月" Value="month"></asp:ListItem>
                        <asp:ListItem Text="自定义" Value="auto"></asp:ListItem>
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
                </div>
                <div id="div_autoDate" style="display: none;">
                    <asp:TextBox ID="t_Date1" runat="server" CssClass="m_txt" Width="70px" onfocus="WdatePicker();"></asp:TextBox>
                    至
                    <asp:TextBox ID="t_Date2" runat="server" CssClass="m_txt" Width="70px" onfocus="WdatePicker();"></asp:TextBox>
                </div>
            </td>
            <td rowspan="3" class="t_c t_bg">
                <asp:Button ID="btnQuery" runat="server" Text="统计" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                <br />
                <input id="btnClear" type="button" value="重置" class="m_btn_w2" onclick="$('[id^=t_],[id^=dr_]').val('');"
                    style="margin: 6px auto;" />
                <br />
                <asp:Button ID="btnOut" runat="server" Text="导出" CssClass="m_btn_w2" OnClick="btnOut_Click" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程类别：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server">
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                设计规模：
            </td>
            <td>
                <asp:DropDownList ID="t_FScale" runat="server">
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                投资总额(万元)：
            </td>
            <td>
                <asp:TextBox ID="t_FAllMoney1" runat="server" CssClass="m_txt t_r" Width="60px" onblur="isFloat(this);"></asp:TextBox>
                至
                <asp:TextBox ID="t_FAllMoney2" runat="server" CssClass="m_txt t_r" Width="60px" onblur="isFloat(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                一审通过：
            </td>
            <td>
                <asp:DropDownList ID="t_FirstApp" runat="server">
                    <asp:ListItem Value="" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="是"></asp:ListItem>
                    <asp:ListItem Value="2" Text="否"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                合格证：
            </td>
            <td>
                <asp:DropDownList ID="t_Pint" runat="server">
                    <asp:ListItem Value="" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="已打印"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未打印"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                备案情况：
            </td>
            <td>
                <asp:DropDownList ID="t_Bak" runat="server">
                    <asp:ListItem Value="" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未办理"></asp:ListItem>
                    <asp:ListItem Value="1" Text="正在备案"></asp:ListItem>
                    <asp:ListItem Value="2" Text="备案通过"></asp:ListItem>
                    <asp:ListItem Value="3" Text="备案不通过"></asp:ListItem>
                    <asp:ListItem Value="4" Text="打回"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="7" style="line-height: 22px; padding: 4px 20px; color: #333333;">
                项目总数（
                <asp:Label ID="la_all_Count" runat="server"></asp:Label>
                ）个，投资总额（
                <asp:Label ID="la_all_Money" runat="server"></asp:Label>
                ）万元，一次通过审查项目（
                <asp:Label ID="la_all_First" runat="server"></asp:Label>
                ）个。
                <br />
                房屋建筑工程项目（
                <asp:Label ID="la_fw_Count" runat="server"></asp:Label>
                ）个，投资总额（
                <asp:Label ID="la_fw_Money" runat="server"></asp:Label>
                ）万元，一次通过审查项目（
                <asp:Label ID="la_fw_First" runat="server"></asp:Label>
                ）个，总建筑面积（
                <asp:Label ID="la_fw_Area" runat="server"></asp:Label>
                ）平方米，总层数（
                <asp:Label ID="la_fw_Layers" runat="server"></asp:Label>
                ）层，地上（
                <asp:Label ID="la_fw_Ground" runat="server"></asp:Label>
                ）层，地下（
                <asp:Label ID="la_fw_Underground" runat="server"></asp:Label>
                ）层，总建筑高度（
                <asp:Label ID="la_fw_Height" runat="server"></asp:Label>
                ）米；
                <br />
                市政基础工程项目（
                <asp:Label ID="la_sz_Count" runat="server"></asp:Label>
                ）个，投资总额（
                <asp:Label ID="la_sz_Money" runat="server"></asp:Label>
                ）万元，一次通过审查项目（
                <asp:Label ID="la_sz_First" runat="server"></asp:Label>
                ）个。
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
    </table>
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Width="2000"
        Style="margin: 4px 10px;">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <HeaderStyle Width="40px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="工程名称">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <%#Eval("p.FPrjName")%>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="工程所属区域">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="工程类别">
                <HeaderStyle Width="60" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="设计规模">
                <HeaderStyle Width="60" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="建设性质">
                <HeaderStyle Width="60" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="建筑面积（㎡）">
                <HeaderStyle Width="60" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="地上层数">
                <HeaderStyle Width="30" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="地下层数">
                <HeaderStyle Width="30" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="高度（m）">
                <HeaderStyle Width="30" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="投资总额（万元）">
                <HeaderStyle Width="60" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="结构类型">
                <HeaderStyle Width="60" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="建设单位">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="勘察单位">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审查机构" Visible="false">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审查人员">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="违反强条数量">
                <HeaderStyle Width="40" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="工程勘察收费&lt;/br&gt;(万元)">
                <HeaderStyle Width="60" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审查收费（万元）">
                <HeaderStyle Width="60" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审查受理&lt;/br&gt;日期">
                <HeaderStyle Width="70" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审查通过&lt;/br&gt;日期">
                <HeaderStyle Width="70" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审查历时(天)">
                <HeaderStyle Width="50" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="是否一审通过">
                <HeaderStyle Width="50" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="合格证">
                <HeaderStyle Width="80" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="备案情况">
                <HeaderStyle Width="80" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table width="98%" align="center">
        <tr>
            <td>
                <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
