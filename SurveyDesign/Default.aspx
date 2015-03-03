<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
     <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        #form1 label.errorMsg 
			{ 
			color:Red; 
			font-size:13px; 
			margin-left:5px;
			}
         .m_btn_w2 {
             height: 21px;
         }
    </style>
     <script type="text/javascript" src="script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="script/default.js"></script>
    <script type="text/javascript" src="DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="script/messages_zh.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txtFZRQ" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox>
    </div>
    </form>
</body>
</html>
