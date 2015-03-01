<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleSet.aspx.cs" Inherits="Admin_main_RoleSet" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss(); //文本框样式
             DynamicGrid(); //列表光标移动效果
        });       
    </script>

    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"> </script>

    <base target="_self"></base>
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                权限设置    
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
                <input id="btnSysRole" type="button" value="系统菜单权限" onclick="changeUrl('SysRoleSet.aspx?froleid=<%=Request["froleid"] %>')"
                    class="m_btn_w6" />
                <input id="btnNewsRole" type="button" value="新闻菜单权限" onclick="changeUrl('NewsRoleSet.aspx?froleid=<%=Request["froleid"] %>')"
                    class="m_btn_w6" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
   <table width="98%" align="center" class="sou">
        <tr>
            <td>
                 <iframe width="100%" id="main" frameborder="no" marginheight="0" marginwidth="0" src="SysRoleSet.aspx?froleid=<%=Request["froleid"] %>"
                    height="620"></iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script language="javascript">
function changeUrl(sUrl)
{
    document.getElementById("main").src=sUrl+'&r='+Math.random();
}
</script>

