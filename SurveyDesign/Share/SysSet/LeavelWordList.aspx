<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeavelWordList.aspx.cs" Inherits="Admin_main_LeavelWordList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>沟通平台管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                沟通平台管理
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_table">
        <tr>
            <td class="tdRight" width="50">
                搜索：
            </td>
            <td align="left" width="150">
                <asp:TextBox ID="txtQueryText" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td nowrap="noWrap" width="300">
                &nbsp;&nbsp;
                <asp:RadioButton ID="rb1" runat="server" GroupName="rbQuery" Text="留言人" Checked="True" />
                <asp:RadioButton ID="rb2" runat="server" GroupName="rbQuery" Text="留言主题" />
                <asp:RadioButton ID="rb3" runat="server" GroupName="rbQuery" Text="回复人" />
            </td>
            <td class="tdRight" nowrap="nowrap" width="50">
                状态：
            </td>
            <td nowrap="nowrap">
                <asp:DropDownList ID="dbState" runat="server" CssClass="m_txt">
                    <asp:ListItem Text="全部" Value="">全部</asp:ListItem>
                    <asp:ListItem Text="未回复" Value="0">未回复</asp:ListItem>
                    <asp:ListItem Text="已回复" Value="1">已回复</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdRight" nowrap="nowrap" width="100">
                所属系统：
            </td>
            <td nowrap="nowrap">
                <asp:DropDownList ID="dbFSystemId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td align="right" colspan="3">
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" class="m_btn_w2" onclick="clearQuery();"
                    style="margin-left: 3px" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click1" />
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="LeavelWord_List" runat="server" HorizontalAlign="Center" Width="98%"
        CssClass="m_dg1" Style="margin-top: 7px; word-break: normal; word-wrap: normal;"
        AutoGenerateColumns="False" OnItemDataBound="LeavelWord_List_ItemDataBound" OnItemCommand="LeavelWord_List_ItemCommand"
        OnSelectedIndexChanged="LeavelWord_List_SelectedIndexChanged">
        <HeaderStyle CssClass="m_dg1_h" Wrap="False" />
        <ItemStyle CssClass="m_dg1_i" Wrap="False" /> 
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
                <FooterStyle />
                <HeaderStyle />
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" />
                <HeaderStyle />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FState" HeaderText="状态">
                <ItemStyle Wrap='false' />
                <HeaderStyle />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FLevelPerson" HeaderText="留言人">
                <ItemStyle HorizontalAlign="Left"/>
                <HeaderStyle />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FTitle" HeaderText="留言主题">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FCreateTime" HeaderText="留言时间" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle  Wrap="false"/>
                <HeaderStyle />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FRevertPerson" HeaderText="回复人">
                <ItemStyle />
                <HeaderStyle Wrap="false" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FRevertDate" HeaderText="回复时间" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle  Wrap="false"/>
                <HeaderStyle />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="显示顺序">
                <ItemStyle CssClass="cNowarp" />
                <ItemTemplate>
                    <asp:TextBox ID="tFLockNumber" Text='<%# DataBinder.Eval(Container.DataItem,"Forder") %>'
                        CssClass='m_txt' runat="server" Width="40px" onblur="isInt(this);"></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle />
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="是否发布">
                <ItemStyle CssClass="cNowarp" />
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsPub" runat="server" />
                </ItemTemplate>
                <HeaderStyle />
            </asp:TemplateColumn>
            <asp:ButtonColumn Text="保存" CommandName="Save"></asp:ButtonColumn>
            <asp:BoundColumn DataField="FISpub" Visible="false"></asp:BoundColumn>
            <asp:BoundColumn DataField="FID" Visible="false"></asp:BoundColumn>
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
