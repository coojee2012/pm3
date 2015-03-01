<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectMType.aspx.cs" Inherits="Share_SysSet_SelectMType" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择附件</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="3">
                选择关联业务
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属业务系统：
            </td>
            <td>
                <asp:DropDownList ID="drop_FSystemId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td class="t_c" rowspan="2">
                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="m_btn_w2" />
                <input type="reset" class="m_btn_w2 " value="清空" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                编码：
            </td>
            <td>
                <asp:TextBox ID="text_FNumber" runat="server" CssClass="m_txt" Width="60"></asp:TextBox>
                类型编码：
                <asp:TextBox ID="text_FMTypeId" runat="server" CssClass="m_txt" Width="60"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r">
                <asp:Button ID="Button1" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                <input type="button" class="m_btn_w2 " value="返回" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" Width="98%" CssClass="m_dg1" HorizontalAlign="Center"
        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound" 
        onitemcommand="DG_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="业务名称">
                <ItemStyle HorizontalAlign="Left" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FNumber" HeaderText="编号">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FMTypeId" HeaderText="类型编号">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSystemName" HeaderText="所属业务系统"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="操作">
                <ItemStyle Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Literal ID="lit_FMType" runat="server"></asp:Literal>
                    <asp:Button ID="btnFMType" CommandName="cnFMType" CommandArgument='<%#Eval("FNumber") %>'
                        runat="server" Text="选择" CssClass="m_btn_w2" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table width="98%" align="center">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
