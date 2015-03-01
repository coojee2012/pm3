<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selfAssessmentList.aspx.cs" Inherits="ACZJ_main_selfAssessmentList" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">安全自评列表
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="t_r t_bg">工程名称：
                </td>
                <td>
                    <asp:TextBox ID="t_GCMC" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r t_bg">建设单位：
                </td>
                <td>
                    <asp:TextBox ID="t_JSDW" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r" style="padding-right: 10px;" colspan="4">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" class="m_btn_w2" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
            <tr>
                <td class="t_r t_bg">工程所属地：
                </td>
                <td>
                    <uc1:govdeptid ID="t_SD" runat="server" />
                </td>
                <td class="t_r t_bg">施工许可证号：
                </td>
                <td>
                    <asp:TextBox ID="t_XKZ" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
            AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JD" HeaderText="阶段" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DJ" HeaderText="评定等级" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="RQ" HeaderText="评定日期" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DWGCMC" HeaderText="工程名称" HeaderStyle-Width="150px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XKZBH" HeaderText="施工许可证号" HeaderStyle-Width="110px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JSDW" HeaderText="建设单位" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XMSD" HeaderText="工程所属地" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JSXZ" HeaderText="工程性质" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XMDZ" HeaderText="工程地址" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="HTJG" HeaderText="合同价格（万元）" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ZT" HeaderText="开工状态" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="isJG" HeaderText="是否竣工" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="fid" Visible="False"></asp:BoundColumn>
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
