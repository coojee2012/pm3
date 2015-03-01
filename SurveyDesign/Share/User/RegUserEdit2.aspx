<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegUserEdit2.aspx.cs" Inherits="Share_User_RegUserEdit2" %>

<%@ Register Src="../../Common/govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业用户增开系统权限审核</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
            DynamicGrid(".m_dg1_i");
        });
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
                企业用户增开系统权限审核
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
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
                开通原由：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FLicencePic" runat="server" CssClass="m_txt" TextMode="MultiLine"
                    Height="68px" Width="213px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                申请时间：
            </td>
            <td class="m_txt_M">
                <asp:Literal ID="t_FCreateTime" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                状态：
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
    <input id="hidd_FRFID" runat="server" type="hidden" />
    <input id="t_FManageDeptId" runat="server" type="hidden" />
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
