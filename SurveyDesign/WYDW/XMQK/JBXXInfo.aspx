<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBXXInfo.aspx.cs" Inherits="WYDW_XMQK_JBXXInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目基本情况</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" src="../../JSDW/../script/jquery.js"></script>

    <script type="text/javascript" src="../../JSDW/../script/default.js"></script>

    <script type="text/javascript" src="../../JSDW/../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();

        });
        function checkInfo() {
            return AutoCheckInfo();
        }
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
        <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv"> 
                <div style="position:absolute;left:40%;top:50%;background-color:peru;border:solid 3px red;">
                    <table  align="center">
                    <tr>
                    <td ><h1>正在保存数据</h1></td>
                    <td><img src="../../JSDW/../image/load2.gif" alt="请稍候"/></td>
                    </tr>
                                    
                    </table>
                </div>
                    </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
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
                            <asp:TextBox ID="t_ProjectName" ReadOnly="True" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1" width="10%"></td>
                        <td class="t_r t_bg" width="15%">项目地址：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:TextBox ID="t_Address" ReadOnly="True" runat="server" CssClass="m_txt" Width="220px" MaxLength="30"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1" width="10%"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目所属地：
                        </td>
                        <td colspan="1">
                            <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDW" ReadOnly="True" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">建设单位组织机构代码：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDWDM" ReadOnly="True" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位地址：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDWDZ" ReadOnly="True" runat="server" CssClass="m_txt" Width="220px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">联系法人：
                        </td>
                        <td>
                            <asp:TextBox ID="t_Contacts" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="10" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">联系法人手机：
                        </td>
                        <td>
                            <asp:TextBox ID="t_Mobile" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="20" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位技术负责人：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDWJSFZR" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="10" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">建设单位技术负责人职称：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDWJSFZRZC" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="10" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位技术负责人电话：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDWJSFZRDH" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="10" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目类型：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="t_XMLX" Enabled="False" runat="server" Width="120px" CssClass="m_txt">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                            </asp:DropDownList>
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">项目子类型：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="t_XMZLX" Enabled="False" runat="server" CssClass="m_txt" Width="120px">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                            </asp:DropDownList>
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设性质：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="t_JSXZ" Enabled="False" runat="server" Width="120px" CssClass="m_txt">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                            </asp:DropDownList>
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">建设模式：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="t_JSMS" Enabled="False" runat="server" CssClass="m_txt" Width="120px">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                            </asp:DropDownList>
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目总投资：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_XMZTZ" ReadOnly="True" runat="server" CssClass="m_txt"
                                onblur='isFloat(this);' Width="120px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td colspan="1">万元</td>
                        <td class="t_r t_bg">建设规模：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSGM" ReadOnly="True" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="120px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1">万平方米</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg"  height="100px">建设内容：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="t_JSNR" ReadOnly="True" runat="server" CssClass="m_txt"
                                MaxLength="4000" Width="99.5%" Height="100px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="t_r t_bg">创建时间：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_CreateTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">更新时间：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="t_Ftime" onfocus="WdatePicker()" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="195px"></asp:TextBox>
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
