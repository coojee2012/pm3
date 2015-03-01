<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegUserList.aspx.cs" Inherits="Share_User_RegUserList" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc2" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

            $("#t_FState").change(function() {
                isApp($(this).val());
            });
        });
        function isApp(str) {
            if (str == "1") {
                $("#div_IsApp").show();
            }
            else {
                $("#div_IsApp").hide();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                企业注册用户审核
            </th>
        </tr>
        <tr>
            <td class="t_r">
                企业名称：
            </td>
            <td>
                <asp:TextBox ID="t_FEntName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_r">
                注册时间：
            </td>
            <td>
                <asp:TextBox ID="t_CreateTime1" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="80px"></asp:TextBox>至
                <asp:TextBox ID="t_CreateTime2" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="80px"></asp:TextBox>
            </td>
            <td class="t_r">
                申请类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="新申请" Value="2"></asp:ListItem>
                    <asp:ListItem Text="增开系统" Value="8"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                状态：
            </td>
            <td>
                <div class="f_l">
                    <asp:DropDownList ID="t_FState" runat="server">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        <asp:ListItem Text="未审核" Value="0"></asp:ListItem>
                        <asp:ListItem Text="已审核" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="f_l" id="div_IsApp" style="display: none;">
                    审核结果：
                    <asp:DropDownList ID="t_FIsApp" runat="server">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不通过" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>
            <td class="t_r">
                申请系统权限：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server">
                </asp:DropDownList>
            </td>
            <td class="t_c" colspan="2">
                <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button3" type="reset" value="清空" class="m_btn_w2 bnts_left20" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
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
            <asp:BoundColumn DataField="FCompany" HeaderText="企业名称">
                <ItemStyle CssClass="t_l" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FType" HeaderText="申请类型">
                <HeaderStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FCreateTime" HeaderText="注册时间" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FManageDeptId" HeaderText="主管部门">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="申请系统权限">
                <ItemStyle CssClass="t_l" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FState" HeaderText="审核状态">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
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
