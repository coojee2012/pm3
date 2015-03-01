<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="KC_ApplyHTBA_ApplyBaseInfo" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            if (!AutoCheckInfo())
                return false;
            if ($("#t_FFloat1").val() == "" && $("#t_FTxt8").val() == "") {
                alert("“合同金额”和“收费标准”这两项中不能同时为空");
                return false;
            }
            return true;
        }
        function selEnt(tagId, fsysid, obj, hasCerti, notTag) {
            var url = "EntListSel.aspx";
            if (hasCerti == "1")
                url = "EntListSelCerti.aspx";
            url += "?fsysid=" + fsysid;
            if ($("#" + notTag).val() != undefined)
                url += "&notBid=" + $("#" + notTag).val();
            var pid = showWinByReturn('../../JSDW/appmain/' + url, 700, 500);
            if (pid != null && pid != '') {
                var ps = pid.split('|');
                if (ps.length > 0)
                    $("#" + tagId).val(ps[0]);
                if (ps.length > 1)
                    $("#" + tagId + "_c").val(ps[1]);
                __doPostBack(obj.id, '');
            }
        }
        function seePrj() {
            var fid = $('#p_FId').val();
            showAddWindow('../../JSDW/appmain/AddPrjRegist.aspx?fid=' + fid, 900, 700);
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                基本信息
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r" style="padding-right: 10px;">
                <input type="hidden" id="hidd_FLinkId" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" style="width: 200px;">
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FPrjName" runat="server" CssClass="m_txt" Width="400px" ReadOnly="true"
                    MaxLength="40"></asp:TextBox>
                <input type="hidden" id="t_FPrjId" runat="server" />
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt2" runat="server" CssClass="m_txt" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                标段号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt3" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地点：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="t_FDeptName" runat="server" CssClass="m_txt" Width="250px" MaxLength="30"
                    ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备案部门：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_FInt1" runat="server" CssClass="m_txt" Enabled="false">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建筑面积或规模：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt5" runat="server" CssClass="m_txt" ReadOnly="true" Width="200"
                    MaxLength="25"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同金：
            </td>
            <td>
                <asp:TextBox ID="t_FFloat1" runat="server" CssClass="m_txt t_r"  onblur="isFloat(this);"
                    Width="70"></asp:TextBox>
                万元
            </td>
            <td class="t_r t_bg" style="width: 100px;">
                收费标准：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt8" runat="server" CssClass="m_txt" MaxLength="20" Width="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同签订日期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FDate1" runat="server" CssClass="m_txt" ReadOnly="true" onfocus="WdatePicker();"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" style="width: 200px;">
                发包人（合同备案人）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt1" runat="server" CssClass="m_txt" ReadOnly="true" Width="300px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                经办人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt4" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg" style="width: 100px;">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt6" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                日期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FDate2" runat="server" CssClass="m_txt" onfocus="WdatePicker();"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" style="width: 200px;">
                承包人（管理人）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt7" runat="server" CssClass="m_txt" ReadOnly="true" Width="300px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                经办人：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt9" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg" style="width: 100px;">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt14" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                日期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FDate3" runat="server" CssClass="m_txt" onfocus="WdatePicker();"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                承包人（管理人）企业地址：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt16" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否联合体：
            </td>
            <td colspan="3">
                <asp:CheckBox ID="t_FInt2" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                其他承包人：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_FTxt10" runat="server" CssClass="m_txt" ReadOnly="true" Width="400px"
                    Visible="false"></asp:TextBox>
                <asp:Button ID="btnAdd" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt('l_FBaseInfoId',1553,this,'0');"
                    UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEnt_Click" Style="margin-bottom: 4px;" />
                <input id="l_FBaseInfoId" runat="server" type="hidden" />
                <asp:GridView ID="DG_List" runat="server" CssClass="m_dg1" AutoGenerateColumns="False"
                    DataKeyNames="FId" EmptyDataText="没有添加联合体" OnRowCommand="DG_List_RowCommand"
                    Style="margin: 0px;">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="单位名称" DataField="FName">
                            <ItemStyle CssClass="t_l" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <HeaderStyle Width="60" />
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDel" runat="server" CommandName="Del" CommandArgument='<%#Eval("FID")%>'
                                    OnClientClick="return confirm('确定删除吗？');" Text="删除">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>
