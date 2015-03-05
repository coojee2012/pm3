<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZJBHHZMForm.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_ZJBHHZMForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        #form1 label.errorMsg 
			{ 
			color:Red; 
			font-size:13px; 
			margin-left:5px;
			}
         .m_btn_w2 {
             height: 21px;
         }
    </style>
     <script type="text/javascript" src="../../../script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../../script/default.js"></script>
    <script type="text/javascript" src="../../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../../script/messages_zh.js"></script>
    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfId" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                资金保函或证明
            </th>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" OnClientClick="return SaveValidate()" OnClick="btnSave_Click"/>
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    项目名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtXMMC" Enabled="false" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    工程名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtGCMC" Enabled="false" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSDW" Enabled="false" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td rowspan="4" class="t_r t_bg" width="150px">资金保函或证明</td>
                <td class="t_r t_bg" colspan="2" style="text-align:left"><asp:CheckBox ID="cbIsBLDB" runat="server" Text="已办理担保书" /></td>
            </tr>
            <tr>
                <td class="t_r t_bg" colspan="2" style="text-align:left"><asp:TextBox ID="txtDBSM" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
			<tr>
                <td class="t_r t_bg">甲方</td>
                <td><asp:TextBox ID="txtJF" runat="server" CssClass="required" Width="200px"></asp:TextBox><tt>*</tt></td>
            </tr>
			<tr>
                <td class="t_r t_bg">乙方</td>
                <td><asp:TextBox ID="txtYF" runat="server" CssClass="required" Width="200px"></asp:TextBox><tt>*</tt></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" width="150px;">
                    附图及附件名称：
                </td>
                <td colspan="3">
                    <asp:HiddenField ID="hfUpadFile" runat="server" />
                    <asp:Button ID="btnUpLoad" runat="server" CssClass="m_btn_w4" Text="选择文件..." OnClientClick="return SelectFiles()" /><br/>
                    <div style="width:100%;height:auto;text-align:left;margin-top:10px;" id="fileList">
                        <%--<div>fdafdsa</div>--%>
                    </div>
                    <table width="70%" align="left" class="m_title">
                         <tr id="firstFile">
                            <td class="t_r" style="text-align:center;">附件名称</td><td class="t_r" style="text-align:center;">上传日期</td><td class="t_r" style="text-align:center;">操作</td>
                        </tr>
                         <asp:Literal ID="ltrFile" runat="server"></asp:Literal>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        <div style="height:50px;"></div>
    </form>
</body>
</html>
<script type="text/javascript">

    $(function () {
        $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
    });
    function SelectFiles() {
        var width = 600;
        var height = 400;
        sUrl = '../../../tiny_mce/plugins/ajaxfilemanager/filemanager.aspx?type=file&iseditor=1&p=<%=SecurityEncryption.DesEncrypt("../../|"+Session["FUserId"]+"|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now.AddHours(1)),"12345687")%>';
        var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
        if (rv != null && rv != 'undefined') {
            var items = rv.split('|');
            var paths = items[0].split('/');
            $("#fileList").append("<div class='file' style='height:20px;' value='" + rv + "'>" + paths[paths.length - 1] + "&nbsp;&nbsp;<a href='javascript:void(0)' onclick='DelListFile(this)' style='color:red'>删 除</a></div>");
            //var file = $("#hfUpadFile").val();
            //file += rv + ",";
            //$("#hfUpadFile").val(file);
            // array.push(rv);
            return false;
        }
        return false;
    }
    function DelListFile(obj) {
        $(obj).parent("div").remove();
    }
    function SaveValidate() {
        var success = $("#form1").valid();
        if (success) {
            var array = new Array();
            var files = $("#fileList").find(".file");
            files.each(function (index, value) {
                array.push($(this).attr("value"));
            });
            $("#hfUpadFile").val(array.join(','));
            return true;
        }
        else
            return false;
    }
    function DelFile(Id, $obj) {
        if (confirm("确认删除")) {
            var url = "../../../uploadify/DelFile.ashx";
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
</script>


