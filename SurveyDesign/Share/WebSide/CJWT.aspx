<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CJWT.aspx.cs" Inherits="Share_WebSide_CJWT" %>

<%@ Register Src="../../Common/SelectSkin.ascx" TagName="SelectSkin" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <title>四川省勘察设计科技信息系统</title>
    <style type="text/css">
        html, body, form { width: 100%; height: 100%; background-color: #bfbfbf; }
        .indexPage_Right { width: 100%; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%; vertical-align: middle" align="center">
        <tr>
            <td valign="middle">
                <div id="indexPage" style="width: 900px; height: 495px; margin-left: auto; margin-right: auto;
                    background-color: #FFFFFF">
                    <div class="top_backimgindex">
                    </div>
                    <div class="indexPage_Right">
                        <div class="indexPage_BSZN_Top">
                            常见问题
                        </div>
                        <asp:Repeater ID="rep_BSZN" runat="server">
                            <ItemTemplate>
                                <div class="indexPage_BSZN">
                                    <a href='Article.aspx?FID=<%#Eval("FID") %>' target="_blank">
                                        <%#Eval("FName") %>
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Literal ID="lit_BSZN" runat="server"></asp:Literal>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
