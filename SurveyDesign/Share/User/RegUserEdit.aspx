<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegUserEdit.aspx.cs" Inherits="Share_User_RegUserEdit" %>

<%@ Register Src="../../Common/govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业注册用户审核</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        function CheckInfo() {
            return AutoCheckInfo();
        }        
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业注册用户审核
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <input class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                企业类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                企业名称：
            </td>
            <td>
                <asp:TextBox ID="t_FCompany" runat="server" CssClass="m_txt" Width="225px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                组织机构代码：
            </td>
            <td>
                <asp:TextBox ID="t_FJuridcialCode" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                营业执照号：
            </td>
            <td>
                <asp:TextBox ID="t_FLicence" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                密码：
            </td>
            <td>
                <asp:TextBox ID="t_FPassWord" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                主管部门：
            </td>
            <td>
                <uc1:Govdept ID="Govdept1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审核状态：
            </td>
            <td>
                <asp:Literal ID="lit_FState" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                用户申请开通的系统权限↓
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w4" Text="审核" OnClick="btnSave_Click" />
                &nbsp;<asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:GridView ID="DG_Rights" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" Style="margin-top: 4px" Width="98%" EmptyDataText="用有可以审核的项"
        OnRowDataBound="DG_Rights_RowDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <RowStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateField>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="序号">
                <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="系统类型">
                <ItemTemplate>
                    <%#(Eval("FName"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FIsApp" HeaderText="审核状态" />
            <asp:TemplateField HeaderText="是否通过">
                <ItemStyle Width="100px" />
                <ItemTemplate>
                    <asp:DropDownList ID="drop_State" runat="server">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        <asp:ListItem Text="通过" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="不通过" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FNumber" Visible="False"></asp:BoundField>
            <asp:BoundField DataField="FId" Visible="False"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <input id="t_FManageDeptId" runat="server" type="hidden" />
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
