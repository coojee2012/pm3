<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjList.aspx.cs" Inherits="Government_statis_PrjList" %>

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
                工程类别：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server">
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
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 6px;"
        Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="工程名称">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                <ItemTemplate>
                    <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
                    <a href='javascript:showAddWindow("../../JSDW/appmain/AddPrjRegist.aspx?FID=<%#Eval("FID") %>",900,700);'>
                        <%#Eval("FPrjName")%></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="建设单位" DataField="jsdw">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="工程所属区域">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="工程类别">
                <HeaderStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="初步设计"></asp:BoundColumn>
            <asp:ButtonColumn HeaderText="初步设计文件审查"></asp:ButtonColumn>
            <asp:ButtonColumn HeaderText="项目勘察"></asp:ButtonColumn>
            <asp:ButtonColumn HeaderText="勘察文件审查"></asp:ButtonColumn>
            <asp:ButtonColumn HeaderText="施工图设计文件编制"></asp:ButtonColumn>
            <asp:ButtonColumn HeaderText="施工图设计文件审查"></asp:ButtonColumn>
            <asp:TemplateColumn HeaderText="查看">
                <ItemTemplate>
                    <a href='javascript:showAddWindow("Prjall.aspx?FID=<%#Eval("FID") %>",900,700);'>查看</a>
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
    <div style="width: 98%; margin: 4px auto;">
        <img src="../../image/s_yes.gif" />
        <font color='green'>已办理完成</font>
        <img src="../../image/s_no.gif" />
        <font color='#888888'>未办理或未办理完</font>
    </div>
    </form>
</body>
</html>
