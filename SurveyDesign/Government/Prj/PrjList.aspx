<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjList.aspx.cs" Inherits="Government_Prj_PrjList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                <asp:Literal ID="lit_Title" runat="server" Text="项目查询"></asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程所属区域：
            </td>
            <td>
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
            </td>
            <td class="t_r t_bg">
                项目终止方式：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server">
                    <asp:ListItem Value="" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="建设单位终止"></asp:ListItem>
                    <asp:ListItem Value="2" Text="管理部门终止"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td rowspan="2" class="t_c t_bg">
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                <input id="btnClear" type="button" value="重置" class="m_btn_w2" onclick="$('[id^=t_]').val('');" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="t_prjName" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                建设单位：
            </td>
            <td>
                <asp:TextBox ID="t_jsBaseName" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar" id="t_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_l">
                已终止项目列表
            </td>
            <td class="t_r">
                <input id="btnAdd" type="button" value="新增终止项目" class="m_btn_w6" onclick="showAddWindow('AddStopPrj.aspx?',600,400);" />
                <asp:Button ID="btnQuery1" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 6px;"
        Width="98%" OnItemCommand="dg_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="工程名称">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                <ItemTemplate>
                    <a href='javascript:showAddWindow("AddStopPrj.aspx?FID=<%#Eval("FID") %>",600,400);'>
                        <%#Eval("FPrjName")%></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="建设单位" DataField="jsdw">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="主管部门">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="项目终止时间" DataField="FAppDate" DataFormatString="{0:d}">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="项目终止原因" DataField="FRemark">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="项目终止方式">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="操作">
                <ItemTemplate>
                    <asp:Button ID="btnOp" runat="server" Text="恢复" CssClass="m_btn_w4" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="width: 98%; margin: 4px auto;">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            pageindexboxtype="TextBox" PageSize="15" PrevPageText="上一页" ShowCustomInfoSection="Right"
            showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>
