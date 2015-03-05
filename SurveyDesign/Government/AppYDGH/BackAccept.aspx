<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackAccept.aspx.cs" Inherits="JSDW_ApplyYDGH_AuditMain_BackAccept" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退件</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script language="javascript" type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });

        function Hide(obj) {
            if ($("#entList").css("display") == "none") {
                $("#entList").attr("style", "display:block");
                obj.value = "隐藏退件数据";
            }
            else {
                $("#entList").attr("style", "display:none");
                obj.value = "显示退件数据";
            }
        }

        function checkInfo(obj) {
            if (document.getElementById("t_FAppIdea").value == ""
        || document.getElementById("t_FAppIdea").value == null) {
                alert("请填写意见!");
                return false;
            }
            obj.disabled = true;
            obj.value = "请稍后...";
            __doPostBack(obj.id, '');
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                退件
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                退件原因
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FAppIdea" runat="server" CssClass="m_txt" Height="84px" Style="word-break: break-all;
                    text-align: left; word-wrap: break-word" TextMode="MultiLine" Width="500px"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnApp" runat="server" CssClass="m_btn_w6" OnClick="btnReport_Click"
                    Text="打回企业" OnClientClick="return checkInfo(this);" UseSubmitBehavior="false" />
                <asp:Button ID="btnEnd" runat="server" CssClass="m_btn_w6" OnClick="btnEnd_Click"
                    Visible="false" Text="不予以受理" OnClientClick="return checkInfo(this);" UseSubmitBehavior="false" />
                <input type="button" id="btnBack" class="m_btn_w2" value="返回" onclick="javascript: window.close();" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <input id="btnHide" class="m_btn_w6" type="button" value="隐藏退件数据" onclick="Hide(this);" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <div id="entList" style="display: block">
        <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Width="98%">
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
                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
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
                <asp:BoundColumn DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="上报时间">
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
                <asp:BoundColumn DataField="FSeeTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="接件时间"
                    Visible="false">
                    <ItemStyle Font-Underline="False" Width="80px" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Width="80px" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FBaseInfoId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FERFId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FMeasure" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FLinkId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <input id="HFID" runat="server" type="hidden" value="INIT" />
    <input id="HFID1" runat="server" type="hidden" />
    <asp:Button ID="btnShowInfo" runat="server" OnClick="btnShowInfo_Click" Style="display: none" />
    </form>
</body>
</html>

<script language="javascript">
    //function getValue() {
    //    var obj = window.dialogArguments;
    //    document.getElementById("HFID").value = obj.id;
    //}
    //if (document.getElementById("HFID").value == "INIT") {
    //    getValue();
    //}
    //else {
    //    document.getElementById("HFID").value = "";
    //}
    //if (document.getElementById("HFID").value != "") {

    //    document.getElementById("HFID1").value = document.getElementById("HFID").value;
    //    document.getElementById("btnShowInfo").click();


    //}


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

