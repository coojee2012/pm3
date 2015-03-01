<%@ Control Language="C#" AutoEventWireup="true" CodeFile="t2.ascx.cs" Inherits="Government_maintable_t2" %>

<script type="text/javascript">
    $(document).ready(function() {
        show("p", "Pie3D.swf");
    });
    function show(first, url) {
        showChart("1", first + "1", url); //企业总数
        showChart("2", first + "2", url); //新办企业
        showChart("3", first + "3", url); //有资质无档案企业
        showChart("4", first + "4", url); //有档案企业
    }
    function showChart(type, controlid, url) {
        var SystemID = '<%=Request.QueryString["sysId"]%>'; //企业类型
        var DeptNumber = '<%=DeptNumber %>'; //地区

        var chart = new FusionCharts("../chart/" + url, "ChartId", 620, 400, "0", "0");
        var url = "../statis/UserEntDeptXml.aspx?type=" + type + "&SystemID=" + SystemID + "&DeptNumber=" + DeptNumber + "&e=" + Math.random();
        chart.setDataURL(escape(url));
        chart.render(controlid);
    }   
</script>

<div id="module_2" style="position: relative; padding-bottom: 10px;">
    <table class="TableBlock" width="100%" cellspacing="0" cellpadding="1">
        <tr class="TableHeader">
            <td id="module_2_head" width="50%">
                &nbsp;<asp:Literal ID="lit_Title" runat="server"></asp:Literal>
                企业按地区统计
            </td>
            <td id="module_2_more" align="right">
                <div id="module_2_op" style="float: right; display: none;">
                    <a href="../Bulletin/UpBulletin.aspx"><span class="mytableLink">全部</span></a>&nbsp;&nbsp;<a
                        id="module_2_url" href="#" onclick="_del('2');"><img src="../image/x.gif" alt="不显示该模块"
                            border="0" align="absbottom"></a></div>
            </td>
        </tr>
        <tr class="TableData">
            <td align="center" colspan="2">
                <div id="p1">
                </div>
                <div id="p2">
                </div>
                <div id="p3">
                </div>
                <div id="p4">
                </div>
                <div id="p5">
                </div>
            </td>
        </tr>
    </table>
</div>
