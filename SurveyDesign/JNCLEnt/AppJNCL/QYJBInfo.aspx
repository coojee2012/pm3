<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QYJBInfo.aspx.cs" Inherits="JNCLEnt_AppJNCL_QYJBInfo" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../script/messages_zh.js"></script>

    <script type="text/javascript">
        function SaveValidate() {
            var success = $("#form1").valid();
            if (success)
                return true;
            return false;
        }
        function SearchProject() {
            var result = showWinByReturn("../ApplyXZYJS/ChooseBuildUnit.aspx?", 800, 500);
            if (result != undefined) {
                $("#hfXMBM").val(result);
                return true;
            }
            return false;
        }
        $(function () {
            $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
        });
    </script>
    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业基本信息
            </th>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                    
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" 
                        OnClientClick="return SaveValidate()" onclick="btnSave_Click" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    企业名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtQYMC" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    企业地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtQYDZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr><tr>
                <td class="t_r t_bg">
                    企业所在地：
                </td>
                <td colspan="3">
                    <uc1:govdeptid ID="ucProjectPlace" runat="server" /> <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    生产地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtSCDZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    营业执照号：
                </td>
                <td>
                    <asp:TextBox ID="txtYYZZH" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    组织结构代码：
                </td>
                <td><asp:TextBox ID="txtZZJGDM" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt></td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    邮政编码：
                </td>
                <td>
                    <asp:TextBox ID="txtYZBM" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    电子邮件：
                </td>
                <td><asp:TextBox ID="txtDZYJ" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td>
                    <asp:TextBox ID="txtLXR" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    联迪电话：
                </td>
                <td>
                    <asp:TextBox ID="txtLXDH" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox>
                    <tt>*</tt>
                </td>
           </tr>
            <tr>
                 <td class="t_r t_bg">
                   法人代表：
                </td>
                <td>
                    <asp:TextBox ID="txtFRDB" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                   手机：
                </td>
                <td><asp:TextBox ID="txtSJ" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt></td>
           </tr>
            <tr>
                 <td class="t_r t_bg">
                   传真：
                </td>
                <td>
                    <asp:TextBox ID="txtCZ" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                   经济性质：
                </td>
                <td>
                    <asp:DropDownList ID="ddlJJXZ" runat="server" CssClass="m_txt required" Width="200"></asp:DropDownList>
                <tt>*</tt></td>
           </tr>
            <tr>
                 <td class="t_r t_bg">
                   注册资金：
                </td>
                <td>
                    <asp:TextBox ID="txtZCZJ" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                   企业人数：
                </td>
                <td><asp:TextBox ID="txtQYRS" runat="server"  CssClass="m_txt required number" Width="200"></asp:TextBox><tt>*</tt></td>
           </tr>
            <tr>
                 <td class="t_r t_bg">
                   年生产能力：
                </td>
                <td>
                    <asp:TextBox ID="txtNSCNL" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                   累计生产量：
                </td>
                <td><asp:TextBox ID="txtLJSCL" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt></td>
           </tr>
        </table>
        </div>
    </form>
</body>
</html>

