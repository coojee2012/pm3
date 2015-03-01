<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Staffing.aspx.cs" Inherits="JZDW_applykcxmwt_Staffing" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>见证人员安排</title>
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
            if (!AutoCheckInfo())
                return false;

            return true;
        }
        function selEnt(tagId, fsysid, obj, isCkAll) {
            var url = '?fsysid=' + fsysid;
            if (isCkAll == 1) {
                url += '&ckAll=1';
            }
            url += '&fAppId=<%=Request.QueryString["FAppId"] %>&c=<%=Request.QueryString["c"] %>';
            var pid = showWinByReturn('../appmain/EmpListSel.aspx' + url, 700, 500);
            if (pid != null && pid != '') {
                if (tagId != null && tagId != '')
                    $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }
        function doSave(obj) {
            var txt = $(obj).parent().prev().find(":text[id*=dg_List]");
            if ($(txt).val() != '' && $(txt).val() != null) {
                return true;
            }
            else {
                alert('请填写主要职责！');
                $(txt).focus();
                return false;
            }
        }
        function seePrj() {
            var fid = $('#hidd_FDataID').val();
            showAddWindow('ApplyBaseInfo.aspx?FDataID=' + fid, 900, 700);
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Literal ID="lit_title" runat="server" Text="见证人员安排"></asp:Literal>
            </th>
        </tr>
    </table>
    <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click" />
                <asp:Button ID="btnReport" runat="server" Text="安排完毕" CssClass="m_btn_w4" OnClick="btnReport_Click" />
                <input id="btnClose" type="button" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4" style="padding-left: 20px; color: Red;">
                见证确认情况：
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                办理人：
            </td>
            <td colspan="1" class="m_txt_M" width="20%">
                <asp:TextBox ID="s_FTxt18" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_r t_bg" width="20%">
                办理时间：
            </td>
            <td colspan="1">
                <asp:TextBox ID="s_FAppDate" runat="server" CssClass="m_txt" Width="100px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                办理意见：
            </td>
            <td class="m_txt_M" colspan="3">
                <asp:TextBox ID="s_FAppIdea" runat="server" CssClass="m_txt" ReadOnly="true" Height="50px"
                    Width="500px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4" style="padding-left: 20px; color: Red;">
                本步骤办理情况：
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="150">
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="300px" Enabled="false"></asp:TextBox>
                <input type="button" id="btnLook" value="查看合同备案信息" class="m_btn_w8" onclick="seePrj()" />
                <input type="hidden" id="hidd_FDataID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                办理时间：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" Width="100px" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                项目负责人：
            </td>
            <td style="line-height: 20px; padding: 3px;">
                <asp:Literal ID="lit_oldEmp" runat="server"></asp:Literal>
                <asp:TextBox ID="f_FName" runat="server" CssClass="m_txt" ReadOnly="true" Width="100px"></asp:TextBox>
                <tt>*</tt>
                <asp:Button ID="btnMod" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEnt('f_FEmpBaseInfo',1,this);"
                    UseSubmitBehavior="false" CommandName="SJ" OnClick="btnSel_Click" />
            </td>
            <td class="t_r t_bg" width="150">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="f_FTel" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                手机：
            </td>
            <td>
                <asp:TextBox ID="f_FCall" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                Email：
            </td>
            <td>
                <asp:TextBox ID="f_FEmail" runat="server" CssClass="m_txt" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                从事专业：
            </td>
            <td>
                <asp:TextBox ID="f_FMajor" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td>
                <asp:TextBox ID="f_FCertiNo" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                计划开始时间：
            </td>
            <td>
                <asp:TextBox ID="f_FBeginTime" runat="server" CssClass="m_txt" onfocus="WdatePicker();"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                计划结束时间：
            </td>
            <td>
                <asp:TextBox ID="f_FEndTime" runat="server" CssClass="m_txt" onfocus="WdatePicker();"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" style="border-right: none; padding-left: 20px; color: Red;">
                见证人员情况
            </td>
            <td class="t_r t_bg" style="border-left: none;">
                <asp:Button ID="btnAdd" runat="server" Text="选择" CssClass="m_btn_w4" OnClientClick="return selEnt('',2,this,1);"
                    UseSubmitBehavior="false" CommandName="ZC" OnClick="btnSel_Click" />
                <asp:Button ID="btnQuery" runat="server" Text="刷新" CssClass="m_btn_w4" OnClick="btnQuery_Click" />
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w4" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" />
            </td>
        </tr>
        <tr id="tr_oldEmp" runat="server" visible="false">
            <td colspan="2" style="padding: 4px; line-height: 20px;">
                <asp:Literal ID="lit_oldEmpList" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 3px;
        margin-bottom: 1px;" Width="98%" OnItemCommand="dg_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <HeaderStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <HeaderStyle Width="30px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="姓名"></asp:BoundColumn>
            <asp:BoundColumn DataField="FMajor" HeaderText="从事专业"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="主要职责">
                <ItemTemplate>
                    <asp:TextBox ID="DG_FFunction" runat="server" Text='<%#Eval("FFunction") %>' CssClass="m_txt"
                        Width="200px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="操作">
                <HeaderStyle Width="80" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnSave" runat="server" CommandName="Save" Text='保存' CommandArgument='<%#Eval("FID") %>'>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FEmpBaseInfo" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
    </div>
    <input id="f_FEmpBaseInfo" type="hidden" runat="server" />
    <input id="f_FId" type="hidden" runat="server" />
    </form>
</body>
</html>
