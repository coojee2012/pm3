<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActionList.aspx.cs" Inherits="Government_AppEntAction_ActionList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <base target="_self" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                不良行为标准(备选项)
            </th>
        </tr>
        <tr>
            <td class="t_r">
                单位类型
            </td>
            <td>
                <asp:DropDownList ID="dbFUnitTypeId" CssClass="m_txt" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="dbFUnitTypeId_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                行为类别
            </td>
            <td>
                <asp:DropDownList ID="dbFActionTypeId" CssClass="m_txt" runat="server">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_s" OnClick="btnQuery_Click" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r">
                <asp:Button ID="btnOk" runat="server" Text="选定" CssClass="m_btn_w2" OnClick="btnOk_Click" />
                <input id="btnReturn" class="m_btn_w2" type="button" value="返回" onclick="window.returnValue=0;window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Action_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="Action_List_ItemDataBound" Style="margin-top: 7px"
        Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FNumber" HeaderText="行为代码">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FPName" HeaderText="行为类别">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FDesc" HeaderText="行为描述">
                <ItemStyle Font-Underline="False" CssClass="padLeft" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FScore" HeaderText="分数">
                <ItemStyle Font-Underline="False" CssClass="padLeft" Width="50px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" class="martop" width="97%">
        <tr>
            <td style="height: 26px">
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    <input id="hiddle_IsSaveOk" runat="server" type="hidden" value="0" />
    </form>
</body>
</html>
