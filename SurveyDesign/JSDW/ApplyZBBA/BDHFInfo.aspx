<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BDHFInfo.aspx.cs" Inherits="JSDW_ApplyZBBA_BDHFInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>标段划分信息</title>
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
            var fid = document.getElementById("txtFId").value;;
            if (fid == null || fid == '') {
                alert('请先保存上方的标段划分信息！');
                return;
            }
            showAddWindow('BDInfo.aspx?BDHFBAId=' + fid, 800, 550);
            //  alert('dd')
        }
        
        function showTa() {
            $("#dg_list").show();
            $("#table1").show();
            $("#Pager1").show();
        }
        function hideTa() {
            $("#dg_list").hide();
            $("#table1").hide();
            $("#Pager1").hide();
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
        <div style="height:100%;width:100%;">
            
        <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                标段划分
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
                    
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" width="12%">
                    招标编码：
                </td>
                <td colspan="1" width="45%">
                    <asp:TextBox ID="t_ZBBM" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg" width="14%">
                    项目名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" width="12%">
                    招标代理机构负责人：
                </td>
                <td colspan="1" width="45%">
                    <asp:TextBox ID="t_ZBDLJGFZR" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg" width="14%">
                    联系电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_LXDH" runat="server" CssClass="m_txt" Width="195px" onblur="isTel(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    招标备案名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_ZBBAMC" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    招标类别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZBLB" runat="server" Width="200px" CssClass="m_txt">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    资格预审方式：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZGYSFS" runat="server" CssClass="m_txt" Width="200px">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    设计中标通知书号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_SJZBTZSH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    监理中标通知书号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JLZBTZSH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    招标范围：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZBFW" runat="server" Width="200px" CssClass="m_txt">
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    招标计价方式：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZBJJFS" runat="server" CssClass="m_txt" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    邀请投标申请人资质等级：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_YQTBSQRZZDJ" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,2000,'邀请投标申请人资质等级');"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    计划招标开始日期：
                </td>
                <td>
                    <asp:TextBox ID="t_JHZBStartTime" runat="server" onfocus="WdatePicker()" CssClass="m_txt" MaxLength="20" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    计划招标结束日期：
                </td>
                <td>
                    <asp:TextBox ID="t_JHZBEndTime" runat="server" onfocus="WdatePicker()" CssClass="m_txt" MaxLength="20" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    施工现场情况：
                </td>
                <td>
                    <asp:TextBox ID="t_SGXCQK" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    场地情况：
                </td>
                <td>
                    <asp:TextBox ID="t_CDQK" runat="server" CssClass="m_txt" MaxLength="20" Width="195px"
                        ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    进场交易：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_JCJY" runat="server" CssClass="m_txt" Width="200px">
                        <asp:ListItem Value="0">否</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    是否划分标段：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_HFBD" runat="server" CssClass="m_txt" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="t_HFBD_SelectedIndexChanged">
                        <asp:ListItem Value="0">否</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>       
            <tr>
                <td class="t_r t_bg">
                    招标人提供的主要设备或材料：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_ZYSB" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,2000,'招标人提供的主要设备或材料');"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table id="table1" width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    标段信息
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
                <asp:BoundColumn HeaderText="标段编码" DataField="BDBM">
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="标段名称" DataField="BDMC">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="标段说明" DataField="BDSM">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="建造师等级" DataField="ZBBM">
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
