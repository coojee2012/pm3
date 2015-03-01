<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addUnit.aspx.cs" Inherits="GFEnt_ApplyEnt_addUnit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">主要完成单位维护
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">单位名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FName" Width="300px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td  class="t_r t_bg">通讯地址：
                </td>
                <td colspan="3" class="auto-style1">
                    <asp:TextBox ID="t_FAddress" Width="300px" runat="server" CssClass="m_txt"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">邮编：
                </td>
                <td>
                    <asp:TextBox ID="t_FPostcode" runat="server" CssClass="m_txt"></asp:TextBox>

                </td>
                <td class="t_r t_bg">联系人：
                </td>
                <td>
                    <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">办公电话：
                </td>
                <td>
                    <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt"></asp:TextBox>

                </td>
                <td class="t_r t_bg">手机：
                </td>
                <td>
                    <asp:TextBox ID="t_FMobile" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FRemark" runat="server" Width="300px" CssClass="m_txt" Height="100px" TextMode="MultiLine"></asp:TextBox>

                </td>
            </tr>
        </table>
        <input id="t_FUpgradeTF" runat="server" type="hidden" />
        <input id="t_YWBM" runat="server" type="hidden" />
        <input id="t_FBaseInfoId" runat="server" type="hidden" />
        <input id="t_FSystemId" runat="server" type="hidden" />
        <input id="t_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
