<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectEdit.aspx.cs" Inherits="JSDW_ApplyXZYJS_ProjectEdit" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
     <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/lock.js"></script>
    <base target="_self"></base>
    <script type="text/javascript">

        function SaveValidate() {
            var buildUntiName = $("#txtJSDW").val();
            var buildUnitAddress = $("#txtJSDWDZ").val();
            var projectName = $("#txtXMMC").val();
            var address = $("#txtXMDZ").val();
            if ($.trim(buildUntiName).length == 0) {
                alert("建设单位名称不能为空");
                return false;
            } else if ($.trim(buildUnitAddress).length == 0) {
                alert("建设单位地址不能为空");
                return false;
            } else if ($.trim(projectName).length == 0) {
                alert("项目名称不能为空");
                return false;
            } else if ($.trim(address).length == 0) {
                alert("项目地址不能为空");
                return false;
            }
            return true;
        }
        function Exit() {
            window.close();
            window.returnValue = '1';
        }
    </script>

    <%--<script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>--%>

</head>
<body id="body1">
    <form id="form1" runat="server">
    <input id="hidd_LockID" type="hidden" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                项目基本信息
            </th>
        </tr>
    </table>
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" OnClientClick="return SaveValidate()" OnClick="btnSave_Click" />
                    <input id="Button1" class="m_btn_w2" type="button" value="返 回" onclick="Exit()" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">建设单位名称：</td>
                <td colspan="3"><asp:TextBox ID="txtJSDW" runat="server" CssClass="m_txt" Width="500px"></asp:TextBox><tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">建设单位地址：</td>
                <td colspan="3"><asp:TextBox ID="txtJSDWDZ" runat="server" CssClass="m_txt" Width="500px"></asp:TextBox><tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">联系人： </td>
                <td><asp:TextBox ID="txtLXR" runat="server" CssClass="m_txt" Width="150"></asp:TextBox></td>
                <td class="t_r t_bg">联系电话</td>
                <td><asp:TextBox ID="txtLXDH" runat="server" CssClass="m_txt" Width="150"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t_r t_bg">项目名称：</td>
                <td colspan="3"><asp:TextBox ID="txtXMMC" runat="server" CssClass="m_txt" Width="500px"></asp:TextBox><tt>*</tt></td>
            </tr>
            <tr>
                 <td class="t_r t_bg">项目属地：</td>
                <td><uc1:govdeptid ID="ucProjectPlace" runat="server" /></td>
                <td class="t_r t_bg">项目类别</td>
                <td><asp:TextBox ID="txtJGLX" runat="server" Width="150"></asp:TextBox></td>
            </tr>
            <tr>
                 <td class="t_r t_bg">项目地址：</td>
                <td colspan="3"><asp:TextBox ID="txtXMDZ" runat="server" CssClass="m_txt" Width="500px"></asp:TextBox><tt>*</tt></td>
           </tr>
            <tr>
                 <td class="t_r t_bg">立项文号：</td>
                <td><asp:TextBox ID="txtLXWH" runat="server" CssClass="m_txt" Width="150"></asp:TextBox></td>
                <td class="t_r t_bg">立项时间：</td>
                <td><asp:TextBox ID="txtLXSJ" runat="server" CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="150"></asp:TextBox><tt>*</tt></td>
           </tr>
            <tr>
                 <td class="t_r t_bg">项目编号：</td>
                <td><asp:TextBox ID="txtXMBH" runat="server" CssClass="m_txt" Width="150"></asp:TextBox></td>
                <td class="t_r t_bg">是否涉外：</td>
                <td>
                    <asp:DropDownList ID="ddlXMSFSW" runat="server" Width="150">
                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                        <asp:ListItem Value="1">涉外</asp:ListItem>
                        <asp:ListItem Value="0">非涉外</asp:ListItem>
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
           </tr>
            <tr>
                 <td class="t_r t_bg"> 建设规模或建筑总面积(㎡)：</td>
                <td><asp:TextBox ID="txtJZZMJ" runat="server" Width="150"></asp:TextBox></td>
                  <td class="t_r t_bg">总投资(万元)：</td>
                <td><asp:TextBox ID="txtZTZ" runat="server" Width="150"></asp:TextBox></td>
           </tr>
            <tr>
                 <td class="t_r t_bg">建设依据：</td>
                <td colspan="3"><asp:TextBox ID="txtJSYJ" runat="server" Width="500" CssClass="m_txt" Height="60" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
           <tr>
                 <td class="t_r t_bg">建设内容：</td>
                <td colspan="3"><asp:TextBox ID="txtXMNR" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox></td>
           </tr>
        </table>
        </div>
    </form>
</body>
</html>
