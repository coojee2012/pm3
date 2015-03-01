<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectEnt.aspx.cs" Inherits="Share_User_SelectEnt" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择企业</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
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
                选择企业
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                企业名称：
            </td>
            <td>
                <asp:TextBox ID="t_FCompany" runat="server" CssClass="m_txt" Width="292px"></asp:TextBox>
            </td>
            <td style="text-align: center" rowspan="2">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="查询" OnClick="btnQuery_Click" />
                <input type="reset" value="重置" class="m_btn_w2 bnts_left10" />
            </td>
        </tr>
      <%--  <tr>
            <td class="t_r t_bg">
                组织机构代码：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="104px"></asp:TextBox>
            </td>
        </tr>--%>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound" OnItemCommand="DG_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" Wrap="False" />
        <ItemStyle CssClass="m_dg1_i" Wrap="False" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="企业名称" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <%#Eval("FCompany")%>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FAddress" HeaderText="地址" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
            <%--            <asp:BoundColumn DataField="FSystemId" HeaderText="系统类型"></asp:BoundColumn>--%>
            <asp:TemplateColumn>
                <ItemStyle Width="60px" Font-Underline="False" Wrap="False" />
                <ItemTemplate>
                    <asp:Button ID="btnSelect" CommandName="cnSelect" CommandArgument='<%#Eval("FID") %>'
                        runat="server" Text="选取" CssClass="m_btn_w2" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
   <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
    </div>
    <tt>提示：请输入企业名称后再查询</tt>
    </form>
</body>
</html>
