<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyFor.aspx.cs" Inherits="JSDW_ApplyYDGH_ApplyFor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script language="javascript">
        function Submit(obj) {
            obj.disabled = true;
            obj.value = "请稍后...";
            __doPostBack(obj.id, '');
        }
        function SearchProject() {
            var result = showWinByReturn("../ApplyXZYJS/ChooseProject.aspx?anc=1", 800, 500);
            if (result != undefined) {
                $("#hfFid").val(result);
                return true;
            }
            return false;
        }
        function ClickOk() {
            var year = $("#t_FYear").val();
            var name = $("#t_FName").val();
            var projectName = $("#txtFPrjName").val();
            if ($.trim(year).length == 0) {
                alert("年度不能为空");
                return false;
            } else if ($.trim(name).length == 0) {
                alert("业务名称不能为空");
                return false;
            } else if ($.trim(projectName).length == 0) {
                alert("工程名称不能为空");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfFid" runat="server" />
        <asp:HiddenField ID="hfProjectType" runat="server" />
        <table height="95%" width="98%" align="center">
            <tr>
                <td colspan="3" height="10px;"></td>
            </tr>
            <tr>
                <td class="wxts_top_l"></td>
                <td class="wxts_top"></td>
                <td class="wxts_top_r"></td>
            </tr>
            <tr>
                <td class="wxts_l"></td>
                <td class="wxts_m" valign="top">
                    <div class="wxts_title">
                       工程规划许可申报
                    </div>
                    <div style="width: 98%; margin: 0 auto;">
                        <table align="center" width="600" id="applyInfo" class="m_table">
                            <tr>
                                <td class="t_r">年度：
                                </td>
                                <td>
                                    <asp:TextBox ID="t_FYear" runat="server" CssClass="m_txt" Width="100px"
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="t_r">申请业务名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="t_r">项目名称：<tt>*</tt>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
                                     <asp:Button ID="btnChoose" CssClass="m_btn_w4" runat="server" Text="选择..." OnClientClick="return SearchProject();" OnClick="btnChoose_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" height="30">
                                    <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" CssClass="m_btn_w2" Text="确认" OnClientClick="return ClickOk();" />
                                    <input id="btnCancel" class="m_btn_w2" onclick="javascript: window.location.href = 'ApplyIndex.aspx'" style="margin-left: 10px"
                                        type="button" value="取消" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td class="wxts_r"></td>
            </tr>
            <tr>
                <td class="wxts_bot_l"></td>
                <td class="wxts_bot"></td>
                <td class="wxts_bot_r"></td>
            </tr>
        </table>
    </form>
</body>
</html>

