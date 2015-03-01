<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="TalkEdit.aspx.cs"
    Inherits="OA_Talk_TalkEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>讨论内容编辑</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        function addpreson(obj) {
            var str = document.getElementById(obj).value;
            var s = showModalDialog("../main/CheckPresonList.aspx?PresonList=" + str + "", "", "dialogWidth=608px;dialogHeight=635px");


            if (s != "undefined" && s != null) {
                document.getElementById(obj).value = s;
                document.getElementById('Button1').click();
            }
        }
        function checkInfo() {
            return AutoCheckInfo();
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" class="m_title" width="98%">
        <tr>
            <th align="left">
                <asp:Label ID="t_BB" runat="server" Text="编辑新讨论"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" OnClick="btnSave_Click"
                    Text="保存" OnClientClick="return checkInfo();" />
                <asp:Button ID="btnOK" runat="server" CssClass="m_btn_w2" OnClick="btnOK_Click" Text="提交"
                    Visible="false" />
                <input id="btnGetBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />&nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table align="center" class="m_table" style="width: 98%">
        <tr>
            <td class="t_r t_bg" width="90">
                标题：
            </td>
            <td>
                <asp:TextBox ID="t_FTalkName" runat="server" CssClass="m_txt" MaxLength="50" TabIndex="1"
                    Width="448px"></asp:TextBox><tt>*</tt> (50字以内)
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                板块：
            </td>
            <td>
                <img src="../../image/Talk1.gif" />
                <asp:DropDownList ID="t_Fproject" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                口令：
            </td>
            <td>
                <asp:TextBox ID="t_FKey" runat="server" CssClass="m_txt" Width="80px" MaxLength="8"></asp:TextBox>
                <font color="red">字母和数字组成，最长8位，不填为公开。</font>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系方式：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkWay" runat="server" CssClass="m_txt" Width="150px" MaxLength="25"></asp:TextBox>
                <font color="red">方便参与讨论者与您联系</font>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                讨论内容：
            </td>
            <td class="m_txt_M">
                <input id="t_FContent" runat="server" class="m_txt" name="content1" style="width: 166px"
                    type="hidden" /><iframe id="eWebEditor1" frameborder="0" height="400" language="javascript"
                        scrolling="no" src="../../eWebEditor/ewebeditor.htm?id=t_FContent&style=mini"
                        width="100%" style="width: 100%;"></iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script type="text/javascript">
    function exitt() {
        var ok = document.getElementById("SaveIsOK").value;
        if (ok == "1") {
            window.returnValue = "1";
        }
        window.close();
    }
</script>

