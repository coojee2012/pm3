<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpLoadPrjPic.aspx.cs" Inherits="PersonApp_CommonPager_UpLoadPrjPic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script>
        $(document).ready(function() {
            txtCss();
        });
    </script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <base target="_self"></base>
    <title>上传图片</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                上传附件
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td colspan="2" align="left">
                可以上传word 格式或图片(图片应小于900K，图片格式gif,jpg)
                <br />
           
            </td>
        </tr>
        <tr>
            <td width="100">
                <img id="img_EmpPic" runat="server" height="100" width="85" enableviewstate="true" />
            </td>
            <td valign="middle">
                <input id="File1" type="file" runat="server" style="width: 280px" class="m_txt" />
                <asp:Button ID="btnUp" runat="server" Text="上传" OnClick="btnUp_Click" CssClass="m_btn_w2" />
                <input id="btnReturn" type="button" value="返回" onclick="window.close();" class="m_btn_w2" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
