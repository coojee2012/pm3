<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Article2.aspx.cs" Inherits="Share_WebSide_Article2" %>

<%@ Register Src="abottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<%@ Register Src="atop.ascx" TagName="top" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        }
        .tab_a
        {
            width: 98%;
            line-height: 32px;
            text-align: left;
            border: 1px solid #9FD5FB;
        }
        .tab_a td
        {
            border: 1px solid #9FD5FB;
        }
        .sty1
        {
            width: 120px;
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="indexPage" style="width: 900px; margin: 0px auto;">
        <uc3:top ID="top1" runat="server" />
        <div style="padding: 10px; height: 100%; margin: auto; overflow: hidden;">
            <div class="Articel_Title">
                <asp:Literal ID="t_FPrjName" runat="server"></asp:Literal>
            </div>
            <div class="Articel_Title_l">
                <s></s>
            </div>
            <div class="Acontent">
                <table class="tab_a">
                    <tr>
                        <td class="sty1">
                            工程地址：
                        </td>
                        <td colspan="3">
                            <asp:Label ID="t_FAllAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            工程等级：
                        </td>
                        <td>
                            <asp:Label ID="t_FLevel" runat="server"></asp:Label>
                        </td>
                        <td class="sty1">
                            结构类型：
                        </td>
                        <td>
                            <asp:Label ID="t_FStruType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            建设模式：
                        </td>
                        <td>
                            <asp:Label ID="t_JSMS" runat="server"></asp:Label>
                        </td>
                        <td class="sty1">
                            工程概算：
                        </td>
                        <td>
                            <asp:Label ID="t_FAllMoney" runat="server"></asp:Label>（万元）
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            资金来源：
                        </td>
                        <td colspan="3">
                            <asp:Label ID="t_FFunds" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            建设性质：
                        </td>
                        <td>
                            <asp:Label ID="t_FKind" runat="server"></asp:Label>
                        </td>
                        <td class="sty1">
                            设计规模：
                        </td>
                        <td>
                            <asp:Label ID="t_FScale" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="sty1">
                            抗震设防分类标准：
                        </td>
                        <td  colspan="3">
                            <asp:Label ID="t_FAntiSeismic" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            地震基本烈度：
                        </td>
                        <td>
                            <asp:Label ID="t_FEarthquake" runat="server"></asp:Label>（度）
                        </td>
                        <td class="sty1">
                            抗震设防烈度：
                        </td>
                        <td>
                            <asp:Label ID="t_FIntensity" runat="server"></asp:Label>（度）
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            总用地面积：
                        </td>
                        <td>
                            <asp:Label ID="t_FLandArea" runat="server"></asp:Label>
                            (㎡)</td>
                        <td class="sty1">
                            总建筑面积：
                        </td>
                        <td>
                            <asp:Label ID="t_FArea" runat="server"></asp:Label>
                            (㎡)</td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            建设单位：
                        </td>
                        <td colspan="3">
                            <asp:Label ID="e_FName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            联系人：
                        </td>
                        <td>
                            <asp:Label ID="e_FLinkMan" runat="server"></asp:Label>
                        </td>
                        <td class="sty1">
                            联系电话：
                        </td>
                        <td>
                            <asp:Label ID="e_FTel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            勘察单位：
                        </td>
                        <td colspan="3">
                            <asp:Label ID="k_FName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            等级：
                        </td>
                        <td>
                            <asp:Label ID="k_FLevelName" runat="server"></asp:Label>
                        </td>
                        <td class="sty1">
                            证书号：
                        </td>
                        <td>
                            <asp:Label ID="k_FCertiNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sty1">
                            办理结果：
                        </td>
                        <td>
                            <asp:Label ID="Msg" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc1:bottom ID="bottom1" runat="server" />
    </div>
    </form>
</body>
</html>
