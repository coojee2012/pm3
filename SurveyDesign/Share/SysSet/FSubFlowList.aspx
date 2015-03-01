<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FSubFlowList.aspx.cs" Inherits="Government_ProcessManager_FSubFlowList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>接见件</title>
    <style type="text/css">
        .cBtn12
        {
            background-image: url(   "../images/buttonbgl.gif" );
            background-repeat: no-repeat;
            width: 90px;
            height: 22px;
            background-color: Transparent;
            border: none 0px;
            text-align: center;
            cursor: pointer;
            margin-top: 3px;
            margin-right: 3px;
            margin-bottom: 3px;
        }
    </style>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script src="../script/default.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });       
    </script>

    <meta http-equiv="x-ua-compatible" content="ie=7" />

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script>
    function showApproveWindow1(sUrl,width,height)
    {
     var ret=window.showModalDialog(sUrl+'&rid='+Math.random(),'','dialogWidth:'+width+'px; dialogHeight:'+height+'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')      

     if (ret=="1")
     {
         form1.btnQuery.click()
     }
     }
    function ShowWindow(url,width,hieght,obj)
    {
        var sFeatures = "status:no;dialogHeight:"+hieght+"px;dialogwidth:"+width+"px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;"; 
        
        var idvalue= window.showModalDialog(url+'&rid='+Math.random(),obj,sFeatures); 
        
        if(idvalue=="1")
        {
            form1.btnQuery.click();
        }
          
       
    }
    </script>

    <base target="_self" />
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                <asp:Literal ID="lPostion" runat="server" Text="子流程列表"></asp:Literal>
            </th>
        </tr>
    </table>
    <asp:Button ID="btnQuery" runat="server" CssClass="cBtn12" OnClick="btnQuery_Click"
        Text="提取办结记录" OnClientClick="return confirm('确定要提取吗')" /><br />
    <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="100%"
        OnItemCommand="JustAppInfo_List_ItemCommand">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn Visible="false">
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
            <asp:BoundColumn HeaderText="子流程名称" DataField="FName">
                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="所属审核阶段" DataField="FTypeId">
                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核角色" DataField="FRoleId">
                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="等级" DataField="FLevel">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="流程是否结束" DataField="FIsEnd">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="是否就位" DataField="FIsQuali">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:ButtonColumn Text="跳转到该步" CommandName="Select"></asp:ButtonColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
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
    </form>
</body>
</html>
