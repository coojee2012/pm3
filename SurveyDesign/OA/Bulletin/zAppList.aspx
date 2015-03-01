﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zAppList.aspx.cs" Inherits="OA_Bulletin_zAppList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>通知公告管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <link href="../style/Grid.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../../script/default.js"> 

    </script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });       
    </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("txtFTitle").value = "";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                公告信息查询
            </th>
        </tr>
        <tr>
            <td class="t_r">
                公告标题：
            </td>
            <td align="left">
                <asp:TextBox ID="txtFTitle" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="查询" OnClick="btnQuery_Click" />
                <input id="btnClear" type="button" value="清空" class="m_btn_w2" onclick="clearQuery();" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r" style="padding-right: 20px;">
                <input type="button" class="m_btn_w2" value="新增" onclick="showAddWindow('zAdd.aspx?',800,500);" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:GridView ID="BulList" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        EmptyDataText="暂时没有" HorizontalAlign="Center" OnRowDataBound="BulList_RowDataBound"
        Width="98%" Style="margin-top: 6px;" DataKeyNames="FID">
        <RowStyle CssClass="m_dg1_i" />
        <HeaderStyle CssClass="m_dg1_h" />
        <EmptyDataRowStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateField>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="序号">
                <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="Ftitle" HeaderText="公告标题">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="FState" HeaderText="是否发布">
                <ItemStyle Width="80" />
            </asp:BoundField>
            <asp:BoundField DataField="FID" Visible="false" />
        </Columns>
    </asp:GridView>
    <table align="center" width="98%">
        <tr>
            <td width="10">
            </td>
            <td>
                <uc1:pager ID="Pager1" runat="server" />
            </td>
            <td width="10">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
