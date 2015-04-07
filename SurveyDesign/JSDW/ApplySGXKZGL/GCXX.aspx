<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GCXX.aspx.cs" Inherits="JSDW_ApplySGXKZGL_GCXX" %>
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
        function addTKJLInfo() {
            var FAppId = document.getElementById("t_fLinkId").value;
            var btn = document.getElementById("btnRefresh");
            showAddWindow('TKJLInfo.aspx?FAppId=' + FAppId, 600, 500, btn);
        }
        function addYZInfo() {
            var FAppId = document.getElementById("t_fLinkId").value;
            var btn = document.getElementById("btnReload");
            var FPrjItemId = document.getElementById("t_PrjItemId").value;
            //var FPrjId = document.getElementById("t_FPrjId").value;
            //var FPrjItemId = document.getElementById("t_FPrjItemId").value;
            showAddWindow('YZInfo.aspx?FAppId=' + FAppId + "&FPrjItemId=" + FPrjItemId, 600, 450, btn);
        }
        function LockEmpInfo() {

            var FAppId = document.getElementById("t_fLinkId").value;
            var FPrjItemId = document.getElementById("t_PrjItemId").value;
            showApproveWindow('SDRYSC.aspx?FAppId=' + FAppId + "&FPrjItemId=" + FPrjItemId, 600, 450);
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
                <asp:Label ID="lbTitle" runat="server" Text="工程信息">工程信息</asp:Label>
            </th>
        </tr>
        <tr>
           
            <td class="t_r">
                上报日期：
            </td>
            <td colspan="3" class="auto-style3">
                <asp:TextBox ID="t_ReportTime" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="t_PrjItemName" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                <input id="HSeeReportInfo" type="button" runat="server" class="m_btn_w6" value="查看上报资料"  /></td>
            <td class="t_r">
                工程类别：
            </td>
            <td>
                <asp:DropDownList ID="t_PrjItemType" runat="server" ReadOnly="true" CssClass="m_txt" Enabled="false" Width="201px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                建设单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JSDW" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                建设地址：
            </td>
            <td colspan="3" class="auto-style3">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="400px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td class="t_r">
                领证人：
            </td>
            <td>
                <asp:TextBox ID="t_LZR" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                电话：
            </td>
            <td>
                <asp:TextBox ID="t_LXDH" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
    </table>
        <br />
        <div style="width: 95%; margin: 0px auto;">       
        <table width="100%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    现场踏勘记录
                </td>
                <td class="t_r">
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_TKJL" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="DG_ListTKJL_ItemDataBound" Style="margin-top: 6px;
            margin-bottom: 1px;" Width="100%">
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
                <asp:BoundColumn HeaderText="踏勘时间" DataField="TKSJ" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Wrap="False"  />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="踏勘部门" DataField="TKBM">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="踏勘人员" DataField="TKRY">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="踏勘情况" DataField="TKQK">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="记录人"  DataField="JLR">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="记录时间"  DataField="JLSJ" DataFormatString="{0:yyyy-MM-dd}" >
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="附件">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <webdiyer:AspNetPager ID="Pager_TKJL" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager_TKJL_PageChanging"
            pageindexboxtype="TextBox" PageSize="3" PrevPageText="上一页" ShowCustomInfoSection="Right"
            showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
        </webdiyer:AspNetPager>
        </div>
        <br />
        <div style="width: 95%; margin: 0px auto;">       
       
        </div>
        <br />
        <table width="95%" runat="server" align="center" class="m_title" id="table_audit_list">
            <tr>
                <th colspan="4">各级审查意见：
                </th>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 1px"
                        Width="100%">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <ItemStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:BoundColumn HeaderText="序号">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="30px" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="FAppPerson" HeaderText="审查人">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FCompany" HeaderText="审查部门">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FFunction" HeaderText="审查人职务">
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="70px" CssClass="padLeft" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="审查日期" Visible="false">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="90px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FResult" HeaderText="审查结果">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FIdea" HeaderText="审查意见">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FName" HeaderText="审查环节">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn  HeaderText="备注">
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

     
 
        <input id="t_fLinkId" runat="server" type="hidden" />
        <input id="t_PrjItemId" runat="server" type="hidden" />
        <input id="t_fTypeId" runat="server" type="hidden" />
        <input id="t_fSubFlowId" runat="server" type="hidden" />
        <input id="t_fBaseInfoId" runat="server" type="hidden" />
        <input id="t_fProcessRecordID" runat="server" type="hidden" />
        <input id="t_fProcessInstanceID" runat="server" type="hidden" />
    </form>
</body>
</html>

