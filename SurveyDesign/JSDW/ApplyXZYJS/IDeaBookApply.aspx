<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDeaBookApply.aspx.cs" Inherits="JSDW_ApplyXZYJS_IDeaBookApply" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
     <script type="text/javascript" src="../../script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../script/messages_zh.js"></script>

    <script type="text/javascript">
        function SaveValidate()
        {
            var success = $("#form1").valid();
            if(success)
                return true;
            return false;
        }
        function SearchProject()
        {
            var result = showWinByReturn("ChooseBuildUnit.aspx?", 800, 500);
            if (result != undefined)
            {
                $("#hfXMBM").val(result);
                return true;
            }
            return false;
        }
        $(function () {
            $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
        });
    </script>

    <%--<script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>--%>

    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfXMBM" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                选址意见书申请表（建筑工程）
            </th>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                    
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" OnClick="btnSave_Click" OnClientClick="return SaveValidate()" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    建设单位名称：
                </td>
                <td>
                    <asp:TextBox ID="txtJSDWMC" runat="server" Enabled="false" CssClass="m_txt" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位地址：
                </td>
                <td>
                    <asp:TextBox ID="txtJSDWDZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td>
                    <asp:TextBox ID="txtLXR" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox>
                    <tt>*</tt>
                    电话：
                    <asp:TextBox ID="txtLXDH" runat="server" CssClass="m_txt" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目名称：
                </td>
                <td>
                    <asp:TextBox ID="txtXMMC" runat="server" CssClass="m_txt" Enabled="false" Width="460px"></asp:TextBox>
                    <tt>*</tt>
                    <asp:Button ID="btnChoose" runat="server" class="m_btn_w2" Visible="false" Text="选 择" OnClientClick="return SearchProject()" OnClick="btnChoose_Click" />
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    项目属地：
                </td>
                <td>
                    <uc1:govdeptid ID="ucProjectPlace" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    用地性质：
                </td>
                <td>
                    <asp:DropDownList ID="ddlYDXZ" CssClass="required" runat="server" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
           <tr>
                 <td class="t_r t_bg">
                    建设地址：
                </td>
                <td>
                    <asp:TextBox ID="txtJSDZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
           </tr>
            <tr>
                 <td class="t_r t_bg">
                    拟建设规模：
                </td>
                <td>
                    面积(㎡)<asp:TextBox ID="txtJSGMMJ" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                    跨度(高度)(m)
                    <asp:TextBox ID="txtJSGMGD" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
                    其它
                    <asp:TextBox ID="txtOther" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="ddlGG">
                        <asp:ListItem Selected="True" Value="0">--请选择--</asp:ListItem>
                        <asp:ListItem Value="1">平方米</asp:ListItem>
                        <asp:ListItem Value="2">千米</asp:ListItem>
                        <asp:ListItem Value="3">米</asp:ListItem>
                        <asp:ListItem Value="4">立方米</asp:ListItem>
                        <asp:ListItem Value="5">座</asp:ListItem>
                        <asp:ListItem Value="6">其它</asp:ListItem>
                    </asp:DropDownList>
                </td>
           </tr>
            <tr>
                 <td class="t_r t_bg">
                    拟建设面积(㎡)：
                </td>
                <td>
                    <asp:TextBox ID="txtJSMJ" runat="server" CssClass="m_txt required" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                    立项文号： <asp:TextBox ID="txtLXWH" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                    立项时间： <asp:TextBox ID="txtLXSJ" runat="server"  CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="150px"></asp:TextBox>

                </td>
           </tr>
            <tr>
                 <td class="t_r t_bg">
                    是否涉外：
                </td>
                <td>
                    <asp:DropDownList ID="ddlXMSFSW" runat="server">
                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                        <asp:ListItem Value="0">非涉外项目</asp:ListItem>
                        <asp:ListItem Value="1">涉外项目</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    项目编号：
                    <asp:TextBox ID="txtBH" runat="server" CssClass="m_txt" Width="209px"></asp:TextBox>
                </td>
           </tr>
            <tr>
                 <td class="t_r t_bg">
                    项目建设依据：
                </td>
                <td>
                    <asp:TextBox ID="txtJSYJ" runat="server" Width="500" CssClass="m_txt required" Height="60" TextMode="MultiLine"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
           <tr>
                 <td class="t_r t_bg">
                    项目内容：
                </td>
                <td>
                    <asp:TextBox ID="txtXMNR" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                    
                </td>
           </tr>
           <tr>
                 <td class="t_r t_bg">
                    建设用地情况：
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbYMGMS" Text="有名古名树" />
                    <asp:CheckBox runat="server" ID="cbYKZHDSGX" Text="有空中或地上管线" />
                    <asp:CheckBox runat="server" ID="cbSZSSHGQ" Text="市政设施或沟渠" />
                    <asp:CheckBox runat="server" ID="cbDMYWWGJ" Text="地面有文物古迹" />
                </td>
           </tr>
             <tr>
                 <td class="t_r t_bg">
                    附图及附件名称：
                </td>
                <td>
                     <asp:TextBox ID="txtFJMC" runat="server" CssClass="m_txt" Width="500px" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
           </tr>
             <tr>
                 <td class="t_r t_bg">
                    备注：
                </td>
                <td>
                     <asp:TextBox ID="txtBZ" runat="server" CssClass="m_txt" Height="60" Width="500px" TextMode="MultiLine"></asp:TextBox>
                </td>
           </tr>
        </table>
        </div>
    </form>
</body>
</html>
