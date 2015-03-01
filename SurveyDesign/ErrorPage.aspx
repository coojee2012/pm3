<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage"
    EnableViewState="false" %>

<%@ Register Assembly="Seaskyer.WebApp.Utility" Namespace="Seaskyer.WebApp.Utility.WebControls"
    TagPrefix="UPC" %>
<html>
<head>
    <title>出错啦！</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
       <meta http-equiv="x-ua-compatible" content="ie=7" />
    <style type="text/css">INPUT { FONT-SIZE: 12px }
	TD { FONT-SIZE: 12px }
	.p2 { FONT-SIZE: 12px }
	.p6 { FONT-SIZE: 12px; COLOR: #1b6ad8 }
	A { COLOR: #1b6ad8; TEXT-DECORATION: none }
	A:hover { COLOR: red }
	</style>
    <meta content="Microsoft FrontPage 5.0" name="GENERATOR">

    <script language="JavaScript">
<!-- Begin
var max=0;
function textlist() {
max=textlist.arguments.length;
for (i=0; i<max; i++)
this[i]=textlist.arguments[i];
}
tl = new textlist(
'<%=sMessage %>'
);
var x = 0; pos = 0;
var l = tl[0].length;
function textticker() 
{
    document.getElementById("ErrMessage").innerText = tl[x].substring(0, pos) + "_";
    if(pos++ == l) 
    {
        pos = 0;
        setTimeout("textticker()", 2000);
        if(++x == max) x = 0;
        l = tl[x].length;
    } 
    else
    {
        setTimeout("textticker()", 100);
    }
}
//  End -->
    </script>

</head>
<body onload="textticker();">
    <p align="center">
    </p>
    <p align="center">
    </p>
    <table cellspacing="0" cellpadding="0" width="540" align="center" border="0">
        <tbody>
            <tr>
                <td valign="top" height="270">
                    <div align="center">
                        <br>
                        <img height="211" src="bsimages/error.gif" width="329"><br>
                        <br>
                        <table cellspacing="0" cellpadding="0" width="80%" border="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <font class="p2">&nbsp;&nbsp;&nbsp; <font color="#ff0000">
                                            <img height="13" src="bsimages/emessage.gif" width="12" align="absMiddle">
                                            <span id="ErrMessage"></span></font></font>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="32">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        &nbsp;&nbsp;</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
                <tr>
                    <td align="center">
                        <center>
                            <table cellspacing="0" cellpadding="0" width="480" border="0">
                                <tbody>
                                    <tr>
                                        <td width="6">
                                            <img height="26" src="bsimages/left.gif" width="7"></td>
                                        <td background="bsimages/bg.gif">
                                            <div align="center">
                                                <font class="p6" onclick="parent.close();" style="cursor: pointer">关闭本页面</font>
                                            </div>
                                        </td>
                                        <td width="7">
                                            <img src="bsimages/right.gif"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </center>
                    </td>
                </tr>
        </tbody>
    </table>
    <p align="center">
    </p>
    <p align="center">
    </p>
</body>
</html>

<script>

</script>

