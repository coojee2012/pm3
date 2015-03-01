<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleRightsSet.aspx.cs" Inherits="Share_Sys_RoleRightsSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>菜单权限设置</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        $(document).ready(function() {
            DynamicGrid(".m_dg1_i");
        });


        //全选
        function cAll(obj, name) {
            $("input[name$='" + name + "']").attr('checked', obj.checked);
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                菜单权限设置
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="btnBack" class="m_btn_w2" type="button" value="返回" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_dg1 m_dg1_h">
        <tr style="height: 25px;">
            <td width="50px" align="center">
                序号
            </td>
            <td>
                菜单名
            </td>
            <td width="100px" class="t_c">
                是否有权限<input type="checkbox" onclick="cAll(this,'CB_IsPope')" />
            </td>
        </tr>
    </table>
    <div style="height: 460px; overflow: auto;">
        <table width="98%" align="center" class="m_dg1">
            <asp:Repeater runat="server" ID="PopeTreeMain" OnItemDataBound="PopeTreeMain_ItemDataBound">
                <ItemTemplate>
                    <tr class="m_dg1_i">
                        <td width="50px" align="center">
                            <%# Container.ItemIndex +1 %>
                        </td>
                        <td class="t_l">
                            <%# Eval("FName") %>
                        </td>
                        <td width="100px" class="t_c">
                            <asp:Label ID="la_FNumber" runat="server" Text='<%# Eval("FNumber") %>' Style="display: none;"> </asp:Label>
                            <asp:CheckBox ID="CB_IsPope" runat="server" ToolTip="是否有此管理权限" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
