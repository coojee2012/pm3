<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JSEntList.aspx.cs" Inherits="Government_EntData_JSEntList" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script>
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });

        function openWinNew(Url) {
            var rid = Math.random();
            var newopen = window.open(Url + "&rid=" + rid, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no");
            if (newopen && newopen != null) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }
    </script>

</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="lTitle" runat="server" Text="企业总库"></asp:Label>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                企业名称：
            </td>
            <td class="txt34">
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
            <td class="t_r" nowrap="nowrap" style="width: 128px">
                所属地区：
            </td>
            <td>
                <div nowrap="nowrap">
                    <asp:DropDownList ID="dbMangeDept" runat="server" CssClass="m_txt" Width="102px">
                    </asp:DropDownList>
                    &nbsp;</div>
            </td>
            <td rowspan="1" colspan="1" style="text-align: center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                    Text="查询" />
                <input type="button" value="重置" onclick="clearPage();" class="m_btn_w2" />
                <asp:Button ID="btnOut" runat="server" CssClass="m_btn_w2" OnClick="btnOut_Click"
                    Text="导出" />
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar" id="td_ZX" style="display: none"
        runat="server">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Ent_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" Style="margin-top: 7px" Width="100%" OnItemDataBound="Ent_List_ItemDataBound"
        OnItemCommand="Ent_List_ItemCommand">
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
                <ItemStyle Font-Underline="False" Wrap="False" Width="50px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="单位名称" DataField="fname">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FRegistDeptId" HeaderText="所属地区"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="注册地址">
                <ItemTemplate>
                    <%#Eval("FRegistAddress")%>
                </ItemTemplate>
                <ItemStyle CssClass="t_l" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="组织机构" DataField="FJuridcialCode" />
            <asp:BoundColumn HeaderText="资质等级" />
            <asp:TemplateColumn HeaderText="联系人">
                <ItemTemplate>
                    <%#Eval("FLinkMan") %>
                </ItemTemplate>
                <ItemStyle CssClass="padLeft" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="联系人手机">
                <ItemTemplate>
                    <%#Eval("FMobile")%>
                </ItemTemplate>
                <ItemStyle CssClass="padLeft" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="企业状态" Visible="false">
                <ItemTemplate>
                    <asp:DropDownList ID="FState" runat="server" CssClass="m_txt" Width="80px">
                        <asp:ListItem Text="请选择" Value=""> </asp:ListItem>
                        <asp:ListItem Text="正常" Value="2"> </asp:ListItem>
                        <asp:ListItem Text="注销" Value="5"> </asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal ID="lit_FState" runat="server"></asp:Literal>
                </ItemTemplate>
                <HeaderStyle Wrap="False" />
                <ItemStyle Wrap="False" Width="100px" />
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="工程业绩"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" HeaderText="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" class="table1 txt33" width="100%">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
