<%@ Page Language="C#" AutoEventWireup="true" CodeFile="desktop.aspx.cs" Inherits="Government_main_desktop" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server" Href="../../Skin/Blue/main.css">
    </asp:Link>
    <script src="../../script/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Chart/highcharts.js"></script>
    <script src="../Chart/highcharts-3d.js"></script>
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
        $(document).ready(function () {
            $("#span_xmsbs").click(function () {
                $("#span_xmsbs_table").show();
                $("#span_xmbls_table").hide();
                $("#span_xmsbs").removeClass("tab_d").addClass("tab_d");
                $("#span_xmbls").removeClass("tab_d")
            });

            $("#span_xmbls").click(function () {
                $("#span_xmsbs_table").hide();
                $("#span_xmbls_table").show();
                $("#span_xmsbs").removeClass("tab_d");
                $("#span_xmbls").removeClass("tab_d").addClass("tab_d");
            });
        });

        function fnClick(sFNumber) {
            var sFeatures = "status:no;dialogHeight:600px;dialogwidth:840px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";
            var idvalue = window.showModalDialog('desktopOpen.aspx?FNumber=' + sFNumber + '&rid=' + Math.random(),null, sFeatures);
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table align="center" width="98%" style="margin-top: 4px;">
            <tr>
                <td style="width: 100%; padding-right: 2px;" valign="top">
                    <div class="i_top">
                        <i></i><span>项目业务办理情况</span><b></b>
                    </div>
                    <div style="height: 100%" class="indexIMGBorder">
                        <div id="module_1" style="position: relative; padding-bottom: 10px;">
                            <table class="TableBlock" width="100%" cellspacing="0" cellpadding="1">
                                <tr class="TableData">
                                    <td style="padding: 4px; background: #FFFFFF;" valign="top">
                                        <div style="width: 100%; margin: 0 auto;">
                                            
                                            <div style='display: block; padding-left: 5px; height: 23px;'>
                                                <%if (DFId.Length == 2){ %>
                                                <span id='span_xmsbs' class="tab tab_d">项目申报数
                                                </span>
                                                <span id='span_xmbls' class="tab" style="margin-left:5px;">项目办理数
                                                </span>
                                                <%}
                                                  else{ %>
                                                <span class="tab tab_d">项目申报数
                                                </span>
                                                <%} %>
                                            </div>
                                            <div id="span_xmsbs_table">
                                                <table class="m_dg1" cellspacing="0" align="Center" rules="all" border="1" style="width: 100%; border-collapse: collapse;margin-top:0px">
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
                                                        <td style="width: 35px;"><%=(i+1).ToString()%></td>
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
                                            <%
                                                string sDisplay = "display: none;";
                                                if (DFId.Length > 2){ %>
                                             <div style='display: block; padding-left: 5px; height: 23px;'>                                                
                                                <span class="tab tab_d" style="margin-left:5px;margin-top:5px;">项目办理数
                                                </span>
                                            </div>
                                             <%
                                                    sDisplay = "";
                                             } %>
                                            <div id="span_xmbls_table" style="<%=sDisplay%>">
                                                <table class="m_dg1" cellspacing="0" align="Center" rules="all" border="1" style="width: 100%; border-collapse: collapse;margin-top:0px">
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
                                                        <td style="width: 35px;"><%=(i+1).ToString()%></td>
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
                                    <td style="width:300px;" valign="top">
                                        <div style="display: block; width: 290px; margin-top: 7px; margin-left: 3px;">
                                            <%=FName%>在建工程项目共：<font color="red"><%=ZJGZ_Row["AA"].ToString()%></font>个
                                        </div>
                                        <div style="display: block; width: 290px; border: solid 1px #79BDFE; margin-top:2px; margin-left: 2px;">
                                            <table cellspacing="0" cellpadding="0" style="margin: 5px;">
                                                <tr>
                                                    <td style="width: 37px;" valign="top">其中：</td>
                                                    <td style="line-height: 20px;">房屋建筑工程：<font color="red"><%=ZJGZ_Row["A1"].ToString()%></font>个；
                                                        总面积<font color="red"><%=ZJGZ_Row["B1"].ToString()%></font>万平方米，工程总造价：<font color="red"><%=ZJGZ_Row["C1"].ToString()%></font>万元；
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="line-height: 20px;">市政工程：<font color="red"><%=ZJGZ_Row["A2"].ToString()%></font>个，总面积<font color="red"><%=ZJGZ_Row["B2"].ToString()%></font>万平方米，工程总造价<font color="red"><%=ZJGZ_Row["C2"].ToString()%></font>万元；
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="line-height: 20px;">其它工程：<font color="red"><%=ZJGZ_Row["A3"].ToString()%></font>个，总面积<font color="red"><%=ZJGZ_Row["B3"].ToString()%></font>万平方米，工程总造价<font color="red"><%=ZJGZ_Row["C3"].ToString()%></font>万元。
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div id="container" style="display: block; width:290px;border: solid 1px #79BDFE; margin-top:5px;min-height:320px">
                                            
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                </td>
            </tr>
        </table>
        <input id="hidd_FID" type="hidden" runat="server" />
    </form>
    <script type="text/javascript">
        var oPieData = <%=sPieData%>
           
        $(function () {
            var i, iCount;
            var odata = new Array();
            iCount = oPieData.length;
            var iZS =0;
            for (i = 0; i < iCount; i++) {
                if (i == 0) {
                    odata.push({ name: oPieData[i]["name"] + "" + oPieData[i]["y"] + "个", y: parseInt(oPieData[i]["y"]), sliced: true, selected: true });
                }
                else {
                    odata.push({ name: oPieData[i]["name"] + "" + oPieData[i]["y"] + "个", y: parseInt(oPieData[i]["y"]) });
                }
                iZS += parseInt(oPieData[i]["y"]);
            }

            //if (oPieData.length > 0) {
            //    oPieData[0]["sliced"] = true;
            //    oPieData[0]["selected"] = true;
            //};

            $('#container').highcharts({
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45,
                        beta: 0
                    }
                },
                title: {
                    text: '<%=FName%>在建工程项目 <font color=red>' + iZS + "</font> 个",
                    style: {fontSize:"15px"}

                },
                tooltip: {
                    pointFormat: '{series.name}'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            format: '{point.name}',
                            distance: 5
                        },
                        showInLegend:false
                    }
                },
                colors: ["#7cb5ec", "#f7a35c", "#90ee7e", "#7798BF", "#aaeeee", "#ff0066", "#eeaaee",
		"#55BF3B", "#DF5353", "#7798BF", "#aaeeee"],
                series: [{
                    type: 'pie',
                    name: ' ',
                    data: odata
                    //    [
                    //    { name: '房屋建筑(32)', y: 32 },
                    //    { name: ' 市政(70) ', y: 70 },
                    //    {
                    //        name: '其它(50)',
                    //        y: 50
                    //    }
                    //]
                }]
            });
        });
    </script>
</body>
</html>
