<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZBJGInfo.aspx.cs" Inherits="JSDW_ApplyZBBA_ZBJGINFO" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>中标结果备案</title>
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
        function selHXR(obj) {
            var fBDId = '<%=ViewState["BDId"] %>';
            var pid = showWinByReturn('../ApplyZBBA/HXRSel.aspx?fBDId=' + fBDId, 1000, 500);
            if (pid != null && pid != '') {
                $("#t_QYId").val(pid);
                __doPostBack(obj.id, '');
            }
        }
    </script>
    <base target="_self">
    </base>
    <style type="text/css">
        .modalDiv {display:none; position: absolute; top: 1px; left: 1px; width: 100%; height: 100%; z-index:9999; background-color:gray; opacity:.50; filter: alpha(opacity=50); }
        .m_txt {}
        .auto-style1 {
            height: 25px;
        }
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
                    第<asp:TextBox ID="t_CS" runat="server" CssClass="m_txt" Width="30px"></asp:TextBox>次招标<tt>*</tt>
                    <input id="txtFId" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    标段编码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_BDBM" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    标段名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_BDMC" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    标段项目名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    招投标编码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZTBBM"  runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>  
             <tr>
                <td class="t_r t_bg">
                   招标代理单位：
                </td>
                <td colspan="1" class="auto-style1">
                    <asp:TextBox ID="t_ZBDLDW" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox> 
                    <asp:Button ID="btnSelEnt" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selHXR(this);"
                                        UseSubmitBehavior="false" OnClick="btnSel_Click" />
                </td>
                <td class="t_r t_bg">
                    招标人：
                </td>
                <td colspan="1" class="auto-style1">
                    <asp:TextBox ID="t_ZHAOBR"  runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr> 
             <tr>
                <td class="t_r t_bg">
                    开标时间：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_KBSJ" runat="server" onfocus="WdatePicker()"  CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    企业编码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_QYBM" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr> 
             <tr>
                <td class="t_r t_bg">
                    中标人：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_ZHONGBR" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                    <asp:Button ID="btnSel" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selHXR(this);"
                                        UseSubmitBehavior="false" OnClick="btnSel_Click" />
                    <input id="t_QYId" type="hidden" runat="server" />
                </td>
                
            </tr>  
            <tr>
                <td class="t_r t_bg">
                    合同签订地点：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_HTQDDD" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    通知发放时间：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_TZFFSJ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    开工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_KGRQ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    竣工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JGRQ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>
             
             <tr>
                <td class="t_r t_bg">
                    中标结果：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_ZBJG" runat="server" CssClass="m_txt" Width="200px" ></asp:DropDownList>
                </td>
                
            </tr> 
            <tr>
                <td class="t_r t_bg">
                    中标原因：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_ZBYY" runat="server" CssClass="m_txt" Width="539px" Height="35px" TextMode="MultiLine" ></asp:TextBox>
                </td>
                
            </tr>        
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    附件
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
