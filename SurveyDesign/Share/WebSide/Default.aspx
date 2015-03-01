<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Share_WebSide_Default" %>

<%@ Register Src="abottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%@ Register Src="atop.ascx" TagName="top" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>四川省勘察设计科技管理信息平台</title>
    <link rel="stylesheet" type="text/css" href="../style/css.css" />

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <style type="text/css">
        html, body, form
        {
            width: 100%;
            height: 100%;
            background-color: #DEDEDE;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%" align="center">
        <tr>
            <td>
                <div id="indexPage" class="auto_h" style="width: 900px; margin: 0px auto; background-color: #FFFFFF">
                    <uc3:top ID="top1" runat="server" />
                    <div class=" auto_h">
                        <div class="auto_h" style="float: left; width: 245px;">
                            <div class="index_left">
                                <a href="JSDWLogin1.aspx" target="_blank" class="indexPage_left_item1"></a><a href="KCDWLogin1.aspx"
                                    class="indexPage_left_item2" target="_blank"></a><a href="JZDWLogin1.aspx" class="indexPage_left_item3"
                                        target="_blank"></a><a href="SJDWLogin1.aspx" class="indexPage_left_item4" target="_blank">
                                        </a><a href="SGTLogin1.aspx" target="_blank" class="indexPage_left_item5">
                                </a><a href="ManLogin1.aspx" class="indexPage_left_item6" target="_blank"></a><a
                                    href="RegEntUser1.aspx" class="indexPage_left_item7" target="_blank" style="display: none">
                                </a>
                            </div>
                            <div class="auto_h" style="width: 236px; margin: 0px auto; padding-top: 7px;">
                                <div class="m2_t">
                                    <tt>招标信息</tt><s><a href='http://www.scjst.gov.cn/webSite/main/newslist.aspx?fcol=254004'>更多>></a></s>
                                </div>
                                <div class="m2_m" style="height: 350px;">
                                    <ul>
                                        <asp:Literal ID="lit_05" runat="server"></asp:Literal>
                                    </ul>
                                </div>
                                <div class="m2_b">
                                    <b></b><s></s>
                                </div>
                            </div>
                        </div>
                        <div class="auto_h" style="float: left; margin-left: 2px; width: 648px;">
                            <div style="height: 256px; margin-top: 5px;">
                                <div style="width: 319px;" class="f_l auto_h">
                                    <div class="m1_t">
                                        <tt>公示公告</tt><s><a href='channel.aspx?fcol=60801'>更多>></a></s>
                                    </div>
                                    <div style="height: 219px;" class="m1_m">
                                        <ul>
                                            <asp:Literal ID="lit_01" runat="server"></asp:Literal>
                                            <%--<li><tt><b></b><a class="w200" href="#">dd</a><u>[2012-12-06]</u></tt></li>--%>
                                        </ul>
                                    </div>
                                    <div class="m1_b">
                                        <b></b><s></s>
                                    </div>
                                </div>
                                <div style="width: 319px;" class="f_l auto_h m_left7">
                                    <div class="m1_t">
                                        <tt>文件通知</tt><s><a href='channel.aspx?fcol=60803'>更多>></a></s>
                                    </div>
                                    <div style="height: 219px;" class="m1_m">
                                        <ul>
                                            <asp:Literal ID="lit_03" runat="server"></asp:Literal>
                                        </ul>
                                    </div>
                                    <div class="m1_b">
                                        <b></b><s></s>
                                    </div>
                                </div>
                            </div>
                            <div style="height: 244px;">
                                <div style="width: 319px;" class="f_l auto_h">
                                    <div class="m1_t">
                                        <tt>行业动态</tt><s><a href='channel.aspx?fcol=60802'>更多>></a></s>
                                    </div>
                                    <div style="height: 208px;" class="m1_m">
                                        <ul>
                                            <asp:Literal ID="lit_02" runat="server"></asp:Literal>
                                            <%--<li><tt><b></b><a class="w200" href="#">dd</a><u>[2012-12-06]</u></tt></li>--%>
                                        </ul>
                                    </div>
                                    <div class="m1_b">
                                        <b></b><s></s>
                                    </div>
                                </div>
                                <div style="width: 319px;" class="f_l auto_h m_left7">
                                    <div class="m1_t">
                                        <tt>工程勘察</tt><s><a href='prjList.aspx?fcol=60805' target="_blank">更多>></a></s>
                                    </div>
                                    <div style="height: 208px;" class="m1_m">
                                        <ul>
                                            <asp:Literal ID="lit_04" runat="server"></asp:Literal>
                                        </ul>
                                    </div>
                                    <div class="m1_b">
                                        <b></b><s></s>
                                    </div>
                                </div>
                            </div>
                            <div style="height: 244px;">
                                <div style="width: 319px;" class="f_l auto_h">
                                    <div class="m1_t">
                                        <tt>工程设计</tt><s><a href='prjList.aspx?fcol=60804' target="_blank">更多>></a></s>
                                    </div>
                                    <div style="height: 208px;" class="m1_m">
                                        <ul>
                                            <asp:Literal ID="lit_60404" runat="server"></asp:Literal>
                                            <%--<li><tt><b></b><a class="w200" href="#">dd</a><u>[2012-12-06]</u></tt></li>--%>
                                        </ul>
                                    </div>
                                    <div class="m1_b">
                                        <b></b><s></s>
                                    </div>
                                </div>
                                <div style="width: 319px;" class="f_l auto_h m_left7">
                                    <div class="m1_t">
                                        <tt>施工图审查</tt><s><a href='prjList.aspx?fcol=60806' target="_blank">更多>></a></s>
                                    </div>
                                    <div style="height: 208px;" class="m1_m">
                                        <ul>
                                            <asp:Literal ID="lit_60401" runat="server"></asp:Literal>
                                        </ul>
                                    </div>
                                    <div class="m1_b">
                                        <b></b><s></s>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <uc1:bottom ID="bottom1" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
