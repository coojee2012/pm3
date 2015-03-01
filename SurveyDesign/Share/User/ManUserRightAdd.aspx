<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManUserRightAdd.aspx.cs"
    Inherits="Share_User_ManUserRightAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑用户权限</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
            //复选框样式
            $(":checkbox:checked").parent().css("background", "#E5FaFf");
            $(":checkbox").click(function() {
                $(this).parent().css("background", this.checked ? '#E5FaFf' : "");
            });

            $("#btnRead").click(function() { //读锁
                var number = getLockId();
                if (number == undefined) {
                    alert("请插入加密锁");
                    return;
                }
                $("#t_FLockNumber").attr("value", number);
            });

            $("#btnSelect").click(function() { //从锁库中选择
                var rv = showWinByReturn("../Sys/CardNoSelect.aspx?", 600, 500);
                if (rv) {
                    $("#hidd_LockID").attr("value", rv);
                    $("#btn_LockID").click();
                }
            });

        });
        function CheckInfo(obj) {//验证
            if (AutoCheckInfo()) {
                if ($(":checkbox[id*=txtFMenuRoleId]:checked").length > 0) {
                    obj.disabled = true;
                    obj.value = "保存中..";
                    __doPostBack(obj.id, '');
                    return true;
                }
                else
                    alert("请选择菜单角色!");
            }
            return false;
        }

        //市场监管选择特殊权限
        function CheckPope() {
            if ($("#Hid_RightId").val() == "") {
                alert("请先保存");
                return;
            }
            showAddWindow('CheckUserPope.aspx?RightId=' + $("#Hid_RightId").val(), 600, 600);
        }

        //审核系统 指定建造师审核专业
        function selFType() {
            var selFtype = window.showModalDialog("ManJzsType.aspx?v=" + $("#t_FSelType").val() + "&r=" + Math.random(), '', "dialogWidth:250px;dialogHeight:300px");
            if (selFtype != null && selFtype != '') {
                var result = selFtype.split('~');
                $("txt_FSelType").val(result[1]); //名称集
                $("t_FSelType").val(result[1]); //结果集
            }

        }
    </script>

    <script type="text/vbscript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                编辑管理部门用户权限
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click"
                    UseSubmitBehavior="false" />
                <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                系统权限：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="t_FSystemId_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr id="tr_Pope1" runat="server" visible="false">
            <td class="t_r t_bg">
                发布权限：
            </td>
            <td class="m_txt_M">
                <asp:CheckBox ID="t_FIsPub" runat="server" />
            </td>
        </tr>
        <tr id="tr_Pope2" runat="server" visible="false">
            <td class="t_r t_bg">
                特殊权限：
            </td>
            <td class="m_txt_M">
                <asp:CheckBox ID="t_FIsPope" runat="server" AutoPostBack="True" OnCheckedChanged="t_FIsPope_CheckedChanged" />
                &nbsp;<input id="btnCheckPope" class="m_btn_w4" runat="server" onclick="CheckPope();"
                    type="button" value="权限设置" visible="False" />
                <asp:Label ID="la_Pope" runat="server" Text="*点击“权限设置”菜单，设置该用户权限" ForeColor="Red"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr id="tr_App0" runat="server" visible="false">
            <td class="t_r t_bg">
                是否指定审核建造师专业：
            </td>
            <td class="m_txt_M">
                <asp:CheckBox ID="check_JZS" runat="server" Text="启用指定" AutoPostBack="True" OnCheckedChanged="check_JZS_CheckedChanged" />
            </td>
        </tr>
        <tr id="tr_Jzs0" runat="server" visible="false">
            <td class="t_r t_bg">
                审核建造师专业：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="txt_FSelType" runat="server" CssClass="m_txt" Height="39px" ReadOnly="true"
                    TextMode="MultiLine" Width="367px"></asp:TextBox>
                <input type="hidden" id="t_FSelType" runat="server" />
                <input id="btnSelType" class="m_btn_w4" onclick="selFType()" type="button" value="指定专业.." />
            </td>
        </tr>
        <tr id="tr_App1" runat="server" visible="false">
            <td class="t_r t_bg">
                是否有权限修改企业数据：
            </td>
            <td class="m_txt_M">
                <asp:DropDownList ID="t_FCanMod" runat="server" CssClass="m_txt">
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr_App2" runat="server" visible="false">
            <td class="t_r t_bg">
                是否可审核企业市场行为：
            </td>
            <td class="m_txt_M">
                <asp:DropDownList ID="t_FPri" runat="server" CssClass="m_txt">
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="156px" 
                    MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                密码：
            </td>
            <td>
                <asp:TextBox ID="txtFPassWord" runat="server" CssClass="m_txt" Width="156px" 
                    MaxLength="40"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                加密锁标签编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockLabelNumber" runat="server" CssClass="m_txt" 
                    Width="120px" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                加密锁硬件编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt" Width="120px" 
                    MaxLength="20"></asp:TextBox>
                <tt>*</tt>
                <input id="btnRead" class="m_btn_w2" type="button" value="读锁" />
                <input id="btnSelect" class="m_btn_w4" type="button" value="未发锁库" />
                <input id="hidd_LockID" type="hidden" runat="server" />
                <asp:Button ID="btn_LockID" runat="server" Text="" OnClick="btn_LockID_Click" Style="display: none;" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                有效开始日期：
            </td>
            <td>
                <asp:TextBox ID="t_FBeginTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                有效结束日期：
            </td>
            <td>
                <asp:TextBox ID="t_FEndTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                状态：
            </td>
            <td>
                <asp:DropDownList ID="t_FState" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="正常" Value="1"></asp:ListItem>
                    <asp:ListItem Text="注销" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否启用用户名登陆：
            </td>
            <td>
                <asp:CheckBox ID="Check_FIsUserName" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审核角色：
            </td>
            <td class="m_txt_M">
                <asp:CheckBoxList ID="t_FRoleId" CssClass="noborder" runat="server" RepeatColumns="3"
                    RepeatDirection="Horizontal">
                </asp:CheckBoxList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                菜单角色：
            </td>
            <td class="m_txt_M">
                <asp:Repeater ID="rpMenuRoleId" runat="server" OnItemDataBound="rpMenuRoleId_OnItemDataBound">
                    <ItemTemplate>
                        <asp:Label ID="ltTitle" runat="server" Text='<%#Eval("FSystemId") %>' ForeColor="Red"></asp:Label>
                        <br />
                        <asp:CheckBoxList ID="txtFMenuRoleId" CssClass="noborder" runat="server" RepeatColumns="3"
                            RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <br />
    <input id="hidd_oldLockNumber" type="hidden" runat="server" />
    <input id="Hid_RightId" runat="server" type="hidden" />
    </form>
</body>
</html>
