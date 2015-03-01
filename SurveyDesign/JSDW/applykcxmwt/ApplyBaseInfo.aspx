<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="JSDW_appmain_ApplyBaseInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合同基本信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            isVisible();
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function selEnt(tagId, fsysid, obj, hasCerti, notTag) {
            var url = "EntListSel.aspx";
            if (hasCerti == "1")
                url = "EntListSelCerti.aspx";
            url += "?fsysid=" + fsysid;
            if ($("#" + notTag).val() != undefined)
                url += "&notBid=" + $("#" + notTag).val();
            var pid = showWinByReturn('../appmain/' + url, 700, 500);
            if (pid != null && pid != '') {
                var ps = pid.split('|');
                if (ps.length > 0)
                    $("#" + tagId).val(ps[0]);
                if (ps.length > 1)
                    $("#" + tagId + "_c").val(ps[1]);
                __doPostBack(obj.id, '');
            }
        }
        function isVisible() {
            var cb = $("#t_FInt3");
            var table = $("#otherKC");
            var tr = $("#mainKC");
            if ($(cb).attr("checked") != undefined) {
                if ($(cb).attr("checked")) {
                    $(table).show();
                    $(tr).show();
                }
                else {
                    $(table).hide();
                    $(tr).hide();
                }
            }
        }
        function doSelOther(tagId, obj, fsysid, hasCerti, oTagId, tip) {
            if ($('#' + oTagId).val() == '') {
                alert(tip);
                return false;
            }
            else {
                return selEnt(tagId, fsysid, obj, '1', oTagId);
            }
        }
        function btnClearL() {
            $("#j_FMoney,#j_FPlanDate").val('');
            $("#j_FBaseInfoId,#j_FId").val('');
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
                合同基本信息
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
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                工程编号：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_FPrjNo" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地点：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="p_FAllAddress" runat="server" CssClass="m_txt" Width="224px" MaxLength="30"
                    Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同提交单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FName" runat="server" CssClass="m_txt" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="e_FLinkMan" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="e_FTel" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                Email：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FAddress" runat="server" CssClass="m_txt" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" width="22%">
                合同内容及范围（100字内）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt10" runat="server" CssClass="m_txt" Width="500px" Height="30px"
                    TextMode="MultiLine"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同承诺和要求（500字内）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt11" runat="server" CssClass="m_txt" Width="500px" Height="80px"
                    TextMode="MultiLine"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr class="t_r t_bg">
            <td class="t_l t_bg" colspan="4">
                勘察单位信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" align="right">
                是否联合体
            </td>
            <td colspan="3">
                <asp:CheckBox runat="server" ID="t_FInt3" onclick="isVisible();" />
            </td>
        </tr>
        <tr class="t_l t_bg" id="mainKC" style="display: none;" runat="server">
            <td colspan="4">
                主要勘察单位
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                单位名称：
            </td>
            <td class="t_l t_bg" colspan="3">
                <asp:TextBox ID="k_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox>
                <tt>*</tt>
                <asp:Button ID="btnMod" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEnt('k_FBaseInfoId',1554,this,'1');"
                    UseSubmitBehavior="false" CommandName="KC" OnClick="btnSel_Click" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="k_FLevelName" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td>
                <asp:TextBox ID="k_FCertiNo" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同金额：
            </td>
            <td>
                <asp:TextBox ID="k_FMoney" runat="server" CssClass="m_txt" onblur="isFloat(this)"></asp:TextBox>(万元) 
            </td>
            <td class="t_r t_bg">
                要求完成日期：
            </td>
            <td>
                <asp:TextBox ID="k_FPlanDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <table runat="server" class="m_table" width="98%" align="center" id="otherKC" style="display: none;">
        <tr>
            <td class="t_l t_bg">
                其他勘察单位列表
            </td>
            <td class="t_r t_bg">
                <input type="hidden" runat="server" id="kc_FBaseInfoId" />
                <input type="hidden" runat="server" id="kc_FBaseInfoId_c" />
                <input type="hidden" runat="server" id="kc_FId" />
                <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="m_btn_w4" OnClientClick="return doSelOther('kc_FBaseInfoId',this,'1554','1','k_FBaseInfoId','请先选择上方的‘主要勘察单位’！');"
                    UseSubmitBehavior="false" OnClick="btnAddKC_Click" />
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
                    <asp:Repeater runat="server" ID="repeaterDisplay" OnItemDataBound="repeaterDisplay_ItemDataBound"
                        OnItemCommand="repeaterDisplay_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="lit_NO"></asp:Literal>
                                </td>
                                <td align="left">
                                    <%#Eval("FName") %>
                                </td>
                                <td>
                                    <%-- <a id="aDelete" href="" onclick=" >删除</a>--%>
                                    <asp:LinkButton runat="server" ID="btnDel" OnClientClick="return confirm('确定要删除吗')"
                                        Text="删除" CommandName="Delete" CommandArgument='<%#Eval("fid") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center" style="display:none">
        <tr>
            <td class="t_l t_bg" colspan="4">
                见证单位信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                单位名称：
            </td>
            <td class="t_l t_bg" colspan="3">
                <asp:TextBox ID="j_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox>
                <asp:Button ID="btnDel" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEnt('j_FBaseInfoId',1261,this);"
                    UseSubmitBehavior="false" CommandName="JZ" OnClick="btnSel_Click" />
                <asp:Button  runat="server"  ID="btnClear" class="m_btn_w4" Text="清空" 
                    onclick="btnClear_Click"  />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同金额：
            </td>
            <td>
                <asp:TextBox ID="j_FMoney" runat="server" CssClass="m_txt" onblur="isFloat(this)"></asp:TextBox>(万元)
            </td>
            <td class="t_r t_bg">
                要求完成日期：
            </td>
            <td>
                <asp:TextBox ID="j_FPlanDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="p_FAddressDept" type="hidden" runat="server" />
    <input id="k_FBaseInfoId" type="hidden" runat="server" />
    <input id="k_FId" type="hidden" runat="server" />
    <input id="k_FBaseInfoId_c" type="hidden" runat="server" />
    <input id="j_FBaseInfoId" type="hidden" runat="server" />
    <input id="j_FId" type="hidden" runat="server" />
    <input id="e_FBaseInfoId" type="hidden" runat="server" />
    <input id="e_FId" type="hidden" runat="server" />
    </form>
</body>
</html>
