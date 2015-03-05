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
        function ChooseFile(typeId, GC_Id, typeName) {
            var url = "../AppXZYJS/file.aspx?typeId=" + typeId + "&YJS_ID=" + GC_Id + "&typeName=" + typeName;
            showAddWindow(url, 500, 200);
        }
        function Save(btnId) {
            var items = $("#CLQQ").find(".clDetail");
            var array = new Array();
            $.each(items, function (index, item) {
                var arrayTD = $(item).find("td");
                var fileId = $(arrayTD[0]).attr("value");
                var isHave = $(arrayTD[3]).find("input[type=checkbox]").eq(0).attr("checked") == true ? 1 : 0;
                var reMark = $(arrayTD[5]).find("input[type=text]").eq(0).val();
                array.push(fileId + "-" + isHave + "-" + reMark);
            });

            $("#hfFile").val(array.join('|'));
            var text = $("#" + btnId).val();
            if (text == "提交中...") {
                return false;
            }
            $("#" + btnId).val("提交中...");
            return true;
        } 
        function FindUpStuff() {
            var YWBM = $("#hfFLinkId").val();
            var Id = $("#hfId").val();
            var url = "";
            if ($("#hfProjectType").val() == "1")//房建
                url = "../../JSDW/ApplyGCGH/AppMain/index.aspx?GC_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            else//市政
                url = "../../JSDW/ApplyGCGHSZ/AppMain/index.aspx?GC_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            showAddWindow(url, 1000, 500);
        }
        function BackApp(url) {
            var tmpVal = $("#hfFLinkId").val();
            var fsubid = $("#hfFlowId").val();
            showAddWindow(url + '?FLinkId=' + tmpVal + '&fSubFlowId=' + fsubid, 700, 600);
            return true;
        }
        $(function () {
            $("#allFile").click(function () {
                $("#CLQQ").find("input[type=checkbox]").attr("checked", $(this).attr("checked"));
            });
            if ($("#hfTypeId").val() != "1")//不是接件 材料情况复选框、文本框为只读
            {
                $("#CLQQ").find("input[type=checkbox]").attr("disabled", "false");
                $("#CLQQ").find("input[type=text]").attr("disabled", "false");
            }
//            $("#btnAccept,#btnSave").click(function () {
//                if (Save()) {
//                    return true;
//                }
//                return false;
//            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfTypeId" runat="server" />
        <table width="100%" align="center" class="m_title">
            <tr>
                <th colspan="4">
                    <asp:Label ID="lbTile" runat="server"></asp:Label>
                </th>
            </tr>
            <tr>
                <td class="t_r t_bg">生产企业：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtQYMC" runat="server" CssClass="m_txt" Width="400px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">法人代表：
                </td>
                <td>
                    <asp:TextBox ID="txtFRDB" runat="server" CssClass="m_txt" Width="151px" Enabled="false"></asp:TextBox>&nbsp;&nbsp;
                </td>
                <td class="t_r t_bg">上报日期：
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtSBRQ" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">联系人：
                </td>
                <td>
                    <asp:TextBox ID="txtLXR" runat="server" CssClass="m_txt" Width="151px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">电话：
                </td>
                <td class="txt34" style="height: 24px; width: 159px;">
                    <asp:TextBox ID="txtLXDH" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="m_title" id="sqInfo">
           <tr>
                <td class="t_r t_bg">申请信息：
                </td>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 10%; text-align: center;">序号</td>
                <td class="t_r t_bg" style="width: 40%; text-align: center;">产品名称</td>
                <td class="t_r t_bg" style="width: 20%; text-align: center;">标识等级</td>
                <td class="t_r t_bg" style="width: auto; text-align: center;">产品类别</td>
            </tr>
            <asp:Literal ID="ltrSQXX" runat="server"></asp:Literal>
        </table>
         <table width="98%" align="center" id="CLQQ" class="m_title">
            <tr>
                <th colspan="6">材料情况
                </th>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 30px; text-align: center;">序号</td>
                <td class="t_r t_bg" style="width: 200px; text-align: center;">材料名称</td>
                <td class="t_r t_bg" style="width: 30px;text-align: center;">份数</td>
                <td class="t_r t_bg" style="width:50px;text-align: center;"><input type="checkbox" id="allFile" />是否具备</td>
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
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="6" style="text-align: left;">审核意见
                </th>
            </tr>
            <tr>
                <td class="t_r"><asp:Literal ID="ltrPerSon" Text="接件人" runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r"><asp:Literal ID="ltrFunction" Text="接件人职务" runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtFunction" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r"><asp:Literal ID="ltrUnit" Text="接件单位" runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">是否受理
                </td>
                <td>
                    <asp:DropDownList ID="dResult" runat="server" CssClass="m_txt" Width="100">
                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r"><asp:Literal ID="ltrTime" Text="接件时间" runat="server"></asp:Literal>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r"><asp:Literal ID="ltrComment" Text="接件意见" runat="server"></asp:Literal>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="t_FAppIdea" runat="server" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                        Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin-top: 2PX">
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClientClick="return Save('btnSave')" onclick="btnSave_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnAcccpt" runat="server" Text="接件" CssClass="m_btn_w4" OnClientClick="return Save('btnAcccpt')"  onclick="btnAcccpt_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnNoAccept" runat="server" Text="不予受理" CssClass="m_btn_w4"  OnClientClick="return confirm('确认操作？')"
                onclick="btnNoAccept_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnBack" runat="server" Text="打回企业"  CssClass="m_btn_w4" OnClientClick="return confirm('确认操作？')" onclick="btnBack_Click" />&nbsp;&nbsp;
            <input type="button" value="返回" class="m_btn_w2" onclick="window.close();" />
            <input id="t_YWBM" runat="server" type="hidden" />
            <input id="t_ftype" runat="server" type="hidden" />
            <input id="t_fSubFlowId" runat="server" type="hidden" />
            <input id="t_ProcessRecordID" runat="server" type="hidden" />
            <input id="t_FProcessInstanceID" runat="server" type="hidden" />
            <asp:HiddenField ID="hfFile" runat="server" />
            <asp:HiddenField ID="hfFLinkId" runat="server" />
        </div>
    </form>
</body>
</html>
