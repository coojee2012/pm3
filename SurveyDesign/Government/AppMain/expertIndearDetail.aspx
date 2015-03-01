<%@ Page Language="C#" AutoEventWireup="true" CodeFile="expertIndearDetail.aspx.cs" Inherits="Government_AppMain_expertIndearDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>
    <style type="text/css">
        .m_txt {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">专家意见详情
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m t_r">
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">专家姓名：
                </td>
                <td>
                    <asp:TextBox ID="t_ExpertName" Width="150px" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>

                </td>
                <td class="t_r t_bg">评审时间：
                </td>
                <td>
                    <asp:TextBox ID="t_Ftime" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">专家意见：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_Fresult" Width="500px" runat="server" CssClass="m_txt" Height="300px" TextMode="MultiLine"></asp:TextBox>

                </td>
            </tr>
        </table>
        <input id="t_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
