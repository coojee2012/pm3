<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyFileUp.aspx.cs" Inherits="GFEnt_ApplyFileUp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function getFilUp(url, type) {
            var fid = document.getElementById("t_YWBM").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?FAppId=" + fid + "&type=" + type, 550, 400);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">文件上传
                </th>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">工法内容材料：
                </td>
                <td>
                    <input id="btnUP" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1000");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">完成单位意见、无争议声明相关附件：
                </td>
                <td>
                    <input id="btnUP1" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1001");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">工程应用证明相关附件：
                </td>
                <td>
                    <input id="btnUP2" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1002");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">工法成熟可靠性说明文件：
                </td>
                <td>
                    <input id="btnUP3" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1003");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">关键技术评估意见：
                </td>
                <td>
                    <input id="btnUP4" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1006");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">科技成果获奖证明相关附件：
                </td>
                <td>
                    <input id="btnUP5" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1004");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">专业技术专利证明文件：
                </td>
                <td>
                    <input id="btnUP6" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1007");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">经济效益证明：
                </td>
                <td>
                    <input id="btnUP7" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1005");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">工法操作要点照片（10到15张）：
                </td>
                <td>
                    <input id="btnUP8" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1008");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">技术转让的证明材料：
                </td>
                <td>
                    <input id="btnUP9" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1009");' />
                </td>
            </tr>
        </table>
        <asp:Button ID="btnQuery" Style="display: none;" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" />
        <input id="t_YWBM" runat="server" type="hidden" />
    </form>
</body>
</html>
