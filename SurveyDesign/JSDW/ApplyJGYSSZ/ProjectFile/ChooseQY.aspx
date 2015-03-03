<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChooseQY.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_ChooseQY" %>

<%@ Register Src="../../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../script/default.js"> </script>


    <script type="text/javascript">
        function Verify(QYBM, QYMC, JGDM, QYZSID) {
            var str = QYBM + "|" + QYMC + "|" + JGDM + "|" + QYZSID;
            window.close();
            window.returnValue = str;
        }
    </script>

</head>
<body style="width:1500px;">
    <form id="form1" runat="server">
        <table width="100%" align="center" class="m_title">
            <tr>
                <td width="100">企业名称：</td>
                <td width="150"><asp:TextBox ID="txtQYName" runat="server"></asp:TextBox></td>
                <td width="auto"><asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="m_btn_w2" OnClick="btnQuery_Click"  /></td>
            </tr>
        </table>
    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" CssClass="m_dg1"
        AutoGenerateColumns="False" onitemdatabound="DG_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
               <asp:BoundColumn HeaderText="序号">
            <ItemStyle Width="50px" />
            </asp:BoundColumn>
                <asp:TemplateColumn HeaderStyle-Width="80">
                    <ItemTemplate>
                        <a href="javascript:void(0)" onclick="Verify('<%#Eval("QYBM") %>','<%#Eval("QYMC") %>','<%#Eval("JGDM") %>','<%#Eval("QYZSID") %>')">选 定</a>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="SD" HeaderText="属地" HeaderStyle-Width="150"></asp:BoundColumn>
                <asp:BoundColumn DataField="QYMC" HeaderText="企业名称" HeaderStyle-Width="200">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ZSBH" HeaderText="资质证书编号" HeaderStyle-Width="150"></asp:BoundColumn>
                <asp:BoundColumn DataField="ZZMC" HeaderText="资质项" HeaderStyle-Width="200">
                    <ItemStyle CssClass="t_l" Wrap="true" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FRDB" HeaderText="法人" HeaderStyle-Width="100"></asp:BoundColumn>
                <asp:BoundColumn DataField="LXR" HeaderText="联系人" HeaderStyle-Width="100"></asp:BoundColumn>
                <asp:BoundColumn DataField="LXDH" HeaderText="联系电话" HeaderStyle-Width="100"></asp:BoundColumn>
                <asp:BoundColumn DataField="JGDM" HeaderText="机构代码" HeaderStyle-Width="100"></asp:BoundColumn>
                <asp:BoundColumn DataField="QYXXDZ" HeaderText="单位地址" HeaderStyle-Width="200"></asp:BoundColumn>
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

