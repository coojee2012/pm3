<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="JSDW_ApplyYDGH_AppMain_index" %>

<html>
<head id="Head1" runat="server">
    <title></title>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
</head>
<%  Session["FIsApprove"] = 1;
    string fAppId = Request.QueryString["fAppId"];
    string FIsApprove = Request.QueryString["FIsApprove"];
    string YD_Id = Request.QueryString["YD_Id"];
    var param = "?YD_Id=" + YD_Id + "&fAppId=" + fAppId;
    %>
<frameset border="0">
  <frameset cols="191,*" id="trleft" border=0 frameborder=no framespacing="0">
	<frame name="left" src="aleft.aspx<%=param %>" scrolling="no" noresize>
 	<frame name="appMain" src="blank.aspx" scrolling="auto">
  </frameset>
</frameset>
</html>

