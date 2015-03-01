<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualiAppConditionAdd.aspx.cs"
    Inherits="Admin_main_QualiAppConditionAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>资质审核认定条件维护</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });       
    </script>

    <script language="javascript">
    function CheckInfo()
    {
        if(document.getElementById("t_FName").value.trim()=="")
        {
            alert("名称必须填写");
            document.getElementById("t_FName").focus();
            return false;
        }
        if(document.getElementById("t_FSystemId").value.trim()=="")
        {
            alert("请选择所属系统");
            document.getElementById("t_FSystemId").focus(); 
            return false;
        }
         if(document.getElementById("t_FQualiLevelId").value.trim()=="")
        {
            alert("请选择所属资质级别");
            document.getElementById("t_FQualiLevelId").focus(); 
            return false;
        } 
        
        return true;
    }
    function ifSaveOk()
    {
        var HSaveResult=document.getElementById("HSaveResult");
        if(HSaveResult)
        {
            window.returnValue=HSaveResult.value;
        }
        window.close();
    } 
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                资质审核认定条件维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
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
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                名称：
            </td>
            <td>
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="300px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属系统：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server" CssClass="m_txt" OnSelectedIndexChanged="t_FSystemId_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属资质级别：
            </td>
            <td>
                <asp:DropDownList ID="t_FQualiLevelId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属序列：
            </td>
            <td>
                <asp:DropDownList ID="t_FQualiListId" runat="server" CssClass="m_txt" AutoPostBack="True"
                    OnSelectedIndexChanged="dbList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属专业：
            </td>
            <td>
                <asp:DropDownList ID="t_FQualiTypeId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
