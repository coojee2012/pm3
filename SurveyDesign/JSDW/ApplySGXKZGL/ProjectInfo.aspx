<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectInfo.aspx.cs" Inherits="JSDW_ApplySGXKZGL_ProjectInfo" %>

<%@ Register TagPrefix="uc1" TagName="pager" Src="~/Common/pager.ascx" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <style type="text/css">
        .style1 { text-align: left; height: 31px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                文件或证明材料
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                    OnClientClick="return checkInfo();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table id="table1" class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>项目立项信息</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                立项文号：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_ProjectNo" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                <input id="txtFId" type="hidden" runat="server" />
            </td>
            <td class="t_r t_bg">
                立项文件： </td>
            <td>
                <asp:TextBox ID="t_ProjectFile" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                    立项级别：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_ProjectLevel" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                    立项分类：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_ProjectSetType" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>           
        </tr>
        <tr>
            <td class="t_r t_bg">
                    项目类型：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_ProjectType" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                    建筑性质：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_ConstrType" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                投资规模：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_Cost" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>(万元)
            </td>
        </tr>
     
    </table>
    </form>
</body>
</html>

