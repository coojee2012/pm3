<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Online.aspx.cs" Inherits="JSDW_ApplyAQJDBA_Online" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
    </script>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click"
                        OnClientClick="return checkInfo();" />
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    姓名：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FHumanName" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    性别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FSex" runat="server" CssClass="m_txt" Width="202px">
                        <asp:ListItem Value="1">男</asp:ListItem>
                        <asp:ListItem Value="2">女</asp:ListItem>
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    出生日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FBirthDay" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    项目职位：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_XMZW" runat="server" CssClass="m_txt" Width="195px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    证件类型：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZJLX" runat="server" CssClass="m_txt" Width="202px">
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    证件号码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZJHM" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    所在企业：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_SZQY" runat="server" CssClass="m_txt"  Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    职称：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZC" runat="server" CssClass="m_txt" Width="202px">
                        <asp:ListItem Value="1">初级</asp:ListItem>
                        <asp:ListItem Value="2">高级</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    职称专业：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZCZY" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    职称证书号：
                </td>
                <td colspan="1">
                   <asp:TextBox ID="t_ZCZSH" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    最高学历：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZGXL" runat="server" CssClass="m_txt" Width="202px">
                        <asp:ListItem Value="1">高中</asp:ListItem>
                        <asp:ListItem Value="2">大学</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    注册专业：
                </td>
                <td colspan="1">
                   <asp:TextBox ID="t_ZHUCZY" runat="server" CssClass="m_txt" Width="250px" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    注册证书号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZHUCZSH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    安全考核合格证号：
                </td>
                <td colspan="1">
                   <asp:TextBox ID="t_AQKHHGZH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>   
            <tr>
                <td class="t_r t_bg">
                    移动电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FMobile" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                </td>
            </tr>     
        </table>
    </div>
    </form>
</body>
</html>