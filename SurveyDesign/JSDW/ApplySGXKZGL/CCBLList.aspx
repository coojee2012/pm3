<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CCBLList.aspx.cs" Inherits="JSDW_APPLYSGXKZGL_CCBLList" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>施工许可证管理初次办理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        function selPrj(obj) {
            var pid = showWinByReturn('../project/ProjectItemSel.aspx?ftype=<%=fMType %>', 700, 500);
            if (pid != null && pid != '') {
                $("#t_FPriItemId").val(pid);
                __doPostBack(obj.id, '');
            }
        }
        function checkInfo(obj) {
            if ($("#t_FPrjItemName").val() == '') {
                alert('请先选择项目！');
                return false;
            }
            return true;
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table height="95%" width="98%" align="center" style="margin-top: 10px; margin-bottom: 10px;">
            <tr>
                <td class="wxts_top_l"></td>
                <td class="wxts_top"></td>
                <td class="wxts_top_r"></td>
            </tr>
            <tr>
                <td class="wxts_l"></td>
                <td class=" wxts_m">
                    <div class="wxts_title">
                        施工许可证管理初次办理</div>
                    <div>
                        <table width="100%">
                            <tr>
                                <td align="left" valign="top">
                                    <table width="100%" id="appTab" runat="server">
                                        <tr>
                                            <td class="t_r"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 10px">
                                                <table width="100%" align="center" class="m_title">
                                                    <tr>
                                                        <td class="t_r" colspan="6">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="t_r">工程名称：
                                                        </td>
                                                        <td align="left">
                                                            
                                                            <asp:TextBox ID="txtFPrjItemName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                                                        </td>
                                                        <td class="t_r">
                                                            工程所属地：
                                                        </td>
                                                        <td>
                                                            <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" ReadOnly="true" />
                                                        </td>

                                                        <td colspan="2" rowspan="3" style="text-align: center; padding-right: 10px">
                                                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />&nbsp;<asp:Button ID="btnPup" runat="server" CssClass="m_btn_w12" OnClick="btn_Click" Text="新增业务"></asp:Button>

                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="t_r">建设地址：
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtJSDZ" runat="server" CssClass="m_txt" Width="561px"></asp:TextBox>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr style ="height:30px;">
                                                    <td class="t_r">
                                                        办理状态：
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFState" runat="server" Width="174px">
                                                            <asp:ListItem Value="-1">--全部--</asp:ListItem>
                                                            <asp:ListItem Value="0">未上报</asp:ListItem>
                                                            <asp:ListItem Value="1">已上报</asp:ListItem>
                                                            <asp:ListItem Value="2">退回</asp:ListItem>
                                                            <asp:ListItem Value="6">已办结</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="t_r">
                                                        审核结果：
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFResult" runat="server" Width="174px">
                                                            <asp:ListItem Value="-1">--全部--</asp:ListItem>
                                                            <asp:ListItem Value="1">通过</asp:ListItem>
                                                            <asp:ListItem Value="3">不通过</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                    <tr>
                                                        <td class="t_r">申报日期起：
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSDate" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                                                        </td>
                                                        <td class="t_r">申报日期止：
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEDate" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top" colspan="2">
                                                <asp:GridView ID="gv_list" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="False" OnRowDataBound="gv_list_RowDataBound" DataKeyNames="FId"
                                                    EmptyDataText="没有数据" EnableModelValidation="True" OnRowCommand="gv_list_RowCommand" OnRowCreated="gv_list_RowCreated">
                                                    <HeaderStyle CssClass="m_dg1_h" />
                                                    <RowStyle CssClass="m_dg1_i" />
                                                    <EmptyDataRowStyle CssClass="m_dg1_i" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="序号">
                                                            <HeaderStyle Width="40px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbautoid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="工程名称">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnItemSee" runat="server" CommandName="See" Text='<%# Bind("PrjItemName") %>' CommandArgument='<%#Eval("FID")+"@"+Eval("FState")%>'>
                                                                </asp:LinkButton>
                                                                <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                                                <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="工程所属地" DataField="PrjAddressDeptName" />
                                                        <asp:BoundField HeaderText="建设地址" DataField="Address" />
                                                        <asp:BoundField HeaderText="申报日期" DataField="FReportDate" DataFormatString="{0:d}">
                                                            <HeaderStyle Width="80" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="上报状态">
                                                            <HeaderStyle Width="120px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="操作">
                                                            <HeaderStyle Width="100" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnOp" CommandName="Op" Text="上报" runat="server" CommandArgument='<%#Eval("FID")%>'>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnDel" CommandName="Del" Text="删除" runat="server" CommandArgument='<%#Eval("FID")%>'
                                                                    Style="margin-left: 6px;">
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnBack" CommandName="Back" Text="撤销上报" runat="server" CommandArgument='<%#Eval("FID")%>'
                                                                     Style="margin-left: 6px;">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="审核结果"/>
                                                        <asp:BoundField DataField="FId" HeaderText="FId" Visible="False" />
                                                    </Columns>
                                                </asp:GridView>
                                                <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                                                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                                                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                                                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                                                    PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                                                    ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
                                                </webdiyer:AspNetPager>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table align="center" width="600" id="applyInfo" runat="server" visible="false" class="m_table">
                            <tr>
                                <td class="t_r">年度：
                                </td>
                                <td>
                                    <asp:TextBox ID="t_FYear" runat="server" CssClass="m_txt" onblur="isInt(this);" Width="100px"
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="t_r">申请业务名称：<tt>*</tt>
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="t_r">工程名称：<tt>*</tt>
                                </td>
                                <td>
                                    <asp:TextBox ID="t_FPrjItemName" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox>
                                    <input id="t_FPriItemId" type="hidden" runat="server" />
                                    <input id="t_FPrjId" type="hidden" runat="server" />
                                    <input id="t_FPrjName" type="hidden" runat="server" />
                                    <input id="t_AddressDept" type="hidden"  runat="server" />
                                    <input id="t_JSDWAddressDept" type="hidden" runat="server"/>
                                    <input id="t_PrjItemType" type="hidden" runat="server" />
                                    <input id="t_JSDW" type="hidden" runat="server" />
                                    <input id="t_OldFAppId" type="hidden" runat="server" />
                                    <input id="t_FCount" type="hidden" runat="server" />
                                    <asp:Button ID="btnSel" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selPrj(this);"
                                        UseSubmitBehavior="false" OnClick="btnSel_Click" />
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td class="t_r">建设单位：<tt>*</tt>
                                </td>
                                <td>
                                    <asp:TextBox ID="t_FJSDW" runat="server" CssClass="m_txt" Width="300px" MaxLength="30"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" height="30">
                                    <asp:Button ID="btnOk" runat="server" CssClass="m_btn_w2" OnClick="btnOk_Click" Text="确认"
                                        OnClientClick="return checkInfo(this);" />
                                    <input id="btnCancel" class="m_btn_w2" onclick="window.close();" style="margin-left: 10px"
                                        type="button" value="取消" onserverclick="btnCancel_ServerClick" runat="server" />
                                    <asp:Label ID="lblMessage" runat="server" ></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td class="wxts_r"></td>
            </tr>
            <tr>
                <td class="wxts_bot_l"></td>
                <td class="wxts_bot"></td>
                <td class="wxts_bot_r"></td>
            </tr>
        </table>
        <input id="HPid" type="hidden" runat="server" />
    </form>
</body>
</html>
