<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batchEdit.aspx.cs" Inherits="Government_AppMain_batchEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script>
        function checkInfo() {
            if (document.getElementById("t_FNumber").value == "") {
                alert("请填写批次编号");
                document.getElementById("t_FNumber").focus();
                return false;
            }

            if (document.getElementById("t_FTtile").value == "") {
                alert("请填写批次名称");
                document.getElementById("t_FTtile").focus();
                return false;
            }

            if (document.getElementById("t_FSystemID").value == "") {
                alert("请选择所属系统");
                document.getElementById("t_FSystemID").focus();
                return false;
            }
            return true;
        }
        function checkNo() {
            if (document.getElementById("t_FNumber").value.trim().length != 7) {
                alert("请输入7位整数编号");
                document.getElementById("t_FNumber").focus();
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
                <th colspan="5">批次维护
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">批次编号：
                </td>
                <td>
                    <asp:TextBox ID="t_FNumber" Width="150px" runat="server" onblur="checkNo();" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">批次名称：
                </td>
                <td>
                    <asp:TextBox ID="t_FTtile" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">年度：
                </td>
                <td>
                    <asp:TextBox ID="t_FYear" runat="server" Width="150px" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r t_bg">所属系统：
                </td>
                <td>
                    <asp:DropDownList ID="t_FSystemID" runat="server" CssClass="cTextBox1" Width="130px">
                    </asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">状态：
                </td>
                <td>
                    <asp:DropDownList ID="t_FState" runat="server" CssClass="cTextBox1" Width="130px">
                        <asp:ListItem Text="--请选择--" Value=""> </asp:ListItem>
                        <asp:ListItem Text="办结" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未办结" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg"></td>
                <td></td>
            </tr>
        </table>
         <input id="t_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
