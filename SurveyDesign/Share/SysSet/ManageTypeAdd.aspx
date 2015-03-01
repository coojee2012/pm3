<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="ManageTypeAdd.aspx.cs"
    Inherits="Admin_main_ManageTypeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务类型维护</title>
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
                alert("业务名称必须填写");
                document.getElementById("t_FName").focus();
                return false;
            }
            if (document.getElementById("t_FNumber").value.trim() == "") {
                alert("业务编号必须填写");
                document.getElementById("t_FNumber").focus();
                return false;
            }
            if (document.getElementById("t_FSystemId").value.trim == "") {
                alert("请选择所属系统");
                document.getElementById("t_FSystemId").focus();
                return false;
            }
            if (document.getElementById("t_FDesc").value.length > 15) {
                alert("说明应小于１５字");
                document.getElementById("t_FDesc").focus();
                return false;
            }
            return true;
        }
      
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                业务类型维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
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
            <td class="t_r">
                业务名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox><tt>*</tt>
            </td>
            <td class="t_r">
                业务编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                承办处室：
            </td>
            <td>
                <asp:TextBox ID="t_FOperDeptName" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
            <td class="t_r">
                所属系统：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                资质标准页地址：
            </td>
            <td>
                <asp:TextBox ID="t_FLawUrl" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
            <td class="t_r">
                示范表格下载地址：
            </td>
            <td>
                <asp:TextBox ID="t_FTableUrl" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                说明：
            </td>
            <td>
                <asp:TextBox ID="t_FDesc" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>(15字)
            </td>
            <td class="t_r">
                显示顺序：
            </td>
            <td>
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt" Width="80px" onblur="isInt(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                类别：
            </td>
            <td>
                <asp:DropDownList ID="t_FTypeId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td class="t_r">
                审核时限：
            </td>
            <td>
                <asp:TextBox ID="t_FDay" runat="server" CssClass="m_txt" onblur="isInt(this);" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                公示时限：
            </td>
            <td>
                <asp:TextBox ID="t_FPublicDay" runat="server" CssClass="m_txt" onblur="isInt(this);"
                    Width="80px"></asp:TextBox>
            </td>
            <td class="t_r">
                类型编号：
            </td>
            <td>
                <asp:TextBox ID="t_FMTypeId" runat="server" CssClass="m_txt" onblur="isInt(this);"
                    Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                审核地址：
            </td>
            <td>
                <asp:TextBox ID="t_FAUrl" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
            <td class="t_r">
                查询地址：
            </td>
            <td>
                <asp:TextBox ID="t_FQurl" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                作为附件时查看地址：
            </td>
            <td>
                <asp:TextBox ID="t_FLookFJurl" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
            </td>
                        <td class="t_r">
               是否自动审批
            </td>
            <td>
                <asp:TextBox ID="t_FIsPrint" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                业务介绍：
            </td>
            <td colspan="3">
                <input id="FMain" runat="server" name="content1" type="hidden" value="<p>请输入新闻内容！</p>" /><iframe
                    id="eWebEditor1" frameborder="0" height="500" language="javascript" scrolling="no"
                    src="../../eWebEditor/ewebeditor.htm?id=FMain&style=mini" width="100%"></iframe>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
