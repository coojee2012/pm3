<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackupCount.aspx.cs" Inherits="Government_statis_BackupCount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../DateSelect/WdatePicker.js" type="text/javascript"></script>

    <script src="../chart/FusionCharts.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");

            showChart(2, "div2", "Pie3D.swf");
            //showChart("div3", "MSSpline.swf");
        });

        //调用图形
        function showChart(type, controlid, url) {
            var FStartTime = $("#txtStartTime").val();
            var FEndTime = $("#txtEndTime").val();
         escape
             chart = new FusionCharts("../Chart/" + url+"?ChartNoDataText=没有数据&InvalidXMLText=加载失败", "ChartId", getWidth(document.getElementById("t_bar")), "450", "0", "0");
            var url = location.href+ "&type=" + type + "&FStartTime=" + FStartTime + "&FEndTime=" + FEndTime + "&CountWay=diy&DeptId=<%=DeptNumber %>&e=" + Math.random();
          
            chart.setDataURL(escape(url));
            chart.render(controlid);
        }


        $(function() {
            $("#drop_CountWay").change(function() { tab(); $("#btnCount").click(); });
            tab();
        });


        function tab() {
            $("#dr_Quarter,#dr_Month,#spanDIY").hide();
            if ($("#drop_CountWay").val() == "quarter")
                $("#dr_Quarter").show();
            else if ($("#drop_CountWay").val() == "month")
                $("#dr_Month").show();
            else if ($("#drop_CountWay").val() == "diy") {
                $("#spanDIY").show();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="3">
                <asp:Literal ID="lPostion" runat="server"></asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                查询方式：
            </td>
            <td>
                <asp:DropDownList ID="drop_CountWay" runat="server">
                    <asp:ListItem Text="按年" Value="year"></asp:ListItem>
                    <asp:ListItem Text="按季度" Value="quarter"></asp:ListItem>
                    <asp:ListItem Text="按月" Value="month"></asp:ListItem>
                    <asp:ListItem Text="自定义" Value="diy"></asp:ListItem>
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
                <span id="spanDIY">
                从<asp:TextBox ID="txtStartTime" CssClass="m_txt" runat="server" onfocus="WdatePicker()"
                    Width="65px"></asp:TextBox>至
                <asp:TextBox ID="txtEndTime"  CssClass="m_txt" runat="server" onfocus="WdatePicker()"
                    Width="65px"></asp:TextBox></span>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="统计" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="m_btn_w2" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar" id="t_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_l">
                统计列表
            </td>
            <td class="t_r">
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <div id="div1">
        <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Width="98%" OnItemCommand="DG_List_ItemCommand" OnItemDataBound="DG_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="地区">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%# Eval("FNumber") %>'
                            Text='<%# DataBinder.Eval(Container, "DataItem.FName") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="备案项目数" DataField="FCount"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="合同金额（万元）" DataField="Money"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="见证合同金额（万元）" DataField="JZMoney"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <table width="98%" align="center" class="m_bar" id="Table1">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_l">
                统计图形展示
            </td>
            <td class="t_r">
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <div id="div2" class="t_c" style="margin: 4px auto;">
    </div>
    <div id="div3" class="t_c" style="margin: 4px auto;">
    </div>
    </form>
</body>
</html>
