<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AcceptInfoGF.aspx.cs" Inherits="Government_AppMain_AcceptReportInfoGF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";

            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);
            window.returnValue = '1';
            if (idvalue == "1") {
                form1.btnReturn.click();
            }
        }
        function WriteInfo(obj) {
            obj.disabled = true;
            obj.className = "m_btn_w6"
            obj.value = '提交中，请等待';
            return true;
        }

        function app(url) {
            var obj = new Object();
            obj.name = '';
            obj.id = document.getElementById("t_YWBM").value;
            ShowWindow(url, 700, 600, obj);
            return false
        }

        function getFilUp(url, type) {
            var fid = document.getElementById("t_YWBM").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?FAppId=" + fid + "&see=1&type=" + type, 550, 400);
        }

        function checkJW() {
            var ftype = document.getElementById("t_ftype").value;
            if (ftype == "5") {
                if (document.getElementById("t_FNu").value == "") {
                    alert("请填写工法编号");
                    return false;
                }
                if (document.getElementById("t_Fwh").value == "") {
                    alert("请填写省级工法批准文号");
                    return false;
                }
                if (document.getElementById("t_Fpztime").value == "") {
                    alert("请填选择批准时间");
                    return false;
                }
                if (document.getElementById("t_FDep").value == "") {
                    alert("请填颁发部门");
                    return false;
                }
                return true;
            }
        }
        function ShowFile(url) {
            var fid = document.getElementById("t_YWBM").value;
            var type = document.getElementById("t_ftype").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?FAppId=" + fid + "&see=1&type=" + type, 600, 500);
        }
    </script>
    <style type="text/css">
        .cBtn7 {
            height: 21px;
        }

        .auto-style1 {
            height: 24px;
        }

        .auto-style2 {
            width: 159px;
            height: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" align="center" class="m_title">
            <tr>
                <th colspan="4">
                    <asp:Label ID="lbTile" runat="server"></asp:Label>
                </th>
            </tr>
            <tr>
                <td class="t_r t_bg">申报单位：
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="t_Fname" runat="server" CssClass="m_txt" Width="151px" Enabled="false"
                        TabIndex="100"></asp:TextBox>
                </td>
                <td class="t_r t_bg">上报日期：
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="t_FReportDate" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">工法名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_GFMC" runat="server" CssClass="m_txt" Width="400px" Enabled="false"
                        TabIndex="100"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSee" runat="server" CssClass="m_btn_w2" Text="查看" OnClick="btnSee_Click" />
                    &nbsp;<asp:Button ID="btnFile" runat="server" CssClass="m_btn_w4" Text="材料清单" OnClientClick="return ShowFile('fileQuery.aspx')" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">类别：
                </td>
                <td>
                    <asp:TextBox ID="t_FListName" runat="server" CssClass="m_txt" Width="151px" Enabled="false"
                        TabIndex="100"></asp:TextBox>
                </td>
                <td class="t_r t_bg">专业分类：
                </td>
                <td class="txt34" style="height: 24px; width: 159px;">
                    <asp:TextBox ID="t_FTypeName" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">联系人：
                </td>
                <td>
                    <asp:TextBox ID="t_Linkman" runat="server" CssClass="m_txt" Width="151px" Enabled="false"
                        TabIndex="100"></asp:TextBox>
                </td>
                <td class="t_r t_bg">联系人手机：
                </td>
                <td class="txt34" style="height: 24px; width: 159px;">
                    <asp:TextBox ID="t_LinkmanMobile" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="m_title" width="98%" align="center">
            <tr>
                <td class="m_dg1_h" style="width: 400px; text-align: center;">审查要素
                </td>
                <td class="m_dg1_h" style="width: 130px; text-align: center;">是否符合
                </td>
                <td class="m_dg1_h" style="width: 100px; text-align: center;">备注
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 400px; text-align: left;">申报材料形式审查（一）
                </td>
                <td class="t_r t_bg" style="width: 130px; text-align: center;">
                    <asp:RadioButtonList ID="rblOne" Width="100%" runat="server" RepeatDirection="Horizontal" Height="26px">
                        <asp:ListItem Value="0">符合</asp:ListItem>
                        <asp:ListItem Value="1">不符合</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t_r t_bg" style="width: 100px; text-align: center;">
                    <asp:TextBox ID="tbOne" runat="server"></asp:TextBox>
                    <input id="ts_one" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 400px; text-align: left;">申报的工法为本省（部）级公布的工法，其文号和题目与省（部）级工法批准文件一致
                </td>
                <td class="t_r t_bg" style="width: 130px; text-align: center;">
                    <asp:RadioButtonList ID="rblTWO" Width="100%" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">符合</asp:ListItem>
                        <asp:ListItem Value="1">不符合</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t_r t_bg" style="width: 100px; text-align: center;">
                    <asp:TextBox ID="tbTWO" runat="server"></asp:TextBox>
                    <input id="ts_TWO" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 400px; text-align: left;">申报单位与工法的主要完成单位一致，申报单位应是研制开发应用工法的主要完成单位
                </td>
                <td class="t_r t_bg" style="width: 130px; text-align: center;">
                    <asp:RadioButtonList ID="rblThree" Width="100%" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">符合</asp:ListItem>
                        <asp:ListItem Value="1">不符合</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t_r t_bg" style="width: 100px; text-align: center;">
                    <asp:TextBox ID="tbThree" runat="server"></asp:TextBox>
                    <input id="ts_Three" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 400px; text-align: left;">工法的主要完成单位及主要完成人不存在争议，已提供工法主要完成单位盖章、主要完成人签字的工法无争议声明书
                </td>
                <td class="t_r t_bg" style="width: 130px; text-align: center;">
                    <asp:RadioButtonList ID="rblFour" Width="100%" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">符合</asp:ListItem>
                        <asp:ListItem Value="1">不符合</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t_r t_bg" style="width: 100px; text-align: center;">
                    <asp:TextBox ID="tbFour" runat="server"></asp:TextBox>
                    <input id="ts_Four" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 400px; text-align: left;">工法的主要完成单位及主要完成人与省（部）级工法批准文件一致，主要完成人员不超过5人
                </td>
                <td class="t_r t_bg" style="width: 130px; text-align: center;">
                    <asp:RadioButtonList ID="rblFive" Width="100%" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">符合</asp:ListItem>
                        <asp:ListItem Value="1">不符合</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t_r t_bg" style="width: 100px; text-align: center;">
                    <asp:TextBox ID="tbFive" runat="server"></asp:TextBox>
                    <input id="ts_Five" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 400px; text-align: left;">工法申报表与工法材料一致
                </td>
                <td class="t_r t_bg" style="width: 130px; text-align: center;">
                    <asp:RadioButtonList ID="rblSix" Width="100%" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">符合</asp:ListItem>
                        <asp:ListItem Value="1">不符合</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t_r t_bg" style="width: 100px; text-align: center;">
                    <asp:TextBox ID="tbSix" runat="server"></asp:TextBox>
                    <input id="ts_Six" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 400px; text-align: left;">工法文本及附件材料符合申报通知要求，反映实际施工中工法操作要点的照片（10张）
                </td>
                <td class="t_r t_bg" style="width: 130px; text-align: center;">
                    <asp:RadioButtonList ID="rblSeven" Width="100%" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">符合</asp:ListItem>
                        <asp:ListItem Value="1">不符合</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t_r t_bg" style="width: 100px; text-align: center;">
                    <asp:TextBox ID="tbSeven" runat="server"></asp:TextBox>
                    <input id="ts_Seven" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 400px; text-align: left;">工法文本中11章内容齐全
                </td>
                <td class="t_r t_bg" style="width: 130px; text-align: center;">
                    <asp:RadioButtonList ID="rblEghit" Width="100%" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">符合</asp:ListItem>
                        <asp:ListItem Value="1">不符合</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t_r t_bg" style="width: 100px; text-align: center;">
                    <asp:TextBox ID="tbEghit" runat="server"></asp:TextBox>
                    <input id="ts_Eghit" runat="server" type="hidden" />
                </td>
            </tr>
        </table>
        <table width="100%" id="file" runat="server" style="display: none;" align="center" class="m_title">
            <tr>
                <th colspan="4">材料清单：
                     <asp:Button ID="btnQuery" runat="server" CssClass="cBtn7" Text="刷新" OnClick="btnQuery_Click"
                         Style="display: none" />
                </th>
            </tr>
            <tr>
                <td>
                    <table class="m_table" width="98%" align="center">
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">序号
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">具备
                            </td>
                            <td class="m_dg1_h" style="width: 200px; text-align: center;">材料名称
                            </td>
                            <td class="m_dg1_h" style="width: 50px;">查看
                            </td>
                            <td class="m_dg1_h" style="width: 80px;">检验状态
                            </td>
                            <td class="m_dg1_h">备注
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">1
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbBaoBiao" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">省工法报表
                            </td>
                            <td class="t_r t_bg" style="width: 50px;"></td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lBaoBiao" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBaoBiao" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_BaoBiao" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">2
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">工法内容材料
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1000");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID0" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">3
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbFYJ" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">企业级工法批准文件复印件
                            </td>
                            <td class="t_r t_bg" style="width: 50px;"></td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lFYJ" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbFYJ" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FYJ" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">4
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb4" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">关键技术评估意见
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP4" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1006");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY4" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ4" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID4" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">5
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb2" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">工程应用证明相关附件
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP2" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1002");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY2" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ2" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID2" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">6
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb7" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">经济效益证明
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP7" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1005");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY7" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ7" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID7" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">7
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb1" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">完成单位意见、无争议声明相关附件
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP1" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1001");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY1" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ1" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID1" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">8
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbCL" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">专业技术情报部门提供的科技查新报告复印件
                            </td>
                            <td class="t_r t_bg" style="width: 50px;"></td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lCL" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbCL" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_CL" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">9
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb5" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">科技成果获奖证明相关附件
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP5" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1004");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY5" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ5" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID5" runat="server" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">10
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb6" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">专业技术专利证明文件
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP6" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1007");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY6" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ6" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID6" runat="server" type="hidden" /></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">4
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb3" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">工法成熟可靠性说明文件
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP3" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1003");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY3" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ3" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID3" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">11
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb8" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">工法操作要点照片（10到15张）
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP8" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1008");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY8" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ8" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID8" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">10
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb9" runat="server" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">技术转让的证明材料
                            </td>
                            <td class="t_r t_bg" style="width: 50px;">
                                <input id="btnUP9" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../GFEnt/ApplyEnt/FileUp.aspx", "1009");' />
                            </td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lJY9" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBZ9" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_FID9" runat="server" type="hidden" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="m_title">
            <tr>
                <th colspan="4">各级审批意见：
                </th>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
                        Width="100%">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <ItemStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:BoundColumn HeaderText="序号">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="30px" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FRoleDesc" HeaderText="审批岗位">
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="70px" CssClass="padLeft" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppPerson" HeaderText="审批人">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FCompany" HeaderText="审批人单位">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="审批日期">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="90px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FResult" HeaderText="审批结果">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FIdea" HeaderText="审批意见">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="m_title" id="accept" runat="server" visible="true">
            <tr>
                <th colspan="4" style="text-align: left;">接件意见
                </th>
            </tr>
            <tr>
                <td class="t_r">接件人
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">接件单位
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt"></asp:TextBox>
                    <asp:TextBox ID="t_FAppPersonJob" runat="server" CssClass="m_txt" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">是否受理
                </td>
                <td>
                    <asp:DropDownList ID="dResult" runat="server" CssClass="m_txt">
                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">接件时间
                </td>
                <td>
                    <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">接件意见
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FAppIdea" runat="server" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                        Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="m_title" id="jiuwei" runat="server" visible="false">
            <tr>
                <th colspan="3" style="text-align: left;">就位工法证书
                    &nbsp;&nbsp;&nbsp;<asp:Button Visible="false" ID="btnJW" runat="server" CssClass="m_btn_w2" Text="就位" OnClick="btnJW_Click" />
                </th>
            </tr>
            <tr>
                <td class="t_r">工法编号
                </td>
                <td>
                    <asp:TextBox ID="t_FNu" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r">省级工法批准文号
                </td>
                <td>
                    <asp:TextBox ID="t_Fwh" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r">批准时间
                </td>
                <td>
                    <asp:TextBox ID="t_Fpztime" onfocus="WdatePicker()" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r">颁发部门
                </td>
                <td>
                    <asp:TextBox ID="t_FDep" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="m_title" id="oneAudit" runat="server" visible="false">
            <tr>
                <th colspan="4" style="text-align: left;">本级审批意见
                </th>
            </tr>
            <tr>
                <td class="t_r">审批人
                </td>
                <td>
                    <asp:TextBox ID="t_Auditer" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">审批单位
                </td>
                <td>
                    <asp:TextBox ID="t_AuditUnit" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">审批人职务
                </td>
                <td>
                    <asp:TextBox ID="t_AuditFunction" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">审批时间
                </td>
                <td>
                    <asp:TextBox ID="t_AuditTime" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">是否审核通过
                </td>
                <td>
                    <asp:DropDownList ID="dAudit" runat="server" CssClass="m_txt">
                        <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                        <asp:ListItem Text="打回" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r">审批意见
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_AuditIdear" runat="server" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                        Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin-top: 2PX">
            <asp:Button ID="btnSaveYJ" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSaveYJ_Click" />
            &nbsp;&nbsp;<input id="btnSave" runat="server" class="m_btn_w4" onclick="if (WriteInfo(this)) { document.getElementById('btnAccept').click(); }"
                type="button" value="同意接件" />
            &nbsp;&nbsp;<input id="btnUPCS" runat="server" class="m_btn_w4" onclick="if (WriteInfo(this)) { document.getElementById('btnCS').click(); }"
                type="button" value="初审提交" />
            &nbsp;&nbsp;<input id="btnUPFS" runat="server" class="m_btn_w4" onclick="if (WriteInfo(this)) { document.getElementById('btnFS').click(); }"
                type="button" value="复审提交" />
            &nbsp;&nbsp;<input id="btnUPEND" runat="server" class="m_btn_w2" onclick="if (WriteInfo(this)) { document.getElementById('btnEnd').click(); }"
                type="button" value="办结" />
            &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w4"
                Text="打回企业" OnClientClick="return app('BackSeeOneReportInfo.aspx?type=1')" />
            &nbsp;&nbsp;<asp:Button ID="btnBackNext" runat="server" CssClass="m_btn_w2" Style="margin-left: 5px;" Text="打回" OnClick="btnBackNext_Click" />
            &nbsp;&nbsp;<input id="btnReturn" type="button" runat="server" class="m_btn_w2" value="返回" onclick="window.close();" />
        </div>
        <asp:Button ID="btnAccept" runat="server" CssClass="cBtn7" Text="接件" OnClick="btnAccept_Click"
            Style="display: none" />
        <asp:Button ID="btnCS" runat="server" CssClass="cBtn7" Text="初审" OnClick="btnCS_Click"
            Style="display: none" />
        <asp:Button ID="btnFS" runat="server" CssClass="cBtn7" Text="复审" OnClick="btnFS_Click"
            Style="display: none" />
        <asp:Button ID="btnEnd" runat="server" CssClass="cBtn7" Text="办结" OnClick="btnEnd_Click"
            Style="display: none" />
        <input id="t_YWBM" runat="server" type="hidden" />
        <input id="t_ftype" runat="server" type="hidden" />
        <input id="t_fSubFlowId" runat="server" type="hidden" />
        <input id="t_ProcessRecordID" runat="server" type="hidden" />
        <input id="t_FProcessInstanceID" runat="server" type="hidden" />
    </form>
</body>
</html>
