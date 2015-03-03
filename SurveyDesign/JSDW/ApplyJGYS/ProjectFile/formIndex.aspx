<%@ Page Language="C#" AutoEventWireup="true" CodeFile="formIndex.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_formIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" />
<head id="Head1" runat="server">
    <title></title>

    <script src="../../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../script/default.js"></script>

    <script type="text/javascript">

        window.onbeforeunload = function () { EntWinUnloadEvent("Auto", ""); }
    </script>

</head>
<frameset border="0">
  <frameset cols="191,*" id="trleft" border=0 frameborder=no framespacing="0">
	<frame name="left" src="formLeft.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>" scrolling="no" noresize>
 	<frame name="appMain" src="XZYJSForm.aspx?JG_Id=<%=Request.QueryString["JG_Id"] %>&audit=1" scrolling="auto">
  </frameset>
</frameset>
