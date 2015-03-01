<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpPic.aspx.cs" Inherits="Share_main_UpPic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css"></asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <base target="_self">
    </base>
    <title>上传图片</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                上传图片
            </th>
        </tr>
    </table>
    
        <table  width="98%" align="center" class="m_table">

            <tr>
                <td colspan="2">
                    (图片应小于200K,最佳大小为100*100)</td>
            </tr>
            <tr>
                <td style="width:100px;">
                    <img id="img_EmpPic" runat="server" height="100" width="100" enableviewstate="true" />
                </td>
                <td>
                    <input id="File1" type="file" runat="server" style="width: 180px" class="m_txt" />
                    <asp:Button ID="btnUp" runat="server" Text="上传" OnClick="btnUp_Click" CssClass="m_btn_w2" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>


