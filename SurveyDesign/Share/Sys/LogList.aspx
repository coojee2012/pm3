<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogList.aspx.cs" Inherits="Admin_MainOther_LogList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title>日志管理页</title>

    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>
    <style type="text/css">
        #UserLockInfo_List td a
        {
            text-decoration: none;
        }
        #UserLockInfo_List td a:hover
        {
            color: Red;
            text-decoration: none;
        }
        .ff
        {
            font-weight: bold;
        }
        .PageTop01 .td6
        {
            background: url(../../Admin/images/leftImg01.gif) no-repeat center;
        }
    </style>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script language="javascript">

        function Clear() {
            document.getElementById("txt_BeginTime").value = "";
            document.getElementById("txt_EndTime").value = "";
            document.getElementById("txt_Title").value = "";
            document.getElementById("txt_Content").value = "";
            document.getElementById("txt_Server").value = "";
            document.getElementById("txt_IP").value = "";
            document.getElementById("txt_Operator").value = "";
            document.getElementById("txt_FMac").value = "";
            document.getElementById("dpl_LogType").options[0].selected = true;
            document.getElementById("dpl_FOperation").options[0].selected = true;
        }

        function IsIp(ip) {
            var a = /^(\d{1,3}|\*)\.(\d{1,3}|\*)\.(\d{1,3}|\*)\.(\d{1,3}|\*)$/;
            if (ip.value != "") {
                if (!(a.test(ip.value))) {

                    alert("请输入正确的IP");
                    ip.value = "";
                }
            }

        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title" id="QueryTable1">
        <tr>
            <th colspan="8">
                事件查看器
            </th>
        </tr>
        <tr>
            <td align="right">
                服务器：&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txt_Server" runat="server" CssClass="m_txt" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
            <td align="right">
                &nbsp;内容：
            </td>
            <td>
                <asp:TextBox ID="txt_Content" runat="server" CssClass="m_txt" Width="115px" MaxLength="100"></asp:TextBox>
            </td>
            <td align="right">
                类别：
            </td>
            <td class="tdRight">
                <asp:DropDownList ID="dpl_LogType" runat="server" Height="20px" Width="104px">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">信息</asp:ListItem>
                    <asp:ListItem Value="2">错误</asp:ListItem>
                    <asp:ListItem Value="3">警告</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right">
                操作者：
            </td>
            <td class="tdRight">
                <asp:TextBox ID="txt_Operator" runat="server" CssClass="m_txt" Width="70px" MaxLength="36"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                标题：
            </td>
            <td>
                <asp:TextBox ID="txt_Title" runat="server" CssClass="m_txt" Width="150px" MaxLength="100"></asp:TextBox>
            </td>
            <td align="right">
                IP：
            </td>
            <td>
                <asp:TextBox ID="txt_IP" runat="server" CssClass="m_txt" Width="115px" MaxLength="15"></asp:TextBox>
            </td>
            <td align="right">
                系统：
            </td>
            <td class="tdRight" colspan="3">
                <asp:DropDownList ID="ddlFSystemType" runat="server">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                时间：
            </td>
            <td>
                <asp:TextBox ID="txt_BeginTime" runat="server" CssClass="m_txt" Width="70px" onfocus="WdatePicker()"
                    MaxLength="15"></asp:TextBox>
                至<asp:TextBox ID="txt_EndTime" runat="server" CssClass="m_txt" Width="70px" onfocus="WdatePicker()"
                    MaxLength="15"></asp:TextBox>
            </td>
            <td align="right">
                Mac：
            </td>
            <td>
                <asp:TextBox ID="txt_FMac" runat="server" CssClass="m_txt" Width="115px" MaxLength="50"></asp:TextBox>
            </td>
            <td align="right">
                类型：
            </td>
            <td class="tdRight">
                <asp:DropDownList ID="dpl_FOperation" runat="server" Height="20px" Width="104px">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">系统</asp:ListItem>
                    <asp:ListItem Value="2">安全</asp:ListItem>
                    <asp:ListItem Value="3">应用</asp:ListItem>
                    <asp:ListItem Value="4">操作</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdRight" align="center">
                &nbsp;
                <input id="Button2" type="button" value="清空" class="m_btn_w2" onclick="Clear();" />
            </td>
            <td class="tdRight" align="center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
            </td>
        </tr>
    </table>
    <table align="center" width="98%" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                查询列表
            </td>
            <td align="right" class="m_bar_m t_r">
                &nbsp;<asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="LogInfo_List" runat="server" HorizontalAlign="Center" Width="98%"
        CssClass="m_dg1" Style="margin-top: 7px;" AutoGenerateColumns="False" OnItemDataBound="LogInfo_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="30" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
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
            <asp:BoundColumn DataField="FOperation" HeaderText="类型">
                <HeaderStyle Width="40px"></HeaderStyle>
            </asp:BoundColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    标题</HeaderTemplate>
                <ItemTemplate>
                    <a href="#" onclick='<%#getUrl(Eval("FID")) %>'>
                        <%# Eval("FTitle")%></a></ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="操作者" HeaderStyle-Width="70px"></asp:BoundColumn>
            <asp:BoundColumn DataField="FServerName" HeaderText="服务器"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="IP/MAC">
                <ItemStyle Width="100px" />
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "FIPAddress")%><br />
                    <%#DataBinder.Eval( Container.DataItem,"FMac") %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FLogTime" HeaderText="时间" HeaderStyle-Width="150px">
                <HeaderStyle Width="110px"></HeaderStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FOperator" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table align="center" width="95%">
        <tr>
            <td height="40px;">
                <uc1:pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>
    <div style="margin-left: 20px; width: 150px;">
        执行时间:<%
                 TimeSpan kk = new TimeSpan(System.DateTime.Now.Ticks - Context.Timestamp.Ticks);
                 Response.Write(kk.TotalSeconds);
        %></div>
    </form>
</body>
</html>
