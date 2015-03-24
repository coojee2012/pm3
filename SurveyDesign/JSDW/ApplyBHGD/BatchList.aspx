<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchList.aspx.cs" Inherits="Government_AppBHGD_BatchList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>批次管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">批次管理</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">年度
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txt_Year"></asp:TextBox>
                </td>
                <td align="center" rowspan="3" colspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" />
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w4" Text="添加批次" />
                    <asp:Button runat="server" ID="btn_Edit" CssClass="m_btn_w4" Text="编辑批次" />
                    <asp:Button ID="btn_Del" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w4"
                        Text="删除批次" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:DataGrid ID="gv_list" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                    HorizontalAlign="Center" Width="98%">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <ItemStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemStyle Width="20px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn HeaderText="序号">
                            <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="工程名称" DataField="ProjectName">
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="申报单位" >
                             <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                         <asp:BoundColumn HeaderText="年度" >
                             <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="批次" >
                             <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="审批环节" >
                             <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                  <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
                </webdiyer:AspNetPager>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
