<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjItemDescForYQ.aspx.cs" Inherits="JSDW_ApplySGXKZGL_PrjItemDescForYQ" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptidfalse2.ascx" TagName="govdeptid1" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工程简要说明</title>
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
    </script>
    <base target="_self">
    </base>
    <style type="text/css">
        .modalDiv { position: absolute; top: 1px; left: 1px; width: 100%; height: 100%; z-index:9999; background-color:gray; opacity:.50; filter: alpha(opacity=50); }
        .m_txt {}
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="up_Main" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv" style="display:none;"> 
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
                        <asp:HiddenField  runat="server" ID="hf_FprjItemId" Value="" />
                             <asp:HiddenField  runat="server" ID="hf_FAppId" Value="" />
        <asp:HiddenField  runat="server" ID="hf_FId" Value="" />
                        </ContentTemplate>
                    </asp:UpdatePanel>                
                    
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="4">
                    建设单位基本信息
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="200px" Enabled="false" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width:18.8%;">
                    建设单位所属地：
                </td>
                <td colspan="1" style="width:29%;">
                    <input type="hidden"  runat="server" ID="t_JSDWAddressDept" value="" />
                    <uc2:govdeptid ID="JSDW_DeptID" runat="server" />
                    
                </td>
                <td class="t_r t_bg">
                    建设单位地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDWDZ" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    所有制性质：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_JSDWXZ" runat="server" CssClass="m_txt" Width="203px" Enabled="false"></asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    法定代表人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FDDBR"  runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    法人电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FRDH" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    领证人：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="t_LZR" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td >
                    <asp:TextBox ID="t_LXDH" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    建设单位负责人：
                </td>
                <td >
                    <asp:TextBox ID="t_JSFZR" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位项目负责人职称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSFZRZC" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    建设单位项目负责人电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSFZRDH" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                    
                </td>
            </tr>            
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="4">
                    工程基本信息
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg"  style="width:18.8%;">
                    项目名称：
                </td>
                <td style="width:29%;">
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    工程名称：
                </td>
                <td >
                    <asp:TextBox ID="t_PrjItemName" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程类别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_PrjItemType" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:DropDownList>
                    
                </td>
                <td class="t_r t_bg">
                    工程所属地：
                </td>
                <td colspan="1">
                    <input type="hidden" runat="server" ID="t_PrjAddressDept" value="" />
                    <uc1:govdeptid1 ID="PrjGovdeptid" runat="server" />                   
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="77%" Enabled="false"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_ConstrScale" runat="server" CssClass="m_txt" Width="77%" Height="40px" TextMode="MultiLine"  Enabled="false"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    报建时间：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ProjectTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    申报时间：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="t_ReportTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    结构类型：
                </td>
                <td >
                    <asp:DropDownList ID="t_ConstrType" runat="server" CssClass="m_txt" Width="200px" Enabled="false">
                    </asp:DropDownList>

                </td>
                <td class="t_r t_bg">
                    合同价格（万元）：
                </td>
                <td >
                    <asp:TextBox ID="t_Price" runat="server" CssClass="m_txt" Width="100px" Enabled="false"></asp:TextBox>币种：<asp:DropDownList ID="t_Currency" runat="server" CssClass="m_txt" Width="103px" Enabled="false"></asp:DropDownList>
                </td>
            </tr>
            
            
            <tr>
                <td class="t_r t_bg">
                    合同开工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_StartDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    合同竣工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_EndDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>

                </td>
            </tr> 
            <tr>
                <td class="t_r t_bg">
                    备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_Remark" runat="server" CssClass="m_txt" Width="95%" Height="40px" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                </td>
               
            </tr>           
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="4">
                    延期申请
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg">
                    施工许可证编号：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="p_SGXKZBH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width:18.8%;">
                    发证机关：
                </td>
                <td colspan="1" style="width:29%;">
                    <asp:TextBox ID="p_FZJG" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    
                </td>
                <td class="t_r t_bg">
                    发证日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="p_FZTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    上次延期申请日期：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="p_LastYQTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    本次延期开始日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="p_YQStartTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    本次延期截止日期：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="p_YQEndTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    本次申请人：
                </td>
                <td >
                    <asp:TextBox ID="p_YQSQR" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    本次申请日期：
                </td>
                <td >
                    <asp:TextBox ID="p_YQTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    本次延期原因：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="p_YQYY" runat="server" CssClass="m_txt" Width="95%" Height="40px" TextMode="MultiLine"></asp:TextBox>
                </td>
               
            </tr>  
            <tr>
                <td class="t_r t_bg">
                    备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="p_Remarks" runat="server" CssClass="m_txt" Width="95%" Height="40px" TextMode="MultiLine"></asp:TextBox>
                </td>
               
            </tr>             
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    延期记录
                </td>
                <td class="t_r">
                    
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
                <asp:BoundColumn HeaderText="延期次数" DataField="YQCS">
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="本次到期日期" DataField="YQEndTime" DataFormatString="{0:d}">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="发证机关" DataField="FZJG">
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="发证日期" DataField="FZTime" DataFormatString="{0:d}">
                    <ItemStyle Wrap="False"/>
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

