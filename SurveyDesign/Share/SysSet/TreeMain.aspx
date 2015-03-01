<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreeMain.aspx.cs" Inherits="Share_Main_TreeMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function CheckInfo() {
            if (document.getElementById("t_FName").value.trim() == "") {
                alert("栏目名称必须填写");
                document.getElementById("t_FName").focus();
                return false;
            }
            if (document.getElementById("t_FNumber").value.trim() == "") {
                alert("栏目编号必须填写");
                document.getElementById("t_FNumber").focus();
                return false;
            }
            if (document.getElementById("t_FKindId").value.trim() == "") {
                alert("请选择栏目类型");
                document.getElementById("t_FKindId").focus();
                return false;
            }

            return true;

        }
        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th>
                菜单维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" Style="margin-left: 20px;"
                    OnClick="btnNew_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click"
                    Style="margin-left: 20px;" />
                &nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" cellspacing="0" align="center">
        <tr>
            <td class="t_r t_bg">
                栏目名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="290px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                栏目编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                栏目等级：
            </td>
            <td>
                <asp:DropDownList ID="t_FLevel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FLevel_SelectedIndexChanged">
                    <asp:ListItem Value="1">一级</asp:ListItem>
                    <asp:ListItem Value="2">二级</asp:ListItem>
                    <asp:ListItem Value="3">三级</asp:ListItem>
                    <asp:ListItem Value="4">四级</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                上级栏目：
            </td>
            <td>
                <asp:DropDownList ID="drop_Parent1" runat="server" OnSelectedIndexChanged="drop_Parent1_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:DropDownList ID="drop_Parent2" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="drop_Parent2_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="drop_Parent3" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                后台维护页面地址：
            </td>
            <td>
                <asp:TextBox ID="t_FAdminUrl" runat="server" CssClass="m_txt" Width="290px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                前台显示列表地址：
            </td>
            <td>
                <asp:TextBox ID="t_FWebListUrl" runat="server" CssClass="m_txt" Width="290px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                前台显示页面地址：
            </td>
            <td>
                <asp:TextBox ID="t_FWebUrl" runat="server" CssClass="m_txt" Width="290px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                栏目类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FClass" runat="server">
                    <asp:ListItem Value="1">维护栏目</asp:ListItem>
                    <asp:ListItem Value="2">系统管理栏目</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                频道类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server">
                <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                    <asp:ListItem Value="1" Text="新闻频道"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FKindId" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                打开方式：
            </td>
            <td>
                <asp:DropDownList ID="t_FDisType" runat="server" Width="152px">
                    <asp:ListItem Value="0">当前页</asp:ListItem>
                    <asp:ListItem Value="1">新开页</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否前台显示：
            </td>
            <td>
                <asp:DropDownList ID="t_FIsShow" runat="server" Width="152px">
                    <asp:ListItem Value="0">显示</asp:ListItem>
                    <asp:ListItem Value="1">不显示</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                目标框架：
            </td>
            <td>
                <asp:TextBox ID="t_FTarget" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                正常图片：
            </td>
            <td>
                <asp:TextBox ID="t_FPicName" runat="server" CssClass="m_txt" Width="206px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                选中图片：
            </td>
            <td>
                <asp:TextBox ID="t_FSelcePicName" runat="server" CssClass="m_txt" Width="206px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                展开图片：
            </td>
            <td>
                <asp:TextBox ID="t_FExpPicName" runat="server" CssClass="m_txt" Width="206px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
