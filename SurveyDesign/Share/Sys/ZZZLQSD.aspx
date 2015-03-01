<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZZZLQSD.aspx.cs" Inherits="Share_Sys_ZZZLQSD" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script language="javascript">
      
        function clearQuery() {
            document.getElementById("t_FTxt").value = "";

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title" id="QueryTable1">
        <tr>
            <th colspan="8">
                纸质资料签收单
            </th>
        </tr>
        <tr>
            <td>
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="t_FTxt" CssClass="m_txt" MaxLength="20"></asp:TextBox>
            </td>
            <td colspan="4">
                <asp:Button runat="server" ID="btnSearch" Text="收索" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" style="margin-left: 10px;" class="m_btn_w2"
                    onclick="clearQuery();" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_title" id="QueryTable2">
        <tr>
            <td>
                查询列表
            </td>
            <td class="m_bar_m t_r">
                <input id="btnAdd" type="button" value="新增" class="m_btn_w2" onclick="showAddWindow('ZzzlQsdADD.aspx?e=0',800,500);" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w6" Text="删除" 
                    onclick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" 
                    onclick="btnReload_Click" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
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
            <asp:BoundColumn DataField="FPrjName" HeaderText="工程名称"></asp:BoundColumn>
            <asp:BoundColumn DataField="FJSDW" HeaderText="建设单位"></asp:BoundColumn>
            <asp:BoundColumn DataField="FKCDW" HeaderText="勘察单位"></asp:BoundColumn>
            <asp:BoundColumn DataField="FSJDW" HeaderText="设计单位"></asp:BoundColumn>
            <asp:BoundColumn DataField="FPrjPerson" HeaderText="项目负责人"></asp:BoundColumn>
            <asp:BoundColumn DataField="FSignPerson" HeaderText="签收人"></asp:BoundColumn>
            <asp:BoundColumn DataField="fid" Visible="false"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="98%" style="margin-top: 5px;">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
