<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="JNCLEnt_AppMain_Index" %>

<html/>
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
</head>
<%  Session["FIsApprove"] = 1;
    Session["FManageTypeId"] = "4001";
    var YWBM = Request.QueryString["YWBM"];
    Session["FAppId"] = YWBM;
    var param = "?YWBM=" + YWBM;
    %>
<frameset border="0">
  <frameset cols="191,*" id="trleft" border=0 frameborder=no framespacing="0">
	<frame name="left" src="aleft.aspx<%=param %>" scrolling="no" noresize>
 	<frame name="main" src="blank.aspx" scrolling="auto">
  </frameset>
</frameset>
