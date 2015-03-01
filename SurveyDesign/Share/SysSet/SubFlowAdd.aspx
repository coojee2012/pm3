<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubFlowAdd.aspx.cs" Inherits="Admin_main_SubFlowAdd"
    EnableEventValidation="false" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });

        function CheckInfo() {
            if (document.getElementById("t_FName").value.trim() == "") {
                alert("流程名称必须填写");
                document.getElementById("t_FName").focus();
                return false;
            }
            if (document.getElementById("t_FLevel").value.trim() == "") {
                alert("流程等级必须填写");
                document.getElementById("t_FLevel").focus();
                return false;
            }
            return true;
        }
        function ifSaveOk() {
            var HSaveResult = document.getElementById("HSaveResult");
            if (HSaveResult) {
                window.returnValue = HSaveResult.value;
            }
            window.close();
        } 
    </script>

    <base target="_self" />
    </base>
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                子流程维护
            </th>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                &nbsp;<asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                &nbsp;<input id="btnBack" class="m_btn_w2" onclick="ifSaveOk();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="100%" align="center">
        <tr>
            <td class="t_r t_bg">
                父流程：
            </td>
            <td>
                <asp:TextBox ID="text_parentProcess" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                流程名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                等级：
            </td>
            <td>
                <asp:TextBox ID="t_FLevel" runat="server" CssClass="m_txt" onblur="isInt(this)"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                标准定义天数：
            </td>
            <td>
                <asp:TextBox ID="t_FDefineDay" runat="server" CssClass="m_txt" onblur="isInt(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt" onblur="isInt(this);"></asp:TextBox>
            </td>
        </tr>
        <%-- <tr>
                            <td class="tdRight">
                                是否需要转发：</td>
                            <td>
                                <asp:DropDownList ID="t_FIsSend" runat="server" CssClass="cTextBox1" AutoPostBack="True" OnSelectedIndexChanged="t_FIsSend_SelectedIndexChanged">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="2">否</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr id="ManamgeDep" runat="server">
                            <td class="tdRight">
                                转发部门：</td>
                            <td>
                                <uc1:Govdept ID="Govdept1" runat="server" />
                            </td>
                        </tr>--%>
        <tr>
            <td class="t_r t_bg">
                是否审核结束：
            </td>
            <td>
                <asp:DropDownList ID="t_FIsAppEnd" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="2" Selected="True">否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否就位定级：
            </td>
            <td>
                <asp:DropDownList ID="t_FIsQuali" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="2" Selected="True">否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否流程结束：
            </td>
            <td>
                <asp:DropDownList ID="t_FIsEnd" runat="server" CssClass="m_txt" OnSelectedIndexChanged="t_FIsEnd_SelectedIndexChanged">
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="2" Selected="True">否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属审核阶段：
            </td>
            <td>
                <asp:DropDownList ID="t_FTypeId" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="1">接件</asp:ListItem>
                    <asp:ListItem Value="3">正常审核</asp:ListItem>
                    <asp:ListItem Value="10">初审</asp:ListItem>
                    <asp:ListItem Value="5">负责人审核</asp:ListItem>
                    <asp:ListItem Value="7">归档</asp:ListItem>
                    <asp:ListItem Value="15">公示</asp:ListItem>
                    <asp:ListItem Value="20">建设部审核</asp:ListItem>
                    <asp:ListItem Value="25">转外审核</asp:ListItem>
                    <asp:ListItem Value="22">调出市审核</asp:ListItem>
                    <asp:ListItem Value="24">调入市审核</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                角色：
            </td>
            <td>
                <asp:DropDownList ID="t_FRoleId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否打印：
            </td>
            <td style="display: none">
                <asp:DropDownList ID="t_FIsPrint" runat="server" CssClass="m_txt" Enabled="False"
                    Visible="False">
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="2" Selected="True">否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                描述：
            </td>
            <td>
                <asp:TextBox ID="t_FDesc" runat="server" CssClass="m_txt" Width="280px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="t_FSendDeptId" runat="server" type="hidden" />
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
