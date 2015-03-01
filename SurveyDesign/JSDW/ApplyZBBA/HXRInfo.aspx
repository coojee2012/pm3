<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HXRInfo.aspx.cs" Inherits="JSDW_ApplyZBBA_HXRInfo" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>候选人信息</title>
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
        function selTB(obj) {
            var pid = showWinByReturn('../ApplyZBBA/TBSel.aspx?fLinkId=<%=ViewState["FLinkId"] %>', 1000, 500);
            if (pid != null && pid != '') {
                $("#t_BMId").val(pid);
                __doPostBack(obj.id, '');
            }
        }
    </script>
    <base target="_self">
    </base>
    <style type="text/css">
        .modalDiv { position: absolute; top: 1px; left: 1px; width: 100%; height: 100%; z-index:9999; background-color:gray; opacity:.50; filter: alpha(opacity=50); }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="up_Main" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv"> 
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
                    中标候选人名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_HXRMC" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <asp:Button ID="btnSel" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selTB(this);"
                                        UseSubmitBehavior="false" OnClick="btnSel_Click" />
                    <input id="t_BMId" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    中标候选人顺序：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtOrder" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    评标价：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_PBJ" onblur="isFloat(this);" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>（万元）
                </td>
                <td class="t_r t_bg">
                    投标价：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_TBJ" onblur="isFloat(this);" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>（万元）
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目负责人姓名：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FZRXM" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    执业资格证号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZYZGZH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>  
            <tr>
                <td class="t_r t_bg">
                    注册证书号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZCZSH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    职称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZC" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>    
            <tr>
                <td class="t_r t_bg">
                    职称专业：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZCZY" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    职称证号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZCZH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr> 
        </table>
    </div>
    
    </form>
</body>
</html>

