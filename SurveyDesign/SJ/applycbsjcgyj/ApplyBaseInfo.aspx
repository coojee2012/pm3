<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="KC_ApplyKCCGYJ_ApplyBaseInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>初步设计成果移交</title>
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
            if (!AutoCheckInfo()) {
                return false;
            }
            if (!getLength(document.getElementById("d_FTxt20"), 100, '“施工图设计文件编制结论”')) {
                return false;
            }
            if (!getLength(document.getElementById("d_FTxt21"), 100, '“初步设计结论”')) {
                return false;
            }
            return true;
        }
        function seePrj() {
            var fid = $('#hidd_FDataID').val();
            showAddWindow('../applycbsjwt/ApplyBaseInfo.aspx?FDataID=' + fid, 900, 700);
        }
        function selEnt(fsysid, obj, isCkAll) {
            var url = '?fsysid=' + fsysid + "&fspe=1"; //表示专业负责人
            if (isCkAll == 1) {
                url += '&ckAll=1';
            }
            url += '&fAppId=<%= Request.QueryString["FAppId"]%>&c=<%=Request.QueryString["c"]%>';
            var pid = showWinByReturn('../appmain/EmpListSel.aspx' + url, 700, 500);
            if (pid != null && pid != '') {
                __doPostBack(obj.id, '');
            }
        } 
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                初步设计成果移交
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r" style="padding-right: 10px;">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w4"
                    OnClientClick="return checkInfo();" />
                <asp:Button ID="btnReport" runat="server" Text="提交" OnClientClick="if(checkInfo())return confirm('确定要提交吗，提交后将不所修改。');else return false;"
                    OnClick="btnReport_Click" class="m_btn_w4" />
                <input type="button" class="m_btn_w2" value="返回" onclick="window.close();" />
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
                <input type="button" id="btnLook" value="查看合同备案信息" class="m_btn_w8" onclick="seePrj()" />
                <input type="hidden" id="hidd_FDataID" runat="server" />
                <input type="hidden" id="p_FId" runat="server" />
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
                建设单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FName" runat="server" CssClass="m_txt" ReadOnly="true" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同备案确认时间：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" ReadOnly="true" Width="90"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width: 150px;">
                计划开始时间：
            </td>
            <td>
                <asp:TextBox ID="d1_FDate1" runat="server" CssClass="m_txt" ReadOnly="true" Width="90"></asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width: 150px;">
                计划结束时间：
            </td>
            <td>
                <asp:TextBox ID="d1_FDate2" runat="server" CssClass="m_txt" ReadOnly="true" Width="90"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                实际开始时间：
            </td>
            <td>
                <asp:TextBox ID="d_FDate4" runat="server" CssClass="m_txt" Width="90" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                实际结束时间：
            </td>
            <td>
                <asp:TextBox ID="d_FDate5" runat="server" CssClass="m_txt" Width="90" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                初步设计完成标志：
            </td>
            <td colspan="3">
                <asp:TextBox ID="d_FTxt15" runat="server" CssClass="m_txt" MaxLength="25" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                初步设计结论：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="d_FTxt21" runat="server" CssClass="m_txt" MaxLength="100" Width="250px"
                    TextMode="MultiLine" Height="60"></asp:TextBox>
                （100字）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                成果移交办理人：
            </td>
            <td>
                <asp:TextBox ID="d_FTxt1" runat="server" CssClass="m_txt" Width="90" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                办理时间：
            </td>
            <td>
                <asp:TextBox ID="d_FDate3" runat="server" CssClass="m_txt" Width="90" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" style="border-right: none; padding-left: 20px; color: Red;">
                专业负责人情况
            </td>
            <td class="t_r t_bg" style="border-left: none">
                <asp:Button ID="btnSelE" runat="server" Text="选择" CssClass="m_btn_w4" OnClientClick="return selEnt(0,this,1);"
                    UseSubmitBehavior="false" CommandName="ZC" OnClick="btnSel_Click" />
                <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w4" OnClick="btnReload_Click" />
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
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FEmpName" HeaderText="姓名"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="负责专业">
                <ItemTemplate>
                    <asp:TextBox ID="t_FSpecialName" runat="server" Text='<%#Eval("FSpecialName") %>'
                        CssClass="m_txt" Width="150px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FCertiNo" HeaderText="注册证号">
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="操作">
                <HeaderStyle Width="80" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnSave" runat="server" CommandName="Save" Text='保存' CommandArgument='<%#Eval("FID") %>'>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
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
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="4" class="t_bg" style="padding-left: 20px; color: Red;">
                附件：
                <asp:Button ID="btnQuery" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnShowFile_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="m_txt_M">
                <table class="m_dg1" width="100%" align="center">
                    <tr class="m_dg1_h">
                        <th style="width: 30px;">
                            序号
                        </th>
                        <th>
                            资料名称
                        </th>
                        <th>
                            是否必需
                        </th>
                        <th style="width: 60px;">
                            已上传<br />
                            文件个数
                        </th>
                        <th style="width: 160px;">
                            <font color="green">是</font>/<font color="red">否</font> 上传
                        </th>
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
    </form>
</body>
</html>
