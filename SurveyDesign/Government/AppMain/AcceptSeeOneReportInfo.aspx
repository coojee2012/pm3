<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AcceptSeeOneReportInfo.aspx.cs"
    Inherits="Government_AppMain_seeOneReportInfo" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
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
    </script>

    <base target="_self"></base>
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                接件
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
            <td class="txt34" style="height: 24px; width: 159px;">
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
                审核人单位：
            </td>
            <td>
                <asp:TextBox ID="t_FAppPersonUnit" Enabled="false" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 3px">
                <input id="btnSave" runat="server" class="m_btn_w2" onclick="if(WriteInfo(this)){document.getElementById('btnAccept').click();}"
                    type="button" value="接件" />
                <input id="btnReturn" type="button" runat="server" class="m_btn_w2" value="返回" onclick="window.close();" />&nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
        Width="100%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Checked="true"
                        Enabled="false" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" Checked="true" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Width="50px" Wrap="False" />
                <HeaderStyle Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FEntName" HeaderText="企业名称">
                <ItemStyle HorizontalAlign="Left" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FManageTypeId" HeaderText="业务类型">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FListId" HeaderText="申报内容">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FTypeId" HeaderText="申请类别" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FLevelId" HeaderText="申请等级" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FIsPrime" HeaderText="是否主项" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="各级审核意见" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSubFlowId" HeaderText="当前所在位置">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FStateDesc" HeaderText="状态" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FYear" HeaderText="年度" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FReporttime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="上报时间">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSeeState" HeaderText="相关表格维护" Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSeeState" HeaderText="状态" Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSeeTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="受理时间"
                Visible="false">
                <ItemStyle Font-Underline="False" Width="80px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Width="80px" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FBaseInfoId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FManageTypeId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FMeasure" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FRId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FLinkId" Visible="False"></asp:BoundColumn>
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
    <input id="HFID" runat="server" type="hidden" value="INIT" />
    <input id="HFID1" runat="server" type="hidden" />
    <asp:Button ID="btnShowInfo" runat="server" OnClick="btnShowInfo_Click" Style="display: none" />
    <input id="HFSystemId" runat="server" type="hidden" />
    <asp:Button ID="btnAccept" runat="server" CssClass="cBtn7" Text="受理" OnClick="btnAccept_Click"
        Style="display: none" />
    <input id="HIsPsotBack" runat="server" type="hidden" value="0" />
    </form>
</body>
</html>

<script language="javascript">
    if (document.getElementById("HFID").value == "INIT") {
        getValue();
    }
    else {
        document.getElementById("HFID").value = "";
    }
    if (document.getElementById("HFID").value != "") {
        document.getElementById("HFID1").value = document.getElementById("HFID").value;
        document.getElementById("btnShowInfo").click();
    }
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

