﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="WYDW_main_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>四川省建设工程项目综合监管系统--物业单位版</title>

    <script src="../../JSDW/../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../JSDW/../script/default.js"></script>

    <script type="text/javascript" src="../../JSDW/../zDialogNew/zDrag.js"></script>

    <script type="text/javascript" src="../../JSDW/../zDialogNew/zDialog.js"></script>

    <script type="text/javascript">
        var n = 0;
        function openOnLine() {
            if (n == 0) {
                var diag = new Dialog();
                diag.Modal = false;
                diag.Width = 135;
                diag.Height = 300;
                diag.Top = "50%";
                diag.Left = "100%";
                diag.CancelEvent = function () { n = 0; diag.close(); };
                diag.Title = "在线服务";
                diag.URL = "../../Common/onlineServices.aspx";
                diag.show();
                n = 1;
            }
        }
    </script>

    <script type="text/javascript">

        window.onbeforeunload = function () { EntWinUnloadEvent("Auto", ""); }
    </script>

    <style type="text/css">
        * { padding: 0; margin: 0; }
        html, body { height: 100%; border: none 0; _overflow-y: hidden; }
        iframe { width: 100%; height: 100%; border: none 0; _overflow-y: hidden; }
    </style>
</head>
<body style="overflow-y: hidden; _overflow-y: hidden;">
    <iframe src="aIndexframeset.aspx" scrolling="no" frameborder="0"></iframe>
</body>
</html>
