<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjBaseInfo.aspx.cs" Inherits="JSDW_ApplyZLJDBA_PrjBaseInfo" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工程基本情况信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#CBLCityPlan").change(function () {
                showTable();
            });
            $("#briBaseType").change(function () {
                showBriBaseTxt();
            }); 
            $("#briTopType").change(function () {
                showBriTopTxt();
            });
            $("#briLength").change(function () {
                showBriLengthTxt();
            });
            $("#DrainageDiameter").change(function () {
                showDiameterTxt();
            });
            $("#GasPipeType").change(function () {
                showGasPipeTxt();
            });
            $("#WTBaseType").change(function () {
                showWTBaseTxt();
            });
            $("#BaseConstrType").change(function () {
                showBaseConstrTxt();
            });
            $("#MixFoundationType").change(function () {
                showMixFoundationTxt();
            });
            $("#PileBaseType").change(function () {
                showPileBaseTxt();
            });
            $("#CurtainWall").change(function () {
                showCurtainWallTxt();
            });
        });
        function showDiv1() {
            $("#div2").hide();
            $("#div1").show();
            $("#tableCP").show();
        }
        function showDiv2() {
            $("#div1").hide();
            $("#div2").show();
            $("#tableCP").hide();
        }
        function showTable() {
            $("#CBLCityPlan :checkbox").each(function () {
                if (this.checked) {
                    var id = $(this).parent('span').attr('val');
                    $("#table" + id).show();
                } else {
                    var id = $(this).parent('span').attr('val');
                    $("#table" + id).hide();
                }
            });
        }
        function showBriBaseTxt() {
            var id = $('#briBaseType input[type=radio]:checked').val();
            if (id == '3') {
                $("#briBaseTxt").show();
            } else {
                $("#briBaseTxt").val("");
                $("#briBaseTxt").hide();
            }
        }
        function showBriTopTxt() {
            $("#briTopType :checkbox").each(function () {
                var id = $(this).parent('span').attr('val');
                if (id == '11') {
                    if (this.checked) {
                        $("#birTopTxt").show();
                    } else {
                        $("#birTopTxt").val("");
                        $("#birTopTxt").hide();
                    }
                }
            });
        }
        function showBriLengthTxt() {
            $("#briLength :radio").each(function () {
                if (this.checked) {
                    var id = $(this).val();
                    $("#briLengthTxt" + id).show();
                } else {
                    var id = $(this).val();
                    $("#briLengthTxt" + id).hide();
                }
            });
        }
        function showDiameterTxt() {
            $("#DrainageDiameter :checkbox").each(function () {
                if (this.checked) {
                    var id = $(this).parent('span').attr('val');
                    $("#DiameterTxt" + id).show();
                } else {
                    var id = $(this).parent('span').attr('val');
                    $("#DiameterTxt" + id).hide();
                }
            });
        }
        function showGasPipeTxt() {
            $("#GasPipeType :checkbox").each(function () {
                var id = $(this).parent('span').attr('val');
                if (id == '3') {
                    if (this.checked) {
                        $("#GasPipeTxt").show();
                    } else {
                        $("#GasPipeTxt").val("");
                        $("#GasPipeTxt").hide();
                    }
                }
            });
        }
        function showWTBaseTxt() {
            var id = $('#WTBaseType input[type=radio]:checked').val();
            if (id == '2') {
                $("#WTBaseTxt").show();
            } else {
                $("#WTBaseTxt").val("");
                $("#WTBaseTxt").hide();
            }
        }
        function showBaseConstrTxt() {
            $("#BaseConstrType :checkbox").each(function () {
                var id = $(this).parent('span').attr('val');
                if (id == '6') {
                    if (this.checked) {
                        $("#BaseConstrTxt").show();
                    } else {
                        $("#BaseConstrTxt").val("");
                        $("#BaseConstrTxt").hide();
                    }
                }
            });
        }
        function showMixFoundationTxt() {
            $("#MixFoundationType :checkbox").each(function () {
                var id = $(this).parent('span').attr('val');
                if (id == '6') {
                    if (this.checked) {
                        $("#MixFoundationTxt").show();
                    } else {
                        $("#MixFoundationTxt").val("");
                        $("#MixFoundationTxt").hide();
                    }
                }
            });
        }
        function showPileBaseTxt() {
            $("#PileBaseType :checkbox").each(function () {
                var id = $(this).parent('span').attr('val');
                if (id == '5') {
                    if (this.checked) {
                        $("#PileBaseTxt").show();
                    } else {
                        $("#PileBaseTxt").val("");
                        $("#PileBaseTxt").hide();
                    }
                }
            });
        }
        function showCurtainWallTxt() {
            $("#CurtainWall :checkbox").each(function () {
                var id = $(this).parent('span').attr('val');
                if (id == '4') {
                    if (this.checked) {
                        $("#CurtainWallTxt").show();
                    } else {
                        $("#CurtainWallTxt").val("");
                        $("#CurtainWallTxt").hide();
                    }
                }
            });
        }
        function hideSaveBtn() {
            $("#btnSave").hide();
        }
    </script>

    <base target="_self"></base>
    <style type="text/css">
        .m_txt {}
        /*去掉继承的边框*/
