<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecondAuditAccept.aspx.cs" Inherits="JSDW_ApplyGCGH_AuditMain_SecondAuditAccept" %>

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
        function app(url) {
            var items = $("#DG_List").find(".checkboxItem > input[type=checkbox][checked]");
            if (items.length < 1) {
                alert("当前未选择任何项");
                return false;
            } else if (items.length > 1) {
                alert("只能选择一项进行申报");
                return false;
            }
            var JG_Id = $(items[0]).parent("span").attr("JG_Id");
            var YWBM = $(items[0]).parent("span").attr("name");
            var fsubid = $(items[0]).parent("span").attr("fSubFlowId");
            var TypeId = $(items[0]).parent("span").attr("TypeId");
            showAddWindow(url + '?typeId=5&JG_Id=' + JG_Id + '&fSubFlowId=' + fsubid+'&YWBM='+YWBM, 700, 600);
            return true;
        }
        function BackApp(url) {
            var items = $("#DG_List").find(".checkboxItem > input[type=checkbox][checked]");
            if (items.length < 1) {
                alert("当前未选择任何项");
                return false;
            } else if (items.length > 1) {
                alert("只能选择一项进行打回");
                return false;
            }
            var tmpVal = $(items[0]).parent("span").attr("name");
            var fsubid = $(items[0]).parent("span").attr("fSubFlowId");
            var TypeId = $(items[0]).parent("span").attr("TypeId");
//            if (TypeId == "7080")//市政
//                url = "../../ApplyJGYSSZ/AuditMain/" + url;
            showAddWindow(url + '?typeId=5&FLinkId=' + tmpVal + '&fSubFlowId=' + fsubid, 800, 600);
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">竣工验收办理
                </th>
            </tr>
            <tr>
                <td class="t_r">工程名称：
                </td>
                <td>
                   <asp:TextBox ID="txtXZMC" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                </td>
                <td class="t_r">建设单位：
                </td>
                <td>
                    <asp:TextBox ID="txtJSDW" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
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
                        <asp:ListItem Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">待复审</asp:ListItem>
                        <asp:ListItem Value="1">复审已通过</asp:ListItem>
                        <asp:ListItem Value="2">复审未通过</asp:ListItem>
                        <%--<asp:ListItem Value="3">不准予受理</asp:ListItem>
                        <asp:ListItem Value="5">打回企业,重新上报</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                      <%--<asp:Button ID="btnAccept" runat="server" Text="接件" CssClass="m_btn_w2"  OnClientClick="return app('AcceptInfoGF.aspx')" />--%>
                <input type="button" value="审核" class="m_btn_w2" onclick="return app('AuditProjectPlan.aspx')" />
                <input type="button" value="退件" class="m_btn_w2" onclick="return BackApp('BackAccept.aspx')" />
               <%-- <asp:Button ID="btnExit" runat="server" Text="退件" CssClass="m_btn_w2" />
                <asp:Button ID="Button1" runat="server" Text="导出" CssClass="m_btn_w2" />--%>
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
                        <asp:CheckBox ID="CheckItem" CssClass="checkboxItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="50px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FEmpName" HeaderText="工程名称">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JSDW" HeaderText="建设单位"></asp:BoundColumn>
            <asp:BoundColumn DataField="AuditType" HeaderText="工程类型"></asp:BoundColumn>
                <asp:BoundColumn DataField="XMSDMC" HeaderText="所属地">
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FSeeState" HeaderText="业务状态">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="申请日期" DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FMeasure" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FFResult" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FBarCode" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FBaseInfoId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="YWBM" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FManageTypeId" Visible="False"></asp:BoundColumn>
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


