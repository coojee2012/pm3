<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Baseinfo2.aspx.cs" Inherits="KcsjSgt_main_Baseinfo2" %>

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
                注册人员信息
            </td>
            <td class="t_r">
                
                <asp:Button ID="btnReload" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
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
                <asp:TextBox ID="t_FIdCard" runat="server" CssClass="m_txt" Width="224px" MaxLength="30"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Emp_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="Emp_List_ItemDataBound" Style="margin-top: 3px;
        margin-bottom: 1px;" Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn Visible="false">
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
            <asp:BoundColumn DataField="FCertiNo" HeaderText="证书编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="FEndTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="有效期">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FUserName" HeaderText="用户名"></asp:BoundColumn>
            <asp:BoundColumn DataField="FPassWord" HeaderText="密码"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            PageIndexBoxType="TextBox" PageSize="25" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
    </div>

    </form>
</body>
</html>
