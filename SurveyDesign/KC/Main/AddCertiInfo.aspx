<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCertiInfo.aspx.cs" Inherits="Government_EntQualiCerti_AddCertiInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>证书信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>

    <script type="text/javascript">
        function checkInfo() {
            return AutoCheckInfo();
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                企业证书信息
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="Button1" class="m_btn_w2" type="button" value="返回" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" width="15%">
                证书编号
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FCertiNo" runat="server" CssClass="m_txt" MaxLength="30"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg" width="15%">
                
            </td>
            <td class="txt34">
                <asp:DropDownList ID="t_FLevelId" runat="server" CssClass="m_txt" Visible="false">
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr style=" display:none">
            <td class="t_r t_bg" width="15%">
                证书类别
            </td>
            <td class="txt34" colspan="1">
                <asp:DropDownList ID="t_FCertiType" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                是否暂定
            </td>
            <td class="txt34">
                <asp:CheckBox ID="cbFIsTemp" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                颁发部门
            </td>
            <td class="txt34">
                <asp:DropDownList ID="t_FAppDeptId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <font color="red"></font>
            </td>
            <td class="t_r t_bg">
                颁发时间
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FAppTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="15%">
                开始时间
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FBeginTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                结束时间
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FEndTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                包含范围
            </td>
            <td class="txt34" colspan="3">
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Height="70px" TextMode="MultiLine"
                    Width="95%" Style="text-align: left"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="left">
                行业分类信息
            </td>
            <td class="m_bar_m" align="right">
                <input id="btnAdd" class="m_btn_w2" type="button" value="添加" onclick="CanAdd();" runat="server" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 3px"
        Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
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
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FListName" HeaderText="序列">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FTypeName" HeaderText="行业">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FLevelName" HeaderText="等级">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FAppDeptName" HeaderText="核准单位">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FAppTime" HeaderText="核准时间" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" HeaderText="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <input id="HFID" runat="server" type="hidden" />
    <input id="FRegistDeptId" runat="server" type="hidden" />
    </form>
</body>
</html>

<script language="javascript">
    function CanAdd() {
        var fCertiNoId = document.getElementById("HFID").value.trim();
        if (fCertiNoId == null || fCertiNoId == "") {
            alert("请先保存上方的证书信息！");
            return false;
        }
        showAddWindow('AddQualiInfo.aspx?fbid=<%=ViewState["FBaseId"] %>&fcid=' + fCertiNoId, 700, 500);
    } 
</script>

