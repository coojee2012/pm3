<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XmForm1.aspx.cs" Inherits="NJSWebApp.PageXm.XmForm1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=XMMC%></title>
    <link href="../css/XmCommand.css" rel="stylesheet" type="text/css" />
    <link href="../css/form.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function window.onload(){
             var sformid = '<%=Request["formid"]%>';
             if (sformid == "") sformid = "cd0dab3f-95b1-4323-a14a-5b81d203cd2f";
             if (sformid == "cd0dab3f-95b1-4323-a14a-5b81d203cd2f"){
                 var otxtSJDW = document.all["txtSJDW"];
                 var sText = "";
                 if (otxtSJDW){  //设计单位
                     sText = otxtSJDW.value;
                     document.all['labsb'].style.cursor = "hand";
                     document.all['labsb'].onclick = function(){
                         window.location.href = "../../EntCredit/EntInfo.aspx?Fid=4E050CB1-E42C-4F2A-8B18-06E9FBE0144B";
                     }
       
                     document.all['labjl'].style.cursor = "hand";
                     document.all['labjl'].onclick = function(){
                         window.location.href = "../../EntCredit/EntInfo.aspx?Fid=AC4A1F7F-7741-417C-87C4-388A93C34400";
                     }
                     document.all['labsj'].style.cursor = "hand";
                     document.all['labsj'].onclick = function(){
                         window.location.href = "../../EntCredit/EntInfo.aspx?Fid=FFF0A3C4-1E27-4220-9793-3857BF1D8032";
                     }
                 }   
             }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 1000px;" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td class="location"><span style="font-weight: bold;">您的位置:</span><%=sPath%></td>
            </tr>
            <tr>
                <td valign="top" style="background-color: #ffffff">
                    <table style="width: 1000px;" border="0" cellpaddileng="0" cellspacing="0" align="center">
                        <tr>
                            <td style="padding-left: 3px; padding-top: 3px;" valign="top">
                                <div style="position: relative">
                                    <%=formhtml%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
