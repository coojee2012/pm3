<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZTBList.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_ZTBList" %>

<!DOCTYPE html>
<%@ Register Src="../../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="Link1" runat="server">
    </asp:Link>
    <script src="../../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../script/default.js"> </script>
    <script type="text/javascript">
        function Add() {
            showAddWindow("ZTBJLForm.aspx?JG_Id=" + $("#hfId").val(), 900, 500);
            document.getElementById("btnRefresh").click();
        }
        function Edit(Id, obj) {
            var ZTBID = $(obj).attr("ZTBID");
            showAddWindow("ZTBJLForm.aspx?JG_Id=" + $("#hfId").val() + "&Id=" + Id + "&ZTBID=" + ZTBID, 900, 500);
            document.getElementById("btnRefresh").click();
        }
        function Show(Id, obj) {
            var ZTBID = $(obj).attr("ZTBID");
            showAddWindow("ZTBForm.aspx?JG_Id=" + $("#hfId").val() + "&Id=" + Id + "&IsShow=1" + "&ZTBID=" + ZTBID, 900, 500);
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
                招投标信息
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
                    <asp:DropDownList ID="ddlQYType" runat="server" style="display:none;">
                        <asp:ListItem Value="11220801">监理单位</asp:ListItem>
                        <asp:ListItem Value="11220802">施工单位</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="新增" OnClientClick="Add();return false" />
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
            <asp:BoundColumn DataField="GCMC" HeaderText="工程名称"></asp:BoundColumn>
            <asp:BoundColumn DataField="UnitType" HeaderText="企业类型"></asp:BoundColumn>
            <asp:BoundColumn DataField="ZBTZSBH" HeaderText="中标通知书编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="ZMJ" HeaderText="总面积（平方米）"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="操作" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:LinkButton ID="lbDel" runat="server" CommandName="del" OnClientClick="return confirm('确认删除？')" Text="删 除"></asp:LinkButton>
                    &nbsp;&nbsp;<a href="#" value="<%#Eval("UnitType") %>" ZTBID="<%#Eval("ID") %>" onclick="Edit('<%#Eval("DWID") %>',this)">编 辑</a>
                </ItemTemplate>
            </asp:TemplateColumn>
           <asp:BoundColumn DataField="DWID" Visible="False"></asp:BoundColumn>
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

