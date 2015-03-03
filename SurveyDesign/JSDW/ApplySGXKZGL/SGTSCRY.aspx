<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGTSCRY.aspx.cs" Inherits="JSDW_ApplySGXKZGL_SGTSCRY" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptidfalse2.ascx" TagName="govdeptid1" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>施工图审查人员信息</title>
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
            var type = document.getElementById("t_Type").value;
            var qybm = "";
            if (type == "1") {
                qybm = '<%=ViewState["SC"] %>';
            } else if (type == "2") {
                qybm = '<%=ViewState["KC"] %>';
            } else if (type == "3") {
                qybm = '<%=ViewState["SJ"] %>';
            }
            if (qybm != null && qybm != "") {
                var url = "../project/EmpListSel.aspx";
                url += "?qybm=" + qybm;
                var pid = showWinByReturn(url, 1000, 600);
                if (pid != null && pid != '') {
                    $("#" + tagId).val(pid);
                    __doPostBack(obj.id, '');
                }
            } else {
                alert('请先选择所属单位类型！');
                return;
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
        <asp:HiddenField  runat="server" ID="hf_FAppId" Value="" />
        <asp:HiddenField  runat="server" ID="hf_FId" Value="" />
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
        <table width="98%" align="center" class="m_bar">
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
                        <input id="txtFId" type="hidden" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>                
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    所属单位类型：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_Type" runat="server" CssClass="m_txt" Width="203px" >
                        <asp:ListItem Value="1">施工图审查机构</asp:ListItem>
                        <asp:ListItem Value="2">勘察单位</asp:ListItem>
                        <asp:ListItem Value="3">设计单位</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width:18.8%;">
                    所属单位名称：
                </td>
                <td colspan="1" style="width:auto;">
                    <asp:TextBox ID="t_DWMC" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    所属单位组织机构代码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_DWZZJGDM"  runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    人员姓名：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_RYXM" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                    <input type="hidden"  runat="server" ID="t_RYId" value="" />
                    <input type="hidden"  runat="server" ID="t_FEntId" value="" />
                    <asp:Button ID="btnAdd" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEmp(this,'t_RYId');"
                    UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEmp_Click" Style="margin-bottom: 4px;margin-left:5px;" />
                </td>
                <td class="t_r t_bg">
                    承担角色：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_CDJS" runat="server" CssClass="m_txt" Width="203px"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" >
                    证件类型：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZJLX" runat="server" CssClass="m_txt" Width="203px"></asp:DropDownList><tt>*</tt>
                    
                </td>
                <td class="t_r t_bg">
                    证件号码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZJHM" onblur="CheckSFZHM(this);" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    注册类型及等级：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_ZCLXJDJ" runat="server" CssClass="m_txt" Width="203px" ></asp:DropDownList><tt>*</tt>
                </td>
               
            </tr>
            
        </table>
        
    </div>
    
    </form>
</body>
</html>
