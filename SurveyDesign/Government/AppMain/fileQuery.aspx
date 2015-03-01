<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fileQuery.aspx.cs" Inherits="Government_AppMain_fileQuery" %>

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
    <script type="text/javascript">
        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";

            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);
        }
        function getFilUp(url, type) {
            var fid = document.getElementById("t_YWBM").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?FAppId=" + fid + "&see=1&type=" + type, 550, 400);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" id="file" runat="server" align="center" class="m_title">
            <tr>
                <th colspan="4">接件材料清单：                    
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
                                <asp:CheckBox ID="cbBaoBiao" runat="server" Enabled="False" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">省工法报表
                            </td>
                            <td class="t_r t_bg" style="width: 50px;"></td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lBaoBiao" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbBaoBiao" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_BaoBiao" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">2
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID0" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">3
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbFYJ" runat="server" Enabled="False" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">企业级工法批准文件复印件
                            </td>
                            <td class="t_r t_bg" style="width: 50px;"></td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lFYJ" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbFYJ" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FYJ" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">4
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb4" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ4" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID4" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">5
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb2" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ2" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID2" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">6
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb7" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ7" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID7" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">7
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb1" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ1" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID1" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">8
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cbCL" runat="server" Enabled="False" />
                            </td>
                            <td class="t_r t_bg" style="width: 200px; text-align: center;">专业技术情报部门提供的科技查新报告复印件
                            </td>
                            <td class="t_r t_bg" style="width: 50px;"></td>
                            <td class="t_r t_bg" style="width: 80px; text-align: left;">
                                <asp:Literal ID="lCL" runat="server"></asp:Literal>
                            </td>
                            <td class="t_r t_bg" style="text-align: left;">
                                <asp:TextBox ID="tbCL" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_CL" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">9
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb5" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ5" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID5" runat="server" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">10
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb6" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ6" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID6" runat="server" type="hidden" /></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">4
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb3" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ3" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID3" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">11
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb8" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ8" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID8" runat="server" type="hidden" /></td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">10
                            </td>
                            <td class="t_r t_bg" style="width: 50px; text-align: center;">
                                <asp:CheckBox ID="cb9" runat="server" Enabled="False" />
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
                                <asp:TextBox ID="tbBZ9" Width="95%" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
                                <input id="t_FID9" runat="server" type="hidden" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input id="t_YWBM" runat="server" type="hidden" />
    </form>
</body>
</html>
