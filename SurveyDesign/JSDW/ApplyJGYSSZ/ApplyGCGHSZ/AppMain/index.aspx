<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="JSDW_ApplyGCGHSZ_AppMain_index" %>

<html/>
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <script src="../../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../script/default.js"></script>

    <script type="text/javascript">

        window.onbeforeunload = function () { EntWinUnloadEvent("Auto", ""); }
    </script>

</head>
<%  Session["FIsApprove"] = 1;
    string fAppId = Request.QueryString["fAppId"];
    string FIsApprove = Request.QueryString["FIsApprove"];
    string GC_Id = Request.QueryString["GC_Id"];
    var param = "?GC_Id=" + GC_Id + "&fAppId=" + fAppId;
    %>
<frameset border="0">
  <frameset cols="191,*" id="trleft" border=0 frameborder=no framespacing="0">
	<frame name="left" src="aleft.aspx<%=param %>" scrolling="no" noresize>
 	<frame name="appMain" src="blank.aspx" scrolling="auto">
  </frameset>
</frameset>
