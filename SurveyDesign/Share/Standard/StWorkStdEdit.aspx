<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StWorkStdEdit.aspx.cs" Inherits="Admin_Standard_StWorkStdEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>工程考核指标管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        function ifSaveOk() {
            var HSaveResult = document.getElementById("HSaveResult");
            if (HSaveResult) {
                window.returnValue = HSaveResult.value;
            }
            window.close();
        }  
    </script>

    <script language="javascript">
        function CheckInfo() {
            if (document.getElementById("t_FName").value.trim() == "") {
                alert("请填写工程标准名称");
                document.getElementById("t_FName").focus();
                return false;
            }
            if (document.getElementById("t_FLEVELID").value.trim() == "") {
                alert("请选择资质等级");
                document.getElementById("t_FLEVELID").focus();
                return false;
            }
            if (document.getElementById("t_FTTYPEID").value.trim() == "") {
                alert("请选择工程类型");
                document.getElementById("t_FTTYPEID").focus();
                return false;
            }

            if (document.getElementById("t_FRELATION").value.trim() == "") {
                alert("请填写与目标值关系");
                document.getElementById("t_FRELATION").focus();
                return false;
            }

            if (document.getElementById("t_FTARGETVALUE").value.trim() == "") {
                alert("请填写目标值");
                document.getElementById("t_FTARGETVALUE").focus();
                return false;
            } 
            if (document.getElementById("t_FNEEDCOUNT").value.trim() == "") {
                alert("请填写要满足的数量");
                document.getElementById("t_FNEEDCOUNT").focus();
                return false;
            } 
            return true;
        }
    </script>

    <base target="_self">
    </base>
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                工程考核指标管理
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />&nbsp;
                <input id="btnBack" class="m_btn_w2" type="button" value="返回" onclick="ifSaveOk();" />
            </td>
            <td width="20">
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="98%" class="m_table" align="center">
        <tr>
            <td class="t_r">
                指标名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>&nbsp;
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                工程类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FTTYPEID" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                单位：
            </td>
            <td>
                <asp:TextBox ID="t_FTARGETUNIT" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                是否必须：
            </td>
            <td>
                <asp:DropDownList ID="t_FOption" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="1" Text="是"></asp:ListItem>
                    <asp:ListItem Value="0" Text="否"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        </table>
    <input id="HSaveResult" runat="server" type="hidden" value="0" />
    </form>
</body>
</html>
