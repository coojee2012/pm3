<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowAppList.aspx.cs" Inherits="Government_AppQualiInfo_ShowAppList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>各级审核意见</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/Govdept.js" language="javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th>
                各级审核意见
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r">
                企业名称
            </td>
            <td>
                <asp:Literal ID="liter_FBaseName" runat="server"></asp:Literal>
            </td>
            <td class="t_r">
                上报日期
            </td>
            <td>
                <asp:Literal ID="liter_FReportDate" runat="server"></asp:Literal>
            </td>
            <td class="t_r">
                业务类型
            </td>
            <td>
                <asp:Literal ID="liter_FManageType" runat="server"></asp:Literal>
            </td>
            <td class="t_r">
                上报年度
            </td>
            <td>
                <asp:Literal ID="liter_FYear" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                申报内容
            </td>
            <td colspan="7">
                <asp:Literal ID="liter_FReportInfo" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <asp:Repeater ID="AppInfo_List" runat="server" OnItemDataBound="AppInfo_List_ItemDataBound">
        <HeaderTemplate>
            <table align="center" width="98%" class="m_table" style="border: none 0px; margin-top: 5px;">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="t_r">
                    <%#Eval("FStepName") %>人：
                </td>
                <td>
                    <asp:Label ID="t_FAppPerson" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FAppPerson")%>'></asp:Label>
                </td>
                <td class="t_r">
                    <asp:Literal ID="lAppTime" runat="server"></asp:Literal>时间：
                </td>
                <td>
                    <asp:Label ID="t_FAppTime" runat="server" Text='<%# rc.StrToDate(DataBinder.Eval(Container, "DataItem.FAppTime").ToString())%>'></asp:Label>
                </td>
                <td class="t_r">
                    <asp:Literal ID="lAppResult" runat="server"></asp:Literal>结果：
                </td>
                <td>
                    <asp:Label ID="t_FResult" runat="server" Text='<%# GetResult(DataBinder.Eval(Container, "DataItem.FResult").ToString())%>'></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="t_r">
                    <asp:Literal ID="lAppIdea" runat="server"></asp:Literal>意见：
                </td>
                <td colspan="5">
                    <asp:Label ID="t_FIdea" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FIdea")%>'></asp:Label>
                </td>
              
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
  
    <input id="hidden_Sql" type="hidden" runat="server" />
    </form>
</body>
</html>
