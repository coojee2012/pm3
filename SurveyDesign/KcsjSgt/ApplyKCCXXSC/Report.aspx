<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="KcsjSgt_ApplyKCCXXSC_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>程序性审查意见</title>
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
        }

        function checkInfo() {
            if ($("#t_FInt1").val() == "") {
                alert("请填写审查结果");
                $("#t_FInt1").focus();
                return false;
            }
            if ($("#t_FTxt20").val() == "") {
                alert("请填写审查意见");
                $("#t_FTxt20").focus();
                return false;
            }

            if (!getLength(document.getElementById("t_FTxt20"), 50, '“审查意见”')) {
                return false;
            }
            return true;
        }
        function seePrj() {
            var fid = $('#hidd_DataFId').val();
            showAddWindow('../ApplyKCWJSCWTSL/ApplyBaseInfo.aspx?FDataID=' + fid, 900, 700);
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
                审图机构意见
            </th>
        </tr>
    </table>
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
            <td colspan="2" class="t_bg" style="padding-left: 20px; color: Red;">
                本步骤办理情况
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
                <input type="button" id="btnLook" value="查看合同备案信息" class="m_btn_w8" onclick="seePrj()" />
                <input type="hidden" id="hidd_DataFId" runat="server" />
                <input id="hidd_FPrjId" runat="server" type="hidden" />
                <input id="hidd_JSDWFBaseinfoId" runat="server" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查时间：
            </td>
            <td>
                <asp:TextBox ID="t_FDate2" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="250">
                审查结果：
            </td>
            <td>
                <asp:DropDownList ID="t_FInt2" runat="server">
                    <asp:ListItem Value="1" Text="合格"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不合格"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查意见：
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
                <tt>*提交前请确定信息无误，提交后将不能修改。</tt>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
