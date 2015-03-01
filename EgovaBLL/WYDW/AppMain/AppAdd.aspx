﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppAdd.aspx.cs" Inherits="WYDW_AppMain_AppPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新业务申请</title>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script>
        function selZGXM(obj) {
            var fMType = $("#hidfMType").val();
            var result = showWinByReturn('../Common/ItemList.aspx?ftype='+fMType, 900, 480);
            
            if (result != null) {
                $("#hidXMBH").val(result[0]);
                $("#hidXMMC").val(result[1]);
                $("#t_XMMC").val(result[1]);
                $("#hidIsSelected").val("1");            
            }
        }
        function checkInfo() {
            var xmmc_inp = $("#t_XMMC").val();
            var xmmc_sel = $("#hidXMMC").val();
            if (!AutoCheckInfo()) {
                return false;
            }
            else {
                if (xmmc_sel != "") {
                    if (xmmc_inp != xmmc_sel) {
                        if (!confirm("您更改了所选择的项目的项目名称，确定要保存吗？")) {
                            return false;
                        }
                    }
                }
                else {
                    if (!confirm("您未选择项目，确定添加一个新的项目吗？")) {
                        return false;
                    }
                }
                return true;
            }
        }
    </script>
    <base target="_self"/>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" width="550" id="applyInfo" runat="server" class="m_table">
            <tr>
                <td  class="t_r t_bg">年度：
                </td>
                <td>
                    <asp:TextBox ID="t_FYear" runat="server" CssClass="m_txt" Enabled="False" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  class="t_r t_bg">申请业务名称：
                </td>
                <td>
                    <asp:TextBox ID="t_FName" Text="在管项目申请" runat="server" Enabled="False" CssClass="m_txt" Width="280px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  class="t_r t_bg">项目名称：
                </td>
                <td>
                    <asp:TextBox ID="t_XMMC" runat="server" CssClass="m_txt" Width="280px"></asp:TextBox><asp:Label ID="selTips" runat="server" ForeColor="Red" >（未选择）</asp:Label><tt>*</tt>
                    <asp:Button Text="选择..." ID="Btn_Search" runat="server" OnClientClick="return selZGXM(this)" OnClick="Btn_Search_Click" CssClass="m_btn_w4"></asp:Button>
                    
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" height="30">
                    <asp:Button ID="btnOk" runat="server" CssClass="m_btn_w2" OnClientClick="return checkInfo()" OnClick="btnOk_Click" Text="确认" />
                    <input id="btnCancel" class="m_btn_w2" onclick="window.close();" style="margin-left: 10px"
                        type="button" value="取消" runat="server" />
                </td>
            </tr>
        </table>
        <div id="pageError" style="text-align: center;line-height:140px; height: 140px; vertical-align: middle; font-family: 微软雅黑; font-size: 15px; display: none;" runat="server">页面错误</div>
        <input type="hidden" id="hidXMBH" value="" runat="server" />
        <input type="hidden" id="hidXMMC" value="" runat="server" />
        <!--表示已经选择，0表示未选择，1表示已选择-->
        <input type="hidden" id="hidIsSelected" value="0" runat="server" /> 
        <input id="hidfMType" type="hidden" value="14401" runat="server" />
    </form>
</body>
</html>
