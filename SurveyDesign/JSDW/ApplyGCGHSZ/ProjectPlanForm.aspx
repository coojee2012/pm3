<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectPlanForm.aspx.cs" Inherits="JSDW_ApplyGCGH_ProjectPlanForm" %>

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
        function SaveValidate() {
            var success = $("#form1").valid();
            if (success)
                return true;
            return false;
        }
        function SearchProject() {
            var result = showWinByReturn("../ApplyXZYJS/ChooseBuildUnit.aspx?", 800, 500);
            if (result != undefined) {
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
                工程规划许可证申请表(市政工程)
            </th>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                    
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" OnClientClick="return SaveValidate()" OnClick="btnSave_Click" />
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
                <td colspan="3">
                    <asp:TextBox ID="txtJSDWMC" Enabled="false" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSDWDZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td>
                    <asp:TextBox ID="txtLXR" runat="server" CssClass="m_txt required" Width="220px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    电话：
                </td>
                <td><asp:TextBox ID="txtLXDH" runat="server" CssClass="m_txt" Width="215"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtXMMC" runat="server" Enabled="false" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                    <asp:Button ID="btnChoose" runat="server" class="m_btn_w2" Text="选 择" Visible="false" OnClientClick="return SearchProject()"  OnClick="btnChoose_Click"  />
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    项目编号：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtBH" runat="server" Enabled="false" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    项目属地：
                </td>
                <td>
                    <uc1:govdeptid ID="ucProjectPlace" runat="server" />
                </td>
                <td class="t_r t_bg">
                    是否涉外：
                </td>
                <td>
                    <asp:DropDownList ID="ddlSFSW" runat="server" Width="150">
                            <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                            <asp:ListItem Value="0">--非涉外项目--</asp:ListItem>
                            <asp:ListItem Value="1">--涉外项目--</asp:ListItem>
                         </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    用地性质：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlYDXZ" CssClass="required" runat="server" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
           <tr>
                 <td class="t_r t_bg">
                    建设地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSDZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
           </tr>
            <%--<tr>
                 <td class="t_r t_bg">
                    用地面积(m2)：
                </td>
                <td>
                    面积(㎡)<asp:TextBox ID="txtYDMJ" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
                    <tt>*</tt>
                    建筑性质
                    <asp:TextBox ID="txtJZXZ" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
           </tr>--%>
            <tr>
                 <td class="t_r t_bg">
                   立项文号：
                </td>
                <td>
                    <asp:TextBox ID="txtLXWH" runat="server" CssClass="m_txt" Width="230"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                   立项日期：
                </td>
                <td><asp:TextBox ID="txtLXSJ" runat="server"  CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="195"></asp:TextBox><tt>*</tt></td>
           </tr>
           <tr>
                 <td class="t_r t_bg">
                    建设规模：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSGM" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                    
                </td>
           </tr>
           <tr>
                 <td class="t_r t_bg">
                    项目内容：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtXMNR" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
           </tr>
            <tr>
                <td class="t_r t_bg">
                    建设内容：
                </td>
                <td colspan="3">
                    <asp:CheckBox runat="server" ID="cbZHSZ" Text="综合市政" />
                    <asp:CheckBox runat="server" ID="cbDT" Text="地铁" />
                    <asp:CheckBox runat="server" ID="cbQLSD" Text="桥梁、隧道" />
                    <asp:CheckBox runat="server" ID="cbHD" Text="河堤" />
                    <asp:CheckBox runat="server" ID="cbGX" Text="管线" />
                    <asp:CheckBox runat="server" ID="cbKK" Text="开口" />
                    <asp:CheckBox runat="server" ID="cbQT" Text="其它" />
                </td>
            </tr>
             <tr>
                 <td class="t_r t_bg">
                    附图及附件名称：
                </td>
                <td colspan="3">
                     <asp:TextBox ID="txtFJMC" runat="server" CssClass="m_txt" Width="500px" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
           </tr>
             <tr>
                 <td class="t_r t_bg">
                    备注：
                </td>
                <td colspan="3">
                     <asp:TextBox ID="txtBZ" runat="server" CssClass="m_txt" Height="60" Width="500px" TextMode="MultiLine"></asp:TextBox>
                </td>
           </tr>
        </table>
        </div>
    </form>
</body>
</html>
