<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessLostList.aspx.cs"
    Inherits="Government_ProcessManager_ProcessLostList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>丢失流程列表</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });       
    </script>

    <script src="../script/default.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <meta http-equiv="x-ua-compatible" content="ie=7" />

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

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                <asp:Literal ID="lPostion" runat="server" Text="丢失流程列表"></asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                企业类别：
            </td>
            <td class="txt34">
                <asp:DropDownList ID="dbSystemId" runat="server" CssClass="m_txt" Width="100px" AutoPostBack="True"
                    OnSelectedIndexChanged="dbSystemId_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                业务类型：
            </td>
            <td class="txt34">
                <asp:DropDownList ID="dmType" runat="server" CssClass="m_txt" Width="100px">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                企业名称：
            </td>
            <td class="txt34">
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
            <td style="text-align: center">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                    Text="查询" /><input id="btnClear" class="m_btn_w2" type="button" value="重置" onclick="clearPage();"
                        style="margin-left: 10px" />
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <asp:Button ID="btnReport" runat="server" CssClass="m_btn_w4" Text="重新上报" OnClick="btnReport_Click" />
                <asp:Button ID="btnOut" runat="server" CssClass="m_btn_w2" OnClick="btnOut_Click"
                    Text="导出" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Style="margin-top: 4px"
        Width="100%">
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
            <asp:BoundColumn HeaderText="企业名称" DataField="FEntName">
                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="业务类型" DataField="FManageTypeId">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="申报内容" DataField="FListId">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="申报内容" DataField="FTypeId" Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="申报内容" DataField="FLevelid" Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="上报时间" DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FBaseInfoId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FManageTypeId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FLinkId" Visible="False"></asp:BoundColumn>
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
    <div class="d div1 tcen">
        <uc1:pager ID="Pager1" runat="server"></uc1:pager>
    </div>
    </form>
</body>
</html>

<script language="javascript">
function fIsCanBatchApp()
{
//    if(document.getElementById("HMTypeId").value=="")
//    {
//        alert("请先查询出相同业务类型的数据,在批量审核");
//        return false;
//    }
    return true;
}
</script>

