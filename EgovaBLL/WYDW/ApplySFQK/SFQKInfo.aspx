<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SFQKInfo.aspx.cs" Inherits="WYDW_ApplyCharge_Info" %>

<%@ Register Src="~/common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目基本情况</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv"> 
                <div style="position:absolute;left:40%;top:50%;background-color:peru;border:solid 3px red;">
                    <table  align="center">
                    <tr>
                    <td ><h1>正在保存数据</h1></td>
                    <td><img src="../../JSDW/../image/load2.gif" alt="请稍候"/></td>
                    </tr>
                                    
                    </table>
                </div>
                    </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        <div style="height: 100%; width: 100%;">
            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">人员配置及收费
                    </th>
                </tr>
                <tr runat="server" id="tr_his" visible="false">
                    <td class="t_r t_bg" width="15%">
                        <tt>历次变更记录：>历次变更记录：</tt>
                    </td>
                    <td class="t_l">
                        <asp:DropDownList ID="ddlHis" runat="server" AutoPostBack="true"
                            TabIndex="10">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
                <table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td></td>
                        <td class="t_r">
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                OnClientClick="return checkInfo();" />
                            <%--<input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />--%>
                        </td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>
                <%--<table class="m_table" width="98%" align="center">
                    <tr>
                        <td width="15%" class="t_r t_bg">相关业主大会：
                        </td>
                        <td width="25%">
                            <asp:TextBox runat="server" ID=""></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:Button Text="选择" ID="Btn_Search" runat="server"></asp:Button></td>
                        <td width="50%">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                </table>--%>
                <%--<table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td>收费标准
                        </td>
                        <td class="t_r"></td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>--%>
                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td width="15%" class="t_r t_bg">多层住宅面积：
                        </td>
                        <td colspan="1" width="25%"><asp:TextBox ID="t_DC_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                Width="80px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td width="10%">万平方米</td>
                        <td class="t_r t_bg" width="15%">多层住宅收费：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:TextBox ID="t_DC_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                Width="80px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td width="10%">元/平方米.月</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">多层住宅（带电梯）面积：
                        </td>
                        <td>
                            <asp:TextBox ID="t_DCDT_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>

                        </td>
                        <td>万平方米</td>
                        <td class="t_r t_bg">多层住宅（带电梯）收费：
                        </td>
                        <td>
                            <asp:TextBox ID="t_DCDT_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/平方米.月</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">高层住宅面积：
                        </td>
                        <td>
                            <asp:TextBox ID="t_GC_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>万平方米</td>
                        <td class="t_r t_bg">高层住宅收费：
                        </td>
                        <td>
                            <asp:TextBox ID="t_GC_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/平方米.月</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">公寓、别墅面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_BS_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>万平方米</td>
                        <td class="t_r t_bg">公寓、别墅收费：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_BS_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/平方米.月</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">办公楼或写字楼面积：
                        </td>
                        <td>
                            <asp:TextBox ID="t_BG_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>万平方米</td>
                        <td class="t_r t_bg">办公楼或写字楼收费：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_BG_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/平方米.月</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">商业用房面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_SY_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>万平方米</td>
                        <td class="t_r t_bg">商业用房收费：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_SY_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/平方米.月</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">工业用房面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_GY_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>万平方米</td>
                        <td class="t_r t_bg">工业用房收费：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_GY_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/平方米.月</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">其他类型面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_QT_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>万平方米</td>
                        <td class="t_r t_bg">其他类型收费：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_QT_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/平方米.月</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">露天车位面积：
                        </td>
                        <td>
                            <asp:TextBox ID="t_LTCW_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>平方米</td>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">露天车位个数：
                        </td>
                        <td>
                            <asp:TextBox ID="t_LTCW_GS" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>个</td>
                        <td class="t_r t_bg">露天车位收费：
                        </td>
                        <td>
                            <asp:TextBox ID="t_LTCW_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/个.月</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">地下车位面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_DXCW_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>平方米</td>
                        <td colspan="3"></td>
                    </tr>
                    <tr>

                        <td class="t_r t_bg">地下车位个数：
                        </td>
                        <td>
                            <asp:TextBox ID="t_DXCW_GS" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>个</td>
                        <td class="t_r t_bg">地下车位收费：
                        </td>
                        <td>
                            <asp:TextBox ID="t_DXCW_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/平方米.个</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">自行车车库面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_ZXC_MJ" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>平方米</td>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">自行车车库收费标准：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_ZXC_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/月.辆</td>
                        <td class="t_r t_bg">电瓶车收费：
                        </td>
                        <td>
                            <asp:TextBox ID="t_DPC_SF" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>元/月.辆</td>
                    </tr>
                </table>
                <table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td>人员配置
                        </td>
                        <td class="t_r"></td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>
                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td class="t_r t_bg" width="15%">经理人数：
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="t_RYPZ_JL" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td width="10%">人</td>
                        <td class="t_r t_bg" width="15%">行政后勤人数：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:TextBox ID="t_RYPZ_XZ" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td width="10%">人</td>
                    </tr>
                    <tr>

                        <td class="t_r t_bg">客服人数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_RYPZ_KF" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>人</td>
                        <td class="t_r t_bg">安全人数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_RYPZ_AQ" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>人</td>
                    </tr>
                    <tr>

                        <td class="t_r t_bg">维修人数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_RYPZ_WX" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>人</td>
                        <td class="t_r t_bg">保洁人数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_RYPZ_BJ" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>人</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">绿化人数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_RYPZ_LH" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td>人</td>
                        <td class="t_r t_bg">其它人数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_RYPZ_QT" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td>人</td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
