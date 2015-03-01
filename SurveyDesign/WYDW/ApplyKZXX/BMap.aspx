<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BMap.aspx.cs" Inherits="WYDW_Project_BMap" %>

<!DOCTYPE html>

<html>
<head>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <style type="text/css">
        body, html, #allmap {
            width: 100%;
            height: 100%;
            overflow: hidden;
            margin: 0;
            font-family: "微软雅黑";
        }
    </style>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=IuQydlVvHK41wZY7piKkjTqW"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/library/MarkerTool/1.2/src/MarkerTool_min.js"></script>
    <title></title>
</head>
<body onload="pageready()">
    <form runat="server">
        <div runat="server" align="center" id="hh">
            <input type="hidden" value="" id="Pointx" />
            <input type="hidden" value="" id="Pointy" />
            <input type="hidden" value="" id="Px" />
            <input type="hidden" value="" id="Py" />
        </div>
        <table width="100%" align="center" class="m_title">
            <tr>
                <td class="t_r">
                    <input type="button" runat="server" value="标记位置" id="OpenM" onclick="OpenMarkers()" class="m_btn_w4" />
                    <input type="button" runat="server" value="清除标记" id="ClearM" onclick="ReOpen()" class="m_btn_w4" />
                </td>
            </tr>
        </table>
    </form>
    <div style='width: 100%; height: 95%' id="allmap"></div>
</body>
</html>
<script type="text/javascript">
    // 百度地图API功能
    var map = new BMap.Map("allmap");
    var point = new BMap.Point(116.331398, 39.897445);
    map.centerAndZoom(point, 12);
    map.enableScrollWheelZoom(true);
    var temp = 0;
    function ReOpen() {
        map.clearOverlays();
        document.getElementById("OpenM").disabled = false;
        document.getElementById("Pointx").value = "";
        document.getElementById("Pointy").value = "";
        window.parent.clearmapvalue();
    }
    function GetData() {
        map.addEventListener("click", shwoInfo);
    }
    function shwoInfo(e) {
        document.getElementById("Pointx").value = e.point.lng;
        document.getElementById("Pointy").value = e.point.lat;
        window.parent.setmapvalue(e.point.lng, e.point.lat);
        map.removeEventListener("click", shwoInfo);
    }
    function closeClick() { }
    function OpenMarkers() {
        document.getElementById("OpenM").disabled = true;
        var mkrTool = new BMapLib.MarkerTool(map, { autoClose: true, followText: "请添加标记" });
        mkrTool.open();
        GetData();
    }
    function myFun(result) {
        var cityName = result.name;
        map.setCenter(cityName);
    }
    

    function theLocation(mapx, mapy) {

        if (mapx != "" && mapy != "") {
            document.getElementById("Px").value = mapx;
            document.getElementById("Py").value = mapy;
            map.clearOverlays();
            var new_point = new BMap.Point(mapx, mapy);
            var marker = new BMap.Marker(new_point); // 创建标注
            map.addOverlay(marker); // 将标注添加到地图中
            map.panTo(new_point);
            try {
                document.getElementById("OpenM").disabled = true;
            } catch (e) {

            }
        } else {
            var myCity = new BMap.LocalCity();
            myCity.get(myFun);
        }
    }

    function pageready() {
        window.parent.markmap();
    }
</script>
