<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SBCLXX.aspx.cs" Inherits="OA_Bulletin_SBCLXX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        function clearPage() {
            $("#text_FName").val('');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                <asp:Literal ID="lPostion" runat="server">企业文件通知</asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                文件标题：
            </td>
            <td>
                <asp:TextBox ID="text_FName" runat="server" CssClass="m_txt" Width="204px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" style="margin-left: 10px;" class="m_btn_w2"
                    onclick="clearPage();" />
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                &nbsp;<asp:Button ID="btnDel1" runat="server" CssClass="m_btn_w2" Text=" 删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text=" 刷新" OnClick="btnReload_Click" />&nbsp;&nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="News_List" runat="server" HorizontalAlign="Center" Width="100%"
        CssClass="m_dg1" AutoGenerateColumns="False" OnItemDataBound="News_List_ItemDataBound"
        OnItemCommand="News_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="20" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="标题">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FCreateTime" HeaderText="申请日期" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" Width="80px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FPubTime" HeaderText="发布日期" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" Width="80px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="已经发布的栏目" Visible="false">
                <ItemStyle Width="200px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="是否发布">
                <ItemStyle Font-Underline="False" Wrap="False"  Width="80px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="IsPub" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="顺序">
                <ItemStyle Width="88" />
                <ItemTemplate>
                    <asp:TextBox ID="Forder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FOrder") %>'
                        Width="80" CssClass="m_txt"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="查看评论" Visible="false">
                <ItemStyle Width="60px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="发布栏目" Visible="false">
                <ItemStyle Width="60px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="保存">
                <ItemStyle Width="30" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnItemSave" CssClass="link3" runat="server" CommandName="Save"
                        Text="保存"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="100%" style="margin-top: 5px;">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
