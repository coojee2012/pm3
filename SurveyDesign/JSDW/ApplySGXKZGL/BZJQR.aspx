<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BZJQR.aspx.cs" Inherits="JSDW_APPLYSGXKZGL_BZJQR" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>保证金确认表</title>
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
    <base target="_self"></base>
    <style type="text/css">
        .modalDiv {
            position: absolute;
            top: 1px;
            left: 1px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: gray;
            opacity: .50;
            filter: alpha(opacity=50);
        }

        .m_txt {
        }

        .auto-style1 {
            height: 23px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField runat="server" ID="hf_FAppId" Value="" />
        <asp:HiddenField runat="server" ID="hf_FId" Value="" />
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="up_Main" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv" style="display: none;">
                    <div style="position: absolute; left: 40%; top: 50%; background-color: peru; border: solid 3px red;">
                        <table align="center">
                            <tr>
                                <td>
                                    <h1>正在保存数据</h1>
                                </td>
                                <td>
                                    <img src="../../image/load2.gif" alt="请稍候" /></td>
                            </tr>

                        </table>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>


        <div id="divSetup2" runat="server">
            <table width="98%" align="center" class="m_bar">
                <tr>
                    <td class="m_bar_l"></td>
                    <td></td>
                    <td class="t_r">
                        <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                    OnClientClick="return checkInfo();" />

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                    </td>
                    <td class="m_bar_r"></td>
                </tr>
            </table>

            <table class="m_table" width="98%" align="center">
                <tr>
                    <td class="t_l t_bg" colspan="4">工程相关信息
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg" style="width: 18.8%;">建设项目名称：
                    </td>
                    <td style="width: 29%;">
                        <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="200px" MaxLength="40" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="t_r t_bg">项目面积（m2）
                    </td>
                    <td>
                        <asp:TextBox ID="t_Area" runat="server" CssClass="m_txt" Width="200px" MaxLength="40" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">建设地点
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="200px" MaxLength="30" Enabled="false"></asp:TextBox>

                    </td>
                    <td class="t_r t_bg">项目编号
                    </td>
                    <td colspan="1">
                        <input type="hidden" runat="server" id="t_PrjAddressDept" value="" />
                        <asp:TextBox ID="t_ProjectNo" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">建设单位
                    </td>
                    <td colspan="1" class="auto-style1">
                        <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="t_r t_bg">项目类别
                    </td>
                    <td colspan="1" class="auto-style1">
                        <asp:DropDownList ID="t_PrjItemType" runat="server" CssClass="m_txt" Width="203px" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">建设项目联系人
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_LZR" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="t_r t_bg">联系电话
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_LXDH" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>

            </table>

            <table class="m_table" width="98%" align="center" style="margin-top: 10px;">
                <tr>
                    <td class="t_l t_bg" colspan="5">相关费用交纳及保证金担保情况
                    </td>
                </tr>
                <tr>
                    <td class="t_bg" align="center" style="width:31%;">缴费项目
                    </td>
                    <td class="t_bg" align="center" style="width:15%;">金额（万元）
                    </td>
                    <td class="t_bg" align="center" style="width:12%;">缴费时间
                    </td>
                    <td class="t_bg" align="center" style="width:20%;">收款经办人
                    </td>
                    <td class="t_bg" align="center" >收款单位签章
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">特大城市市政基础设施配套费（包含原收取的市政建设配套费、城市燃气配套费、自来水配套费）
                    <asp:HiddenField runat="server" ID="hf_FId1" Value="" />
                    <input type="hidden" id="t1_JFXMBM" value="1" runat="server" />
                    <input type="hidden" id="t1_JFXM" value="特大城市市政基础设施配套费（包含原收取的市政建设配套费、城市燃气配套费、自来水配套费）" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="t1_Money" runat="server" CssClass="m_txt" Width="94%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t1_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t1_SKJBR" runat="server" CssClass="m_txt" Width="94%" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t1_SKDW" runat="server" CssClass="m_txt" Width="94%" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">工程定额测定费<input type="hidden" id="t2_JFXMBM" value="2" runat="server" />
                        <input type="hidden" id="t2_JFXM" value="工程定额测定费" runat="server" />
                        <asp:HiddenField ID="hf_FId2" runat="server" Value="" />
                    </td>
                    <td>
                        <asp:TextBox ID="t2_Money" runat="server" CssClass="m_txt" MaxLength="40" Width="94%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t2_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t2_SKJBR" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t2_SKDW" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">文物勘探发掘费<input type="hidden" id="t3_JFXMBM" value="3" runat="server" />
                        <input type="hidden" id="t3_JFXM" value="文物勘探发掘费" runat="server" />
                        <asp:HiddenField ID="hf_FId3" runat="server" Value="" />
                    </td>
                    <td>
                        <asp:TextBox ID="t3_Money" runat="server" CssClass="m_txt" MaxLength="40" Width="94%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t3_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t3_SKJBR" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t3_SKDW" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">建筑工程质量监督费<input type="hidden" id="t4_JFXMBM" value="4" runat="server" />
                        <input type="hidden" id="t4_JFXM" value="建筑工程质量监督费" runat="server" />
                        <asp:HiddenField ID="hf_FId4" runat="server" Value="" />
                    </td>
                    <td>
                        <asp:TextBox ID="t4_Money" runat="server" CssClass="m_txt" MaxLength="40"  Width="94%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t4_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t4_SKJBR" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t4_SKDW" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">新建项目白蚁防治费<input type="hidden" id="t5_JFXMBM" value="5" runat="server" />
                        <input type="hidden" id="t5_JFXM" value="新建项目白蚁防治费" runat="server" />
                        <asp:HiddenField ID="hf_FId5" runat="server" Value="" />
                    </td>
                    <td>
                        <asp:TextBox ID="t5_Money" runat="server" CssClass="m_txt" MaxLength="40" Width="94%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t5_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t5_SKJBR" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t5_SKDW" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">新型墙体材料专项基金<input type="hidden" id="t6_JFXMBM" value="6" runat="server" />
                        <input type="hidden" id="t6_JFXM" value="新型墙体材料专项基金" runat="server" />
                        <asp:HiddenField ID="hf_FId6" runat="server" Value="" />
                    </td>
                    <td >
                        <asp:TextBox ID="t6_Money" runat="server" CssClass="m_txt" MaxLength="40" Width="94%"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="t6_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="t6_SKJBR" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="t6_SKDW" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">散装水泥费<input type="hidden" id="t7_JFXMBM" value="7" runat="server" />
                        <input type="hidden" id="t7_JFXM" value="散装水泥费" runat="server" />
                        <asp:HiddenField ID="hf_FId7" runat="server" Value="" />
                    </td>
                    <td>
                        <asp:TextBox ID="t7_Money" runat="server" CssClass="m_txt" Width="94%" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t7_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t7_SKJBR" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t7_SKDW" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">异地绿化费<input type="hidden" id="t8_JFXMBM" value="8" runat="server" />
                        <input type="hidden" id="t8_JFXM" value="异地绿化费" runat="server" />
                        <asp:HiddenField ID="hf_FId8" runat="server" Value="" />
                    </td>
                    <td>
                        <asp:TextBox ID="t8_Money" runat="server" CssClass="m_txt" Width="94%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t8_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t8_SKJBR" runat="server" CssClass="m_txt" Width="94%" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t8_SKDW" runat="server" CssClass="m_txt" Width="94%" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">防空地下室异地建设费<input type="hidden" id="t9_JFXMBM" value="9" runat="server" />
                        <input type="hidden" id="t9_JFXM" value="防空地下室异地建设费" runat="server" />
                        <asp:HiddenField ID="hf_FId9" runat="server" Value="" />
                    </td>
                    <td>
                        <asp:TextBox ID="t9_Money" runat="server" CssClass="m_txt" Width="94%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t9_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t9_SKJBR" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t9_SKDW" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_bg">农民工工资支付保证金担保情况<input type="hidden" id="t10_JFXMBM" value="10" runat="server" />
                        <input type="hidden" id="t10_JFXM" value="农民工工资支付保证金担保情况" runat="server" />
                        <asp:HiddenField ID="hf_FId10" runat="server" Value="" />
                    </td>
                    <td>
                        <asp:TextBox ID="t10_Money" runat="server" CssClass="m_txt" MaxLength="40" Width="94%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t10_JFSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t10_SKJBR" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="t10_SKDW" runat="server" CssClass="m_txt" Width="94%" MaxLength="40" ></asp:TextBox>
                    </td>
                </tr>
            </table>

        </div>
        <br />
        <br />
    </form>
</body>
</html>
