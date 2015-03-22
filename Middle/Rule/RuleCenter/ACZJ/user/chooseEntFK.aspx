<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chooseEntFK.aspx.cs" Inherits="GFEnt_user_chooseEntFK" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <base target="_self"></base>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">未发卡库
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td colspan="1" class="t_r">用户名：
                </td>
                <td class="t_l">
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="90px"></asp:TextBox>
                </td>
                <td colspan="1" class="t_r">密码：
                </td>
                <td class="t_l">
                    <asp:TextBox ID="t_passWord" runat="server" CssClass="m_txt" Width="90px"></asp:TextBox>
                    <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                    <input id="btnClear" type="button" value="重置" class="m_btn_w2" onclick="clearPage();" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
            AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound" OnItemCommand="DG_List_ItemCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="50px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FName" HeaderText="用户名" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FEndTime" HeaderText="有效结束日期" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="Select" HeaderStyle-Width="60px" Text="选定"></asp:ButtonColumn>
                <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <table align="center" width="98%">
            <tr>
                <td>
                    <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
