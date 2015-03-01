<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CardNoSelect.aspx.cs" Inherits="Share_Sys_CardNoSelect" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
            DynamicGrid(".m_dg1_i");
        });
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                未发锁库
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                加密标签编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockLabelNumber" runat="server" CssClass="m_txt" Width="143px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                批次：
            </td>
            <td nowrap="noWrap">
                <asp:DropDownList ID="t_FBatchId" runat="server">
                </asp:DropDownList>
            </td>
            <td style="text-align: center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="查询" OnClick="btnQuery_Click" />
                <input type="reset" value="重置" class="m_btn_w2 bnts_left10" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound" 
        onitemcommand="DG_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" Wrap="False" />
        <ItemStyle CssClass="m_dg1_i" Wrap="False" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="加密锁标签编号">
                <ItemTemplate>
                    <a href="javascript:showAddWindow('CardNoEdit.aspx?FID=<%#Eval("FID") %>',420,300);">
                        <%#Eval("FLockLabelNumber")%></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FLockNumber" HeaderText="加密锁硬件编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="FBatchId" HeaderText="批次"></asp:BoundColumn>
            <asp:TemplateColumn>
                <ItemStyle Width="60px" Font-Underline="False" Wrap="False" />
                <ItemTemplate>
                    <asp:Button ID="btnSelect" CommandName="cnSelect" CommandArgument='<%#Eval("FID") %>'
                        runat="server" Text="选取" CssClass="m_btn_w2" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="98%">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
