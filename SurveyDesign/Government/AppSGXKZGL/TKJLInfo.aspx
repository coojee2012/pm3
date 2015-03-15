<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TKJLInfo.aspx.cs" Inherits="JSDW_APPSGXKZGL_YZInfo" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>踏勘记录信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            var filepath = $("#t_FilePath").val();
            //alert(filepath);
            var patchs = filepath.split("|");
            for (var i = 0; i < patchs.length; i++) {
                if (patchs[i] != '') {
                    var filename = patchs[i].split('/');
                    filename = filename[filename.length - 1];

                    var trHTML = "<tr id='id" + i + "'><td>" + filename + "</td>";
                   
                    trHTML += "<td align=\"center\"><a href=\"javascript:removeRow('" + patchs[i] + "','id" + i + "');\">删除</a></td></tr>";

                    $("#fileTables").append(trHTML);//在table最后面添加一行
                }
                

            }
            

        });
        var tmpid = 0;
        function SelectFiles() {
            var width = 600;
            var height = 400;
            sUrl = '<%=ProjectBLL.RBase.GetSysObjectName("FileServerPath") %>tiny_mce/plugins/ajaxfilemanager/filemanager.aspx?type=file&iseditor=1&p=<%=SecurityEncryption.DesEncrypt("../../|"+Session["FUserId"]+"|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now.AddHours(1)),"12345687")%>';
            var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
            if (rv != null && rv.split('|')[0] != 'undefined') {
                var fileInfo = rv.split('|');
                $('#t_FilePath').val($('#t_FilePath').val()+fileInfo[0]+"|");
                var filename = fileInfo[0].split('/');
                filename = filename[filename.length - 1];
                $('#t_Size').val(fileInfo[1]);
                var filename2 = 'id' + tmpid;
                tmpid += 1;
                var trHTML = "<tr id='" + filename2 + "'><td>" + filename + "</td>";
               // trHTML += "<td align=\"center\">" + (fileInfo[1] / 1024).toFixed(2) + "</td>";
                trHTML += "<td align=\"center\"><a href=\"javascript:removeRow('" + fileInfo[0] + "','" + filename2+ "');\">删除</a></td></tr>";
               
                $("#fileTables").append(trHTML);//在table最后面添加一行
                //$("#btnFileUpload").click();
                return true;
            }
            return false;
        }

        function removeRow(filePath,id) {
            var self = this;
            //alert(id);
            //alert($("#" + id));
            var filePathnew = $("#t_FilePath").val();
            filePathnew = filePathnew.replace(filePath + '|', '');
            $("#t_FilePath").val(filePathnew);
            //alert(filePathnew);
            $("#"+id).remove();
        }

        function checkInfo() {
            return AutoCheckInfo();
        }
        function selEmp(obj) {
            var fAppId = document.getElementById("t_FAppId").value;
            var rv = showWinByReturn('EmpSel.aspx?FAppId=' + fAppId, 600, 500);           
            if (pid != null && pid != '') {
                var list = rv.toString().split("@");
                $("#t_FEmpId").val(list[0]);
                $("#t_CZR").val(list[1]);
                 
             }
         }
    </script>
    <base target="_self">
    </base>
    <style type="text/css">
        .modalDiv { position: absolute; top: 1px; left: 1px; width: 100%; height: 100%; z-index:9999; background-color:gray; opacity:.50; filter: alpha(opacity=50); }
        .m_txt {}
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <input type="hidden"  runat="server" ID="h_FAppId" value="" />
        <input type="hidden"  runat="server" ID="t_FId" value="" />
        <input type="hidden"  runat="server" ID="t_FEmpId" value="" />
        <input type="hidden"  runat="server" ID="t_FEntId" value="" />
        <input type="hidden"  runat="server" ID="t_FPrjId" value="" />
        <input type="hidden"  runat="server" ID="t_FPrjItemId" value="" />
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="up_Main" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv" style="display:none;"> 
                <div style="position:absolute;left:40%;top:50%;background-color:peru;border:solid 3px red;">
                    <table  align="center">
                    <tr>
                    <td ><h1>正在保存数据</h1></td>
                    <td><img src="../../image/load2.gif" alt="请稍候"/></td>
                    </tr>
                                    
                    </table>
                </div>
                    </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

            
    <div id="divSetup2" runat="server">
        <table width="95%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                         <ContentTemplate>
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                            OnClientClick="return checkInfo();" />  
                        </ContentTemplate>
                    </asp:UpdatePanel>                
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="95%" align="center">
             <tr>
                <td class="t_r t_bg" >
                    记录时间：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JLSJ" runat="server"  onfocus="WdatePicker()" CssClass="m_txt" Width="200px" MaxLength="30" ></asp:TextBox>
                </td>
                <td class="t_r t_bg" >
                    记录人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JLR" runat="server" CssClass="m_txt" Width="200px" MaxLength="30" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg" >
                    踏勘时间：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_TKSJ" runat="server"  onfocus="WdatePicker()" CssClass="m_txt" Width="200px" MaxLength="30" ></asp:TextBox>
                </td>
                <td class="t_r t_bg" >
                    踏勘部门：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_TKBM" runat="server" CssClass="m_txt" Width="200px" MaxLength="30" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg" >
                    踏勘人员：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_TKRY" runat="server" CssClass="m_txt" Width="200px" MaxLength="30" ></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td class="t_r t_bg" >
                    踏勘情况：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_TKQK" runat="server" CssClass="m_txt" Width="98%" MaxLength="30" Height="48px" ></asp:TextBox>
                </td>
                
            </tr>
             <tr>
                <td class="t_r t_bg" >
                    备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_BZ" runat="server" CssClass="m_txt" Width="98%" MaxLength="30" Height="48px" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg" >上传附件：</td>
                <td colspan="3">
                <input type="hidden" id="t_FilePath" runat="server" />
                <input type="hidden" id="t_FileType" runat="server" />
                <input type="hidden" id="t_Size" runat="server" />
                <asp:Literal ID="name" runat="server" Text=""></asp:Literal>
                <br />
 <asp:Button ID="btnFileUpload" runat="server" OnClick="btnFileUpload_Click" Style="display: none;" />
                <input id="btnSelect" runat="server" class="m_btn_w6" onclick="SelectFiles();" type="button"
                    value="选择文件..." />
                </td>
                
            </tr>
           
        </table>
        <table id="fileTables" class="m_table" width="95%" align="center">
             <tr>
                 <td class="t_c t_bg" align="center" style="width:80%;"><b>文件名</b></td>    
                 <td class="t_c t_bg" align="center" style="width:20%;"><b>操作</b></td>
             </tr>
            
                 </table>
    </div>
    
    </form>
</body>
</html>
