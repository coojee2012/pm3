<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBad.aspx.cs" Inherits="BadBehavior_main_AddBad" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>举报</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function checkInfo() {

            return AutoCheckInfo();

        }
        
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                市场不良行为举报处理
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" CommandArgument="6"
                    OnClientClick="return checkInfo();" OnCommand="btnSave_Click" />
                <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                举报对象：
            </td>
            <td>
                <asp:TextBox ID="t_FSubject" runat="server" CssClass="m_txt" Width="285px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                举报内容：
            </td>
            <td>
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Width="287px" MaxLength="30"
                    Height="91px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                处理结果：
            </td>
            <td>
                <asp:TextBox ID="t_FResult" runat="server" CssClass="m_txt" Width="287px" MaxLength="30"
                    Height="43px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                举报时间：
            </td>
            <td>
                <asp:TextBox ID="t_FReportTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt"
                    MaxLength="18" Width="200px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                状态：
            </td>
            <td>
                <asp:DropDownList ID="t_FState" runat="server">
                    <asp:ListItem Value="1" Text="未处理"></asp:ListItem>
                    <asp:ListItem Value="6" Text="已处理"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                添加处罚记录
            </td>
            <td class="t_l t_r">
                <asp:Button id="btnAdd" CssClass="m_btn_w2"  Text="新增" runat=server 
                    onclick="btnAdd_Click"   /><asp:Button
                    ID="btnPub" runat="server" CssClass="m_btn_w2" Text="发布" OnClick="btnPub_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="m_btn_w2" Text="撤销" OnClick="btnCancel_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="BadAction_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="BadAction_List_ItemDataBound" Style="margin-top: 4px"
        Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn Visible="True">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="处罚单位">
                <ItemStyle CssClass="t_l" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="fsystemname" HeaderText="企业类型">
                <ItemStyle CssClass="t_l" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FBuidUnit" HeaderText="建设单位">
                <ItemStyle CssClass="t_l" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FProjectName" HeaderText="工程名称">
                <ItemStyle CssClass="t_l" Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FDTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="处罚时间">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="98%">
        <tr>
            <td style="height: 26px">
    <table align="center" width="98%">
        <tr>
            <td style="height: 26px">
                <uc1:pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>
               
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
