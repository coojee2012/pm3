<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aIndexframesetOne.aspx.cs" Inherits="JSDW_main_aIndexframesetOne" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" />
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset rows="98,*,29" border="0">
  <frame name="top" src="atopOne.aspx" scrolling="no" noresize>
  <frameset cols="191,*" id="trleft" border=0 frameborder=no framespacing="0">
	<frame name="left" src="aleftOne.aspx" scrolling="no" noresize>
 	<frame name="main" src="viewOne.aspx" scrolling="auto">
  </frameset>
  <frame name="bottom" src="bottom.aspx" scrolling="no" noresize>
</frameset>
