<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectEnt.aspx.cs" Inherits="Government_AppEntAction_SelectEnt" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

    <base target="_self" />
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                选择企业
            </th>
        </tr>
        <tr>
            <td class="t_r">
                企业名称：
            </td>
            <td class="txt34">
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
            </td>
            <td class="t_r">
                企业类别：
            </td>
            <td class="txt34">
                <asp:DropDownList ID="dbFSystemId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                所属管理部门：
            </td>
            <td class="txt34">
                <asp:DropDownList ID="dbMangeDept" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td class="txt32" colspan="2" align="center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_s" OnClick="btnQuery_Click"
                    Text="查询" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Ent_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" Style="margin-top: 7px" Width="100%" OnItemDataBound="Ent_List_ItemDataBound"
        OnItemCommand="Ent_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="40px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="企业名称" DataField="fname">
                <ItemStyle CssClass="t_l" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="联系人" DataField="flinkman">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="电话" DataField="ftel">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:ButtonColumn Text="选定" CommandName="Ok"></asp:ButtonColumn>
            <asp:BoundColumn DataField="FId" Visible="False" HeaderText="FId"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="100%" class="martop">
        <tr>
            <td>
                <uc2:pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
