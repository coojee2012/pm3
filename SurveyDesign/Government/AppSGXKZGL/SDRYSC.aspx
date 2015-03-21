<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SDRYSC.aspx.cs" Inherits="Government_AppSGXKZGL_SDRYSC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>锁定人员核查</title>
     <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <input type="hidden"  runat="server" ID="h_FAppId" value="" />
        <input type="hidden"  runat="server" ID="h_FPrjItemId" value="" />
                     <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                        HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <ItemStyle CssClass="m_dg1_i" />
                        <Columns>
                             <asp:BoundColumn HeaderText="姓名" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" DataField="FHumanName">
                <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" Width="100px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
                             <asp:BoundColumn HeaderText="锁定次数" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" DataField="FCount">
                <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" Width="100px" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="锁定地区" HeaderStyle-HorizontalAlign="Center" DataField="FAddress">
                <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
                            </Columns>
                         </asp:DataGrid>

     
    </form>
</body>
</html>
