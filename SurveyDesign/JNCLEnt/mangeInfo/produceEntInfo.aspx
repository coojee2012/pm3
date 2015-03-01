<%@ Page Language="C#" AutoEventWireup="true" CodeFile="produceEntInfo.aspx.cs" Inherits="JNCLEnt_ApplyInfo_produceEntInfo" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
    <base target="_self"></base>
    <script type="text/javascript">
        function checkInfo() {
            if ($("#t_FUpDeptName").val() == "") {
                alert("请选择建设行业营业部门");
                return false;
            }
            if ($("#t_QYMC").val() == "") {
                alert("请填写企业名称");
                return false;
            }
            if ($("#t_address").val() == "") {
                alert("请填写注册地址");
                return false;
            }
            if ($("#t_JYDZ").val() == "") {
                alert("请填写经营地址");
                return false;
            }
            if ($("#t_YYZZ").val() == "") {
                alert("请填写营业执照注册号");
                return false;
            }
            if ($("#t_ZZJG").val() == "") {
                alert("请填写组织机构代码");
                return false;
            }
            if ($("#t_postCode").val() == "") {
                alert("请填写邮政编码");
                return false;
            }
            if ($("#t_QYWZ").val() == "") {
                alert("请填写企业网站");
                return false;
            }
            if ($("#t_JJTypeID").val() == "") {
                alert("请选择经济类型");
                return false;
            }
            if ($("#t_CLTime").val() == "") {
                alert("请选择成立时间");
                return false;
            }
            if ($("#t_ZCZJ").val() == "") {
                alert("请填写注册资金");
                return false;
            }
            if ($("#t_QYRS").val() == "") {
                alert("请填写企业人数");
                return false;
            }
            if ($("#t_SCNL").val() == "") {
                alert("请填写年生产能力");
                return false;
            }
            if ($("#t_LJSCL").val() == "") {
                alert("请填写累计生产力");
                return false;
            }
            if ($("#t_CFDZ").val() == "") {
                alert("请填写厂房地址");
                return false;
            }
            if ($("#t_CFMJ").val() == "") {
                alert("请填写厂房面积");
                return false;
            }
            if ($("#t_FR").val() == "") {
                alert("请填写企业法人");
                return false;
            }
            if ($("#t_SJ").val() == "") {
                alert("请填写手机");
                return false;
            }
            if ($("#t_ZJType").val() == "") {
                alert("请选择证件类型");
                return false;
            }
            if ($("#t_ZJCode").val() == "") {
                alert("请填写证件号码");
                return false;
            }
            if ($("#t_linkMan").val() == "") {
                alert("请填写联系人姓名");
                return false;
            }
            if ($("#t_linkZW").val() == "") {
                alert("请填写联系人职务");
                return false;
            }
            if ($("#t_linkPho").val() == "") {
                alert("请填写联系人手机");
                return false;
            }
            if ($("#t_linkFax").val() == "") {
                alert("请填写联系人传真");
                return false;
            }
            if ($("#t_JYFW").val() == "") {
                alert("请填写营业范围（主营）");
                return false;
            }
            if ($("#t_JYFW2").val() == "") {
                alert("请填写营业范围（兼营）");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">生产企业基本信息
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r" style="padding-right: 10px;">
                    <input type="hidden" id="hidd_FLinkId" runat="server" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
                    <asp:Button ID="btnQuery" Style="display: none;" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">建设行业营业部门：</td>
                <td colspan="3">
                    <uc1:govdeptid ID="t_FMangeDeptId" runat="server" />
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">企业名称：</td>
                <td colspan="3">
                    <asp:TextBox ID="t_QYMC" runat="server" CssClass="m_txt" Width="350px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">注册地址：</td>
                <td colspan="3">
                    <asp:TextBox ID="t_address" runat="server" CssClass="m_txt" Width="350px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">经营地址：</td>
                <td colspan="3">
                    <asp:TextBox ID="t_JYDZ" runat="server" CssClass="m_txt" Width="350px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">营业执照注册号：</td>
                <td>
                    <asp:TextBox ID="t_YYZZ" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">组织机构代码：</td>
                <td>
                    <asp:TextBox ID="t_ZZJG" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">邮政编码：</td>
                <td>
                    <asp:TextBox ID="t_postCode" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">企业网站：</td>
                <td>
                    <asp:TextBox ID="t_QYWZ" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">经济类型：</td>
                <td>
                    <asp:DropDownList ID="t_JJTypeID" Width="120px" CssClass="m_txt" runat="server" AutoPostBack="True">
                    </asp:DropDownList><tt>*</tt>
                </td>
                <td class="t_r t_bg">成立时间：</td>
                <td>
                    <asp:TextBox ID="t_CLTime" runat="server" onfocus="WdatePicker()" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">注册资金：</td>
                <td>
                    <asp:TextBox ID="t_ZCZJ" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>万元
                    <asp:DropDownList ID="t_BZ" CssClass="m_txt" Width="60px" runat="server">
                        <asp:ListItem>人民币</asp:ListItem>
                        <asp:ListItem>美元</asp:ListItem>
                        <asp:ListItem>其他</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">企业人数：</td>
                <td>
                    <asp:TextBox ID="t_QYRS" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">年生产能力：</td>
                <td>
                    <asp:TextBox ID="t_SCNL" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">累计生产力：</td>
                <td>
                    <asp:TextBox ID="t_LJSCL" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>万吨/年
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">厂房地址：</td>
                <td>
                    <asp:TextBox ID="t_CFDZ" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">厂房面积：</td>
                <td>
                    <asp:TextBox ID="t_CFMJ" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>平方米
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">企业法人姓名：</td>
                <td>
                    <asp:TextBox ID="t_FR" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">手机：</td>
                <td>
                    <asp:TextBox ID="t_SJ" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">证件类型：</td>
                <td>
                    <asp:DropDownList ID="t_ZJType" CssClass="m_txt" Width="120px" runat="server">
                        <asp:ListItem>身份证</asp:ListItem>
                        <asp:ListItem>军官证</asp:ListItem>
                        <asp:ListItem>其他</asp:ListItem>
                    </asp:DropDownList><tt>*</tt>
                </td>
                <td class="t_r t_bg">证件号码：</td>
                <td>
                    <asp:TextBox ID="t_ZJCode" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">联系人姓名：</td>
                <td>
                    <asp:TextBox ID="t_linkMan" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">联系人职务：</td>
                <td>
                    <asp:TextBox ID="t_linkZW" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">联系人手机：</td>
                <td>
                    <asp:TextBox ID="t_linkPho" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">联系人传真：</td>
                <td>
                    <asp:TextBox ID="t_linkFax" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">营业范围（主营）：</td>
                <td>
                    <asp:TextBox ID="t_JYFW" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">营业范围（兼营）：</td>
                <td>
                    <asp:TextBox ID="t_JYFW2" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
        </table>
        <input id="t_fid" runat="server" type="hidden" />
    </form>
</body>
</html>
