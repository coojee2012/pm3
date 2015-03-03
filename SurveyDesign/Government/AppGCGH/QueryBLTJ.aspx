<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryBLTJ.aspx.cs" Inherits="Government_AppGCGH_QueryBLTJ" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
     <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        function showWindow() {
            showAddWindow("IDeaBookApply.aspx?anc=1", 800, 500);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">办理查询
                </th>
            </tr>
            <tr>
                <td class="t_r">企业名称：
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                </td>
                <td class="t_r">工程名称：
                </td>
                <td>
                   <asp:TextBox ID="txtXZMC" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                </td>
                <td rowspan="2" align="center">

                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                </td>
            </tr>
            <tr>
                <td class="t_r">所属地：
                </td>
                <td>
                    <uc2:govdeptid ID="uegovdeptid" runat="server" />
                </td>
                <td class="t_r">状态：
                </td>
                <td>
                    <asp:DropDownList ID="dbSeeState" runat="server" CssClass="m_txt" Width="100px">
                        <asp:ListItem Value="" Selected="True">请选择</asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">审核中</asp:ListItem>
                        <asp:ListItem Value="2">未通过</asp:ListItem>
                        <%--<asp:ListItem Value="2">初审未通过</asp:ListItem>--%>
                        <asp:ListItem Value="3">不准予受理</asp:ListItem>
                    </asp:DropDownList>
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
                <asp:BoundColumn DataField="FEmpName" HeaderText="用地规划名称">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FEntName" HeaderText="工程名称"></asp:BoundColumn>
                <asp:BoundColumn DataField="AuditType" HeaderText="工程类型"></asp:BoundColumn>
                <asp:BoundColumn DataField="XMSDMC" HeaderText="所属地">
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FSeeState" HeaderText="业务状态">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="上报时间" DataField="FReporttime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FMeasure" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FFResult" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FBarCode" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FBaseInfoId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FManageTypeId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="YWBM" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="typeId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FMeasure" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FLinkId" Visible="False"></asp:BoundColumn>
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


