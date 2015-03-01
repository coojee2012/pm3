<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chooseExpert.aspx.cs" Inherits="Government_AppMain_chooseExpert" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function getValue() {
            var obj = window.dialogArguments;
            document.getElementById("t_appid").value = obj.id;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" class="m_title" style="width: 100%;">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">专家选择</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r t_bg">姓名：
                </td>
                <td>
                    <asp:TextBox ID="t_ExpertName" runat="server" Width="150px" CssClass="m_txt"></asp:TextBox>

                </td>
                <td class="t_r t_bg">专业组别：
                </td>
                <td>
                    <asp:DropDownList ID="t_Industry" runat="server" CssClass="m_txt" Width="100px">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="房建">房建</asp:ListItem>
                        <asp:ListItem Value="安装">安装</asp:ListItem>
                        <asp:ListItem Value="土木">土木</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; padding-right: 10px;">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                        Text="查询" />
                    &nbsp;
                <input id="btnClear" class="m_btn_w2" style="margin-top: 3px;" type="button" value="重置"
                    onclick="clearPage();" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%" OnItemCreated="JustAppInfo_List_ItemCreated">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemStyle Width="20px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" runat="server" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="姓名" DataField="ExpertName" HeaderStyle-Width="100px">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ExpertName" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="性别" DataField="Sex" HeaderStyle-Width="40px">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="工作单位" DataField="UnitName" HeaderStyle-Width="200px">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="专业组别" DataField="Industry" HeaderStyle-Width="80px">
                    <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="从事专业" HeaderStyle-Width="80px" DataField="FirstProfessial">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="技术职称" HeaderStyle-Width="100px" DataField="WorkCerName" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="职务" DataField="WorkName" HeaderStyle-Width="60px">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="办公电话" HeaderStyle-Width="80px" DataField="UnitTelephone">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="手机" HeaderStyle-Width="80px" DataField="Phone">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="电子邮箱" HeaderStyle-Width="90px" DataField="Email">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PsID" Visible="False"></asp:BoundColumn>
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
        <div class="d div1 tcen" style="width: 98%; margin: 0px auto;">
            <uc1:pager ID="Pager1" runat="server"></uc1:pager>
        </div>
        <table align="center" class="m_title" style="width: 100%;">
            <tr>
                <td style="width: 100%; text-align: center;">
                    <asp:TextBox ID="tbName" runat="server" Width="90%" Height="100px" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; text-align: center;">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
            </tr>
        </table>
        <input id="t_appid" runat="server" type="hidden" value="0" />
        <input id="t_psid" runat="server" type="hidden" value="0" />
    </form>
</body>
</html>
<script language="javascript">
    if (document.getElementById("t_appid").value == "0") {
        getValue();
    }
</script>
