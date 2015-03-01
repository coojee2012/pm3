<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LSXMInfo.aspx.cs" Inherits="WYDW_LSXM_LSXMInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目基本情况</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });

    </script>

    <base target="_self"></base>
    <style type="text/css">
        .modalDiv {
            position: absolute;
            top: 1px;
            left: 1px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: gray;
            opacity: .50;
            filter: alpha(opacity=50);
        }

        .auto-style1 {
            height: 24px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">

        <div style="height: 100%; width: 100%;">

            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">项目基本情况
                    </th>
                </tr>

            </table>
            <div id="divSetup2" runat="server">

                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td class="t_r t_bg" width="15%">项目名称：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:Label ID="t_ProjectName" runat="server" Width="180px"></asp:Label>

                        </td>
                        <td colspan="1" width="10%"></td>
                        <%--<td class="t_r t_bg" width="15%">项目编号：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:Label ID="t_ProjectNo" runat="server"  Width="120px" Enabled="false" ToolTip="系统自动生成"></asp:Label>
                        </td>
                        <td colspan="1" width="10%"></td>--%>
                        <td class="t_r t_bg" width="15%">项目地址：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:Label ID="t_Address" runat="server" Width="220px"></asp:Label>

                        </td>
                        <td colspan="1" width="10%"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目所属地：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_XMSD" runat="server" Width="220px"></asp:Label>

                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_JSDW" runat="server" Width="180px"></asp:Label>

                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">建设单位组织机构代码：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_JSDWDM" runat="server" Width="120px"></asp:Label>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位地址：
                        </td>
                        <td colspan="5">
                            <asp:Label ID="t_JSDWDZ" runat="server" Width="220px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">联系法人：
                        </td>
                        <td>
                            <asp:Label ID="t_Contacts" runat="server" MaxLength="10" Width="120px"></asp:Label>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">联系法人手机：
                        </td>
                        <td>
                            <asp:Label ID="t_Mobile" runat="server" MaxLength="20" Width="120px"></asp:Label>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位技术负责人：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_JSDWJSFZR" runat="server" MaxLength="10" Width="120px"></asp:Label>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">建设单位技术负责人职称：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_JSDWJSFZRZC" runat="server" MaxLength="10" Width="120px"></asp:Label>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位技术负责人电话：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_JSDWJSFZRDH" runat="server" MaxLength="20" Width="120px"></asp:Label>
                        </td>
                        <td colspan="1"></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目类型：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_XMZLX" runat="server" Width="120px">
                            </asp:Label>

                        </td>
                        <td colspan="1"></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设性质：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_JSXZ" runat="server" Width="120px">
                            </asp:Label>

                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">建设模式：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_JSMS" runat="server" Width="120px">
                            </asp:Label>

                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目总投资：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_XMZTZ" runat="server" Width="120px" MaxLength="20"></asp:Label>
                        </td>
                        <td colspan="1">万元</td>
                        <td class="t_r t_bg">建设规模：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_JSGM" runat="server"
                                MaxLength="20" Width="120px"></asp:Label>

                        </td>
                        <td colspan="1">万平方米</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg" height="100px">建设内容：
                        </td>
                        <td colspan="5">
                            <asp:Label ID="t_JSNR" runat="server"
                                MaxLength="4000" Width="99.5%" TextMode="MultiLine" Height="95px"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="t_r t_bg">创建时间：
                        </td>
                        <td colspan="1">
                            <asp:Label ID="t_CreateTime" onfocus="WdatePicker()" runat="server" 
                                MaxLength="20" Width="195px"></asp:Label>
                        </td>
                        <td class="t_r t_bg">更新时间：
                        </td>
                        <td colspan="3">
                            <asp:Label ID="t_Ftime" onfocus="WdatePicker()" runat="server" 
                                MaxLength="20" Width="195px"></asp:Label>
                        </td>
                    </tr>--%>
                </table>
            </div>
            <input id="t_AddressDept" type="hidden" runat="server" />
            <input id="t_Province" type="hidden" runat="server" />
            <input id="t_City" type="hidden" runat="server" />
            <input id="t_County" type="hidden" runat="server" />
            <input id="txtFId" type="hidden" runat="server" />
            <input id="txtUrl" type="hidden" runat="server" />
        </div>


    </form>
</body>

<script type="text/javascript">
    function changeCheck(obj) {
        obj.style.background = obj.checked ? '#1eaffc' : "";
    }
    $.each($(":checkbox[id^=t_F]"), function () {
        $(this).click(function () { changeCheck(this); });
        changeCheck(this);
    });
</script>

</html>
