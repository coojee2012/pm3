<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckPutForm.aspx.cs" Inherits="JSDW_ApplyJGYS_CheckPutForm" %>

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
        function SearchProject() {
            var result = showWinByReturn("../ApplyXZYJS/ChooseBuildUnit.aspx?", 800, 500);
            if (result != undefined) {
                $("#hfXMBM").val(result);
                return true;
            }
            return false;
        }
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
                竣工验收备案表(建筑工程)
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
                    工程名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtGCMC" runat="server" Enabled="false" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程所属地：
                </td>
                <td>
                     <uc1:govdeptid ID="ucProjectPlace" runat="server" />
                </td>
                <td class="t_r t_bg">
                    用地性质：
                </td>
                <td>
                    <asp:DropDownList ID="ddlYDXZ" CssClass="required" runat="server" Width="200"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程地点：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtGCDD" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSDW" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    项目负责人：
                </td>
                <td>
                    <asp:TextBox ID="txtXMFZR" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    联系电话：
                </td>
                <td>
                     <asp:TextBox ID="txtLXDH" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    建筑面积(㎡)：
                </td>
                <td>
                    <asp:TextBox ID="txtJZMJ" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    结构类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlJGLX" runat="server" Width="200" CssClass="required"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>

             <tr>
                <td class="t_r t_bg">
                    层数(地上/地下)：
                </td>
                <td>
                    <asp:TextBox ID="txtCSDS" runat="server" CssClass="m_txt required" Width="100"></asp:TextBox><tt>*</tt> / 
                    <asp:TextBox ID="txtCSDX" runat="server" CssClass="m_txt required" Width="100"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    高度(m)：
                </td>
                <td>
                    <asp:TextBox ID="txtGD" runat="server" Width="200" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    电梯：
                </td>
                <td>
                    <asp:TextBox ID="txtDT" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    自动扶梯：
                </td>
                <td>
                    <asp:TextBox ID="txtZDFT" runat="server" Width="200"  CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程用途：
                </td>
                <td>
                    <asp:DropDownList ID="ddlGCYT" runat="server" CssClass="required" Width="200px"></asp:DropDownList><tt>*</tt>
                    <%--<asp:TextBox ID="txtGCYT" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox>--%>
                </td>
                 <td class="t_r t_bg">
                    工程造价(万元)：
                </td>
                <td>
                    <asp:TextBox ID="txtGCZJ" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    开工日期：
                </td>
                <td>
                    <asp:TextBox ID="txtKGRQ" runat="server"  CssClass="m_txt Wdate required" onfocus="WdatePicker({skin:'whyGreen' })" Width="200"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    竣工验收日期：
                </td>
                <td>
                    <asp:TextBox ID="txtJGYSRQ" runat="server"  CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="200"></asp:TextBox>
                </td>
            </tr>
            <!--弃用开始-->
             <tr style="display:none;">
                <td class="t_r t_bg">
                    规划许可证号：
                </td>
                <td>
                    <asp:TextBox ID="txtGHXKZH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    施工许可证号：
                </td>
                <td>
                    <asp:TextBox ID="txtSGXKZH" runat="server" CssClass="m_txt" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="t_r t_bg">
                    施工图设计文件审查机构：
                </td>
                <td>
                    <asp:TextBox ID="txtSGTSJWJSCJG" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    施工图设计文件审查批准书号：
                </td>
                <td>
                    <asp:TextBox ID="txtSGTSJWJSCPZWH" runat="server" CssClass="m_txt" Width="200"></asp:TextBox>
                </td>
            </tr>
            <!--弃用结束-->
            <tr>
                <td class="t_r t_bg">
                    工程等级：
                </td>
                <td>
                    <asp:TextBox ID="txtGCDJ" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    消防验收合格文件编号：
                </td>
                <td>
                    <asp:TextBox ID="txtXFHGYSWJBH" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位等级：
                </td>
                <td>
                    <asp:TextBox ID="txtZZDJ" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    建设单位负责人：
                </td>
                <td>
                    <asp:TextBox ID="txtJSDWFR" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    质量监督机构名称：
                </td>
                <td colspan="3">
                     <asp:TextBox ID="txtZLJDJGMC" runat="server" CssClass="m_txt" Width="500"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="t_r t_bg">
                    质量监督机构组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtZLJDJGZZJGDM" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    监督登记号：
                </td>
                <td>
                    <asp:TextBox ID="txtJDDJH" runat="server" CssClass="m_txt" Width="200"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td colspan="3">
                     <asp:TextBox ID="txtJSGM" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>

