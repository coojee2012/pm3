﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BadActionList.aspx.cs" Inherits="Admin_main_BadActionList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>不良行为维护</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
             DynamicGrid(); //列表光标移动效果
        });       
    </script>

    <script language="javascript">
    function clearQuery()
    {
        document.getElementById("txtFname").value="";
        document.getElementById("txtFNumber").value="";
        
    }
    </script>

    <meta http-equiv="x-ua-compatible" content="ie=7" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                不良行为维护
            </th>
        </tr>
        <tr>
            <td class="t_r">
                行为类别：
            </td>
            <td>
                <asp:TextBox ID="txtFname" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
            <td class="t_r">
                行为代码：
            </td>
            <td>
                <asp:TextBox ID="txtFNumber" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
            <td align="center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" class="m_btn_w2" onclick="clearQuery();" />
            </td>
        </tr>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m">
                    <input id="Submit1" type="button" value="新增" onclick="showApproveWindow('BadActionAdd.aspx?e=0&fparentid=<%= Request["fparentid"] %>',500,600);"
                        class="m_btn_w2" />
                    <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
                    <asp:Button ID="btnReturn" runat="server" CssClass="m_btn_w2" Text="返回" OnClick="btnReturn_Click" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
    </table>
    <asp:DataGrid ID="BadActioin_List" runat="server" HorizontalAlign="Center" Width="98%"
        CssClass="m_dg1" Style="margin-top: 7px;" AutoGenerateColumns="False" OnItemDataBound="BadActioin_List_ItemDataBound"
        OnItemCommand="BadActioin_List_ItemCommand">
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
            <asp:BoundColumn DataField="" HeaderText="单位类型">
                <ItemStyle Width="150px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="fparentName" HeaderText="行为类别">
                <ItemStyle Width="100px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="fnumber" HeaderText="行为代码">
                <ItemStyle Width="70px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FDesc" HeaderText="行为描述"></asp:BoundColumn>
            <asp:BoundColumn DataField="FScore" HeaderText="分数">
                <ItemStyle Width="30px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="顺序">
                <ItemStyle Width="95" />
                <ItemTemplate>
                    <asp:TextBox ID="FOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FOrder") %>'
                        Width="80" CssClass="cTextBox1" onblur="isInt(this);"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="保存">
                <ItemStyle Width="60" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnItemSave" CssClass="link3" runat="server" CommandName="Save"
                        Text="保存"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="fparentnumber" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="fparentid" Visible="False"></asp:BoundColumn>
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
