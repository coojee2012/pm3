<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditYLXWForm.aspx.cs" Inherits="Share_Main_EditYLXWForm" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        #form1 label.errorMsg 
			{ 
			color:Red; 
			font-size:13px; 
			margin-left:5px;
			}
    </style>
     <script type="text/javascript" src="../../script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../script/messages_zh.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
        });
        function SaveValidate() {
            var success = $("#form1").valid();
            if (success)
                return true;
            else
                return false;
        }
    </script>

    <%--<script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>--%>

    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                良好行为信息
            </th>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" OnClientClick="return SaveValidate()" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    企业名称
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtGCMC" runat="server" CssClass="m_txt" Text="广安市盛世园物业管理有限责任公司" Enabled=false Width="500px"></asp:TextBox>
                    <input type="button" value="选 择" class="m_btn_w2" /><input type="button" value="详 细" class="m_btn_w2" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    良好行为名称
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox7" runat="server" Text="广安市盛世园物业管理有限责任公司" CssClass="m_txt" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    奖项级别：
                </td>
                <td>
                    <asp:DropDownList ID="ddl" runat="server" Width="200">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">国家级</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">省级</asp:ListItem>
                        <asp:ListItem Value="3">市级</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    获奖类别：
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="200">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">县级科技奖</asp:ListItem>
                        <asp:ListItem Value="2">天府杯</asp:ListItem>
                        <asp:ListItem Value="3">省结构优质科技奖</asp:ListItem>
                        <asp:ListItem Value="4">省级功法</asp:ListItem>
                        <asp:ListItem Value="5">省行业先进企业奖</asp:ListItem>
                        <asp:ListItem Value="6">科技奖</asp:ListItem>
                        <asp:ListItem Value="7">其他</asp:ListItem>
                        <asp:ListItem Value="8">省级标准化工地</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    证书（文件）名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="m_txt" Text="资质证书" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    证书（文件）编号：
                </td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" Text="51168172208" CssClass="m_txt" Width="150px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    颁证（发文）机关：
                </td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server" CssClass="m_txt" Text="华蓥市住房和城乡规划建设局" Width="150px"></asp:TextBox>
                    颁证（发文）日期：
                    <asp:TextBox ID="TextBox11" runat="server" CssClass="m_txt Wdate" Text="2011-12-14" onfocus="WdatePicker({skin:'whyGreen' })" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    附件：
                </td>
                <td colspan="3">
                    <input type="button" value="上传附件" />
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_title">
           <tr id="Tr1">
              <td class="t_r" style="text-align:center;" width="6%">序号</td><td class="t_r" style="text-align:center;">附件名称</td><td class="t_r" style="text-align:center;">其它</td><td class="t_r" style="text-align:center;">上传日期</td><td class="t_r" style="text-align:center;">操作</td><td class="t_r" style="text-align:center;">查看</td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>


