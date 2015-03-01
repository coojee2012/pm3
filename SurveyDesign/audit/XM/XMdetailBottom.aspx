<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XMdetailBottom.aspx.cs" Inherits="audit_XM_XMdetailBottom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">项目信息
                </th>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">项目名称：
                </td>
                <td>
                    <asp:TextBox ID="t_xmmc" Width="250px" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                </td>
                <td class="t_r t_bg">项目地址：
                </td>
                <td>
                    <asp:TextBox ID="t_xmdz" Width="250px" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">建设规模：
                </td>
                <td>
                    <asp:TextBox ID="t_jsgm" Width="250px" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                </td>
                <td class="t_r t_bg">建设单位：
                </td>
                <td>
                    <asp:TextBox ID="t_jsdw" Width="250px" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">工程地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_gcdd" Width="450px" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                </td>               
            </tr>
        </table>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">参建单位
                </th>
            </tr>
        </table>
        <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
            AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="" HeaderText="参与角色" HeaderStyle-Width="100px" Visible="false">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cjdwmc" HeaderText="企业名称" HeaderStyle-Width="300px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cjdwfr" HeaderText="企业法人" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cjdwzxzzjdj" HeaderText="资质等级" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CJDWDZ" HeaderText="单位地址" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>                
            </Columns>
        </asp:DataGrid>
    </form>
</body>
</html>
