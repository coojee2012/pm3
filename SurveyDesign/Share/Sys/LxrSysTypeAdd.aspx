<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LxrSysTypeAdd.aspx.cs" Inherits="Admin_User_LxrSysTypeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
    <title>添加客服</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            //文本框样式
            txtCss();
        });

        $(function() {
            txtCss(); //文本框样式  
            $("#btnBack").click(function() {
                window.returnValue = $("#HSaveResult").val();
                window.close();
            });
        });
        function exitt() {
            if ($("#HSaveResult").val() == "1")
                window.returnValue = "1";
            window.close();
        }
        function CheckInfo() {
            if ($("#t_FType").val() == "86602") {
                var t_FUserId = document.getElementById("t_FUserId");
                if (t_FUserId) {
                    if (t_FUserId.value == "") {
                        alert("请选择关联的用户。");
                        return false;
                    }
                }
            }
            return AutoCheckInfo();
        }

        function showSelectWindow(sUrl, width, height) {
            var result = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')
            if (result) {
                var t_FUserId = document.getElementById("t_FUserId");
                if (t_FUserId) {
                    t_FUserId.value = result;
                }
            }
        }
        
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="Label1" runat="server" Text="添加客服"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <input id="btnExit" class="m_btn_w2" value="返回" type="button" onclick="exitt();" />
            </td>
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table align="center" class="m_table" width="98%">
        <tr>
            <td class="t_r t_bg">
                用户类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="t_FType_SelectedIndexChanged">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox runat="server" Text="" CssClass="m_txt" ID="t_FLinkName" Width="101px"></asp:TextBox><tt>*</tt>
                <asp:Button ID="btnSelect" runat="server" CssClass="m_btn_w2" Text="选择" OnClientClick="showSelectWindow('LxrSysTypeAddSelect.aspx?e=0',800,600);"
                    OnClick="btnSelect_Click" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                系统类型：
            </td>
            <td>
                <asp:CheckBoxList runat="server" ID="t_FSystemId" RepeatLayout="Flow">
                </asp:CheckBoxList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属主管部门：
            </td>
            <td>
                <asp:DropDownList ID="t_FDeptId" runat="server" CssClass="m_txt" Width="143px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                在线客服登录ID：
            </td>
            <td>
                <asp:TextBox runat="server" Text="" CssClass="m_txt" ID="t_FLoginKey" Height="17px"
                    Width="187px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                排序：
            </td>
            <td>
                <asp:TextBox runat="server" Text="" CssClass="m_txt" ID="t_FOrder" Width="93px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="t_FUserId" runat="server" />
    <input id="HSaveResult" runat="server" type="hidden" value="0" />
    <input id="hiddenBaseId" runat="server" type="hidden" />
    </form>
</body>
</html>
