<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpUserAdd.aspx.cs" Inherits="Share_User_EmpUserAdd"
    EnableEventValidation="false" %>

<%@ Register Src="../../Common/govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
            DynamicGrid(".m_dg1_i");
            $("#btnRead").click(function() { //读锁
                var number = getLockId();
                if (number == undefined) {
                    alert("请插入加密锁");
                    return;
                }
                $("#t_FLockNumber").attr("value", number);
            });
            $("#btnSelect").click(function() { //选则
                var rv = showWinByReturn("../Sys/CardNoSelect.aspx?", 600, 500);
                if (rv) {
                    $("#hidd_LockID").attr("value", rv);
                    $("#btn_LockID").click();
                }
            });
        });
        function CheckInfo() {
            if (AutoCheckInfo()) {
                return true;
            }
            return false;
        }
        function selectEnt(obj) {
            var rv = showWinByReturn("EmpSelectEnt.aspx?", 700, 500);
            if (rv != null && rv != '') {
                $("#t_FBaseinfoId").val(rv);

            }
            return false;
        }
    </script>

    <script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <input id="hidd_LockID" type="hidden" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                人员用户维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
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
            <td colspan="3">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtEntName" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                <asp:Button ID="btnSelectEnt" runat="server" Text="选择..." OnClientClick="selectEnt(this);"
                    CssClass="m_btn_w4" OnClick="btnSelectEnt_Click" />
                <input type="hidden" id="t_FBaseinfoId" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                身份证号码：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FIdCard" runat="server" CssClass="m_txt" Width="200px" MaxLength="30"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <td class="t_r t_bg">
            执业类型：
        </td>
        <td colspan="3">
            <asp:DropDownList ID="t_FPersonTypeId" runat="server" CssClass="m_txt">
            </asp:DropDownList>
        </td>
        <tr>
            <td class="t_r t_bg">
                从事专业：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FRegistSpecialId" runat="server" CssClass="m_txt" MaxLength="15"
                    Width="200px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FCertiNo" runat="server" CssClass="m_txt" MaxLength="30" Width="200px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                印章编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FPrintNo" runat="server" CssClass="m_txt" MaxLength="18" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                有效期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FEndTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt"
                    MaxLength="18" Width="200px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td colspan="3">
                <asp:TextBox ID="tt_FTel" runat="server" CssClass="m_txt" MaxLength="20" Width="200px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                职称：
            </td>
            <td>
                <asp:DropDownList ID="t_FTechId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                职称证号：
            </td>
            <td>
                <asp:TextBox ID="t_FSealNo" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="t_FUserName" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                密码：
            </td>
            <td>
                <asp:TextBox ID="txtFPassWord" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                CA证书编号：
            </td>
            <td>
                <asp:TextBox ID="tt_FCANumber" runat="server" CssClass="m_txt" Width="100px" MaxLength="10"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                CA证书序号：
            </td>
            <td>
                <asp:TextBox ID="tt_FCACardId" runat="server" CssClass="m_txt" Width="100px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                CA发放日期：
            </td>
            <td>
                <asp:TextBox ID="tt_FCAStartTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="100px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                CA有效结束日期：
            </td>
            <td>
                <asp:TextBox ID="tt_FCAEndTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="100px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
