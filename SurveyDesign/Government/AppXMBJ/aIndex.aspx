<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aIndex.aspx.cs" Inherits="JSDW_appmain_aIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" />
<head id="Head1" runat="server">
    <title></title>

    <script src="../../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../script/default.js"></script>

    <script type="text/javascript">

        window.onbeforeunload = function() { EntWinUnloadEvent("Auto", ""); }
    </script>

</head>
<frameset rows="98,*,29" border="0">
  <frame name="top" src="atop.aspx?fbid=<%=Request.QueryString["fbid"] %>" scrolling="no" noresize>
  <frameset cols="191,*" id="trleft" border=0 frameborder=no framespacing="0">
	<frame name="left" src="aleft.aspx" scrolling="no" noresize>
 	<frame name="appMain" src="LandPlanList.aspx" scrolling="auto">
  </frameset>
  <frame name="bottom" src="bottom.aspx" scrolling="no" noresize>
</frameset>
