<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntUserAdd.aspx.cs" Inherits="Admin_User_EntUserAdd"
    EnableEventValidation="false" %>

<%@ Register Src="../../Common/govdeptid2.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script src="../../zDialogNew/zDialog.js" type="text/javascript"></script>

    <script src="../../zDialogNew/zDrag.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" src="../../script/lock.js"></script>

    <script type="text/javascript"> 
     window.name = "win";
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
            initUpdateProgress();
            prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
          
        });
        function CheckInfo() {
            if (AutoCheckInfo()) {
                var t_FJuridcialCode = $("#t_FJuridcialCode").val();
                var patrn = /^[A-Za-z0-9]{1}[0-9]{7}-[A-Za-z0-9]{1}$/;
                if (!patrn.exec(t_FJuridcialCode)) {
                    alert("组织结构代码格式不正确");
                    return false
                }
                return true;
            }
            return false;
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
            showAddWindow("EntUserRightAdd.aspx?FUserId=" + FUserID + "&FID=" + FID, 500, 500);
        }
        function addRightFromReg(FUserID) {
            if (FUserID == null || FUserID == "" || FUserID == undefined) {
                alert("请先保存" + FUserID);
                return;
            }
            var rv = showWinByReturn("RegUserSelect.aspx?FUserId=" + FUserID, 600, 500);
            if (rv) {
                $("#hidd_RegFID").attr("value", rv);
                $("#btn_FRFID").click();
            }
        }

        function SelectEnt(obj) {
            var rv = showWinByReturn("SelectEnt.aspx?EntType=" + $("#t_FSystemId").val(), 600, 500);
            if (rv) {
                $("#hiFUserId").attr("value", rv);
                obj.disabled = true;
                __doPostBack(obj.id, '');
                return true;
            }
            return false;
        }
        function selEntTypes(obj) {
            var rv = showWinByReturn("EntTypesSel.aspx?FEntTypes=" + $("#t_FEntTypes").val(), 700, 500);
            if (rv != null && rv != '') {
                $("#t_FEntTypes").val(rv);
                __doPostBack(obj.id, '');
            }
            return false;
        }

        var postBackElement, prm;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);
            postBackElement = args.get_postBackElement();

            $get('UpdateProgress1').style.display = 'block';
        }
        function EndRequest(sender, args) {
            $get('UpdateProgress1').style.display = 'none';
        }
    </script>

    <script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>

    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server" target="win">
    <div style="display: none;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <input id="hidd_LockID" type="hidden" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业用户维护
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center" id="tabSetup1" runat="server">
        <tr>
            <td class="t_r t_bg">
                请选择用户类型后点下一步：
            </td>
            <td>
                <asp:DropDownList ID="t_FEntType" CssClass="m_txt" runat="server">
                </asp:DropDownList>
                <asp:Button ID="btnNext" runat="server" CssClass="m_btn_w2" Text="下一步" OnClick="btnNext_Click" />
            </td>
        </tr>
    </table>
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDownload" runat="server" CssClass="m_btn_w4" Text="下载最新" OnClick="btnSelectEnt_Click" />
                    <input id="btnBack" class="m_btn_w2" onclick="exitt();" type="button" value="返回" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    企业名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FCompany" runat="server" CssClass="m_txt" Width="155px" MaxLength="50"
                        ReadOnly="true"></asp:TextBox>
                    <tt>*</tt>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:Button ID="btnSelectEnt" runat="server" CssClass="m_btn_w2" Text="选择" OnClientClick="if(!SelectEnt(this)){return false;}"
                                OnClick="btnSelectEnt_Click" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnDownload" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="t_FJuridcialCode" runat="server" CssClass="m_txt" Width="104px"
                        ReadOnly="true"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    主管部门：
                </td>
                <td>
                    <uc1:Govdept ID="Govdept1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td>
                    <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt" MaxLength="15" Width="104px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td>
                    <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt" MaxLength="15" onblur="isInt(this);"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    用户名：
                </td>
                <td>
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="100px" MaxLength="10" ReadOnly="true"></asp:TextBox>
                    <tt>*</tt>&nbsp;
                </td>
                <td class="t_r t_bg">
                    &nbsp; 密码：
                </td>
                <td>
                    <asp:TextBox ID="txtFPassWord" runat="server" CssClass="m_txt" Width="100px" MaxLength="40" ReadOnly="true"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    加密锁标签编号：
                </td>
                <td>
                    <asp:TextBox ID="t_FLockLabelNumber" runat="server" CssClass="m_txt" Width="100px"
                        MaxLength="10"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    &nbsp;加密锁硬件编号：&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt" Width="100px" MaxLength="20"></asp:TextBox>
                    <tt>*</tt>
                    <input id="btnRead" class="m_btn_w2" type="button" value="读锁" />
                    <input id="btnSelect" class="m_btn_w4" type="button" value="未发锁库" />
                    <asp:Button ID="btn_LockID" runat="server" Text="" OnClick="btn_LockID_Click" Style="display: none;" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    有效开始日期：
                </td>
                <td>
                    <asp:TextBox ID="t_FBeginTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                        Width="100px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    &nbsp; 有效结束日期：
                </td>
                <td>
                    <asp:TextBox ID="t_FEndTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                        Width="100px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    CA证书编号：
                </td>
                <td>
                    <asp:TextBox ID="t_FCANumber" runat="server" CssClass="m_txt" Width="100px" MaxLength="10"></asp:TextBox>
                    <asp:Button ID="btnReadCA" runat="server" CssClass="m_btn_w2" Text="读取" OnClientClick="if(!fnQYLogin()){}"
                        OnClick="btnReadCA_Click" />
                </td>
                <td class="t_r t_bg">
                    CA证书序号：
                </td>
                <td>
                    <asp:TextBox ID="t_FCACardId" runat="server" CssClass="m_txt" Width="100px" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    CA发放日期：
                </td>
                <td>
                    <asp:TextBox ID="t_FCAStartTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                        Width="100px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    CA有效结束日期：
                </td>
                <td>
                    <asp:TextBox ID="t_FCAEndTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                        Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    用户状态：
                </td>
                <td>
                    <asp:DropDownList ID="t_FState" runat="server">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        <asp:ListItem Text="正常" Value="1"></asp:ListItem>
                        <asp:ListItem Text="注销" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <tt>*</tt> &nbsp; &nbsp;
                </td>
                <td class="t_r t_bg">
                    是否启用用户名登陆：
                </td>
                <td>
                    <asp:CheckBox ID="Check_FIsUserName" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    用户类型：
                </td>
                <td colspan="3">
                    <%--<asp:TextBox ID="txt_FEntTypes" runat="server" TextMode="MultiLine" Height="73px"
                    Width="400px" Enabled="false" CssClass="m_txt"></asp:TextBox>
                <asp:Button ID="btnSel" runat="server" Text="选择" CssClass="m_btn_w2" 
                    OnClientClick="return selEntTypes(this)" onclick="btnSel_Click" />--%>
                    <asp:DropDownList ID="t_FSystemId" CssClass="m_txt" runat="server">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    用户权限↓
                </td>
                <td class="m_bar_m t_r">
                    <input id="hidd_RegFID" type="hidden" runat="server" />
                    <asp:Button ID="btn_FRFID" runat="server" Text="" OnClick="btn_FRFID_Click" Style="display: none;" />
                    <input id="btnAdd" type="button" value="新增" class="m_btn_w2" onclick="addRight('<%=ViewState["FID"] %>','');" />
                    <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:GridView ID="DG_Rights" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Style="margin-top: 4px" Width="98%" EmptyDataText="暂时没有任何系统权限"
            OnRowCommand="DG_Rights_RowCommand" OnRowDataBound="DG_Rights_RowDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <RowStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundField HeaderText="序号">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="系统类型">
                    <ItemTemplate>
                        <a href="javascript:addRight('<%#ViewState["FID"] %>','<%#Eval("FID") %>');">
                            <%#Eval("sysName")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="加密锁硬件编号">
                    <ItemTemplate>
                        <%#Eval("FLockNumber") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FEndTime" HeaderText="有效结束日期" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <%#Eval("FState") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Button ID="btnDel" CommandName="cnDel" CommandArgument='<%#Eval("FID") %>' runat="server"
                            CssClass="m_btn_w2" Text="删除" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FId" Visible="False"></asp:BoundField>
            </Columns>
        </asp:GridView>
        <input id="hidd_oldLockNumber" type="hidden" runat="server" />
        <input id="t_FManageDeptId" runat="server" type="hidden" />
        <input id="HSaveResult" runat="server" type="hidden" />
        <input id="hiFUserId" runat="server" type="hidden" />
        <input type="hidden" id="CaCerti" name="CaCerti" value="" runat="server" />
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    </asp:UpdateProgress>
    <%-- <input id="t_FEntTypes" runat="server" type="hidden" />--%>
    </form>
</body>
</html>
