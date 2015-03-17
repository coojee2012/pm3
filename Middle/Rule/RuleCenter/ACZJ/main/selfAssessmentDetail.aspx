<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selfAssessmentDetail.aspx.cs" Inherits="ACZJ_main_selfAssessmentDetail" %>

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
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">自评详情
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    &nbsp;<asp:Button ID="btnUP" runat="server" CssClass="m_btn_w2" Text="上报" OnClick="btnUP_Click" />
                    &nbsp;<input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">评定阶段：
                </td>
                <td>
                    <asp:TextBox ID="t_JD" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">评定等级：
                </td>
                <td>
                    <asp:DropDownList ID="t_DJ" Width="150px" runat="server">
                        <asp:ListItem>优良</asp:ListItem>
                        <asp:ListItem>合格</asp:ListItem>
                        <asp:ListItem>不合格</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">评定日期：
                </td>
                <td>
                    <asp:TextBox ID="t_RQ" Width="150px" runat="server" onfocus="WdatePicker()" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">备注：
                </td>
                <td>
                    <asp:TextBox ID="t_BZ" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
        </table>
        <input id="t_FID" runat="server" type="hidden" />
        <input id="t_ZT" runat="server" type="hidden" />
        <input id="t_GCBH" runat="server" type="hidden" />
    </form>
</body>
</html>
