﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpBulletin.aspx.cs" Inherits="OA_Bulletin_UpBulletin" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公告信息查询</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
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
        function clearQuery()
    {
        document.getElementById("txtFTitle").value = "";
        document.getElementById("textFType").value = "";
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
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
            <td class="t_r">
                公告类型：
            </td>
            <td align="left">
                <asp:DropDownList ID="textFType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />&nbsp;
                <input id="btnClear" type="button" value="清空" class="m_btn_w2" onclick="clearQuery();"
                    size="" />&nbsp;
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />&nbsp;
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:GridView ID="BulList" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        EmptyDataText="没有公告记录" HorizontalAlign="Center" OnRowDataBound="BulList_RowDataBound"
        Width="100%" Style="margin-top: 6px;">
        <RowStyle CssClass="m_dg1_i" />
        <HeaderStyle CssClass="m_dg1_h" />
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
            <%--<asp:TemplateField HeaderText="序号">
                    <ItemStyle Width="50px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            <asp:BoundField DataField="fname" HeaderText="公告发布者">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Width="100px" Wrap="False" />
                <HeaderStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Ftitle" HeaderText="公告标题">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="TypeName" HeaderText="公告类型">
                <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="FDateOn" HeaderText="公告时间" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="FID" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="LItable_end" Font-Size="X-Small" HorizontalAlign="Right" />
        <EmptyDataRowStyle Font-Bold="True" Font-Size="10pt" ForeColor="Desktop" />
    </asp:GridView>
    </div>
    <table align="center" class="DataListTableBottom01" width="100%">
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
