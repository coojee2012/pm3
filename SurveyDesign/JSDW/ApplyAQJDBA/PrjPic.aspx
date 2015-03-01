<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjPic.aspx.cs" Inherits="JSDW_ApplyAQJDBA_PrjPic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上传</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        function SelectFiles() {
            var width = 600;
            var height = 400;
            sUrl = '<%=ProjectBLL.RBase.GetSysObjectName("FileServerPath") %>tiny_mce/plugins/ajaxfilemanager/filemanager.aspx?type=file&iseditor=1&p=<%=SecurityEncryption.DesEncrypt("../../|"+Session["FUserId"]+"|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now.AddHours(1)),"12345687")%>';
            var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
            if (rv != null && rv.split('|')[0] != 'undefined') {
                $('#t_FFilePath').val(rv.split('|')[0]);
                $('#t_FSize').val(rv.split('|')[1]);
                $("#btnQuery").click();
                return true;
            }
            return false;
        }

        function check() {
            if ($("#t_FFilePath").val() == "") {
                alert("请选择文件");
                return false;
            }
            if ($("#t_FName").val() == "") {
                alert("请填写附件名称");
                $("#t_FName").focus();
                return false;
            }
            return trim;
        }

        function showTr2() {
            $("tr[name=t_r2]").show();
            $("tr[name=t_r1]").hide();
        }

        function showTr1() {
            $("tr[name=t_r1]").show();
            $("tr[name=t_r2]").hide();
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                上传
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" class="m_btn_w2" OnClick="btnSave_Click"/>
                <input id="btnGetBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />&nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table align="center" class="m_table" style="width: 98%">
        <tr>
            <td class="t_r t_bg">
                资料名称：
            </td>
            <td>
                <asp:TextBox ID="t_FFileName" runat="server" CssClass="m_txt" Width="300"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                文件说明：
            </td>
            <td>
                <asp:TextBox ID="t_FRemarks" runat="server" CssClass="m_txt" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr name="t_r2">
            <td class="t_r t_bg">
                文件信息：
            </td>
            <td>
                <asp:Literal ID="name1" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr name="t_r1">
            <td class="t_r t_bg">
                上传附件：
            </td>
            <td style="padding: 4px; line-height: 28px;">
                <input type="hidden" id="t_FFilePath" runat="server" />
                <input type="hidden" id="t_FFileType" runat="server" />
                <input type="hidden" id="t_FSize" runat="server" />
                <asp:Literal ID="name" runat="server" Text="<tt>请选择文件</tt>"></asp:Literal>
                <br />
                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Style="display: none;" />
                <input id="btnSelect" runat="server" class="m_btn_w6" onclick="SelectFiles();" type="button"
                    value="选择文件..." />
            </td>
        </tr>
        <tr name="t_r2">
            <td class="t_r t_bg">
                附件：
            </td>
            <td>
                <asp:Literal ID="fileName" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
