<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManUserAdd.aspx.cs" Inherits="Share_User_ManUserAdd"
    EnableEventValidation="false" %>

<%@ Register Src="../../Common/govdeptid4.ascx" TagName="Govdept" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理部门用户维护</title>
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
            showAddWindow("ManUserRightAdd.aspx?FUserId=" + FUserID + "&FID=" + FID, 850, 660);
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
                管理部门用户维护
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
                行政区划：
            </td>
            <td colspan="3">
                <uc1:Govdept ID="Govdept1" runat="server" OnSelectedIndexChanged="govdeptid1_SelectedIndexChanged" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属单位：
            </td>
            <td colspan="3">
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlPartType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPartType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <tt>*</tt>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>
                <asp:TextBox ID="ddlPartType" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                部门：
            </td>
            <td>
                <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="t_FCompany" runat="server">
                        </asp:DropDownList>
                        <tt>*</tt>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>
                <asp:TextBox ID="t_FCompanyName" runat="server"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                职务：
            </td>
            <td>
                <asp:TextBox ID="t_FFunction" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                姓名：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                手机：
            </td>
            <td>
                <asp:TextBox ID="t_FLicence" runat="server" CssClass="m_txt" MaxLength="15" onblur="isInt(this);"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                Email：
            </td>
            <td>
                <asp:TextBox ID="t_FAddress" runat="server" CssClass="m_txt" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="100px" MaxLength="10"></asp:TextBox>
                <tt>*</tt>&nbsp;
            </td>
            <td class="t_r t_bg">
                &nbsp; 密码：
            </td>
            <td>
                <asp:TextBox ID="txtFPassWord" runat="server" CssClass="m_txt" Width="100px" MaxLength="40"></asp:TextBox>
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
                有有效开始日期：
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
        <%--<tr style="display: none">
            <td class="t_r t_bg">
                OA部门：
            </td>
            <td>
                <asp:DropDownList ID="t_FOAorg" runat="server">
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                OA菜单权限：
            </td>
            <td>
                <asp:DropDownList ID="t_FOAmenuRole" runat="server">
                </asp:DropDownList>
            </td>
        </tr>--%>
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
                启用：
            </td>
            <td>
                <asp:CheckBox ID="Check_FIsUserName" runat="server" Text="用户名登陆方式" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_title">
        <tr>
            <td colspan="4" class="t_bg" style="padding-left: 20px; color: Red;">
                附件：
                <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnShowFile_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="m_txt_M">
                <table class="m_dg1" width="100%" align="center">
                    <tr class="m_dg1_h">
                        <td style="width: 30px;">
                            序号
                        </td>
                        <td>
                            资料名称
                        </td>
                        <td>
                            是否必需
                        </td>
                        <td style="width: 60px;">
                            已上传<br />
                            文件个数
                        </td>
                        <td style="width: 160px;">
                            <font color="green">是</font>/<font color="red">否</font> 上传
                        </td>
                    </tr>
                    <asp:Repeater ID="rep_List" runat="server" OnItemDataBound="rep_List_ItemDataBound">
                        <ItemTemplate>
                            <tr class="m_dg1_select">
                                <td>
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td class="t_l">
                                    <%#Eval("FFileName")%>
                                </td>
                                <td>
                                    <%#Eval("FIsMust")%>
                                </td>
                                <td>
                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="lit_Has" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <asp:Repeater ID="rep_File" runat="server" OnItemCommand="rep_File_ItemCommand">
                                <ItemTemplate>
                                    <tr class="m_dg1_i">
                                        <td colspan="6" class="t_l" style="padding-left: 50px;">
                                            (<%# Container.ItemIndex+1 %>)、 <a href='<%#Eval("FFilePath") %>' target="_blank"
                                                title="点击查看该文件">
                                                <%#Eval("FFileName")%>
                                            </a>
                                            <asp:LinkButton ID="btnDel" runat="server" Text="[删除]" CommandName="cnDel" CommandArgument='<%#Eval("FID") %>'
                                                OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
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
                <input id="btnRoleSet" class="m_btn_w6" type="button" value="设置菜单角色" runat="server"
                    visible="false" />
                <input id="btnAdd" type="button" value="新增" class="m_btn_w2" onclick="addRight('<%=ViewState["FID"] %>','');" />
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
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
                        <%#getSysName(Approve.EntityBase.EConvert.ToString( Eval("FSystemId")))%></a>
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
    <br />
    <input id="hidd_LockID" type="hidden" runat="server" />
    <input id="hidd_oldLockNumber" type="hidden" runat="server" />
    <input id="t_FManageDeptId" runat="server" type="hidden" />
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
