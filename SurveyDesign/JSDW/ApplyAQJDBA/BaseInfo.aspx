<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaseInfo.aspx.cs" Inherits="JSDW_ApplyAQJDBA_BaseInfo" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptidfalse.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>安全备案基本信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            isVisible();
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function isVisible() {
            var cb = $("#t_FFloat11");
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
            cb = $("#t_FFloat10");
            table = $("#otherSJ");
            tr = $("#mainSJ");
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
        function BtnClear() {
            $("#s_FBaseInfoId").val('');
            $("#s_FId").val('');
            $("#s_FBaseInfoId_c").val('');
            return true;
        }
        function doSelOther(tagId, obj, fsysid, hasCerti, oTagId, tip) {
            if ($('#' + oTagId).val() == '') {
                alert(tip);
                return false;
            }
            else {
                return selEnt(tagId, fsysid, obj, oTagId);
            }
        }
        //选择勘察、设计、监理类企业
        function selEnt(obj, tagId) {
            var url = "../project/EntListSel.aspx";
            var qylx = "101";
            if (tagId == "t_SGId") {
                qylx = "101";
            } else if (tagId == "t_JLId") {
                qylx = "125";
            }
            url += "?qylx=" + qylx;
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }

        //选择施工、专业、劳务类单位
        function selEntSg(obj, tagId) {
            var url = "../project/EntListSelSg.aspx";
            var qylx = "101";
            if (tagId == "t_SGId") {
                qylx = "101";
            } else if (tagId == "t_JLId") {
                qylx = "125";
            }
            url += "?qylx=" + qylx;
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }
        function selEmp(obj, tagId) {
            var qybm = "";
            if (tagId == "t_SGRYId") {
                qybm = document.getElementById("t_SGId").value;
            } else if (tagId == "t_JLRYId") {
                qybm = document.getElementById("t_JLId").value;
            }
            if (qybm != null && qybm != "") {
                var url = "../project/EmpListSel.aspx";
                url += "?qybm=" + qybm + "&rylx=" + tagId;
                var pid = showWinByReturn(url, 1000, 600);
                if (pid != null && pid != '') {
                    $("#" + tagId).val(pid);
                    __doPostBack(obj.id, '');
                }
            } else {
                alert('请先选择单位！');
                return;
            }

        }
    </script>

    <base target="_self"></base>
    <style type="text/css">
        .m_txt {}
        .auto-style1 {
            height: 27px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                安监备案表
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
                备案号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="p_RecordNo" runat="server" CssClass="m_txt" Enabled="false" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg"  style="width:18.8%;">
                立项文号：
            </td>
            <td  style="width:29%;">
                <asp:TextBox ID="pj_ProjectNumber" runat="server" CssClass="m_txt" Enabled="false" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg"  style="width:18.8%;">
                立项日期：
            </td>
            <td>
                <asp:TextBox ID="pj_ProjectTime" runat="server" CssClass="m_txt" Enabled="false" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                项目名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_ProjectName" runat="server" CssClass="m_txt" Enabled="false" Width="195px"></asp:TextBox><tt>*</tt>
            </td>
                        <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_PrjItemName" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程所属地：
            </td>
            <td colspan="1">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server"/>
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
                建筑面积(m2)：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_Area" runat="server" CssClass="m_txt" Enabled="false" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                建筑层数（层）：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_Floor" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设单位名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_JSDW" runat="server" CssClass="m_txt" Enabled="false" Width="195px"></asp:TextBox><tt>*</tt>
                <input type="hidden"  runat="server" ID="p_FJSDWID" value="" />
            </td>
                        <td class="t_r t_bg">
                建设单位法人：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_LegalPerson" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
       <tr>
            <td class="t_r t_bg">
                结构类型：
            </td>
            <td>
                <asp:DropDownList ID="q_ConstrType" runat="server" CssClass="m_txt" Enabled="false" Width="202px">
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                工程总造价：
            </td>
            <td>
                <asp:TextBox ID="p_Cost" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="195px" Enabled="false"></asp:TextBox>(万元) <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                计划施工期限：
            </td>
            <td>
                <asp:TextBox ID="q_PlanStartTime" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                计划开工日期：
            </td>
            <td>
                <asp:TextBox ID="q_StartDate" runat="server" CssClass="m_txt" Width="195px" onfocus="WdatePicker()"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                计划竣工日期：
            </td>
            <td>
                <asp:TextBox ID="q_EndDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="pj_Contacts" runat="server" CssClass="m_txt"  Width="195px"></asp:TextBox><tt>*</tt>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="pj_Mobile" runat="server" CssClass="m_txt"  Width="195px"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
    </table>
   
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                施工单位信息
            </td>
        </tr>
        <!--<tr>
            <td class="t_r t_bg" align="right">
                是否联合体
            </td>
            <td colspan="3">
                <asp:CheckBox runat="server" ID="CheckBox1" onclick="isVisible();" />
            </td>
        </tr>-->
        <tr class="t_l t_bg" id="Tr1" runat="server" style="display: none;">
            <td colspan="4">
                主要施工单位
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1" style="width:18.8%;">
                施工单位名称：
            </td>
            <td class="auto-style1" colspan="3" >
                <asp:TextBox ID="q_SGDW" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox><tt>*</tt>
                <input type="hidden"  runat="server" ID="t_SGId" value="" />
                <asp:Button ID="btnAddEnt" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEntSg(this,'t_SGId');"
                UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntSG_Click" Style="margin-bottom: 4px;margin-left:5px;" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                施工单位法人：
            </td>
            <td style="width:29%;">
                <asp:TextBox ID="q_SGDWFR" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="q_SGDWDH" runat="server" CssClass="m_txt" Width="195px" ></asp:TextBox>&nbsp;</td>
            
        </tr>
        <tr>
            <td class="t_r t_bg">
                资质证书号：
            </td>
            <td>
                <asp:TextBox ID="q_SGDWZZZSH" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                安许证号：</td>
            <td>
                <asp:TextBox ID="q_SGDWAXZH" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="q_SGDWZZDJ" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                组织机构代码：</td>
            <td>
                <asp:TextBox ID="q_SGDWZZJGDM" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                项目经理：
            </td>
            <td>
                <asp:TextBox ID="q_SGDWXMJL" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox><tt>*</tt>
                <input type="hidden"  runat="server" ID="t_SGRYId" value="" />
                <asp:Button ID="btnAdd" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEmp(this,'t_SGRYId');"
                UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEmpSG_Click" Style="margin-bottom: 4px;margin-left:5px;" />
            </td>
            <td class="t_r t_bg">
                资格证号：</td>
            <td>
                <asp:TextBox ID="q_SGDWZGZH" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                身份证号：
            </td>
            <td>
                <asp:TextBox ID="q_SGDWSFZH" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                文明施工创优目标：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="q_WMSGCYMB" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine" Width="539px"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                监理单位信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1" style="width:18.8%;">
                监理单位名称：
            </td>
            <td class="auto-style1" colspan="1" style="width:29%;">
                <asp:TextBox ID="q_JLDW" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox><tt>*</tt>
                <input type="hidden"  runat="server" ID="t_JLId" value="" />
                <asp:Button ID="Button1" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_JLId');"
                UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntJL_Click" Style="margin-bottom: 4px;margin-left:5px;" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                监理单位法人：
            </td>
            <td>
                <asp:TextBox ID="q_JLDWFR" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="q_JLDWDH" runat="server" CssClass="m_txt" Width="195px" ></asp:TextBox>&nbsp;</td>
            
        </tr>
        <tr>
            <td class="t_r t_bg">
                资质证书号：
            </td>
            <td>
                <asp:TextBox ID="q_JLDWZZZSH" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="q_JLDWZZDJ" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                组织机构代码：</td>
            <td>
                <asp:TextBox ID="q_JLDWZZJGDM" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                项目总监：
            </td>
            <td>
                <asp:TextBox ID="q_JLDWXMZJ" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox><tt>*</tt>
                <input type="hidden"  runat="server" ID="t_JLRYId" value="" />
                <asp:Button ID="Button2" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEmp(this,'t_JLRYId');"
                UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEmpJL_Click" Style="margin-bottom: 4px;margin-left:5px;" />
            </td>
            <td class="t_r t_bg">
                资格证号：</td>
            <td>
                <asp:TextBox ID="q_JLDWZGZH" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                重大危源情况：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="q_ZDWJQK" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine" Width="539px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备注：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="q_Remark" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine" Width="539px"></asp:TextBox>
            </td>
        </tr>
    </table>
   
    <input id="q_AddressDept" type="hidden" runat="server" />
    <input id="pj_AddressDept" type="hidden" runat="server" />
    <input id="q_SGDWID" type="hidden" runat="server" />
    <input id="q_JLDWID" type="hidden" runat="server" />
    </form>
</body>
</html>

