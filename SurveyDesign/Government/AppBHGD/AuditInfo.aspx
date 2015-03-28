<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditInfo.aspx.cs" Inherits="Government_AppZLJDBA_AuditInfo" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function openWinNew(Url) {
            var newopen = window.open(Url, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no");
            if (newopen) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }
    </script>
    <style type="text/css">
        .cBtn7 {
            height: 21px;
        }

        .auto-style3 {
            height: 23px;
        }
        .m_txt {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="95%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                <asp:Label ID="lbTitle" runat="server" Text="审核信息">审核信息</asp:Label>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                项目名称：
            </td>
            <td>
                <asp:TextBox ID="t_ProjectName" ReadOnly="true" runat="server" CssClass="m_txt" Width="151px"></asp:TextBox>
            </td>
            <td class="t_r">
                备案编号：
            </td>
            <td>
                <asp:TextBox ID="t_RecordNo" Enabled="false" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                工程名称：<input id="HSeeReportInfo" type="button" runat="server" class="m_btn_w6" value="查看上报资料"  />
            </td>
            <td>
                <asp:TextBox ID="t_PrjItemName" ReadOnly="true" runat="server" CssClass="m_txt" Width="151px"></asp:TextBox>
            </td>
            <td class="t_r">
                工程类别：
            </td>
            <td>
                <asp:DropDownList ID="t_PrjItemType" runat="server" ReadOnly="true" CssClass="m_txt" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                建设单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JSDW" ReadOnly="true" runat="server" CssClass="m_txt" Width="220px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                建设地址：
            </td>
            <td colspan="3" class="auto-style3">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="224px" MaxLength="30" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                材料是否具备：
            </td>
            <td>
                <asp:CheckBox ID="chkHasFile" runat="server"/>
            </td>
        </tr>
    </table>
        <div style="width: 95%; margin: 0px auto;">
        <table class="m_dg1" width="100%" align="center">
            <tr class="m_dg1_h">
                <th style="width: 30px;">
                    序号
                </th>
                <th>
                    资料名称
                </th>
                <th>
                    需要文件数量
                </th>
                <th style="width: 60px;">
                    已上传<br />
                    文件个数
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
                            <%#Eval("FFileCount")%>
                        </td>
                        <td>
                            <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <asp:Repeater ID="rep_File" runat="server">
                        <ItemTemplate>
                            <tr class="m_dg1_i">
                                <td colspan="6" class="t_l" style="padding-left: 50px;">
                                    (<%# Container.ItemIndex+1 %>)、 <a href='<%#Eval("FFilePath") %>' target="_blank"
                                        title="点击查看该文件">
                                        <%#Eval("FFileName")%>
                                    </a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            pageindexboxtype="TextBox" PageSize="3" PrevPageText="上一页" ShowCustomInfoSection="Right"
            showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
        </webdiyer:AspNetPager>
    </div>
        <table width="95%" runat="server" align="center" class="m_title" id="table_audit_list">
            <tr>
                <th colspan="4">各级审批意见：
                </th>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
                        Width="100%">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <ItemStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:BoundColumn HeaderText="序号">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="30px" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FRoleDesc" HeaderText="审批岗位">
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="70px" CssClass="padLeft" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppPerson" HeaderText="审批人">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FCompany" HeaderText="审批人单位">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="审批日期">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="90px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FResult" HeaderText="审批结果">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FIdea" HeaderText="审批意见">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <table width="95%" align="center" class="m_title" id="accept" runat="server" visible="true">
            <tr>
                <th colspan="4" style="text-align: left;">接件意见
                </th>
            </tr>
            <tr>
                <td class="t_r">审查人
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">审查部门
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt"></asp:TextBox>
                    <asp:TextBox ID="t_FAppPersonJob" runat="server" CssClass="m_txt" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">是否受理
                </td>
                <td>
                    <asp:DropDownList ID="dResult" runat="server" CssClass="m_txt">
                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">审查时间
                </td>
                <td>
                    <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">审查意见
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FAppIdea" runat="server" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                        Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="95%" align="center" class="m_title" id="oneAudit" runat="server" visible="false">
            <tr>
                <th colspan="4" style="text-align: left;">本级审批意见
                </th>
            </tr>
            <tr>
                <td class="t_r">审批人
                </td>
                <td>
                    <asp:TextBox ID="t_Auditer" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">审批单位
                </td>
                <td>
                    <asp:TextBox ID="t_AuditUnit" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">审批人职务
                </td>
                <td>
                    <asp:TextBox ID="t_AuditFunction" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">审批时间
                </td>
                <td>
                    <asp:TextBox ID="t_AuditTime" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">是否审核通过
                </td>
                <td>
                    <asp:DropDownList ID="dAudit" runat="server" CssClass="m_txt">
                        <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                        <asp:ListItem Text="打回" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r">审批意见
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_AuditIdear" runat="server" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                        Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin-top: 2PX">
                            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>

                 &nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
            &nbsp;&nbsp;<asp:Button id="btnAccept" runat="server" class="m_btn_w4" type="button" Text="同意接件" OnClick="btnAccept_Click" />
            &nbsp;&nbsp;<asp:Button id="btnUPCS" runat="server" class="m_btn_w4"  type="button" Text="初审提交" OnClick="btnUPCS_Click" />
            &nbsp;&nbsp;<asp:Button id="btnUPFS" runat="server" class="m_btn_w4" type="button" Text="复审提交" OnClick="btnUPFS_Click" />
            &nbsp;&nbsp;<asp:Button id="btnUPEND" runat="server" class="m_btn_w2" type="button" Text="办结" OnClick="btnUPEND_Click" />
            &nbsp;&nbsp;<asp:Button ID="bthEndApp" runat="server" Text="审核不通过" class="m_btn_w6" OnClick="bthEndApp_Click"/>
            &nbsp;&nbsp;<asp:Button ID="btnBackToEnt" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w4"
                Text="打回企业" OnClientClick="return app('BackSeeOneReportInfo.aspx?type=1')" OnClick="btnBackToEnt_Click" />
            &nbsp;&nbsp;<asp:Button ID="btnBackToPre" runat="server" CssClass="m_btn_w2" Style="margin-left: 5px;" Text="打回" OnClick="btnBackToPre_Click"  />
            
                </ContentTemplate>
            </asp:UpdatePanel>
           &nbsp;&nbsp;<input id="btnReturn" type="button" runat="server" class="m_btn_w2" value="返回" onclick="window.close();" />
        </div>
 
        <input id="t_fLinkId" runat="server" type="hidden" />
        <input id="t_fTypeId" runat="server" type="hidden" />
        <input id="t_fSubFlowId" runat="server" type="hidden" />
        <input id="t_fBaseInfoId" runat="server" type="hidden" />
        <input id="t_fProcessRecordID" runat="server" type="hidden" />
        <input id="t_fProcessInstanceID" runat="server" type="hidden" />
    </form>
</body>
</html>
