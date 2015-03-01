<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZGXMList.aspx.cs" Inherits="WYDW_ZGXM_ZGXMList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="~/common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在管项目</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">在管项目列表
                </th>
            </tr>
        </table>
        <%--<table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r">项目名称：
                </td>
                <td>
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                </td>
                <td class="t_r">项目属地：
                </td>
                <td>
                    <uc1:govdeptid ID="t_AreaID" runat="server" />
                </td>
                <td rowspan="2" style="text-align: center; padding-right: 10px">
                    <asp:Button ID="Button2" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                </td>
            </tr>
            <tr>
                <td class="t_r">开发单位：
                </td>
                <td>
                    <asp:TextBox ID="t_KFDW" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                </td>
                <td class="t_r">项目类型：
                </td>
                <td>
                    <asp:DropDownList ID="t_XMLX" runat="server" Width="200px" CssClass="m_txt" OnSelectedIndexChanged="t_ProjectType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>--%>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td>单项工程情况
                </td>
                <td class="t_r">

                    <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px; margin-bottom: 1px;"
            Width="98%" OnItemCommand="dg_List_ItemCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
               <%-- <asp:BoundColumn DataField="XMMC" HeaderText="项目名称">
                    <ItemStyle Wrap="false" />
                </asp:BoundColumn>--%>
                <asp:TemplateColumn HeaderText="项目名称">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnMC" runat="server" CommandName="See" Text='<%#Eval("XMMC") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle CssClass="t_l" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="项目属地">
                    <ItemStyle Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JSDW" HeaderText="建设单位">
                    <ItemStyle Wrap="false" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="项目类型">
                    <ItemStyle Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XMBH" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="XMSD" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="XMLX" Visible="False"></asp:BoundColumn>
                <%--<asp:BoundColumn DataField="PrjItemType" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ConstrType" Visible="False"></asp:BoundColumn>--%>
            </Columns>
        </asp:DataGrid>
        <div style="padding-left: 1%">
            <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            <br />
            <tt>注：</tt>
        </div>
    </form>
</body>
</html>
