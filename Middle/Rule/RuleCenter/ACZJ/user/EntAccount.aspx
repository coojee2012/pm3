<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntAccount.aspx.cs" Inherits="GFEnt_EntAccount" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc2" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <base target="_self"></base>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            DynamicGrid(".m_dg1_i");
            $("option[value^=-]").css("color", "#3DACE1");

            initUpdateProgress();
            prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
        });
        function addRight(FUserID, FID) {
            if (FUserID == null || FUserID == "" || FUserID == undefined) {
                alert("请先保存" + FUserID);
                return;
            }
            showAddWindow("EntUserRightAdd.aspx?FUserId=" + FUserID + "&FID=" + FID, 500, 500);
        }

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
        function refresh(url) {
            showAddWindow(url, 800, 600);
            document.getElementById("btnQuery").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">施工-安全自检用户管理（企业）
                </th>
            </tr>
            <tr>
                <td class="t_r">企业名称：
                </td>
                <td>
                    <asp:TextBox ID="t_FEntName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
                </td>
                <td class="t_r">用户名：
                </td>
                <td>
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="130px"></asp:TextBox>
                </td>
                <td rowspan="2" align="center">
                    <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                    <input id="Button3" type="reset" value="重置" class="m_btn_w2 bnts_left10" />
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m">
                    <input id="Submit1" type="button" value="新增" onclick="refresh('AddEntUser.aspx?type=1');"
                        class="m_btn_w2" />
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnQuery_Click" />
                    <asp:Button ID="btnDel" Enabled="true" runat="server" CssClass="m_btn_w2" OnClick="btnDel_Click"
                        Text="删除" />
                </td>
                <td class="m_bar_m" style="text-align: right; padding-right: 10px">&nbsp;
                </td>
                <td class="m_bar_r"></td>
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
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FName" HeaderText="用户名">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FCompany" HeaderText="企业名称">
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="相关权限" ItemStyle-HorizontalAlign="Left">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FLinkMan" HeaderText="联系人" HeaderStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FTel" HeaderText="联系电话">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="系统权限">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
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
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        </asp:UpdateProgress>
    </form>
</body>
</html>
