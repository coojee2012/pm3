<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="PropertyEntApp_Common_FileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../../script/jquery.js"></script>
    <base target="_self"></base>
    <title>上传附件</title>
    <style>
        * {
            font-size: 12px;
        }
    </style>
    <script language="javascript">
        function Exit(result) {
            window.returnValue = result;
            window.close();
        }

        function fnChange() {
            if (document.all["txtTitle"].value != "") return;
            var sFile = document.all["File1"].value;
            var iIndex = sFile.lastIndexOf("\\");
            var sFileName = sFile.substring(iIndex + 1, sFile.length);
            document.all["txtTitle"].value = sFileName;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" width="98%" class="marTop">
            <tr>
                <td class="td7"></td>
                <td class="td8" width="16"></td>
                <td class="td8 txt26 txt20 txt28">上传附件</td>
                <td class="td6"></td>
            </tr>
        </table>
        <table class="tdCenter" align="center" width="98%">
            <tr>
                <td align="left" class=" txt25 txt20" style="height: 30px">
                    <!-- 请上传jpg格式文件，大小不超过300K。 -->
                    上传的文件名中不能包含字符：+、？、%、#、&、空格！</td>
            </tr>
            <tr>
                <td valign="middle" class=" td5" style="height: 30px">
                    <div>选择附件：<input id="File1" name="File1" type="file" runat="server" class="cTextBox1" style="width: 280px;" size="20" onchange="fnChange();" accept="jpg,pdf" /></div>

                    <div style="margin-top: 5px;">附件名称：<input id="txtTitle" class="m_txt" name="txtTitle" type="text" style="width: 280px;" runat="server" /></div>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px; padding-bottom: 10px">
                    <asp:Button ID="btnUp" runat="server" Text="上传" OnClick="btnUp_Click" CssClass="m_btn_w2" />
                    <input id="Button1" class="m_btn_w2" type="button" value="返回" onclick="Exit(); window.close();" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>


