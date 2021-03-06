﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualiTypeSelected.aspx.cs"
    Inherits="Admin_main_QualiTypeSelected" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <base target="_self"></base>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
             DynamicGrid(); //列表光标移动效果
        });  
       function selectWindowReturnValue(value) 
       {
         window.returnValue=value;
         window.close();
       }         
    </script>

    <meta http-equiv="x-ua-compatible" content="ie=7" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                流程包含专业维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" OnClick="btnSave_Click"
                    Text="保存" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Dg_List" runat="server" AutoGenerateColumns="false" BorderWidth="0px"
        HorizontalAlign="Center" OnItemDataBound="Dg_List_ItemDataBound" ShowHeader="true"
        Width="98%">
        <Columns>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td bgcolor="#F0F4FF" height="30">
                                <asp:Label ID="la_Tilte" runat="server" Style="font-size: 14px; font-weight: bold;"
                                    Text='<%# DataBinder.Eval(Container.DataItem,"FName") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="checkBoxInfo" runat="server" RepeatColumns="5" Width="98%">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FNumber" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    </form>
</body>
</html>
