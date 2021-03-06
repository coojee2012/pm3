﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackIdeasList.aspx.cs" Inherits="Admin_main_BackIdeasList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<head runat="server">
    <title>协同办公平台</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                打回意见维护
            </th>
        </tr>
        <td class="t_r">
            所属平台：
        </td>
        <td>
            <asp:DropDownList ID="t_FPlatId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FPlatId_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="t_r">
            所属系统：
        </td>
        <td>
            <asp:DropDownList ID="t_FSystemId" runat="server">
            </asp:DropDownList>
        </td>
        <td class="t_c">
            <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
            <input id="Button1" type="button" value="清空" class="m_btn_w2" onclick="clearQuery();"
                style="margin-left: 10px" />
        </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('BackIdeaAdd.aspx?e=0',460,290,$('#<%=btnReload.ClientID %>'));"
                    class="m_btn_w2" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Holidays_List" runat="server" HorizontalAlign="Center" Width="100%"
        CssClass="m_dg1" Style="margin-top: 4px;" AutoGenerateColumns="False" OnItemDataBound="Holidays_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="30" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="50" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FType" HeaderText="类型">
                <ItemStyle HorizontalAlign="center" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FContent" HeaderText="内容">
                <ItemStyle HorizontalAlign="Left" Wrap="true" Width="500px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FTime" HeaderText="添加时间">
                <ItemStyle HorizontalAlign="center" />
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
