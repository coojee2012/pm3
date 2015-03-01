<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prjList.aspx.cs" Inherits="Share_WebSide_prjList" %>

<%@ Register Src="abottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%@ Register Src="atop.ascx" TagName="top" TagPrefix="uc3" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>四川省勘察设计科技信息系统</title>
    <link rel="stylesheet" type="text/css" href="../style/css.css" />

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <style type="text/css">
        html, body, form { width: 100%; height: 100%; background-color: #DEDEDE; }
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
                                <a href="ManLogin.aspx" target="_blank" class="indexPage_left_item1"></a><a href="JSDWLogin.aspx"
                                    class="indexPage_left_item2" target="_blank"></a><a href="KCDWLogin.aspx" class="indexPage_left_item3"
                                        target="_blank"></a><a href="JZDWLogin.aspx" class="indexPage_left_item4" target="_blank">
                                        </a><a href="SJDWLogin.aspx" target="_blank" class="indexPage_left_item5">
                                </a><a href="SGTLogin.aspx" class="indexPage_left_item6" target="_blank"></a><a href="RegEntUser.aspx"
                                    class="indexPage_left_item7" target="_blank"></a>
                            </div>
                        </div>
                        <div class="auto_h" style="float: left; margin-left: 2px; width: 648px; padding-top: 5px;">
                            <div class="m1_t">
                                <tt>
                                    <asp:Literal ID="lit_Title" runat="server"></asp:Literal>
                                </tt><s></s>
                            </div>
                            <div style="height: 340px;" class="m1_m">
                                <ul>
                                    <asp:Repeater ID="rep_List" runat="server" 
                                        onitemdatabound="rep_List_ItemDataBound">
                                        <ItemTemplate>
                                            <li><tt><b></b>
                                                <asp:Literal ID="prjName" runat="server"></asp:Literal>
                                            <%--<a href='Article.aspx?FID=<%#Eval("FID") %>' style="width: 495px;"
                                                target="_blank">
                                                <%#Eval("FName") %></a>--%>
                                            </tt></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <div style="width: 96%; margin: 0px auto; padding-top: 10px;">
                                    <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                                        CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                                        CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                                        NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                                        pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                                        showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
                                    </webdiyer:AspNetPager>
                                </div>
                            </div>
                            <div class="m1_b">
                                <b></b><s></s>
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
