<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="KcsjSgt_ApplyKCCXXSC_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>勘察文件审查--审查人员安排 </title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();


            //选择是否同意时切换要填写的内容
            $("#t_FInt1").change(function() {
                tab();
            });
        });

        //选择是否同意时切换要填写的内容
        function tab() {
            var v = $("#t_FInt1").val();
            $("table[id^=tab_]").hide();
            $("table[id=tab_" + v + "]").show();
        }

        function checkInfo() {
            return AutoCheckInfo();
        }

        //选择人员
        function selEmp(tagId, fsysid, obj, isCkAll) {
            var url = '?fsysid=' + fsysid;
            if (isCkAll == 1) {
                url += '&ckAll=1';
            }
            url += '&fAppId=<%= Request.QueryString["FAppId"]%>&c=<%= Request.QueryString["c"]%>';
            var pid = showWinByReturn('../AppMain/EmpListSel.aspx' + url, 700, 500);
            if (pid != null && pid != '') {
                if (tagId != null && tagId != '')
                    $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }
        function seePrj() {
            var fid = $('#hidd_DataFId').val();
            showAddWindow('../ApplyKCWJSCWTSL/ApplyBaseInfo.aspx?FDataID=' + fid, 900, 700);
        }
        function doCheck(obj) {
            var v1 = $(obj).parent().prev().find(":text[id*=t_FFunction]").val();
            if (v1 != undefined && (v1 == null || v1 == '')) {
                alert("请填写主要职责！");
                return false;
            }
            $('#h_FFunction').val(v1);
            return true;
        } 
    </script>

    <style type="text/css">
        .hi { display: none; }
    </style>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Literal ID="lit_title" runat="server" Text="勘察文件审查--审查人员安排 "></asp:Literal>
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_bg" style="padding-left: 20px; color: Red;">
                各步骤办理情况：
            </td>
        </tr>
        <tr>
            <td class="m_txt_M">
                <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" DataKeyNames="FId"
                    EmptyDataText="没有办理情况" Style="margin: 0px;">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_select" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="步骤名称"></asp:BoundField>
                        <asp:BoundField HeaderText="办理时间" DataField="FAppTime" DataFormatString="{0:d}">
                            <HeaderStyle Width="80" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="办理意见" DataField="FContent">
                            <ItemStyle CssClass="t_l" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <HeaderStyle Width="80" />
                            <ItemTemplate>
                                <a href='javascript:showAddWindow("../AppMain/LookIdea.aspx?FID=<%#Eval("FID") %>",500,350);'>
                                    查看详情</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_bg" style="padding-left: 20px; color: Red; border-right: none;">
                本步骤办理情况
            </td>
            <td class="t_bg t_r" colspan="3" style="border-left: none;">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w4"
                    OnClientClick="return checkInfo();" />
                <asp:Button ID="btnReport" runat="server" Text="提交" OnClick="btnReport_Click" CssClass="m_btn_w4"
                    OnClientClick="if (confirm('确定提交吗？')){return checkInfo();}else{return false;}"
                    Style="margin-left: 10px;" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="150">
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
                <input type="button" id="btnLook" value="查看合同备案信息" class="m_btn_w8" onclick="seePrj()" />
                <input id="hidd_FPrjId" runat="server" type="hidden" />
                <input id="hidd_DataFId" runat="server" type="hidden" />
                <input id="hidd_JSDWFBaseinfoId" runat="server" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                办理时间：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FDate5" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                项目负责人：
            </td>
            <td style="line-height: 20px; padding: 3px;">
                <asp:Literal ID="lit_oldEmp" runat="server"></asp:Literal>
                <asp:TextBox ID="emp_FName" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
                <asp:Button ID="btnSelectEmp" runat="server" Text="选择..." CssClass="m_btn_w4" OnClick="btnSelectEmp_Click"
                    OnClientClick="return selEmp('emp_FEmpBaseInfo','1-2',this);" />
                <input type="hidden" id="emp_FEmpBaseInfo" runat="server" />
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="emp_FTel" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                手机：
            </td>
            <td>
                <asp:TextBox ID="emp_FCall" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                E-Mail：
            </td>
            <td>
                <asp:TextBox ID="emp_FEmail" runat="server" CssClass="m_txt" MaxLength="30"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                从事专业：
            </td>
            <td>
                <asp:TextBox ID="emp_FFunction" runat="server" CssClass="m_txt" ReadOnly="true" Width="150"></asp:TextBox>
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
                <asp:TextBox ID="t_FDate3" runat="server" CssClass="m_txt" Width="90" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                计划结束时间：
            </td>
            <td>
                <asp:TextBox ID="t_FDate4" runat="server" CssClass="m_txt" Width="90" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_bg" style="padding-left: 20px; color: Red; border-right: none;">
                注册人员
            </td>
            <td class="t_r t_bg" style="border-left: none;">
                <asp:Button ID="btnSelEmpList" runat="server" Text="选择" CssClass="m_btn_w4" OnClientClick="return selEmp('hidd_empId2',2,this,1);"
                    UseSubmitBehavior="false" OnClick="btnSelEmpList_Click" CommandArgument="2" />
                <input type="hidden" id="hidd_empId" runat="server" />
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w4" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" CommandArgument="2" />
            </td>
        </tr>
        <tr id="tr_oldEmp2" runat="server" visible="false">
            <td colspan="2" style="padding: 4px; line-height: 20px;">
                <asp:Literal ID="lit_oldEmpList2" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_bg m_txt_M" colspan="2">
                <asp:GridView ID="EmpList1" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" DataKeyNames="FId" EmptyDataText="还没有选择注册人员" Style="margin-top: 0px;"
                    OnRowCommand="EmpList2_RowCommand">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="30px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAllByTag(this,'CheckItem_ZC');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckItem_ZC" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="姓名" DataField="FName"></asp:BoundField>
                        <asp:BoundField HeaderText="从事专业" DataField="FMajor"></asp:BoundField>
                        <asp:TemplateField HeaderText="主要职责">
                            <HeaderStyle Width="160" />
                            <ItemTemplate>
                                <asp:TextBox ID="t_FFunction" runat="server" CssClass="m_txt" MaxLength="10" Text='<%#Eval("FFunction") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemStyle Wrap="false" />
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("FId") %>' CommandName="doSave"
                                    OnClientClick="return doCheck(this);" runat="server">保存</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="fEmpBaseInfo" ItemStyle-CssClass="hi" HeaderStyle-CssClass="hi">
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_bg" style="padding-left: 20px; color: Red; border-right: none;">
                非注册人员
            </td>
            <td class="t_r t_bg" style="border-left: none">
                <asp:Button ID="Button1" runat="server" Text="选择" CssClass="m_btn_w4" OnClientClick="return selEmp('hidd_empId3',3,this,1);"
                    UseSubmitBehavior="false" OnClick="btnSelEmpList_Click" CommandArgument="3" />
                <input type="hidden" id="Hidden1" runat="server" />
                <asp:Button ID="Button2" runat="server" Text="删除" CssClass="m_btn_w4" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" CommandArgument="3" />
            </td>
        </tr>
        <tr id="tr_oldEmp3" runat="server" visible="false">
            <td colspan="2" style="padding: 4px; line-height: 20px;">
                <asp:Literal ID="lit_oldEmpList3" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_bg m_txt_M" colspan="2">
                <asp:GridView ID="EmpList2" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" DataKeyNames="FId" EmptyDataText="还没有选择注册人员" Style="margin-top: 0px;"
                    OnRowCommand="EmpList2_RowCommand">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="30px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAllByTag(this,'CheckItem_FZC');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckItem_FZC" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="姓名" DataField="FName"></asp:BoundField>
                        <asp:BoundField HeaderText="从事专业" DataField="FMajor"></asp:BoundField>
                        <asp:TemplateField HeaderText="主要职责">
                            <HeaderStyle Width="160" />
                            <ItemTemplate>
                                <asp:TextBox ID="t_FFunction" runat="server" CssClass="m_txt" MaxLength="10" Text='<%#Eval("FFunction") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemStyle Wrap="false" />
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" CommandArgument='<%#Eval("FId") %>' CommandName="doSave"
                                    OnClientClick="return doCheck(this);" runat="server">保存</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="fEmpBaseInfo" ItemStyle-CssClass="hi" HeaderStyle-CssClass="hi" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <input id="h_FFunction" type="hidden" runat="server" />
    </form>
</body>
</html>
