<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZTBForm.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_ZTBForm" %>
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
    <asp:HiddenField ID="hfJLId" runat="server" />
    <asp:HiddenField ID="hfSGId" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                招投标信息
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
                    <input type="button" value="返 回" class="m_btn_w2" onclick="javascript: window.returnValue = '1'; window.close();" />
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
                    <asp:TextBox ID="txtXMMC" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtGCMC" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSDW" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目编号：
                </td>
                <td>
                    <asp:TextBox ID="txtXMBH" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                   
                </td>
                 <td class="t_r t_bg">
                    中标通知书编号：
                </td>
                <td>
                    <asp:TextBox ID="txtZBTZSBH" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td>
                    <asp:TextBox ID="txtJSGM" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                   
                </td>
                 <td class="t_r t_bg">
                    总面积（平方米）：
                </td>
                <td>
                    <asp:TextBox ID="txtZMJ" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    招标代理单位名称：
                </td>
                <td>
                    <asp:TextBox ID="txtZBDLDWMC" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    招标代理单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtZBDLDWZZJGDM" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            </table>
            <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg"  style="width:20px;text-align:center" rowspan="7">监理单位招投标信息</td>
                <td class="t_r t_bg">
                    招标方式：
                </td>
                <td>
                    <asp:DropDownList ID="ddlZBFS" runat="server" CssClass="required" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    招标类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlZBLX" runat="server" CssClass="required" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    中标单位：
                </td>
                <td>
                    <asp:TextBox ID="txtZBDW" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    中标单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtZBDWZZJGDM" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    中标企业资质证书号：
                </td>
                <td>
                    <asp:TextBox ID="txtZBQYZZZSH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    中标企业资质等级：
                </td>
                <td>
                    <asp:TextBox ID="txtZBQYZZDJ" runat="server"  CssClass="m_txt" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    中标金额(万元)：
                </td>
                <td>
                    <asp:TextBox ID="txtZBJE" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    中标日期：
                </td>
                <td>
                    <asp:TextBox ID="txtZBRQ" runat="server"  CssClass="m_txt Wdate required" onfocus="WdatePicker({skin:'whyGreen' })" Width="200"></asp:TextBox><tt>*</tt>   
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    总监理工程师姓名：
                </td>
                <td>
                    <asp:TextBox ID="txtZJLGCSXM" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    总监理工程师证件类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlZJLGCSZJLX" runat="server" CssClass="required" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    总监理工程师证件号：
                </td>
                <td>
                    <asp:TextBox ID="txtZJLGCSZJH" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    备案时间：
                </td>
                <td>
                    <asp:TextBox ID="txtBASJ" runat="server"  CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
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


            <tr>
                <td class="t_r t_bg" style="width:20px;text-align:center" rowspan="7">施工单位招投标信息</td>
                <td class="t_r t_bg">
                    招标方式：
                </td>
                <td>
                    <asp:DropDownList ID="ddlSGZBFS" runat="server" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    招标类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlSGZBLX" runat="server" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    中标单位：
                </td>
                <td>
                    <asp:TextBox ID="txtSGZBDW" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    中标单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtSGZBDWZZJGDM" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    中标企业资质证书号：
                </td>
                <td>
                    <asp:TextBox ID="txtSGZBQYZZZSH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    中标企业资质等级：
                </td>
                <td>
                    <asp:TextBox ID="txtSGZBQYZZDJ" runat="server"  CssClass="m_txt" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    中标金额(万元)：
                </td>
                <td>
                    <asp:TextBox ID="txtSGZBJE" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    中标日期：
                </td>
                <td>
                    <asp:TextBox ID="txtSGZBRQ" runat="server" CssClass="m_txt Wdate required" onfocus="WdatePicker({skin:'whyGreen' })" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目经理姓名：
                </td>
                <td>
                    <asp:TextBox ID="txtSGXMJLXM" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    项目经理证件类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlXMJLZJLX" runat="server" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目经理证件号码：
                </td>
                <td>
                    <asp:TextBox ID="txtSGXMJLZJHM" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    备案时间：
                </td>
                <td>
                    <asp:TextBox ID="txtSGBASJ" runat="server"  CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    附图及附件名称：
                </td>
                <td colspan="3">
                    <asp:HiddenField ID="hfSGUpadFile" runat="server" />
                    <asp:Button ID="Button1" runat="server" CssClass="m_btn_w4" Text="选择文件..." OnClientClick="return SelectFilesSGT()" /><br/>
                    <div style="width:100%;height:auto;text-align:left;margin-top:10px;" id="fileListSGT">
                        <%--<div>fdafdsa</div>--%>
                    </div>
                   <table width="70%" align="left" class="m_title">
                         <tr id="Tr1">
                            <td class="t_r" style="text-align:center;">附件名称</td><td class="t_r" style="text-align:center;">上传日期</td><td class="t_r" style="text-align:center;">操作</td>
                        </tr>
                         <asp:Literal ID="ltrSGText" runat="server"></asp:Literal>
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
     function SaveValidate() {
         var success = $("#form1").valid();
         if (success) {
             var array = new Array();
             var files = $("#fileList").find(".file");
             files.each(function (index, value) {
                 array.push($(this).attr("value"));
             });
             $("#hfUpadFile").val(array.join(','));

             var arraySGT = new Array();
             var fileSGT = $("#fileListSGT").find(".file");
             fileSGT.each(function (index, value) {
                 arraySGT.push($(this).attr("value"));
             });
             $("#hfSGUpadFile").val(arraySGT.join(','));
             return true;
         }
         return false;
     }
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
     function SelectFilesSGT() {
         var width = 600;
         var height = 400;
         sUrl = '../../../tiny_mce/plugins/ajaxfilemanager/filemanager.aspx?type=file&iseditor=1&p=<%=SecurityEncryption.DesEncrypt("../../|"+Session["FUserId"]+"|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now.AddHours(1)),"12345687")%>';
         var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
         if (rv != null && rv != 'undefined') {
             var items = rv.split('|');
             var paths = items[0].split('/');
             $("#fileListSGT").append("<div class='file' style='height:20px;' value='" + rv + "'>" + paths[paths.length - 1] + "&nbsp;&nbsp;<a href='javascript:void(0)' onclick='DelListFile(this)' style='color:red'>删 除</a></div>");
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
     $(function () {
         $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
     });
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

