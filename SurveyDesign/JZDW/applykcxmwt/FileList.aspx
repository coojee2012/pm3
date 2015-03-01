<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileList.aspx.cs" Inherits="PrjManage_ConstructionLicence_ApplyBaseinfo_Paperdrawing" %>

<%@ Register TagPrefix="uc1" TagName="pager" Src="~/Common/pager.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>附件列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                上传附件信息
            </th>
        </tr>
    </table>
    <asp:GridView ID="File_list" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        Style="width: 98%;" OnRowDataBound="File_list_RowDataBound" align="center">
        <HeaderStyle CssClass="m_dg1_h" />
        <RowStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundField HeaderText="序号" DataField="FOrder"></asp:BoundField>
            <asp:BoundField DataField="FFileName" HeaderText="资料名称">
                <ItemStyle CssClass="t_l" />
            </asp:BoundField>
            <asp:BoundField DataField="FFileAmount" HeaderText="应送份数"></asp:BoundField>
            <asp:TemplateField HeaderText="文件编号">
                <ItemTemplate>
                    <asp:TextBox ID="t_FFileNo" runat="server" CssClass="m_txt" Width="160px" ReadOnly="true"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="文件个数">
                <ItemTemplate>
                    <asp:TextBox ID="t_FCount" runat="server" CssClass="m_txt" onblur="isInt(this)" ReadOnly="true"
                        Width="50px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="是否上传" Visible="False"></asp:BoundField>
            <asp:TemplateField HeaderText="<font color=green>是</font>/<font color=red>否</font> 上传">
                <ItemTemplate>
                    <asp:Label ID="IsUpload" runat="server" Text="Label"></asp:Label>
                    <input id="btnUpLoad" type="button" runat="server" class="m_btn_w4" value="上传文件" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FId" Visible="False"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <table align="center" width="98%">
        <tr>
            <td style="height: 23px">
                <uc1:pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
