<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YDGHForm.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_YDGHForm" %>
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
    <asp:HiddenField ID="hfSrouce" runat="server" /><!--来源 True标准库 false新增-->
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                建设用地规划许可证
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
                    办理选项：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlIsTrans" runat="server" Width="200">
                        <asp:ListItem Value="1">补填</asp:ListItem>
                        <asp:ListItem Value="2">不需要办理</asp:ListItem>
                        <asp:ListItem Value="3">以后补办</asp:ListItem>
                        <asp:ListItem Value="4">已办</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="LY">
                <td class="t_r t_bg">
                    理由：
                </td>
                <td colspan="3"><asp:TextBox ID="txtLY" runat="server" CssClass="m_txt" Width="500" Height="40" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtXMMC" runat="server" Enabled="false" CssClass="m_txt" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    建设地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSDZ" runat="server" CssClass="m_txt" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    用地单位：
                </td>
                <td>
                    <asp:TextBox ID="txtYDDW" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    用地面积（㎡）：
                </td>
                <td>
                    <asp:TextBox ID="txtYDMJ" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td>
                    <asp:TextBox ID="txtJSGM" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    其他：
                </td>
                <td>
                    <asp:TextBox ID="txtQT" runat="server" CssClass="m_txt" Width="100"></asp:TextBox>
                    <asp:DropDownList ID="ddlType" runat="server" Visible="false">
                        <asp:ListItem Value="0">请选择</asp:ListItem>
                        <asp:ListItem Value="1">千米</asp:ListItem>
                        <asp:ListItem Value="2">米</asp:ListItem>
                        <asp:ListItem Value="3">立方米</asp:ListItem>
                        <asp:ListItem Value="4">座</asp:ListItem>
                        <asp:ListItem Value="5">其它</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    用地规划许可证编号：
                </td>
                <td>
                    <asp:TextBox ID="txtYDGHXKZBH" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    用地性质：
                </td>
                <td>
                    <asp:DropDownList ID="ddlYDXZ" runat="server" CssClass="m_txt" Width="200px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    发证机关：
                </td>
                <td>
                    <asp:TextBox ID="txtFZJG" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                   
                </td>
                 <td class="t_r t_bg">
                    发证日期：
                </td>
                <td>
                    <asp:TextBox ID="txtFZRQ" runat="server"  CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="200"></asp:TextBox>
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
        </table>
        </div>
        <div style="height:50px;"></div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
        if ($("#hfSrouce").val() == "True") {//来源标准库
            $("#ddlIsTrans").val('4');
            $("#ddlIsTrans").attr("disabled", "disabled");
            $("#LY").hide();
        } else {
            $("#ddlIsTrans").children().eq(3).remove();
            //var state = $("#hfState").val(); //1 已提交  0未提交
            //if (state == "0") {
            var isTrans = $("#ddlIsTrans").val();
            if (isTrans == "1") {
                $("#txtLY").val('');
                $("#txtLY").attr("disabled", "disabled");
                $("#LY").hide();
            } else {
                $("#txtLY").removeAttr("disabled");
                $("#LY").show();
            }
            // }
            $("#ddlIsTrans").change(function () {
                var isTrans = $(this).val();
                if (isTrans == "1") {
                    $("#txtLY").val('');
                    $("#txtLY").attr("disabled", "disabled");
                    $("#LY").hide();
                } else {
                    $("#txtLY").removeAttr("disabled");
                    $("#LY").show();
                }
            });
        }
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
        var isTrans = $("#ddlIsTrans").val();
        var LY = $("#txtLY").val();
        if (isTrans != "1") {
            if ($.trim(LY).length == 0) {
                alert("请输入理由");
                return false;
            }
            var items = $("#form1").find("input[type=text]");
            items.each(function (index, element) {
                $(element).removeClass("required");
            });
            var ddlItems = $("#form1").find("input[type=text]");
            ddlItems.each(function (index, element) {
                $(element).removeClass("required");
            });
            return true;
        }
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
