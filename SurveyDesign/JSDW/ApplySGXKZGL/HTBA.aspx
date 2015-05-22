<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTBA.aspx.cs" Inherits="JSDW_ApplySGXKZGL_HTBA" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptidfalse2.ascx" TagName="govdeptid1" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>合同备案</title>
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
        function selEnt(obj, tagId) {           
            //var url = "../project/EntListSel.aspx?e=1";
            var url = "../project/EntListSelsql.aspx?e=1";
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
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
                    合同备案编号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_HTBABH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    项目编号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ProjectNo" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                    <input id="t_FPrjItemId" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width:18.8%;">
                    合同编号：
                </td>
                <td colspan="1" style="width:29%;">
                    <asp:TextBox ID="t_HTBH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                    
                </td>
                <td class="t_r t_bg">
                    合同金额：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_HTJE" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>(万元)
                    <tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_ConstrScale" runat="server" CssClass="m_txt" Width="705px" Height="40px" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
                <td class="t_r t_bg">
                    合同签订日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_HTQDTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    合同类别：
                </td>
                <td colspan="1">
                     <asp:DropDownList ID="t_HTLB" runat="server" CssClass="m_txt" Width="200px"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    发包单位名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FBDWMC" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                    <input type="hidden"  runat="server" ID="t_FBDWId" value="" />
                    <asp:Button ID="Button1" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_FBDWId');"
                    UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntFB_Click" Style="margin-bottom: 4px;margin-left:5px;" />
                </td>
                <td class="t_r t_bg">
                    发包单位组织机构代码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FBDWZZJGDM"  runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    承包单位名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_CBDWMC" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                    <input type="hidden"  runat="server" ID="t_CBDWId" value="" />
                    <asp:Button ID="Button2" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_CBDWId');"
                    UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntCB_Click" Style="margin-bottom: 4px;margin-left:5px;" />
                </td>
                <td class="t_r t_bg">
                    承包单位组织机构代码：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="t_CBDWZZJGDM" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联合体承包单位名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_LHTCBDWMC" runat="server" CssClass="m_txt" Width="85%" Enabled="false" Height="45px" TextMode="MultiLine"></asp:TextBox>
                    <input type="hidden"  runat="server" ID="t_LHTDWId" value="" />
                    <asp:Button ID="Button3" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_LHTDWId');"
                    UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntLHT_Click" Style="margin-bottom: 4px;margin-left:5px;" />
                </td>
              
            </tr>
             <tr>
                <td class="t_r t_bg">
                    联合体承包单位组织代码：
                </td>
                <td colspan="3">
                     <asp:TextBox ID="t_LHTCBDWZZJGDM" runat="server" CssClass="m_txt" Width="85%" Enabled="false" Height="46px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>           
        </table>
        
    </div>
    
    </form>
</body>
</html>
