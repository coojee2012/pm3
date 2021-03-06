﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjRegistListBG.aspx.cs"
    Inherits="JSDW_appmain_PrjRegistListBG" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function CheckInfo() {
            return AutoCheckInfo();
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                设计变更
            </th>
        </tr>
        <tr>
            <td colspan="1" class="t_r">
                工程名称
            </td>
            <td colspan="4" class="t_l">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                工程项目变更情况
            </td>
            <td class="t_r">
                <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="showAddWindow('AddPrjRegistBG.aspx?e=1',900,700);" />
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" Visible="false" />
                <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px;
        margin-bottom: 1px;" Width="98%" OnItemCommand="dg_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn Visible="false">
                <ItemStyle Width="30px" />
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
            <asp:BoundColumn DataField="FPrjName" HeaderText="工程名称" ItemStyle-HorizontalAlign="Left">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FPrjNo" HeaderText="工程编号">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FAddressDept" HeaderText="工程地点">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FType" HeaderText="工程类别">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FBGTime" HeaderText="变更时间" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FBGDesc" HeaderText="变更原因">
                <ItemStyle CssClass="t_l" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="项目办理情况">
                <ItemTemplate>
                    <asp:LinkButton ID="btnLink" runat="server"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="变更前项目信息" Visible="false"></asp:BoundColumn>
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
        <br />
        <tt>注：项目进行变更，必须是项目做过业务，并且当前没有“办理中”的业务，任何时候均可进行变更，不限制次数。
            <br />
            &nbsp; &nbsp; 变更后的项目还未做业务，如果要修改项目信息，则直接去‘项目登记’页面修改信息即可。</tt>
    </div>
    </form>
</body>
</html>
