<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CardNoPubList.aspx.cs" Inherits="Share_Sys_CardNoPubList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>加密锁管理</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        }); 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                加密锁管理
            </th>
        </tr>
        <tr>
            <td class="t_r">
                加密锁硬件编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt" Width="110px"></asp:TextBox>
            </td>
            <td class="t_r">
                批次：
            </td>
            <td>
                <asp:DropDownList ID="t_FBatchId" runat="server">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                状态：
            </td>
            <td nowrap="noWrap">
                <asp:DropDownList ID="t_FState" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="未分配" Value="0"></asp:ListItem>
                    <asp:ListItem Text="已分配" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="center" rowspan="1">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input type="reset" value="清空" class="m_btn_w2 bnts_left10" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('CardNoEdit.aspx?',420,300);"
                    class="m_btn_w2" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" Wrap="False" />
        <ItemStyle CssClass="m_dg1_i" Wrap="False" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="20px" Font-Underline="False" Wrap="False" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
                <FooterStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="加密锁标签编号">
                <ItemTemplate>
                    <a href="javascript:showAddWindow('CardNoEdit.aspx?FID=<%#Eval("FID") %>',420,300);">
                        <%#Eval("FLockLabelNumber")%></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FLockNumber" HeaderText="加密锁硬件编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="FBatchId" HeaderText="批次"></asp:BoundColumn>
            <asp:BoundColumn DataField="FState" HeaderText="状态">
                <ItemStyle Width="80px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
        <FooterStyle Font-Underline="False" Wrap="False" />
        <EditItemStyle Font-Underline="False" Wrap="False" />
        <SelectedItemStyle Font-Underline="False" Wrap="False" />
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
