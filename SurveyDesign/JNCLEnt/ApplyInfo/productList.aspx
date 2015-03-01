<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productList.aspx.cs" Inherits="JNCLEnt_mangeInfo_productList" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

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
    <script type="text/javascript">
        function getFilUp(url) {
            var fid = document.getElementById("t_FID").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?FAppId=" + fid + "&type=1001", 550, 400);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">本次备案材料和产品
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="t_r t_bg">材料和产品名称：
                </td>
                <td>
                    <asp:TextBox ID="t_MC" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r t_bg">产品类别：
                </td>
                <td>
                    <asp:TextBox ID="t_BZMC" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>                
                <td class="t_r" style="padding-right: 10px;" colspan="3">
                    <input id="Submit1" type="button" value="新增" onclick="showAddWindow('editProduct.aspx?', 800, 400);"
                        runat="server" class="m_btn_w2" />
                    &nbsp;<asp:Button ID="btnDel" runat="server" Text="删除" OnClick="btnDel_Click" class="m_btn_w2" />
                    &nbsp;<asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" class="m_btn_w2" />
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
                    <ItemStyle Width="30px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="" HeaderText="产品类别" HeaderStyle-Width="90px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MC" HeaderText="材料和产品名称" HeaderStyle-Width="120px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BZMC" HeaderText="产品标准名称" HeaderStyle-Width="120px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BZH" HeaderText="产品标准号" HeaderStyle-Width="120px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
               <%-- <asp:BoundColumn DataField="DJBH" HeaderText="备案登记号" HeaderStyle-Width="120px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DJSJ" HeaderText="备案登记时间" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="120px">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>--%>
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
        <input id="t_fappid" runat="server" type="hidden" />
        <input id="t_fid" runat="server" type="hidden" />
    </form>
</body>
</html>
