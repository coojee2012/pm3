<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="EmpJZDW_appmain_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>提交</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();


            //选择是否同意时切换要填写的内容
            $("#t_FInt1").change(function() {
                tab();
            });
        });

        //选择是否同意时切换要填写的内容
        function tab() {
            var v = $("#t_FInt1").val();
            $("table[id^=tab_]").hide();
            $("table[id=tab_" + v + "]").show();
        }

        function checkInfo() {
            if ($("#t_FInt1").val() == "") {
                alert("请填写是否同意受理");
                $("#t_FInt1").focus();
                return false;
            }
            if ($("#t_FInt1").val() == "6") {
                if ($("#t_FTxt19").val() == "") {
                    alert("请填写办理意见");
                    $("#t_FTxt19").focus();
                    return false;
                }
                if ($(".m_dg1_i").length == 0) {
                    alert("请上传合同备案合同");
                    return false;
                }
            }
            else {
                if ($("#t_FTxt20").val() == "") {
                    alert("请填写退回原因");
                    $("#t_FTxt20").focus();
                    return false;
                }
            }
        }

        function up() {
            var width = "554";
            var height = "234";
            var idvalue = window.showModalDialog('UploadPhoto.aspx?rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:no;')
            if (idvalue != null && idvalue == "1") {
                document.getElementById('btnShowFile').click();
            }
        } 
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                见证意见
            </th>
        </tr>
    </table>
    <div id="divAll" runat="server">
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="3" style="border-right: none">
                    见证人员情况
                </td>
                <td class="t_r t_bg" style="border-left: none">
                    <asp:Button ID="btnAdd0" runat="server" Text="见证完毕" OnClick="btnAdd_Click" CssClass="m_btn_w4"
                        OnClientClick="return checkInfo();" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 3px;
            margin-bottom: 1px;" Width="98%"  >
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn Visible="false">
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
                <asp:BoundColumn DataField="FName" HeaderText="姓名" ItemStyle-Width="80px">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FMajor" HeaderText="从事专业">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="主要职责">
                    <ItemTemplate>
                        <%#Eval("FFunction") %>
                    </ItemTemplate>
                    <ItemStyle></ItemStyle>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="见证结论">
                    <ItemTemplate>
                        <asp:Literal ID="txtResult" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="见证时间" ItemStyle-Width="80px">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="操作">
                    <ItemTemplate>
                        <a href='javascript:showAddWindow("AddReport.aspx?FId=<%# Eval("FId") %>&FAppId=<% =FAppId%>",800,600);'><%# EConvert.ToInt(Session["FIsApprove"]) == 1?"查看意见":"填写意见"%></a>
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
    </div>
    <input id="k_FBaseInfoId" type="hidden" runat="server" />
    <input id="j_FBaseInfoId" type="hidden" runat="server" />
    </form>
</body>
</html>
