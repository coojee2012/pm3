<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LookBull.aspx.cs" Inherits="OA_Bulletin_LookBull" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公告查看</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <style type="text/css">
        </style>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"> </script>

    <script type="text/javascript" language="javascript">
        function onchick() {

            return true;

        }
        function addpreson(obj) {
            var str = document.getElementById(obj).value;
            var s = showModalDialog("../main/CheckRole.aspx?PresonList=" + str + "", "", "dialogWidth=608px;dialogHeight=635px");



            if (s != null && s != "undefined") {
                document.getElementById(obj).value = s;

            }
            else {
                return;
            }

            document.getElementById('btnReload').click();
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%" align="center" class="m_title">
            <tr>
                <th colspan="5">
                    <asp:Label ID="Label1" runat="server" Text="公告查看"></asp:Label>
                </th>
            </tr>
        </table>
        <table width="100%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m" style=" text-align:right">
                    <input id="Button1" class="m_btn_w2" onclick="exitt();" type="button" value="返回" />&nbsp;
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="m_table"
            style="width: 98%; height: 80px">
            <tr>
                <td align="center" colspan="3" style="font-weight: bold; font-size: 16px; height: 17px"
                    class="m_title">
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 17%; height: 16px">
                    公告标题：
                </td>
                <td colspan="2">
                    <asp:TextBox ID="t_FTitle" runat="server" CssClass="m_txt" Width="285px" TabIndex="3"
                        ReadOnly="True"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 17%; height: 16px">
                    有效期：
                </td>
                <td>
                    起始日期：<asp:TextBox ID="t_FDateOn" CssClass="m_txt" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    终止日期：<asp:TextBox ID="t_FDateOff" CssClass="m_txt" runat="server"  ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 17%; height: 16px">
                    消息类型：
                </td>
                <td colspan="2">
                    <asp:TextBox CssClass="m_txt" ID="t_FBulTypeId" runat="server" ReadOnly="True"></asp:TextBox>
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 17%; height: 16px">
                    发布范围：
                </td>
                <td colspan="2">
                    <%--<table border="0" style="width:98%" >
                                <tr>
                                    <td class="td14" style="width: 75%">--%>
                    <asp:TextBox ID="presonList" runat="server" CssClass="m_txt" Width="98%" TextMode="MultiLine"
                        ReadOnly="True"></asp:TextBox>
                    <%--</td>
                                    <td class="td14" style="width: 25%">--%>
                    <input id="btnAddPreson" type="hidden" class="cBtn1" style="height: 21px" value="添加"
                        onclick="addpreson('presonFID')" />
                    <input id="presonFID" runat="server" type="hidden" />
                    <%--</td>
                                </tr>
                            </table>--%>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 17%; height: 16px">
                    公告内容：
                </td>
                <td style="height: 220px; vertical-align: top" colspan="2">
                    <%--<asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Width="98%" TextMode="MultiLine"
                        ReadOnly="True" Height="213px"></asp:TextBox>--%>
                        
                        <asp:Literal ID="t_FContent" runat="server"></asp:Literal>
                    <%--<asp:Label ID="t_FContent" runat="server" Text="Label"></asp:Label>--%>
                </td>
            </tr>
        </table>
        <table align="center" style="width: 98%">
            <tr>
                <td align="center" style="height: 25px">
                    &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnReload" runat="server" Text="Button" OnClick="btnReload_Click"
                        Style="display: none" Width="0px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    function exitt() {
        window.returnValue = 1;
        window.close();
    }

</script>

