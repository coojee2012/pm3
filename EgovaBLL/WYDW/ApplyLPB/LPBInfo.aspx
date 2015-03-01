<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LPBInfo.aspx.cs" Inherits="WYDW_ApplyLPB_LPBInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>楼盘表查看</title>
    <base target="_self" />
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <style type="text/css">
        body {
            font-family: 微软雅黑;
            font-size: 13px;
            margin-top: 6px;
            margin-left: 1px;
            margin-right: 0px;
            margin-bottom: 6px;
        }

        a:hover {
            text-decoration: underline;
            color: Brown;
        }

        .FixedTitleRow {
            position: relative;
            top: expression(this.offsetParent.scrollTop);
            z-index: 10;
            background-color: #E6ECF0;
            font-size: 13px;
            text-align: center;
            vertical-align: middle;
            height: 28px;
        }

        .FixedTitleColumn {
            position: relative;
            background-color: #E6ECF0;
            left: expression(this.parentElement.offsetParent.scrollLeft);
        }

        .FixedDataColumn {
            position: relative;
            left: expression(this.parentElement.offsetParent.parentElement.scrollLeft-1);
            background-color: #E6ECF0;
        }

        .td_div {
            width: 100px;
            height: 20px;
            margin: 0 auto;
        }

        .td_span1 {
            width: 20px;
            height: 20px;
            display: block;
            float: left;
        }

        .td_span2 {
            float: left;
            display: block;
            height: 20px;
            line-height: 20px;
            margin-left: 3px;
        }

        .lb-lc {
            border: 1px solid #cccccc;
            background-color: #f5f5f5;
            text-align: center;
            width: 50px;
            height: 30px;
            cursor: pointer;
        }

        .lb-fw {
            border: 1px solid #cccccc;
            font-size: 12px;
            text-align: center;
            vertical-align: middle;
            width: 70px;
            height: 30px;
            cursor: pointer;
        }

        .trtl {
            font-size: 13px;
            text-align: left;
            vertical-align: middle;
            height: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" align="center" class="m_title">
                <tr>
                    <th colspan="2">楼盘表查看
                    </th>
                </tr>
            </table>
            <%=strBuild%>
            <input id="hiddenType" name="hiddenType" type="hidden" value="" /><%--<%=type %>--%>
        </div>
    </form>
</body>
</html>

