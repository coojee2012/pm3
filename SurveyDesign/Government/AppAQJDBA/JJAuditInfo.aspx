<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JJAuditInfo.aspx.cs" Inherits="Government_AppAQJDBA_JJAuditInfo" %>
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
    </script>
    <style type="text/css">
        .cBtn7 {
            height: 21px;
        }

        .auto-style3 {
            height: 23px;
        }
        .m_txt {}
        .auto-style4 {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv"> 
                <div style="position:absolute;left:40%;top:50%;background-color:peru;border:solid 3px red;">
                    <table  align="center">
                    <tr>
                    <td ><h1>正在保存数据</h1></td>
                    <td><img src="../../image/load2.gif" alt="请稍候"/></td>
                    </tr>
                                    
                    </table>
                </div>
                    </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
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
                <asp:TextBox ID="t_ProjectName" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                备案编号：
            </td>
            <td>
                <asp:TextBox ID="t_RecordNo" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
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
    </table>
        <div style="width: 95%; margin: 0px auto;">
        <table class="m_dg1" width="100%" align="center">
            <tr class="m_dg1_h">
                <th style="width: 30px;">
                    序号
                </th>
                <th>
                    材料名称
                </th>
                <th style="width: 60px;">
                    份数
                </th>
                <th style="width: 50px;">
                    是否具备
                </th>
                <th style="width: 60px;">
                    电子件
                </th>
                <th style="width: 200px;">
                    备注
                </th>
                <th style="width: 50px;">
                 
                </th>
            </tr>
            <asp:Repeater ID="rep_List" runat="server" OnItemDataBound="rep_List_ItemDataBound" OnItemCommand="rep_List_ItemCommand">
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
                        <td >
                            <asp:CheckBox ID="chkIsReady"  runat="server" />
                        </td>
                        <td >
                            <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFileRemark" width="98%" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="update" CommandArgument='<%#Eval("FId")%>'>保存</asp:LinkButton>
                        </td>
                    </tr>
                    <asp:Repeater ID="rep_File" runat="server">
                        <ItemTemplate>
                            <tr class="m_dg1_i">
                                <td colspan="7" class="t_l" style="padding-left: 50px;">
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
        
        <table width="95%" align="center" class="m_title" id="accept" runat="server" >
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
                        onfocus="WdatePicker()" ></asp:TextBox>
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
            <tr style="display:none;">
                <td class="t_r">是否受理
                </td>
                <td>
                    <asp:DropDownList ID="dResult" runat="server" CssClass="m_txt" Width="201px">
                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">&nbsp;</td>
                <td>
                    &nbsp;</td>
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
            &nbsp;&nbsp;<asp:Button id="btnAccept" runat="server" class="m_btn_w4" type="button" Text="同意接件" OnClick="btnAccept_Click" />
                    &nbsp;&nbsp;<asp:Button ID="bthEndApp" runat="server" Text="不予受理" class="m_btn_w6" OnClick="bthEndApp_Click"/>
            &nbsp;&nbsp;<asp:Button ID="btnBackToEnt" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w6"
                Text="退回建设单位"  OnClick="btnBackToEnt_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
           &nbsp;&nbsp;<input id="btnReturn" type="button" runat="server" class="m_btn_w2" value="返回" onclick="window.returnValue = 1; window.close();" />
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
