<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="KC_ApplyKCXXBA_Report" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>提交</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();


            //选择是否同意时切换要填写的内容
            $("#t_FInt1").change(function() {
                tab();
            });
        });

        //选择是否同意时切换要填写的内容
        function tab() {
            var v = $("#t_FInt1").val();
            $("table[id^=tab_]").hide();
            $("table[id=tab_" + v + "]").show();
        }

        function checkInfo() {
            if ($("#t_FInt1").val() == "") {
                alert("请填写是否同意受理");
                $("#t_FInt1").focus();
                return false;
            }
            if ($("#t_FInt1").val() == "6") {
                if ($("#t_FTxt19").val() == "") {
                    alert("请填写办理意见");
                    $("#t_FTxt19").focus();
                    return false;
                }
                if ($(".m_dg1_i").length == 0) {
                    alert("请上传合同备案合同");
                    return false;
                }
            }
            else {
                if ($("#t_FTxt20").val() == "") {
                    alert("请填写退回原因");
                    $("#t_FTxt20").focus();
                    return false;
                }
            }
            return confirm('确认要上报吗？');
        }

        function up() {
            var width = "554";
            var height = "234";
            var idvalue = window.showModalDialog('UploadPhoto.aspx?rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:no;')
            if (idvalue != null && idvalue == "1") {
                document.getElementById('btnShowFile').click();
            }
        } 
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                数据上报
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="250">
                业务名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                上报部门：
            </td>
            <td>
                <asp:DropDownList ID="p_FManageDeptId" runat="server" CssClass="m_txt" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="2" class="t_c">
                <asp:Button ID="btnSave" runat="server" Text="上报" OnClick="btnSave_Click" CssClass="m_btn_w2" />
                <font color='red'>上报前请确定信息无误，上报后将不能修改。</font>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
