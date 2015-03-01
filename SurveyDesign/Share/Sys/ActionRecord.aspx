<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActionRecord.aspx.cs" Inherits="Share_Sys_ActionRecord" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>日志详细信息</title>
<asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css"></asp:Link>
    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
    <base  target="_self"/>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title" id="QueryTable1">
        <tr>
            <th>
                事件查看器
            </th>
        </tr>
    </table>    
    <table class="m_table" width="98%" align="center">
                    <tr>
                        <td align="center">
                            登陆时间：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_BeginTime" TabIndex=3 runat="server" CssClass="m_txt" Width="100px" onfocus="WdatePicker()" MaxLength="15"></asp:TextBox></td>
                        <td align="center">
                            退出时间：</td>
                        <td>
                            <asp:TextBox ID="txt_EndTime" runat="server" TabIndex=0 CssClass="m_txt" Width="100px"  onfocus="WdatePicker()" MaxLength="15"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="Button2" CssClass="m_btn_w2" runat="server" Text="搜索" 
                                onclick="Button2_Click" />
                <input id="Button1" class="m_btn_w2" onclick="javascript:window.close();" type="button"
                    value="返回" style=""  /></td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
        <td class="td2">
        <asp:DataGrid ID="LogInfo_List" runat="server" HorizontalAlign="Center" Width="98%"
            CssClass="m_dg1" Style="margin-top: 7px;" AutoGenerateColumns="False" OnItemDataBound="LogInfo_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                <ItemStyle Width="20" />
                    <HeaderTemplate>
                        序号</HeaderTemplate>
                    <ItemTemplate>
                        <%# Container.ItemIndex+1%>
                    </ItemTemplate>
                    <HeaderStyle Width="40px"></HeaderStyle>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="FLogType" HeaderText="类别">
                    <HeaderStyle Width="40px"></HeaderStyle>
                </asp:BoundColumn>                
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        信息</HeaderTemplate>
                    <ItemTemplate>
                        <a href="#" onclick='<%#getUrl(Eval("FID")) %>'>
                            <%# Eval("errmsg")%></a></ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateColumn>            
                <asp:BoundColumn DataField="Title" HeaderText="时间" HeaderStyle-Width="150px">
                    <HeaderStyle Width="50px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <uc1:pager ID="Pager1" runat="server" />
        </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>
