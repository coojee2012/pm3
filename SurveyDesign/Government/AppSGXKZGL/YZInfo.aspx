<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YZInfo.aspx.cs" Inherits="JSDW_APPSGXKZGL_YZInfo" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
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
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <input type="hidden"  runat="server" ID="h_FAppId" value="" />
        <input type="hidden"  runat="server" ID="h_FPrjItemId" value="" />
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
        <table width="80%" align="center" class="m_bar">
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
        <table class="m_table" width="80%" align="center">
            <tr>
                <td class="t_r t_bg" style="width:100px;">
                    证书名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FCertificateName" runat="server" CssClass="m_txt" Width="300px" MaxLength="30" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    存放位置：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FLocation" runat="server" CssClass="m_txt" Width="300px" MaxLength="30" ></asp:TextBox>
                </td>
               
            </tr>
            <tr>
                <td class="t_r t_bg">
                    持证人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_CZR" runat="server" CssClass="m_txt" Width="300px" MaxLength="30" ></asp:TextBox>
                    <asp:Button ID="btnSel" runat="server" CssClass="m_btn_w4" OnClientClick="return selEmp(this);" Text="选择..." UseSubmitBehavior="false" />
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    收证人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_SZR" runat="server" CssClass="m_txt" Width="300px" MaxLength="30" text="省住建厅"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    接收时间：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FAcceptTime" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="300px" MaxLength="30" ></asp:TextBox>
                </td>
               
            </tr>
            
                   
        </table>
    </div>
    
    </form>
</body>
</html>
