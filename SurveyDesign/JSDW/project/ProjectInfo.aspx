<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectInfo.aspx.cs" Inherits="JSDW_project_ProjectInfo" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目登记情况</title>
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
            
            var projectNo = document.getElementById("t_ProjectNo").value;
            if (projectNo == null || projectNo == '') {
                var p = document.getElementById("txtProjectNo").value;
                var t = document.getElementById("t_ProjectType").value;
                var fNo = '';
                if (t == '2000101') {
                    fNo = '01';
                }
                else if (t == '2000102') {
                    fNo = '02';
                }
                else {
                    fNo = '99';
                }
                var n = p.substring(0, 12) + fNo + p.substring(14, 16);
                $("#t_ProjectNo").val(n);
            }
            return AutoCheckInfo();
            
        }
        function addPrjItem() {
            var fid = document.getElementById("txtFId").value;
            if (fid == null || fid == '') {
                alert('请先保存上方的工程项目信息！');
                return;
            }
            showAddWindow('ProjectItemInfo.aspx?fprjId=' + fid, 800, 550);
          //  alert('dd')
        }
        function openLink() {
            var fid = document.getElementById("txtFId").value;
            var projectName = document.getElementById("t_ProjectName").value;
            projectName = encodeURI(projectName, 'utf-8');
            projectName = encodeURI(projectName, 'utf-8');
            if (fid == null || fid == '') {
                alert('请先保存上方的工程项目信息！');
                return;
            }
            var url = document.getElementById("txtUrl").value;
            showAddWindow(url + "projectID=" + fid + "&projectName=" + projectName, 1600, 800);
            
        }
        //确认项目之前向操作者发出提醒。
        function confirmrefresh() {
            if (window.confirm('当前信息确定填写完整无误吗？确认后将不能修改！'))
                return true;
            else
                return false;
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
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
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
        <div style="height:100%;width:100%;">
            
        <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                项目登记情况
            </th>
        </tr>
        <tr runat="server" id="tr_his" visible="false">
            <td class="t_r t_bg" width="15%">
                <tt>历次变更记录：</tt>
            </td>
            <td class="t_l">
                <asp:DropDownList ID="ddlHis" runat="server" AutoPostBack="true"
                    TabIndex="10">
                </asp:DropDownList>
            </td>
        </tr>
    </table> 
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                    OnClientClick="return checkInfo();" />
                            <input id="txtFId" type="hidden" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>    
                            <asp:Button ID="btnRefresh" runat="server" Text="确认"  CssClass="m_btn_w6"
                                onclick="btnRefresh_Click" OnClientClick="return confirmrefresh();" />
                    <input type="button" id="btnLink" runat="server" value="定位" class="m_btn_w2" onclick="openLink();" />              
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" width="12%">
                    项目名称：
                </td>
                <td colspan="1" width="45%">
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg" width="14%">
                    项目编号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ProjectNo" runat="server" CssClass="m_txt" Width="195px" Enabled="false" ToolTip="系统自动生成"></asp:TextBox>
                    <input id="txtProjectNo" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目所属地：
                </td>
                <td colspan="1">
                    <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                </td>
                <td class="t_r t_bg">
                    项目地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="195px" MaxLength="30"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="195px" ReadOnly="true"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    建设单位组织机构代码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDWDM" runat="server" CssClass="m_txt" Width="195px" ReadOnly="true"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDWDZ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    建设单位法人：
                </td>
                <td>
                    <asp:TextBox ID="t_JSDWFR" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td>
                    <asp:TextBox ID="t_Contacts" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    联系人手机：
                </td>
                <td>
                    <asp:TextBox ID="t_Mobile" runat="server" CssClass="m_txt" MaxLength="20" Width="195px"
                        onblur="isTel(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目类别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ProjectType" runat="server" Width="200px" AutoPostBack="true" CssClass="m_txt" OnSelectedIndexChanged="t_ProjectType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    立项级别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ProjectLevel" runat="server" CssClass="m_txt" Width="200px">
                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    立项文号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ProjectNumber" runat="server" CssClass="m_txt" 
                        Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    立项时间：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ProjectTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="200px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设总面积（m2）
                </td>
                <td colspan="1">
                      <asp:TextBox ID="t_Area" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    投资规模：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Investment" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="200px"></asp:TextBox>(万元) <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    用地性质：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_LandUse" runat="server" Width="200px" CssClass="m_txt">
                    </asp:DropDownList>
                </td>
            </tr> 
            <tr>
                <td class="t_r t_bg">
                    工程用途：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ProjectUse" runat="server" Width="200px" CssClass="m_txt">
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    建设性质：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ConstrType" runat="server" Width="200px" CssClass="m_txt">
                    </asp:DropDownList>
                </td>
            </tr> 
            <!--<tr>
                <td class="t_r t_bg">
                    建设用地规划许可证编号：
                </td>
                <td>
                    <asp:TextBox ID="t_JSYDXKZ" runat="server" CssClass="m_txt" MaxLength="10" Width="90px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    建设工程规划许可证编号：
                </td>
                <td>
                    <asp:TextBox ID="t_JSGCXKZ" runat="server" CssClass="m_txt" MaxLength="20" Width="90px"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td class="t_r t_bg">
                    实际开工日期：
                </td>
                <td>
                    <asp:TextBox ID="t_StartDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" MaxLength="20" Width="70px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    实际竣工日期：
                </td>
                <td>
                    <asp:TextBox ID="t_EndDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" MaxLength="20" Width="70px"></asp:TextBox>
                </td>
            </tr>-->  
            <tr>
                <td class="t_r t_bg">
                    是否涉外：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_IsForeign" runat="server" CssClass="m_txt" Width="200px">
                        <asp:ListItem Value="0">否</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    记录登记时间：
                </td>
                <td>
                    <asp:TextBox ID="t_RegisterTime" runat="server" onfocus="WdatePicker()" CssClass="m_txt" MaxLength="20" Width="195px"></asp:TextBox>
                </td>
            </tr>     
            <tr>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_ConstrScale" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,1000,'建设规模');"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>  
            <tr>
                <td class="t_r t_bg">
                    建设依据：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_ConstrBasis" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,1000,'建设依据');"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设内容：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_ConstrContent" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,1000,'建设内容');"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    单体工程信息
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up25" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="addPrjItem();" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
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
                <asp:BoundColumn HeaderText="工程名称" DataField="PrjItemName">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="工程类别" DataField="FDJ">
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
    <input id="t_AddressDept" type="hidden" runat="server" />
    <input id="t_Province" type="hidden" runat="server" />
    <input id="t_City" type="hidden" runat="server" />
    <input id="t_County" type="hidden" runat="server" />
    
    <input id="txtUrl" type="hidden" runat="server" />
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

