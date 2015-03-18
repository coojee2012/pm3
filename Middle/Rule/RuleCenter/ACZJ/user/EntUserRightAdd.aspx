<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntUserRightAdd.aspx.cs"
    Inherits="Share_User_EntUserRightAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业用户权限</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss(); //文本框样式
            $("#btnRead").click(function () { //读锁
                var number = getLockId();
                if (number == undefined) {
                    alert("请插入加密锁");
                    return;
                }
                $("#t_FLockNumber").attr("value", number);
            });
            $("#btnSelect").click(function () { //选则
                var rv = showWinByReturn("../Sys/CardNoSelect.aspx?", 600, 500);
                if (rv) {
                    $("#hidd_LockID").attr("value", rv);
                    $("#btn_LockID").click();
                }
            });
            $("#t_FIsSMS").click(function () { showCheck(); });
        });
        function showCheck() {
            if ($("#t_FIsSMS").attr("checked")) { $("#div_OpenSMS").show(); }
            else { $("#div_OpenSMS").hide(); }
        }
        function CheckInfo() {//验证
            if (!AutoCheckInfo()) return false;

            if ($("#t_FIsSMS").attr("checked")) {
                if ($.trim($("#t_FSMSPhone").val()) == "") {
                    alert("请填写短信提醒手机号");
                    $("#t_FSMSPhone").focus();
                    return false;
                }
            }
            return true;
        }

    </script>

    <script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">企业用户权限
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">企业名称：
                </td>
                <td>
                    <asp:Label ID="l_Company" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">组织机构代码：
                </td>
                <td>
                    <asp:Label ID="l_FJuridcialCode" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">系统类型：
                </td>
                <td>
                    <asp:DropDownList ID="t_FSystemId" runat="server">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">用户名：
                </td>
                <td>
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="156px"
                        MaxLength="10"></asp:TextBox>
                    <tt>*</tt>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">密码：
                </td>
                <td>
                    <asp:TextBox ID="txtFPassWord" runat="server" CssClass="m_txt" Width="156px"
                        MaxLength="40"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">加密锁标签编号：
                </td>
                <td>
                    <asp:TextBox ID="t_FLockLabelNumber" runat="server" CssClass="m_txt" Width="120px"
                        MaxLength="10"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">加密锁硬件编号：
                </td>
                <td>
                    <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt" Width="120px"
                        MaxLength="20"></asp:TextBox>
                    <tt>*</tt>
                    <input id="btnRead" style="display: none;" class="m_btn_w2" type="button" value="读锁" />
                    <input id="btnSelect" class="m_btn_w4" style="display: none;" type="button" value="未发锁库" />
                    <input id="hidd_LockID" type="hidden" runat="server" />
                    <asp:Button ID="btn_LockID" runat="server" Text="" OnClick="btn_LockID_Click" Style="display: none;" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">有效开始日期：
                </td>
                <td>
                    <asp:TextBox ID="t_FBeginTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">有效结束日期：
                </td>
                <td>
                    <asp:TextBox ID="t_FEndTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">开通短信提醒：
                </td>
                <td>
                    <%-- <div style="float: left;">
                    <asp:CheckBox ID="t_FIsSMS" runat="server" />
                </div>--%>
                手机号
                <asp:TextBox ID="t_FSMSPhone" runat="server" CssClass="m_txt" Width="120px" MaxLength="11"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">状态：
                </td>
                <td>
                    <asp:DropDownList ID="t_FState" runat="server">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">启用：
                </td>
                <td>
                    <asp:CheckBox ID="Check_FIsUserName" runat="server" Text="用户名登陆方式" />
                </td>
            </tr>
        </table>
        <input id="hidd_oldLockNumber" type="hidden" runat="server" />
        <input id="HSaveResult" type="hidden" runat="server" />
    </form>
</body>
</html>
