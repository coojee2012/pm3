<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Baseinfo4.aspx.cs" Inherits="KcsjSgt_main_Baseinfo3" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                非注册人员信息
            </td>
            <td class="t_r">
                <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="showAddWindow('AddEmpInfo2.aspx?e=1',600,400);"
                    runat="server" />
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                人员姓名：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="118px"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                身份证号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FIdCard" runat="server" CssClass="m_txt" Width="150px" MaxLength="30"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                状态：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlFState" runat="server">
                    <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未上报"></asp:ListItem>
                    <asp:ListItem Value="1" Text="已上报"></asp:ListItem>
                    <asp:ListItem Value="2" Text="打回"></asp:ListItem>
                    <asp:ListItem Value="6" Text="已审核"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_c">
                <asp:Button ID="btnReload" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload3_Click" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Emp_List2" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="Emp_List2_ItemDataBound" Style="margin-top: 3px;
        margin-bottom: 1px;" Width="98%" OnItemCommand="Emp_List2_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <HeaderStyle Width="30px" />
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
            <asp:BoundColumn DataField="FName" HeaderText="姓名"></asp:BoundColumn>
            <asp:BoundColumn DataField="FIdCard" HeaderText="身份证号码"></asp:BoundColumn>
            <asp:BoundColumn DataField="FRegistSpecialId" HeaderText="从事专业"></asp:BoundColumn>
            <asp:BoundColumn DataField="FUserName" HeaderText="卡号"></asp:BoundColumn>
            <asp:BoundColumn DataField="FPassWord" HeaderText="密码" Visible="false"></asp:BoundColumn>
            <asp:BoundColumn DataField="FState" HeaderText="状态"></asp:BoundColumn>
            <asp:ButtonColumn HeaderText="操作" Text="上报" CommandName="Report"></asp:ButtonColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="Pager2" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager2_PageChanging"
            PageIndexBoxType="TextBox" PageSize="25" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <font style="color:red;font-size:13px;">注：上报管理部门审核后，才能在项目中进行选择和使用！</font>
    </div>
    </form>
</body>
</html>
