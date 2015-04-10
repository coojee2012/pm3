<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportList.aspx.cs" Inherits="Government_AppBHGD_SHDCList" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上会导出</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="9">
                    <asp:Literal ID="lPostion" runat="server">上会导出</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">年度
                </td>
                <td>

                    <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                </td>

                <td class="t_r">批次
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Batch" runat="server"></asp:DropDownList>

                </td>

                <td class="t_r">
                    工程名称
                </td>
                <td>
                    <asp:TextBox ID="txt_prjName" runat="server"></asp:TextBox>
                </td>


                 <td class="t_r">
                    申报单位
                </td>
                <td>
                    <asp:TextBox ID="txt_deptName" runat="server"></asp:TextBox>
                </td>
               




                <td align="center" rowspan="3" colspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="BtnQuery" CssClass="m_btn_w2" />
                </td>


                
               
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:Button ID="btnOut" OnClick="Onbtn_OutClick" runat="server" CssClass="m_btn_w4" Text="导出" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
         <asp:DataGrid ID="gv_list" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Width="98%" OnItemDataBound="App_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemStyle Width="20px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="工程名称" DataField="ProjectName">
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="申报单位" DataField="SBDW">
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="年度" DataField="FYear">
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="批次" DataField="FBatchNumber">
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审批环节" DataField="SPHJ">
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
       
    
    <div class="d div1 tcen" style="width: 98%; margin: 0px auto;">
        <uc1:pager ID="Pager1" runat="server"></uc1:pager>
    </div>
    </form>
</body>
</html>
