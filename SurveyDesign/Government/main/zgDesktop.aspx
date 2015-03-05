<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zgDesktop.aspx.cs" Inherits="Government_main_zgDesktop" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
    <style type="text/css">
        tt {
            height:25px;
            line-height:25px;
        }
        a {
            height:22px;
            line-height:22px;
            margin-bottom:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%if (JJRows.Length > 0){%>
        <table id="tb_1" class="m_table" width="98%" align="center" style="margin-top:10px;">
            <tr>
                <td class="t_bg" style="padding-left: 26px; background-image: url(../../image/img_JJ.gif); background-position: 10px 5px; 
                      background-repeat: no-repeat;">&nbsp;待接件业务</td>
            </tr>
            <tr>
                <td valign="middle" style="height: 40px">
                    <%
                        int i,iCount;
                        iCount = JJRows.Length;
                       for(i=0;i<iCount;i++){
                    %>
                    <div class='mytable_list f_l' style='white-space: nowrap;margin-right:20px;'>
                        <tt></tt>
                        <a href="javascript:gotoPage('<%=JJRows[i]["SL"].ToString()%>','<%=JJRows[i]["MC"].ToString()%>','<%=JJRows[i]["URL"].ToString()%>')"
                            target="left"><%=JJRows[i]["MC"].ToString()%>(<font color='red'><%=JJRows[i]["SL"].ToString()%></font>)</a>
                    </div>
                    <%}%>
                </td>
            </tr>
        </table>
        <%} %>
        <%if (SCRows.Length > 0)
          {%>
        <table id="tb_1" class="m_table" width="98%" align="center" style="margin-top:10px;">
            <tr>
                <td class="t_bg" style="padding-left: 26px; background-image: url(../../image/img_JJ.gif); background-position: 10px 5px; 
                      background-repeat: no-repeat;">&nbsp;待审查业务</td>
            </tr>
            <tr>
                <td valign="middle" style="height: 40px">
                    <%
                        int i,iCount;
                        iCount = SCRows.Length;
                       for(i=0;i<iCount;i++){
                    %>
                    <div class='mytable_list f_l' style='white-space: nowrap;margin-right:20px;'>
                        <tt></tt>
                        <a href="javascript:gotoPage('<%=SCRows[i]["SL"].ToString()%>','<%=SCRows[i]["MC"].ToString()%>','<%=SCRows[i]["URL"].ToString()%>')"
                            target="left"><%=SCRows[i]["MC"].ToString()%>(<font color='red'><%=SCRows[i]["SL"].ToString()%></font>)</a>
                    </div>
                    <%}%>
                </td>
            </tr>
        </table>
        <%} %>

        <%if (SPRows.Length > 0)
          {%>
        <table id="tb_1" class="m_table" width="98%" align="center" style="margin-top:10px;">
            <tr>
                <td class="t_bg" style="padding-left: 26px; background-image: url(../../image/img_JJ.gif); background-position: 10px 5px; 
                      background-repeat: no-repeat;">&nbsp;待审批业务</td>
            </tr>
            <tr>
                <td valign="middle" style="height:40px">
                    <%
                        int i,iCount;
                        iCount = SPRows.Length;
                       for(i=0;i<iCount;i++){
                    %>
                    <div class='mytable_list f_l' style='white-space:nowrap;height:24px;'>
                        <tt></tt>
                        <a href="javascript:gotoPage('<%=SPRows[i]["SL"].ToString()%>','<%=SPRows[i]["MC"].ToString()%>','<%=SPRows[i]["URL"].ToString()%>')"
                            target="left"><%=SPRows[i]["MC"].ToString()%>(<font color='red'><%=SPRows[i]["SL"].ToString()%></font>)</a>
                    </div>
                    <%}%>
                </td>
            </tr>
        </table>
        <%} %>

        <table align="center" width="98%" style="margin-top:10px;">
            <tr>
                <td style="width: 100%; padding-right: 2px;" valign="top">
                    <div class="i_top">
                        <i></i><span>项目业务办理情况</span><b></b>
                    </div>
                    <div style="height: 100%" class="indexIMGBorder">
                        <div id="module_1" style="position: relative; padding-bottom: 10px;">
                            <table class="TableBlock" width="100%" cellspacing="0" cellpadding="1">
                                <tr class="TableData">
                                    <td colspan="2" style="padding:4px; background: #FFFFFF;" valign="top">
                                           <table class="m_dg1" cellspacing="0" align="Center" rules="all" border="1" style="width: 100%; border-collapse: collapse;margin-top:0px">
                                                    <tr class="m_dg1_h rowh">
                                                        <td style="text-decoration: none; white-space: nowrap;">序号</td>
                                                        <td>类型</td>
                                                        <td>小计</td>
                                                        <td>选址意见书</td>
                                                        <td>用地规划许可</td>
                                                        <td>工程规划许可</td>
                                                        <td>招标备案</td>
                                                        <td>项目报建</td>
                                                        <td>质量监督</td>
                                                        <td>安全监督</td>
                                                        <td>施工许可管理</td>
                                                        <td>竣工验收</td>
                                                    </tr>
                                                    <%
                                                        System.Data.DataRowCollection oRows = DT.Rows;
                                                        int jCount = oRows.Count;
                                                        for (int i = 0; i < jCount; i++)
                                                        { 
                                                    %>
                                                    <tr class="m_dg1_i row">
                                                        <td style="width: 40px;"><%=(i+1).ToString()%></td>
                                                        <td><%=oRows[i]["MC"].ToString()%></td>
                                                        <td><%=oRows[i]["AA"].ToString()%></td>
                                                        <td><%=oRows[i]["A1"].ToString()%></td>
                                                        <td><%=oRows[i]["A2"].ToString()%></td>
                                                        <td><%=oRows[i]["A3"].ToString()%></td>
                                                        <td><%=oRows[i]["A4"].ToString()%></td>
                                                        <td><%=oRows[i]["A5"].ToString()%></td>
                                                        <td><%=oRows[i]["A6"].ToString()%></td>
                                                        <td><%=oRows[i]["A7"].ToString()%></td>
                                                        <td><%=oRows[i]["A8"].ToString()%></td>
                                                        <td><%=oRows[i]["A9"].ToString()%></td>
                                                    </tr>
                                                    <%}%>
                                            </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <input id="hidd_FID" type="hidden" runat="server" />
    </form>
</body>
</html>
