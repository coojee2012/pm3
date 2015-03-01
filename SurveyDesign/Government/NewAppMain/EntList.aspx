<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntList.aspx.cs" Inherits="Admin_main_EntList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
     <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script language="javascript">
    function clearQuery()
    {
        document.getElementById("txtFName").value="";
        document.getElementById("dbSystemId").value=""; 
    }
    </script>

    <style>
    .PadL
    {
        padding-left:3px;
    }
    </style>
    <base target="_self">
    </base>
</head>
<body style="overflow-x: hidden">
    <form id="form1" runat="server">
    
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                选择企业
            </th>
        </tr>
        <tr id="Tr1">
            <td class="t_r">
                     企业名称：
            </td>
            <td>
               <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r">
                企业类型：
            </td>
            <td>
              <asp:DropDownList ID="dbSystemId" runat="server" CssClass="cTextBox1" Width="150px">
                                            </asp:DropDownList>
            </td>
            <td align="center">
              <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="查询" OnClick="btnQuery_Click" />
                                            <input id="Button1" type="button" value="清空" class="m_btn_w2" onclick="clearQuery();"
                                                style="margin-left: 3px" />
                                            <asp:Button ID="btnOk" runat="server" CssClass="m_btn_w2" Text="选定" OnClick="btnOk_Click" />
            </td>
        </tr>
    </table>
    
 
           
                  
                    <asp:DataGrid ID="EntInfo_List" runat="server" HorizontalAlign="Center" Width="98%"
                        CssClass="m_dg1" Style="margin-top: 7px; word-break: normal; word-wrap: normal;"
                        AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound">
                   <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemStyle Width="25px" Font-Underline="False" Wrap="False" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckItem" runat="server" />
                                </ItemTemplate>
                                <FooterStyle Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn HeaderText="序号">
                                <ItemStyle Width="30px" Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FName" HeaderText="企业名称">
                                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" CssClass="PadL" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FSystemName" HeaderText="企业类型">
                                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" CssClass="PadL" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FLinkMan" HeaderText="联系人">
                                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" CssClass="PadL" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FTel" HeaderText="联系电话">
                                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" CssClass="PadL" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FId" Visible="False">
                                <ItemStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Underline="False" Wrap="False" />
                    </asp:DataGrid>
       <table align="center" width="98%" style="margin-top: 5px;">
                        <tr>
                            <td>
                                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                            </td>
                        </tr>
                    </table>
    </form>
</body>
</html>
