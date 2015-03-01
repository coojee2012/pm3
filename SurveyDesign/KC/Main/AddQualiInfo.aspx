<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQualiInfo.aspx.cs" Inherits="Government_EntQualiCerti_AddQualiInfo" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>资质信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        function checkInfo() {
            return AutoCheckInfo();
        }  
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                资质信息
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="btnReturn" class="m_btn_w2" type="button" value="返回" onclick=" window.close(); " />
            </td>
        </tr>
    </table>
    <table align="center" class="m_table" width="98%">
        <tr id="Tr1" runat="server">
            <td class="t_r t_bg">
                类型：
            </td>
            <td class="txt34">
                <asp:DropDownList ID="dbTypeId" runat="server" CssClass="m_txt" AutoPostBack="True"
                    OnSelectedIndexChanged="dbTypeId_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                序列：
            </td>
            <td class="txt34">
                <asp:DropDownList ID="t_FListId" runat="server" CssClass="m_txt" AutoPostBack="True"
                    OnSelectedIndexChanged="t_FListId_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                行业：
            </td>
            <td class="txt34 lTxt">
                <asp:DropDownList ID="t_FTypeId" runat="server" CssClass="m_txt" AutoPostBack="True"
                    OnSelectedIndexChanged="t_FTypeId_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                专业：
            </td>
            <td class="txt34 lTxt">
                <asp:CheckBoxList ID="cSpcal" runat="server" CssClass="lTxt " RepeatColumns="3" Style="border: none 0px;">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                等级：
            </td>
            <td class="txt34 lTxt">
                <asp:DropDownList ID="t_FLevelId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否有效：
            </td>
            <td class="txt34">
                <asp:CheckBox ID="t_FState" runat="server" Enabled="false" Checked="true" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                核准单位：
            </td>
            <td class="txt34">
                <asp:DropDownList ID="t_FAppDeptId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                核准时间：
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FAppTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
