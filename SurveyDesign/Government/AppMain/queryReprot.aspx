<%@ Page Language="C#" AutoEventWireup="true" CodeFile="queryReprot.aspx.cs" Inherits="Government_AppMain_queryReprot" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">工法上报查询</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">企业名称：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">工法名称：
                </td>
                <td>
                    <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">类别：
                </td>
                <td>
                    <asp:DropDownList ID="t_FListName" Width="120px" CssClass="m_txt" runat="server" OnSelectedIndexChanged="t_FListName_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                        <asp:ListItem Value="房屋建筑工程">房屋建筑工程</asp:ListItem>
                        <asp:ListItem Value="土木工程">土木工程</asp:ListItem>
                        <asp:ListItem Value="工业安装工程">工业安装工程</asp:ListItem>
                        <asp:ListItem Value="其他">其他</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="4" rowspan="4" style="text-align: center; padding-right: 10px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                        Text="查询" />
                    &nbsp;
                <input id="btnClear" class="m_btn_w2" style="margin-top: 3px;" type="button" value="重置"
                    onclick="clearPage();" />&nbsp;
                    <asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w2"
                        OnClick="btnOut_Click" Text="导出" />
                </td>
            </tr>
            <tr>

                <td class="t_r">专业分类：
                </td>
                <td>
                    <asp:DropDownList ID="t_FTypeName" Width="120px" CssClass="m_txt" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="t_FTypeName1" CssClass="m_txt" Visible="false" runat="server"></asp:TextBox>
                </td>
                <td class="t_r">上报时间起：
                </td>
                <td align="left">
                    <asp:TextBox ID="t_Stime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">上报时间止：
                </td>
                <td>
                    <asp:TextBox ID="t_Etime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">当前位置：
                </td>
                <td colspan="5">
                    <asp:DropDownList ID="ddlWZ" Width="120px" CssClass="m_txt" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="1">厅接件</asp:ListItem>
                        <asp:ListItem Value="10">厅初审</asp:ListItem>
                        <asp:ListItem Value="15">公示</asp:ListItem>
                        <asp:ListItem Value="5">厅负责人</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%" OnItemCommand="JustAppInfo_List_ItemCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemStyle Width="20px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="工法名称" HeaderStyle-Width="200px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnQY" runat="server" CommandName="See" Text='<%#Eval("GFMC") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="类别" HeaderStyle-Width="100px" DataField="FListName">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="专业分类" DataField="FTypeName" HeaderStyle-Width="100px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="企业名称" HeaderStyle-Width="200px" DataField="FName">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="所属地" HeaderStyle-Width="80px" DataField="FUpDeptName">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="上报时间" HeaderStyle-Width="100px" DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="当前所在位置" DataField="FSubFlowId" HeaderStyle-Width="80px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FLinkId" Visible="False"></asp:BoundColumn>
            </Columns>
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
        <div class="d div1 tcen" style="width: 98%; margin: 0px auto;">
            <uc1:pager ID="Pager1" runat="server"></uc1:pager>
        </div>
    </form>
</body>
</html>
