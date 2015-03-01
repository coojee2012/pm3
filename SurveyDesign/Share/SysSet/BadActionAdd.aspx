<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BadActionAdd.aspx.cs" Inherits="Admin_main_BadActionAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>不良行为维护</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
             DynamicGrid(); //列表光标移动效果
        });       
    </script>

    <script language="javascript">
    function CheckInfo()
    {
        if(document.getElementById("t_FNumber").value.trim()=="")
        {
            alert("行为编码必须填写");
            document.getElementById("t_FNumber").focus();
            return false;
        }
         
        if(document.getElementById("t_FScore").value.trim()=="")
        {
            alert("分数必须填写");
            document.getElementById("t_FScore").focus(); 
            return false;
        }
        if(document.getElementById("t_FDesc").value.trim=="")
        {
            alert("行为描述必须填写");
            document.getElementById("t_FDesc").focus();  
            return false;
        }
        return true;
    }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                不良行为维护
            </th>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                &nbsp;<asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                &nbsp;<input id="btnBack" class="m_btn_w2" onclick="ifSaveOk();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table align="center" class="m_table" width="100%">
        <tr>
            <td class="t_r t_bg">
                单位类型：
            </td>
            <td>
                <asp:TextBox ID="tPPName" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                行为类别：
            </td>
            <td>
                <asp:TextBox ID="tPName" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                行为编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                分数：
            </td>
            <td>
                <asp:TextBox ID="t_FScore" runat="server" CssClass="m_txt" onblur="isInt(this);"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt" onblur="isInt(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                行为描述：
            </td>
            <td>
                <asp:TextBox ID="t_FDesc" runat="server" CssClass="m_txt" Height="100px" Width="400px"
                    TextMode="MultiLine" Style="word-break: break-all; word-wrap: break-word"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                法律依据：
            </td>
            <td>
                <asp:TextBox ID="t_FLawGist" runat="server" CssClass="m_txt" Height="100px" Width="400px"
                    TextMode="MultiLine" Style="word-break: break-all; word-wrap: break-word"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                处罚依据：
            </td>
            <td>
                <asp:TextBox ID="t_FPunishGist" runat="server" CssClass="m_txt" Height="100px" Width="400px"
                    TextMode="MultiLine" Style="word-break: break-all; word-wrap: break-word"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
