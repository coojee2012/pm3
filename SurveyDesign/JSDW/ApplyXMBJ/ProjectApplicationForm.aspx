<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectApplicationForm.aspx.cs" Inherits="JSDW_ApplyXMBJ_ProjectApplicationForm" %>

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
            $("#txtZFTZ,#txtWSTZ,#txtDKTZ,#txtQTTZ,#txtZCTZ").blur(function () {
                var ZFTZ = $("#txtZFTZ").val();
                var ZCTZ = $("#txtZCTZ").val();
                var WSZT = $("#txtWSTZ").val();
                var DKTZ = $("#txtDKTZ").val();
                var QTTZ = $("#txtQTTZ").val();
                var total = 0;
                if (checkNumber(ZFTZ))
                    total += parseFloat(ZFTZ);
                if (checkNumber(ZCTZ))
                    total += parseFloat(ZCTZ);
                if (checkNumber(WSZT))
                    total += parseFloat(WSZT);
                if (checkNumber(DKTZ))
                    total += parseFloat(DKTZ);
                if (checkNumber(QTTZ))
                    total += parseFloat(QTTZ);
                $("#txtZTZ,#hfZTZ").val(total.toFixed(2));
            });
        });
        function checkNumber(value) {
            if ($.trim(value).length == 0)
                return false;
            var re = /^[1-9]\d*\,\d*|[1-9]\d*$/;   //判断字符串是否为数字     //判断正整数 /^[1-9]+[0-9]*]*$/  
            if (!re.test(value)) {
                return false;
            }
            return true;
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
                项目报建申请表
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
                    项目名称：
                </td>
                <td>
                    <asp:TextBox ID="txtXMMC" runat="server" CssClass="m_txt" Width="400px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    项目编号：
                </td>
                <td>
                    <asp:TextBox ID="txtBH" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
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
                    填报日期：
                </td>
                <td>
                    <asp:TextBox ID="txtTBRQ" runat="server" CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设地点：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSDD" runat="server" CssClass="m_txt required" Width="660px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程类别：
                </td>
                <td>
                    <asp:DropDownList ID="ddlGCLB" runat="server"  Width="200px">
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    结构类型：
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlJGLX" Width="200">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    政府投资：
                </td>
                <td>
                    <asp:TextBox ID="txtZFTZ" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox> 万元&nbsp;
                    自筹投资： <asp:TextBox ID="txtZCTZ" CssClass="m_txt" runat="server" Width="80px"></asp:TextBox> 万元
                </td>
                 <td class="t_r t_bg">
                    外商投资：
                </td>
                <td>
                    <asp:TextBox ID="txtWSTZ" runat="server" CssClass="m_txt" ></asp:TextBox>万元
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    贷款投资：
                </td>
                <td>
                    <asp:TextBox ID="txtDKTZ" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox> 万元&nbsp;
                    其它投资： <asp:TextBox ID="txtQTTZ" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox> 万元
                </td>
                 <td class="t_r t_bg">
                    总投资：
                </td>
                <td>
                    <asp:TextBox ID="txtZTZ" runat="server" Enabled="false" CssClass="m_txt" ></asp:TextBox>万元
                    <asp:HiddenField ID="hfZTZ" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    用地面积(㎡)：
                </td>
                <td>
                    <asp:TextBox ID="txtYDMJ" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>&nbsp;&nbsp;
                    建筑面积(㎡)： <asp:TextBox ID="txtJZMJ" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    栋数(栋)：
                </td>
                <td>
                    <asp:TextBox ID="txtDS" runat="server" CssClass="m_txt" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    地上(层)：
                </td>
                <td>
                    <asp:TextBox ID="txtCSDS" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    最大高度(m)：
                </td>
                <td>
                    <asp:TextBox ID="txtZDGD" runat="server" CssClass="m_txt" ></asp:TextBox>
                </td>
            </tr>
           <tr>
                <td class="t_r t_bg">
                    地下(层)：
                </td>
                <td>
                    <asp:TextBox ID="txtCSDX" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    最大跨度(m)：
                </td>
                <td>
                    <asp:TextBox ID="txtZDKD" runat="server" CssClass="m_txt" ></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    计划开工日期：
                </td>
                <td>
                    <asp:TextBox ID="txtJHKGRQ" runat="server" CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    计划竣工日期：
                </td>
                <td>
                    <asp:TextBox ID="txtJHJGRQ" runat="server"  CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" ></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    发包方式：
                </td>
                <td>
                    <asp:DropDownList ID="ddlFBFS" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
                 <td class="t_r t_bg">
                    建筑性质：
                </td>
                <td>
                    <asp:DropDownList ID="ddlJZXZ" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    项目建设内容：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtXMJSNR" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
           </tr>
            <tr>
                <td class="t_r t_bg">
                    建设工程用地许可证编号：
                </td>
                <td>
                    地字第 <asp:TextBox ID="txtJSGCYDXKZH" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox> 号
                </td>
                 <td class="t_r t_bg">
                    建设工程规划许可证号：
                </td>
                <td>
                    建字第 <asp:TextBox ID="txtJSGCGHXKZH" runat="server" CssClass="m_txt" ></asp:TextBox> 号
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSDW" runat="server" CssClass="m_txt" Width="660px"></asp:TextBox>
                </td>
           </tr>
             <tr>
                 <td class="t_r t_bg">
                    建设单位地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtJSDWDZ" runat="server" CssClass="m_txt" Width="660px"></asp:TextBox>
                </td>
           </tr>
            <tr>
                <td class="t_r t_bg">
                    法定代表人：
                </td>
                <td>
                    <asp:TextBox ID="txtFDDBR" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    建设单位性质：
                </td>
                <td>
                    <asp:TextBox ID="txtJSDWXZ" runat="server" CssClass="m_txt"  Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td>
                    <asp:TextBox ID="txtLXR" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    联系电话：
                </td>
                <td>
                    <asp:TextBox ID="txtLXDH" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位银行信贷证明：
                </td>
                <td>
                    <asp:TextBox ID="txtJSDWYHXDZM" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    上级主管部门：
                </td>
                <td>
                    <asp:TextBox ID="txtSJZGBM" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    立项级别：
                </td>
                <td>
                    <asp:CheckBox ID="cbGWY" runat="server" Text="国务院(各部委)" />&nbsp;&nbsp;
                    <asp:CheckBox ID="cbSHENG" runat="server" Text="省" />&nbsp;&nbsp;
                    <asp:CheckBox ID="cbSHI" runat="server" Text="市" />&nbsp;&nbsp;
                    <asp:CheckBox ID="cbQU" runat="server" Text="区(县)" />&nbsp;&nbsp;
                </td>
           </tr>
           <tr>
                <td class="t_r t_bg">
                    立项文件：
                </td>
                <td>
                    <asp:TextBox ID="txtLXWJ" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    立项文号：
                </td>
                <td>
                    <asp:TextBox ID="txtLXWH" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
           <tr>
                <td class="t_r t_bg">
                    批准单位：
                </td>
                <td>
                    <asp:TextBox ID="txtPZDW" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    批准时间：
                </td>
                <td>
                    <asp:TextBox ID="txtPZSJ" runat="server" CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="200px"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    立项批准面积(㎡)：
                </td>
                <td>
                    <asp:TextBox ID="txtLXPZMJ" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    立项批准规模(㎡)：
                </td>
                <td>
                    <asp:TextBox ID="txtLXPZGM" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    当年投资(万元)：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDNTZ" runat="server"  CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                 <td class="t_r t_bg">
                    工程筹备情况：
                </td>
                <td colspan="3">
                     <asp:TextBox ID="txtGCCBQQ" runat="server" CssClass="m_txt" Width="500px" Height="60" TextMode="MultiLine"></asp:TextBox>
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
        <div style="font-size:13px;color:red;width:100%;text-align:left;padding-left:10px;margin-bottom:30px;">注：金额为万元，币种为人民币</div>
        </div>
    </form>
</body>
</html>

