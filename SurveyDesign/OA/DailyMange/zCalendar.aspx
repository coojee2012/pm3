<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zCalendar.aspx.cs" Inherits="OA_DailyMange_zCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelectNew/WdatePicker.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                我的日程
            </th>
        </tr>
        <tr>
            <td class="t_r">
                月份：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_Year" runat="server">
                </asp:DropDownList>
                年
                <asp:DropDownList ID="t_Month" runat="server">
                </asp:DropDownList>
                月
            </td>
            <td class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="查询" OnClick="btnQuery_Click" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td style="width: 100px;">
                <a id="btn_Disp" runat="server" href="HolidaysList.aspx" visible="false">列表查看器</a>
            </td>
            <td style="width: 120px;">
                <b>
                    <asp:Label ID="la_MonthName" runat="server" Text=""></asp:Label>
                </b>
            </td>
            <td>
                <a id="btn_LastMonth" runat="server" class="m_btn_w2 f_l block" href="#">上月</a>
                <a id="btn_NextMonth" runat="server" class="m_btn_w2 f_l block" href="#">下月</a>
                <a id="btn_ThisMonth" runat="server" class="m_btn_w2 f_l block" enable="false" href="HolidaysCalendar.aspx">
                    本月</a>
            </td>
            <td class="t_r">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('zAdd.aspx?e=0',460,290);"
                    class="m_btn_w2" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="98%" align="center" style="margin-top: 5px; margin-bottom: 5px;">
        <tr>
            <td>
                <div class="day_div">
                    <asp:Calendar ID="C_Date" runat="server" Width="100%" align="center" OnDayRender="C_Date_DayRender"
                        OnVisibleMonthChanged="C_Date_VisibleMonthChanged" SelectionMode="None" CssClass="day_table"
                        NextMonthText="" PrevMonthText="">
                        <TitleStyle CssClass="day_Title" />
                        <DayHeaderStyle CssClass="day_Week" />
                        <NextPrevStyle CssClass="day_m_last" />
                        <DayStyle CssClass="day_day" />
                    </asp:Calendar>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
