<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relationEmp.aspx.cs" Inherits="OA_Talk_relationEmp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>参与人</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"></script>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" style="width: 97%">
            <tr>
                <td align="center" style="height: 1px">
                </td>
            </tr>
        </table>
        <table align="center" class="m_title" width="98%">
            <tr>
                <th align="left">
                    <asp:Label ID="t_BB" runat="server" ForeColor="#2A586F" Text="参与人列表"></asp:Label></th>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 27px">
                    &nbsp;<img src="../../OA/images/question1.gif" />&nbsp; 话题名称：<asp:Label ID="Label_Name"
                        runat="server" Font-Bold="False"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 27px">
                    &nbsp; &nbsp;&nbsp; 状 态：<asp:Label ID="Label_FState" runat="server" Font-Bold="False"></asp:Label>&nbsp;
                </td>
            </tr>
        </table>
        
        <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <input id="Button1" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />&nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
        
        
        
        <table align="center" class="TableContent" style="width: 98%">
            
            <tr>
                <td align="right">
                    参与讨论人：</td>
                <td style="height: 56px" valign="middle">
                    <asp:TextBox ID="t_toEmp" runat="server" CssClass="m_txt" Height="65px" ReadOnly="true"
                        TabIndex="1" TextMode="MultiLine" Width="330px"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="该话题尚未设置参与讨论人。" Visible="False"></asp:Label></td>
            </tr>
        </table>
        <input id="CheckEmp" runat="server" type="hidden" />
    </form>
</body>
</html>
