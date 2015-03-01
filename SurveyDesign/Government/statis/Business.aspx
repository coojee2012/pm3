<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Business.aspx.cs" Inherits="Government_statis_Business" %>

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

            showChart("div2", "MSColumn3D.swf");
            showChart("div3", "MSSpline.swf");
        });

        //调用图形
        function showChart(controlid, url) {
            var txt_Year1 = $("#txt_Year1").val();
            var txt_Month1 = $("#txt_Month1").val();
            var txt_Year2 = $("#txt_Year2").val();
            var txt_Month2 = $("#txt_Month2").val();
            var chart = new FusionCharts("../Chart/" + url, "ChartId", getWidth(document.getElementById("t_bar")), "450", "0", "0");
            var url = "BusinessXML.aspx?txt_Year1=" + txt_Year1 + "&txt_Month1=" + txt_Month1 + "&txt_Year2=" + txt_Year2 + "&txt_Month2=" + txt_Month2 + "&Quarter=<%=rbQuarter.Checked %>&e=" + Math.random();
            chart.setDataURL(escape(url));
            chart.render(controlid);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table align="center" class="m_title" width="98%" cellpadding="0" cellspacing="0">
        <tr>
            <th colspan="5">
                业务统计
            </th>
        </tr>
        <tr>
            <td class="t_r">
                <asp:RadioButton ID="rbYear" Text="按年度" GroupName="A" runat="server" AutoPostBack="True"
                    OnCheckedChanged="rbQuarter_CheckedChanged" />
                <asp:RadioButton ID="rbQuarter" Text="按月份" GroupName="A" runat="server" Checked="True"
                    AutoPostBack="True" OnCheckedChanged="rbQuarter_CheckedChanged" />
                办理时间：
            </td>
            <td>
                <asp:DropDownList ID="txt_Year1" runat="server">
                </asp:DropDownList>
                年 <span id="span1" runat="server" visible="false">--
                    <asp:DropDownList ID="txt_Year2" runat="server">
                    </asp:DropDownList>
                    年</span>
            </td>
            <td class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                    Text="查询" />
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
        <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="True" CssClass="m_dg1"
            HorizontalAlign="Center" Width="98%">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
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
