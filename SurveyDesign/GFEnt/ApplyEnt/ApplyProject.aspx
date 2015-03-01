<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyProject.aspx.cs" Inherits="GFEnt_ApplyEnt_ApplyProject" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function getFilUp(url) {
            var fid = document.getElementById("t_FID").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?FAppId=" + fid + "&type=1002", 550, 400);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">工法应用工程
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r" style="padding-right: 10px;">工程应用证明相关附件           
                    <input type="button" id="btnUP" runat="server" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx");' />
                    &nbsp;<input id="Submit1" type="button" value="新增" onclick="showAddWindow('addProject.aspx?', 550, 450);"
                        runat="server" class="m_btn_w2" />&nbsp;<asp:Button ID="btnDel" runat="server" Text="删除" OnClick="btnDel_Click" class="m_btn_w2" />&nbsp;
                    <asp:Button ID="btnQuery" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
            <tr>
                <td style="height: 4px;">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="color: red; font-family: 'Adobe Caslon Pro'">工法应用工程少于3项时应在表单“工法成熟可靠性说明”中进行阐述，同时可在此处上传相关附件。</td>
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
                    <ItemStyle Width="30px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GCMC" HeaderText="工程名称" HeaderStyle-Width="200px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SZD" HeaderText="工程所在地区" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="KSSJ" HeaderText="开工时间" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JSSJ" HeaderText="竣工时间" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GCID" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <table align="center" width="98%">
            <tr>
                <td>
                    <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                </td>
            </tr>
        </table>
        <input id="t_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
