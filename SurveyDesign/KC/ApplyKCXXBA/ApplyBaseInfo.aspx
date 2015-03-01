<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="KC_ApplyKCXXBA_ApplyBaseInfo" %>

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
            <td colspan="3">
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
                <input type="hidden" id="p_FId" runat="server" />
                <input type="button" id="btnLook" value="查看工程基本信息" class="m_btn_w8" onclick="seePrj();" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="p_FPrjNo" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地点：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="p_FAllAddress" runat="server" CssClass="m_txt" Width="224px" MaxLength="30"
                    ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备案部门：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="p_FManageDeptId" runat="server" CssClass="m_txt" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FName" runat="server" CssClass="m_txt" ReadOnly="true" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="e_FLinkMan" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="e_FTel" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                Email：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FEmail" runat="server" CssClass="m_txt" ReadOnly="true" Width="250px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                勘察单位信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                勘察单位名称：
            </td>
            <td>
                <asp:TextBox ID="k_FName" runat="server" CssClass="m_txt" Width="300px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="k_FLevelName" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                勘察项目负责人：
            </td>
            <td>
                <asp:TextBox ID="emp_FName" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
                <asp:Button ID="btnSelectEmp" runat="server" Text="选择..." CssClass="m_btn_w4" OnClick="btnSelectEmp_Click"
                    OnClientClick="return selEmp('emp_FEmpBaseInfo',1,this);" />
                <input type="hidden" id="emp_FEmpBaseInfo" runat="server" />
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="emp_FTel" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                手机：
            </td>
            <td>
                <asp:TextBox ID="emp_FCall" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                E-Mail：
            </td>
            <td>
                <asp:TextBox ID="emp_FEmail" runat="server" CssClass="m_txt" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                职称：
            </td>
            <td>
                <asp:DropDownList ID="emp_FTechId" runat="server" Enabled="false">
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td>
                <asp:TextBox ID="emp_FCertiNo" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                计划开始时间：
            </td>
            <td>
                <asp:TextBox ID="t_FDate1" runat="server" CssClass="m_txt" Width="90" onfocus="WdatePicker()" ></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                计划结束时间：
            </td>
            <td>
                <asp:TextBox ID="t_FDate2" runat="server" CssClass="m_txt" Width="90" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" style="border-right: none">
                勘察人员
            </td>
            <td class="t_r t_bg" style="border-left: none">
                <asp:Button ID="btnSelEmpList" runat="server" Text="选择" CssClass="m_btn_w4" OnClientClick="return selEmp('hidd_empId',2,this,1);"
                    UseSubmitBehavior="false" OnClick="btnSelEmpList_Click" />
                <input type="hidden" id="hidd_empId" runat="server" />
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w4" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" />
            </td>
        </tr>
        <tr>
            <td class="t_bg m_txt_M" colspan="2">
                <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" DataKeyNames="FId"
                    EmptyDataText="还没有选择勘察人员" Style="margin-top: 0px;" 
                    onrowcommand="DG_List_RowCommand">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="30px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <asp:Label ID="lbautoid" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="姓名" DataField="FName"></asp:BoundField>
                        <asp:BoundField HeaderText="从事专业" DataField="FMajor"></asp:BoundField>
                        <asp:TemplateField HeaderText="主要职责">
                            <HeaderStyle Width="230" />
                            <ItemTemplate>
                                <asp:TextBox ID="t_FFunction" runat="server" CssClass="m_txt" MaxLength="10" Text='<%#Eval("FFunction") %>'></asp:TextBox>
                                <asp:Button ID="btnSaveEmp" CommandName="cnSaveEmp" runat="server" Text="保存" CommandArgument='<%#Eval("FID") %>'
                                    class="m_btn_w2" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
