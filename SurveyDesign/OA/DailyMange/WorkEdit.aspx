<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkEdit.aspx.cs" Inherits="OA_DailyMange_WorkEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑事务</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" class="m_title" width="100%">
        <tr>
            <th colspan="3">
                <asp:Label ID="l_BB" runat="server" ForeColor="#2A586F">编辑事务</asp:Label>&nbsp;
                <asp:Label ID="lblDate" runat="server" ForeColor="Red"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right">
                <input id="Button1" runat="server" class="m_btn_w2"  onclick="" value="保存"
                    type="button"  onserverclick="btnSave_ServerClick" />
                <input id="Button2" class="m_btn_w2" onclick="exitt();" type="button" value="返回" onserverclick="exit_ServerClick1" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="m_table" style="width: 98%;
        height: 80px">
        
        <tr>
            <td class="t_r t_bg" style="width:150px">
                开始时间：
            </td>
            <td>
                <asp:DropDownList ID="ddlBeginH" runat="server" TabIndex="1">
                </asp:DropDownList>
                时<asp:DropDownList ID="ddlBeginM" runat="server">
                </asp:DropDownList>
                分
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                结束时间：
            </td>
            <td>
                <asp:DropDownList ID="ddlEndH" runat="server">
                </asp:DropDownList>
                时<asp:DropDownList ID="ddlEndM" runat="server">
                </asp:DropDownList>
                分
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                事务类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Text="工作事务" Value="0"></asp:ListItem>
                    <asp:ListItem Text="个人事务" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                定期模式：
            </td>
            <td>
                <asp:DropDownList ID="ddlTermly" runat="server">
                    <asp:ListItem Text="每日" Value="0"></asp:ListItem>
                    <asp:ListItem Text="每周" Value="1"></asp:ListItem>
                    <asp:ListItem Text="每月" Value="2"></asp:ListItem>
                    <asp:ListItem Text="只有一次" Value="3"></asp:ListItem>
                </asp:DropDownList>
                <asp:CheckBox ID="cheDone" runat="server" AutoPostBack="True" Text="已经完成" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                事务标题：
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server"  MaxLength="50" CssClass="m_txt"
                    Width="90%" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                事务内容：
            </td>
            <td style="height: 220px">
                <asp:TextBox ID="txtContent" runat="server" MaxLength="100" TextMode="MultiLine"
                    CssClass="m_txt" Width="90%" Height="210px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    function exitt() {
        window.returnValue = 1;
        window.close();
    }
    function returnback() {
        history.back();
    }
</script>

