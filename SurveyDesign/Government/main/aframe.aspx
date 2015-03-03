<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aframe.aspx.cs" Inherits="Government_main_aframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset rows="98,*,29" border="0" scrolling="no" >
    <frame name="top" src="atop.aspx" scrolling="no" noresize>
    <frameset cols="0,*" id="trleft" border="0" frameborder="no" framespacing="0">
        <frame name="left" src="aLeft.aspx" scrolling="no" noresize>
        <frame name="main" src="desktop.aspx" noresize>
    </frameset>
    <frameset border="0" frameborder="no" framespacing="0">
        <frame name="bottom" src="bottom.aspx" scrolling="no" noresize>
    </frameset>
</frameset>
