<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BadManageList.aspx.cs" Inherits="Government_AppEntAction_BadManageList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')
            if (ret == "1") {
                form1.btnQuery.click();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                不良行为发布管理
            </th>
        </tr>
        <tr>
            <td class="t_r">
                企业名称：
            </td>
            <td width="230">
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="175px"></asp:TextBox>
            </td>
            <td class="t_r">
                企业类别：
            </td>
            <td width="150">
                <asp:DropDownList ID="dbFSystemId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                是否发布：
            </td>
            <td width="150">
                <asp:DropDownList ID="dbFState" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="">--请选择--</asp:ListItem>
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                处罚机构：
            </td>
            <td>
                <asp:TextBox ID="txtFDeptIdName" runat="server" CssClass="m_txt" Width="175px"></asp:TextBox>
            </td>
            <td class="t_r">
                填报单位：
            </td>
            <td>
                <asp:DropDownList ID="dbFDeptId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td colspan="2" class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Style="margin-left: 6px;"
                    Text="查询" OnClick="btnQuery_Click" />
            </td>
        </tr>
        
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_l">
                <input id="btnAdd" class="m_btn_w2" type="button" value="新增" onclick="showAddWindow('BadActionEdit.aspx?sysId=<%=Request.QueryString["sysId"] %>&e=0',625,780)" /><asp:Button ID="btnPub" runat="server" CssClass="m_btn_w2" Text="发布" OnClick="btnPub_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="m_btn_w2" Text="撤销" OnClick="btnCancel_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="BadAction_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" Style="margin-top: 4px" Width="98%" OnItemDataBound="BadAction_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn Visible="True">
                <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="企业名称" DataField="FName">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="企业类型" DataField="fsystemname">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="联系人" DataField="FLinkMan">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="联系电话" DataField="FTel">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="处罚机构" DataField="FDeptIdName">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="处罚时间" DataField="FDTime" DataFormatString="{0:yyyy-MM-dd}"
                Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="填报单位行政区划" DataField="FDeptName">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="发布单位" DataField="FAppDeptId" Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="状态">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="处罚次数" DataField="FCount">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="操作" Visible="false">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <ItemTemplate>
                    <asp:Label Text="信用" ID="lHistory" runat="server"></asp:Label>
                </ItemTemplate>
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FDeptId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FResult" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FSystemId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FBaseInfoId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="98%">
        <tr>
            <td style="height: 26px">
                <uc1:pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
