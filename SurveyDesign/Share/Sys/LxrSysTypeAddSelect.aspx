<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LxrSysTypeAddSelect.aspx.cs"
    Inherits="Admin_User_LxrSysTypeAddSelect" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc2" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
    <title>选择人员</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" language="javascript">
        function clearQuery() {
            document.getElementById("FLinkName").value = "";
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Literal ID="Literal1" runat="server">管理部门用户</asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="text_FName" runat="server" CssClass="m_txt" Width="139px"></asp:TextBox>
            </td>
            <td class="t_r">
                管理部门：
            </td>
            <td>
                <uc2:Govdept ID="Govdept1" runat="server" />
            </td>
            <td rowspan="2" class="t_c" align="center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_s" Text="搜索" OnClick="btnQuery_Click" />
                <br />
                <br />
                <input type="reset" value="清空" class="m_btn_s" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                硬件编号：
            </td>
            <td>
                <asp:TextBox ID="text_FLockNumber" runat="server" CssClass="m_txt"></asp:TextBox>
                <input type="button" value="读锁" onclick="readLock('text_FLockNumber');" class="m_btn_w2" />
            </td>
            <td class="t_r">
                标签编号：
            </td>
            <td>
                <asp:TextBox ID="txtLockLabelNumber" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right">
                &nbsp;&nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="EntInfo_List" runat="server" HorizontalAlign="Center" Width="98%"
        CssClass="m_dg1" AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" Wrap="False" />
        <ItemStyle CssClass="m_dg1_i" Wrap="False" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="用户名称"></asp:BoundColumn>
            <asp:BoundColumn DataField="FLinkMan" HeaderText="审批人">
                <ItemStyle CssClass="t_l" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FManageDeptId" HeaderText="管理部门"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="选择">
                <ItemTemplate>
                    <a href="javascript:window.returnValue='<%# Eval("FID") %>';window.close()">选 择</a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="100%" style="margin-top: 5px;">
        <tr>
            <td style="height: 30px">
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    &nbsp;&nbsp;
    </form>
</body>
</html>
