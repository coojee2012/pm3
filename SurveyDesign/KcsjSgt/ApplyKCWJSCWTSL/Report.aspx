<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="KcsjSgt_ApplyKCWJSCWTSL_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>勘察文件审查--合同确认 </title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();


            //选择是否同意时切换要填写的内容
            $("#t_FInt1").change(function() {
                tab();
            });
        });

        //选择是否同意时切换要填写的内容
        function tab() {
            var v = $("#t_FInt1").val();
            $("table[id^=tab_]").hide();
            $("table[id=tab_" + v + "]").show();
            if (v == "2") {
                $("#span_back").text("退回此合同，以下为退回原因：");
                $("#span_back2").text("退回");
            }
            if (v == "7") {
                $("table[id=tab_2]").show();
                $("#span_back").text("不同意，以下为原因：");
                $("#span_back2").text("不同意");
            }
        }

        function checkInfo() {
            if ($("#t_FInt1").val() == "") {
                alert("请填写是否同意接受合同");
                $("#t_FInt1").focus();
                return false;
            }
            if ($("#t_FInt1").val() == "6") {
                if ($("#t_FTxt16").val() == "") {
                    alert("请填写办理人");
                    $("#t_FTxt16").focus();
                    return false;
                }
                if ($("#t_FTxt19").val() == "") {
                    alert("请填写办理意见");
                    $("#t_FTxt19").focus();
                    return false;
                }
                if (!getLength(document.getElementById("t_FTxt19"), 50, '“办理意见”')) {
                    return false;
                }
            }
            else if ($("#t_FInt1").val() == "2") {
                if ($("#t_FTxt20").val() == "") {
                    alert("请填写退回原因");
                    $("#t_FTxt20").focus();
                    return false;
                }
                if (!getLength(document.getElementById("t_FTxt20"), 50, '“退回原因”')) {
                    return false;
                }
            }
            else if ($("#t_FInt1").val() == "7") {
                if ($("#t_FTxt20").val() == "") {
                    alert("请填写不同意原因");
                    $("#t_FTxt20").focus();
                    return false;
                }
                if (!getLength(document.getElementById("t_FTxt20"), 50, '“不同意原因”')) {
                    return false;
                }
            }
            return true;
        }
        function seePrj() {
            var fid = $('#hidd_FDataID').val();
            showAddWindow('ApplyBaseInfo.aspx?FDataID=' + fid, 900, 700);
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                勘察文件审查--合同确认
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" width="20%">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
                <input type="button" id="btnLook" value="查看合同信息" class="m_btn_w8" onclick="seePrj()" />
                <input type="hidden" id="hidd_FDataID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设单位：
            </td>
            <td>
                <asp:TextBox ID="txtJSDW" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                是否接受合同：
            </td>
            <td>
                <asp:DropDownList ID="t_FInt1" runat="server">
                    <asp:ListItem Value="6" Text="资料完整接受合同"></asp:ListItem>
                    <%--<asp:ListItem Value="2" Text="退回"></asp:ListItem>--%>
                    <asp:ListItem Value="7" Text="资料不完整不接受合同"></asp:ListItem>
                </asp:DropDownList>
                <tt>* 是否同意接受此勘察文件审查合同。</tt>
            </td>
        </tr>
    </table>
    <table id="tab_6" class="m_table" width="98%" align="center">
        <tr>
            <td colspan="4" class="t_bg" style="padding-left: 20px; color: Red;">
                您同意接受，以下是接受信息：
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                办理人：
            </td>
            <td colspan="1" class="m_txt_M" width="20%">
                <asp:TextBox ID="t_FTxt16" runat="server" CssClass="m_txt" Width="100px" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                办理时间：
            </td>
            <td colspan="1" class="m_txt_M">
                <asp:TextBox ID="t_FDate6" runat="server" CssClass="m_txt" Width="100px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                办理意见：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_FTxt19" runat="server" CssClass="m_txt" Width="320px" MaxLength="50"
                    TextMode="MultiLine" Height="60"></asp:TextBox>
                <tt>* 50字。</tt>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="t_bg" style="padding-left: 20px; color: Red;">
                附件：
                <asp:Button ID="btnQuery" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnShowFile_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="m_txt_M">
                <table class="m_dg1" width="100%" align="center">
                    <tr class="m_dg1_h">
                        <th style="width: 30px;">
                            序号
                        </th>
                        <th>
                            资料名称
                        </th>
                        <th>
                            是否必需
                        </th>
                        <th style="width: 60px;">
                            已上传<br />
                            文件个数
                        </th>
                        <th style="width: 160px;">
                            <font color="green">是</font>/<font color="red">否</font> 上传
                        </th>
                    </tr>
                    <asp:Repeater ID="rep_List" runat="server" OnItemDataBound="rep_List_ItemDataBound">
                        <ItemTemplate>
                            <tr class="m_dg1_select">
                                <td>
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td class="t_l">
                                    <%#Eval("FFileName")%>
                                </td>
                                <td>
                                    <%#Eval("FIsMust")%>
                                </td>
                                <td>
                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="lit_Has" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <asp:Repeater ID="rep_File" runat="server" OnItemCommand="rep_File_ItemCommand">
                                <ItemTemplate>
                                    <tr class="m_dg1_i">
                                        <td colspan="6" class="t_l" style="padding-left: 50px;">
                                            (<%# Container.ItemIndex+1 %>)、 <a href='<%#Eval("FFilePath") %>' target="_blank"
                                                title="点击查看该文件">
                                                <%#Eval("FFileName")%>
                                            </a>
                                            <asp:LinkButton ID="btnDel" runat="server" Text="[删除]" CommandName="cnDel" CommandArgument='<%#Eval("FID") %>'
                                                OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
    <table id="tab_2" class="m_table" width="98%" align="center">
        <tr>
            <td colspan="2" class="t_bg" style="padding-left: 20px; color: Red;">
                <span id='span_back'>退回此合同， 以下为退回原因：</span>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                <span id='span_back2'></span>原因：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FTxt20" runat="server" CssClass="m_txt" Width="320px" MaxLength="50"
                    TextMode="MultiLine" Height="60"></asp:TextBox>
                <tt>* 50字。</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="2" class="t_c">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w4"
                    OnClientClick="return checkInfo();" />
                <asp:Button ID="btnReport" runat="server" Text="提交" OnClick="btnReport_Click" CssClass="m_btn_w4"
                    Style="margin-left: 10px;" />
                <tt>*提交前请确定信息无误，提交后将不能修改。</t>
            </td>
        </tr>
    </table>
    <input id="k_FBaseInfoId" type="hidden" runat="server" />
    <input id="j_FBaseInfoId" type="hidden" runat="server" />
    </form>
</body>
</html>
