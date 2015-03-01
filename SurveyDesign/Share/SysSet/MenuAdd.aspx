<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuAdd.aspx.cs" Inherits="Admin_main_MenuAdd" %>

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
            return AutoCheckInfo();
        }

        function setLeftMenu(fnumber) {
            parent.frames["left"].document.location.reload();
        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

    <style type="text/css">
        .a_1
        {
            color: #2A85E0;
            text-decoration: underline;
        }
        .a_1:hover, .a_2:hover
        {
            color: #FF0000;
            text-decoration: underline;
        }
        .a_2
        {
            color: green;
            text-decoration: underline;
        }
    </style>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
            <td style="padding-left: 10px;">
                <img src="../../image/ts.gif" />
                <asp:Literal ID="a_Help" runat="server"></asp:Literal>
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click"
                    Style="display: none;" />
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
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt"></asp:TextBox>
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
                <asp:DropDownList ID="t_FLevel" runat="server" OnSelectedIndexChanged="t_FLevel_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem Value="1">一级</asp:ListItem>
                    <asp:ListItem Value="2">二级</asp:ListItem>
                    <asp:ListItem Value="3">三级</asp:ListItem>
                    <asp:ListItem Value="4">四级</asp:ListItem>
                    <asp:ListItem Value="5">五级</asp:ListItem>
                    <asp:ListItem Value="6">六级</asp:ListItem>
                    <asp:ListItem Value="7">七级</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="isShowTr1" runat="server">
            <td class="t_r t_bg">
                一级栏目：
            </td>
            <td>
                <asp:DropDownList ID="t_FParentId1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FParentId1_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr id="isShowTr2" runat="server">
            <td class="t_r t_bg">
                二级栏目：
            </td>
            <td>
                <asp:DropDownList ID="t_FParentId2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FParentId2_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr id="isShowTr3" runat="server">
            <td class="t_r t_bg">
                三级栏目：
            </td>
            <td>
                <asp:DropDownList ID="t_FParentId3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FParentId3_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr id="isShowTr4" runat="server">
            <td class="t_r t_bg">
                四级栏目：
            </td>
            <td>
                <asp:DropDownList ID="t_FParentId4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FParentId4_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr id="isShowTr5" runat="server">
            <td class="t_r t_bg">
                五级栏目：
            </td>
            <td>
                <asp:DropDownList ID="t_FParentId5" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FParentId5_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr id="isShowTr6" runat="server">
            <td class="t_r t_bg">
                六级栏目：
            </td>
            <td>
                <asp:DropDownList ID="t_FParentId6" runat="server">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                维护地址：
            </td>
            <td>
                <asp:TextBox ID="t_FUrl" runat="server" CssClass="m_txt" Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                查询地址：
            </td>
            <td>
                <asp:TextBox ID="t_FQUrl" runat="server" CssClass="m_txt" Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                查询是否显示：
            </td>
            <td>
                <asp:DropDownList ID="t_FIsDis" runat="server" Width="55px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                目标框架：
            </td>
            <td>
                <asp:TextBox ID="t_FTarget" runat="server" CssClass="m_txt"></asp:TextBox>
                <span id="div_Map" runat="server" visible="false" style="color: Red;">地图专用配置【 小图标：<asp:TextBox
                    ID="t_FMapPic" runat="server" CssClass="m_txt" Width="100"></asp:TextBox>
                    显示层级：<asp:TextBox ID="t_FMapZoom" runat="server" CssClass="m_txt" onblur="isInt(this);"
                        Width="50"></asp:TextBox>】 </span>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                选中状态下图片：
            </td>
            <td>
                <asp:TextBox ID="t_FSelcePicName" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                普通状态下图片：
            </td>
            <td>
                <asp:TextBox ID="t_FPicName" runat="server" CssClass="m_txt"></asp:TextBox>
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
                对应业务系统：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FSystemId_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                对应业务类别：
            </td>
            <td style="height: 30px">
                <asp:DropDownList ID="t_FManageTypeId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FManageTypeId_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                <label for="cbkAll">
                    角色</label><input type="checkbox" id="cbkAll" />：
            </td>
            <td>
                <asp:CheckBoxList ID="t_FRoleId" runat="server" BorderWidth="0px" BorderStyle="None"
                    RepeatColumns="4" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                刷新左侧目录树：
            </td>
            <td>
                <asp:CheckBox ID="cbIsReLoad" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function() {
        $(":checkbox:checked[id^=t_FRoleId]").parent().css("background", "#E5FaFf");
        $(":checkbox[id^=t_FRoleId]").click(function() {
            $(this).parent().css("background", this.checked ? '#E5FaFf' : "");
            if (!this.checked)
                $("#cbkAll").removeAttr("checked");
            if ($(":checkbox:checked[id^=t_FRoleId]").length == $(":checkbox[id^=t_FRoleId]").length)
                $("#cbkAll").attr("checked", $(this).attr("checked"));
        });
        $("#cbkAll").click(function() {
            $(":checkbox[id^=t_FRoleId]").attr("checked", $(this).attr("checked"));
            $(":checkbox[id^=t_FRoleId]").parent().css("background", $("#cbkAll").attr("checked") ? '#E5FaFf' : "");
        });
    }); 
</script>

