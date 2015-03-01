<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Video.aspx.cs" Inherits="JSDW_ApplyAQJDBA_Video" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>视频信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
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
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click"
                        OnClientClick="return checkInfo();" />
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    安装位置：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="195px" MaxLength="40"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    摄像头ID：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_VideoCode" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    设备类型：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_EquipTyp" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>   
            <tr>
                <td class="t_r t_bg">
                    是否点名处：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_IsCallPlace" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:DropDownList >
                </td>
            </tr>     
        </table>
    </div>
    </form>
    <br />
    <br />
    <tt style="margin-left:20px;">注：摄像头须安装于工地大门口、人员进出场门口、工地最高点（塔吊、物料提升机顶端）、材料储备（加工）区、存在重大危险源的分部、分项工程作业面等关键位置。</tt>
</body>
</html>
