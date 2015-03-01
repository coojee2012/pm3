<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBXXInfo.aspx.cs" Inherits="WYDW_ApplyBaseInfo_Info" %>


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
                <tr runat="server" id="tr_his" visible="false">
                    <td class="t_r t_bg" width="15%">
                        <tt>历次变更记录：>历次变更记录：</tt>
                    </td>
                    <td class="t_l">
                        <asp:DropDownList ID="ddlHis" runat="server" AutoPostBack="true"
                            TabIndex="10">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
                <table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td></td>
                        <td class="t_r">
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                OnClientClick="return checkInfo();" />
                            <%--<input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />--%>
                        </td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>
                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td class="t_r t_bg" width="15%">项目名称：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:TextBox ID="t_ProjectName" Enabled="False" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1" width="10%"></td>
                        <%--<td class="t_r t_bg" width="15%">项目编号：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:TextBox ID="t_ProjectNo" runat="server" CssClass="m_txt" Width="120px" Enabled="false" ToolTip="系统自动生成"></asp:TextBox>
                        </td>
                        <td colspan="1" width="10%"></td>--%>
                        <td class="t_r t_bg" width="15%">项目地址：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="220px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1" width="10%"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目所属地：
                        </td>
                        <td colspan="1">
                            <uc1:govdeptid ID="govd_FRegistDeptId"  runat="server" />
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">建设单位组织机构代码：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDWDM" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位地址：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="t_JSDWDZ" runat="server" CssClass="m_txt" Width="220px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">联系法人：
                        </td>
                        <td>
                            <asp:TextBox ID="t_Contacts" runat="server" CssClass="m_txt" MaxLength="10" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">联系法人手机：
                        </td>
                        <td>
                            <asp:TextBox ID="t_Mobile" onblur="isTel(this)" runat="server" CssClass="m_txt" MaxLength="20" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位技术负责人：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDWJSFZR" runat="server" CssClass="m_txt" MaxLength="10" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">建设单位技术负责人职称：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDWJSFZRZC" runat="server" CssClass="m_txt" MaxLength="10" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">建设单位技术负责人电话：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSDWJSFZRDH" onblur="isTel(this)" runat="server" CssClass="m_txt" MaxLength="20" Width="120px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目类型：
                        </td>
                        <%--<td colspan="1">
                            <asp:DropDownList ID="t_XMLX" runat="server" Width="120px" CssClass="m_txt">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                            </asp:DropDownList>
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">项目子类型：
                        </td>--%>
                        <td colspan="1">
                            <asp:DropDownList ID="t_XMZLX" runat="server" CssClass="m_txt" Width="120px">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                            </asp:DropDownList>
                            <tt>*</tt>
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
                            <asp:DropDownList ID="t_JSXZ" runat="server" Width="120px" CssClass="m_txt">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                            </asp:DropDownList>
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">建设模式：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="t_JSMS" runat="server" CssClass="m_txt" Width="120px">
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
                            <asp:TextBox ID="t_XMZTZ" runat="server" CssClass="m_txt"
                                onblur='isFloat(this);' Width="120px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td colspan="1">万元</td>
                        <td class="t_r t_bg">建设规模：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JSGM" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="160px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg" height="100px">建设内容：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="m_txt"
                                MaxLength="4000" Width="99.5%" TextMode="MultiLine" Height="95px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg" height="100px">建设内容：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="t_JSNR" runat="server" CssClass="m_txt"
                                MaxLength="4000" Width="99.5%" TextMode="MultiLine" Height="95px"></asp:TextBox>
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
