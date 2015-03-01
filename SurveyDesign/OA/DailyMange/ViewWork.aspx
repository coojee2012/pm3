<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewWork.aspx.cs" Inherits="OA_DailyMange_ViewWork" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看事务</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

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

    <style> 
.hidden { display:none;}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" class="m_title" width="99%">
            <tr>
                <th align="left" colspan="5">
                    <asp:Label ID="l_BB" runat="server" Text="查看事务"></asp:Label></th>
            </tr>
        
            <tr>
                <td style="width: 13%; height: 16px" align="right">
                    开始时间：</td>
                <td>
                    <asp:TextBox ID="txt_BeginTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                        TabIndex="3" type="button" Width="99px" Height="16px"></asp:TextBox></td>
                <td style="width: 12%" align="right">
                    结束时间：</td>
                <td>
                    <asp:TextBox ID="txtEndtime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                        TabIndex="3" type="button" Width="99px" Height="16px"></asp:TextBox></td>
                <td align="center" rowspan="3">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                        Text="查询" />
                    <input id="btnReset" runat="server" class="m_btn_w2" onclick="clearPage();" type="button"
                        value="重填" />
                </td>
            </tr>
            <tr>
                <td style="width: 13%" align="right">
                    事物类型：</td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Text="请选择" Value="99"></asp:ListItem>
                        <asp:ListItem Text="工作事务" Value="0"></asp:ListItem>
                        <asp:ListItem Text="个人事务" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 12%" align="right">
                    定期模式：</td>
                <td>
                    <asp:DropDownList ID="ddlTermly" runat="server">
                        <asp:ListItem Text="请选择" Value="99"></asp:ListItem>
                    <asp:ListItem Text="每日" Value="0"></asp:ListItem>
                    <asp:ListItem Text="每周" Value="1"></asp:ListItem>
                    <asp:ListItem Text="每月" Value="2"></asp:ListItem>
                    <asp:ListItem Text="只一次" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 13%" align="right">
                    事务标题：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtTitle" CssClass="m_txt" runat="server" MaxLength="50" 
                        Width="263px"></asp:TextBox></td>
                
                
            </tr>
        </table>
        <asp:GridView ID="dgList" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Style="margin-top: 5px;" OnRowDataBound="dgList_RowDataBound"
            Width="99%" EmptyDataText="您还没有录入日程事务安排">
            <RowStyle CssClass="m_dg1_i" />
        <HeaderStyle CssClass="m_dg1_h" />
            <Columns>
                <asp:BoundField HeaderText="序号">
                    <ItemStyle Width="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="标题" DataField="FTitle">
                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="定期模式" DataField="FTermly">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="事务类型" DataField="FCalendarType">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="事务内容" DataField="FContent">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FIsOver" HeaderText="状态">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="开始时间" DataField="FbeginDate" DataFormatString="{0:HH:mm:ss}">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="80px" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField HeaderText="结束时间" DataField="FOverDate" DataFormatString="{0:HH:mm:ss}">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="查看">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbuSelect" runat="server" Text="查看">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle CssClass="hidden" Width="6%" />
                    <HeaderStyle CssClass="hidden" />
                </asp:TemplateField>
                <asp:BoundField DataField="FIsRead" HeaderText="已读">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:BoundField DataField="Fid" Visible="false" />
            </Columns>
            <PagerStyle CssClass="LItable_end" Font-Size="X-Small" HorizontalAlign="Right" />
            <EmptyDataRowStyle Font-Bold="True" Font-Size="10pt" ForeColor="Desktop" />
        </asp:GridView>
        <table align="center" class="DataListTableBottom01" width="99%">
            <tr>
                <td width="10">
                </td>
                <td>
                    <uc1:pager ID="Pager1" runat="server" />
                </td>
                <td width="10">
                </td>
            </tr>
        </table>
        <asp:Button ID="btnReload" runat="server" OnClick="btnReload_Click" Style="display: none" />
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

