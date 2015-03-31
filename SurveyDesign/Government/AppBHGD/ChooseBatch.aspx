<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChooseBatch.aspx.cs" Inherits="Government_AppBHGD_ChooseBatch" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>选择批次</title>
     <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <base target="_self" />
    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:DataGrid ID="gv_list" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Width="98%" OnItemDataBound="App_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>

                <asp:BoundColumn HeaderText="序号">
                    <HeaderStyle Width="30px" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
               
                <asp:BoundColumn HeaderText="年度" DataField="FYear">
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="批次" DataField="FBatchNumber">
                    <ItemStyle Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
               <asp:TemplateColumn HeaderText="选择">
                     <ItemStyle Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                   <ItemTemplate>
                       <asp:Button runat="server" ID="BtnChoose"  CssClass="m_btn_w2" Text="选择"/>
                   </ItemTemplate>
                   </asp:TemplateColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <div style="padding-left: 1%">
            <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
            </webdiyer:AspNetPager>
        </div>
    </div>
    </form>
</body>
</html>
