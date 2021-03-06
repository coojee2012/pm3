﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YQBLJJAuditInfo.aspx.cs" Inherits="Government_AppSGXKZGL_YQBLJJAuditInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>延期办理接件审核</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#chkHasFile").click(function () {
                $("input[name$=chkIsReady]").each(function () {
                    $(this).attr("checked", $("#chkHasFile").attr("checked"));
                });
            });
        });
        function openWinNew(Url) {
            var newopen = window.open(Url, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no");
            if (newopen) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }
        function checkAll(chk) {
            var form = chk.form;
            for (var i = 0; i < form.elements.length; i++) {
                if (form.elements[i].type == "checkbox" && !form.elements[i].disabled) {
                    if (form.elements[i].id.indexOf('chkIsReady') > -1) {
                        var e = form.elements[i];
                        if (e.name != chk.name)
                            e.checked = chk.checked;
                    }
                }
            }
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
        function LockPersonList() {
            var FAppId = document.getElementById("t_fLinkId").value;
            var FPrjItemId = document.getElementById("t_PrjItemId").value;
            showApproveWindow('LockPerson.aspx?FAppId=' + FAppId + "&FPrjItemId=" + FPrjItemId, 600, 450);
        }
         
    </script>
    <style type="text/css">
        .cBtn7 {
            height: 21px;
        }

        .auto-style3 {
            height: 23px;
        }

        .m_txt {
        }

        #btnAdd {
            height: 21px;
        }
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
                <td class="t_r">业务类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlMType" runat="server" CssClass="m_txt" Width="200
                    px"
                        Enabled="false">
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                        <asp:ListItem Value="11223">初次办理</asp:ListItem>
                        <asp:ListItem Value="11224" Selected="True">延期办理</asp:ListItem>
                        <asp:ListItem Value="11225">变更办理</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">上报日期：
                </td>
                <td>
                    <asp:TextBox ID="t_ReportTime" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">工程名称：
                </td>
                <td>
                    <asp:TextBox ID="t_PrjItemName" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                    <input id="HSeeReportInfo" type="button" runat="server" class="m_btn_w6" value="查看上报资料" />
                &nbsp;&nbsp;<input id="seeLockPerson" type="button" runat="server" style="margin-left: 5px;" class="m_btn_w6" onclick="LockPersonList();"  value="查看锁定人员"   />
                </td>
                <td class="t_r">工程类别：
                </td>
                <td>
                    <asp:DropDownList ID="t_PrjItemType" runat="server" ReadOnly="true" CssClass="m_txt" Enabled="false" Width="201px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r">建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_JSDW" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">建设地址：
                </td>
                <td colspan="3" class="auto-style3">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="400px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">领证人：
                </td>
                <td>
                    <asp:TextBox ID="t_LZR" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r">电话：
                </td>
                <td>
                    <asp:TextBox ID="t_LXDH" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_bg" colspan="4"><strong>延期申请</strong></td>
            </tr>
            <tr>
                <td class="t_r">施工许可证编号：
                </td>
                <td>
                    <asp:TextBox ID="y_SGXKZBH" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                    <input id="HSeeReportInfo0" type="button" runat="server" class="m_btn_w6" value="查看延期申报" /></td>
                <td class="t_r">发证机关：
                </td>
                <td>
                    <asp:TextBox ID="y_FZJG" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">发证日期：
                </td>
                <td>
                    <asp:TextBox ID="y_FZTime" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r">上次延期申请日期：
                </td>
                <td>
                    <asp:TextBox ID="y_LastYQTime" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">本次延期开始日期：
                </td>
                <td>
                    <asp:TextBox ID="y_YQStartTime" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r">本次延期截止日期：
                </td>
                <td>
                    <asp:TextBox ID="y_YQEndTime" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">本次申请人：
                </td>
                <td>
                    <asp:TextBox ID="y_YQSQR" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r">本次申请日期：
                </td>
                <td>
                    <asp:TextBox ID="y_YQTime" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">本次延期原因：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="y_YQYY" ReadOnly="true" runat="server" CssClass="m_txt" Width="98%" Height="38px"></asp:TextBox>
                </td>

            </tr>
        </table>
        <br />

        <div style="width: 95%; margin: 0px auto;">
            <table class="m_dg1" width="100%" align="center">
                <tr class="m_dg1_h">
                    <th colspan="5" align="left">接件材料
                    </th>
                </tr>
                <tr class="m_dg1_h">
                    <th style="width: 30px;">序号
                    </th>
                    <th style="width: 50px;">具备
                    </th>
                    <th>资料名称
                    </th>


                    <th style="width: 200px;">文件或证明材料情况</th>
                    <th style="width: 50px;"></th>
                </tr>
                <asp:Repeater ID="rep_List" runat="server" OnItemDataBound="rep_List_ItemDataBound" OnItemCommand="rep_List_ItemCommand">
                    <ItemTemplate>
                        <tr class="m_dg1_select">
                            <td>
                                <%# Container.ItemIndex+1 %>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsReady" runat="server" />
                            </td>
                            <td class="t_l">
                                <%#Eval("FFileName")%>
                            </td>


                            <td>
                                <asp:TextBox ID="txtFileRemark" Width="98%" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="update" CommandArgument='<%#Eval("FId")%>'>保存</asp:LinkButton>
                            </td>
                        </tr>
                        <asp:Repeater ID="rep_File" runat="server">
                            <ItemTemplate>
                                <tr class="m_dg1_i">
                                    <td colspan="7" class="t_l" style="padding-left: 50px;">(<%# Container.ItemIndex+1 %>)、 <a href='<%#Eval("FFilePath") %>' target="_blank"
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
        <br />

        <div style="width: 95%; margin: 0px auto;">

            <table width="100%" align="center" class="m_bar">
                <tr>
                    <td class="m_bar_l"></td>
                    <td>押证信息
                    </td>
                    <td class="t_r">
                        <asp:UpdatePanel ID="up25" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="addYZInfo();" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                            OnClick="btnDel_Click" />
                        <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                    </td>
                    <td class="m_bar_r"></td>
                </tr>
            </table>
            <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
                HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px; margin-bottom: 1px;"
                Width="100%">
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
                    <asp:BoundColumn HeaderText="持证人" DataField="CZR">
                        <ItemStyle Wrap="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="证书名称" DataField="FCertificateName">
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="收证人" DataField="SZR">
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="接收时间" DataField="FAcceptTime" DataFormatString="{0:yyyy-MM-dd}">
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="存放位置" DataField="FLocation">
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
            <webdiyer:AspNetPager ID="Pager2" runat="server" AlwaysShow="True" CssClass="pages"
                CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager2_PageChanging"
                pageindexboxtype="TextBox" PageSize="3" PrevPageText="上一页" ShowCustomInfoSection="Right"
                showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
            </webdiyer:AspNetPager>
        </div>
        <br />
        <table width="95%" align="center" class="m_title" id="accept" runat="server">
            <tr>
                <th colspan="4" style="text-align: left;">审查意见
                </th>
            </tr>
            <tr>
                <td class="t_r">审查人
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r">审查时间</td>
                <td>
                    <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" Width="195px" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">审查人职务
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPersonJob" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r">审查部门
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="t_r">是否受理
                </td>
                <td>
                    <asp:DropDownList ID="dResult" runat="server" CssClass="m_txt" Width="201px">
                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">&nbsp;</td>
                <td>&nbsp;</td>
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

        <div style="text-align: center; margin-top: 2PX">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    &nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    &nbsp;&nbsp;<asp:Button ID="btnAccept" runat="server" class="m_btn_w4" type="button" Text="同意接件" OnClick="btnAccept_Click" />
                    &nbsp;&nbsp;<asp:Button ID="bthEndApp" runat="server" Text="不予受理" class="m_btn_w6" OnClick="bthEndApp_Click" />
                    &nbsp;&nbsp;<asp:Button ID="btnBackToEnt" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w6"
                        Text="退回建设单位" OnClick="btnBackToEnt_Click" />
                    &nbsp;&nbsp;<input id="btnLockCheck" type="button" runat="server" style="margin-left: 5px;" class="m_btn_w6" onclick="LockEmpInfo();"
                        value="锁定人员核查" />
                </ContentTemplate>
            </asp:UpdatePanel>
            &nbsp;&nbsp;<input id="btnReturn" type="button" runat="server" class="m_btn_w2" value="返回" onclick="window.returnValue = '1'; window.close(); " />
        </div>

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
