<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryEnd.aspx.cs" Inherits="Government_AppXMBJ_QueryEnd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script src="../../DateSelect/WdatePicker.js" type="text/javascript"></script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">办结查询
                </th>
            </tr>
            <tr>
                <td class="t_r">项目名称：</td>
                <td><asp:TextBox ID="txtXMMC" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <td class="t_r">项目编号：</td>
                <td><asp:TextBox ID="txtXMBH" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <td class="t_r">建设单位：</td>
                <td><asp:TextBox ID="txtJSDW" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <td rowspan="2" align="center">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                </td>
            </tr>
            <tr>
                <td class="t_r">所属地：
                </td>
                <td>
                    <uc2:govdeptid ID="uegovdeptid" runat="server" />
                </td>
                <td class="t_r">审核结果：
                </td>
                <td>
                    <asp:DropDownList ID="ddlSeeState" runat="server" CssClass="m_txt" Width="100px">
                        <asp:ListItem Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="1">通过</asp:ListItem>
                        <asp:ListItem Value="3">不通过</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">办结日期起：</td>
                <td>
                <asp:TextBox ID="txtBJRQStart" runat="server" CssClass="m_txt Wdate" Width="100px" onfocus="WdatePicker({skin:'whyGreen' })"></asp:TextBox>
                办结日期止：<asp:TextBox ID="txtBJRQEnd" runat="server" CssClass="m_txt Wdate" Width="100px" onfocus="WdatePicker({skin:'whyGreen' })"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号" HeaderStyle-Width="50px">
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="项目编号" HeaderStyle-Width="100px" DataField="BH">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="项目名称" HeaderStyle-Width="100px" DataField="XMMC">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="所属地" HeaderStyle-Width="100px" DataField="XMSDMC">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="建设单位" HeaderStyle-Width="100px" DataField="JSDW">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="申请日期" HeaderStyle-Width="100px" DataField="CreateTime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="办结日期" HeaderStyle-Width="100px" DataField="FTime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核结果" DataField="idear" HeaderStyle-Width="80px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="YWBM" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
            </Columns>
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
        <div class="d div1 tcen" style="width: 98%; margin: 0px auto;">
            <uc1:pager ID="Pager1" runat="server"></uc1:pager>
        </div>
    </form>
</body>
</html>
