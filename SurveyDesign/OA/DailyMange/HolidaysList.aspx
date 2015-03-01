<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidaysList.aspx.cs" Inherits="OA_zSys_HolidaysList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelectNew/WdatePicker.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script language="javascript">
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
                节假日维护
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
                <a id="btn_Disp" runat="server" href="HolidaysCalendar.aspx">日历查看器</a>
            </td>
            <td style="width: 120px;">
                <b>
                    <asp:Label ID="la_MonthName" runat="server" Text=""></asp:Label>
                </b>
            </td>
            <td>
                <a id="btn_LastMonth" runat="server" class="m_btn_w2 f_l block" href="#">上月</a>
                <a id="btn_NextMonth" runat="server" class="m_btn_w2 f_l block" href="#">下月</a>
                <a id="btn_ThisMonth" runat="server" class="m_btn_w2 f_l block" href="HolidaysList.aspx">
                    本月</a>
            </td>
            <td class="t_r">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('HolidaysAdd.aspx?e=0',460,290);"
                    class="m_btn_w2" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Holidays_List" runat="server" HorizontalAlign="Center" Width="98%"
        CssClass="m_dg1" AutoGenerateColumns="False" OnItemDataBound="Holidays_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <HeaderStyle Width="30" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <HeaderStyle Width="50" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FDate" HeaderText="日期">
                <HeaderStyle Width="140" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FType" HeaderText="类型"></asp:BoundColumn>
            <asp:BoundColumn DataField="FTxt" HeaderText="备注"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    </form>
</body>
</html>
