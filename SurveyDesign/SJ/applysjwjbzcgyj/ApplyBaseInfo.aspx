<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="KC_ApplyKCCGYJ_ApplyBaseInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>施工图设计文件编制成果移交</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function checkInfo() {
            if (!AutoCheckInfo()) {
                return false;
            }
            if (!getLength(document.getElementById("d_FTxt20"), 100, '“施工图设计文件编制结论”')) {
                return false;
            }
            return true;
        }
        function seePrj() {
            var fid = $('#hidd_FDataID').val();
            showAddWindow('../applysjwjbzwt/ApplyBaseInfo.aspx?FDataID=' + fid, 900, 700);
        }     
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                施工图设计文件编制成果移交
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r" style="padding-right: 10px;">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w4"
                    OnClientClick="return checkInfo();" />
                <asp:Button ID="btnReport" runat="server" Text="提交" OnClientClick="if(checkInfo())return confirm('确定要提交吗，提交后将不所修改。');else return false;"
                    OnClick="btnReport_Click" class="m_btn_w4" />
                <input type="button" class="m_btn_w2" value="返回" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" style="width: 170px">
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
                <input type="button" id="btnLook" value="查看合同备案信息" class="m_btn_w8" onclick="seePrj()" />
                <input type="hidden" id="hidd_FDataID" runat="server" />
                <input type="hidden" id="p_FId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="p_FPrjNo" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FName" runat="server" CssClass="m_txt" ReadOnly="true" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同备案受理时间：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" ReadOnly="true" Width="90"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width: 150px;">
                计划开始时间：
            </td>
            <td>
                <asp:TextBox ID="d1_FDate1" runat="server" CssClass="m_txt" ReadOnly="true" Width="90"></asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width: 150px;">
                计划结束时间：
            </td>
            <td>
                <asp:TextBox ID="d1_FDate2" runat="server" CssClass="m_txt" ReadOnly="true" Width="90"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                实际开始时间：
            </td>
            <td>
                <asp:TextBox ID="d_FDate4" runat="server" CssClass="m_txt" Width="90" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                实际结束时间：
            </td>
            <td>
                <asp:TextBox ID="d_FDate5" runat="server" CssClass="m_txt" Width="90" onfocus="WdatePicker()"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                施工图设计文件编制完成标志：
            </td>
            <td colspan="3">
                <asp:TextBox ID="d_FTxt15" runat="server" CssClass="m_txt" MaxLength="100" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                施工图设计文件编制结论：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="d_FTxt21" runat="server" CssClass="m_txt" MaxLength="100" Width="250px"
                    TextMode="MultiLine" Height="60"></asp:TextBox>
                （100字）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                成果移交办理人：
            </td>
            <td>
                <asp:TextBox ID="d_FTxt1" runat="server" CssClass="m_txt" Width="90" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                办理时间：
            </td>
            <td>
                <asp:TextBox ID="d_FDate3" runat="server" CssClass="m_txt" Width="90" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
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
    </form>
</body>
</html>
