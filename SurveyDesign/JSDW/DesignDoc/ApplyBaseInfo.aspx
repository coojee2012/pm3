<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="KcsjSgt_ApplyKCWJBA_ApplyBaseInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function checkInfo() {
            if (!AutoCheckInfo()) {
                return false;
            }
            if (!getLength(document.getElementById("t_FTxt10"), 100, '“合同备案内容及范围”')) {
                return false;
            }
            if (!getLength(document.getElementById("t_FTxt11"), 500, '“合同备案承诺和要求”')) {
                return false;
            }
            return true;
        }

        function showTr() {
            var t = $("#t_FType").val();
            if (t == "2000101") {
                $("tr[name=tr_t1]").show();
                $("tr[name=tr_t2]").hide();
            }
            else {
                $("tr[name=tr_t2]").show();
                $("tr[name=tr_t1]").hide();
            }
        }
        function selEnt(tagId, fsysid, obj, hasCerti) {
            var url = "EntListSel.aspx";
            if (hasCerti == "1")
                url = "EntListSelCerti.aspx";
            var pid = showWinByReturn('../appmain/' + url + '?fsysid=' + fsysid, 700, 500);
            if (pid != null && pid != '') {
                var ps = pid.split('|');
                if (ps.length > 0)
                    $("#" + tagId).val(ps[0]);
                if (ps.length > 1)
                    $("#" + tagId + "_c").val(ps[1]);
                __doPostBack(obj.id, '');
            }
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                基本信息
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" width="15%">
                工程名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FPrjName" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
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
                工程概算：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FAllMoney" onblur="isFloat(this)" runat="server" CssClass="m_txt"></asp:TextBox>(万元)
                <tt>*</tt>
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
                <asp:TextBox ID="t_FEarthquake" runat="server" CssClass="m_txt" MaxLength="30" Width="200px"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                抗震设防烈度：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FIntensity" runat="server" CssClass="m_txt" MaxLength="30" Width="200px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr name="tr_t1">
            <td class="t_r t_bg">
                总用地面积：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FLandArea" runat="server" CssClass="m_txt" onblur="isFloat(this)"
                    Width="170px"></asp:TextBox>(㎡)
            </td>
            <td class="t_r t_bg">
                总建筑面积：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FArea" runat="server" CssClass="m_txt" onblur="isFloat(this)"
                    Width="170px"></asp:TextBox>(㎡)
            </td>
        </tr>
        <tr name="tr_t1">
            <td class="t_r t_bg">
                建筑高度：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FHeight" runat="server" CssClass="m_txt" onblur="isFloat(this)"
                    Width="200px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                栋数：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FSize" runat="server" CssClass="m_txt" Width="200px" onblur="isInt(this)"></asp:TextBox>
            </td>
        </tr>
        <tr name="tr_t1">
            <td class="t_r t_bg">
                层数：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FLayers" runat="server" CssClass="m_txt" Width="60px" onblur="isInt(this)"
                    MaxLength="3"></asp:TextBox>
                层 &nbsp; 地上：
                <asp:TextBox ID="t_FGround" runat="server" CssClass="m_txt" Width="60px" onblur="isInt(this)"
                    MaxLength="3"></asp:TextBox>
                层&nbsp;&nbsp; 地下：
                <asp:TextBox ID="t_FUnderground" runat="server" CssClass="m_txt" Width="60px" onblur="isInt(this)"
                    MaxLength="3"></asp:TextBox>
                层
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
        <tr>
            <td class="t_r t_bg">
                设计采用新技术新材料新工艺的名称、方法：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FRemark" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                    Width="539px"></asp:TextBox>
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
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
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
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="4" class="t_bg" style="padding-left: 20px; color: Red; border-right: none;">
                建设单位信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                单位名称：
            </td>
            <td>
                <asp:TextBox ID="e_FName" runat="server" CssClass="m_txt" Width="300px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg" width="150">
                法人：
            </td>
            <td>
                <asp:TextBox ID="e_FOTxt5" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="150">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="e_FLinkMan" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg" width="150">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="e_FTel" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="4" class="t_bg" style="padding-left: 20px; color: Red; border-right: none;">
                设计单位信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" align="right">
                是否联合体
            </td>
            <td colspan="3">
                <asp:CheckBox runat="server" ID="t_FFloat10" />
            </td>
        </tr>
        <tr class="t_l t_bg" id="mainSJ" style="display: none;" runat="server">
            <td colspan="4">
                主要设计单位
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                单位名称：
            </td>
            <td>
                <asp:TextBox ID="s_FName" runat="server" CssClass="m_txt" Width="300px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg" width="150">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="s_FLevelName" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="150">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="semp_FName" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg" width="150">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="semp_FTel" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                手机：
            </td>
            <td>
                <asp:TextBox ID="semp_FCall" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                E-Mail：
            </td>
            <td>
                <asp:TextBox ID="semp_FEmail" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table runat="server" class="m_table" width="98%" align="center" id="otherSJ" style="display: none;">
        <tr>
            <td class="t_l t_bg" colspan="2">
                其他设计单位列表
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <table style="width: 100%">
                    <tr class="t_c t_bg">
                        <td>
                            序号
                        </td>
                        <td>
                            企业名称
                        </td>
                        <td>
                            操作
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="repeaterDisplay" OnItemDataBound="repeaterDisplay_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="lit_NO"></asp:Literal>
                                </td>
                                <td align="left">
                                    <%#Eval("FName") %>
                                </td>
                                <td>
                                    --
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="4" class="t_bg" style="padding-left: 20px; color: Red; border-right: none;">
                勘察单位信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" align="right">
                是否联合体
            </td>
            <td colspan="3">
                <asp:CheckBox runat="server" ID="t_FFloat11" />
            </td>
        </tr>
        <tr class="t_l t_bg" id="mainKC" runat="server" style="display: none;">
            <td colspan="4">
                主要勘察单位
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="150">
                单位名称：
            </td>
            <td>
                <asp:TextBox ID="k_FName" runat="server" CssClass="m_txt" Width="300px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg" width="150">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="k_FLevelName" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="k_FLinkMan" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="k_FTel" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                手机：
            </td>
            <td>
                <asp:TextBox ID="k_FCall" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                E-Mail：
            </td>
            <td>
                <asp:TextBox ID="k_FEmail" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table runat="server" class="m_table" width="98%" align="center" id="otherKC" style="display: none;">
        <tr>
            <td class="t_l t_bg" colspan="2">
                其他勘察单位列表
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <table style="width: 100%">
                    <tr class="t_c t_bg">
                        <td>
                            序号
                        </td>
                        <td>
                            企业名称
                        </td>
                        <td>
                            操作
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="repeaterDisplay_KC" OnItemDataBound="repeaterDisplay_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="lit_NO"></asp:Literal>
                                </td>
                                <td align="left">
                                    <%#Eval("FName") %>
                                </td>
                                <td>
                                    --
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
    <input id="t_FAddressDept" type="hidden" runat="server" />
    </form>
</body>
</html>
