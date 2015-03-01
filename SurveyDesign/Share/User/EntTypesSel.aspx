<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntTypesSel.aspx.cs" Inherits="Share_User_EntTypesSel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑用户菜单角色</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

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
        function exitt() { //返回
            if ($("#HSaveResult").val() == "1")
                window.returnValue = "1";
            window.close();
        }
        function CheckInfo() {//验证
            return AutoCheckInfo();
        }
 
    </script>

    <script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                选择企业类型
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="确定" OnClick="btnSave_Click" />
                <input id="btnBack" class="m_btn_w2" onclick="exitt();" type="button" value="关闭" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                企业类型： <tt>*</tt>
            </td>
            <td class="m_txt_M">
                <asp:CheckBoxList ID="t_FEntTypes" CssClass="noborder" runat="server" RepeatColumns="3"
                    RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    <input id="hidd_oldLockNumber" type="hidden" runat="server" />
    <input id="HSaveResult" type="hidden" runat="server" />
    </form>
</body>
</html>

<script type="text/javascript">
$(function(){
    $(":checkbox:checked").parent().css("background","#E5FaFf");
    $(":checkbox").click(function()
    {
        $(this).parent().css("background",this.checked ? '#E5FaFf' : ""); 
    });
}); 
</script>

