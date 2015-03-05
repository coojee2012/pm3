<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FirstAcceptJieJian.aspx.cs" Inherits="JSDW_ApplyXZYJS_AuditMain_FirstAcceptJieJian" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
     <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        function ChooseFile(typeId, YJS_ID, typeName) {
            var url = "../AppXZYJS/file.aspx?typeId=" + typeId + "&YJS_ID=" + YJS_ID + "&typeName=" + typeName;
            showAddWindow(url, 500, 200);
        }
        function FindUpStuff() {
            var YWBM = $("#hfFLinkId").val();
            var Id = $("#hfId").val();
            var url = "";
            if ($("#hfProjectType").val() == "1")//房建
                url = "../../JSDW/ApplyYDGH/AppMain/index.aspx?YD_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            else//市政
                url = "../../JSDW/ApplyYDGHSZ/AppMain/index.aspx?YD_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            showAddWindow(url, 1000, 500);
        }
        function BackApp(url) {
            var tmpVal = $("#hfFLinkId").val();
            var fsubid = $("#hfFlowId").val();
            showAddWindow(url + '?ftype=' + $("#hfType").val() + '&FLinkId=' + tmpVal + '&fSubFlowId=' + fsubid, 700, 600);
            return true;
        }
        $(function () {
            $("#CLQQ").find("input[type=checkbox]").attr("disabled", "false");
            $("#CLQQ").find("input[type=text]").attr("disabled", "false");
            var type = $("#hfType").val();
            if (type == "5")
                $(".Table1").show();
            else
                $(".Table1").hide();
        });
        function Audit() {
            var type = $("#hfType").val();
            if (type == "5") {
                var YDGHBH = $("#txtYDGHXKZBH").val();
                var HFJG = $("#txtHFJG").val();
                var HFRQ = $("#txtHFRQ").val();
                if ($.trim(YDGHBH).length == 0) {
                    alert("用地规划许可证编号不能为空");
                    return false;
                } else if ($.trim(HFJG).length == 0) {
                    alert("核发机关不能为空");
                    return false;
                } else if ($.trim(HFRQ).length == 0) {
                    alert("核发日期不能为空");
                    return false;
                }
                return true;
            }
            return true;
        }
        $(function () {
            $("#btnAccept").click(function () {
                var success = Audit();
                if (success) {
                    var text = $("#btnAccept").val();
                    if (text == "提交中...") {
                        return false;
                    }
                    $("#btnAccept").val("提交中...");
                    return true;
                }
                return false;
            });
        });
    </script>

    <%--<script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>--%>

    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfType" runat="server" />
    <asp:HiddenField ID="hfXMBM" runat="server" />
    <asp:HiddenField ID="hfYWBM" runat="server" />
         <asp:HiddenField ID="hfFLinkId" runat="server" />
        <asp:HiddenField ID="hfFlowId" runat="server" />
        <asp:HiddenField ID="hfFile" runat="server" />
        <asp:HiddenField ID="hfFunction" runat="server" />
        <asp:HiddenField ID="hfProcessRecordFID" runat="server" />
        <asp:HiddenField ID="hfProcessInstanceID" runat="server" />
        <asp:HiddenField ID="hfId" runat="server" />
        <asp:HiddenField ID="hfProjectType" runat="server" />
        <asp:HiddenField ID="hfXMBH" runat="server" />
    <table width="98%" align="center" class="m_title" id="m_title">
        <tr>
            <th colspan="5"><asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">项目名称</td>
            <td><asp:TextBox ID="txtXMMC" runat="server" Enabled="false" Width="280"></asp:TextBox></td>
            <td class="t_r t_bg">项目编号</td>
            <td><asp:TextBox ID="txtBH" runat="server" Enabled="false" Width="300"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">建设单位</td>
            <td colspan="3"><asp:TextBox ID="txtJSDWMC" Enabled="false" runat="server" Width="280"></asp:TextBox></td>
        </tr>
        <tr>
             <td class="t_r t_bg">建设地址</td>
            <td colspan="3"><asp:TextBox ID="txtJSDWDZ" Enabled="false" runat="server" Width="280"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">联系人</td>
            <td><asp:TextBox ID="txtLXR" runat="server" Enabled="false" Width="280"></asp:TextBox></td>
            <td class="t_r t_bg">联系电话</td>
            <td><asp:TextBox ID="txtLXDH" runat="server" Enabled="false" Width="300"></asp:TextBox></td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_title">
        <tr>
            <td>
                <input type="button" value="【查看上报材料】" onclick="FindUpStuff()" class="m_btn_w12" />
            </td>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" id="CLQQ" class="m_title">
            <tr>
                <th colspan="6">材料情况
                </th>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 30px; text-align: center;">序号</td>
                <td class="t_r t_bg" style="width: 200px; text-align: center;">材料名称</td>
                <td class="t_r t_bg" style="width: 30px;text-align: center;">份数</td>
                <td class="t_r t_bg" style="width:50px;text-align: center;"><input type="checkbox" />是否具备</td>
                <td class="t_r t_bg" style="width:50px;text-align: center;">电子件</td>
                <td class="t_r t_bg" style="width:auto;text-align: center;">备注</td>
            </tr>
            <asp:Literal ID="ltrText" runat="server"></asp:Literal>
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
                            <asp:BoundColumn DataField="FAppPerson" HeaderText="审查人">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FCompany" HeaderText="审查机构">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FFunction" HeaderText="审查人职务">
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="70px" CssClass="padLeft" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="审查日期">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="90px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FResult" HeaderText="审查结果">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FIdea" HeaderText="审查意见">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FRoleDesc" HeaderText="审查环节">
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
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="6" style="text-align: left;">审核意见
                </th>
            </tr>
            <tr>
                <td class="t_r">审查人
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">审查人职务
                </td>
                <td>
                    <asp:TextBox ID="txtFunction" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r">审查单位
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">是否受理
                </td>
                <td>
                    <asp:DropDownList ID="dResult" runat="server" CssClass="m_txt">
                        <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不通过" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">审查时间
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr class="Table1">
                 <td class="t_r">用地规划许可证编号</td>
                <td colspan="5">地字第（<asp:TextBox ID="txtYDGHXKZBH" runat="server" Width="200"></asp:TextBox>）号</td>
            </tr>
            <tr class="Table1">
                <td class="t_r">核发机关</td>
                <td><asp:TextBox ID="txtHFJG" runat="server"></asp:TextBox></td>
                <td class="t_r" colspan="3">核发日期</td>
                <td><asp:TextBox ID="txtHFRQ" runat="server"  CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t_r">审查意见
                </td>
                <td colspan="5">
                    <asp:TextBox ID="t_FAppIdea" runat="server" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                        Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin-top: 2PX">
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClientClick="return Audit();" OnClick="btnSave_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnAccept" runat="server" CssClass="m_btn_w4" Text="上报审批" OnClick="btnAccept_Click" />
            &nbsp;&nbsp;<%--<asp:Button ID="btnAccept1" runat="server"  class="m_btn_w4" Text="打回企业" />--%>
         <%--   <input type="button" value="打回企业"   class="m_btn_w4" onclick="BackApp('BackAccept.aspx')" />--%>
            <%--<asp:Button ID="btnExit" runat="server" Text="打回企业" CssClass="m_btn_w4" OnClick="btnExit_Click" />--%>
     <%--       &nbsp;&nbsp;<input id="btnUPCS" runat="server" class="m_btn_w4" onclick="if (WriteInfo(this)) { document.getElementById('btnCS').click(); }"
                type="button" value="不予受理" />
            &nbsp;&nbsp;<input id="btnUPFS" runat="server" class="m_btn_w4" onclick="if (WriteInfo(this)) { document.getElementById('btnFS').click(); }"
                type="button" value="打回企业" />--%>
            &nbsp;&nbsp;<input id="btnReturn" type="button" class="m_btn_w2" value="返回" onclick="window.returnValue = '1';window.close();" />
        </div>
        </div>
    </form>
</body>
</html>
