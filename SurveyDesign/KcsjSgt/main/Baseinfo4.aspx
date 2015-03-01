<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Baseinfo4.aspx.cs" Inherits="KcsjSgt_main_Baseinfo4" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                资质证书信息
            </td>
            <td class="t_r">
                
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="dgCerti_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="dgCerti_List_ItemDataBound" Style="margin-top: 3px;
        margin-bottom: 1px;" Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn Visible="false">
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll1" runat="server" onclick="checkAllByTag(this,'CheckItem2');" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem2" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FCertiNo" HeaderText="证书编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="FAppDeptId" HeaderText="颁发部门"></asp:BoundColumn>
            <asp:BoundColumn DataField="FAppTime" HeaderText="发证日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
            <asp:BoundColumn DataField="FEndTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="有效期">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    </form>
</body>
</html>
