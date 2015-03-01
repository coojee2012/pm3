<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AcceptInfoJNCL.aspx.cs" Inherits="Government_AppMain_AcceptInfoJNCL" %>

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
                <td class="auto-style1" colspan="3">
                    <asp:TextBox ID="t_Fname" runat="server" CssClass="m_txt" Width="400px" Enabled="false"
                        TabIndex="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">业务名称：
                </td>
                <td>
                    <asp:TextBox ID="t_GFMC" runat="server" CssClass="m_txt" Width="151px" Enabled="false"
                        TabIndex="100"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSee" runat="server" Visible="false" CssClass="m_btn_w2" Text="查看" OnClick="btnSee_Click" />
                </td>
                <td class="t_r t_bg">上报日期：
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="t_FReportDate" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">联系人：
                </td>
                <td>
                    <asp:TextBox ID="t_Linkman" runat="server" CssClass="m_txt" Width="151px" Enabled="false"
                        TabIndex="100"></asp:TextBox>
                </td>
                <td class="t_r t_bg">电话：
                </td>
                <td class="txt34" style="height: 24px; width: 159px;">
                    <asp:TextBox ID="t_LinkmanMobile" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="m_title" id="sqInfo" runat="server">
            <tr>
                <th colspan="4">申请信息：
                </th>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="dgSQinfo" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                        HorizontalAlign="Center" OnItemDataBound="dgSQinfo_ItemDataBound" Style="margin-top: 7px"
                        Width="100%">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <ItemStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:BoundColumn HeaderText="序号">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="30px" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FRoleDesc" HeaderText="材料和产品名称">
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="300px" CssClass="padLeft" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="是否同意">
                                <ItemTemplate>
                                    <asp:DropDownList ID="dResult" runat="server" Width="60px" OnSelectedIndexChanged="dResult_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="1">同意</asp:ListItem>
                                        <asp:ListItem Value="-1">不同意</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
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
        <table width="100%" id="file" runat="server" align="center" class="m_title">
            <tr>
                <th colspan="4">材料清单：
                     <asp:Button ID="btnQuery" runat="server" CssClass="cBtn7" Text="刷新" OnClick="btnQuery_Click"
                         Style="display: none" />
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CssClass="m_btn_w2" Text="全选" OnClick="btnAll_Click" />
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
                            <td class="m_dg1_h" style="width: 260px; text-align: center;">材料名称
                            </td>
                            <td class="m_dg1_h" style="width: 50px;">查看
                            </td>
                            <td class="m_dg1_h" style="width: 80px;">检验状态
                            </td>
                            <td class="m_dg1_h">备注
                            </td>
                        </tr>
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">1
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbOne" runat="server" />
                            </td>
                            <td class="m_dg1_h" style="width: 260px; text-align: center;">《四川省建筑节能材料和产品备案表》（见附表）
                            </td>
                            <td class="m_dg1_h" style="width: 50px;">
                                <input id="btnOne" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../JNCLEnt/ApplyInfo/FileUp.aspx", "3005");' />
                            </td>
                            <td class="m_dg1_h" style="width: 80px;">
                                <asp:Literal ID="lOne" runat="server"></asp:Literal>
                            </td>
                            <td class="m_dg1_h">
                                <asp:TextBox ID="tbOne" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_One" runat="server" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">2
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbTwo" runat="server" />
                            </td>
                            <td class="m_dg1_h" style="width: 200px; text-align: center;">企业营业执照（复印件加盖企业印章）
                            </td>
                            <td class="m_dg1_h" style="width: 50px;">
                                <input id="btnTwo" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../JNCLEnt/ApplyInfo/FileUp.aspx", "3000");' />
                            </td>
                            <td class="m_dg1_h" style="width: 80px;">
                                <asp:Literal ID="lTwo" runat="server"></asp:Literal>
                            </td>
                            <td class="m_dg1_h">
                                <asp:TextBox ID="tbTwo" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_Two" runat="server" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">3
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbThree" runat="server" />
                            </td>
                            <td class="m_dg1_h" style="width: 200px; text-align: center;">代理商的代理合同
                            </td>
                            <td class="m_dg1_h" style="width: 50px;">
                                <input id="btnThree" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../JNCLEnt/ApplyInfo/FileUp.aspx", "3001");' />
                            </td>
                            <td class="m_dg1_h" style="width: 80px;">
                                <asp:Literal ID="lThree" runat="server"></asp:Literal>
                            </td>
                            <td class="m_dg1_h">
                                <asp:TextBox ID="tbThree" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_Three" runat="server" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">4
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbFour" runat="server" />
                            </td>
                            <td class="m_dg1_h" style="width: 200px; text-align: center;">企业简介及主要生产、检测设备清单（须加盖企业公章。必要时，建设行业主管部门将会同质量技术监督部门到企业生产现场查看）
                            </td>
                            <td class="m_dg1_h" style="width: 50px;"></td>
                            <td class="m_dg1_h" style="width: 80px;"></td>
                            <td class="m_dg1_h"></td>
                        </tr>
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">5
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbFive" runat="server" />
                            </td>
                            <td class="m_dg1_h" style="width: 200px; text-align: center;">产品执行标准（若是企业标准，则须提供经省级质量技术监督部门备案以及产品通过省级以上技术鉴定的相关材料）
                            </td>
                            <td class="m_dg1_h" style="width: 50px;"></td>
                            <td class="m_dg1_h" style="width: 80px;"></td>
                            <td class="m_dg1_h"></td>
                        </tr>
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">6
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbSix" runat="server" />
                            </td>
                            <td class="m_dg1_h" style="width: 200px; text-align: center;">设计施工验收技术规程（规范、标准）、标准图集、使用手册等相关技术资料
                            </td>
                            <td class="m_dg1_h" style="width: 50px;">
                                <input id="btnSix" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../JNCLEnt/ApplyInfo/FileUp.aspx", "3002");' />
                            </td>
                            <td class="m_dg1_h" style="width: 80px;">
                                <asp:Literal ID="lSix" runat="server"></asp:Literal>
                            </td>
                            <td class="m_dg1_h">
                                <asp:TextBox ID="tbSix" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_Six" runat="server" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">7
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbSeven" runat="server" />
                            </td>
                            <td class="m_dg1_h" style="width: 200px; text-align: center;">法定检测机构出具的有效期内的产品型式检验报告（查验原件，收复印件，复印件须加盖企业公章）
                            </td>
                            <td class="m_dg1_h" style="width: 50px;">
                                <input id="btnSeven" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../JNCLEnt/ApplyInfo/FileUp.aspx", "3003");' />
                            </td>
                            <td class="m_dg1_h" style="width: 80px;">
                                <asp:Literal ID="lSeven" runat="server"></asp:Literal>
                            </td>
                            <td class="m_dg1_h">
                                <asp:TextBox ID="tbSeven" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_Seven" runat="server" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">8
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbEight" runat="server" />
                            </td>
                            <td class="m_dg1_h" style="width: 200px; text-align: center;">质量管理有关资料或质量保证体系认证证书
                            </td>
                            <td class="m_dg1_h" style="width: 50px;">
                                <input id="Button1" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../JNCLEnt/ApplyInfo/FileUp.aspx", "3004");' />
                            </td>
                            <td class="m_dg1_h" style="width: 80px;">
                                <asp:Literal ID="lEight" runat="server"></asp:Literal>
                            </td>
                            <td class="m_dg1_h">
                                <asp:TextBox ID="tbEight" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_Eight" runat="server" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">9
                            </td>
                            <td class="m_dg1_h" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbNight" runat="server" />
                            </td>
                            <td class="m_dg1_h" style="width: 200px; text-align: center;">省外进入我省的建筑节能材料和产品除提供上述资料外，还应提供当地省级建设行政主管部门备案(认定)证明
                            </td>
                            <td class="m_dg1_h" style="width: 50px;">
                                <input id="btnNight" runat="server" type="button" class="m_btn_w2" value="查看" onclick='getFilUp("../../JNCLEnt/ApplyInfo/FileUp.aspx", "3006");' />
                            </td>
                            <td class="m_dg1_h" style="width: 80px;">
                                <asp:Literal ID="lNight" runat="server"></asp:Literal>
                            </td>
                            <td class="m_dg1_h">
                                <asp:TextBox ID="tbNight" Width="95%" runat="server" CssClass="m_txt"></asp:TextBox>
                                <input id="t_Night" runat="server" type="hidden" />
                            </td>
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
        <table width="100%" align="center" class="m_title" id="accept" runat="server" visible="false">
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
            &nbsp;&nbsp;<asp:Button ID="btnBackNext" runat="server" Visible="false" CssClass="m_btn_w2" Style="margin-left: 5px;" Text="打回" OnClick="btnBackNext_Click" />
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
