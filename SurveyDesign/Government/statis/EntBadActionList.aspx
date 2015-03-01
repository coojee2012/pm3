<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntBadActionList.aspx.cs"
    Inherits="Government_statis_EntBadActionList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        
        
       
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业市场不良行为
            </th>
        </tr>
        <tr>
            <td class="t_r">
                处罚日期：
            </td>
            <td>
                <asp:TextBox ID="time" runat="server" CssClass="m_txt" Width="130px" onfocus="WdatePicker()"></asp:TextBox>
            </td>
            <td class="t_r">
                企业类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server">
                    <asp:ListItem Value="">---请选择---</asp:ListItem>
                    <asp:ListItem Value="155">勘察设计企业</asp:ListItem>
                    <asp:ListItem Value="140">入川勘察设计企业</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td rowspan="2" align="center">
                <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button3" type="reset" value="重置" class="m_btn_w2 bnts_left10" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                地区：
            </td>
            <td>
                <uc2:govdeptid ID="govd_FRegistDeptId" runat="server" />
            </td>
            <td class="t_r">
                时间区间：
            </td>
            <td>
                <asp:TextBox ID="time1" CssClass="m_txt" runat="server" onfocus="WdatePicker()" Width="65px"></asp:TextBox>至
                <asp:TextBox ID="time2" CssClass="m_txt" runat="server" onfocus="WdatePicker()" Width="65px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_l">
                <tt>
                    <asp:Literal ID="li_MSG" runat="server"></asp:Literal>
                </tt>
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="50px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="QYMC" HeaderText="企业名称">
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ZRZTLB" HeaderText="企业类型">
                <ItemStyle CssClass="t_c" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="RDDW" HeaderText="认定单位">
                <ItemStyle HorizontalAlign="Left" />
                <ItemStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FS" HeaderText="扣分">
                <ItemStyle CssClass="t_c" Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="处罚日期" DataField="CFRQ" DataFormatString="{0:yyyy-MM-dd}">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
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
