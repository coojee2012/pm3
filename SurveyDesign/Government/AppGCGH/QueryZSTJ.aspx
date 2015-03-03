<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryZSTJ.aspx.cs" Inherits="Government_AppGCGH_QueryZSTJ" %>
<!DOCTYPE html>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    办结查询
                </th>
            </tr>
            <tr>
                <td class="t_r">工程名称：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFEmpName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">企业名称：
                </td>
                <td>
                    <asp:TextBox ID="txtFEntName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">工程类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlAuditType" Width="120px" CssClass="m_txt" runat="server">
                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                        <asp:ListItem Value="4050">房建</asp:ListItem>
                        <asp:ListItem Value="4060">市政</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: center; padding-right: 10px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                        Text="查询" />
            </tr>
        </table>--%>
        <asp:DataGrid ID="dgList" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Width="98%" 
           onitemdatabound="dgList_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="地区" DataField="FName">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="总用地面积" DataField="JZZMJ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="改建" DataField="GJ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="新建" DataField="XJ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="扩建" DataField="KJ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="迁建" DataField="QJ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="恢复" DataField="HF">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="改迁建" DataField="GQJ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FNumber" Visible="False"></asp:BoundColumn>
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
    </form>
</body>
</html>

