﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Online.aspx.cs" Inherits="JSDW_ApplyAQJDBA_Online" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function selEmp(obj, tagId) {
            var zw = document.getElementById("t_XMZW").value;;
            var priitemid = document.getElementById("hdfprjitemid").value;//当前工程编号
            if (tagId == "t_SGRYId") {
                qybm = document.getElementById("t_XMZW").value;
            } else if (tagId == "t_JLRYId") {
                qybm = document.getElementById("t_JLId").value;
            } else if (tagId == "t_FHumanId") {
                qybm = document.getElementById("t_FHumanId").value;
            }
            if (qybm != null && qybm != "") {
                //var url = "../project/EmpListSel.aspx";
                var url = "../project/EmpListSelAqjd.aspx";
                url += "?qybm=" + qybm + "&emptype=aqjd" + "&FPrjItemId=" + priitemid; //安全监督的人员不能选择证书过期的人员
                var pid = showWinByReturn(url, 1000, 600);
                if (pid != null && pid != '') {
                    $("#" + tagId).val(pid);
                    __doPostBack(obj.id, '');
                }
            } else {
                alert('请先选择单位！');
                return;
            }

        }
        function SelectFiles() {
            var width = 600;
            var height = 400;
            sUrl = '<%=ProjectBLL.RBase.GetSysObjectName("FileServerPath") %>tiny_mce/plugins/ajaxfilemanager/filemanager.aspx?type=file&iseditor=1&p=<%=SecurityEncryption.DesEncrypt("../../|"+Session["FUserId"]+"|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now.AddHours(1)),"12345687")%>';
            var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
            if (rv != null && rv.split('|')[0] != 'undefined') {
                $('#t_FilePath').val(rv.split('|')[0]);
                $('#t_Size').val(rv.split('|')[1]);
                $("#btnQuery").click();
                return true;
            }
            return false;
        }
    </script>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divSetup2" runat="server">
          <input type="hidden"  runat="server" ID="hdfprjitemid" value="" />
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click"
                        OnClientClick="return checkInfo();" />
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    姓名：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FHumanName" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                    <tt>*</tt>
                    <input type="hidden"  runat="server" ID="t_FHumanId" value="" />
                    <asp:Button ID="btnAdd" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEmp(this,'t_FHumanId');"
                    UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEmpSG_Click" Style="margin-bottom: 4px;margin-left:5px;" />
                </td>
                <td class="t_r t_bg">
                    性别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FSex" runat="server" CssClass="m_txt" Width="202px">
                        <asp:ListItem Value="1">男</asp:ListItem>
                        <asp:ListItem Value="2">女</asp:ListItem>
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    出生日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FBirthDay" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    项目职位：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_XMZW" runat="server" CssClass="m_txt" Width="202px">
                    </asp:DropDownList>
                    <tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    证件类型：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZJLX" runat="server" CssClass="m_txt" Width="195px">
                    </asp:DropDownList>
                    <tt>*</tt></td>
                <td class="t_r t_bg">
                    证件号码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZJHM" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="195px"></asp:TextBox>
                    <tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    所在企业：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_SZQY" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                    <tt>*</tt></td>
                <td class="t_r t_bg">
                    职称：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZC" runat="server" CssClass="m_txt" Width="202px">
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    职称专业：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZCZY" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    职称证书号：
                </td>
                <td colspan="1">
                   <asp:TextBox ID="t_ZCZSH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    最高学历：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZGXL" runat="server" CssClass="m_txt" Width="202px">
                        
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    注册专业：
                </td>
                <td colspan="1">
                   <asp:TextBox ID="t_ZHUCZY" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    注册证书号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZHUCZSH" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    安全考核合格证号：
                </td>
                <td colspan="1">
                   <asp:TextBox ID="t_AQKHHGZH" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                </td>
            </tr>   
            <tr>
                <td class="t_r t_bg">
                    移动电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Mobile" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                </td>
            </tr>   
            <tr>
                <td class="t_r t_bg">
                上传照片：
                </td>
                <td style="padding: 4px; line-height: 28px;">
                    
                    <asp:FileUpload ID="file_FPhoto" runat="server" />
                    &nbsp;
                    <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" />
                    
                </td>
                <td class="t_r t_bg">
                    照片：
                </td>
                <td colspan="3">
                    
                    <asp:Image ID="image_FPhoto" runat="server" Height="150px" Width="150px" />
                    
                </td>
            </tr>   
        </table>
    </div>
    </form>
</body>
</html>