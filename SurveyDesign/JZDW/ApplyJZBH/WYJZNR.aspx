<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WYJZNR.aspx.cs" Inherits="JZDW_ApplyJZBH_WYJZNR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        function checkInfo() {
            if (!AutoCheckInfo()) {
                return false;
            }
            if (!getLength(document.getElementById("t_FTxt10"), 200, '“其他需要说明的资料”')) {
                return false;
            }

            return true;
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                外业见证内容
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r" style="padding-right: 10px;">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                勘察任务书：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_FTxt13" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="有" Value="1"></asp:ListItem>
                    <asp:ListItem Text="无" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                勘察纲要：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_FTxt14" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="有" Value="1"></asp:ListItem>
                    <asp:ListItem Text="无" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                勘察纲要变更：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_FTxt15" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="有" Value="1"></asp:ListItem>
                    <asp:ListItem Text="无" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                变更人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt5" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                变更批准人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt6" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                原位测试方法是否符合规范：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_FTxt16" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                测量放点：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_FTxt9" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="有控制点" Value="1"></asp:ListItem>
                    <asp:ListItem Text="无控制点" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                测量控制点的来源：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt11" runat="server" CssClass="m_txt" MaxLength="50" Width="350"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                初见水位观测：
            </td>
            <td>
                <asp:DropDownList ID="t_FTxt7" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="有" Value="1"></asp:ListItem>
                    <asp:ListItem Text="无" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                稳定水位观测：
            </td>
            <td>
                <asp:DropDownList ID="t_FTxt8" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="有" Value="1"></asp:ListItem>
                    <asp:ListItem Text="无" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                钻探工作量：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FInt0" runat="server" CssClass="m_txt t_r" MaxLength="9" Width="90"
                    onblur="isInt(this);"></asp:TextBox>
                孔，共计
                <asp:TextBox ID="t_FFloat1" runat="server" CssClass="m_txt t_r" MaxLength="50" Width="90"
                    onblur="isFloat(this);"></asp:TextBox>
                米
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                静探工作量：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FInt1" runat="server" CssClass="m_txt t_r" MaxLength="9" Width="90"
                    onblur="isInt(this);"></asp:TextBox>
                孔，共计
                <asp:TextBox ID="t_FFloat2" runat="server" CssClass="m_txt t_r" MaxLength="50" Width="90"
                    onblur="isFloat(this);"></asp:TextBox>
                米
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                取土试样：
            </td>
            <td colspan="3">
                原状
                <asp:TextBox ID="t_FInt2" runat="server" CssClass="m_txt t_r" MaxLength="9" Width="90"
                    onblur="isInt(this);"></asp:TextBox>
                件、扰动
                <asp:TextBox ID="t_FInt3" runat="server" CssClass="m_txt t_r" MaxLength="9" Width="90"
                    onblur="isInt(this);"></asp:TextBox>
                件
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                取水试样：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FInt4" runat="server" CssClass="m_txt t_r" MaxLength="9" Width="90"
                    onblur="isInt(this);"></asp:TextBox>
                件、扰动
                <asp:TextBox ID="t_FFloat3" runat="server" CssClass="m_txt t_r" MaxLength="10" Width="90"
                    onblur="isInt(this);"></asp:TextBox>
                件
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                取岩试样：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FFloat4" runat="server" CssClass="m_txt t_r" MaxLength="10" Width="90"
                    onblur="isInt(this);"></asp:TextBox>
                件、扰动
                <asp:TextBox ID="t_FFloat5" runat="server" CssClass="m_txt t_r" MaxLength="10" Width="90"
                    onblur="isInt(this);"></asp:TextBox>
                件
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                原位测试：
            </td>
            <td colspan="3">
                标贯
                <asp:TextBox ID="t_FFloat6" runat="server" CssClass="m_txt t_r" MaxLength="50" Width="90"
                    onblur="isFloat(this);"></asp:TextBox>
                段次；动力触探
                <asp:TextBox ID="t_FFloat7" runat="server" CssClass="m_txt t_r" MaxLength="50" Width="90"
                    onblur="isFloat(this);"></asp:TextBox>
                段次（重型N<sub>63.5</sub>、超重型N<sub>120</sub>）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                其他原位测试：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt20" runat="server" CssClass="m_txt" MaxLength="50" Width="350"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                其他测试：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt21" runat="server" CssClass="m_txt" MaxLength="50" Width="350"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                其他需要说明的资料：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_FTxt10" runat="server" CssClass="m_txt" MaxLength="50" Width="350"
                    TextMode="MultiLine" Height="60" onblur="getLength(this,200,'“其他需要说明的资料”');"></asp:TextBox>
                (200字内)
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
