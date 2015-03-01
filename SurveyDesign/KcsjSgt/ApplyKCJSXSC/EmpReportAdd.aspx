<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpReportAdd.aspx.cs" Inherits="KcsjSgt_ApplyKCJSXSC_EmpReport" %>

<%@ Register TagPrefix="uc1" TagName="pager" Src="~/Common/pager.ascx" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>问题</title>
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
            if (!AutoCheckInfo())
                return false;

            if (!getLength(document.getElementById("t_FTxt3"), 100, '“违反工程建设强制性标准编号及条文编号”')) {
                return false;
            }
            if (!getLength(document.getElementById("t_FContent"), 100, '“审查意见”')) {
                return false;
            }
            return true;
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

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                问题
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                资料类型：
            </td>
            <td>
              <%--  <asp:TextBox ID="" runat="server" CssClass="m_txt" MaxLength="25"></asp:TextBox>--%>
                 <asp:DropDownList ID="t_FTxt1" runat="server">
                 <%--   <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                    <asp:ListItem Value="1" Text="外业资料"></asp:ListItem>
                    <asp:ListItem Value="2" Text="实验资料"></asp:ListItem>
                    <asp:ListItem Value="3" Text="报告书"></asp:ListItem>--%>
                </asp:DropDownList>
               
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                图号/页码：
            </td>
            <td>
                <asp:TextBox ID="t_FTxt2" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                初审（复审）意见：
            </td>
            <td>
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Width="280px" MaxLength="50"
                    TextMode="MultiLine" Height="60"></asp:TextBox>
                <tt>*（50字内）</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                问题类别：
            </td>
            <td>
               <%-- <asp:TextBox ID="" runat="server" CssClass="m_txt" MaxLength="50"></asp:TextBox>--%>
                 <asp:DropDownList ID="t_FRemark" runat="server">
                  <%--  <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                    <asp:ListItem Value="1" Text="强条"></asp:ListItem>
                    <asp:ListItem Value="2" Text="非强条"></asp:ListItem>
                    <asp:ListItem Value="3" Text="法规"></asp:ListItem>--%>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="t_bg" align="center">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
                <input type="button" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
