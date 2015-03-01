<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemAdd.aspx.cs" Inherits="Admin_main_SystemAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务系统维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        function CheckInfo() {
            return AutoCheckInfo(); //自动验证
        }
        function exitt() { //返回
            if ($("#HSaveResult").val() == "1")
                window.returnValue = "1";
            window.close();
        }
        $(document).ready(function() {
            txtCss(); //文本框样式
        });

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
                业务系统维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="98%" class="m_table" align="center">
        <tr>
            <td class="t_r t_bg">
                所属平台类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FPlatId" runat="server">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                系统名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                系统编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
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
                简称：
            </td>
            <td>
                <asp:TextBox ID="t_FDesc" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                说明(60字内)：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Width="240px" TextMode="MultiLine"
                    Height="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                登录地址：
            </td>
            <td>
                <asp:TextBox ID="t_FLurl" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                系统权限：
            </td>
            <td>
                <asp:TextBox ID="t_FQurl" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                只签字系统：
            </td>
            <td>
                <asp:TextBox ID="t_FSignSys" runat="server" CssClass="m_txt" Width="300px" ToolTip="必需在“系统权限”内"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                信用档案查询地址：
            </td>
            <td>
                <asp:TextBox ID="t_FDAQUrl" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                招投标系统查询地址：
            </td>
            <td>
                <asp:TextBox ID="t_FZTBUrl" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                密钥：
            </td>
            <td>
                <asp:TextBox ID="t_FShareKey" runat="server" CssClass="m_txt" ForeColor="Red" MaxLength="8"
                    onblur="isInt(this);"></asp:TextBox>
                <tt>*</tt>8位数字
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                状态：
            </td>
            <td>
                <asp:DropDownList ID="t_FState" runat="server">
                    <asp:ListItem Value="1" Text="有效"></asp:ListItem>
                    <asp:ListItem Value="0" Text="无效"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="display: none">
            <td class="t_r t_bg">
                图标：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FPic" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                <div style="float: left;">
                    <img id="img_url" runat="server" />
                </div>
                <div style="float: left; width: 50px; height: 25px; margin-top: 75px; padding-left: 10px;">
                    <a href="javascript:upPic('../main/uppic.aspx',394,220);">［修改］</a>
                </div>
            </td>
        </tr>
    </table>
    <input id="hidden_Fid" runat="server" type="hidden" />
    </form>
</body>
</html>
