<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerSonAdd.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_PerSonAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
     <style type="text/css">
        #form1 label.errorMsg 
			{ 
			color:Red; 
			font-size:13px; 
			margin-left:5px;
			}
         .m_btn_w2 {
             height: 21px;
         }
    </style>
    <script type="text/javascript" src="../../../script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../../script/default.js"></script>
    <script type="text/javascript" src="../../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../../script/messages_zh.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });;
        });
        function SaveValidate() {
            var success = $("#form1").valid();
            if(success)
                return true;
            return false;
        }
        function Exit() {
            window.close();
            window.returnValue = '1';
        }
    </script>

    <%--<script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>--%>

    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
    <input id="hidd_LockID" type="hidden" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                添加人员信息
            </th>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" OnClientClick="return SaveValidate()" OnClick="btnSave_Click" />
                    <input id="Button1" class="m_btn_w2" type="button" value="返 回" onclick="Exit()" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">所属单位名称：</td>
                <td colspan="3"><asp:TextBox ID="txtSSDWMC" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox><tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">所属单位组织机构代码：</td>
                <td><asp:TextBox ID="txtSSDWZZJGDM" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt></td>
                <td class="t_r t_bg">人员姓名：</td>
                <td><asp:TextBox ID="txtRYXM" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">证件类型： </td>
                <td><asp:DropDownList ID="ddlZJLX" CssClass="required" runat="server"></asp:DropDownList><tt>*</tt></td>
                <td class="t_r t_bg">证件号码</td>
                <td><asp:TextBox ID="txtZJHM" runat="server" CssClass="m_txt required" Width="150"></asp:TextBox><tt>*</tt></td>
            </tr>
            <tr>
                 <td class="t_r t_bg">注册类型及等级：</td>
                <td><asp:DropDownList ID="ddlZCLXDJ" CssClass="required" runat="server"></asp:DropDownList><tt>*</tt></td>
                <td class="t_r t_bg">承担角色</td>
                <td>
                    <asp:TextBox ID="txtCDJS" runat="server" CssClass="required"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
