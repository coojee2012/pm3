﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LiftList.aspx.cs" Inherits="JSDW_ApplyAQJDBA_LiftList" %>

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

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function addList() {
            var fid = '<%=ViewState["FAppId"] %>';
            if (fid == null || fid == '') {
                return;
            }
            showAddWindow('Lift.aspx?fAppId=' + fid, 800, 550);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                起重设备
            </th>
        </tr>
        <tr>
            <td colspan="5" class="t_l">
                设备名称：
                <asp:TextBox ID="txtSBMC" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
                设备备案编号：
                <asp:TextBox ID="txtBABH" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                起重设备情况
            </td>
            <td class="t_r">
                <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="addList();" />
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px;
        margin-bottom: 1px;" Width="98%" >
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
                <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="SBMC" HeaderText="设备名称">
                <ItemStyle Wrap="false" HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="BABH" HeaderText="设备备案编号" ItemStyle-HorizontalAlign="Left">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="SBXH" HeaderText="设备型号">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="SYDW" HeaderText="使用单位">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="AZDW" HeaderText="安装单位">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>           
            <asp:BoundColumn DataField="FSBId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FAppId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" 
            PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <br />
        <tt>注：</tt>
    </div>
    </form>
</body>
</html>
