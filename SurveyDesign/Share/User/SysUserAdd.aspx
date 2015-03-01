<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysUserAdd.aspx.cs" Inherits="Share_User_ManUserAdd"
    EnableEventValidation="false" %>

<%@ Register Src="../../Common/govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统管理员用户</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
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
            return AutoCheckInfo();
        }
        function exitt() { //返回
            if ($("#HSaveResult").val() == "1")
                window.returnValue = "1";
            window.close();
        }

        function addRight(FUserID, FID) {
            if (FUserID == null || FUserID == "" || FUserID == undefined) {
                alert("请先保存" + FUserID);
                return;
            }
            showAddWindow("ManUserRightAdd.aspx?FUserId=" + FUserID + "&FID=" + FID, 500, 400);
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                系统管理员用户维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="exitt();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="156px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                密码：
            </td>
            <td>
                <asp:TextBox ID="t_FPassWord" runat="server" CssClass="m_txt" Width="156px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                加密锁标签编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockLabelNumber" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                加密锁硬件编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
                <tt>*</tt>
                <input id="btnRead" class="m_btn_w2" type="button" value="读锁" />
                <input id="btnSelect" class="m_btn_w4" type="button" value="未发锁库" />
                <input id="hidd_LockID" type="hidden" runat="server" />
                <asp:Button ID="btn_LockID" runat="server" Text="" OnClick="btn_LockID_Click" Style="display: none;" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                主管部门：
            </td>
            <td>
                <uc1:Govdept ID="Govdept1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                &nbsp;姓名：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                职务：
            </td>
            <td>
                <asp:TextBox ID="t_FFunction" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所在单位：
            </td>
            <td>
                <asp:TextBox ID="t_FCompany" runat="server" CssClass="m_txt" Width="225px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                角色：
            </td>
            <td>
                <asp:DropDownList ID="t_FMenuRoleId" runat="server">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                系统权限：
            </td>
            <td class="m_txt_M">
                <asp:CheckBoxList ID="t_FRoleId" runat="server" RepeatColumns="3">
                </asp:CheckBoxList>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <input id="t_FManageDeptId" runat="server" type="hidden" />
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
