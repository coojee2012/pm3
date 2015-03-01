<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmsList.aspx.cs" Inherits="Admin_mainother_SmsList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>消息服务监控</title>
      <asp:Link id="skin1" runat="server">
    </asp:Link>

   
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
 

</head>
<body>
    <form id="form1" runat="server">
    
     <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                    短信
                  </th>
        </tr>         <tr>
                <td class="t_r t_bg">
                    状态：
                </td>
                <td>
                    <asp:DropDownList ID="t_FState" runat="server">
                        <asp:ListItem Text="全部" Value=""></asp:ListItem>
                        <asp:ListItem Text="未发送" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="已发送" Value="1"></asp:ListItem>
                        <asp:ListItem Text="发送失败" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    创建时间：
                </td>
                <td>
                    <asp:TextBox ID="t_FSubmitTime1" runat="server" CssClass="m_txt" Width="90px"
                        onfocus="WdatePicker()"></asp:TextBox>
                    至<asp:TextBox ID="t_FSubmitTime2" runat="server" CssClass="m_txt" Width="90px"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                    <input type="button" value="清空" class="m_btn_w2 " onclick="clearQuery();" />
                </td>
            </tr>
        </table>
        
         <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
                   <td class="t_r">
                  <input class="m_btn_w2" type="button" value="新增" onclick="showApproveWindow('SmsEdit.aspx?',600,350);" />
                    <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
                    <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>

        <asp:DataGrid ID="Dic_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
            Style="margin-top: 7px;" AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemStyle Width="20px" Font-Underline="False" Wrap="False" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" runat="server" />
                    </ItemTemplate>
                    <FooterStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FMobile" HeaderText="手机号"></asp:BoundColumn>
                <asp:BoundColumn DataField="FContent" HeaderText="内容">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FState" HeaderText="状态">
                    <ItemStyle Width="60" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FSubmitTime" HeaderText="创建时间"></asp:BoundColumn>
                <asp:BoundColumn DataField="FPlanTime" HeaderText="计划发送时间"></asp:BoundColumn>
                <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <table align="center" width="98%" height="30px;">
            <tr>
                <td>
                    <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
