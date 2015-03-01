<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyReward.aspx.cs" Inherits="GFEnt_ApplyEnt_ApplyReward" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function getFilUp(url) {
            var fid = document.getElementById("t_YWBM").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?FAppId=" + fid + "&type=1004", 550, 400);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">科技成果奖励情况
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r" style="padding-right: 10px;">
                    <input type="hidden" id="hidd_FLinkId" runat="server" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
                    <asp:Button ID="btnQuery" Style="display: none;" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" /></td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center" hight="500px">
            <tr style="text-align: right;">
                <td class="t_r t_bg" colspan="2">科技成果获奖证明相关附件               
                    <input id="btnUP" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">工法关键技术获科技成果奖励情况
                </td>
                <td style="height: 400px; width: 82%">
                    <asp:TextBox ID="t_JLQK" runat="server" CssClass="m_txt" Width="100%" Height="400px" TextMode="MultiLine"></asp:TextBox>
                    <input type="hidden" id="p_FId" runat="server" />
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
