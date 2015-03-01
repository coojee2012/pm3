<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysUserList.aspx.cs" Inherits="Share_User_ManUserList"
    EnableEventValidation="false" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc2" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

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
            <th colspan="5">
                系统管理员维护
            </th>
        </tr>
        <tr>
            <td class="t_r">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="t_FEntName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_r">
                管理部门：
            </td>
            <td>
                <uc2:Govdept ID="Govdept1" runat="server" />
            </td>
            <td class="t_c" style="width: 200px" rowspan="2">
                <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button3" type="reset" value="清空" class="m_btn_w2 bnts_left20" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                加密锁编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_r">
                角色：
            </td>
            <td>
                <asp:DropDownList ID="t_FMenuRoleId" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('SysUserAdd.aspx?fmatypeid=<%=Request.QueryString["fmatypeid"] %>',700,500);"
                    class="m_btn_w2" />
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" OnClick="btnDel_Click"
                    Text="删除" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
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
                <ItemStyle Width="50px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="用户名">
                <ItemStyle CssClass="t_l" />
                <ItemTemplate>
                    <a href="javascript:showAddWindow('SysUserAdd.aspx?FID=<%#Eval("FID") %>&fmatypeid=<%=Request.QueryString["fmatypeid"] %> ',700,500);">
                        <%#Eval("FName") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FLockNumber" HeaderText="加密锁硬件编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="FMenuRoleId" HeaderText="角色"></asp:BoundColumn>
            <asp:BoundColumn DataField="FManageDeptId" HeaderText="管理部门"></asp:BoundColumn>
            <asp:BoundColumn DataField="FCompany" HeaderText="所在单位">
                <ItemStyle CssClass="t_l" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FTel" HeaderText="联系电话"></asp:BoundColumn>
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
