<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="KC_ApplyHTBA_ApplyBaseInfo" %>

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
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function selEnt(tagId, fsysid, obj) {
            var pid = showWinByReturn('../appmain/EntListSel.aspx?fsysid=' + fsysid, 700, 500);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }
        function selEmp(tagId, fsysid, obj, isCkAll) {
            var url = '?fsysid=' + fsysid;
            if (isCkAll == 1) {
                url += '&ckAll=1';
            }
            url += '&fAppId=<%=Session["FAppId"] %>';
            var pid = showWinByReturn('../AppMain/EmpListSel.aspx' + url, 700, 500);
            if (pid != null && pid != '') {
                if (tagId != null && tagId != '')
                    $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }
        function seePrj() {
            var fid = $('#p_FId').val();
            showAddWindow('../../JSDW/appmain/AddPrjRegist.aspx?fid=' + fid, 900, 700);
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                基本信息
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r" style="padding-right: 10px;">
                <input type="hidden" id="hidd_FLinkId" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="t_FPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
                <input type="hidden" id="p_FId" runat="server" />
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                工程编号：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt2" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                标段号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt3" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地点：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="t_FDeptName" runat="server" CssClass="m_txt" Width="224px" MaxLength="30"
                    ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同类型：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_FInt3" runat="server" CssClass="m_txt" Enabled="false">
                    <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                    <asp:ListItem Value="287" Text="勘察文件审查"></asp:ListItem>
                    <asp:ListItem Value="300" Text="施工图设计文件审查"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备案部门：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_FInt1" runat="server" CssClass="m_txt" Enabled="false">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建筑面积或规模：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt5" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                发包人（合同备案人）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt1" runat="server" CssClass="m_txt" ReadOnly="true" Width="250px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <!---->
        <tr>
            <td class="t_r t_bg">
                经办人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt4" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg" style="width: 100px;">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt6" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                日期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FDate2" runat="server" CssClass="m_txt" onfocus="WdatePicker();"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
<!---->
        <tr>
            <td class="t_r t_bg">
                承包人（管理人）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt7" runat="server" CssClass="m_txt" ReadOnly="true" Width="250px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <!---->
        <tr>
            <td class="t_r t_bg">
                经办人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt9" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg" style="width: 100px;">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt14" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                日期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FDate3" runat="server" CssClass="m_txt" onfocus="WdatePicker();"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
<!---->
        <tr>
            <td class="t_r t_bg">
                承包人（管理人）企业地址：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt16" runat="server" CssClass="m_txt" ReadOnly="true" Width="250px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同金：
            </td>
            <td>
                <asp:TextBox ID="t_FFloat1" runat="server" CssClass="m_txt"  onblur="isFloat(this);"></asp:TextBox>
               万元
            </td>
            <td class="t_r t_bg">
                收费标准：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt8" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同签订日期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FDate1" runat="server" CssClass="m_txt" ReadOnly="true" onfocus="WdatePicker();"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
