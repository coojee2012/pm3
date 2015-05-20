<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntListSelsql.aspx.cs"
    Inherits="JSDW_project_EntListSelsql" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="../../Common/pager.ascx" tagname="pager" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="lTitle" runat="server" Text="" ></asp:Label>企业列表
            </th>
        </tr>
        <tr>
            <td colspan="1" class="t_r">
                企业名称
            </td>
            <td colspan="4" class="t_l">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                <input type="button" id="Button1" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
        </tr>
    </table>
    <asp:Repeater ID="dg_List" runat="server" OnItemDataBound="dg_List_ItemDataBound" OnItemCommand="dg_List_ItemCommand">
        <HeaderTemplate>
            <table width="98%" align="center" class="m_dg1">
                <tr class="m_dg1_h">
                    <th>
                        序号
                    </th>
                    <th>
                        企业名称
                    </th>
                    <th>
                        资质证书编号
                    </th>
                    
                    <th>
                        资质及等级
                    </th>
                    <th>
                       属地
                    </th>
                    <th>
                       单位地址
                    </th>
                   <%-- <th>
                        法人
                    </th>
                    <th>
                        联系人
                    </th>
                    <th>
                        联系电话
                    </th>--%>
                    <th>
                        选择
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                    <tr class="m_dg1_i">
                        <td>
                            <%# Container.ItemIndex + 1%> 
                        </td>
                        <td>
                            <%# Eval("QYMC")%>
                        </td>
                        <td>
                            
                            <%# Eval("ZSBH")%>
                        </td>
                       
                        <td>
                            <%# Eval("ZZMC")%>
                        </td>
                        <td>
                            
                            <%# Eval("RegAdrProvinceName")%>
                        </td>
                        <td>
                            
                            <%# Eval("QYXXDZ")%>
                        </td>
                        <td runat ="server" visible ="false">
                            <%# Eval("FRDB")%>
                        </td>
                        <td runat ="server" visible ="false">
                            
                            <%# Eval("LXR")%>
                        </td>
                        <td runat ="server" visible ="false">
                            <%# Eval("LXDH")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnSelect" CommandName="Sel" runat="server">选择</asp:LinkButton>
                             <asp:HiddenField ID="hfFBaseInfoId" Value='<%# Eval("QYBM") %>' runat="server" />
                            <asp:HiddenField ID="hfqyzzid" Value='<%# Eval("QYZZID") %>' runat="server" />
                        </td>
                    </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div style="padding-left: 1%">
        <uc1:pager ID="pager1" runat="server" />
    </div>
    </form>
</body>
</html>
