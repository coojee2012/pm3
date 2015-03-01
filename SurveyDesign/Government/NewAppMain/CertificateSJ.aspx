<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertificateSJ.aspx.cs" Inherits="Government_NewAppMain_CertificateSJ" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        function checkLSH(obj) {
            var lsh = $(':text[id*=t_LSH]', $(obj).parent().parent()).val();
            if (lsh == null || lsh == '') {
                alert('请填写流水号！');
                return false;
            }
            $('#h_LSH').val(lsh);
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Literal ID="lPostion" runat="server">合格证查询</asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                工程名称：
            </td>
            <td class="txt34">
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
            </td>
            <td class="t_r">
                审查时间：
            </td>
            <td class="txt34">
                <asp:TextBox ID="txtFReportDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="65px"></asp:TextBox>
                至<asp:TextBox ID="txtFReportDate1" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="65px"></asp:TextBox>
            </td>
            <td align="center">
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                &nbsp;<input id="btnClear" class="m_btn_w2" onclick="clearPage();" type="button"
                    value="重置" />
                <asp:Button ID="btnOut" runat="server" CssClass="m_btn_w2" OnClick="btnOut_Click"
                    Text="导出" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" CssClass="m_dg1" Width="98%" HorizontalAlign="Center"
        AutoGenerateColumns="False" DataKeyNames="FId" EmptyDataText="当前没有数据" OnItemDataBound="DG_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn HeaderText="序号">
                <HeaderStyle Width="40px" />
                <ItemTemplate>
                    <asp:Label ID="lbautoid" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="工程名称">
                <ItemStyle CssClass="t_l" />
                <ItemTemplate>
                    <a href='javascript:showAddWindow("../../kcsjsgt/ApplyKCWJSCWTSL/ApplyBaseInfo.aspx?FDataID=<%#Eval("FLinkId") %>",800,700);'>
                        <%#Eval("FPrjName")%>
                    </a>
                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                    <asp:Literal ID="prj_Count" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="建设单位" DataField="FJSEnt" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="勘察单位" DataField="FKCEnt" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="设计单位" DataField="FSJEnt" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="审查日期" DataField="FAppDate" DataFormatString="{0:yyyy-MM-dd}">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="合格证流水号">
                <ItemStyle CssClass="t_c" />
                <ItemTemplate>
                    <%#Eval("FResult") %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="备案日期"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="备案号"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="98%">
        <tr>
            <td>
                <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    <input id="h_LSH" type="hidden" runat="server" />
    </form>
</body>
</html>
