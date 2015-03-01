<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="jzjn_main_Article" %>

<%@ Register Src="abottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%@ Register Src="atop.ascx" TagName="top" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>四川省勘察设计科技信息系统</title>
    <link rel="stylesheet" type="text/css" href="../style/css.css" />
    <style type="text/css">
        /*文章--标题*/.Articel_Title
        {
            font-size: 22px;
            font-weight: bold;
            line-height: 40px;
            padding: 10px 0px;
            color: #000000; /*filter: Dropshadow(offx=0,offy=-1,color=#C1D2E4) Dropshadow(offx=-1,offy=-1,color=#C1D2E4);
            text-shadow: 0px -2px 1px #C1D2E4,-2px -2px 1px #C1D2E4;*/
            text-align: center;
        }
        .Articel_Title_l
        {
            font-size: 0px;
            line-height: 0px;
            height: 2px;
            margin: 0px auto;
            background: #017CC0;
            font-size: 0px;
            line-height: 0px;
        }
        .Articel_Title_l s
        {
            display: block;
            height: 2px;
            font-size: 0px;
            line-height: 0px;
            float: left;
            width: 50%;
            background: #FAB711;
            font-size: 0px;
            line-height: 0px;
        }
        /*文章--日期*/.Articel_Date
        {
            line-height: 42px;
            height: 42px;
            color: #8D8B8C;
            text-align: center;
        }
        /*文章--摘要*/.Articel_Abs
        {
            line-height: 30px;
            padding: 10px auto;
            color: #8D8B8C;
            text-align: center;
        }
        /*文章--正文*/.Acontent
        {
            padding: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="indexPage" style="width: 900px; margin: 0px auto;">
        <uc3:top ID="top1" runat="server" />
        <div style="padding: 10px; height: 100%; margin: auto; overflow: hidden;">
            <div class="Articel_Title">
                <asp:Literal ID="newstitle" runat="server"></asp:Literal>
            </div>
            <div class="Articel_Title_l">
                <s></s>
            </div>
            <div class="Articel_Date">
                &nbsp;&nbsp;&nbsp; 发布人：
                <asp:Literal ID="pubPerson" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp; 发布日期：
                <asp:Literal ID="pubTime" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp; 查看次数：
                <asp:Literal ID="Count" runat="server"></asp:Literal>
                次
            </div>
            <asp:Literal ID="zhaiyao" runat="server"></asp:Literal>
            <div class="Acontent">
                <asp:Literal ID="newsCount" runat="server"> </asp:Literal>
            </div>
        </div>
        <uc1:bottom ID="bottom1" runat="server" />
    </div>
    </form>
</body>
</html>
