<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Government_main_Default" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
            DynamicGrid(".m_dg1_i");
        });
        function QualiIdear(FAPPID, type) {
            showApproveWindow('../../Goverment/Approve/EntAppIdear.aspx?FAPPID=' + FAPPID + '&type=' + type + '&rid=' + Math.random(), 400, 300);
        }
        function control(FID, obj, msg) {
            if (confirm(msg)) {
                document.getElementById('hidd_FID').value = FID;
                obj.click();
            }
        }
        function toMainPage(sUrl) {
            window.top.location.href = sUrl;
        }
        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";

            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);

            if (idvalue == "1") {
                form1.btnQuery.click();
            }


        }
        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret == "1") {
                form1.btnQuery.click()
            }
        }
        function openWinNew(Url) {
            var rid = Math.random();
            var newopen = window.open(Url + "&rid=" + rid, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no");
            if (newopen && newopen != null) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table align="center" width="98%" style="margin-top: 4px;">
        <tr>
            <td style="width: 100%; padding-right: 2px;" valign="top">
                <div class="i_top">
                    <i></i><span>待办理事项</span><b></b>
                </div>
                <div style="height: 100%" class="indexIMGBorder">
                    <div id="module_1" style="position: relative; padding-bottom: 10px;">
                        <table class="TableBlock" width="100%" cellspacing="0" cellpadding="1">
                            <tr class="TableData">
                                <td colspan="2" style="padding: 4px; background: #FFFFFF;" valign="top">
                                    <table id="tb_1" class="m_table" width="98%" align="center">
                                        <tr>
                                            <td class="t_bg" style="padding-left: 26px; background-image: url(../../image/img_JJ.gif);
                                                background-position: 10px 5px; background-repeat: no-repeat;">
                                                共有待办理事项：
                                                <asp:Literal ID="liCount" runat="server"></asp:Literal>项
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" style="height: 40px">
                                                <asp:Repeater ID="repManageType" runat="server">
                                                    <ItemTemplate>
                                                        <div class='mytable_list f_l' style='white-space: nowrap; width: 179px;'>
                                                            <tt></tt><a href="javascript:gotoPage('<%# Eval("FNumber") %>','<%# Eval("FName") %>','<%# Eval("Url") %>')"
                                                                target="left">
                                                                <%# Eval("FName") %>：<font color='red'><%# Eval("Count")%>
                                                                </font>项</a>;
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="width: 98%; margin: 0 auto;">
                                        <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                            HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="100%">
                                            <HeaderStyle CssClass="m_dg1_h" />
                                            <ItemStyle CssClass="m_dg1_i" />
                                            <%-- <AlternatingItemStyle CssClass="m_dg1_i" />--%>
                                            <Columns>
                                                <asp:BoundColumn HeaderText="序号">
                                                    <HeaderStyle Font-Underline="False" Width="30px" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FEmpName" HeaderText="工程名称">
                                                    <ItemStyle CssClass="t_l" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="业务名称">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="FLeadName" HeaderText="建设单位" ItemStyle-HorizontalAlign="Left">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="上报时间">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FState" HeaderText="审核结果"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="操作">
                                                    <HeaderStyle Width="60" />
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                        <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                                        <div style="margin: 4px auto;">
                                            <tt>提示：点击“工程名称”查看上报资料。</tt>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
        Style="display: none" Text="查询" />
    <input id="hidd_FID" type="hidden" runat="server" />
    </form>
</body>
</html>
