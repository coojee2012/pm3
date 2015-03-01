<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertiList.aspx.cs" Inherits="Government_NewAppMain_CertiList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
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
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                <asp:Literal ID="lPostion" runat="server">备案情况</asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                工程名称：
            </td>
            <td class="txt34">
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r">
                备案编号：
            </td>
            <td>
                <asp:TextBox ID="txtFCertiNo" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td style="text-align: center" rowspan="3" colspan="2">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                    Text="查询" />
                <input id="btnClear" class="m_btn_w2" type="button" value="重置" onclick="clearPage();" />
                <asp:Button ID="btnOut" runat="server" CssClass="m_btn_w2" OnClick="btnOut_Click"
                    Text="导出" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                建设单位：
            </td>
            <td class="txt34">
                <asp:TextBox ID="txtFEntName" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r">
                备案时间：
            </td>
            <td>
                <asp:TextBox ID="txtFReportDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="80px"></asp:TextBox>
                至<asp:TextBox ID="txtFReportDate1" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                备案结果：
            </td>
            <td class="txt34" colspan="3" runat="server" id="td_type0">
                <asp:DropDownList ID="ddlState" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                    <asp:ListItem Text="不通过" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r" runat="server" id="td_type1" visible="false">
                备案类型：
            </td>
            <td runat="server" id="td_type2" visible="false">
                <asp:DropDownList ID="ddlFType" runat="server">
                    <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                    <asp:ListItem Value="283" Text="项目信息备案"></asp:ListItem>
                    <asp:ListItem Value="28003" Text="见证报告备案"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
        Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="40px" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="工程名称" DataField="FName" ItemStyle-HorizontalAlign="Left">
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="工程地址" DataField="FAddress" ItemStyle-HorizontalAlign="Left">
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="建设单位" DataField="FJSEntName" ItemStyle-HorizontalAlign="Left">
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="申报单位" DataField="FEntName" ItemStyle-HorizontalAlign="Left">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FAppDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="备案时间" />
            <asp:BoundColumn DataField="FCertiNo" HeaderText="备案编号"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="技术性审查结果" Visible="false"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <tt>*号表示自动审批</tt>
    <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
        CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
        CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
        NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
        PageIndexBoxType="TextBox" PageSize="15" PrevPageText="上一页" ShowCustomInfoSection="Right"
        ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
    </webdiyer:AspNetPager>
    </form>
</body>
</html>
