<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogRecord.aspx.cs" Inherits="Admin_main_LogRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>操作日志查询</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });       
    </script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script language="javascript">
    function clearQuery()
    {
        document.getElementById("txtTitle").value="";
        document.getElementById("txtOverTitle").value="";
        document.getElementById("txtUser").value="";
        document.getElementById("txtDb").value="";  
        document.getElementById("txtTable").value="";  
        document.getElementById("ddlAction").value="all"; 
        document.getElementById("ddlKey").value="all";
        document.getElementById("txtWordKeys").value="";
    } 
    
    function showInfo(sUrl)
    {
        window.showModalDialog(sUrl,'','dialogWidth:700px;dialogHeight:750px;status:no;help:no;resizable:yes');
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                操作日志记录管理
            </th>
        </tr>
        <tr>
            <td class="t_r">
                起始时间：
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="m_txt" Width="76px" onfocus="WdatePicker()"></asp:TextBox>
            </td>
            <td class="t_r">
                截止时间：
            </td>
            <td>
                <asp:TextBox ID="txtOverTitle" runat="server" CssClass="m_txt" Width="148px" onfocus="WdatePicker()"></asp:TextBox>
            </td>
            <td class="t_r">
                操作人：
            </td>
            <td>
                <asp:TextBox ID="txtUser" runat="server" CssClass="m_txt" Width="84px"></asp:TextBox>
            </td>
            <td class="t_r">
                操作：
            </td>
            <td>
                <asp:DropDownList ID="ddlAction" runat="server" Width="64px" CssClass="m_txt">
                    <asp:ListItem Value="all">全部</asp:ListItem>
                    <asp:ListItem Value="insert">添加数据</asp:ListItem>
                    <asp:ListItem Value="update">修改数据</asp:ListItem>
                    <asp:ListItem Value="select">查询数据</asp:ListItem>
                    <asp:ListItem Value="delete">删除数据</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                数据表名称：
            </td>
            <td>
                <asp:TextBox ID="txtTable" runat="server" CssClass="m_txt" Width="75px"></asp:TextBox>
            </td>
            <%--<td style="height: 30px; width: 85px;" class="tdRight">
                                &nbsp;数据库名称：</td>
                            <td class="tdLeft" nowrap="noWrap" style="height: 30px; width: 80px;">
                                <asp:TextBox ID="txtDb" runat="server" CssClass="cTextBox1" Width="69px"></asp:TextBox></td>--%>
            <td>
                <asp:DropDownList ID="ddlKey" runat="server" CssClass="m_txt" Width="109px">
                    <asp:ListItem Value="all">-日志类型-</asp:ListItem>
                    <asp:ListItem Value="1">操作错误日志</asp:ListItem>
                    <asp:ListItem Value="3">一般操作日志</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:TextBox ID="txtWordKeys" runat="server" CssClass="m_txt" Width="86px"></asp:TextBox>
                <asp:CheckBox ID="ckIsNull" runat="server" Text="可否为空" Checked />
            </td>
            <td colspan="2">
                <asp:DropDownList ID="dbType" runat="server">
                    <asp:ListItem Value="dbCenter">dbCenter</asp:ListItem>
                    <asp:ListItem Value="dbQuali">dbQuali</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="center" colspan="2">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_s" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" class="m_btn_s" onclick="clearQuery();" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Log_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="AppQuali_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <%-- <AlternatingItemStyle CssClass="cGridAlterItem1" />--%>
        <Columns>
            <asp:TemplateColumn Visible="false">
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="50px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="Title" HeaderText="记录时间"></asp:BoundColumn>
            <asp:BoundColumn DataField="Content" HeaderText="记录内容"></asp:BoundColumn>
            <asp:BoundColumn DataField="errmsg" HeaderText="错误信息"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="操作人员"></asp:BoundColumn>
            <asp:BoundColumn HeaderText="数据库名称"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" style="margin-top: 5px; width: 98%;">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
