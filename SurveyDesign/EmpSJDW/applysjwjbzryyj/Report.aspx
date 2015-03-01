<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="JZDW_ApplyXMJZ_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>施工图设计文件编制人员意见</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });

        function checkInfo() {
            if ($('font[name=noidea]').size() > 0) {
                alert('请先填写完整所有设计人员的意见！');
                return false;
            }
            return true;
        }

        function up() {
            var width = "554";
            var height = "234";
            var idvalue = window.showModalDialog('UploadPhoto.aspx?rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:no;')
            if (idvalue != null && idvalue == "1") {
                document.getElementById('btnShowFile').click();
            }
        }
        function seePrj() {
            var fid = $('#hidd_FDataID').val();
            showAddWindow('../applysjwjbzryyj/ApplyBaseInfo.aspx?FDataID=' + fid, 900, 700);
            
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                施工图设计文件编制人员意见
            </th>
        </tr>
    </table>
    <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
    <div id="divAll" runat="server">
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" width="160">
                    工程名称：
                </td>
                <td>
                    <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
                    <input type="button" id="btnLook" value="查看合同备案信息" class="m_btn_w8" onclick="seePrj()" />
                    <input type="hidden" id="hidd_FState" runat="server" />
                    <input type="hidden" id="hidd_FDataID" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_l t_bg" style="border-right: none; padding-left: 20px; color: Red;">
                    设计人员意见
                </td>
                <td class="t_r t_bg" style="border-left: none">
                    <asp:Button ID="btnReport" runat="server" Text="确认完毕" OnClick="btnReport_Click" CssClass="m_btn_w4" />
                    <tt>*注意：完毕后不能修改！</tt>
                    <asp:Button ID="btnQuery" runat="server" Text="刷新" OnClick="btnQuery_Click" CssClass="m_btn_w2" />
                    <input type="button" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 3px;
            margin-bottom: 1px;" Width="98%">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
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
                <asp:TemplateColumn HeaderText="完成状态" Visible="false">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="施工图设计完成时间">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="操作">
                    <ItemTemplate>
                        <%--   <a href='javascript:showAddWindow("AddReport.aspx?FId=<%# Eval("FId") %>&FAppId=<%#Eval("FAppId")%>",800,600);'>
                            <%# EConvert.ToInt(Session["FIsApprove"]) == 1?"查看意见":"填写意见"%></a>--%>
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
