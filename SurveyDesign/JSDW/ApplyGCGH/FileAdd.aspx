<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileAdd.aspx.cs" Inherits="JSDW_ApplyYDGH_FileAdd" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
</head>
<body>
    <form id="form2" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="2">上传
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" Text="保存" class="m_btn_w2" OnClick="btnSave_Click" />
                    <input id="btnGetBack" class="m_btn_w2" onclick="window.returnValue = '1'; window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td><%--parent.document.frames('fileupload').parent.location.reload(); --%>
            </tr>
        </table>
        <table align="center" class="m_table" style="width: 98%">
            <tr>
                <td class="t_r t_bg">附件类型：
                </td>
                <td>
                    <asp:Literal ID="ltrtypeName" runat="server"></asp:Literal>
                </td>
            </tr>
            <%--<tr id="tr1" runat="server">
                <td class="t_r t_bg">附件名称：
                </td>
                <td>
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="300"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>--%>
            <tr id="tr2" runat="server">
                <td class="t_r t_bg">上传附件：
                </td>
                <td>
                   <asp:HiddenField ID="hfUpadFile" runat="server" />
                    <asp:Button ID="btnUpLoad" runat="server" CssClass="m_btn_w4" Text="选择文件..." OnClientClick="return SelectFiles()" /><br/>
                    <div style="width:100%;height:auto;text-align:left;margin-top:10px;" id="fileList">
                        <%--<div>fdafdsa</div>--%>
                    </div>
                    <table width="90%" align="left" class="m_title">
                         <tr id="Tr1">
                            <td class="t_r" style="text-align:center;">附件名称</td><td class="t_r" style="text-align:center;">上传日期</td><td class="t_r" style="text-align:center;">操作</td>
                        </tr>
                         <asp:Literal ID="ltrFile" runat="server"></asp:Literal>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#btnSave").click(function () {
            var array = new Array();
            var files = $("#fileList").find(".file");
            files.each(function (index, value) {
                array.push($(this).attr("value"));
            });
            $("#hfUpadFile").val(array.join(','));
            var file = $("#hfUpadFile").val();
            if ($.trim(file).length == 0) {
                alert("未上传新文件");
                return false;
            }
            return true;
        });
    });
    function SelectFiles() {
        var width = 600;
        var height = 400;
        sUrl = '../../tiny_mce/plugins/ajaxfilemanager/filemanager.aspx?type=file&iseditor=1&p=<%=SecurityEncryption.DesEncrypt("../../|"+Session["FUserId"]+"|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now.AddHours(1)),"12345687")%>';
        var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
        if (rv != null && rv != 'undefined') {
            var items = rv.split('|');
            var paths = items[0].split('/');
            $("#fileList").append("<div class='file' style='height:20px;' value='" + rv + "'>" + paths[paths.length - 1] + "&nbsp;&nbsp;<a href='javascript:void(0)' onclick='DelListFile(this)' style='color:red'>删 除</a></div>");
            return false;
        }
        return false;
    }
    function DelListFile(obj) {
        $(obj).parent("div").remove();
    }
    function DelFile(Id, $obj) {
        if (confirm("确认删除")) {
            var url = "../../uploadify/DelFile.ashx";
            $.ajax({
                type: 'post',
                url: url,
                data: { Id: Id },
                success: function (result) {
                    if (result == "1")//成功
                    {
                        $($obj).parent("td").parent("tr").remove();
                    } else {
                        alert("删除失败");
                    }
                }
            });
        }
    }
    function downLoad(filePath, fileName) {
        window.location.href = '../ApplyXZYJS/DownLoad.aspx?filePath=' + filePath + "&fileName=" + fileName;
    }
</script>