.noborder_table { border: none; }
.noborder_table td { border: none; }
        .auto-style2 {
            height: 22px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                工程基本情况信息
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
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    
    <div id="div1"> 
    <table id="tableCP" width="98%" align="center" class="m_bar" style="display:none">
        <tr>
            <td class="m_bar_l">
            </td>
            <td colspan="1" align="center" >
                请选择类型：
            </td>
            <td>
                <asp:CheckBoxList ID="CBLCityPlan" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">桥梁</asp:ListItem>
                    <asp:ListItem Value="2">河提</asp:ListItem>
                    <asp:ListItem Value="3">下穿隧道</asp:ListItem>
                    <asp:ListItem Value="4">道路</asp:ListItem>
                    <asp:ListItem Value="5">排水</asp:ListItem>
                    <asp:ListItem Value="6">给水</asp:ListItem>
                    <asp:ListItem Value="7">电力浅沟</asp:ListItem>
                    <asp:ListItem Value="8">煤气</asp:ListItem>
                    <asp:ListItem Value="9">水处理构筑物</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table id="table1" class="m_table" width="98%" align="center" style="display:none">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>桥梁</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:19.8%;">
                基础类型：
            </td>
            <td style="width:29%;">
                <asp:RadioButtonList ID="briBaseType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">桩基础</asp:ListItem>
                    <asp:ListItem Value="2">刚性扩大基础</asp:ListItem>
                    <asp:ListItem Value="3">其它</asp:ListItem>
                </asp:RadioButtonList>
                <asp:TextBox ID="briBaseTxt" runat="server" CssClass="m_txt" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                下部结构类型： </td>
            <td>
                <asp:CheckBoxList ID="briBottomType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">桥墩</asp:ListItem>
                    <asp:ListItem Value="2">桥台</asp:ListItem>
                    <asp:ListItem Value="3">承台</asp:ListItem>
                    <asp:ListItem Value="4">盖梁</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                上部结构类型：
            </td>
            <td colspan="3">
                <asp:CheckBoxList ID="briTopType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">有预应力简支梁</asp:ListItem>
                    <asp:ListItem Value="2">无预应力简支梁</asp:ListItem>
                    <asp:ListItem Value="3">有预应力连续梁</asp:ListItem>
                    <asp:ListItem Value="4">无预应力连续梁</asp:ListItem>
                    <asp:ListItem Value="5">有预应力拱桥</asp:ListItem>
                    <asp:ListItem Value="6">无预应力拱桥</asp:ListItem>
                    <asp:ListItem Value="7">有预应力刚架桥</asp:ListItem>
                    <asp:ListItem Value="8">无预应力刚架桥</asp:ListItem>
                    <asp:ListItem Value="9">斜拉桥</asp:ListItem>
                    <asp:ListItem Value="10">钢结构</asp:ListItem>
                    <asp:ListItem Value="11">其它</asp:ListItem>
                </asp:CheckBoxList>
                <asp:TextBox ID="birTopTxt" runat="server" CssClass="m_txt" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
                       
        </tr>
        <tr>
            <td class="t_r t_bg">
                桥 梁 全 长（m）：
            </td>
            <td>
                <asp:RadioButtonList ID="briLength" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">单跨（联）</asp:ListItem>
                    <asp:ListItem Value="2">多跨（联）</asp:ListItem>
                </asp:RadioButtonList>
                <asp:TextBox ID="briLengthTxt1" runat="server" CssClass="m_txt" onblur="isFloat(this)" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
                <asp:TextBox ID="briLengthTxt2" runat="server" CssClass="m_txt" onblur="isFloat(this)" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                荷载等级：
            </td>
            <td >
                <asp:RadioButtonList ID="briLoadLevel" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">城－A级</asp:ListItem>
                    <asp:ListItem Value="2">城－B级</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="t_r t_bg">
                桥梁最大跨径（m）：
            </td>
            <td >
                <asp:TextBox ID="t_BriMaxSpan" runat="server" CssClass="m_txt" onblur="isFloat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_BriRemarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
     
    </table>
    
    <table id="table2" class="m_table" width="98%" align="center" style="display:none">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>河堤</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                基础类型：
            </td>
            <td style="width:29%;">
                <asp:RadioButtonList ID="RBBaseType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem  Value="1">桩基础</asp:ListItem>
                    <asp:ListItem  Value="2">砼基础</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                主体结构类型： </td>
            <td style="width:29%;">
                <asp:RadioButtonList ID="RBMainType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem  Value="1">有卸荷台</asp:ListItem>
                    <asp:ListItem  Value="2">无卸荷台</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                全      长（m）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_RBLength" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
                       
        </tr>
       
        <tr style="height:60px;">
            <td class="t_r t_bg">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_RBRemarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
     
    </table>
 
    <table id="table3" class="m_table" width="98%" align="center"  style="display:none">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>下穿隧道</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                基础类型：
            </td>
            <td  style="width:29%;">
                <asp:RadioButtonList ID="TunnelBaseType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem  Value="1">桩基础</asp:ListItem>
                    <asp:ListItem  Value="2">钢筋砼底板</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                主体结构类型： </td>
            <td style="width:29%;">
                <asp:RadioButtonList ID="TunnelMainType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">框 架</asp:ListItem>
                    <asp:ListItem Value="2">U型槽</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                全      长（m）：
            </td>
            <td >
                <asp:TextBox ID="t_TunnelLength" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
             <td class="t_r t_bg">
                开挖方式：
            </td>
            <td >
                <asp:TextBox ID="t_TunnelDig" runat="server" CssClass="m_txt"> </asp:TextBox>
            </td>          
        </tr>
        <tr >
            <td class="t_r t_bg">
                泵房：
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="TunnelPumpHouse" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem  Value="1">有</asp:ListItem>
                    <asp:ListItem  Value="2">无</asp:ListItem>
                </asp:RadioButtonList>
                 
            </td>
                     
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_TunnelRemarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
     
    </table>

    <table id="table4" class="m_table" width="98%" align="center" style="display:none">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>道路</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                面层结构类型：
            </td>
            <td style="width:29%;">
                <asp:RadioButtonList ID="RoadLayerType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">水泥砼</asp:ListItem>
                    <asp:ListItem Value="2">沥青砼</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                道路全长（m）： </td>
            <td style="width:29%;">
                <asp:TextBox ID="t_RoadLength" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                道 路 宽 度（m）：
            </td>
            <td colspan="3" >
                <asp:TextBox ID="t_RoadWidth" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
             
        </tr>
       
        <tr style="height:60px;">
            <td class="t_r t_bg">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_RoadRemarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
     
    </table>

     <table id="table5" class="m_table" width="98%" align="center" style="display:none">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>排水</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                管  道  类  型：
            </td>
            <td style="width:29%;">
                <asp:CheckBoxList ID="DrainagePipeType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">砼平口管</asp:ListItem>
                    <asp:ListItem Value="2">承插管</asp:ListItem>
                    <asp:ListItem Value="3">塑料管</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                全长（m）： </td>
            <td style="width:29%;">
                <asp:TextBox ID="t_DrainageLength" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                管   径（mm）：
            </td>
            <td colspan="3" >
                <asp:CheckBoxList ID="DrainageDiameter" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">雨水主管</asp:ListItem>
                    <asp:ListItem Value="2">污水主管</asp:ListItem>
                </asp:CheckBoxList>
                <asp:TextBox ID="DiameterTxt1" runat="server" onblur="isFloat(this)" CssClass="m_txt" Width="130px" MaxLength="100" style="display:none"> </asp:TextBox>
                <asp:TextBox ID="DiameterTxt2" runat="server" onblur="isFloat(this)" CssClass="m_txt" Width="130px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
             
        </tr>
       
        <tr style="height:60px;">
            <td class="t_r t_bg">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_DrainageRemarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
     
    </table>

    <table id="table6" class="m_table" width="98%" align="center" style="display:none">
        <tr>
            <td class="auto-style2" colspan="4">
                <h3>给水</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                管  道  类  型：
            </td>
            <td style="width:29%;">
                <asp:CheckBoxList ID="WSPipeType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">钢 管</asp:ListItem>
                    <asp:ListItem Value="2">PCCP管</asp:ListItem>
                    <asp:ListItem Value="3">球墨铸铁管</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                设计压力（MPa）： </td>
            <td style="width:29%;">
                <asp:TextBox ID="t_WSPressure" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                管   径（mm）：
            </td>
            <td colspan="3" >
                <asp:TextBox ID="t_WSDiameter" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
             
        </tr>
       
        <tr style="height:60px;">
            <td class="t_r t_bg">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_WSRemarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
     
    </table>

    <table id="table7" class="m_table" width="98%" align="center" style="display:none">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>电力浅沟</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                结  构  类  型：
            </td>
            <td style="width:29%;">
                <asp:CheckBoxList ID="ECConstrType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">U型槽</asp:ListItem>
                    <asp:ListItem Value="2">排 管</asp:ListItem>
                    <asp:ListItem Value="3">砖砌体</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                 全        长（m）： </td>
            <td style="width:29%;">
                <asp:TextBox ID="t_ECLength" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_ECRemarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
     
    </table>

     <table id="table8" class="m_table" width="98%" align="center" style="display:none">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>煤气</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                管  道  类  型：
            </td>
            <td style="width:29%;">
                <asp:CheckBoxList ID="GasPipeType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">钢 管</asp:ListItem>
                    <asp:ListItem Value="2">PE 管</asp:ListItem>
                    <asp:ListItem Value="3">其 它</asp:ListItem>
                </asp:CheckBoxList>
                <asp:TextBox ID="GasPipeTxt" runat="server" CssClass="m_txt" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                 全        长（m）： </td>
            <td style="width:29%;">
                <asp:TextBox ID="t_GasLength" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
        </tr>
         <tr >
            <td class="t_r t_bg">
                管   径（mm）：
            </td>
            <td  >
                <asp:TextBox ID="t_GasDiameter" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
             <td class="t_r t_bg">
                设计压力（MPa）： </td>
            <td >
                <asp:TextBox ID="t_GasPressure" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
        </tr>
         <tr >
            <td class="t_r t_bg">
               重要穿跨越：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_GasSpan" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_GasRemarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
     
    </table>

     <table id="table9" class="m_table" width="98%" align="center" style="display:none">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>水处理构筑物</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                基 础 类 型：
            </td>
            <td style="width:29%;">
                <asp:RadioButtonList ID="WTBaseType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">钢筋砼底板</asp:ListItem>
                    <asp:ListItem Value="2">其 它</asp:ListItem>
                </asp:RadioButtonList>
                <asp:TextBox ID="WTBaseTxt" runat="server" CssClass="m_txt" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                 主体结构类型： </td>
            <td style="width:29%;">
                <asp:RadioButtonList ID="WTMainType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">有预应力现浇</asp:ListItem>
                    <asp:ListItem Value="2">无预应力现浇</asp:ListItem>
                    <asp:ListItem Value="3">有预应力预制安装</asp:ListItem>
                    <asp:ListItem Value="4">无预应力预制安装</asp:ListItem>
                </asp:RadioButtonList> 
            </td>
        </tr>
         <tr >
            <td class="t_r t_bg">
                几何尺寸（m）：
            </td>
            <td colspan="3" >
                <asp:TextBox ID="t_WTSize" runat="server" CssClass="m_txt" onblur="isFloat(this)"> </asp:TextBox> 
            </td>
            
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_WTRemarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>         
        </tr>
     
    </table>
    </div>
    <div id="div2">
         <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                基础结构型式：
            </td>
            <td>
                <asp:CheckBoxList ID="BaseConstrType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">筏板或箱型基础</asp:ListItem>
                    <asp:ListItem Value="2">独立柱基</asp:ListItem>
                    <asp:ListItem Value="3">条型基础</asp:ListItem>
                    <asp:ListItem Value="4">墩基础</asp:ListItem>
                    <asp:ListItem Value="5">桩基础</asp:ListItem>
                    <asp:ListItem Value="6">其它</asp:ListItem>
                </asp:CheckBoxList>
                <asp:TextBox ID="BaseConstrTxt" runat="server" CssClass="m_txt" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
            
        </tr>
         <tr >
           <td class="t_r t_bg">
                 地基类型： </td>
            <td >
                <asp:RadioButtonList ID="FoundationType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">天然地基</asp:ListItem>
                    <asp:ListItem Value="2">复合地基</asp:ListItem>
                </asp:RadioButtonList> 
            </td>
            
        </tr>
        <tr >
           <td class="t_r t_bg">
                 复合地基类型： </td>
            <td >
                <asp:CheckBoxList ID="MixFoundationType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">CFG复合地基</asp:ListItem>
                    <asp:ListItem Value="2">振冲复合地基</asp:ListItem>
                    <asp:ListItem Value="3">高压旋喷复合地基</asp:ListItem>
                    <asp:ListItem Value="4">压力灌浆复合地基</asp:ListItem>
                    <asp:ListItem Value="5">换填地基</asp:ListItem>
                    <asp:ListItem Value="6">其它</asp:ListItem>
                </asp:CheckBoxList> 
                <asp:TextBox ID="MixFoundationTxt" runat="server" CssClass="m_txt" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
            
        </tr>
        <tr >
           <td class="t_r t_bg">
                 桩基础类型： </td>
            <td >
                <asp:CheckBoxList ID="PileBaseType" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">普通预制桩</asp:ListItem>
                    <asp:ListItem Value="2">预应力管桩</asp:ListItem>
                    <asp:ListItem Value="3">振动沉管灌注桩</asp:ListItem>
                    <asp:ListItem Value="4">大直径人工挖孔桩</asp:ListItem>
                    <asp:ListItem Value="5">其它类型桩基</asp:ListItem>
                </asp:CheckBoxList> 
                <asp:TextBox ID="PileBaseTxt" runat="server" CssClass="m_txt" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
            
        </tr>
        <tr >
           <td class="t_r t_bg">
                 钢结构（包括网架结构）子分部： </td>
            <td >
                <asp:RadioButtonList ID="StealStruct" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">普通钢结构</asp:ListItem>
                    <asp:ListItem Value="2">网架</asp:ListItem>
                </asp:RadioButtonList> 
            </td>
            
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg">
               预应力结构工程：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_StructEngin" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg">
               建筑节能：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_EnergySaving" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
        <tr >
           <td class="t_r t_bg">
                 设备安装： </td>
            <td >
                <asp:CheckBoxList ID="EquipInstall" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal" >
                    <asp:ListItem Value="1">配电系统</asp:ListItem>
                    <asp:ListItem Value="2">给排水</asp:ListItem>
                    <asp:ListItem Value="3">自动喷淋</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            
        </tr>
         <tr style="height:60px;">
            <td class="t_r t_bg">
               公用建筑装饰及吊顶：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_Decorate" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg">
               楼宇对讲系统：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_Intercom" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
        <tr >
           <td class="t_r t_bg">
                 建筑幕墙： </td>
            <td >
                <asp:CheckBoxList ID="CurtainWall" runat="server" CssClass="noborder_table" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">石材</asp:ListItem>
                    <asp:ListItem Value="2">玻璃</asp:ListItem>
                    <asp:ListItem Value="3">铝塑板</asp:ListItem>
                    <asp:ListItem Value="4">其它</asp:ListItem>
                </asp:CheckBoxList> 
                <asp:TextBox ID="CurtainWallTxt" runat="server" CssClass="m_txt" Width="260px" MaxLength="100" style="display:none"> </asp:TextBox>
            </td>
            
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg">
               结构转换层：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_ConstrTrans" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
    </table>
    </div>
    <div id="div3">
    <table class="m_table" width="98%" align="center">       
         <tr >
            <td class="t_r t_bg" style="width:19.8%;">
               新技术、新工艺、新材料采用情况：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_NewTecUse" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
         <tr >
            <td class="t_r t_bg" style="width:19.8%;">
               其它需要说明的情况：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_OtherDescription" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
        <tr style="height:60px;">
            <td class="t_r t_bg" style="width:19.8%;">
               备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_Remarks" runat="server" CssClass="m_txt" TextMode="MultiLine" Width="608px" Height="43px" ></asp:TextBox>
            </td>
                     
        </tr>
     
    </table>
    </div>
    </form>
</body>
</html>

