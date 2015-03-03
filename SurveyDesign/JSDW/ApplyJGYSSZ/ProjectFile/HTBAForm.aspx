<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTBAForm.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_HTBAForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
     <link rel="stylesheet" href="../../../uploadify/uploadify.css" type="text/css" media="screen" />
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
    <script type="text/javascript" src="../../../uploadify/jquery.uploadify.js"></script>
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
                合同备案
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
                    <input type="button" class="m_btn_w2" value="返 回" onclick="javascript: window.returnValue = '1'; window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    合同备案编号：
                </td>
                <td>
                    <asp:TextBox ID="txtHTBABH" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
          
                </td>
                 <td class="t_r t_bg">
                    项目编号：
                </td>
                <td>
                    <asp:TextBox ID="txtXMBH" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    合同编号：
                </td>
                <td>
                    <asp:TextBox ID="txtHTBH" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    合同类别：
                </td>
                <td>
                    <asp:DropDownList ID="ddlHTLB" runat="server" CssClass="required" Width="200"></asp:DropDownList><tt>*</tt>
             <%--       <asp:TextBox ID="txtHTLB" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox>--%>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    合同金额(万元)：
                </td>
                <td>
                    <asp:TextBox ID="txtHTJE" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    建设规模：
                </td>
                <td>
                    <asp:TextBox ID="txtJSGM" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    合同签订日期：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtHTQDRQ" runat="server" CssClass="m_txt Wdate required" onfocus="WdatePicker({skin:'whyGreen' })" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    发包单位名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfFBDWQYBM" runat="server" />
                    <asp:TextBox ID="txtFBDWMC" runat="server" Enabled="false" CssClass="m_txt" Width="200"></asp:TextBox>
                    <asp:Button ID="btnFBDW" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchFBDW()" Text="选 择" onclick="btnFBDW_Click" /><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    发包单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtFBDWZZJGDM" runat="server" Enabled="false" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    承包单位名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfCBDWQYBM" runat="server" />
                    <asp:TextBox ID="txtCBDWMC" runat="server" Enabled="false" CssClass="m_txt" Width="200"></asp:TextBox>
                    <asp:Button ID="btnCBDW" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchCBDW()" Text="选 择" onclick="btnCBDW_Click" /><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    承包单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtCBDWZZJGDM" runat="server" Enabled="false" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    联合体承包单位名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfLHTQYBM" runat="server" />
                    <asp:TextBox ID="txtLHTCBDWMC" runat="server" Enabled="false" CssClass="m_txt" Width="200"></asp:TextBox>
                    <asp:Button ID="btnLHTCBDW" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchLHTCBDW()" Text="选 择" 
                        onclick="btnLHTCBDW_Click" /><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    联合体承包单位组织代码：
                </td>
                <td>
                    <asp:TextBox ID="txtLHTCBDWZZJGDM" Enabled="false" runat="server" CssClass="m_txt" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
        </table>
        </div>
        <div style="height:50px;"></div>
    </form>
</body>
</html>
<script type="text/javascript">
    function SearchLHTCBDW() {
        var result = showWinByReturn("ChooseQY.aspx?", 800, 500);
        if (result != undefined) {
            var str = result.split('|');
            $("#hfLHTQYBM").val(str[0]);
//            $("#txtLHTCBDWMC").val(str[1]);
            //            $("#txtLHTCBDWZZJGDM").val(str[2]);
            return true;
        }
        return false;
    }
    function SearchCBDW() {
        var result = showWinByReturn("ChooseQY.aspx?", 800, 500);
        if (result != undefined) {
            var str = result.split('|');
            $("#hfCBDWQYBM").val(str[0]);
//            $("#txtCBDWMC").val(str[1]);
            //            $("#txtCBDWZZJGDM").val(str[2]);
            return true;
        }
        return false;
    }
    function SearchFBDW() {
        var result = showWinByReturn("ChooseQY.aspx?", 800, 500);
        if (result != undefined) {
            var str = result.split('|');
            $("#hfFBDWQYBM").val(str[0]);
//            $("#txtFBDWMC").val(str[1]);
            //            $("#txtFBDWZZJGDM").val(str[2]);
            return true;
        }
        return false;
    }
    $(function () {
        $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
        var array = new Array();
        $("#fileInput").uploadify({
            height: 25,
            swf: '../../../uploadify/uploadify.swf',
            uploader: '../../../uploadify/uploadify.ashx',
            width: 80,
            buttonText: '上传文件',
            auto: true,
            uploadLimit: 5,
            removeCompleted: false, //成功后文件列表不自动消失
            multi: true,
            onUploadSuccess: function (file, data, response) {
                array.push(data);
            },
            onUploadStart: function (file) {
                $("#hfFileSuccess").val("0");//文件开始上传
            },
            onQueueComplete: function (queueData) {
                var value = "";
                var result = array.join(',');
                var uploadFile = $("#hfUpadFile").val();
                if ($.trim(uploadFile).length > 0) {
                    value = uploadFile + "," + result;
                } else {
                    value = result;
                }
                $("#hfFileSuccess").val("1");//文件已上传完成
                $("#hfUpadFile").val(value);
            }
        });
    });
    function SaveValidate() {
        var success = $("#form1").valid();
        if (success)
            return true;
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


