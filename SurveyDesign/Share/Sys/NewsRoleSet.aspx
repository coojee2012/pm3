<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsRoleSet.aspx.cs" Inherits="Admin_main_NewsRoleSet" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>协同办公平台</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
            document.getElementById("text_FNumber").value = "";
            document.getElementById("drop_FSystemId").value = "";
        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });

        function clearQuery() {
            document.getElementById("dbColName").value = "";
        }

        function checkBoxSelect(fParent, arrayIds) {
            var Ids = arrayIds.split("|");
            if (Ids == null || Ids.length == 0) {
                return;
            }
            else {

                for (var i = 0; i < Ids.length - 1; i++) {

                    document.getElementById(Ids[i]).checked = fParent.checked;
                }
            }
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table class="m_table" width="100%" align="center">
        <tr>
            <td class="t_r t_bg">
                栏目名称：
            </td>
            <td>
                <asp:DropDownList ID="dbColName" runat="server" CssClass="cTextBox1" Width="130px">
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />&nbsp;
                <input id="btnClear" type="button" value="清空" class="m_btn_w2" onclick="clearQuery();" />&nbsp;
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="PubRoleSet_List" runat="server" HorizontalAlign="Center" Width="100%"
        CssClass="m_dg1" Style="margin-top: 4px;" AutoGenerateColumns="False" OnItemDataBound="PubRoleSet_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <AlternatingItemStyle CssClass="cGridAlterItem1" />
        <Columns>
            <asp:BoundColumn DataField="FLevel" Visible="False"></asp:BoundColumn>
            <asp:TemplateColumn>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="栏目名称">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FNumber" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FSId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="100%" class=" cMarTop">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
