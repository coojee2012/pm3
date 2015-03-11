<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XMSQInfo.aspx.cs" Inherits="WYDW_ApplyXMSQ_XMSQInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目失去申请</title>
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

    <base target="_self"></base>
    <style type="text/css">
        .modalDiv {
            position: absolute;
            top: 1px;
            left: 1px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: gray;
            opacity: .50;
            filter: alpha(opacity=50);
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv"> 
                <div style="position:absolute;left:40%;top:50%;background-color:peru;border:solid 3px red;">
                    <table  align="center">
                    <tr>
                    <td ><h1>正在保存数据</h1></td>
                    <td><img src="../../JSDW/../image/load2.gif" alt="请稍候"/></td>
                    </tr>
                                    
                    </table>
                </div>
                    </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        <div style="height: 100%; width: 100%;">

            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">项目失去申请
                    </th>
                </tr>

            </table>
            <div id="divSetup2" runat="server">
                <table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td></td>
                        <td class="t_r">
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                OnClientClick="return checkInfo();" />
                        </td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>
                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td class="t_r t_bg">项目失去原因：
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="dropLostReason">
                                <asp:ListItem Selected="True" Value="落聘">落聘</asp:ListItem>
                                <asp:ListItem Value="放弃">放弃</asp:ListItem>
                                <asp:ListItem Value="其它">其它</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="t_r t_bg">项目失去时间：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_Losttime" onfocus="WdatePicker()" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">备注：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="t_BZ" runat="server" TextMode="MultiLine" CssClass="m_txt" Width="98%" Height="100px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
