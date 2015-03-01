<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppInfoPrj2.aspx.cs" Inherits="KC_appmain_AppInfoPrj2" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工程项目信息列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function seePrj(fid) {
            showAddWindow('../../JSDW/appmain/AddPrjRegist.aspx?fid=' + fid, 900, 700);
        }
        function doShow(fid, type, fUrl) {
            if (type == "1")
                seePrj(fid);
            else {
                if (fUrl == "F")
                    fUrl = "ApplyKCWJSCWTSL";
                else
                    fUrl = "ApplySGTSJWJSCWTSL";
                showAddWindow("../" + fUrl + "/ApplyBaseInfo.aspx?FDataID=" + fid, 800, 700);
            }
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                工程信息列表（<asp:Label ID="lTitle" runat="server" ForeColor="Red"></asp:Label>）
            </th>
        </tr>
        <tr>
            <td colspan="1" class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="1" class="t_l">
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
            </td>
            <td colspan="2" class="t_c">
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r" style="padding-right: 10px">
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px;
        margin-bottom: 1px;" Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="工程名称">
                <ItemStyle CssClass="t_l" Wrap="false" />
                <ItemTemplate>
                    <a href="javascript:doShow('<%#Eval("FLinkId") %>','<%#Eval("FType") %>','<%#Eval("FUrl") %>');">
                        <%#Eval("FPrjName")%>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FAddress" HeaderText="工程地点"></asp:BoundColumn>
            <asp:BoundColumn DataField="FJSEnt" HeaderText="建设单位"></asp:BoundColumn>
            <asp:BoundColumn DataField="FMoney" HeaderText="合同金额(万元)">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundColumn>
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
        <div style="width: 98%; margin: 4px auto;">
            提示：<tt>点击列表中“工程名称”可以查看工程信息。</tt>
        </div>
    </div>
    </form>
</body>
</html>
