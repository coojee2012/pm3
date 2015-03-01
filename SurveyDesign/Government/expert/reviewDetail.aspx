<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reviewDetail.aspx.cs" Inherits="Government_expert_reviewDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <style type="text/css">
        .m_txt {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">专家评审</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">申报单位：
                </td>
                <td align="left">
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="169px" Enabled="False"></asp:TextBox>
                </td>
                <td class="t_r">上报日期：
                </td>
                <td>
                    <asp:TextBox ID="t_FReportDate" runat="server" CssClass="m_txt" Width="169px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">工法名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_GFMC" runat="server" CssClass="m_txt" Width="400px" Enabled="False"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Button ID="btnSee" runat="server" CssClass="m_btn_w2" Text="查看" OnClick="btnSee_Click" />
                </td>
            </tr>
            <tr>
                <td class="t_r">类别：
                </td>
                <td align="left">
                    <asp:TextBox ID="t_FListName" runat="server" CssClass="m_txt" Width="169px" Enabled="False"></asp:TextBox>
                </td>
                <td class="t_r">专业分类：
                </td>
                <td>
                    <asp:TextBox ID="t_FTypeName" runat="server" CssClass="m_txt" Width="169px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">联系人：
                </td>
                <td align="left">
                    <asp:TextBox ID="t_Linkman" runat="server" CssClass="m_txt" Width="169px" Enabled="False"></asp:TextBox>
                </td>
                <td class="t_r">联系人手机：
                </td>
                <td>
                    <asp:TextBox ID="t_LinkmanMobile" runat="server" CssClass="m_txt" Width="169px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table class="m_table" width="98%" align="center">
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
                                <asp:RadioButtonList ID="rblOne" Width="100%" runat="server" RepeatDirection="Horizontal">
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
                </td>
            </tr>
            <tr>
                <td class="t_r">审批意见：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_Fresult" runat="server" CssClass="m_txt" Height="280px" Width="500px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <asp:Button ID="btnOK" runat="server" CssClass="m_btn_w2" Text="通过" OnClick="btnOK_Click" />
                    <asp:Button ID="btnNo" runat="server" CssClass="m_btn_w2" Text="不通过" OnClick="btnNo_Click" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
            </tr>
        </table>
        <input id="t_YWBM" runat="server" type="hidden" />
        <input id="t_psid" runat="server" type="hidden" />
        <input id="t_isEnd" runat="server" type="hidden" />

    </form>
</body>
</html>
