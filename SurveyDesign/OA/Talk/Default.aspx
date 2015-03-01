<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="OA_Talk_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script language="javascript">
        function clear() {
            document.getElementById("eWebEditor1").value = "";
        }
        function contentEdit() {
            document.getElementById("tr_eWebEditor").style.display = "";
            document.getElementById("tr_clickEdit").style.display = "none";
            document.getElementById("eWebEditor1").focus();
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" cellpadding="0" cellspacing="0" style="border-top: #0099ff 1px solid;"
            width="100%">
            <tr>
                <td style="background-color: #e5ecf2; width: 260px; border-right: #cdcdcd 1px solid;" valign="top">
                    <table width="99%" style="margin-left: 3px;">
                        <tr>
                            <td style="width: 100%; border-bottom: #0099ff 1px dashed; height: 35px">
                                <div style="margin-left: 35px">
                                    <img src="../../OA/images/p2.gif" />
                                    <asp:LinkButton ID="Link_AppUserName" runat="server" ForeColor="#1F345B" Font-Underline="false"
                                        Font-Bold="true" Font-Size="13px"></asp:LinkButton>
                                    <asp:Label ID="label_IsLZ" runat="server" Font-Bold="true" ForeColor="#DA006E" Text="[楼主]"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 99%; height: 21px; padding-left: 10px">
                                部门：<asp:Label ID="Label_OrgName" ForeColor="#0840B2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 99%; height: 21px; padding-left: 10px">
                                电话：<asp:Label ID="Label_Tel" ForeColor="#0840B2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 99%; height: 21px; padding-left: 10px">
                                手机：<asp:Label ID="Label_Call" ForeColor="#0840B2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <%--                                                    <tr>
                                                        <td>
                                                            QQ：<asp:Label ID="Label_QQ" ForeColor="#0840B2" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>--%>
                        <tr>
                            <td style="width: 99%; height: 21px; padding-left: 10px">
                                EMail：<asp:Label ID="Label_Email" ForeColor="#0840B2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" align="left">
                    <table width="99%">
                        <tr>
                            <td align="left" valign="middle" style="border-bottom: #0099ff 1px dashed; height: 35px">
                                <div style="float: left; margin-left: 35px; width: 35px;">
                                    <img src="../../OA/images/renxi.jpg" />
                                </div>
                                <div style="float: left; width: 285px; margin-top: 3px">
                                    <asp:Label ID="Label_date" runat="server" Text="yyyy_MM_dd HH:MM:SS"></asp:Label>
                                </div>
                            </td>
                            <td align="right">
                                <asp:Label ID="Label_L" Font-Bold="true" runat="server" Text="#"></asp:Label>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="4" align="left" style=" padding-left: 20px; padding-top:20px">
                                <asp:Literal ID="lbuText" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" >
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="background-color: #e5ecf2; width: 260px; border-bottom: #c2d5e3 3px solid;">
                </td>
                <td>
                    <table width="99%">
                        <tr>
                            <td align="right" style="border-top: #0099ff 1px dashed; height: 20px" valign="middle">
                                <a href="#ttt" class="link8"><b>Top</b></a>&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
