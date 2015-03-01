<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Share_Help_edit"
    ValidateRequest="false" %>

<%@ Register Assembly="TinyMCE" Namespace="TinyMCE.Web" TagPrefix="TinyMce" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>帮助信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        //验证
        function CheckInfo() {
            if (AutoCheckInfo()) {
                this.disabled = true
                return true
            }
            return false;
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="l_BB" runat="server" Text="帮助信息维护"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar" id="Table1">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClick="btnDel_Click" />
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click" />
                <input class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table align="center" style="width: 98%;" class="m_table">
        <tr>
            <td class="t_r t_bg" width="120">
                标题：
            </td>
            <td>
                <asp:TextBox ID="t_FTitle" runat="server" CssClass="m_txt" MaxLength="25" Width="218px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                栏目编码：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkNumber" runat="server" CssClass="m_txt" MaxLength="20" Width="80px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg m_txt_M" colspan="2">
                <TinyMce:TextArea ID="t_FContent" Width="100%" Height="400px" runat="server" RelativeUrls="False"
                    ScriptPath="../../tiny_mce/" ThemeAdvancedResizing="False" ThemeAdvancedButtons1="styleprops,fontselect,fontsizeselect,separator,forecolor,backcolor,separator,bold,italic,underline,separator,bullist,numlist,separator, justifyleft, justifycenter, justifyright"
                    ThemeAdvancedButtons2="undo,redo,cut,copy,paste,pastetext,pasteword,separator,link,unlink,image,uploadImage,media,separator,emotions,signature,insertcode,separator,visualaid,fullscreen,preview,code" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
