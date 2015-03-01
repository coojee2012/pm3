<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpSelectEnt.aspx.cs" Inherits="Share_User_EmpSelectEnt" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc2" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <title>选择企业</title>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                统一用户管理（人员）
            </th>
        </tr>
        <tr>
            <td class="t_r">
                企业名称 ：
            </td>
            <td>
                <asp:TextBox ID="t_FEntName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_r">
                企业类型 ：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server" CssClass="m_txt" Width="130px">
                </asp:DropDownList>
            </td>
            <td rowspan="2" align="center">
                <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button3" type="reset" value="重置" class="m_btn_w2 bnts_left10" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                地区：
            </td>
            <td colspan="3">
                <uc2:Govdept ID="Govdept1" runat="server" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn Visible="false">
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="50px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FCompany" HeaderText="企业名称">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSystemId" HeaderText="企业类型"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="地区">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="选择">
                <ItemTemplate>
                    <a href="javascript:window.returnValue='<%# Eval("FBaseInfoId") %>';window.close()">
                        选择</a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
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
