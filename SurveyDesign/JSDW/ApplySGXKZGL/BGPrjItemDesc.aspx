<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BGPrjItemDesc.aspx.cs" Inherits="JSDW_ApplySGXKZGL_BGPrjItemDesc" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptidfalse2.ascx" TagName="govdeptidfalse" TagPrefix="uc2" %>
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
        <asp:HiddenField  runat="server" ID="h_FAppId" Value="" />
        <asp:HiddenField  runat="server" ID="h_FPrjItemId" Value="" />
        <asp:HiddenField  runat="server" ID="h_FPrjInfoId" Value="" />
        <asp:HiddenField  runat="server" ID="h_FId" Value="" />
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
                <td class="t_l t_bg" colspan="4">
                    建设单位基本信息变更前
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="b_JSDW" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width:18.8%;">
                    建设单位所属地：
                </td>
                <td colspan="1" style="width:29%;">
                    <input type="hidden"  runat="server" ID="b_JSDWAddressDept" value="" />
                    <uc2:govdeptidfalse ID="b_JSDW_DeptID" runat="server" Enabled="false"/>
                </td>
                <td class="t_r t_bg">
                    建设单位地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="b_JSDWDZ" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    所有制性质：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="b_JSDWXZ" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    法定代表人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="b_FDDBR"  runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    法人电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="b_FRDH" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    领证人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="b_LZR" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td >
                    <asp:TextBox ID="b_LXDH" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>           
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="4">
                    工程基本信息变更前
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg"  style="width:18.8%;">
                    项目名称：
                </td>
                <td style="width:29%;">
                    <asp:TextBox ID="b_ProjectName" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    工程名称：
                </td>
                <td >
                    <asp:TextBox ID="b_PrjItemName" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                
                <td class="t_r t_bg">
                    工程所属地：
                </td>
                <td colspan="3">
                    <input type="hidden" runat="server" ID="b_PrjAddressDept" value="" />
                    <uc2:govdeptidfalse ID="b_PrjGovdeptid" runat="server" Enabled="false" />                   
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="b_Address" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="b_ConstrScale"  runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程类别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="b_PrjItemType" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    结构类型：
                </td>
                <td >
                    <asp:DropDownList ID="b_ConstrType" runat="server" CssClass="m_txt" Width="200px" Enabled="false">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    合同价格（万元）：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="b_Price" runat="server" CssClass="m_txt" Width="100px" Enabled="false"></asp:TextBox>币种：<asp:DropDownList ID="b_Currency" runat="server" CssClass="m_txt" Width="103px" Enabled="false"></asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td class="t_r t_bg">
                    合同开工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="b_StartDate" runat="server" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    合同竣工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="b_EndDate" runat="server" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>       
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="4">
                    建设单位基本信息变更后
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_JSDW" Value="" />
                    <asp:HiddenField  runat="server" ID="n_JSDW" Value="建设单位" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width:18.8%;">
                    建设单位所属地：
                </td>
                <td colspan="1" style="width:29%;">
                    <input type="hidden"  runat="server" ID="t_JSDWAddressDept" value="" />
                    <uc1:govdeptid ID="JSDW_DeptID" runat="server" />
                    <asp:HiddenField  runat="server" ID="hf_JSDWAddressDept" Value="" />
                    <asp:HiddenField  runat="server" ID="n_JSDWAddressDept" Value="建设单位所属地" />
                </td>
                <td class="t_r t_bg">
                    建设单位地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDWDZ" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_JSDWDZ" Value="" />
                    <asp:HiddenField  runat="server" ID="n_JSDWDZ" Value="建设单位地址" />
                    </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    所有制性质：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_JSDWXZ" runat="server" CssClass="m_txt" Width="200px" ></asp:DropDownList>
                    <asp:HiddenField  runat="server" ID="hf_JSDWXZ" Value="" />
                    <asp:HiddenField  runat="server" ID="n_JSDWXZ" Value="所有制性质" />
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    法定代表人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FDDBR"  runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_FDDBR" Value="" />
                    <asp:HiddenField  runat="server" ID="n_FDDBR" Value="法定代表人" />
                </td>
                <td class="t_r t_bg">
                    法人电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FRDH" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_FRDH" Value="" />
                    <asp:HiddenField  runat="server" ID="n_FRDH" Value="法人电话" />
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    领证人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_LZR" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_LZR" Value="" />
                    <asp:HiddenField  runat="server" ID="n_LZR" Value="领证人" />
                </td>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td >
                    <asp:TextBox ID="t_LXDH" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_LXDH" Value="" />
                    <asp:HiddenField  runat="server" ID="n_LXDH" Value="联系电话" />
                </td>
            </tr>           
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="4">
                    工程基本信息变更后
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg"  style="width:18.8%;">
                    项目名称：
                </td>
                <td style="width:29%;">
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_ProjectName" Value="" />
                    <asp:HiddenField  runat="server" ID="n_ProjectName" Value="项目名称" />
                </td>
                <td class="t_r t_bg">
                    工程名称：
                </td>
                <td >
                    <asp:TextBox ID="t_PrjItemName" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_PrjItemName" Value="" />
                    <asp:HiddenField  runat="server" ID="n_PrjItemName" Value="工程名称" />
                </td>
            </tr>
            <tr>
                
                <td class="t_r t_bg">
                    工程所属地：
                </td>
                <td colspan="3">
                    <input type="hidden" runat="server" ID="t_PrjAddressDept" value="" />
                    <uc1:govdeptid ID="PrjGovdeptid" runat="server" />                   
                    <asp:HiddenField  runat="server" ID="hf_PrjAddressDept" Value="" />
                    <asp:HiddenField  runat="server" ID="n_PrjAddressDept" Value="工程所属地" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_Address" Value="" />
                    <asp:HiddenField  runat="server" ID="n_Address" Value="建设地址" />
                </td>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td colspan="1">
                    <asp:TextBox  ID="t_ConstrScale"  runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_ConstrScale" Value="" />
                    <asp:HiddenField  runat="server" ID="n_ConstrScale" Value="建设规模" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程类别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_PrjItemType" runat="server" CssClass="m_txt" Width="200px"></asp:DropDownList>
                    <asp:HiddenField  runat="server" ID="hf_PrjItemType" Value="" />
                    <asp:HiddenField  runat="server" ID="n_PrjItemType" Value="工程类别" />
                </td>
                <td class="t_r t_bg">
                    结构类型：
                </td>
                <td >
                    <asp:DropDownList ID="t_ConstrType" runat="server" CssClass="m_txt" Width="200px">
                    </asp:DropDownList>
                    <asp:HiddenField  runat="server" ID="hf_ConstrType" Value="" />
                    <asp:HiddenField  runat="server" ID="n_ConstrType" Value="结构类型" />
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    合同价格（万元）：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_Price" runat="server" CssClass="m_txt" Width="100px" ></asp:TextBox>币种：<asp:DropDownList ID="t_Currency" runat="server" CssClass="m_txt" Width="103px"></asp:DropDownList>
                    <asp:HiddenField  runat="server" ID="hf_Price" Value="" />
                    <asp:HiddenField  runat="server" ID="n_Price" Value="合同价格" />
                    <asp:HiddenField  runat="server" ID="hf_Currency" Value="" />
                    <asp:HiddenField  runat="server" ID="n_Currency" Value="币种" />
                </td>
   
            </tr>
            
            <tr>
                <td class="t_r t_bg">
                    合同开工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_StartDate" runat="server" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" CssClass="m_txt" Width="200px" MaxLength="40"></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_StartDate" Value="" />
                    <asp:HiddenField  runat="server" ID="n_StartDate" Value="合同开工日期" />
                </td>
                <td class="t_r t_bg">
                    合同竣工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_EndDate" runat="server" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" CssClass="m_txt" Width="200px" MaxLength="40"></asp:TextBox>
                    <asp:HiddenField  runat="server" ID="hf_EndDate" Value="" />
                    <asp:HiddenField  runat="server" ID="n_EndDate" Value="合同竣工日期" />
                </td>
            </tr>       
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="4">
                    变更信息
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg"  style="width:18.8%;">
                    变更申请人：
                </td>
                <td style="width:29%;">
                    <asp:TextBox ID="t_BGSQR" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    变更申请时间：
                </td>
                <td >
                    <asp:TextBox ID="t_BGTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
        </table>
        <%--<div id="bginfo" style="visibility:hidden">   <!--变更信息企业不查看，审批企业才查看-->--%>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    项目基本信息变更结果
                </td>
                <td class="t_r">
                    
                    <asp:Button ID="Button6" cs="cs1" runat="server" Text="显示本次修改" CssClass="m_btn_w6" OnClick="btnReload_Click" />
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
                <asp:BoundColumn HeaderText="本次变更内容" DataField="BGNR">
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="变更前" DataField="BeforeBG">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="变更后" DataField="AfterBG">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="变更日期" DataField="BGTime" DataFormatString="{0:d}">
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
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    企业变更信息<label runat="server" id="lblQY"></label> 
                </td>
                <td class="t_r">
                    
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_ListQY" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBoundQY" Style="margin-top: 6px;
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
                <asp:BoundColumn HeaderText="企业类型" DataField="YQLX">
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="企业名称" DataField="YQMC">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="变更情况" DataField="BGQK">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="变更日期" DataField="BGTime" DataFormatString="{0:d}">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <div style="padding-left: 1%">
            <webdiyer:AspNetPager ID="PagerQY" runat="server" AlwaysShow="True" CssClass="pages"
                CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChangingQY"
                pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
            </webdiyer:AspNetPager>
        </div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    人员变更信息<label runat="server" id="lblRY"></label> 
                </td>
                <td class="t_r">
                    
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_ListRY" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBoundRY" Style="margin-top: 6px;
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
                <asp:BoundColumn HeaderText="人员类型" DataField="RYLX">
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="姓名" DataField="XM">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="变更情况" DataField="BGQK">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="变更日期" DataField="BGTime" DataFormatString="{0:d}">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <div style="padding-left: 1%">
            <webdiyer:AspNetPager ID="PagerRY" runat="server" AlwaysShow="True" CssClass="pages"
                CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChangingRY"
                pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
            </webdiyer:AspNetPager>
        </div>
    </div>
    <%--</div>--%>
    </form>
</body>
</html>
