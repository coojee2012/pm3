<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerSonJLList.aspx.cs" Inherits="JSDW_ApplyJGYS_PerSonJLList" %>

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
    <script type="text/javascript">
        $(function () {
            $("#btnYes").click(function () {
                var items = $("#DG_List").find(".checkboxItem > input[type=checkbox][checked]");
                if (items.length < 1) {
                    alert("当前未选择任何项");
                    return false;
                }
                var array = new Array();
                for (var i = 0; i < items.length; i++) {
                    var value = $(items[i]).parent("span").attr("RYZSXXID");
                    array.push(value);
                }
                if ($("#ddlRYLX").find("option:selected").text() == "项目负责人") {
                    if (array.length > 1) {
                        alert("项目负责人只能选一人");
                        return false;
                    }
                }
                var str = array.join('|') + "_" + $("#ddlRYLX").val().split('_')[1] + "_" + $("#ddlRYLX").find("option:selected").text();
                //$("#txtPersonName").val(str);
                window.returnValue = str;
                window.close();
            });
            $("#btnExit").click(function () {
                window.close();
            });
            $("#ddlRYLX").change(function () {
                $("#btnQuery").click();

            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <%--<td width="100">人员类型：</td>
                <td width="150">
                    <asp:DropDownList ID="ddlRYLX" runat="server">
                    </asp:DropDownList>
                </td>--%>
                <td width="100">人员名称：</td>
                <td width="150"><asp:TextBox ID="txtPersonName" runat="server"></asp:TextBox></td>
                <td width="100"><asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="m_btn_w2" OnClick="btnQuery_Click"  /></td>
                <td width="auto">
                    <asp:DropDownList ID="ddlRYLX" runat="server">
                        <asp:ListItem Value="8,9_20012101">项目总监</asp:ListItem>
                        <asp:ListItem Value="6,7,10,61_20012102">专业监理工程师</asp:ListItem>
                        <asp:ListItem Value="10_20012103">监理员</asp:ListItem>
                    </asp:DropDownList>
                    <input type="button" value="选择" class="m_btn_w2" id="btnYes" />&nbsp;&nbsp;
                    <input type="button" value="返回" class="m_btn_w2" id="btnExit" />
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
                <asp:BoundColumn DataField="XM" HeaderText="姓名">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XB" HeaderText="性别"></asp:BoundColumn>
                <%--<asp:BoundColumn DataField="ZSJB" HeaderText="证书级别"></asp:BoundColumn>--%>
                <asp:BoundColumn DataField="ZCZY" HeaderText="注册专业名称"></asp:BoundColumn>
                <asp:BoundColumn DataField="ZCZSH" HeaderText="注册证书号">
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ZSYXQJSSJ" HeaderText="证书有效期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
                <asp:BoundColumn DataField="FZSJ" HeaderText="发证日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
            <asp:BoundColumn DataField="RYZSXXID" Visible="false"></asp:BoundColumn>
                <%--<asp:TemplateColumn HeaderStyle-Width="100">
                    <ItemTemplate>
                        <a href="javascript:void(0)" onclick="Verify('<%#Eval("ID") %>')">选 定</a>
                    </ItemTemplate>
                </asp:TemplateColumn>--%>
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
