<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ACLogInfo.aspx.cs" Inherits="Share_Sys_ACLogInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>日志详细信息</title>
<asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css"></asp:Link>
    <script src="../../script/default.js" type="text/javascript"></script>

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
                            日志类型：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_LogType" runat="server" Width="654px" CssClass="m_txt" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            记录时间：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Title" runat="server" Width="654px" CssClass="m_txt" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            详细信息：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_errmsg" runat="server" Height="98px" CssClass="m_txt" ReadOnly="True"
                                TextMode="MultiLine" Width="721px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">
                            执行内容：</td>
                        <td>
                            <asp:TextBox ID="txt_Content" runat="server" Height="263px" CssClass="m_txt" ReadOnly="True"
                                TextMode="MultiLine" Width="721px"></asp:TextBox>
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
