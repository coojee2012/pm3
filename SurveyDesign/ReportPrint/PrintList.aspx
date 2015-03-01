<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintList.aspx.cs" Inherits="ReportPrint_PrintList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>四川省行政审批报表打印</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
            <table style="width:100%;">
            <tr style="height: 720px">
                <td>
                <IFRAME id="reporter_PbaseEntPrint" src="<%=DefaultUrl%>" frameBorder="0" hidefocus="true" 

style="height:99%;width:100%;overflow:auto;"></IFRAME></td>
                </td>
            </tr>
        </table>
</body>
</html>
