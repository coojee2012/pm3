<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JNCLBJSearch.aspx.cs" Inherits="Government_AppMain_JNCLBJSearch" %>
<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <base target="_self"></base>
    <script type="text/javascript">
        function FindUpStuff() {
            var items = $("#JustAppInfo_List").find(".checkboxItem > input[type=checkbox][checked]");
            if (items.length < 1) {
                alert("当前未选择任何项");
                return false;
            } else if (items.length > 1) {
                alert("只能选择一项进行查看");
                return false;
            }
            var YWBM = $(items[0]).parent("span").attr("YWBM");
            var url = "../../JNCLEnt/AppMain/Index.aspx?YWBM=" + YWBM;
            showAddWindow(url, 1000, 500);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    证书查询
                </th>
            </tr>
            <tr>
                <td class="t_r">企业名称：</td>
                <td align="left"><asp:TextBox ID="txtQYMC" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <td class="t_r">产品名称：</td>
                <td><asp:TextBox ID="txtCPMC" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <td rowspan="2" colspan="3" style="text-align: center; padding-right: 10px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                        Text="查询" />
                </td>
            </tr>
            <tr>
                <td class="t_r">产品类别：</td>
                <td>
                    <asp:DropDownList ID="ddlCPLB" runat="server" CssClass="m_txt" Width="150px"></asp:DropDownList>
                </td>
                <td class="t_r">审核结果：</td>
                <td>
                    <asp:DropDownList ID="ddlAuditResult" runat="server" CssClass="m_txt" Width="150px">
                        <asp:ListItem Value="0">--全部--</asp:ListItem>
                        <asp:ListItem Value="1">通过</asp:ListItem>
                        <asp:ListItem Value="3">不通过</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
         <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <input type="button" class="m_btn_w2" onclick="FindUpStuff()" value="查看" />
                    &nbsp;<asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" 
                        CssClass="m_btn_w2" Text="导出" onclick="btnOut_Click" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemStyle Width="20px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" CssClass="checkboxItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="产品名称" HeaderStyle-Width="100px" DataField="SQCPMC">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="产品类别" HeaderStyle-Width="100px" DataField="CPLBMC">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="标识等级" DataField="BSDJMC" HeaderStyle-Width="100px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="企业名称" DataField="QYMC" HeaderStyle-Width="100px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="上报时间" HeaderStyle-Width="100px" DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="办结时间" HeaderStyle-Width="100px" DataField="FTime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核结果" DataField="idear" HeaderStyle-Width="80px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="YWBM" Visible="False"></asp:BoundColumn>
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



