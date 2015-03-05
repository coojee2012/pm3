<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BLXWForm.aspx.cs" Inherits="Share_Main_Default2" %>
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
                不良行为信息
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
                <td class="t_r t_bg" colspan="2">
                    处罚对象
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtGCMC" runat="server" CssClass="m_txt" Enabled=false Width="500px"></asp:TextBox><input type="button" value="选 择" class="m_btn_w2" /><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" rowspan="8">
                    被记录相关信息：
                </td>
                <td class="t_r t_bg">
                    项目名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="m_txt" Enabled=false Width="500px"></asp:TextBox><input type="button" value="选 择" class="m_btn_w2" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程所在地：
                </td>
                <td colspan="3">
                    <uc1:govdeptid ID="ucProjectPlace" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    实施阶段：
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList4" CssClass="required" runat="server" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    责任主体类别：
                </td>
                <td>
                    <asp:TextBox ID="txtZRZTLB" runat="server" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    扣分分值：
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="200"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    发生时间：
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="200"  CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    不良行为事实：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSGM" runat="server" CssClass="m_txt required" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    处罚决定：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox6" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td width="20%" align="left" style="padding-left:10px;">
                    违反规定
                </td>
                <td class="t_r" style="" width="auto">
                    <asp:Button ID="Button1" runat="server" CssClass="m_btn_w2" Text="新 增" />
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_title">
           <tr id="firstFile">
              <td class="t_r" style="text-align:center;">行为代码</td><td class="t_r" style="text-align:center;">行为类别</td><td class="t_r" style="text-align:center;">行为描述</td><td class="t_r" style="text-align:center;">扣分值</td><td class="t_r" style="text-align:center;">删除</td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center" style="margin-top:10px;">
            <tr>
                <td class="t_r t_bg">
                    处罚单位
                </td>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    处罚时间
                </td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server"   CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    发布单位
                </td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    本年度累计记录次数
                </td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server"   CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    发布日期
                </td>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server" CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    发布截止日期
                </td>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server" CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    其他
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox13" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    处罚文件
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox14" runat="server" CssClass="m_txt" Width="500"></asp:TextBox>
                </td>
            </tr>
        </table>
         <table width="98%" align="center" class="m_bar">
            <tr>
                <td width="20%" align="left" style="padding-left:10px;">
                    附件
                </td>
                <td class="t_r" style="" width="auto">
                    <asp:Button ID="Button2" runat="server" CssClass="m_btn_w2" Text="新 增" />
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_title">
           <tr id="Tr1">
              <td class="t_r" style="text-align:center;">附件名称</td><td class="t_r" style="text-align:center;">上传日期</td><td class="t_r" style="text-align:center;">附件类型</td><td class="t_r" style="text-align:center;">删除</td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>

