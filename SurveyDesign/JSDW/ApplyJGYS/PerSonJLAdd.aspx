<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerSonJLAdd.aspx.cs" Inherits="JSDW_ApplyJGYS_PerSonJLAdd" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <style type="text/css">
        #form1 label.errorMsg 
			{ 
			color:Red; 
			font-size:13px; 
			margin-left:5px;
			}
    </style>
     <script type="text/javascript" src="../../script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../script/messages_zh.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
            $("#btnSave").click(function () {
                var success = $("#form1").valid();
                if (success) {
                    var YDDH = $("#txtYDDH").val();
                    var SFZH = $("#txtSFZH").val();
                    var Ismobile = checkMobile(YDDH);
                    var IsCardNo = checkCardNo(SFZH);
                    if (!IsCardNo) {
                        alert("身份证号格式不正确");
                        return false;
                    } else if (!Ismobile) {
                        alert("移动号码格式不正确");
                        return false;
                    }
                    return true;
                }
                return false;
            });
        });
        function checkMobile(str) {
            var re = /^1\d{10}$/
            if (re.test(str)) {
                return true;
            } else {
                return false;
            }
        }
        function checkCardNo(card) {
            // 身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X
            var reg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
            if (reg.test(card)) {
                return true;
            }
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:HiddenField ID="hfId" runat="server" />
     <asp:HiddenField ID="hfCompanyId" runat="server" />
     <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                人员信息
            </th>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存"  OnClick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <input type="button" value="返回" Class="m_btn_w2" onclick="javascript: window.returnValue = '1';window.close()" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    人员类型：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlRYLX" CssClass="required" runat="server" Width="100px">
                        
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    姓名：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtPerSonName" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    身份证号：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtSFZH" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    性 别：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlSex" runat="server" Width="100">
                        <asp:ListItem Value="男">男</asp:ListItem>
                        <asp:ListItem Value="女">女</asp:ListItem>
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    最高学历：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlXL" CssClass="required" runat="server" Width="100">
                        
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    移动电话：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtYDDH" runat="server" CssClass="m_txt required number" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    职称：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlZC" CssClass="required" runat="server" Width="100px">
                        <%--<asp:ListItem  Value="工程师">工程师</asp:ListItem>
                        <asp:ListItem  Value="教授级高工">教授级高工</asp:ListItem>
                        <asp:ListItem  Value="高级工程师">高级工程师</asp:ListItem>
                        <asp:ListItem  Value="助理工程师">助理工程师</asp:ListItem>
                        <asp:ListItem  Value="其他">其他</asp:ListItem>--%>
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    职务：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtZW" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtLXDH" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    专业：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtZY" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    证书编号：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtZSBH" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    等 级：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDJ" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    注册编号：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtZCBH" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    注册专业：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtZCZY" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    注册日期：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtZCRQ" runat="server"   CssClass="m_txt Wdate required" onfocus="WdatePicker({skin:'whyGreen' })" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
           
        </table>
      </div>
    </form>
</body>
</html>

