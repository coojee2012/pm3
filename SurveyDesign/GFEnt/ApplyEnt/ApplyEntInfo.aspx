<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyEntInfo.aspx.cs" Inherits="GFEnt_EntInfo" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function getXS(type) {
            var rFalse = document.getElementById("rFalse");
            var rTrue = document.getElementById("rTrue");
            if (type == "0") {
                document.getElementById("trtrue").style.display = "block";
                document.getElementById("trfalse").style.display = "none";
                rFalse.checked = false;
                rTrue.checked = true;
            }
            else {
                document.getElementById("trtrue").style.display = "none";
                document.getElementById("trfalse").style.display = "block";
                rFalse.checked = true;
                rTrue.checked = false;
            }
            document.getElementById("t_FUpgradeTF").value = type;
        }
        function getFilUp(url) {
            var fid = document.getElementById("t_YWBM").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?FAppId=" + fid + "&type=1000", 550, 400);
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 345px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">基本信息
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r" style="padding-right: 10px;">
                    <input type="hidden" id="hidd_FLinkId" runat="server" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
                    <asp:Button ID="btnQuery" Style="display: none;" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">工法名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_GFMC" runat="server" CssClass="m_txt" Width="350px"></asp:TextBox>
                    <input type="hidden" id="p_FId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">申报单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">申报单位所属地：
                </td>
                <td colspan="3">
                    <uc1:govdeptid ID="t_FUpDeptName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">联系人：
                </td>
                <td>
                    <asp:TextBox ID="t_Linkman" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r t_bg">联系手机：
                </td>
                <td>
                    <asp:TextBox ID="t_LinkmanMobile" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">类别：
                </td>
                <td>
                    <asp:DropDownList ID="t_FListName" Width="120px" CssClass="m_txt" runat="server" OnSelectedIndexChanged="t_FListName_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem>--请选择--</asp:ListItem>
                        <asp:ListItem>房屋建筑工程</asp:ListItem>
                        <asp:ListItem>土木工程</asp:ListItem>
                        <asp:ListItem>工业安装工程</asp:ListItem>
                        <asp:ListItem>其他</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">专业分类：
                </td>
                <td>
                    <asp:DropDownList ID="t_FTypeName" Width="120px" CssClass="m_txt" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="t_FTypeName1" CssClass="m_txt" Visible="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">工法内容材料：
                </td>
                <td colspan="3">
                    <input id="btnUP" type="button" runat="server" class="m_btn_w6" value="文件上传" onclick='getFilUp("FileUp.aspx");' />
                </td>
            </tr>
            <tr style="display: none;">
                <td class="t_r t_bg">是否升级版工法：
                </td>
                <td colspan="3">
                    <input id="rTrue" runat="server" type="radio" name="rTrue" onclick="return getXS('0');" />是
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="rFalse" runat="server" type="radio" checked name="rFalse" onclick="return getXS('1');" />否
                </td>
            </tr>
            <tr style="width: 100%; display: none; text-align: left;" id="trtrue" runat="server">
                <td colspan="4" style="width: 100%; text-align: left;">
                    <table style="width: 100%; height: 100%;" class="m_table">
                        <tr>
                            <td class="t_r t_bg" style="width: 21%;">原工法名称：
                            </td>
                            <td style="width: 79%;">
                                <asp:TextBox ID="t_FOldGFMC" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg">原完成单位：
                            </td>
                            <td>
                                <asp:TextBox ID="t_FOldName" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg">原国家级工法批准文号：
                            </td>
                            <td>
                                <asp:TextBox ID="t_FOldGJJGFPZWH" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="t_r t_bg">原工法编号：
                            </td>
                            <td>
                                <asp:TextBox ID="t_FOldGFBH" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trfalse" runat="server" style="display: block;">
                <td class="t_r t_bg">工法内容简述：
                </td>
                <td colspan="3" style="width: 80%">
                    <asp:TextBox ID="ts_NRJS" runat="server" CssClass="m_txt" Width="90%" Height="150px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <input id="t_FUpgradeTF" runat="server" type="hidden" value="" />
        <input id="t_YWBM" runat="server" type="hidden" />
        <input id="t_FBaseInfoId" runat="server" type="hidden" />
        <input id="t_FSystemId" runat="server" type="hidden" />
        <input id="t_FID" runat="server" type="hidden" />
        <input id="ts_FID" runat="server" type="hidden" />
    </form>
</body>
</html>
