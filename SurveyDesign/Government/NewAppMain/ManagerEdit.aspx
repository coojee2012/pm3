<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerEdit.aspx.cs" Inherits="Government_NewAppMain_ManagerEdit" %>

<%@ Register Src="../../Common/govdeptid4.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>管理用户信息维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        function CheckInfo() {
            var txts = document.getElementsByTagName("input");
            var isOk = true;
            for (var i = 0; i < txts.length; i++) {
                if (txts[i].type == "text") {
                    txts[i].style.background = "white";
                    if (txts[i].value == "") {
                        txts[i].style.background = "#caffaa";
                        isOk = false;
                    }
                }
            }
            return isOk;
        }    
    </script>

    <base target="_self">
    </base>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                用户信息维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_table">
        <tr runat="server" id="tr_Name" visible="false">
            <td class="t_r">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Enabled="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr runat="server" id="tr_pwd" visible="false">
            <td class="t_r">
                密码：
            </td>
            <td>
                <asp:TextBox ID="txtFPwd" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                姓名：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                行政区划：
            </td>
            <td colspan="3">
                <uc1:Govdept ID="Govdept1" runat="server" OnSelectedIndexChanged="govdeptid1_SelectedIndexChanged" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                所属单位：
            </td>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlPartType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPartType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                部门：
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="t_FCompany" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                职务：
            </td>
            <td>
                <asp:TextBox ID="t_FFunction" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                手机：
            </td>
            <td>
                <asp:TextBox ID="t_FLicence" runat="server" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_c" colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />&nbsp;&nbsp;&nbsp;
                <input id="btnReturn" type="button" value="返回" onclick="window.close();" class="m_btn_w2" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
