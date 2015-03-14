<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JqsbxxSel.aspx.cs" Inherits="JSDW_project_JqsbxxSel" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机械设备信息列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
        function Verify(Fid) {
            window.close();
            window.returnValue = Fid;
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="lTitle" runat="server" Text="" ></asp:Label>机械设备信息列表
            </th>
        </tr>
        <tr>
            <td colspan="1" class="t_r">
                设备名称
            </td>
            <td class="t_l">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
            </td>
            <td colspan="1" class="t_r">
                企业名称
            </td>
            <td class="t_l" colspan="2">
                <asp:TextBox ID="t_QYMC" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />
                <input type="button" id="Button1" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
        </tr>
    </table>
        
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" onitemdatabound="DG_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="50px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SBMC" HeaderText="设备名称">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SBBABH" HeaderText="备案编号">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GGXH" HeaderText="设备型号"></asp:BoundColumn>
                <asp:BoundColumn DataField="CCBH" HeaderText="出厂编号">
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderStyle-Width="100">
                    <ItemTemplate>
                        <a href="javascript:void(0)" onclick="Verify('<%#Eval("SBID") %>')">选择</a>
                    </ItemTemplate>
                </asp:TemplateColumn>
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
