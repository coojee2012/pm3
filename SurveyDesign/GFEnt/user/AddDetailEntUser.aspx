<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDetailEntUser.aspx.cs" Inherits="GFEnt_user_AddDetailEntUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
    <script src="../../zDialogNew/zDialog.js" type="text/javascript"></script>
    <script src="../../zDialogNew/zDrag.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }

        .m_btn_w6 {
            height: 21px;
        }
    </style>
    <script type="text/javascript">
        function addRight(FID) {
            var FUserID = document.getElementById("t_Fid").value;
            if (FUserID == null || FUserID == "" || FUserID == undefined) {
                alert("请先保存企业基本信息!");
                return;
            }
            showAddWindow("EntUserRightAdd.aspx?FUserId=" + FUserID + "&FID=" + FID, 500, 500);
        }
        function SelectEntFK(obj) {
            var rv = showWinByReturn("chooseEntFK.aspx?1=1", 600, 500);
            if (rv) {
                $("#link_FID").attr("value", rv);
                return true;
            }
            return false;
        }
        function SelectEnt(obj) {
            var rv = showWinByReturn("chooseEnt.aspx?1=1", 600, 600);
            if (rv) {
                $("#ent_FID").attr("value", rv);
                return true;
            }
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">工法企业用户子账号维护
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSelectEnt" runat="server" CssClass="m_btn_w6" Text="从未发卡库选取" OnClientClick="if(!SelectEntFK(this)){return false;}"
                        OnClick="btnSelectEnt_Click" />
                    &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    &nbsp;<input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                    &nbsp;<asp:Button ID="btnPass" Visible="false" runat="server" CssClass="m_btn_w2" Text="初始化密码" OnClick="btnPass_Click" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">工法用户名：
                </td>
                <td>
                    <asp:TextBox ID="t_FName" Width="150px" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">密码：
                </td>
                <td>
                    <asp:TextBox ID="txtFPassWord" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">用户角色：
                </td>
                <td>
                    <asp:TextBox ID="t_FEntType" Width="150px" runat="server" CssClass="m_txt" Enabled="False">省级工法</asp:TextBox>
            </tr>
            <tr>
                </td>
                <td class="t_r t_bg">菜单角色：
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="t_FMenuRoleId" Width="150px" runat="server" CssClass="m_txt" Enabled="False">省级工法</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">加密硬件锁编号：
                </td>
                <td>
                    <asp:TextBox ID="t_FLockNumber" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    &nbsp;<asp:Button Visible="false" ID="btnUnlock" runat="server" Text="解锁" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">企业名称：
                </td>
                <td>
                    <asp:TextBox ID="t_FCompany" Width="150px" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">工法名称：
                </td>
                <td>
                    <asp:TextBox ID="t_FRESON" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">营业执照号：
                </td>
                <td>
                    <asp:TextBox ID="t_FLicence" Width="150px" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="t_FJuridcialCode" Width="150px" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">法人：
                </td>
                <td>
                    <asp:TextBox ID="t_FAcceptPerson" Width="150px" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">联系人：
                </td>
                <td>
                    <asp:TextBox ID="t_FLinkMan" Width="150px" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">联系电话：
                </td>
                <td>
                    <asp:TextBox ID="t_FTel" Width="150px" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">公司地址：
                </td>
                <td>
                    <asp:TextBox ID="t_FAddress" Width="150px" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        </table>
        <input id="HSaveResult" runat="server" type="hidden" />
        <input id="t_Fid" runat="server" type="hidden" />
        <input id="t_zfid" runat="server" type="hidden" />
        <input id="t_FBaseInfoId" runat="server" type="hidden" />
        <input id="t_FManageDeptId" runat="server" type="hidden" />
        <input id="t_FCompanyId" runat="server" type="hidden" />
        <input id="link_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
