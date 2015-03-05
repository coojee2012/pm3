<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGXKZList.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_SGXKZList" %>

<!DOCTYPE html>
<%@ Register Src="../../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../script/default.js"> </script>
    <script type="text/javascript">
        function Add() {
            showAddWindow("SGXKZForm.aspx?JG_Id=" + $("#hfId").val(), 1000, 500);
            document.getElementById("btnRefresh").click();
        }
        function Edit(Id) {
            showAddWindow("SGXKZForm.aspx?JG_Id=" + $("#hfId").val() + "&Id=" + Id, 1000, 500);
            document.getElementById("btnRefresh").click();
        }
        function Show(Id)
        {
            showAddWindow("SGXKZForm.aspx?JG_Id=" + $("#hfId").val() + "&Id=" + Id+"&IsShow=1", 1000, 500);
        }
        function Save() {
            var isTrans = $("#ddlIsTrans").val();
            var LY = $("#txtLY").val();
            if (isTrans != "1") {
                if ($.trim(LY).length == 0) {
                    alert("请输入理由");
                    return false;
                }
            }
            return true;
        }
        $(function () {
            if ($("#hfSrouce").val() == "True") {//来源标准库
                $("#ddlIsTrans").val('4');
                $("#ddlIsTrans").attr("disabled", "disabled");
                $("#LY").hide();
            } else {
                $("#ddlIsTrans").children().eq(3).remove();
                var isTrans = $("#ddlIsTrans").val();
                if (isTrans == "1") {
                    $("#txtLY").val('');
                    $("#txtLY").attr("disabled", "disabled");
                    $("#LY").hide();
                } else {
                    $("#txtLY").removeAttr("disabled");
                    $("#LY").show();
                }
                $("#ddlIsTrans").change(function () {
                    var isTrans = $(this).val();
                    if (isTrans == "1") {
                        $("#txtLY").val('');
                        $("#txtLY").attr("disabled", "disabled");
                        $("#LY").hide();
                    } else {
                        $("#txtLY").removeAttr("disabled");
                        $("#LY").show();
                    }
                });
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfId" runat="server" />
        <asp:HiddenField ID="hfIsExists" runat="server" />
        <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                施工许可证
            </th>
        </tr>
    </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" 
                        OnClientClick="return Save();" onclick="btnSave_Click" />
                    &nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnRefresh" runat="server" CssClass="m_btn_w2" Text="刷 新" OnClick="btnRefresh_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <%--<input type="button" value="新增" class="m_btn_w2"  onclick="Add()"  />--%>
                    <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="m_btn_w2" OnClientClick="Add();return false;" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    办理选项：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlIsTrans" runat="server" Width="200">
                        <asp:ListItem Value="1">补填</asp:ListItem>
                        <asp:ListItem Value="2">不需要办理</asp:ListItem>
                        <asp:ListItem Value="3">以后补办</asp:ListItem>
                        <asp:ListItem Value="4">已办</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="LY">
                <td class="t_r t_bg">
                    理由：
                </td>
                <td colspan="3"><asp:TextBox ID="txtLY" runat="server" CssClass="m_txt" Width="500" Height="40" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center"
        Style="margin-top: 7px" Width="98%" OnItemDataBound="DG_List_ItemDataBound" OnItemCommand="DG_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="40px" />
            </asp:BoundColumn>      
            <asp:BoundColumn DataField="XMJLXM" HeaderText="项目经理"></asp:BoundColumn>
            <asp:BoundColumn DataField="SGXKZBH" HeaderText="施工许可证编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="BH" HeaderText="项目编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="HTJE" HeaderText="合同金额（万元）"></asp:BoundColumn>
            <asp:BoundColumn DataField="MJ" HeaderText="面积（m2）"></asp:BoundColumn>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Text="删 除" CommandName="del" OnClientClick="return confirm('确认删除?');" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<a href="#" onclick="Edit('<%#Eval("ID") %>')">编 辑</a>
                </ItemTemplate>
            </asp:TemplateColumn>
           <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
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

