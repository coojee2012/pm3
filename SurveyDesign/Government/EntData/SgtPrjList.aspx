<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SgtPrjList.aspx.cs" Inherits="Government_EntData_SgtPrjList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                工程项目情况
            </th>
        </tr>
        <tr>
            <td colspan="1" class="t_r">
                工程名称
            </td>
            <td class="t_l">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            </td>
        </tr>
    </table>
    <asp:Repeater ID="dg_List" runat="server" OnItemDataBound="dg_List_ItemDataBound">
        <HeaderTemplate>
            <table width="98%" align="center" class="m_dg1">
                <tr class="m_dg1_h">
                    <td>
                        序号
                    </td>
                    <td>
                        工程名称
                    </td>
                    <td>
                        工程地址
                    </td>
                    <td>
                        建设单位
                    </td>
                    <td>
                        业务名称
                    </td>
                    <td>
                        审查受理<br />
                        日期
                    </td>
                    <td>
                        审查结束<br />
                        日期
                    </td>
                    <td>
                        是否<br />一审<br />通过
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="m_dg1_i">
                <td rowspan="2">
                    <%#   (Container.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)) %>
                    <asp:HiddenField ID="hfFId" Value='<%# Eval("FId") %>' runat="server" />
                </td>
                <td rowspan="2">
                    <%# Eval("FPrjName")%>
                </td>
                <td rowspan="2">
                    <%# Eval("FAllAddress")%>
                </td>
                <td rowspan="2">
                    <asp:Literal ID="liEntName" runat="server" Text='<%# Eval("FBaseInfoId") %>'></asp:Literal>
                </td>
                </td>
                <td>
                    勘察文件审查
                </td>
                <td>
                    <asp:Literal ID="liAppDate1" runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:Literal ID="liSubmitDate1" runat="server"></asp:Literal>
                </td>
                <td>
                      <asp:Literal ID="liPass1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr class="m_dg1_i" id="t">
                <td>
                    施工图设计文件审查
                </td>
                <td>
                    <asp:Literal ID="liAppDate2" runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:Literal ID="liSubmitDate2" runat="server"></asp:Literal>
                </td>
                   <td>
                     <asp:Literal ID="liPass2" runat="server"></asp:Literal>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <br />
    </div>
    </form>
</body>
</html>
