<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResUserList.aspx.cs" Inherits="Admin_User_UserList"
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
                公共资源交换平台用户
            </th>
        </tr>
        <tr>
            <td class="t_r">
                用&nbsp;户&nbsp;名：
            </td>
            <td>
                <asp:TextBox ID="text_FName" runat="server" CssClass="m_txt" Width="139px"></asp:TextBox>
            </td>
            <td class="t_r">
                管理部门：
            </td>
            <td>
                <uc2:Govdept ID="Govdept1" runat="server" />
            </td>
            <td class="t_c" style="width: 100px" rowspan="4">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <br />
                <br />
                <input id="Button3" type="reset" value="清空" class="m_btn_w2" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                加密锁硬件编号：
            </td>
            <td>
                <asp:TextBox ID="text_FLockNumber" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_r">
                加密锁标签编号：
            </td>
            <td>
                <asp:TextBox ID="text_FLockLabelNumber" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                有&nbsp;效&nbsp;期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="text_FEndTime" runat="server" CssClass="m_txt" Width="80px" onfocus="WdatePicker()"></asp:TextBox>
                至
                <asp:TextBox ID="text_FEndTime1" runat="server" CssClass="m_txt" Width="80px" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('ResUserAdd.aspx?type=<%=Request.QueryString["ftype"] %>',680,500);"
                    class="m_btn_w2" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" OnClick="btnDel_Click1"
                    Text="删除" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="User_List" runat="server" HorizontalAlign="Center" Width="98%"
        CssClass="m_dg1" AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound">
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
            <asp:BoundColumn DataField="FName" HeaderText="用户名">
                <ItemStyle CssClass="t_l" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FLockLabelNumber" HeaderText="加密锁标签编号"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="加密锁硬件编号" Visible="false">
                <ItemTemplate>
                    <asp:TextBox ID="tFLockNumber" Text='<%# DataBinder.Eval(Container.DataItem,"FLockNumber") %>'
                        CssClass='m_txt' runat="server" Width="95"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="角色" Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FManageDeptId" HeaderText="管理部门">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FEndTime" HeaderText="有效期" DataFormatString="{0:yyyy-MM-dd}"
                Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FLinkMan" HeaderText="管理人">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FTel" HeaderText="联系电话">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FBatchId" HeaderText="批次" ReadOnly="True" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:TemplateColumn Visible="false">
                <ItemStyle />
                <ItemTemplate>
                    <asp:Button ID="btnItemSave" runat="server" CssClass="m_btn_w2" Text="保存" CommandName="Save" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FType" Visible="False"></asp:BoundColumn>
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
