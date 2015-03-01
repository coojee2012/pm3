<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessList.aspx.cs" Inherits="Admin_main_ProcessList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
            document.getElementById("text_FNumber").value = "";
            document.getElementById("drop_FSystem").value = "";
        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

    <meta http-equiv="x-ua-compatible" content="ie=7" />
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                流程维护
            </th>
        </tr>
        <tr>
            <td class="t_r">
                流程名称：
            </td>
            <td>
                <asp:TextBox ID="text_FName" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_r">
                编码：
            </td>
            <td>
                <asp:TextBox ID="text_FNumber" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
            <td class="t_r">
                所属系统
            </td>
            <td>
                <asp:DropDownList ID="drop_FSystem" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td style="text-align: center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" class="m_btn_w2" onclick="clearQuery();"
                    style="margin-left: 10px" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="btnAdd" class="m_btn_w2" type="button" value="新增" onclick="showAddWindow('ProcessAdd.aspx?e=0&fmatypeid=<%=Request["fmatypeid"] %>',900,750);" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Process_List" runat="server" HorizontalAlign="Center" Width="98%"
        CssClass="m_dg1" AutoGenerateColumns="False" OnItemDataBound="Process_List_ItemDataBound">
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
                <ItemStyle Width="50px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="流程名称">
                <ItemStyle CssClass="padLeft" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FFullName" HeaderText="流程全称">
                <ItemStyle CssClass="padLeft" />
                <ItemStyle Width="150px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FNumber" HeaderText="编码">
                <ItemStyle Width="60px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSystemName" HeaderText="所属系统">
                <ItemStyle Width="100px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FDefineDay" HeaderText="标准时间定义">
                <ItemStyle Width="80px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FManageDeptName" HeaderText="管理部门">
                <ItemStyle Width="80px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="包含的资质等级" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="包含的业务" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="包含的专业" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="维护子流程">
                <ItemStyle Width="80px" />
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
