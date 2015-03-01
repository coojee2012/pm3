<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fileMange.aspx.cs" Inherits="JNCLEnt_ApplyInfo_fileMange" %>

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
            var fid = document.getElementById("t_fappid").value;
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
                <th colspan="5">附件上传
                </th><asp:Button ID="btnQuery" Style="display: none;" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" />
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">企业营业执照：
                </td>
                <td>
                    <input id="btnUP1" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "3000");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">代理商的代理合同：
                </td>
                <td>
                    <input id="btnUP2" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "3001");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">设计施工验收技术规程：
                </td>
                <td>
                    <input id="btnUP3" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "3002");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">法定检测机构出具的有效期内的产品型式检验报告：
                </td>
                <td>
                    <input id="btnUP4" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "3003");' />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 280px; text-align: center;">质量保证体系认证证书：
                </td>
                <td>
                    <input id="btnUP5" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "3003");' />
                </td>
            </tr>
        </table>
        <input id="t_fappid" runat="server" type="hidden" />
    </form>
</body>
</html>
