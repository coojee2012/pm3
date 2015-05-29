<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SDRYSC.aspx.cs" Inherits="Government_AppSGXKZGL_SDRYSC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>该项目参与人员已参加在建工程查询</title>
     <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <input type="hidden"  runat="server" ID="h_FAppId" value="" />
        <input type="hidden"  runat="server" ID="h_FPrjItemId" value="" />
        该项目参与人员已参加其它的在建工程情况查询
                     <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                        HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <ItemStyle CssClass="m_dg1_i" />
                        <Columns>
            <asp:BoundColumn HeaderText="姓名" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" DataField="FHumanName">
                <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" Width="100px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="人员类别" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" DataField="EmpType">
                <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" Width="100px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
             <asp:BoundColumn HeaderText="工程所属地" HeaderStyle-HorizontalAlign="Center" DataField="FAddress">
                <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
                             <asp:BoundColumn HeaderText="身份证号码" HeaderStyle-HorizontalAlign="Center" DataField="FIdCard" Visible ="false">
                <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PrjItemName" HeaderText="在建工程名称"></asp:BoundColumn>
                            <asp:BoundColumn DataField="StartDate" HeaderText="预估开工日期"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EndDate"   HeaderText="预估竣工时间"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FCreateTime" HeaderText="锁定日期"></asp:BoundColumn>
                            </Columns>
                         </asp:DataGrid>

     
    </form>
</body>
</html>
