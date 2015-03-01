<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpUserList.aspx.cs" Inherits="Share_User_EmpUserList"
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
            $("option[value^=-]").css("color", "#3DACE1");
        });
    </script>

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
                用户名：
            </td>
            <td>
                <asp:TextBox ID="t_FUserName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_r">
                姓名：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="65px"></asp:TextBox>
                用户类型：
                <asp:DropDownList ID="t_FSystemId" runat="server">
                    <asp:ListItem Value="" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1451" Text="审图机构人员"></asp:ListItem>
                    <asp:ListItem Value="1261" Text="见证单位人员"></asp:ListItem>
                    <asp:ListItem Value="1554" Text="勘察单位人员"></asp:ListItem>
                    <asp:ListItem Value="1553" Text="设计单位人员"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td rowspan="3" align="center">
                <asp:DropDownList ID="t_FState" runat="server" Visible="false">
                </asp:DropDownList>
                <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button3" type="reset" value="重置" class="m_btn_w2 bnts_left10" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                所属企业：
            </td>
            <td>
                <asp:TextBox ID="t_FEntName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_r">
                地区：
            </td>
            <td>
                <uc2:Govdept ID="Govdept1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                CA证书号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_r">
                办理状态：
            </td>
            <td>
                <asp:DropDownList ID="t_FCAState" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="0">未办理</asp:ListItem>
                    <asp:ListItem Value="1">已办理</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('EmpUserAdd.aspx?',550,450);"
                    runat="server" class="m_btn_w2" />
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" OnClick="btnDel_Click"
                    Text="删除" />
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 10px">
                &nbsp;
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
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FUserName" HeaderText="用户名">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="姓名">
                <ItemStyle CssClass="t_c" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSystemId" HeaderText="用户类型"></asp:BoundColumn>
            <asp:BoundColumn DataField="FEntName" HeaderText="所属单位">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FCertiNo" HeaderText="证书编号">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
             <asp:BoundColumn DataField="FCANumber" HeaderText="CA证书号">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
             <asp:BoundColumn DataField="FTel" HeaderText="联系电话">
                <ItemStyle HorizontalAlign="Left" />
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
