<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RYListCheck.aspx.cs" Inherits="WYDW_Common_RYListCheck" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员列表</title>
    <base target="_self" />
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });

        function CheckInfo() {
            return AutoCheckInfo();
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">人员列表
                </th>
            </tr>
            <tr>
                <td colspan="5">已备案项目名称：<asp:DropDownList ID="ddlXMMC" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlXMMC_SelectedIndexChanged">
                </asp:DropDownList>已备案人员姓名：<asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="60px"
                    MaxLength="30"></asp:TextBox><asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                    <asp:Button ID="btnDoImp" runat="server" Text="确定导入" class="m_btn_w4" OnClick="btnDoImp_Click" />
                    <input type="button" id="btnBack" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px; margin-bottom: 1px;"
            Width="98%" OnItemCommand="dg_List_ItemCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemStyle Width="25px" />
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

                <asp:BoundColumn HeaderText="姓名" DataField="fPersonName"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="性别" DataField="fSex">
                    <HeaderStyle Width="70px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="fCardType" HeaderText="证件类型">
                    <HeaderStyle Width="110px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="证件号码" DataField="fCardID">
                    <HeaderStyle Width="180px"></HeaderStyle>
                </asp:BoundColumn>

                <asp:BoundColumn HeaderText="所在项目" HeaderStyle-Width="100px" DataField="XMMC">
                    <HeaderStyle Width="130px"></HeaderStyle>

                    <ItemStyle CssClass="lh t_l" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="XMBH" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="T2CardID" Visible="False"></asp:BoundColumn>
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
        <!--当前项目编号-->
        <input type="hidden" id="hidCurXMBH" value="" runat="server" />
    </form>
</body>
</html>
