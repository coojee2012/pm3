<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddStopPrj.aspx.cs" Inherits="Government_Prj_AddStopPrj" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                项目终止
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
                <input type="hidden" id="t_FPrjId" runat="server" />
                <input type="button" id="look" value="查看工程基本信息" class="m_btn_w8" onclick="showPrjInfo();" />
                <asp:Button ID="btnSel" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selPrj(this);"
                    UseSubmitBehavior="false" OnClick="btnSel_Click" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地址：
            </td>
            <td>
                <asp:TextBox ID="p_FAddress" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设单位：
            </td>
            <td>
                <asp:TextBox ID="p_JSDW" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                办理人：
            </td>
            <td>
                <asp:TextBox ID="t_FUserName" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                办理时间：
            </td>
            <td>
                <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" Width="90px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                项目终止原因：
            </td>
            <td>
                <asp:TextBox ID="t_FRemark" runat="server" CssClass="m_txt" Width="350px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="2" class="t_c">
                <asp:Button ID="btnOp" runat="server" Text="终止" OnClick="btnOp_Click" CssClass="m_btn_w2" />
                <input type="button" value="返回" onclick="window.close();" class="m_btn_w2" />
            </td>
        </tr>
    </table>
    <div style="line-height: 24px; color: Red; width: 98%; margin: 0px auto;">
        提示：终止后项目所有业务将自动停止办理。
    </div>
    </form>
</body>
</html>
