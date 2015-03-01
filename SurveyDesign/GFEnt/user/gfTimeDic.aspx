<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gfTimeDic.aspx.cs" Inherits="GFEnt_user_gfTimeDic" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc2" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <base target="_self"></base>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">工法申报时间设置
                </th>
            </tr>
            <tr>
                <td class="t_r">工法年度：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FYear" Width="350px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                    <input id="Button3" type="reset" value="重置" class="m_btn_w2 bnts_left10" />
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m">
                    <input id="Submit1" type="button" value="新增" onclick="showAddWindow('timeEdit.aspx?type=1',600,385);"
                        class="m_btn_w2" /><asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
                    <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" OnClick="btnDel_Click"
                        Text="删除" />

                </td>
                <td class="m_bar_m" style="text-align: right; padding-right: 10px">&nbsp;
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
            AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemStyle Width="30px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="50px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FYear" HeaderText="工法申报年度" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FStime" HeaderText="开始时间" HeaderStyle-Width="100px" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FEtime" HeaderText="结束时间" HeaderStyle-Width="100px" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="已上报" DataField="cou" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="编辑" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
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
