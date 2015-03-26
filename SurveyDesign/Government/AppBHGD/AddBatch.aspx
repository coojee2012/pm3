<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBatch.aspx.cs" Inherits="Government_AppBHGD_AddBatch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加批次</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <base target="_self" />
    <script language="javascript" type="text/javascript">

        function checkInfo() {
            return true;
        }

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv">
                    <div style="position: absolute; left: 40%; top: 50%; background-color: peru; border: solid 3px red;">
                        <table align="center">
                            <tr>
                                <td>
                                    <h1>正在保存数据</h1>
                                </td>
                                <td>
                                    <img src="../../image/load2.gif" alt="请稍候" /></td>
                            </tr>

                        </table>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="height:30%; width: 90%;">

            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">
                        <label runat="server" id="lblTitle">添加批次</label>
                    </th>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
                <table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td></td>
                        <td class="t_r">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="BtnSave" CssClass="m_btn_w2"
                                        OnClientClick="return checkInfo();" />
                                    <input id="txtFId" type="hidden" runat="server" />
                                    <input id="t_QYID" type="hidden" runat="server" />
                                    <input type="hidden" runat="server" id="h_selEntId" value="" />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>
                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td class="t_r t_bg">年份：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_year" runat="server">
                                <asp:ListItem runat="server" Value="2015">2015</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="t_r t_bg" width="12%">批次号：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txt_batchNumber"></asp:TextBox>
                        </td>
                        
                        <td class="t_r t_bg" width="12%">状态：</td>
                        <td>
                            <asp:DropDownList ID="ddl_state" runat="server">
                                <asp:ListItem runat="server" Value="0" Text="未办结"></asp:ListItem>
                                <asp:ListItem runat="server" Value="1" Text="已办结"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </form>
</body>
</html>
