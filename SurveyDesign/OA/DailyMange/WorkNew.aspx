<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="WorkNew.aspx.cs" Inherits="OA_DailyMange_WorkEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建事务</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <link href="../style/Grid.css" rel="Stylesheet" type="text/css" />

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">

        function CheckPeople() {
            var rv = window.showModalDialog('CheckPeople.aspx?rid=' + Math.random(), '', 'dialogWidth:330px; dialogHeight:600px; center:yes; resizable:no; status:no; help:no;scroll:auto;');

            if (rv != "" && rv != "undefined") {
                document.getElementById("t_FBaseId").value = rv;
            }

        } 
      
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" class="m_title" width="100%">
        <tr>
            <th colspan="3">
                &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Label ID="l_BB" runat="server" ForeColor="#2A586F">新建事务</asp:Label>
                <asp:Label ID="lblDate" runat="server" ForeColor="Red"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right">
                <input id="btnSave" runat="server" class="m_btn_w2" onclick="" value="保存" type="button"
                    onserverclick="btnConfirmOff_ServerClick" />
                <input id="exit" runat="server" class="m_btn_w2" type="button" value="返回" onserverclick="exit_ServerClick1" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="100%" align="center">
        <tr>
            <td class="t_r t_bg" style="width:150px">
                开始时间：
            </td>
            <td>
                <asp:DropDownList ID="ddlBeginH" runat="server" TabIndex="1">
                </asp:DropDownList>
                时
                <asp:DropDownList ID="ddlBeginM" runat="server">
                </asp:DropDownList>
                分<tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                结束时间：
            </td>
            <td>
                <asp:DropDownList ID="ddlEndH" runat="server">
                </asp:DropDownList>
                时
                <asp:DropDownList ID="ddlEndM" runat="server">
                </asp:DropDownList>
                分<tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                事物类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Text="工作事务" Value="0"></asp:ListItem>
                    <asp:ListItem Text="个人事务" Value="1"></asp:ListItem>
                </asp:DropDownList><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                定期模式：
            </td>
            <td>
                <asp:DropDownList ID="ddlTermly" runat="server">
                    <asp:ListItem Text="只一次" Value="3"></asp:ListItem>
                    <asp:ListItem Text="每日" Value="0"></asp:ListItem>
                    <asp:ListItem Text="每周" Value="1"></asp:ListItem>
                    <asp:ListItem Text="每月" Value="2"></asp:ListItem>
                </asp:DropDownList><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                事务标题：
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" CssClass="m_txt" Width="90%"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                事务内容：
            </td>
            <td style="height: 220px">
                <asp:TextBox ID="txtContent" runat="server" MaxLength="100" TextMode="MultiLine"
                    CssClass="m_txt" Width="90%" Height="210px"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="代下属上报" OnCheckedChanged="CheckBox1_CheckedChanged"
                    AutoPostBack="True" TextAlign="Left" />--%>
            </td>
            <td>
                <%--<div id="assign" runat="server" visible="false">
                    人员姓名：<asp:TextBox ID="t_FUserName" runat="server" Width="95px" MaxLength="20" CssClass="m_txt"></asp:TextBox>
                    <span style="color: #ff0000">*</span>&nbsp;
                    <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server" OnClientClick=" CheckPeople();">【选择】</asp:LinkButton>
                    <input id="t_FBaseId" runat="server" type="hidden" />
                </div>--%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    function exitt() {
        window.returnValue = 1;
        window.close();
    }
    function returnback() {
        history.back();
    }
</script>

