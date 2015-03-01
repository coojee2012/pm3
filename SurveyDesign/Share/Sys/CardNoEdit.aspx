<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CardNoEdit.aspx.cs" Inherits="Share_Sys_CardNoEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript">
        function CheckInfo() {
            return AutoCheckInfo();
        }
        function readLock() {
            document.getElementById("txtFLockNumber").value = getLockId();
        }
        $(document).ready(function() {
            txtCss(); //文本框样式

            $("#btnBack").click(function() {//返回按钮
                if ($("#HSaveResult").val() == "1")
                    window.returnValue = "1";
                window.close();
            });

            $("#btnRead").click(function() { //读锁
                var number = getLockId();
                if (number == undefined) {
                    alert("请插入加密锁");
                    return;
                }
                $("#t_FLockNumber").attr("value", number);
            });
        });
    </script>

    <script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                加密锁维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                <input id="btnBack" class="m_btn_w2" value="返回" type="button" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                加密锁标签编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockLabelNumber" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                加密锁硬件编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
                <input id="btnRead" type="button" value="读锁" class="m_btn_w2 bnts_left10" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                批次：
            </td>
            <td>
                <asp:DropDownList ID="t_FBatchId" runat="server">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                状态：
            </td>
            <td>
                <asp:DropDownList ID="t_FState" runat="server" Enabled="false">
                    <asp:ListItem Text="未分配" Value="0"></asp:ListItem>
                    <asp:ListItem Text="已分配" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
