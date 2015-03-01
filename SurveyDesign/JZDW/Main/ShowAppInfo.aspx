<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowAppInfo.aspx.cs" Inherits="EntApprove_gzmain_ShowAppInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/Govdept.js" language="javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>

    <title>办理意见详细</title>
</head>
<body scroll="no">
    <form id="form1" runat="server">
    <table align="center" width="98%" visible="false" class="m_table">
        <tr>
            <td align="center" class="t_c t_bg" colspan="2">
                办理意见详细
            </td>
        </tr>
        <asp:Repeater ID="rptIdea" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="t_bg" colspan="2">
                        <%#Container.ItemIndex+1 %>、
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg" width="80px">
                        办理时间：
                    </td>
                    <td>
                        <%#Eval("FTime","{0:yyyy-MM-dd}")%>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">
                        办理意见：
                    </td>
                    <td>
                        <%#Eval("FContent")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
