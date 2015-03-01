<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZBJGList.aspx.cs" Inherits="JSDW_ApplySGXKZGL_ZBJGList" %>

<%@ Register TagPrefix="uc1" TagName="pager" Src="~/Common/pager.ascx" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();

        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function addPrjItemKC() {
            var fid = document.getElementById("txtKCId").value;
            var fAppId = '<%=ViewState["FAppId"] %>';
            var fPrjItemId = '<%=ViewState["FPrjItemId"] %>';
            if (fid == null || fid == '') {
                alert('请先保存上方信息！');
                return;
            }
            showAddWindow('File.aspx?fLinkId=' + fid + "&&fAppId=" + fAppId + "&&fPrjItemId=" + fPrjItemId, 800, 550);
            //  alert('dd')
        }
        function addPrjItemSJ() {
            var fid = document.getElementById("txtSJId").value;
            var fAppId = '<%=ViewState["FAppId"] %>';
            var fPrjItemId = '<%=ViewState["FPrjItemId"] %>';
            if (fid == null || fid == '') {
                alert('请先保存上方信息！');
                return;
            }
            showAddWindow('File.aspx?fLinkId=' + fid + "&&fAppId=" + fAppId + "&&fPrjItemId=" + fPrjItemId, 800, 550);
            //  alert('dd')
        }
        function addPrjItemJL() {
            var fid = document.getElementById("txtJLId").value;
            var fAppId = '<%=ViewState["FAppId"] %>';
            var fPrjItemId = '<%=ViewState["FPrjItemId"] %>';
            if (fid == null || fid == '') {
                alert('请先保存上方信息！');
                return;
            }
            showAddWindow('File.aspx?fLinkId=' + fid + "&&fAppId=" + fAppId + "&&fPrjItemId=" + fPrjItemId, 800, 550);
            //  alert('dd')
        }
        function addPrjItemSG() {
            var fid = document.getElementById("txtSGId").value;
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

    <style type="text/css">
        .style1 { text-align: left; height: 31px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                项目环节材料
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                    OnClientClick="return checkInfo();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table id="table1" class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>招投标信息</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                项目名称：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="195px" ReadOnly="true"></asp:TextBox>
                <input id="txtFId" type="hidden" runat="server" />
            </td>
            <td class="t_r t_bg">
                工程名称： </td>
            <td>
                <asp:TextBox ID="t_PrjItemName" runat="server" CssClass="m_txt" Width="195px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr > 
            <td class="t_r t_bg">
                建设单位： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="195px" ReadOnly="true"></asp:TextBox>
            </td>  
            <td class="t_r t_bg">
                总面积
            </td>
            <td colspan="1">
                    <asp:TextBox ID="t_Area" runat="server" CssClass="m_txt" ReadOnly="true"
                        Width="195px"></asp:TextBox>（m2）
            </td>    
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                项目编号：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_ProjectNo" runat="server" CssClass="m_txt" Width="195px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                中标通知书编号： </td>
            <td>
                <asp:TextBox ID="t_ZBTZSBH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设规模：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_ConstrScale" runat="server" CssClass="m_txt" Width="638px" Height="40px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                招标代理单位名称：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_ZBDLDWMC" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td>
            <td class="t_r t_bg">
                招标代理单位组织机构代码： </td>
            <td>
                <asp:TextBox ID="t_ZBDLDWZZJGDM" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td>
        </tr>

        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>监理单位招投标信息</h3>
                <input id="txtJLId" type="hidden" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                招标方式： </td>
            <td colspan="1">
                <asp:DropDownList ID="t_JLZBFS" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList><tt>*</tt>
            </td> 
            <td class="t_r t_bg">
                招标类型： </td>
            <td colspan="1">
                <asp:DropDownList ID="t_JLZBLX" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList><tt>*</tt>
            </td> 
            <!--<td class="t_r t_bg">
                组织方式： </td>
            <td colspan="1">
                <asp:DropDownList ID="t_JLZZFS" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>  -->
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标单位：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_JLZBDW" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td>
            <td class="t_r t_bg">
                中标单位组织机构代码： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLZBDWZZJGDM" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td> 
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标企业资质等级：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_JLZBQYZZDJ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                中标企业资质证书号： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLZBQYZZZSH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>   
        </tr>
        <!--<tr>
            <td class="t_r t_bg" style="width:18.8%;">
                质量标准：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JLZLBZ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>  
        </tr>-->
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标金额：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_JLZBJ" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（万元）
                <tt>*</tt>
            </td> 
            <!--<td class="t_r t_bg">
                大写： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLZBJDX" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>   -->
            <td class="t_r t_bg">
                中标日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLZBRQ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
            <!--<tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标工期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JLZBGQ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（天）
            </td>  
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同开工日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLHTKGTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                合同竣工日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLHTJGTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>  -->
        <tr>
            <!--<td class="t_r t_bg">
                合同签订日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLHTQDTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>-->
            <td class="t_r t_bg">
                合同备案日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLHTBATime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                总监理工程师姓名：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JLGCS" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td>  
        </tr> 
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                总监理工程师证件类型：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:DropDownList ID="t_JLGCSZJLX" runat="server" CssClass="m_txt" Width="195px"></asp:DropDownList><tt>*</tt>
            </td> 
            <td class="t_r t_bg">
                总监理工程师证件号码： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLGCSZJHM" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td>   
        </tr>
            <tr>
            
        </tr>
    </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    监理材料信息
                </td>
                <td class="t_r">
                    <input type="button" id="Button4" runat="server" value="新增" class="m_btn_w2" onclick="addPrjItemJL();" />
                    <asp:Button ID="Button5" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                        OnClick="btnDel_ClickJL" />
                    <asp:Button ID="Button6" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_ClickJL" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_ListJL" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBoundJL" Style="margin-top: 6px;
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
            <webdiyer:AspNetPager ID="PagerJL" runat="server" AlwaysShow="True" CssClass="pages"
                CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager_PageChangingJL"
                pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
            </webdiyer:AspNetPager>
        </div>
        <table id="table4" class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>施工单位招投标信息</h3>
                <input id="txtSGId" type="hidden" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                招标方式： </td>
            <td colspan="1">
                <asp:DropDownList ID="t_SGZBFS" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList><tt>*</tt>
            </td> 
            <td class="t_r t_bg">
                招标类型： </td>
            <td colspan="1">
                <asp:DropDownList ID="t_SGZBLX" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList><tt>*</tt>
            </td> 
            <!--<td class="t_r t_bg">
                组织方式： </td>
            <td colspan="1">
                <asp:DropDownList ID="t_SGZZFS" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>  -->
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标单位：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_SGZBDW" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td>
            <td class="t_r t_bg">
                中标单位组织机构代码： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGZBDWZZJGDM" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td> 
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标企业资质等级：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_SGZBQYZZDJ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                中标企业资质证书号： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGZBQYZZZSH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>   
        </tr>
        <!--<tr>
            <td class="t_r t_bg" style="width:18.8%;">
                质量标准：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGZLBZ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td> 
            <td class="t_r t_bg">
                质量等级： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGZLDJ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>  
        </tr>-->
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标金额：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_SGZBJ" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（万元）<tt>*</tt>
            </td> 
            <!--<td class="t_r t_bg">
                大写： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGZBJDX" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td> -->
            <td class="t_r t_bg">
                中标日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGZBRQ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td>  
        </tr>
        <!--<tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标工期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_SGZBGQ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（天）
            </td>  
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同开工日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGHTKGTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                合同竣工日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGHTJGTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>-->
        <tr>
            <!--<td class="t_r t_bg">
                合同签订日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGHTQDTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>-->
            <td class="t_r t_bg">
                合同备案日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGHTBATime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                项目经理姓名：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGXMJL" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
            </td> 
        </tr> 
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                项目经理证件类型：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:DropDownList ID="t_SGXMJLZJLX" runat="server" CssClass="m_txt" Width="201px"></asp:DropDownList>
            </td> 
            <td class="t_r t_bg">
                项目经理证件号码： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGXMJLZJHM" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>   
        </tr>
        <!--<tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标范围和内容：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_SGZBFW" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                    Width="726px" onblur="checkLength(this,2000,'中标范围和内容');"></asp:TextBox>
            </td> 
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                建筑面积：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_SGJZBM" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m2）
            </td> 
            <td class="t_r t_bg">
                散装水泥用量： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGSZSNYL" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（吨）
            </td>   
        </tr>
        <tr>
            
            <td class="t_r t_bg">
                项目经理证书号码： </td>
            <td colspan="1">
                <asp:TextBox ID="t_SGXMJLZS" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>   
        </tr>-->
    </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    施工材料信息
                </td>
                <td class="t_r">
                    <input type="button" id="Button7" runat="server" value="新增" class="m_btn_w2" onclick="addPrjItemSG();" />
                    <asp:Button ID="Button8" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                        OnClick="btnDel_ClickSG" />
                    <asp:Button ID="Button9" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_ClickSG" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_ListSG" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBoundSG" Style="margin-top: 6px;
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
            <webdiyer:AspNetPager ID="PagerSG" runat="server" AlwaysShow="True" CssClass="pages"
                CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager_PageChangingSG"
                pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
            </webdiyer:AspNetPager>
        </div>
    </form>
</body>
    <script type="text/javascript">
        function changeCheck(obj) {
            obj.style.background = obj.checked ? '#1eaffc' : "";
        }
        $.each($(":checkbox[id^=t_F]"), function () {
            $(this).click(function () { changeCheck(this); });
            changeCheck(this);
        });
</script>
</html>
