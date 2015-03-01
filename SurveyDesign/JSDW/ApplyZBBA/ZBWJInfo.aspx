<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZBWJInfo.aspx.cs" Inherits="JSDW_ApplyZBBA_ZBWJInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>招标文件</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function addPrjItem() {
            var fid = document.getElementById("txtFId").value;
            var fAppId = '<%=ViewState["FAppId"] %>';
            var fPrjItemId = '<%=ViewState["FPrjItemId"] %>';
            if (fid == null || fid == '') {
                alert('请先保存上方信息！');
                return;
            }
            showAddWindow('File.aspx?fLinkId=' + fid + "&&fAppId=" + fAppId + "&&fPrjItemId=" + fPrjItemId, 800, 550);
            //  alert('dd')
        }
    </script>
    <base target="_self">
    </base>
    <style type="text/css">
        .modalDiv { position: absolute; top: 1px; left: 1px; width: 100%; height: 100%; z-index:9999; background-color:gray; opacity:.50; filter: alpha(opacity=50); }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="up_Main" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv"> 
                <div style="position:absolute;left:40%;top:50%;background-color:peru;border:solid 3px red;">
                    <table  align="center">
                    <tr>
                    <td ><h1>正在保存数据</h1></td>
                    <td><img src="../../image/load2.gif" alt="请稍候"/></td>
                    </tr>
                                    
                    </table>
                </div>
                    </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

            
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                         <ContentTemplate>
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                            OnClientClick="return checkInfo();" />  
                        </ContentTemplate>
                    </asp:UpdatePanel>                
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    招标次数：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_CS" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <input id="txtFId" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    标段编码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_BDBM" runat="server" CssClass="m_txt" Width="200px" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    标段名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_BDMC" runat="server" CssClass="m_txt" Width="200px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    标段项目名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="200px" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    备案时间：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_BATime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td class="t_r t_bg">
                    发包方式：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FBFS" runat="server" Width="200px" CssClass="m_txt">
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    资格预审方式：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZGYSFS" runat="server" CssClass="m_txt" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr> 
            <tr>
                <td class="t_r t_bg">
                    招标组织形式：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZBZZXS" runat="server" Width="200px" CssClass="m_txt" AutoPostBack="true" OnSelectedIndexChanged="t_ZBZZXS_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    代理机构：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_DLJG" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td class="t_r t_bg">
                    编制人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_BZR" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    审核人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_SHR" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>   
            <tr>
                <td class="t_r t_bg">
                    审定人：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_SDR" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                
            </tr>           
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    招标文件
                </td>
                <td class="t_r">
                    <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="addPrjItem();" />
                    <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                        OnClick="btnDel_Click" />
                    <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px;
            margin-bottom: 1px;" Width="98%">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <HeaderStyle Width="30px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <HeaderStyle Width="30px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="材料名称" DataField="FileName">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="上传时间" DataField="ReportTime" DataFormatString="{0:d}">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <div style="padding-left: 1%">
            <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
            </webdiyer:AspNetPager>
        </div>
    </div>
    
    </form>
</body>
</html>