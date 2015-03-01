<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckUserPope.aspx.cs" Inherits="Admin_User_CheckUserPope" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>特殊权限设置</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $(":checkbox[id^=CB_Is]").click(function() {
                $(":checkbox[id$=" + this.id + "]").attr("checked", this.checked ? "checked" : "");
            });
        });
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                特殊权限设置
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="98%" class="m_dg1" align="center">
        <tr class="m_dg1_h">
            <th width="50" align="center">
                序号
            </th>
            <th>
                菜单名
            </th>
            <th width="100" align="center">
                <input id="CB_IsPope" type="checkbox" /><label for="CB_IsPope">是否可管理</label>
            </th>
            <th width="50" align="center">
                <input id="CB_IsAdd" type="checkbox" /><label for="CB_IsAdd">新增</label>
            </th>
            <th width="50" align="center">
                <input id="CB_IsADel" type="checkbox" /><label for="CB_IsADel">删除</label>
            </th>
            <th width="50" align="center">
                <input id="CB_IsPub" type="checkbox" /><label for="CB_IsPub">发布</label>
            </th>
            <th width="80" align="center">
                <input id="CB_IsClosePub" type="checkbox" /><label for="CB_IsClosePub">取消发布</label>
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_dg1">
        <asp:Repeater runat="server" ID="PopeTreeMain" OnItemDataBound="PopeTreeMain_ItemDataBound">
            <ItemTemplate>
                <tr class="m_dg1_i">
                    <td width="50" align="center">
                        <%# Container.ItemIndex +1 %>
                    </td>
                    <td align="left" style="font-weight: bold;">
                        <%# Eval("FName") %>
                    </td>
                    <td width="100" align="center">
                        <asp:Label ID="la_FID" runat="server" Text='<%# Eval("FID") %>' Style="display: none;"> </asp:Label>
                        <asp:CheckBox ID="CB_IsPope" runat="server" ToolTip="是否有此管理权限" />
                    </td>
                    <td width="50" align="center">
                        <asp:CheckBox ID="CB_IsAdd" runat="server" ToolTip="是否有新增权限" />
                    </td>
                    <td width="50" align="center">
                        <asp:CheckBox ID="CB_IsADel" runat="server" ToolTip="是否有删除权限" />
                    </td>
                    <td width="50" align="center">
                        <asp:CheckBox ID="CB_IsPub" runat="server" ToolTip="是否有发布权限" />
                    </td>
                    <td width="80" align="center">
                        <asp:CheckBox ID="CB_IsClosePub" runat="server" ToolTip="是否有取消发布权限" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <table width="98%" align="center" class="m_dg1">
        <asp:Repeater runat="server" ID="PopeTree" OnItemDataBound="PopeTree_ItemDataBound">
            <ItemTemplate>
                <tr class="m_dg1_i">
                    <td width="50" align="center">
                        <%# Container.ItemIndex +1 %>
                    </td>
                    <td align="left">
                        <%# Eval("FName") %>
                    </td>
                    <td width="100" align="center">
                        <asp:Label ID="la_FID" runat="server" Text='<%# Eval("FID") %>' Style="display: none;"> </asp:Label>
                        <asp:CheckBox ID="CB_IsPope" runat="server" ToolTip="是否有此管理权限" />
                    </td>
                    <td width="50" align="center">
                        <asp:CheckBox ID="CB_IsAdd" runat="server" ToolTip="是否有新增权限" />
                    </td>
                    <td width="50" align="center">
                        <asp:CheckBox ID="CB_IsADel" runat="server" ToolTip="是否有删除权限" />
                    </td>
                    <td width="50" align="center">
                        <asp:CheckBox ID="CB_IsPub" runat="server" ToolTip="是否有发布权限" />
                    </td>
                    <td width="80" align="center">
                        <asp:CheckBox ID="CB_IsClosePub" runat="server" ToolTip="是否有取消发布权限" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <input id="HKINDID" runat="server" type="hidden" value="" />
    </form>
</body>
</html>
