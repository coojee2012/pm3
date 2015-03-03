<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KCUnitForm.aspx.cs" Inherits="JSDW_ApplyJGYS_KCUnitForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
      <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript">
        function ComanyAdd() {
            var url = "CompanyAdd.aspx?JG_Id=" + $("#hfId").val() + "&typeId=4&typeName=勘察单位";
            var result = showWinByReturn(url, 800, 500);
            //if (result != undefined) {
            //    //$("#hfPerSonId").val(result);
            //    return true;
            //}
            return true;
        }
        function ComanyEdit(companyId) {
            var url = "CompanyEdit.aspx?JG_Id=" + $("#hfId").val() + "&companyId=" + companyId + "&typeId=4&typeName=勘察单位";
            var result = showWinByReturn(url, 800, 500);
            document.getElementById("btnRefresh").click();
            //if (result != undefined) {
                //$("#hfPerSonId").val(result);
               // return true;
           // }
           // return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfId" runat="server" />
        <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
               勘察单位
            </th>
        </tr>
    </table>
        <table width="98%" align="center" class="m_table">
            <tr>
                <td width="100%" align="right">
                   <asp:Button ID="btnRefresh" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnRefresh_Click" />
                   点击<asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="m_btn_w2" OnClick="btnAdd_Click" OnClientClick="return ComanyAdd()" />按钮，添加该企业
                </td>
            </tr>
        </table>
         <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
            AutoGenerateColumns="False" ItemStyle-Wrap="true" OnItemDataBound="DG_List_ItemDataBound" OnItemCommand="DG_List_ItemCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
                <Columns>
                    <asp:BoundColumn HeaderText="序号">
                        <ItemStyle Width="50px" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="单位名称">
                        <ItemTemplate>
                            <a href="javascript:void(0)" onclick="ComanyEdit('<%#Eval("ID") %>')"><%#Eval("DWMC") %></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn HeaderText="法定代表人" DataField="FDDBR">
                        <ItemStyle CssClass="t_l" Wrap="false" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="资质项" DataField="ZZX" HeaderStyle-Width="40%" HeaderStyle-Wrap="true">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="联系人" DataField="LXR" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <%--<asp:BoundColumn HeaderText="状态" DataField="State" HeaderStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>--%>
                    <asp:TemplateColumn HeaderText="操作">
                        <ItemTemplate>
                           <asp:LinkButton ID="lbDel" runat="server" Text="删除" OnClientClick="return confirm('确认删除?')" CommandName="DEL"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </form>
</body>
</html>
