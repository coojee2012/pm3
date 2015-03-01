<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManUserList.aspx.cs" Inherits="Share_User_ManUserList"
    EnableEventValidation="false" %>

<%@ Register Src="../../Common/Govdeptid4.ascx" TagName="Govdept" TagPrefix="uc2" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript">
        $(document).ready(function() {
            initUpdateProgress();
            prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
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
    </script>

    <style type="text/css">
        .style1 { text-align: right; height: 35px; }
        .style2 { height: 35px; }
        .style3 { text-align: center; width: 200px; height: 35px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                统一用户管理（主管部门）
            </th>
        </tr>
        <tr>
            <td class="style1">
                用户名：
            </td>
            <td class="style2">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="style1">
                行政区划：
            </td>
            <td class="style2">
                <uc2:Govdept ID="Govdept1" runat="server" />
            </td>
            <td class="style3" rowspan="2">
                <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button3" type="reset" value="重置" class="m_btn_w2 bnts_left20" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                姓名：
            </td>
            <td>
                <asp:TextBox ID="t_FEntName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
            </td>
            <td class="t_r">
                加密锁硬件编号：
            </td>
            <td>
                <asp:TextBox ID="t_FLockNumber" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="Submit1" type="button" value="新增" onclick="showAddWindow('ManUserAdd.aspx?',700,500);"
                    class="m_btn_w2" runat="server"/>
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" OnClick="btnDel_Click"
                    Text="删除" />
                <asp:Button ID="btnDownload" runat="server" CssClass="m_btn_w4" Text="下载最新" OnClick="btnSelectEnt_Click" Visible="false" />
                
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
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="50px" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="用户名">
                <ItemStyle CssClass="t_l" />
                <ItemTemplate>
                    <a href="javascript:showAddWindow('ManUserAdd.aspx?FID=<%#Eval("FID") %>',700,500);">
                        <%#Eval("FName")%>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FLinkMan" HeaderText="姓名"></asp:BoundColumn>
            <asp:BoundColumn DataField="flocknumber" HeaderText="加密锁硬件编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="FCompany" HeaderText="行政区划"></asp:BoundColumn>
            <asp:BoundColumn DataField="FDepartmentId" HeaderText="部门"></asp:BoundColumn>
            <asp:BoundColumn DataField="FTel" HeaderText="联系电话">
                <ItemStyle CssClass="t_c" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="" HeaderText="系统权限" Visible="false">
                <ItemStyle CssClass="t_l" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
             
        </Columns>
    </asp:DataGrid>
    <table align="center" width="98%">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnDownload" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    </asp:UpdateProgress>
    </form>
</body>
</html>
