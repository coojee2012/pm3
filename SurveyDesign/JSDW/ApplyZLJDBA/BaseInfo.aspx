<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaseInfo.aspx.cs" Inherits="JSDW_ApplyZLJDBA_BaseInfo" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptidfalse.ascx" TagName="govdeptidfalse" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>基本信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            $("#p_PrjItemType").change(function () {
                showTr();
            });
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function showTr() {
            var t = $("#p_PrjItemType option:selected").val();
            if (t == "2000102") {
                $("tr[name=tr_t1]").show();
                $("tr[name=tr_t2]").hide();
                $("tt[name=tt_t2]").empty();
                $("tt[name=tt_t2]").each(function () {
                    var t = $(this).html();
                    $(this).replaceWith(t);
                });
            }
            else if (t == "2000101") {
                $("tr[name=tr_t2]").show();
                $("tr[name=tr_t1]").hide();
                $("tt[name=tt_t1]").empty();
                $("tt[name=tt_t1]").each(function () {
                    var t = $(this).html();
                    $(this).replaceWith(t);
                });
            } else {
                $("tr[name=tr_t1]").hide();
                $("tr[name=tr_t2]").hide();
                $("tt[name=tt_t1]").empty();
                $("tt[name=tt_t1]").each(function () {
                    var t = $(this).html();
                    $(this).replaceWith(t);
                });
                $("tt[name=tt_t2]").empty();
                $("tt[name=tt_t2]").each(function () {
                    var t = $(this).html();
                    $(this).replaceWith(t);
                });
            }
        }
        function selEnt(obj, tagId, type) {

            var qylx = "101";
            switch (type) {
                case "SG":
                    qylx = "101";
                    break;
                case "SJ":
                    qylx = "103";
                    break;
                case "KC":
                    qylx = "102";
                    break;
                case "JL":
                    qylx = "104";
                    break;
            }
            var url = "../project/EntListSel.aspx";
            url += "?qylx=" + qylx;
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
                //企业变化清空人员
                if (tagId == "q_SGDWId") {
                    if ($("#q_SGDWoldId").val() != pid) {
                        $("#sj_XMJL").val("");
                        $("#q_XMJL").val("");
                        $("#Button4").click();
                    }

                }
                
            }
        }

        function selEmp(obj, tagId,type) {

            var qybm = "101";
            switch (type) {
                case "XMJ":
                    qybm = document.getElementById("q_SGDWId").value;
                    break;
                case "JGS":
                    qybm = document.getElementById("sj_FBaseInfoId").value;
                    break;
                case "JCS":
                    qybm = document.getElementById("sj_FBaseInfoId").value;
                    break;
                case "YTG":
                    qybm = document.getElementById("q_KCDWId").value;
                    break;
                case "XMZ":
                    //qybm = document.getElementById("q_JLDWId").value;
                    qybm = document.getElementById("q_JLDWIdnew").value;
                    break;
            }
            if (type == "XMZ") {
                var url = "../project/EmpListSel.aspx";
                url += "?qybm=" + qybm;
                var pid = showWinByReturn(url, 1000, 600);
                if (pid != null && pid != '') {
                    $("#" + tagId).val(pid);
                    __doPostBack(obj.id, '');
                }
            }
            else {
                var url = "../project/EmpListSelA.aspx";
                url += "?qybm=" + qybm;
                var pid = showWinByReturn(url, 1000, 600);
                if (pid != null && pid != '') {
                    $("#" + tagId).val(pid);
                    __doPostBack(obj.id, '');
                }
            }
        }

        function BtnClear() {
            $("#s_FBaseInfoId").val('');
            $("#s_FId").val('');
            $("#s_FBaseInfoId_c").val('');
            return true;
        }

    </script>

    <base target="_self"></base>
    <style type="text/css">
        .m_txt {}
        .auto-style1 {
            height: 27px;
        }
        .auto-style2 {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                质量监督备案基本信息
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
            <td class="t_r t_bg" style="width:18.8%;">
                建设单位名称：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="p_JSDW" runat="server" CssClass="m_txt" Enabled="false" Width="195px"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                建设单位法人：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_LegalPerson" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                项目名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_ProjectName" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_PrjItemName" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程所属地：
            </td>
            <td colspan="1">
                <uc1:govdeptidfalse ID="govd_FRegistDeptId" runat="server" Enabled="false"/>
            </td>
            <td class="t_r t_bg">
                工程地点：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_Address" runat="server" CssClass="m_txt" Width="195px" MaxLength="30"
                    Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程性质：
            </td>
            <td>
                <asp:DropDownList ID="p_PrjItemType" runat="server" CssClass="m_txt" Width="200px" Enabled="false">
                </asp:DropDownList><tt>*</tt>
            </td>
            <td class="t_r t_bg">
                工程造价：
            </td>
            <td>
                <asp:TextBox ID="p_Cost" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                        Width="195px" Enabled="false"></asp:TextBox>(万元) <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                计划开工日期：
            </td>
            <td>
                <asp:TextBox ID="q_StartDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="195px"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                计划竣工日期：
            </td>
            <td>
                <asp:TextBox ID="q_EndDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="195px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>   
        <tr>
            <td class="t_r t_bg">
                备案号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="p_RecordNo" runat="server" CssClass="m_txt" Enabled="false" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                立项文号：
            </td>
            <td>
                <asp:TextBox ID="pj_ProjectNumber" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                立项时间：
            </td>
            <td>
                <asp:TextBox ID="pj_ProjectTime" onfocus="WdatePicker()" runat="server" Width="195px"  CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="pj_Contacts" runat="server" CssClass="m_txt" Enabled="false" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="pj_Mobile" runat="server" CssClass="m_txt" onblur="isTel(this);" Enabled="false" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr name="tr_t1">
            <td class="t_r t_bg">
                工程规模（长度、管径或面积）：
            </td>
            <td colspan="1">
                <asp:TextBox ID="q_Scale" runat="server" CssClass="m_txt" onblur="isFloat(this)"
                    Width="195px"></asp:TextBox>
                <tt name="tt_t1">*</tt>
            </td>
            <td class="t_r t_bg">
                监督费：
            </td>
            <td colspan="1">
                <asp:TextBox ID="q_SupervisionCost" runat="server" CssClass="m_txt" onblur="isFloat(this)"
                    Width="195px"></asp:TextBox>(元)
                <tt name="tt_t1">*</tt>
            </td>
        </tr>
        <tr name="tr_t2">
            <td class="t_r t_bg">
                投资性质：
            </td>
            <td colspan="1">
                <asp:TextBox ID="q_InvestmentType" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                <tt name="tt_t2">*</tt>
            </td>
        </tr>
        <tr name="tr_t2">
            <td class="t_r t_bg">
                建筑面积(m2)：
            </td>
            <td>
                <asp:TextBox ID="q_Area" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                <tt name="tt_t2">*</tt>
            </td>
            <td class="t_r t_bg">
                结构类型：
            </td>
            <td>
                <asp:DropDownList ID="q_ConstrType" runat="server" CssClass="m_txt" Width="200px">
                </asp:DropDownList>
                <tt name="tt_t2">*</tt>
            </td>
        </tr>
        <tr name="tr_t2">
            <td class="t_r t_bg">
                层数：
            </td>
            <td colspan="3">
                <asp:TextBox ID="q_Layers" runat="server" CssClass="m_txt" Width="70px"
                    MaxLength="9"></asp:TextBox>(层) 其中地上：<tt name="tt_t2">*</tt>
                <asp:TextBox ID="q_Ground" runat="server" CssClass="m_txt" Width="70px"
                    MaxLength="9"></asp:TextBox>(层) 地下：<tt name="tt_t2">*</tt>
                <asp:TextBox ID="q_Underground" runat="server" CssClass="m_txt" Width="70px"
                    MaxLength="9"></asp:TextBox>(层)<tt name="tt_t2">*</tt>
            </td>
        </tr>
        <tr name="tr_t2">
            <td class="t_r t_bg">
                总高：
            </td>
            <td colspan="1">
                <asp:TextBox ID="q_Height" runat="server" CssClass="m_txt" onblur="isFloat(this)"
                    Width="195px" MaxLength="10"></asp:TextBox>(m)
                <tt name="tt_t2">*</tt>
            </td>
            <td class="t_r t_bg">
                电梯：
            </td>
            <td colspan="1">
                <asp:TextBox ID="q_Elevator" runat="server" CssClass="m_txt" Width="195px" onblur="isInt(this)"
                    ></asp:TextBox>(台)
                <tt name="tt_t2">*</tt>
            </td>
        </tr>
        <tr name="tr_t2">
            <td class="t_r t_bg">
                自动扶梯：
            </td>
            <td colspan="1">
                <asp:TextBox ID="q_Escalator" runat="server" CssClass="m_txt" Width="195px"
                    MaxLength="10"></asp:TextBox>(台)
                <tt name="tt_t2">*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                勘察文件：
            </td>
            <td>
                <asp:TextBox ID="q_KCWJ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                施工图设计文件：
            </td>
            <td>
                <asp:TextBox ID="q_SGTSJWJ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                施工图设计文件审查机构：
            </td>
            <td>
                <asp:TextBox ID="q_SGTSJWJSCJG" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                施工图设计文件审查批准文号：
            </td>
            <td>
                <asp:TextBox ID="q_SGTSJWJSCPZWH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
    </table>
    
            <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                施工单位信息
            </td>
        </tr>
        <tr class="t_l t_bg" id="Tr1" runat="server" style="display: none;">
            <td colspan="4" class="auto-style2">
                主要施工单位
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1" style="width:18.8%;">
                施工单位名称：
            </td>
            <td class="auto-style1" colspan="1" style="width:29%;">
                <asp:TextBox ID="q_SGDW" runat="server" CssClass="m_txt" Width="195px" Enabled="false" ReadOnly="True"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEnt(this, 'q_SGDWId','SG');"
                    UseSubmitBehavior="false" OnClick="btnSel_sg_Click" />
                <%--<asp:Button  ID="Button4" Style=" display:none;" runat="server" Text="删除项目经理" OnClick="Button4_Click"  />--%>

                <input id="q_SGDWId" runat="server" type="hidden" />
                <input id="q_SGDWoldId" runat="server" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                施工单位地址：
            </td>
            <td class="t_l t_bg" colspan="3">
               <asp:TextBox ID="q_SGDWDZ" runat="server" CssClass="m_txt"  Width="195px"></asp:TextBox>
                 </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                施工单位法人：
            </td>
            <td>
                <asp:TextBox ID="q_SGDWFR" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                资质证书号：
            </td>
            <td>
                <asp:TextBox ID="q_SGDWZS" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="q_SGDWDH" runat="server" CssClass="m_txt" MaxLength="20" onblur="isTel(this);" Width="195px"></asp:TextBox>&nbsp;</td>
            <td class="t_r t_bg">
                项目经理：</td>
            <td>
                <asp:TextBox ID="q_XMJL" runat="server" CssClass="m_txt" Width="195px" ReadOnly="True"></asp:TextBox>
                
                <asp:Button ID="btnMod2" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEmp(this, 'sj_XMJL','XMJ');"
                    UseSubmitBehavior="false" OnClick="btnSel_XMJ_Click" />
                <input id="sj_XMJL" runat="server" type="hidden" />
                
                &nbsp;</td>
        </tr>
        
    </table>

    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                监理单位信息
            </td>
        </tr>
        <tr class="t_l t_bg" id="Tr2" runat="server" style="display: none;">
            <td colspan="4">
                主要监理单位
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1" style="width:18.8%;">
                监理单位名称：
            </td>
            <td class="auto-style1" colspan="1" style="width:29%;">
                <asp:TextBox ID="q_JLDW" runat="server" CssClass="m_txt" Width="195px" Enabled="false" ReadOnly="True"></asp:TextBox>
                <asp:Button ID="Button3" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEnt(this, 'q_JLDWId','JL');"
                    UseSubmitBehavior="false" OnClick="btnSel_jl_Click" />
                <input id="q_JLDWId" runat="server" type="hidden" />
                <input id="q_JLDWIdnew" runat="server" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                监理单位地址：
            </td>
            <td class="t_l t_bg" colspan="3">
               <asp:TextBox ID="q_JLDWDZ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                 </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                监理单位法人：
            </td>
            <td>
                <asp:TextBox ID="q_JLDWFR" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="q_JLDWDH" runat="server" CssClass="m_txt" MaxLength="20" onblur="isFloat(this);" Width="195px"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                项目总监：</td>
            <td>
                <asp:TextBox ID="q_XMZJ" runat="server" CssClass="m_txt" MaxLength="20" Width="195px" ReadOnly="True"></asp:TextBox>
                
                <asp:Button ID="btnMod4" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEmp(this, 'jl_XMZJ','XMZ');"
                    UseSubmitBehavior="false" OnClick="btnSel_XMZ_Click" /> 
                <input id="jl_XMZJ" runat="server" type="hidden" />
           </td>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td>
                <asp:TextBox ID="q_JLZS" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        
    </table>


    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                勘察单位信息
            </td>
        </tr>
        <tr class="t_l t_bg" id="mainKC" runat="server" style="display: none;">
            <td colspan="4">
                主要勘察单位
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1" style="width:18.8%;">
                勘察单位名称：
            </td>
            <td class="auto-style1" colspan="1" style="width:29%;">
                <asp:TextBox ID="q_CCDW" runat="server" CssClass="m_txt"  Width="195px" Enabled="false" ReadOnly="True"></asp:TextBox>
                
                <asp:Button ID="Button1" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEnt(this, 'q_KCDWId','KC');"
                    UseSubmitBehavior="false" OnClick="btnSel_kc_Click" />
                <input id="q_KCDWId" runat="server" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                勘察单位地址：
            </td>
            <td class="t_l t_bg" colspan="3">
               <asp:TextBox ID="q_CCDWDZ" runat="server" CssClass="m_txt"  Width="195px"></asp:TextBox>
                 </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                勘察单位法人：
            </td>
            <td>
                <asp:TextBox ID="q_CCDWFR" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="q_CCDWDH" runat="server" CssClass="m_txt" MaxLength="20" onblur="isFloat(this);" Width="195px" ></asp:TextBox>&nbsp;</td>
        </tr>
        <tr>
             <td class="t_r t_bg">
                注册岩土工程师：
            </td>
            <td>
                <asp:TextBox ID="q_YTGCS" runat="server" CssClass="m_txt" Width="195px" ReadOnly="True"></asp:TextBox>
                
                <asp:Button ID="btnMod3" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEmp(this, 'kc_YTGCS','YTG');"
                    UseSubmitBehavior="false" OnClick="btnSel_YTG_Click" /> <input id="kc_YTGCS" runat="server" type="hidden" />
                &nbsp;</td>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td>
                <asp:TextBox ID="q_CCDWZS" runat="server" CssClass="m_txt" Width="195px" ReadOnly="True"></asp:TextBox>
            </td>

            
        </tr>
        <tr>
            <td class="t_r t_bg">
                备注：</td>
            <td colspan="3">
               <asp:TextBox ID="q_Remark" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="539px"></asp:TextBox>&nbsp;</td>
        </tr>
    </table>
   

       <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                设计单位信息
            </td>
        </tr>
        <tr class="t_l t_bg" id="mainSJ" style="display: none;" runat="server">
            <td colspan="4">
                主要设计单位
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1" style="width:18.8%;">
                设计单位名称：
            </td>
            <td class="auto-style1" colspan="1" style="width:29%;">
                <asp:TextBox ID="sj_FName" runat="server" CssClass="m_txt" Width="195px" Enabled="false" ReadOnly="True"></asp:TextBox>
                
                <asp:Button ID="btnMod" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEnt(this, 'sj_FBaseInfoId','SJ');"
                    UseSubmitBehavior="false" OnClick="btnSel_sj_Click" />
                <input id="sj_FBaseInfoId" runat="server" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                设计单位地址：
            </td>
            <td class="t_l t_bg" colspan="3">
               <asp:TextBox ID="sj_FRegistAddress" runat="server" CssClass="m_txt" ReadOnly="true" Width="195px"></asp:TextBox>
                 </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                设计单位法人：
            </td>
            <td>
                <asp:TextBox ID="sj_FLinkMan" runat="server" CssClass="m_txt" ReadOnly="true" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                证书编号：
            </td>
            <td>
                <asp:TextBox ID="sj_FLicence" runat="server" CssClass="m_txt" ReadOnly="true" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="sj_FMobile" runat="server" CssClass="m_txt" MaxLength="20" Width="195px"></asp:TextBox>&nbsp;</td>
            <td class="t_r t_bg">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                项目设计注册建筑师：
            </td>
            <td>
                <asp:TextBox ID="q_JZS" runat="server" CssClass="m_txt" MaxLength="20"  Width="195px" ReadOnly="True"></asp:TextBox>
                
                <asp:Button ID="btnMod0" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEmp(this, 'sj_ZCJCS','JCS');"
                    UseSubmitBehavior="false" OnClick="btnSel_JCS_Click" />
                <input id="sj_ZCJCS" runat="server" type="hidden" />
                &nbsp;</td>
            <td class="t_r t_bg">
                项目设计注册结构师：</td>
            <td>
               <asp:TextBox ID="q_JGS" runat="server" CssClass="m_txt" MaxLength="20"  Width="195px" ReadOnly="True"></asp:TextBox>
                
                <asp:Button ID="btnMod1" runat="server" Text="选择..." CssClass="m_btn_w4" OnClientClick="return selEmp(this, 'sj_ZCJGS','JGS');"
                    UseSubmitBehavior="false" OnClick="btnSel_JGS_Click" />
                <input id="sj_ZCJGS" runat="server" type="hidden" />
                &nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否联合体：
            </td>
            <td colspan="3">
                <asp:CheckBox ID="sj_FInt2" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                其他设计单位：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_FTxt10" runat="server" CssClass="m_txt" Width="400px" Visible="false"></asp:TextBox>
                <asp:Button ID="btnAdd" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'sj_other_FBaseInfoId','SJ');"
                    UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEnt_sj_Click" Style="margin-bottom: 4px;" />
                <input id="sj_other_FBaseInfoId" runat="server" type="hidden" />
                <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" AutoGenerateColumns="False"
                    DataKeyNames="FId" EmptyDataText="没有添加联合体" OnRowCommand="DG_List_RowCommand"
                    Style="margin: 0px;">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="单位名称" DataField="FName">
                            <ItemStyle CssClass="t_l" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <HeaderStyle Width="60" />
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDel" runat="server" CommandName="Del" CommandArgument='<%#Eval("FID")%>'
                                    OnClientClick="return confirm('确定删除吗？');" Text="删除">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <input id="q_AddressDept" type="hidden" runat="server" />
    <input id="pj_AddressDept" type="hidden" runat="server" />
    </form>
</body>
</html>

