<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyMange.aspx.cs" Inherits="OA_DailyMange_DailyMange" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>日程安排</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
<script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <style> 
.hidden { display:none;}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" class="m_title" width="99%">
            <tr>
                <th colspan="3">
                    <asp:Label ID="l_BB" runat="server" Text="管理日程安排"></asp:Label></th>
            </tr>
      
        
            <tr>
                <td>
                    &nbsp;<img src="../images/xing-2.jpg" />
                    查询列表</td>
                <td align="center">
                    <asp:Label ID="lblDate" runat="server" ForeColor="Red"></asp:Label></td>
                <td align="right">
                    <asp:Button ID="btnCreate" CssClass="m_btn_w2" runat="server" Text="新增" OnClick="btnCreate_Click" />
                    <asp:Button
                        ID="btnReturn" CssClass="m_btn_w2" runat="server" Text="返回" OnClick="btnReturn_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="dgList" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnRowDataBound="dgList_RowDataBound" Width="99%" EmptyDataText="该天没有安排"
            OnRowDeleting="dgList_RowDeleting" DataKeyNames="Fid">
           <RowStyle CssClass="m_dg1_i" />
        <HeaderStyle CssClass="m_dg1_h" />
            <Columns>
                <asp:BoundField HeaderText="序号">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="标题" DataField="FTitle" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="定期模式" DataField="FTermly">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="事务类型" DataField="FCalendarType">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="事务内容" DataField="FContent" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FIsOver" HeaderText="状态">
                    <ItemStyle Width="7%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="开始时间" DataField="fFbeginDate">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField HeaderText="结束时间" DataField="fFOverDate">
                    <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="修改">
                    <ItemStyle Width="6%" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lbuSelect" runat="server" Text="修改">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" DeleteText=" 删除" HeaderText="删除">
                    <ItemStyle Width="6%" />
                </asp:CommandField>
                <asp:BoundField DataField="FID">
                <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:BoundField DataField="FIsRead" HeaderText="已读">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                </asp:BoundField>
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

