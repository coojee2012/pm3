<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectList.aspx.cs" Inherits="JSDW_ApplyXZYJS_ProjectList" %>

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

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
        });

        var postBackElement, prm;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);
            postBackElement = args.get_postBackElement();

            $get('UpdateProgress1').style.display = 'block';
        }
        function EndRequest(sender, args) {
            $get('UpdateProgress1').style.display = 'none';
        }
        function showWindow() {
            showAddWindow("ProjectAdd.aspx?n=1", 800, 500);
        }
        function EditshowWindow(xmbm)
        {
            showAddWindow("ProjectEdit.aspx?XMBM=" + xmbm, 800, 500);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <td class="t_r" width="100">项目名称：
                </td>
                <td width="200">
                    <asp:TextBox ID="txtProjectName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
                </td>
                <td width="auto">
                     <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="Submit1" type="button" value="新 建" class="m_btn_w2" onclick="showWindow()" />
            
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删 除" OnClick="btnDel_Click" OnClientClick="return confirm('确认删除');" />
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 10px">
                &nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
        <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
           <asp:TemplateColumn>
                    <ItemStyle Width="30px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" CssClass="checkboxItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XMBH" HeaderText="项目编号">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XMMC" HeaderText="项目名称">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FName" HeaderText="项目属地">
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XMDZ" HeaderText="项目地址">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JSDW" HeaderText="建设单位">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="上报时间" HeaderStyle-Width="80" DataField="LXSJ" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XMBM" Visible="False"></asp:BoundColumn>
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

