<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Revert.aspx.cs" Inherits="Admin_main_Revert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
        }

        $(document).ready(function() {
            //文本框样式
            txtCss();
            DynamicGrid(".m_dg1_i");
        }); 
        function checkInfo()
        {
         
            if(document.getElementById("t_FRevertPerson").value.trim()=="")
            {
                alert("回复人不能为空,请输入");
                document.getElementById("t_FRevertPerson").focus();
                return false;
            }
            
               if(document.getElementById("t_FRevertContent").value.trim()=="")
            {
                alert("回复内容不能为空,请输入");
                document.getElementById("t_FRevertContent").focus();
                return false;
            }
            
             
            if(!clen(document.getElementById("t_FRevertContent"),1000))
            {
                alert("回复内容内容字数超过长度限制,请重新输入");
                document.getElementById("t_FRevertContent").focus();
                return false;
            } 
            return true;
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                留言回复
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="tdRight">
                留言人：
            </td>
            <td>
                <asp:TextBox ID="t_FLevelPerson" runat="server" CssClass="m_txt" Enabled="False"
                    EnableTheming="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdRight">
                留言人联系方式：
            </td>
            <td>
                <asp:DropDownList ID="t_FLinkTypeId" runat="server" CssClass="m_txt" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdRight">
                留言人联系内容：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkContent" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdRight">
                留言主题：
            </td>
            <td>
                <asp:TextBox ID="t_FTitle" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdRight" style="height: 29px">
                <span style="color: #007faa">留言主题内容</span>：
            </td>
            <td style="height: 29px">
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Height="170px" TextMode="MultiLine"
                    Width="500px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdRight" style="height: 13px">
                留言时间：
            </td>
            <td style="height: 13px">
                <asp:TextBox ID="t_FCreateTime" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdRight" style="height: 13px">
                回复人：
            </td>
            <td style="height: 13px">
                <asp:TextBox ID="t_FRevertPerson" runat="server" CssClass="m_txt" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdRight" style="height: 27px">
                回复内容：
            </td>
            <td style="height: 27px">
                <asp:TextBox ID="t_FRevertContent" runat="server" CssClass="m_txt" TextMode="MultiLine"
                    Height="170px" Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdRight" style="height: 8px">
                回复日期：
            </td>
            <td style="height: 8px">
                <asp:TextBox ID="t_FRevertDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table align="center" width="100%" style="margin-top: 2px;">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="回复" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
