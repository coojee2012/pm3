<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aIndex.aspx.cs" Inherits="WYDW_XMQK_aIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../script/default.js"></script>

    <script type="text/javascript">

        window.onbeforeunload = function () { EntWinUnloadEvent("Auto", ""); }
    </script>

</head>

<frameset rows="98,*,29" border="0">
  <frame name="top" src="atop.aspx" scrolling="no" noresize>
  <frameset cols="191,*" id="trleft" border=0 frameborder=no framespacing="0">
	<frame name="left" src="aleft.aspx" scrolling="no" noresize>
 	<frame name="appMain" src="JBXXInfo.aspx" scrolling="auto">
  </frameset>
  <frame name="bottom" src="aBottom.aspx" scrolling="no" noresize>
</frameset>