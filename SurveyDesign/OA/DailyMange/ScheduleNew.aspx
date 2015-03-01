<%@ Page Language="c#" AutoEventWireup="true" Inherits="OA_DailyMange_ScheduleNew" CodeFile="ScheduleNew.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>asdsad</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <link href="../style/Grid.css" rel="Stylesheet" type="text/css" />
    <style>
        a
        {
            text-decoration: none;
        }
    </style>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript" language="javascript">

        function CheckPeople() {
            var rv = window.showModalDialog('CheckPeople.aspx?rid=' + Math.random(), '', 'dialogWidth:330px; dialogHeight:600px; center:yes; resizable:no; status:no; help:no;scroll:auto;');

            if (rv != "" && rv != "undefined") {
                document.getElementById("t_FBaseId").value = rv;
            }

        } 
      
    </script>

    <base target="_self" />
</head>
<body>
    <div id="detail" style="position: absolute">
    </div>
    <form id="CLD" method="post" runat="server">
    <table align="center" class="m_title" width="100%">
        <tr>
            <th colspan="3">
                <asp:Label ID="l_BB" runat="server" Text="�ҵ��ճ̰���"></asp:Label></td>
            </th>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td align="right">
                ���ڣ�
            </td>
            <td align="left">
                <asp:TextBox ID="txtYear" runat="server" CssClass="m_txt" Width="100px" Font-Size="X-Small"
                    Height="18px"></asp:TextBox>��
                <asp:DropDownList ID="dropMonth" runat="server" AutoPostBack="True" CssClass="m_txt"
                    Height="23px" Font-Size="X-Small">
                    <asp:ListItem Value="һ��">һ��</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="ʮ��">ʮ��</asp:ListItem>
                    <asp:ListItem Value="ʮһ��">ʮһ��</asp:ListItem>
                    <asp:ListItem Value="ʮ����">ʮ����</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblErr" runat="server" Font-Size="X-Small" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="cmdQuery_Click"
                    Text="��ѯ" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; position: absolute; height: 100%" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <td valign="top" align="center">
                <table style="width: 99%; height: 100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td valign="top" style="font-size: 9pt; height: 470px;">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="tttable" style="font-size: 9pt">
                                        <asp:Label ID="lblYear" runat="server" Font-Size="X-Small"></asp:Label><asp:LinkButton
                                            ID="cmdToday" runat="server" ToolTip="�鿴����[�ҵ��ճ�]" Font-Size="X-Small"></asp:LinkButton><asp:Label
                                                ID="lblDate" runat="server" Visible="False" Font-Size="X-Small"></asp:Label>
                                    </td>
                                    <td style="width: 1px">
                                    </td>
                                    <td class="td" align="right">
                                    </td>
                                </tr>
                            </table>
                            <asp:Calendar ID="calSchedule" runat="server" Width="100%" Height="30%" SelectedDate='<%#GetDate()%>'
                                BackColor="Transparent" BorderColor="#75B5FF" ShowGridLines="True" PrevMonthText="<img src=../images/tou-1.gif alt=��һ�� border=0 />��һ��"
                                NextMonthText="��һ��<img src=../images/tou-2.gif alt=��һ�� border=0 />" BorderStyle="None"
                                DayNameFormat="Full" OnDayRender="calSchedule_DayRender" Font-Size="X-Small"
                                OnSelectionChanged="calSchedule_SelectionChanged" ForeColor="#6DA0F9">
                                <TodayDayStyle BorderWidth="2px" BorderStyle="Solid" BorderColor="#75B5FF" BackColor="#EBF1FF">
                                </TodayDayStyle>
                                <DayStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top"></DayStyle>
                                <NextPrevStyle ForeColor="#223399" CssClass="td"></NextPrevStyle>
                                <DayHeaderStyle Font-Size="Medium" Height="10px" BackColor="#EFEFCB" BorderColor="#75B5FF"
                                    ForeColor="#0F60D8"></DayHeaderStyle>
                                <TitleStyle CssClass="headcenter" Height="30px" BackColor="#75B5FF" Font-Bold="True"
                                    Font-Size="Medium" ForeColor="#0F60D8"></TitleStyle>
                                <WeekendDayStyle ForeColor="Red"></WeekendDayStyle>
                            </asp:Calendar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnReload" runat="server" OnClick="btnReload_Click" Style="display: none" />
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    function exitt() {
        window.returnValue = 1;
        window.close();
    }
</script>

