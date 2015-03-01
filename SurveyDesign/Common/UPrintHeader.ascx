<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UPrintHeader.ascx.cs"
    Inherits="Common_UPrintHeader" %>
<% if (!(Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1"))
   { 
%>
<div style="background-color: #ccccff" align="center">
    <%
        }
        else
        { 
    %>
    <div align="center" class="Noprint" style="background-color: #ccccff">
        <%
            }
        %>
        <object id="WebBrowser" height="0" width="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"
            viewastext>
        </object>
        <font size="2">如果打印不了，请把浏览器安全性设置为低。详细设置说明点击<a href='../../printProblem.doc' class="link1">这里</a></font>
        <% if (!(Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1"))
           { 
        %>
        <input class="cBtn1" onclick="document.WebBrowser.ExecWB(7,1)" type="button" value="打印预览"
            disabled="disabled" title="业务未上报，无法打印报表文件">
        <input class="cBtn1" onclick="document.all.WebBrowser.ExecWB(6,1)" type="button"
            value="打印" disabled="disabled" title="业务未上报，无法打印报表文件">
        <input class="cBtn1" onclick="document.all.WebBrowser.ExecWB(8,1)" type="button"
            value="页面设置" disabled="disabled" title="业务未上报，无法打印报表文件">
        <%
            }
            else
            { 
        %>
        <input class="cBtn1" onclick="document.WebBrowser.ExecWB(7,1)" type="button" value="打印预览">
        <input class="cBtn1" onclick="document.all.WebBrowser.ExecWB(6,1)" type="button"
            value="打印">
        <input class="cBtn1" onclick="document.all.WebBrowser.ExecWB(8,1)" type="button"
            value="页面设置">
        <%
            }
        %>
    </div>
