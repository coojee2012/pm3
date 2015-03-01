<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addPatent.aspx.cs" Inherits="GFEnt_ApplyEnt_addPatent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
    <script type="text/javascript">
        function getFilUp(url) {
            var fid = document.getElementById("t_YWBM").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?FAppId=" + fid + "&type=1007", 550, 400);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">专利情况维护
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                    <asp:Button ID="btnQuery" Style="display: none;" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" /></td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">专利名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_ZLMC" Width="350px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">专利号：
                </td>
                <td>
                    <asp:TextBox ID="t_ZLH" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r t_bg">专利权人：
                </td>
                <td>
                    <asp:TextBox ID="t_ZLQR" runat="server" Width="150px" CssClass="m_txt"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">简介内容：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_JJNR" Width="350px" runat="server" CssClass="m_txt" Height="100px" TextMode="MultiLine"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">专利技术证明文件：
                </td>
                <td colspan="3">
                    <input type="button" id="btnUP" runat="server" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx");' />
                </td>
            </tr>
        </table>
        <input id="t_YWBM" runat="server" type="hidden" />
        <input id="t_FBaseInfoId" runat="server" type="hidden" />
        <input id="t_FSystemId" runat="server" type="hidden" />
        <input id="t_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
