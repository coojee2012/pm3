<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aIndex.aspx.cs" Inherits="WYDW_Project_Main_aIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../script/default.js"></script>

    <script type="text/javascript">
        
        window.onbeforeunload = function () { EntWinUnloadEvent("Auto", ""); }
        $(document).ready(function () {
            $("#appMainDefault").attr("src","<%=mainSrc%>");
        });
    </script>
</head>
<frameset rows="98,*,29" border="0">
    <frame name="top" src="atop.aspx" scrolling="no" noresize />
    <frameset cols="191,*" id="trleft" border="0" frameborder="no" framespacing="0">
        <frame name="left" src="aleft.aspx" scrolling="no" noresize />
        <frame id="appMainDefault" name="appMain" src="../ApplyJBXX/JBXXInfo.aspx" scrolling="auto" runat="server" />
    </frameset>
    <frame name="bottom" src="abottom.aspx" scrolling="no" noresize />
</frameset>
</html>
