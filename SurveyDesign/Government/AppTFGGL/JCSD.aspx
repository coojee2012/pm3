<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCSD.aspx.cs" Inherits="Government_AppTFGGL_JCSD" %>

<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目人员列表</title>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <input type="hidden" runat="server" id="t_FIdCard" value="" />
         <table width="98%" align="center" class="m_title">
            <tr>
                <th >解锁原因:
                </th>
               
                     <td >
                    <asp:TextBox ID="t_JSYY" runat="server" CssClass="m_txt" Width="400px"
                        MaxLength="30"></asp:TextBox>
                </td>
                <td>
                           <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                               <ContentTemplate>
                     <asp:Button ID="btnJS" runat="server" Text="解锁" CssClass="m_btn_w2" OnClick="btnJS_Click"   />
                     <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click"   />
                                  </ContentTemplate>
                    </asp:UpdatePanel> 
                </td>
            </tr>
             </table>
        <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px; margin-bottom: 1px;"
            Width="98%" OnItemCommand="dg_List_ItemCommand">
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
                    <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FHumanName" HeaderText="姓名"></asp:BoundColumn>
                <asp:BoundColumn DataField="ProjectName" HeaderText="项目名称"></asp:BoundColumn>
     <asp:BoundColumn DataField="DeptName" HeaderText="项目属地"></asp:BoundColumn>
                <asp:BoundColumn DataField="StartDate" HeaderText="开工时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
                <asp:BoundColumn DataField="EndDate" HeaderText="预计竣工时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
                  <asp:BoundColumn DataField="EmpType" HeaderText="人员类型"></asp:BoundColumn>
             
                <asp:BoundColumn DataField="FCreateTime" HeaderText="锁定时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
                <asp:BoundColumn DataField="FTime" HeaderText="锁定结束时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn> 
                <asp:BoundColumn DataField="PrjAddressDept" Visible="False"></asp:BoundColumn>         
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <div class="d div1 tcen" style="width: 98%; margin: 0px auto;">
        <uc1:pager ID="Pager1" runat="server"></uc1:pager>
    </div>
    </form>
</body>
</html>
