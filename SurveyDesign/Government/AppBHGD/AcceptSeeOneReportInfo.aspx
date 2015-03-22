<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AcceptSeeOneReportInfo.aspx.cs"
    Inherits="Government_AppZLJDBA_seeOneReportInfo" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>接件</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
  
        });       
        
        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret == "1") {
                form1.btnQuery.click()
            }
        }
        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";

            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);

            if (idvalue == "1") {
                form1.btnShowInfo.click();
            }

        }

        function checkBoxSelect(fParent, arrayIds) {

            var con = document.getElementById(fParent);
            var Ids = arrayIds.split(",");
            if (Ids == null || Ids.length == 0) {
                return;
            }
            else {
                for (var i = 0; i < Ids.length; i++) {

                    document.getElementById(Ids[i]).checked = con.checked;
                }
            }
        }



        function getValue() {
            var obj = window.dialogArguments;
            document.getElementById("HFID").value = obj.id;
            document.getElementById("HFSystemId").value = obj.fsystemid;

        }


        function WriteInfo(obj) {
            obj.disabled = true;
            obj.className = "m_btn_w6"
            obj.value = '提交中，请等待';
            return true;
        }
        function openWinNew(Url) {
            var newopen = window.open(Url, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no");
            if (newopen) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }
    </script>

    <base target="_self"></base>
    <style type="text/css">
        .auto-style1 {
            height: 39px;
        }
    </style>
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">

    <table width="100%" align="center" class="m_bar">
        
        <tr>
            <td class="m_bar_l">
                <asp:HiddenField  runat="server" ID="t_fSubFlowId" Value=""/>
                <asp:HiddenField  runat="server" ID="t_YWBM" Value=""/>
                <asp:HiddenField  runat="server" ID="t_fBaseInfoId" Value=""/>            
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 3px">
                <input id="HSeeReportInfo" type="button" runat="server" class="m_btn_w2" value="上报资料"  />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <input id="btnSave" type="button" runat="server" class="m_btn_w2" value="保存" onserverclick="btnSave_Click" />
                        <input id="btnAccept1" runat="server" class="m_btn_w4" onserverclick="btnAccept_Click" 
                            type="button" value="同意接件" />
                        <input id="btnRefuse" type="button" runat="server" class="m_btn_w4" value="不予受理" onserverclick="btnEndApp_Click"  />
                        <input id="btnReturnJSDW" type="button" runat="server" class="m_btn_w6" value="回退建设单位" onserverclick="btnBack_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <input id="btnReturn" type="button" runat="server" class="m_btn_w2" value="返回" onserverclick="btnReturn_Click" onclick="window.close();" />&nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                审核信息
            </th>
        </tr>
        <tr>
            <td class="t_r">
                审核时间：
            </td>
            <td>
                <asp:TextBox ID="txtFSeeTime" runat="server" CssClass="m_txt" Width="151px" Enabled="false"
                    TabIndex="100"></asp:TextBox>
            </td>
            <td class="t_r">
                审核人：
            </td>
            <td style="height: 24px; width: 159px;">
                <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                审核人职务：
            </td>
            <td>
                <asp:TextBox ID="t_FAppPersonJob" Enabled="false" runat="server" CssClass="m_txt"
                    Width="151px"></asp:TextBox>
            </td>
            <td class="t_r">
                审核部门：
            </td>
            <td>
                <asp:TextBox ID="t_FAppPersonUnit" Enabled="false" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                审核意见：
            </td>
            <td colspan="3" class="auto-style1">
                <asp:TextBox ID="t_FSeeOpinion" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                    Width="539px"></asp:TextBox>
            </td>
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
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="t_PrjItemName" ReadOnly="true" runat="server" CssClass="m_txt" Width="151px"></asp:TextBox>
            </td>
            <td class="t_r">
                工程类别：
            </td>
            <td>
                <asp:DropDownList ID="t_PrjItemType" runat="server" ReadOnly="true" CssClass="m_txt">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                建设单位：
            </td>
            <td>
                <asp:TextBox ID="t_JSDW" ReadOnly="true" runat="server" CssClass="m_txt" Width="151px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                建设地址：
            </td>
            <td colspan="3">
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
    <div style="width: 100%; margin: 0px auto;">
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
    </form>
</body>
</html>

<script language="javascript">
    
    function checkAllItem() {
        var form = form1;
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == "checkbox" && !form.elements[i].disabled) {
                if (form.elements[i].id.indexOf('CheckItem') > -1) {
                    var e = form.elements[i];
                    e.checked = true;

                }
            }
        }
    }
    checkAllItem();
</script>

