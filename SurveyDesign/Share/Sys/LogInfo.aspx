<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogInfo.aspx.cs" Inherits="Admin_MainOther_LogInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>日志详细信息</title>
<asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css"></asp:Link>
    <script type="text/javascript" src="../script/default.js"> </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title" id="QueryTable1">
        <tr>
            <th>
                事件查看器
            </th>
        </tr>
    </table>    
    <table class="m_table" width="98%" align="center">

        <tr>

            <td class="td2">
                <table class="table2" width="100%">
                
                <tr>
            <td align="right" colspan="2" style="background-color: #cbdcfa;padding-right: 20px;" >
                <input id="Button1" class="m_btn_w2" onclick="javascript:window.close();" type="button"
                    value="返回" style=""  />
            </td>
                
                </tr>
                
                    <tr>
                        <td class="t_r t_bg" style="width: 170px">
                            所属系统：</td>
                        <td>
                            <asp:TextBox ID="txt_SysName" runat="server" Width="722px" CssClass="m_txt" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                
                    <tr>
                        <td class="t_r t_bg" style="width: 170px">
                            日志类型：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_LogType" runat="server" Width="722px" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            日志类别：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_LogSort" runat="server" Width="722px" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            记录时间：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_LogTime" runat="server" Width="722px" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            当前页面：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Link" runat="server" Width="722px" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            &nbsp;&nbsp;操作者：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Operator" runat="server" Width="722px" CssClass="m_txt"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            &nbsp;&nbsp;IP地址：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Ip" runat="server" Width="722px" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    
                    
                   <tr>
                        <td class="t_r t_bg">
                            &nbsp;&nbsp;操作系统：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_SystemType" runat="server" Width="722px" CssClass="m_txt"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td class="t_r t_bg">
                            &nbsp;&nbsp;浏览器：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Explorer" runat="server" Width="722px" CssClass="m_txt"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    
                    
                    
                    
                    <tr>
                        <td class="t_r t_bg">
                            服务器名：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_ServerName" runat="server" Width="722px" CssClass="m_txt"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            日志标题：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_LogTitle" runat="server" Width="722px" CssClass="m_txt"
                                ReadOnly="True" TextMode="MultiLine" Height="45px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            详细信息：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Info" runat="server" Height="332px" CssClass="m_txt" ReadOnly="True"
                                TextMode="MultiLine" Width="722px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>
