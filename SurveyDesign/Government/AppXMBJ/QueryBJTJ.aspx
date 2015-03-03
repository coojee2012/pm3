<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryBJTJ.aspx.cs" Inherits="Government_AppXMBJ_QueryBJTJ" %>
<!DOCTYPE html>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DataGrid ID="dgList" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Width="98%" 
           onitemdatabound="dgList_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="地区" DataField="FName">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="总用地面积" DataField="YDMJ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="总建筑面积" DataField="JZMJ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="政府投资" DataField="ZFTZ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="总投资" DataField="ZTZ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="自筹投资" DataField="ZCTZ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="贷款投资" DataField="DKTZ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="外商投资" DataField="WSTZ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="其他投资" DataField="QTTZ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FNumber" Visible="False"></asp:BoundColumn>
            </Columns>
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
    </form>
</body>
</html>


