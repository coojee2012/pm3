<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Participat.aspx.cs" Inherits="JSDW_ApplyAQJDBA_Participat" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参建单位信息</title>
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
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
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
                    企业名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_QYMC" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    参建角色：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_CJJS" runat="server" CssClass="m_txt" Width="202px">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    企业地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_QYDZ" runat="server" CssClass="m_txt" Width="274px" MaxLength="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    组织机构代码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZZJGDM" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    资质等级：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZZDJ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    资质证书号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZZZS" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    营业执照号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_YYZZH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    企业法定代表人：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="t_QYFDDB" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_LXDH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td colspan="1">
                   <asp:TextBox ID="t_LXR" runat="server" CssClass="m_txt" onblur="isFloat(this)" Width="195px" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
                  
        </table>
    </div>
    </form>
</body>
</html>
