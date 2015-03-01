<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XMdetailTop.aspx.cs" Inherits="audit_XM_XMdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">工程明细
                </th>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr background="include/images/list_area_nav.jpg" style="background-repeat: no-repeat; background-color: #FFF;">
                <td style="height: 28px;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr style="background-repeat: no-repeat; background-color: #FFF;">
                            <td style="height: 28px; width: 15px;">&nbsp;
                            </td>
                            <td style="height: 28px; width: 145px; background-image: url(../images/hp_bg.jpg)" valign="middle">
                                <span class="style1">项目进度</span>
                            </td>
                            <td style="height: 28px;" valign="middle" align="right">
                                <img src="../images/hp_xmjd_ybl.jpg" alt="已办理" width="12" height="12" /><span class="style9">已办理</span>
                                &nbsp;<img src="../images/hp_xmjd_dbl.jpg" alt="未办理" width="12" height="12" /><span
                                    class="style9">未办理</span>&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: 1px #d9e1e4 solid; height: 110px;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 70px;">
                        <tr>
                            <td class="style13">&nbsp;
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="选址意见书" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">选址意见书
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="用地规划许可证" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">用地规划许可证
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="工程规划许可证" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">工程规划许可证
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="初步设计" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">初步设计
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="施工图审查" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">施工图审查
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" colspan="3">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style13">&nbsp;
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="项目报建" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">项目报建
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="招投标备案" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">招投标备案
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="质量监督备案" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">质量监督备案
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="安全监督备案" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">安全监督备案
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="施工许可证" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">施工许可证
                            </td>
                            <td valign="middle" align="center" class="style14">
                                <img src="../images/hp_xmjd_jt.jpg" alt="" width="30" height="14" />
                            </td>
                            <td valign="middle" align="center" class="style10">

                                <img src="../images/hp_xmjd_dbl.jpg" alt="竣工验收备案" width="12" height="12" />

                            </td>
                            <td valign="middle" class="style12">竣工验收备案
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
