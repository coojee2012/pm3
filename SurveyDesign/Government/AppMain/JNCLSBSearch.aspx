<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JNCLSBSearch.aspx.cs" Inherits="Government_AppMain_JNCLSBSearch" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
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
        <table align="center" class="m_title" style="width: 98%;">
            <tr>
                <th colspan="7">
                    节能材料上报查询
                </th>
            </tr>
            <tr>
                <td class="t_r t_bg">企业名称：</td>
                <td><asp:TextBox ID="txtQYMC" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <td class="t_r t_bg">产品名称：</td>
                <td><asp:TextBox ID="txtCPMC" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <td class="t_r t_bg">产品类型：</td>
                <td>
                    <asp:DropDownList ID="ddlCPLB" runat="server" CssClass="m_txt" Width="150px">
                    </asp:DropDownList>
                </td>
                <td colspan="4" rowspan="2" style="text-align: left; padding-right: 10px; width: 150px;">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                        Text="查询" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">上报日期起：</td>
                <td><asp:TextBox ID="t_Stime" onfocus="WdatePicker()" runat="server" Width="150px" CssClass="m_txt"></asp:TextBox></td>
                <td class="t_r t_bg">上报日期止：</td>
                <td><asp:TextBox ID="t_Etime" onfocus="WdatePicker()" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox></td>
                <td class="t_r t_bg">当前所在位置：</td>
                <td>
                    <asp:DropDownList ID="ddlCurrentLocation" runat="server" CssClass="m_txt" Width="150px">
                        <asp:ListItem Value="">--全部--</asp:ListItem>
                        <asp:ListItem Value="">企业</asp:ListItem>
                        <asp:ListItem Value="1">接件</asp:ListItem>
                        <asp:ListItem Value="10">初审</asp:ListItem>
                        <asp:ListItem Value="15">复审</asp:ListItem>
                        <asp:ListItem Value="5">领导审批</asp:ListItem>
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
                 <asp:BoundColumn HeaderText="产品名称" DataField="SQCPMC" HeaderStyle-Width="150px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="产品类型" DataField="CPLBMC" HeaderStyle-Width="100px">
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
                <asp:BoundColumn HeaderText="上报日期" HeaderStyle-Width="100px" DataField="FReportTime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="当前所在位置" DataField="CurrentLocation" HeaderStyle-Width="100px">
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
