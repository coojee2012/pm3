<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPrjRegist.aspx.cs" Inherits="JSDW_appmain_AddPrjRegist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目登记情况</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            $("#t_FType").change(function() {
                showTr();
            });
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function showTr() {
            var t = $("#t_FType option:selected").val();
            if (t == "2000101") {
                $("tr[name=tr_t1]").show();
                $("tr[name=tr_t2]").hide();
            }
            else {
                $("tr[name=tr_t2]").show();
                $("tr[name=tr_t1]").hide();
            }
        }
        function addPrjItem() {
            var fid = '<%=ViewState["FID"] %>';
            if (fid == null || fid == '') {
                alert('请先保存上方的工程项目信息！');
                return;
            }
            showAddWindow('AddPrjItem.aspx?fprjId=' + fid, 800, 550);
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
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
                <asp:DropDownList ID="ddlHis" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHis_SelectedIndexChanged"
                    TabIndex="10">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center" id="tabSetup1" runat="server">
        <tr>
            <td class="t_r t_bg">
                请选择工程类别然后点下一步
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <asp:Button ID="btnNext" runat="server" Text="下一步" OnClick="btnNext_Click" CssClass="m_btn_w2" />
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
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                        OnClientClick="return checkInfo();" />
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" width="12%">
                    工程名称：
                </td>
                <td colspan="1" width="45%">
                    <asp:TextBox ID="t_FPrjName" runat="server" CssClass="m_txt" Width="250px" MaxLength="40"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg" width="14%">
                    工程编号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FPrjNo" runat="server" CssClass="m_txt" Enabled="false" ToolTip="系统自动生成"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程地点：
                </td>
                <td colspan="3">
                    <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                    <asp:TextBox ID="t_FAllAddress" runat="server" CssClass="m_txt" Width="224px" MaxLength="30"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="224px" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    建设单位地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDWDZ" runat="server" CssClass="m_txt" Width="224px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    备案部门：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FManageDeptId" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    工程类别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FType" runat="server" Enabled="false" CssClass="m_txt">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程等级：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FLevel" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    结构类型：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FStruType" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设模式：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_JSMS" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    工程概算：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FAllMoney" onblur="isFloat(this)" runat="server" CssClass="m_txt t_r"
                        MaxLength="20" Width="70"></asp:TextBox>(万元) <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    资金来源：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_FFunds" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设性质：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FKind" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    设计规模：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FScale" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建筑使用性质：
                </td>
                <td colspan="1">
                    <asp:CheckBoxList ID="t_FNature" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        RepeatColumns="8">
                    </asp:CheckBoxList>
                </td>
                <td class="t_r t_bg">
                    抗震设防分类标准：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FAntiSeismic" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    地震基本烈度：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FEarthquake" runat="server" CssClass="m_txt t_r" MaxLength="30"
                        Width="70px" onblue="isFloat(this)"></asp:TextBox>(度) <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    抗震设防烈度：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FIntensity" runat="server" CssClass="m_txt t_r" MaxLength="30"
                        Width="70px" onblue="isFloat(this)"></asp:TextBox>(度) <tt>*</tt>
                </td>
            </tr>
            <tr name="tr_t1">
                <td class="t_r t_bg">
                    总用地面积：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FLandArea" runat="server" CssClass="m_txt t_r" onblur="isFloat(this)"
                        Width="70px" MaxLength="8"></asp:TextBox>(㎡)
                </td>
                <td class="t_r t_bg">
                    总建筑面积：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FArea" runat="server" CssClass="m_txt t_r" onblur="isFloat(this)"
                        Width="70px" MaxLength="8"></asp:TextBox>(㎡)
                </td>
            </tr>
            <tr name="tr_t1">
                <td class="t_r t_bg">
                    建筑高度：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FHeight" runat="server" CssClass="m_txt t_r" onblur="isFloat(this)"
                        Width="70px" MaxLength="10"></asp:TextBox>(m)
                </td>
                <td class="t_r t_bg">
                    栋数：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FSize" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"
                        MaxLength="10"></asp:TextBox>(栋)
                </td>
            </tr>
            <tr name="tr_t1">
                <td class="t_r t_bg">
                    层数：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FLayers" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"
                        MaxLength="9"></asp:TextBox>(层) 其中地上：
                    <asp:TextBox ID="t_FGround" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"
                        MaxLength="9"></asp:TextBox>(层) 地下：
                    <asp:TextBox ID="t_FUnderground" runat="server" CssClass="m_txt t_r" Width="70px"
                        onblur="isInt(this)" MaxLength="9"></asp:TextBox>(层)
                </td>
            </tr>
            <tr name="tr_t1">
                <%-- <td class="t_r t_bg"><!--高忠宝-->
                    可规划用地面积：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_KGHYDMJ" runat="server" CssClass="m_txt t_r" onblur="isFloat(this)"
                        Width="70px" MaxLength="10"></asp:TextBox>(㎡)
                </td>--%>
                <td class="t_r t_bg">
                    容积率：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_RJL" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isFloat(this)"
                        MaxLength="10"></asp:TextBox>(%)
                </td>
            </tr>
            <tr name="tr_t1">
                <td class="t_r t_bg">
                    建筑密度：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JZMD" runat="server" CssClass="m_txt t_r" onblur="isFloat(this)"
                        Width="70px" MaxLength="10"></asp:TextBox>(㎡)
                </td>
                <td class="t_r t_bg">
                    绿地率：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_LDL" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isFloat(this)"
                        MaxLength="10"></asp:TextBox>(%)
                </td>
            </tr>
            <tr name="tr_t1">
                <td class="t_r t_bg">
                    地上总面积：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_DSZMJ" runat="server" CssClass="m_txt t_r" onblur="isFloat(this)"
                        Width="70px" MaxLength="10"></asp:TextBox>(㎡)
                </td>
                <td class="t_r t_bg">
                    地下总面积：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_DXZMJ" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isFloat(this)"
                        MaxLength="10"></asp:TextBox>(㎡)
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    岩土工程等级：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FYTLevel" runat="server" CssClass="m_txt t_r" Width="70px" MaxLength="10"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    场地等级：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FCDLevel" runat="server" CssClass="m_txt t_r" Width="70px" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    地基等级：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FDJLevel" runat="server" CssClass="m_txt t_r" Width="70px" MaxLength="10"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    建筑（或市政）工程等级：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FOtherLevel" runat="server" CssClass="m_txt t_r" Width="70px"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    岩土工程类别：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FYTGCType" runat="server" CssClass="m_txt t_r" Width="200px" MaxLength="20"></asp:TextBox>
            </tr>
            <tr name="tr_t1">
                <td class="t_r t_bg">
                    停车位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_TCW" runat="server" CssClass="m_txt t_r" onblur="isInt(this)"
                        Width="70px" MaxLength="10"></asp:TextBox>(个)
                </td>
            </tr>
            <tr name="tr_t2" style="display: none">
                <td class="t_r t_bg">
                    市政行业类别：
                </td>
                <td colspan="3">
                    <asp:CheckBoxList ID="t_FSectors" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        RepeatColumns="8">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr name="tr_t2" style="display: none">
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_JSGM" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,1000,'建设规模');"></asp:TextBox>
                </td>
            </tr>
            <tr name="tr_t2" style="display: none">
                <td class="t_r t_bg">
                    其他：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_FOther" runat="server" CssClass="m_txt" Width="510px" onblur="checkLength(this,100,'其他');"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td>
                    <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt" MaxLength="10" Width="90px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    联系人手机：
                </td>
                <td>
                    <asp:TextBox ID="t_FMobile" runat="server" CssClass="m_txt" MaxLength="20" Width="90px"
                        onblur="isTel(this);"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    设计采用新技术新材料新工艺的名称、方法：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_FRemark" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,1000,'设计采用新技术新材料新工艺的名称、方法');"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设内容：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_JSNR" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,1000,'建设内容');"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程概况：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_FGCGK" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,1000,'工程概况');"></asp:TextBox>
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
                <asp:BoundColumn HeaderText="工程名称" DataField="FPrjItemName">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="工程设计等级" DataField="FDJ">
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
    <input id="t_FAddressDept" type="hidden" runat="server" />
    </form>
</body>

<script type="text/javascript">
    function changeCheck(obj) {
        obj.style.background = obj.checked ? '#1eaffc' : "";
    }
    $.each($(":checkbox[id^=t_F]"), function() {
        $(this).click(function() { changeCheck(this); });
        changeCheck(this);
    }); 
</script>

</html>
