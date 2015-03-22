<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chooseEnt.aspx.cs" Inherits="GFEnt_user_chooseEnt" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">企业选择
                </th>
            </tr>
            <tr>
                <td class="t_r">企业名称：
                </td>
                <td>
                    <asp:TextBox ID="t_FEntName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
                </td>
                <td class="t_r">企业类型：
                </td>
                <td>
                    <asp:DropDownList ID="t_FSystemId" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="101">施工</asp:ListItem>                        
                    </asp:DropDownList>
                </td>
                <td rowspan="2" align="center">

                    <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                    <input id="Button3" type="reset" value="重置" class="m_btn_w2 bnts_left10" />
                </td>
            </tr>
            <tr style="display: none;">
                <td class="t_r">用户名：
                </td>
                <td>
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
                </td>
                <td class="t_r"></td>
                <td></td>
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
                <asp:BoundColumn DataField="FCompany" HeaderText="企业名称" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FLinkMan" HeaderText="联系人" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type" HeaderText="企业类型" HeaderStyle-Width="70px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="Select" HeaderStyle-Width="50px" Text="选定"></asp:ButtonColumn>
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
