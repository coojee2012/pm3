<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="zAdd.aspx.cs"
    Inherits="OA_Bulletin_AddBulletin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公告发布</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> 

    </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });       
    </script>

    <script type="text/javascript" language="javascript">
        function onchick() {

            return true;

        }
         
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="Label1" runat="server" Text="公告发布"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <input id="exit" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg" style="width: 80px;">
                公告标题：
            </td>
            <td>
                <asp:TextBox ID="t_FTitle" runat="server" CssClass="m_txt" Width="350px" MaxLength="50"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否发布：
            </td>
            <td>
                <asp:CheckBox ID="t_FState" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                公告内容：
            </td>
            <td style="height: 320px">
                <input id="t_FContent" type="hidden" value="" name="content1" runat="server" class="m_txt"
                    style="width: 166px" enableviewstate="true" />
                <iframe id="eWebEditor1" src="../../eWebEditor/ewebeditor.htm?id=t_FContent&style=mini"
                    frameborder="0" scrolling="no" width="100%" height="100%" language="javascript">
                </iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
