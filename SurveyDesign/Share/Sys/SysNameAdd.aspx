<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysNameAdd.aspx.cs" Inherits="Share_Sys_SysNameAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
            DynamicGrid(".m_dg1_i");
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
        function exitt() { //返回
            if ($("#HSaveResult").val() == "1")
                window.returnValue = "1";
            window.close();
        }
        function upPic(sUrl, width, height) {
            if ($('#hidden_Fid').val() == null || $('#hidden_Fid').val() == "") {
                alert('请先保存');
                return false;
            }
            var idvalue = window.showModalDialog(sUrl + '?fid=' + $('#hidden_Fid').val() + '&imgUrl=' + $('#t_FPic').val() + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:no;')
            if (idvalue != null && idvalue.indexOf('.') != -1) {
                $('#img_url').attr("src", idvalue);
                $('#t_FPic').val(idvalue);
            }
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                系统管理
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="exitt();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                系统类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="企业系统类型" Value="1"></asp:ListItem>
                    <asp:ListItem Text="管理部门系统类型" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                系统名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="225px" MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                系统编号：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" CssClass="m_txt" MaxLength="8" onblur="isInt(this);"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                登陆地址：
            </td>
            <td>
                <asp:TextBox ID="t_FLoginURL" runat="server" CssClass="m_txt" Width="225px" MaxLength="150"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                登陆转接地址：
            </td>
            <td>
                <asp:TextBox ID="t_FLockCheckURL" runat="server" CssClass="m_txt" Width="225px" MaxLength="150"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                密钥：
            </td>
            <td>
                <asp:TextBox ID="t_FKey" runat="server" CssClass="m_txt" Width="121px" MaxLength="8"
                    onblur="isInt(this);"></asp:TextBox>
                <tt>*</tt> 8位数字
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                排序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt" Width="66px" MaxLength="3"
                    onblur="isInt(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                系统全称：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FDesc" runat="server" CssClass="m_txt" MaxLength="25" Width="231px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                说明：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FRemark" runat="server" CssClass="m_txt" MaxLength="25" Width="231px"
                    TextMode="MultiLine" Height="61px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                图标：
            </td>
            <td class="m_txt_M">
                <div style="float: left;">
                    <img id="img_url" runat="server" height="100" width="100" />
                </div>
                <div style="float: left; width: 50px; height: 25px; margin-top: 75px; padding-left: 10px;">
                    <a href="javascript:upPic('../main/uppic.aspx',394,220);">［修改］</a>
                </div>
            </td>
        </tr>
    </table>
    <input id="t_FPic" runat="server" type="hidden" />
    <input id="hidden_Fid" runat="server" type="hidden" />
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
