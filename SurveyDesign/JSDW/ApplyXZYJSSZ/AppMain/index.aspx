<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="JSDW_ApplyXZYJSSZ_AppMain_index" %>


<html/>
<head id="Head1" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title></title>

</head>
<%  Session["FIsApprove"] = 1;
    string fAppId = Request.QueryString["fAppId"];
    string YJS_GUID = Request.QueryString["YJS_GUID"];
    var param = "?YJS_GUID=" + YJS_GUID + "&fAppId=" + fAppId;
    %>
<frameset border="0">
  <frameset cols="191,*" id="trleft" border=0 frameborder=no framespacing="0">
	<frame name="left" src="aleft.aspx<%=param %>" scrolling="no" noresize>
 	<frame name="appMain" src="blank.aspx" scrolling="auto">
  </frameset>
</frameset>
</html>
