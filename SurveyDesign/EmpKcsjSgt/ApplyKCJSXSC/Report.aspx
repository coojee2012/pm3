<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="KcsjSgt_ApplyKCCXXSC_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>技术性审查意见</title>
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



        function checkInfo(isReport) {
            if ($("#t_FTxt3").val() == "") {
                alert("请填写汇总人");
                $("#t_FTxt3").focus();
                return false;
            }
            if ($("#t_FInt3").val() == "") {
                alert("请填写审查次数");
                $("#t_FInt3").focus();
                return false;
            }
            if ($("#t_FInt4").val() == "") {
                alert("请填写审查结果");
                $("#t_FInt1").focus();
                return false;
            }
            if ($("#t_FDate6").val() == "") {
                alert("请填写审查时间");
                $("#t_FDate6").focus();
                return false;
            }
            if ($("#t_FTxt21").val() == "") {
                alert("请填写审查意见");
                $("#t_FTxt21").focus();
                return false;
            }
            if (!getLength(document.getElementById("t_FTxt21"), 50, '“审查意见”')) {
                return false;
            }
            if (isReport) {
                var v = true;
                $("span[name=span_FResult]").each(function() {
                    if ($(this).html().trim() == "") {
                        alert("审查人员结论未填写完整，无法提交。");
                        v = false;
                        return false;
                    }
                });
                if (!v) {
                    return false;
                }
            }
            if ($("#t_FInt4").val() == "7") {
                if (!confirm("确定审查结论为“补正材料”吗？这样建设单位将要重新办理审查业务。")) {
                    return false;
                }
            }
            return true;
        }
        function seePrj() {
            var fid = $('#hidd_DataFId').val();
            showAddWindow('ApplyBaseInfo.aspx?FDataID=' + fid, 900, 700);
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                技术性审查意见
            </th>
        </tr>
    </table>
    <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_bg" style="padding-left: 20px; color: Red;">
                各步骤办理情况：
            </td>
        </tr>
        <tr>
            <td class="m_txt_M">
                <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" OnRowDataBound="DG_List_RowDataBound" DataKeyNames="FId"
                    EmptyDataText="没有办理情况" Style="margin: 0px;">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_select" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="步骤名称"></asp:BoundField>
                        <asp:BoundField HeaderText="办理时间" DataField="FAppTime" DataFormatString="{0:d}">
                            <HeaderStyle Width="80" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="办理意见" DataField="FContent"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <HeaderStyle Width="80" />
                            <ItemTemplate>
                                <a href='javascript:showAddWindow("../AppMain/LookIdea.aspx?FID=<%#Eval("FID") %>",500,350);'>
                                    查看详情</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_bg" style="padding-left: 20px; color: Red; border-right: none;">
                审查人员结论
            </td>
            <td colspan="3" class="t_bg t_r" style="padding-right: 20px; border-left: none;">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w4" Text="刷新" OnClick="btnQuery_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="t_bg m_txt_M">
                <asp:GridView ID="DGList" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" OnRowDataBound="DGList_RowDataBound" DataKeyNames="FId"
                    EmptyDataText="暂时没有" Style="margin: 0px;">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_select" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="从事专业" DataField="FMajor" Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="审查结论">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="违返强条数量" DataField="FOrder"></asp:BoundField>
                        <asp:BoundField HeaderText="审查意见" DataField="FContent"></asp:BoundField>
                        <asp:BoundField HeaderText="审查人" DataField="FName"></asp:BoundField>
                        <asp:BoundField HeaderText="办理时间" DataField="FAppTime" DataFormatString="{0:d}">
                            <HeaderStyle Width="80" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <HeaderStyle Width="80" />
                            <ItemTemplate>
                                <a href='javascript:showAddWindow("EmpReport.aspx?FID=<%#Eval("FId") %>&FAppId=<%#Request.QueryString["FAppId"] %>&Self=<%#Eval("self") %>",700,600);'>
                                    <%#Eval("FYJ") %>意见</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="t_bg" style="padding-left: 20px; color: Red;">
                综合结论
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
                <input type="button" id="btnLook" value="查看合同备案信息" class="m_btn_w8" onclick="seePrj()" />
                <input id="hidd_FPrjId" runat="server" type="hidden" />
                <input id="hidd_DataFId" runat="server" type="hidden" />
                <input id="hidd_JSDWFBaseinfoId" runat="server" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                汇总人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt3" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                审查次数：
            </td>
            <td>
                <asp:TextBox ID="t_FInt3" runat="server" CssClass="m_txt" MaxLength="15" onblur="isInt(this);"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查结果：
            </td>
            <td>
                <asp:DropDownList ID="t_FInt4" runat="server">
                    <asp:ListItem Value="6" Text="合格"></asp:ListItem>
                    <asp:ListItem Value="3" Text="不合格"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                审查时间：
            </td>
            <td>
                <asp:TextBox ID="t_FDate6" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查意见：
            </td>
            <td class="m_txt_M" colspan="3">
                <asp:TextBox ID="t_FTxt21" runat="server" CssClass="m_txt" Width="320px" MaxLength="50"
                    TextMode="MultiLine" Height="60"></asp:TextBox>
                <tt>* 50字。</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="2" class="t_c">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w4" />
                <asp:Button ID="btnReport" runat="server" Text="提交" OnClick="btnReport_Click" CssClass="m_btn_w4"
                    Style="margin-left: 10px;" />
                <tt>*提交前请确定信息无误，提交后将不能修改。</tt>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
