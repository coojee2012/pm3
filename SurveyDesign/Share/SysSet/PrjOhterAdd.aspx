<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjOhterAdd.aspx.cs" Inherits="Admin_yamain_PrjOhterAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文件详细信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");

            $("#t_FType").change(function() {
                showMType();
            });
        });

        function showMType() {
            if ($("#t_FType").val() == "2") { $("#tr_FMType").show(); } else { $("#tr_FMType").hide(); }
        }

        function CheckInfo() {
            if (document.getElementById("t_FFileName").value.trim() == "") {
                alert("请选择附件");
                document.getElementById("t_FFileName").focus();
                return false;
            }
            return true;
        }

        function SelectFile() {
            var rv = window.showModalDialog('SelectFile.aspx?r=' + Math.random(), '', 'dialogWidth:650px; dialogHeight:450px; center:yes; resizable:no; status:no; help:no;scroll:auto;');
            if (rv != "" && rv != "undefined") {
                document.getElementById("hid_fileID").value = rv;
                document.getElementById("btnReload").click();
            }
        }

        //选择关联业务类型
        function SelectMType(FID) {
            if (FID) {
                var rv = window.showModalDialog('SelectMType.aspx?FID=' + FID + '&r=' + Math.random(), '', 'dialogWidth:650px; dialogHeight:480px; center:yes; resizable:no; status:no; help:no;scroll:auto;');
                if (rv != "" && rv != undefined) {
                    document.getElementById("btnMType").click();
                }
            }
            else {
                alert("请先保存");
            }
        }

        //全选
        function checkAll(obj) {
            var c = true;
            if (obj.checked == false)
                c = false;
            for (var i = 0; i < document.getElementById("check_FIsPrjType").getElementsByTagName("input").length; i++) {
                document.getElementById("check_FIsPrjType_" + i).checked = c;
            }
        }
       
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                文件详细信息
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                <asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                <input class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_table" id="TABLE1">
        <tr>
            <td class="t_r t_bg" width="120">
                业务类型：
            </td>
            <td>
                <asp:TextBox ID="txtFManageName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                业务编号：
            </td>
            <td>
                <asp:TextBox ID="txtFNumber" runat="server" CssClass="m_txt" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                文件形式：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server" CssClass="m_txt">
                    <asp:ListItem Text="上传文件" Value="1"></asp:ListItem>
                    <asp:ListItem Text="查看业务办理情况" Value="2"></asp:ListItem>
                    <asp:ListItem Text="施工许可证" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr_FMType" style="display: none;">
            <td class="t_r t_bg">
                关联业务编号：
            </td>
            <td class="m_txt_M">
                <asp:TextBox ID="t_FMType" runat="server" CssClass="m_txt" ReadOnly="True" Width="180px"></asp:TextBox>
                <input class="m_btn_w2" onclick="SelectMType('<%=ViewState["FID"] %>');" type="button"
                    value="选择" />
                <br />
                <tt style="margin-top:4px; display:block; line-height:20px;">
                    <asp:Literal ID="lit_FMType" runat="server"></asp:Literal>
                </tt>
                <asp:Button ID="btnMType" runat="server" OnClick="btnMType_Click" Style="display: none;" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                资料名称：
            </td>
            <td>
                <asp:TextBox ID="t_FFileName" runat="server" CssClass="m_txt" Width="250px" ReadOnly="True"></asp:TextBox>
                <input class="m_btn_w2" onclick="SelectFile();" type="button" value="选择" />
                <input id="hid_fileID" runat="server" type="hidden" />
                <asp:Button ID="btnReload" runat="server" OnClick="btnReload_ServerClick" Style="display: none;" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                应送份数：
            </td>
            <td>
                <asp:TextBox ID="t_FFileAmount" runat="server" CssClass="m_txt" Width="70px" onblur="isInt(this)"
                    MaxLength="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备注：
            </td>
            <td>
                <asp:TextBox ID="t_FRemark" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                显示顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt" Width="70px" onblur="isFloat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否必需：
            </td>
            <td>
                <asp:DropDownList ID="t_FIsMust" runat="server" CssClass="m_txt"  
                    OnSelectedIndexChanged="t_FIsMust_SelectedIndexChanged">
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr_prjType" runat="server"  >
            <td class="t_r t_bg">
                 工程类型：<br />
                <input type="checkbox" name="selectFlag" onclick="checkAll(this);" />全选
            </td>
            <td>
                <asp:CheckBoxList ID="check_FIsPrjType" runat="server" RepeatLayout="Flow" RepeatColumns="2"
                    RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
