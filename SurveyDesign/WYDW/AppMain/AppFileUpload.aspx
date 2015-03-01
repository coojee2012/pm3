<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppFileUpload.aspx.cs" Inherits="WYDW_AppMain_ApplyFileUpload" %>
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
                <td class="t_r t_bg" style="width: 280px; text-align: center;">项目合同上传：
                </td>
                <td>
                    <input id="btnUP" runat="server" type="button" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx", "1000");' />
                </td>
            </tr>
        </table>
        
        <input id="t_YWBM" runat="server" type="hidden" />
    </form>
</body>
</html>
