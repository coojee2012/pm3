<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChooseProject.aspx.cs" Inherits="JSDW_ApplyXZYJS_ChooseProject" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
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
        function Verify(Fid) {
            window.close();
            window.returnValue = Fid;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <td width="100">项目名称：</td>
                <td width="150"><asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox></td>
                <td width="100">建设单位：</td>
                <td width="150"><asp:TextBox ID="txtUnit" runat="server"></asp:TextBox></td>
                <td width="100">工程类别：</td>
                <td width="150">
                    <asp:DropDownList ID="ddlGCLB" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="auto"><asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="m_btn_w2" OnClick="btnQuery_Click"  /></td>
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
                <asp:BoundColumn DataField="XMMC" HeaderText="项目名称">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JSDW" HeaderText="建设单位">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JSDWDZ" HeaderText="建设地址"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="工程类别" HeaderStyle-Width="100"></asp:BoundColumn>
                <asp:BoundColumn DataField="JSGM" HeaderText="建设规模" HeaderStyle-Width="100">
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderStyle-Width="100">
                    <ItemTemplate>
                        <a href="javascript:void(0)" onclick="Verify('<%#Eval("XMBH") %>')">选 定</a>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="XMLX" Visible="false"></asp:BoundColumn>
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
