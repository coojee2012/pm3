<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BDInfo.aspx.cs" Inherits="JSDW_ApplyZBBA_BDInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>标段信息</title>
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
        function showTr() {
            $("tr[name=tr_t1]").show();
        }
        function hideTr() {
            $("tr[name=tr_t1]").hide();
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
            <%--<tr>
                
                <td class="t_r t_bg">
                    企业资质及等级（序列）：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_QYZZDJXL" runat="server" CssClass="m_txt" Width="203px" OnSelectedIndexChanged="t_QYZZDJXL_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td class="t_r t_bg">
                    企业资质等级：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_QYZZDJ" runat="server" CssClass="m_txt" Width="600px"></asp:TextBox>
                </td>
                <%--<td class="t_r t_bg">
                    企业资质及等级（等级）：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_QYZZDJDJ" runat="server" CssClass="m_txt" Width="203px"></asp:DropDownList>
                </td>--%>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建造师等级：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZBBM" runat="server" CssClass="m_txt" Width="200px">
                        
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    建造师专业：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZBDLJGFZR" runat="server" CssClass="m_txt" Width="200px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    标段编码：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_BDBM" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    标段名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_BDMC" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    标段说明：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="t_BDSM" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    是否接收联合体：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_JSLHT" runat="server" Width="203px" CssClass="m_txt" AutoPostBack="true" OnSelectedIndexChanged="t_JSLHT_SelectedIndexChanged">
                        <asp:ListItem Value="0">否</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr name="tr_t1">
                
                <td class="t_r t_bg">
                    联合体企业资质及等级（序列）：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_LHTQYZZDJXL" runat="server" CssClass="m_txt" Width="203px" OnSelectedIndexChanged="t_LHTQYZZDJXL_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>  --%>
            <tr name="tr_t1">
                <td class="t_r t_bg">
                    联合体企业资质等级：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_LHTQYZZDJ" runat="server" CssClass="m_txt" Width="600px"></asp:TextBox>
                </td>
                <%--<td class="t_r t_bg">
                    联合体企业资质及等级（等级）：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_LHTQYZZDJDJ" runat="server" CssClass="m_txt" Width="203px"></asp:DropDownList>
                </td>--%>
            </tr>               
        </table>
    </div>
    
    </form>
</body>
</html>
