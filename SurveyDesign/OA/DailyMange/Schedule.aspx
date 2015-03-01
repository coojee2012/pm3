<%@ Page Language="c#" AutoEventWireup="true" Inherits="OA_DailyMange_Schedule" CodeFile="Schedule.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>asdsad</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <link href="../style/Grid.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript" language="javascript">
      
     function   CheckPeople()
    {
        var rv = window.showModalDialog('CheckPeople.aspx?rid='+Math.random(),'','dialogWidth:330px; dialogHeight:600px; center:yes; resizable:no; status:no; help:no;scroll:auto;');  
     
       if(rv!=""&&rv!="undefined")
      {
           document.getElementById("t_FBaseId").value=rv;
       }

    } 
      
    </script>

    <base target="_self" />
</head>
<body <%=str_NoticeMsg%>>
    <div id="detail" style="position: absolute">
    </div>
    <form id="CLD" method="post" runat="server">
        <br />
        <table align="center" style="width: 95%">
            <tr>
                <td align="center" style="font-weight: bold; font-size: 16px; height: 17px">
                    <asp:Label ID="l_BB" runat="server" ForeColor="#2A586F" Text="日程任务：我的日程 "></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 95%" class="TableContent" cellspacing="0" cellpadding="0" align="center"
            border="0">
            <tbody>
                <tr>
                    <td style="width: 116px; height: 16px" class="tdRight td14">
                        日期：</td>
                    <td>
                        年<asp:TextBox ID="txtYear" runat="server" Width="100px" Font-Size="X-Small"></asp:TextBox>月<asp:DropDownList
                            ID="dropMonth" runat="server" Width="142px" AutoPostBack="True" Height="23px"
                            Font-Size="X-Small">
                            <asp:ListItem Value="一月">一月</asp:ListItem>
                            <asp:ListItem Value="二月">二月</asp:ListItem>
                            <asp:ListItem Value="三月">三月</asp:ListItem>
                            <asp:ListItem Value="四月">四月</asp:ListItem>
                            <asp:ListItem Value="五月">五月</asp:ListItem>
                            <asp:ListItem Value="六月">六月</asp:ListItem>
                            <asp:ListItem Value="七月">七月</asp:ListItem>
                            <asp:ListItem Value="八月">八月</asp:ListItem>
                            <asp:ListItem Value="九月">九月</asp:ListItem>
                            <asp:ListItem Value="十月">十月</asp:ListItem>
                            <asp:ListItem Value="十一月">十一月</asp:ListItem>
                            <asp:ListItem Value="十二月">十二月</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnQuery" runat="server" class="cBtn2" Height="22px" OnClick="cmdQuery_Click"
                            Text="显示" />
                        <asp:Label ID="lblErr" runat="server" CssClass="err" Visible="False" ForeColor="Red"
                            Font-Size="X-Small"></asp:Label>
                        <asp:Label ID="lblUName" runat="server" CssClass="uname" Font-Size="X-Small"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 116px; height: 16px" class="tdRight td14">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="代下属上报" /></td>
                    <td>
                        &nbsp; 用户姓名：<asp:TextBox ID="t_FUserName" runat="server" Width="95px" MaxLength="20"
                            CssClass="cTextBox1"></asp:TextBox>&nbsp; <span style="color: #ff0000">*</span>&nbsp;
                        <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server" OnClientClick=" CheckPeople();">【选择】</asp:LinkButton>
                        <input id="t_FBaseId" runat="server" type="hidden" />
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                </tr>
            </tbody>
        </table>
        <table style="width: 100%; position: absolute; height: 100%" cellspacing="0" cellpadding="0"
            border="0">
            <tr>
                <td valign="top" align="center">
                    <table style="width: 95%; height: 100%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td valign="top" style="font-size: 9pt; height: 470px;">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="tttable" style="font-size: 9pt">
                                            日程列表
                                            <asp:Label ID="lblYear" runat="server" Font-Size="X-Small"></asp:Label><asp:LinkButton
                                                ID="cmdToday" runat="server" ToolTip="查看今日[我的日程]" Font-Size="X-Small"></asp:LinkButton><asp:Label
                                                    ID="lblDate" runat="server" Visible="False" Font-Size="X-Small"></asp:Label></td>
                                        <td style="width: 1px">
                                        </td>
                                        <td class="td" align="right">
                                        </td>
                                    </tr>
                                </table>
                                <asp:Calendar ID="calSchedule" runat="server" Width="100%" Height="90%" SelectedDate='<%#GetDate()%>'
                                    BackColor="Transparent" BorderColor="#999999" ShowGridLines="True" PrevMonthText="&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;<img src=../img/left_arrow.gif alt=上一月 border=0 />上一月"
                                    NextMonthText="下一月<img src=../img/right_arrow.gif alt=下一月 border=0 />&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;"
                                    BorderStyle="None" DayNameFormat="Full" OnDayRender="calSchedule_DayRender" Font-Size="X-Small"
                                    OnSelectionChanged="calSchedule_SelectionChanged">
                                    <TodayDayStyle BorderWidth="2px" BorderStyle="Solid" BorderColor="CornflowerBlue"
                                        BackColor="#F0F0E8"></TodayDayStyle>
                                    <DayStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top"></DayStyle>
                                    <NextPrevStyle ForeColor="#223399" CssClass="td"></NextPrevStyle>
                                    <DayHeaderStyle Font-Size="13px" Height="10px" BackColor="#F0F0E8"></DayHeaderStyle>
                                    <TitleStyle CssClass="headcenter" Height="10px"></TitleStyle>
                                    <WeekendDayStyle ForeColor="Red"></WeekendDayStyle>
                                </asp:Calendar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
function exitt()
{
window.returnValue=1;
window.close();
}
</script>

