<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntBadAction.aspx.cs" Inherits="Government_statis_EntBadAction" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            if ($("#t_FPrjId").val() != "") {
                $("#look").show();
            }
            else {
                $("#look").hide();
            }
        });

        //选择项目
        function selPrj(obj) {
            var pid = showWinByReturn('PrjSel.aspx?', 700, 500);
            if (pid != null && pid != ' ') {
                $("#t_FPrjId").val(pid);
                __doPostBack(obj.id, '');
            }
        }

        //验证
        function checkInfo() {
            if (!AutoCheckInfo())
                return false;
            return true;
        }

        //查看项目信息
        function showPrjInfo() {
            var FID = $("#t_FPrjId").val();
            showAddWindow('../../JSDW/appmain/AddPrjRegist.aspx?FID=' + FID, 900, 700);
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业不良行为记录
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                被处罚企业：
            </td>
            <td>
                <asp:Literal ID="t_QYMC" runat="server"></asp:Literal>
            </td>
            <td class="t_r t_bg">
                处罚日期：
            </td>
            <td>
                <asp:Literal ID="t_CFRQ" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                被处罚工程：
            </td>
            <td>
                <asp:Literal ID="t_GCMC" runat="server"></asp:Literal>
            </td>
        
            <td class="t_r t_bg">
                工程地址：
            </td>
            <td>
                <asp:Literal ID="t_GCDZ" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                行为事实：
            </td>
            <td  colspan="3">
                <asp:Literal ID="t_XWSS" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                处罚决定：
            </td>
            <td  colspan="3">
                <asp:Literal ID="t_CFJD" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                认定单位：
            </td>
            <td>
                <asp:Literal ID="t_RDDW" runat="server"></asp:Literal>
            </td>
            <td class="t_r t_bg">
                处罚分数：
            </td>
            <td>
                <asp:Literal ID="t_FS" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                不良行为发生日期：
            </td>
            <td>
                <asp:Literal ID="t_BLXWFSRQ" runat="server"></asp:Literal>
            </td>
            <td class="t_r t_bg">
                核定日期：
            </td>
            <td>
                <asp:Literal ID="t_SHRQ" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                处罚文件：
            </td>
            <td colspan="3">
                <asp:Literal ID="t_CFWJ" runat="server"></asp:Literal>
            </td>
            
        </tr>
    </table>
    
    
    </form>
</body>
</html>
