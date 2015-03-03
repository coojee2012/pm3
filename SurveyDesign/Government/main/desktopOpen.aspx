<%@ Page Language="C#" AutoEventWireup="true" CodeFile="desktopOpen.aspx.cs" Inherits="Government_main_desktopOpen" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>
    <script src="../../script/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
    <style>
         .tab {
            position: relative;
            height: 23px;
            line-height: 23px;
            background-image:url('../../image/tabd.jpg');
            width: 117px;
            font-weight: normal;
            font-size: 12px;
            float: left;
            text-align: center;
            cursor: pointer;
        }

        .tab_d {            
            font-weight: bold;
            background-image:url('../../image/tabS.jpg'); !important;
        }


        .rowh {
            height: 25px;
        }

        .row {
            height: 23px;
        }
    </style>
    <script type="text/javascript">
        function fnClick(sFNumber) {
            var sFeatures = "status:no;dialogHeight:600px;dialogwidth:840px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";
            var idvalue = window.showModalDialog('desktopOpen.aspx?FNumber=' + sFNumber + '&rid=' + Math.random(), null, sFeatures);
            return false;
        }
    </script>

</head>
<body style="margin:0px;padding:0px;">
    <form id="form1" runat="server">
        <table align="center" style="width:98%;margin-top:3px;">
            <tr>
                <td style="padding-right: 2px;" valign="top">
                    <div>
                        <div style='display: block; padding-left: 5px; height: 23px;'>
                            <span id='span_xmsbs' class="tab tab_d">项目申报数
                            </span>
                           </div>
                        <div id="span_xmsbs_table"  style='display: block;'>
                            <table class="m_dg1" cellspacing="0" align="Center" rules="all" border="1" style="width: 100%; border-collapse: collapse; margin-top: 0px">
                                <tr class="m_dg1_h  rowh">
                                    <td style="text-decoration: none; white-space: nowrap;">序号</td>
                                    <td>地区</td>
                                    <td>小计</td>
                                    <td>选址意见书</td>
                                    <td>用地规划许可</td>
                                    <td>工程规划许可</td>
                                    <td>招标备案</td>
                                    <td>项目报建</td>
                                    <td>质监备案</td>
                                    <td>安监备案</td>
                                    <td>施工许可证</td>
                                    <td>竣工验收备案</td>
                                </tr>
                                <%
                                    int i, iCount;
                                    System.Data.DataRowCollection oRows = DT.Rows;
                                    iCount = oRows.Count;
                                    for (i = 0; i < iCount; i++)
                                    { 
                                %>
                                <tr class="m_dg1_i row">
                                    <td style="width: 40px;"><%=(i+1).ToString()%></td>
                                    <td><a href="#" onclick="return fnClick('<%=oRows[i]["FUpDeptId"].ToString()%>')"><%=oRows[i]["FName"].ToString()%></a></td>
                                    <td><%=oRows[i]["AA"].ToString()%></td>
                                    <td><%=oRows[i]["A1"].ToString()%></td>
                                    <td><%=oRows[i]["B1"].ToString()%></td>
                                    <td><%=oRows[i]["C1"].ToString()%></td>
                                    <td><%=oRows[i]["D1"].ToString()%></td>
                                    <td><%=oRows[i]["E1"].ToString()%></td>
                                    <td><%=oRows[i]["F1"].ToString()%></td>
                                    <td><%=oRows[i]["G1"].ToString()%></td>
                                    <td><%=oRows[i]["H1"].ToString()%></td>
                                    <td><%=oRows[i]["I1"].ToString()%></td>
                                </tr>
                                <%}%>
                            </table>
                        </div>
                        <div style='display:block;padding-left:5px;height:23px;margin-top:5px;'>
                            <span id='span_xmbls' class="tab tab_d" style="margin-left:5px;">项目办理数
                            </span>
                        </div>
                        <div id="span_xmbls_table" style='display: block;'>
                            <table class="m_dg1" cellspacing="0" align="Center" rules="all" border="1" style="width: 100%; border-collapse: collapse; margin-top: 0px">
                                <tr class="m_dg1_h rowh">
                                    <td style="text-decoration: none; white-space: nowrap;">序号</td>
                                    <td>地区</td>
                                    <td>小计</td>
                                    <td>选址意见书</td>
                                    <td>用地规划许可</td>
                                    <td>工程规划许可</td>
                                    <td>招标备案</td>
                                    <td>项目报建</td>
                                    <td>质监备案</td>
                                    <td>安监备案</td>
                                    <td>施工许可证</td>
                                    <td>竣工验收备案</td>
                                </tr>
                                <%
                                    for (i = 0; i < iCount; i++)
                                    { 
                                %>
                                <tr class="m_dg1_i row">
                                    <td style="width: 40px;"><%=(i+1).ToString()%></td>
                                    <td><a href="#" onclick="return fnClick('<%=oRows[i]["FUpDeptId"].ToString()%>')"><%=oRows[i]["FName"].ToString()%></a></td>
                                    <td><%=oRows[i]["AB"].ToString()%></td>
                                    <td><%=oRows[i]["A2"].ToString()%></td>
                                    <td><%=oRows[i]["B2"].ToString()%></td>
                                    <td><%=oRows[i]["C2"].ToString()%></td>
                                    <td><%=oRows[i]["D2"].ToString()%></td>
                                    <td><%=oRows[i]["E2"].ToString()%></td>
                                    <td><%=oRows[i]["F2"].ToString()%></td>
                                    <td><%=oRows[i]["G2"].ToString()%></td>
                                    <td><%=oRows[i]["H2"].ToString()%></td>
                                    <td><%=oRows[i]["I2"].ToString()%></td>
                                </tr>
                                <%}%>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <input id="hidd_FID" type="hidden" runat="server" />
    </form>
</body>
</html>
