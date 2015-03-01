<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zWJTZ.aspx.cs" Inherits="EntSelf_MainSelf_zWJTZ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
            DynamicGrid(".m_dg1_i");
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table height="95%" width="98%" align="center">
        <tr>
            <td colspan="3" height="10px;">
            </td>
        </tr>
        <tr>
            <td class="wxts_top_l">
            </td>
            <td class="wxts_top">
            </td>
            <td class="wxts_top_r">
            </td>
        </tr>
        <tr>
            <td class="wxts_l">
            </td>
            <td valign="top">
                <div class="wxts_title">
                    文件通知
                </div>
                <asp:GridView ID="gv_List" runat="server" CssClass="m_dg1" Width="98%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" OnRowDataBound="gv_List_RowDataBound" DataKeyNames="FId"
                    EmptyDataText="您当前没有文件通知">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <%--<AlternatingRowStyle CssClass="m_dg1_h" />--%>
                    <EmptyDataRowStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemStyle CssClass="m_dg1_i" Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="lbautoid" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="通知标题">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <a title='<%#DataBinder.Eval(Container.DataItem, "FName")%>' target="_blank" href='FileDetails.aspx?fid=<%#DataBinder.Eval(Container.DataItem, "FID") %>'>
                                    <%#DataBinder.Eval(Container.DataItem, "FName")%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发布日期">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lbpubinfo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发布部门">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%#getDepartment( DataBinder.Eval(Container.DataItem, "FManageDeptId"), DataBinder.Eval(Container.DataItem, "FDepartment"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td class="wxts_r">
            </td>
        </tr>
        <tr>
            <td class="wxts_bot_l">
            </td>
            <td class="wxts_bot">
            </td>
            <td class="wxts_bot_r">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
