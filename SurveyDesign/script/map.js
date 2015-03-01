dojo.require("esri.map");
dojo.require("esri.tasks.geometry");
dojo.require("esri.toolbars.draw");
dojo.require("dojo.number");
dojo.require("dijit.layout.ContentPane");
dojo.require("dijit.layout.BorderContainer");
dojo.require("esri.dijit.Scalebar");
dojo.require("dijit.Toolbar");
dojo.require("esri.tasks.find");
dojo.require("esri.tasks.query");
dojo.require("esri.toolbars.navigation");
dojo.require("dijit.form.Button");
dojo.require("esri.tasks.identify");
dojo.require("esri.layers.FeatureLayer");
dojo.require("dijit.TooltipDialog");
dojo.require("esri.dijit.Geocoder");

/* 全局变量声明 */
// 地图对象
var Map = null;
// 绘制工具
var DrawTool = null;
// GeometryServer实例
var GeoService = null;
// 工具栏实例
var Toolbar = null;
// WMTS层数组
var WMTSLayers = null;

var BaseMapLayer = null;
//动画绘制层
//var FeatureLayer = null;
//添加临时绘制层
var LabelLayer = null;
// 路径层
var RouteGraphicLayer = null;
//路径途经点绘制层
var RouteCourseLayer = null;
//添加自定义绘制层
var CustomLayer = null;
// 临时层
var TempGraphicLayer = null;
//动画绘制层
var CartoonLayer = null;


// 全图范围
var MapFullExtent = null;
// 比例尺条实例
var Scalebar = null;
// 赢眼实例
var MapOverview = null;
// Html文档信息
var HtmlBody = new Object();
HtmlBody.TopHeight = 89;
HtmlBody.LeftWidth = 262;
HtmlBody.CopyrightHeight = 30;
HtmlBody.ResultFrameTopHeight = 247;
// 改变大小时间
var ResizeTimer = null;
// 左侧工具栏是否显示
var LeftColumnShow = true;
// 当前地图模式编号
var CurMapModeID = "shiliang";
// 当前右侧查询窗体编号
var CurLeftQueryFrameID = 1;
// 注记单击句柄
var LabelClkHandler = null;
// 提示事件
var TipsEvent = null;
// 绘图句柄
var DrawEndHandler = null;
// 开始绘图
var DistanceStart = false;
// 测量事件句柄
var DistanceOnClickEvent = null;
// 自定义层
var CustomLayer = null;
// 右顶窗口显示
var LeftTopFrameVisible = true;

var NowClkMapPoint = null;

var AutoDistanceTimer = null;

// 自动控制图层显示控件
var AutoControlMapLayerTimer = null;

var MapClickEvent = null;
// 用户自定义标注
var UserLabels = new Array();
// 用户标注计数器
var UserLabelCount = 1;
// 图层控件可见
var LayerControlVisible = false;

var OldMapModeID = "shiliang";
// 原来的地图按钮
var OldMapBtn = null;

/**********路径相关变量********/
var RouteHandler = null;
// 鼠标选择点类型 1：起始 2：终止
var RouteMouseSelectType = 0;
// 路径查询任务
var RouteTask = null;
// 路径参数
var RouteParams = null;
var RouteSymbol = null;
// 路径结果
var RouteResult = null;
var DirectionFeatures = null;
//路径节点
//var RoutePoint = null;
//起始节点
var StopStart = null;
//终止节点
var StopEnd = null;
//中途点
var StopMore = null;
// 拖拽不可用
var PathDragDisable = false;
//// 路径点集合
var RoutePoints = new Array();
// 当前站点编号
var CurRouteStopID = null;

// 缓冲可拽
var BufferDragDisable = false;
// 自动控制地图层
var AutoControlMapLayer = true;



//// 增高值
var ToBigTopFrameAddHeight = 141;
var TopFrameIsBig = false;
var BtnTopFrameIsBig = false;

// 自动隐藏的层
var AutoHiddenMapLayers = new Array();

// 地图上选择点
var PathMapSelectPointDiv = null;
var PathMapSelectPointId = null;
var PathMapSelectPointEvt = null;
//// 是否登陆
//var IsLogin = false;
var Layer项目选址 = null;
var Layer用地规划 = null;
var Layer工程规划 = null;
var Layer道路交通 = null;
var Layer城市照明 = null;
var Layer污水处理 = null;
var Layer集中供热 = null;
var Layer城市燃气 = null;
var Layer城市供水 = null;
var Layer园林绿化 = null;
var Layer环境卫生 = null;
var Layer市容整治 = null;

//cad显示
var CadLayer, vis = [];


var MapMouseMoveEvt = null;
var onMapLoaded = function() { }; //加载事件
var onAfterLabeling = function(evt) { }; //标记后的事件
var onFinded = function(rExtent) { };
var onLabelClick = function(FID) { }; //标注点击事件
var onDrawLine = function(graphic) { };
var onDrawArea = function(graphic) { };
var SatelliteLayer = null; //卫星图
// 地图初始化
function Initialize() {
    // 显示加载条
    ShowSysLoading(true);
    esri.config.defaults.io.proxyUrl = "/arcgisserver/apis/javascript/proxy/proxy.ashx";
    esri.config.defaults.io.alwaysUseProxy = false;
    //自定义比例
    var lods = [{ "level": 0, "resolution": 3307.29828126323, "scale": 12500000 },
                        { "level": 1, "resolution": 2116.67090000847, "scale": 8000000 },
                        { "level": 2, "resolution": 1058.33545000423, "scale": 4000000 },
                        { "level": 3, "resolution": 529.167725002117, "scale": 2000000 },
                        { "level": 4, "resolution": 264.583862501058, "scale": 1000000 },
                        { "level": 5, "resolution": 132.291931250529, "scale": 500000 },
                        { "level": 6, "resolution": 66.1459656252646, "scale": 250000 },
                        { "level": 7, "resolution": 33.0729828126323, "scale": 125000 },
                        { "level": 8, "resolution": 16.9333672000677, "scale": 64000 },
                        { "level": 9, "resolution": 8.46668360003387, "scale": 32000 },
                        { "level": 10, "resolution": 4.23334180001693, "scale": 16000 },
                        { "level": 11, "resolution": 2.11667090000847, "scale": 8000 },
                        { "level": 12, "resolution": 1.05833545000423, "scale": 4000 },
                        { "level": 12, "resolution": 0.529167725002117, "scale": 2000 },
                        { "level": 12, "resolution": 0.264583862501058, "scale": 1000}];

    Map = new esri.Map("map", { logo: false, nav: false, slider: true, sliderStyle: "large", "lods": lods, isZoomSlider: true, loaded: true, zoom: 0 });

//    var layer = new esri.layers.ArcGISTiledMapServiceLayer(LayerTiledMap);

//    Map.addLayer(layer);


    BaseMapLayer = new esri.layers.ArcGISDynamicMapServiceLayer(BaseMapUrl);
    Map.addLayer(BaseMapLayer, 0);

    //加载cad图
    CadLayer = new esri.layers.ArcGISDynamicMapServiceLayer(CadLayerUrl);

    SatelliteLayer = new esri.layers.ArcGISDynamicMapServiceLayer(SatelliteLayerUrl);

    //设置要显示的图层
    //    if (CadLayer.loaded) {
    //        showLayer(CadLayer);
    //    } else {
    //        dojo.connect(CadLayer, "onLoad", showLayer);
    //    }

    GeoService = new esri.tasks.GeometryService(GeoServiceUrl);




    Toolbar = new esri.toolbars.Navigation(Map);
    Toolbar.zoomSymbol.color = new dojo.Color([0, 99, 198, 0.3]);
    Toolbar.zoomSymbol.outline.color = new dojo.Color([0, 99, 198, 0.8]);
    Toolbar.zoomSymbol.outline.width = 1;

    dojo.connect(Map, "onExtentChange", onMapExtentChange);
    // 实例绘图实例
    DrawTool = new esri.toolbars.Draw(Map);

    dojo.connect(Map, "onUpdateStart", function() { closeDialog(); Map.graphics.disableMouseEvents(); });
    dojo.connect(Map, "onUpdateEnd", function() { Map.graphics.enableMouseEvents(); });

    // create the geocoder
    //    geocoder = new esri.dijit.Geocoder({
    //        map: map
    //    }, "search");
    //    geocoder.startup();


    if (Map.loaded) {
        OnMapLoad();
    }
    else {
        // 地图加载

        dojo.connect(Map, "onLoad", OnMapLoad);
    }

}
function OnMapLoad() {
    //默认范围(全省)
    var ext = new esri.geometry.Extent(-1680814.4140199404, 4245576.038494736, 708096.2228127741, 5923967.586620765, new esri.SpatialReference({ wkid: 2438 }));
    Map.setExtent(ext);

    // 比例尺条
    Scalebar = new esri.dijit.Scalebar({ map: Map, scalebarUnit: 'metric' });

    // 实例绘图实例
    DrawTool = new esri.toolbars.Draw(Map);
    LabelClkHandler = dojo.connect(Map.graphics, "onClick", function(evt) {

        onLabelClick(evt.graphic.FID);
        //    var geometry = new esri.geometry.Point([evt.mapPoint.x, evt.mapPoint.y], 4326);

        //     alert("" + geometry.x + "," + geometry.y);

        dojo.disconnect(mouseDragEvent);
    });
    //    dojo.connect(dojo.byId(Map._slider.id), "onmouseover", OnZoomSliderMouseOver);
    //    dojo.connect(dojo.byId(Map._slider.id), "onmouseout", OnZoomSliderMouseOut);
    onMapLoaded();
}
// 设置地图动作
function SetMapAction(id) {
    try {
        if (id != "full") {

            // 根据当前状态清除
            switch (Map.ActionName) {
                case "dobuffer":
                    MapClear();
                    break;
                case "lengthdistance":
                    MapClear();
                    break;
                case "areadistance":
                    MapClear();
                    break;
            }

            // 设置不可编辑
            // SetMapActionToEdit(false);
            // 复原工具栏
            Toolbar.deactivate();
            // 复原绘制栏
            DrawTool.deactivate();
            // 释放事件
            DisposeMapClickEvent();

            //            if (id != "dobuffer") {
            //                // 隐藏设置半径
            //                ShowRadiiLayer(false);
            //                // 清除设置缓存
            //                SetBuffer(false);
            //                // 清除缓存句柄
            //                dojo.disconnect(BufferHandler);
            //            }
            //            // 清除绘制句柄
            //            dojo.disconnect(DrawEndHandler);
            //
            //            // 清除路径鼠标选择
            //            SwitchRMS(false);
        }
        switch (id) {
            case "full":
                Map.setExtent(MapFullExtent, true);
                //map.centerAndZoom(mapPoint, levelOrFactor);
                //Toolbar.zoomToFullExtent();
                break;
            case "zoomin":
                Toolbar.activate(esri.toolbars.Navigation.ZOOM_IN);
                SetMapCursor("images/cursor/ZoomIn.cur");
                Map.ActionName = "zoomin";
                break;
            case "zoomout":
                Toolbar.activate(esri.toolbars.Navigation.ZOOM_OUT);
                SetMapCursor("images/cursor/ZoomOut.cur");
                Map.ActionName = "zoomout";
                break;
            case "pan":
                //Toolbar.activate(esri.toolbars.Navigation.PAN);
                Map.enablePan();
                SetMapCursor("images/cursor/Pan.cur");
                Map.ActionName = "pan";
                break;
            case "toprevextent":
                Toolbar.zoomToPrevExtent();
                break;
            case "tonextextent":
                Toolbar.zoomToNextExtent();
                break;
            case "deactivate":
                MapClear();
                Toolbar.deactivate();
                SetMapCursor("images/cursor/Pan.cur");
                break;
            case "lengthdistance":
                MapClear();
                LengthDistance();
                SetMapCursor("images/cursor/MeasureDistance.cur");
                Map.ActionName = "lengthdistance";
                break;
            case "DrawLine":
                MapClear();
                LengthDistance();
                SetMapCursor("images/cursor/MeasureDistance.cur");
                Map.ActionName = "lengthdistance";
                break;
            case "areadistance":
                MapClear();
                AreasAndLengthsDistance();
                SetMapCursor("images/cursor/MeasureArea.cur");
                Map.ActionName = "areadistance";
                break;
            case "label":
                MapClear();
                SetMapCursor("images/cursor/PointQuery.cur");
                Map.ActionName = "label";
                break;
            case "dobuffer":
                // ShowRadiiLayer(true);
                // SetBuffer(true);
                // SetMapCursor("images/cursor/CircleQuery.cur");
                // Map.ActionName = "dobuffer";
                break;
            case "edit":
                if (!IsLogin) {
                    ShowLoginFrame(true);
                    return;
                }

                SetMapActionToEdit(true);
                SetMapCursor("images/cursor/PointQuery.cur");
                Map.ActionName = "edit";

                MapMouseMoveEvt = dojo.connect(Map, "onMouseMove", OnMapMouseMove);
                break;
            case "custom":
                // 自定义模式
                Map.disablePan();
                break;
        }
    }
    catch (e) { }
}

// 地图清除
function MapClear() {
    try {
        Map.graphics.clear();
        Map.infoWindow.hide();
        // 清空标注
        if (RouteGraphicLayer != null) {
            RouteGraphicLayer.clear();
        }
        if (RouteCourseLayer != null) {
            RouteCourseLayer.clear();
        }
        if (CustomLayer != null) {
            CustomLayer.clear();
        }
        if (TempGraphicLayer != null) {
            TempGraphicLayer.clear();
        }
        if (CartoonLayer != null) {
            CartoonLayer.clear();
        }
        // 清空变量
        //        SqlQueryClear();
        //        // 路径清空
        //        PathQueryClear();
        //        // 显示热图
        //        ShowHotPhotos(true);
    }
    catch (e) { }
}
// 控制区域识别延时
function onMapExtentChange(extent) {
    try {
        //        if (AreaShowTimer != null) {
        //            clearInterval(AreaShowTimer);
        //        }
        //        AreaShowTimer = setInterval("MapExtentChanged()", 500);
        //alert(Scalebar.domNode.innerHTML);
        // Scalebar.domNode.innerHTML = Scalebar.domNode.innerHTML.replace("km", "公里");

    }
    catch (e) { }
}

// 绘制完成
function OnLengthDistanceClk(evt) {

    if (!DistanceStart) {
        DistanceStart = true;
        Map.graphics.clear();
    }

}
var StrLength = "";
// 显示长度测量结果
function ShowLengthDistanceResult(result) {
    try {
        var length = parseFloat(result.lengths[0]);
        length = length / 1000;
        var html = dojo.number.format(length) + " 公里";
        StrLength = html;
    }
    catch (e) { alert(e.message); }
}
// 设置地图鼠标指针样式
function SetMapCursor(curUrl) {
    try {
        Map.setMapCursor("url(" + curUrl + "),auto");
    }
    catch (e) { }
}
// 释放地图单击事件
function DisposeMapClickEvent() {
    try {
        if (MapClickEvent != null) {
            dojo.disconnect(MapClickEvent);
        }
    }
    catch (e) { }
}



//比例尺提示条
function ShowMapSlider(bool) {
    try {
        var sti = dojo.byId("mssilderLayer");
        if (bool) {
            sti.style.display = "block";
        }
        else {
            sti.style.display = "none";
        }
    }
    catch (e) { }
}
function OnMsZoomTo(level) {
    try {

        //Map.centerAndZoom(Map.extent.getCenter(), level);
        Map.setLevel(level);
    }
    catch (e) { }
}

function OnZoomSliderMouseOver() {
    try {


        var mDiv = dojo.byId(Map.id);
        var msDiv = dojo.byId(Map._slider.id);
        var msHeight = msDiv.offsetHeight;
        var msLeft = msDiv.offsetLeft + mDiv.offsetLeft + 15;

        var add = 50;
        var jieTopJ = 33;
        if (msHeight < 201) {
            add = 25;
            jieTopJ = 24;
        }
        var top = msHeight - 25;
        var bgTop = msDiv.offsetTop + mDiv.offsetTop;

        // 背景
        var bgImg = dojo.byId("msbg");
        bgImg.style.left = msLeft + "px";
        bgImg.style.top = bgTop + "px";

        var qqTop = top - 3;



        var div = dojo.byId("mssheng");
        div.style.left = msLeft + "px";
        div.style.top = top + "px";


        top = top - add;
        div = dojo.byId("msshi");
        div.style.left = msLeft + "px";
        div.style.top = top + "px";


        top = top - add;
        div = dojo.byId("msxian");
        div.style.left = msLeft + "px";
        div.style.top = top + "px";


        top = top - add;
        div = dojo.byId("msjie");
        div.style.left = msLeft + "px";
        div.style.top = top + "px";

        ShowMapSlider(true);
        event.cancelBubble = true;
    }
    catch (e) { }
}
function OnZoomSliderMouseOut() {
    try {
        ShowMapSlider(false);
        event.cancelBubble = true;
    }
    catch (e) { }
}

// 显示系统加载页面
function ShowSysLoading(v) {
    try {
        var l = dojo.byId("sysLoadingLayer");
        if (v) {
            var ico = dojo.byId("sysloadIco");
            var bodyHeight = document.body.offsetHeight;
            var bodyWidth = document.body.offsetWidth;
            var x = bodyWidth / 2 - 16;
            var y = bodyHeight / 2 - 16;
            if (bodyWidth < 1) bodyWidth = 1;
            if (bodyHeight < 1) bodyHeight = 1;
            l.style.width = bodyWidth + "px";
            l.style.height = bodyHeight + "px";
            if (x < 1) x = 1;
            if (y < 1) y = 1;
            ico.style.top = parseInt(y) + "px";
            ico.style.left = parseInt(x) + "px";
            l.style.display = "block";
            setTimeout("ShowSysLoading(false)", 5000);
        }
        else {
            l.style.display = "none";
        }
    }
    catch (e) { }
}

//infoWindow中保存按钮的事件
function finash(index) {
    //获取输入的名字
    var titleStr = dojo.byId('nameText').value;
    //获取当前所画的图形
    var cgraphic = Map.graphics.graphics[index];
    //设置图形的属性，id、title
    cgraphic.attributes = { id: index, title: titleStr };
    tempGraphic = cgraphic;
    dojo.byId('TextBox1').value = dojo.toJson(cgraphic.toJson());

    //对面图形进行geometryService的simplify操作
    //    GeoService.simplify([cgraphic], getLabelPoints);
    //    //获取当前所画图形的json字符串用来保存
    //    var graphicStr = dojo.toJson(cgraphic.toJson());
    //    //设置ajax请求的参数
    //    var params = { graphic: graphicStr }
    //    //用dojo的xhrGet的ajax方法把图形的json字符串提交到服务端保存
    //    dojo.xhrGet({ url: "saveG.aspx", handleAs: "text", preventCache: true, content: params, load: dojo.hitch(this, "saveEnd") });
    //隐藏infoWindow
    Map.infoWindow.hide();
}

function LoadDrawArea(str) {

    var graphic = new esri.Graphic(eval('(' + str + ')'));
    Map.graphics.add(graphic);
    //  dojo.xhrGet({ url: "getG.aspx", handleAs: "json", preventCache: true, load: dojo.hitch(this, "getEnd") });
}
//读取ml文件里自定义图形完成后添加到map进行显示
function getEnd(graphics) {
    for (var i = 0; i < graphics.Graphic.length; i++) {
        var g = graphics.Graphic[i];
        var graphic = new esri.Graphic(g);
        Map.graphics.add(graphic);
    }
}
// 输出面积
function OutputAreaAndLength(result) {
    try {
        // 测量后距离
        //var area = dojo.number.format(result.areas[0]) + " 平公里";
        var unit = "平方米"
        var area = Math.abs(result.areas[0]);
        if (area > 1000000) {
            area = area / 1000000;
            unit = "平方公里";
        }
        //var area = dojo.number.format(result.areas[0] / 1000000);
        //var length=dojo.number.format(result.lengths[0]/1000) + " 公里";
        var l = dojo.byId("myTipsLayer");
        var html = "<div class='distancetxt'>面积：&nbsp;<span class='distancenum'>" + dojo.number.format(area) + "</span>&nbsp;" + unit + "<br><span class='distancetips'>单击确定地点，双击结束</span></div>"; //<br>边长：&nbsp;<span class='distancenum'>"+length+ "</span>&nbsp;公里
        l.innerHTML = html;
    }
    catch (e) { alert(e.message); }
}
// 清除绘制完成句柄
function ClearDrawEndHandler() {
    try {
        if (DrawEndHandler != null) {
            dojo.disconnect(DrawEndHandler);
        }
    }
    catch (e) { }
}


// 面积量算
function AreasAndLengthsDistance() {
    try {

        ClearDrawEndHandler();
        var lengthParams = new esri.tasks.LengthsParameters();
        // 绘制完毕
        DrawEndHandler = dojo.connect(DrawTool, "onDrawEnd", function(geometry) {
            DistanceStart = false;
            var symbol = new esri.symbol.SimpleFillSymbol();
            symbol.color = new dojo.Color([0, 118, 178, 0.35]);
            symbol.outline.color = new dojo.Color([0, 118, 178, 1]);
            symbol.outline.width = 1;
            var graphic = Map.graphics.add(new esri.Graphic(geometry, symbol));
            onDrawArea(graphic);
        });
        DrawTool.activate(esri.toolbars.Draw.POLYGON, { showTooltips: false });
        DrawTool.fillSymbol.color = new dojo.Color([70, 165, 247, 0.35]);
        DrawTool.fillSymbol.outline.color = new dojo.Color([70, 165, 247, 1]);
        DrawTool.fillSymbol.outline.width = 1;
        DistanceOnClickEvent = dojo.connect(Map, "onClick", OnAreaAndLengthDistanceClk);
    }
    catch (e) { }
}

//根据输入的关键字进行findTask操作
function SetMapExtent(searchText) {
    //实例化FindTask
    RouteTask = new esri.tasks.FindTask("http://192.168.0.11/arcgis/rest/services/NMG/MapServer/2");
    //FindTask的参数
    RouteParams = new esri.tasks.FindParameters();
    //返回Geometry
    RouteParams.returnGeometry = true;
    //查询的图层id
    RouteParams.layerIds = [0, 1, 2, 3];
    //查询字段
    //RouteParams.searchFields = ["BJECTID", "AREANAME", "CAPITAL", "ST", "AREA"];
    RouteParams.searchText = searchText;
    // RouteTask.execute(RouteParams, showResults);
    SearchXQByName(searchText);
}
function SearchXQByName(sName) {
    try {

        queryTask = new esri.tasks.QueryTask(areaVillageUrl);
        var query = new esri.tasks.Query();
        query.geometryType = "esriGeometryPolygon";

        query.where = "NAME='" + sName + "'";
        query.rid = Math.random();
        query.returnGeometry = true;

        queryTask.execute(query, showResults);
    }
    catch (e) { }
}
function SearchXQByCityId(CityId) {

    queryTask = new esri.tasks.QueryTask(areaVillageUrl);
    var query = new esri.tasks.Query();
    query.geometryType = "esriGeometryPolygon";

    query.where = "FNUMBER like '" + CityId + "%'";
    query.rid = Math.random();
    query.returnGeometry = true;

    queryTask.execute(query, showResults);

}

function executeQueryTask(geometry) {
    //onClick event returns the evt point where the user clicked on the map.
    //This is contains the mapPoint (esri.geometry.point) and the screenPoint (pixel xy where the user clicked).
    //set query geometry = to evt.mapPoint Geometry
    var query = new esri.tasks.Query();
    query.geometryType = "esriGeometryPoint";

    queryTask = new esri.tasks.QueryTask(areaVillageUrl);
    query.geometry = geometry;
    query.returnGeometry = true;

    //Execute task and call showResults on completion
    queryTask.execute(query, GetPointAear);
}

function GetPointAear(featureSet) {
    //remove all graphics on the maps graphics layer

    //QueryTask returns a featureSet.  Loop through features in the featureSet and add them to the map.
    var rExtent = new esri.geometry.Extent();
    for (var i = 0; i < featureSet.features.length; i++) {
        var graphic = featureSet.features[i];
        //设置查询到的graphic的显示样式
        // graphic.setSymbol(symbol);

        //把查询到的结果添加map.graphics中进行显示
        //Map.graphics.add(graphic);
        //获取查询到的所有geometry的Extent用来设置查询后的地图显示
        if (i == 0) {
            rExtent = graphic.geometry.getExtent();
        }
        else {
            rExtent = rExtent.union(graphic.geometry.getExtent());
        }

    }

    Map.setExtent(rExtent);
    onFinded(rExtent);
}
//显示findTask的结果
function showResults(results) {
    //清楚上一次的高亮显示
    Map.graphics.clear();
    //高亮样式
    highlightSymbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 2), new dojo.Color([125, 125, 125, 0.35]));
    //查询结果样式
    symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([0, 0, 255, 0.35]), 1), new dojo.Color([125, 125, 125, 0.35]));
    var rExtent = null;

    //遍历查询结果
    for (var i = 0; i < results.features.length; i++) {
        var graphic = results.features[i];
        //设置查询到的graphic的显示样式
        try {
            //把查询到的结果添加map.graphics中进行显示
            // Map.graphics.add(graphic);
            //获取查询到的所有geometry的Extent用来设置查询后的地图显示
            if (i == 0) {
                rExtent = graphic.geometry.getExtent();
            }
            else {
                rExtent = rExtent.union(graphic.geometry.getExtent());
            }
        } catch (ex) { }
    }
    //设置地图视图范围
    if (rExtent != null) {
        Map.setExtent(rExtent);
    }
    onFinded(rExtent);

}

// 距离量算
function LengthDistance() {



    //ClearDrawEndHandler();
    var color = new dojo.Color([255, 0, 0, 0.4]);
    var width = 2.8;
    var lengthParams = new esri.tasks.LengthsParameters();
    // 绘制完毕
    DrawEndHandler = dojo.connect(DrawTool, "onDrawEnd", function(geometry) {
        DistanceStart = false;


        // 清除
        //Map.graphics.clear();
        var symbol = new esri.symbol.SimpleLineSymbol();
        symbol.color = new dojo.Color([255, 128, 0, 0.48]);
        symbol.width = width;
        var graphic = new esri.Graphic(geometry, symbol);
        Map.graphics.add(graphic);
        onDrawLine(graphic);
        //结束之后要弹出是否保存
    });
    DrawTool.activate(esri.toolbars.Draw.POLYLINE, { showTooltips: false });
    DrawTool.lineSymbol.color = color;
    DrawTool.lineSymbol.width = width;
    DistanceOnClickEvent = dojo.connect(Map, "onClick", OnLengthDistanceClk);

}



// 缓冲查询
function DoLabel(evt) {
    try {
        //        var info = new Object();
        //        info.Title = "临时标注";
        //        info.Content = GetLabelForm(evt.mapPoint.x, evt.mapPoint.y);
        //        var size = new Object();
        //        size.Width = 276;
        //        size.Height = 168;
        //        ShowMapInfoWindow(evt.mapPoint, size, info)
        addGraphic(evt.mapPoint);

    }
    catch (e) { alert(e.message); }
}
function addGraphic(geometry, isClear) {
    MapClear();
    // var geometry = new esri.geometry.Point([x, y], 4490);
    var symbol = new esri.symbol.PictureMarkerSymbol(FileServerUrl + '/image/markRed.png', 40, 40).setOffset(0, 16);

    dojo.connect(Map.graphics, "onClick", graphicsOnMouseDown);
    //dojo.connect(Map.graphics, "onMouseUp", graphicsOnMouseUp);
    //            //dojo.connect(map.graphics, "onMouseUp", graphicsOnMouseUp);
    var graphic = new esri.Graphic(geometry, symbol)
    Map.graphics.add(graphic);
    // alert("" + geometry.x + "," + geometry.y);
    onAfterLabeling(geometry)
    return graphic;
}
function addGraphicNotClear(geometry, FID) {

    var symbol = new esri.symbol.PictureMarkerSymbol(FileServerUrl + '/image/flag.png', 24, 24).setOffset(8, 10);
    var graphic = new esri.Graphic(geometry, symbol)

    graphic.FID = FID;

    Map.graphics.add(graphic);

}

// 显示地图信息窗口
function ShowMapInfoWindow(location, size, info) {
    try {
        Map.infoWindow.setTitle(info.Title);
        Map.infoWindow.setContent(info.Content);
        Map.centerAt(location);
        Map.infoWindow.resize(size.Width, size.Height);
        Map.infoWindow.show(location, esri.dijit.InfoWindow.ANCHOR_UPPERRIGHT);
    }
    catch (e) { }
}



var oldLoc;
var mouseDragEvent = null;
function graphicsOnMouseDown(evt) {
    // Map.disableMapNavigation();

    if (mouseDragEvent != null) {
        dojo.disconnect(mouseDragEvent);
    }
    oldLoc = evt.mapPoint;
    mouseDragEvent = dojo.connect(Map.graphics, "onMouseDrag", graphicsOnMouseDrag);
}
function graphicsOnMouseDrag(evt) {
    if (mouseDragEvent != null) {
        var moveLoc = evt.mapPoint;

        var geoPt = esri.geometry.webMercatorToGeographic(evt.mapPoint);
        moveLoc.setSpatialReference(map.spatialReference);

        evt.graphic.setGeometry(moveLoc);
        onAfterLabeling(moveLoc);
        //                map.infoWindow.move(moveLoc);
        //                map.infoWindow.setContent("lat/lon : " + geoPt.y.toFixed(2) + ", " + geoPt.x.toFixed(2) +
        //    "<br />Project x/y : " + evt.mapPoint.x + ", " + evt.mapPoint.y);
        //                map.infoWindow.show(map.toScreen(moveLoc), map.getInfoWindowAnchor(map.toScreen(moveLoc)));
    }
}

function graphicsOnMouseUp(evt) {
    if (mouseDragEvent != null) {
        dojo.disconnect(mouseDragEvent);
        Map.enableMapNavigation();

    } var moveLoc = evt.mapPoint;
    moveLoc.setSpatialReference(Map.spatialReference);
    evt.graphic.setGeometry(moveLoc);

}
var popudialog;
function FeatureLayerHover(layerId) {


    popudialog = new dijit.TooltipDialog({
        id: "tooltipDialog",
        style: "position: absolute; width: auto; font: normal normal normal 10pt Helvetica;z-index:100"
    });
    popudialog.startup(); // 开启
    TempGraphicLayer = new esri.layers.GraphicsLayer({ id: "tempGraphicLayer", opacity: 0, visible: true });

    //Map.infoWindow.resize(245, 125); // 设置信息框的大小
    //    AddHoverLayer(layerId, "POLYGON");
    //    AddHoverLayer(layerId, "POLYLINE");
    AddHoverLayer1(layerId, "POLYGON");
    Map.graphics.enableMouseEvents(); // 开启鼠标事件
    dojo.connect(Map.graphics, "onMouseOut", closeDialog);
    Map.addLayer(TempGraphicLayer);
    dojo.connect(TempGraphicLayer, "onMouseOver", HoverLayerOnMouseOver);
    //listen for when the onMouseOver event fires on the countiesGraphicsLayer
    //when fired, create a new graphic with the geometry from the event.graphic and add it to the maps graphics layer

}
 
function AddHoverLayer1(layerId, layerName) {
    if (layerId && layerId.length <= 0) {
        return;
    }
    var Layer = CadLayer;
    var layerInfo = Layer.layerInfos[layerId];
    var subId = layerId[0];
    for (var i = 0; i < layerInfo.subLayerIds.length; i++) {
        var info = Layer.layerInfos[layerInfo.subLayerIds[i]];
        if (info.name == layerName) {
            subId = info.id;
        }
    }
    queryTask = new esri.tasks.QueryTask(CadLayerUrl + "/" + subId);
    var query = new esri.tasks.Query();
    query.geometryType = "esriGeometryPolygon";

    query.where = " TITLE<>'' ";
    query.rid = Math.random();
    query.returnGeometry = true;
    queryTask.execute(query, HoverLayerResults);
}
function HoverLayerResults(results) {

    //遍历查询结果
    for (var i = 0; i < results.features.length; i++) {
        try {
            var graphic = results.features[i];
            //设置查询到的graphic的显示样式

            //把查询到的结果添加map.graphics中进行显示
            //FeatureLayer.add(graphic);
            var symbol = new esri.symbol.SimpleFillSymbol(
			esri.symbol.SimpleFillSymbol.STYLE_SOLID,
			new esri.symbol.SimpleLineSymbol(	//线型符号
				esri.symbol.SimpleLineSymbol.STYLE_SOLID, 	//样式，STYLE_DASH，STYLE_DASHDOT，STYLE_DASHDOTDOT，STYLE_DOT，STYLE_NULL，STYLE_NULL
				new dojo.Color([255, 255, 255, 0]), // 颜色
				1	// 像素
			),
			new dojo.Color([125, 125, 125, 0])
		);
            graphic.setSymbol(symbol);
            TempGraphicLayer.add(graphic);
        }
        catch (ex)
        { }

    }
}
function AddHoverLayer(layerId, layerName) {

    if (layerId && layerId.length <= 0) {
        return;
    }
    var Layer = CadLayer;
    var layerInfo = Layer.layerInfos[layerId];
    var subId = layerId[0];
    for (var i = 0; i < layerInfo.subLayerIds.length; i++) {
        var info = Layer.layerInfos[layerInfo.subLayerIds[i]];
        if (info.name == layerName) {
            subId = info.id;
        }
    }
    //  定义一个功能层
    var featureLayer = new esri.layers.FeatureLayer(CadLayerUrl + "/" + subId, {
        mode: esri.layers.FeatureLayer.MODE_SNAPSHOT, // 此模式下,该feature layer检索所有有关的层的资源,并作为推那个显示在客户端,一旦被添加到地图上, 会触发onUpdateEnd事件
        outFields: ["AUTOCAD_LA", "TITLE"]	// 显示的字段, "TITLE"
    });
    if (layerName == "POLYGON") {
        //featureLayer.outFields = ["AUTOCAD_LA","TITLE"];
        featureLayer.setDefinitionExpression(" TITLE<>'' ");
    }
    else {
        featureLayer.setDefinitionExpression(" 1=1 "); // 只有匹配的才会被显示，这里是 ”TITLE<>''Tilte有值的“
    }
    // 定义一个填充符号
    var symbol = new esri.symbol.SimpleFillSymbol(
			esri.symbol.SimpleFillSymbol.STYLE_SOLID,
			new esri.symbol.SimpleLineSymbol(	//线型符号
				esri.symbol.SimpleLineSymbol.STYLE_SOLID, 	//样式，STYLE_DASH，STYLE_DASHDOT，STYLE_DASHDOTDOT，STYLE_DOT，STYLE_NULL，STYLE_NULL
				new dojo.Color([255, 255, 255, 0]), // 颜色
				1	// 像素
			),
			new dojo.Color([125, 125, 125, 0])
		);
    featureLayer.setRenderer(new esri.renderer.SimpleRenderer(symbol)); // 设置功能层的渲染
    Map.addLayer(featureLayer); // 添加到地图
    dojo.connect(featureLayer, "onMouseOver", HoverLayerOnMouseOver);
}
var CurrentHoverLayerGraphic = null;
function HoverLayerOnMouseOver(evt) {
 
    if (CurrentHoverLayerGraphic && evt.graphic == CurrentHoverLayerGraphic) {
        return;
    }
    CurrentHoverLayerGraphic = evt.graphic;
    var symbolred = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 3);

    // 高亮线
    var highlightSymbol = new esri.symbol.SimpleFillSymbol(
			esri.symbol.SimpleFillSymbol.STYLE_SOLID,
			new esri.symbol.SimpleLineSymbol(
				esri.symbol.SimpleLineSymbol.STYLE_SOLID,
				new dojo.Color([255, 0, 0]),
				3
			),
			new dojo.Color([125, 125, 125, 0.35])
		);
    Map.graphics.clear();
    var t = "${TITLE}";

    var highlightGraphic = new esri.Graphic(
		  		evt.graphic.geometry
		  );
    if (highlightGraphic.geometry.type == "polygon") {



        highlightGraphic.setSymbol(highlightSymbol);
    }
    else if (highlightGraphic.geometry.type == "polyline") {
        highlightGraphic.setSymbol(symbolred);
    }
    Map.graphics.add(highlightGraphic); // 将图标加入图形中
    var content = esri.substitute(evt.graphic.attributes, t); // dojo.string.substitute(),可以处理通配符形式
    if (content == "") {
        t = "${AUTOCAD_LA}";
        content = esri.substitute(evt.graphic.attributes, t);
    }
    popudialog.setContent(content); // 设置弹窗内容

    dojo.style(popudialog.domNode, "opacity", 0.85); // 透明度
    dijit.popup.open({ popup: popudialog, x: evt.pageX, y: evt.pageY }); // 弹出位置
}
 
function HoverLayerOnClick(graphic,evt) {

    if (CurrentHoverLayerGraphic && graphic == CurrentHoverLayerGraphic) {
        return;
    }
    CurrentHoverLayerGraphic = graphic;
    var symbolred = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 3);

    // 高亮线
    var highlightSymbol = new esri.symbol.SimpleFillSymbol(
			esri.symbol.SimpleFillSymbol.STYLE_SOLID,
			new esri.symbol.SimpleLineSymbol(
				esri.symbol.SimpleLineSymbol.STYLE_SOLID,
				new dojo.Color([255, 0, 0]),
				3
			),
			new dojo.Color([125, 125, 125, 0.35])
		);
    Map.graphics.clear();
    var t = "${TITLE}";

    var highlightGraphic = new esri.Graphic(
		  		graphic.geometry
		  );
    if (highlightGraphic.geometry.type == "polygon") {



        highlightGraphic.setSymbol(highlightSymbol);
    }
    else if (highlightGraphic.geometry.type == "polyline") {
        highlightGraphic.setSymbol(symbolred);
    }
    Map.graphics.add(highlightGraphic); // 将图标加入图形中
    var content = esri.substitute(graphic.attributes, t); // dojo.string.substitute(),可以处理通配符形式
    if (content == "") {
        t = "${AUTOCAD_LA}";
        content = esri.substitute(graphic.attributes, t);
    }
    popudialog.setContent(content); // 设置弹窗内容

    dojo.style(popudialog.domNode, "opacity", 0.85); // 透明度
    dijit.popup.open({ popup: popudialog, x: evt.pageX, y: evt.pageY }); // 弹出位置
}
function closeDialog() {
    Map.graphics.clear(); // 清除图形
    dijit.popup.close(popudialog); // 关闭弹出框
}

//隐藏所有项目标记
function HideProjectPoint() {

    if (Layer项目选址 != null) {
        Layer项目选址.hide();
    }
    if (Layer用地规划 != null) {
        Layer用地规划.hide();
    }
    if (Layer工程规划 != null) {
        Layer工程规划.hide();
    }
    if (Layer道路交通 != null) {
        Layer道路交通.hide();
    }
    if (Layer城市照明 != null) {
        Layer城市照明.hide();
    }
    if (Layer污水处理 != null) {
        Layer污水处理.hide();
    }
    if (Layer集中供热 != null) {
        Layer集中供热.hide();
    }
    if (Layer城市燃气 != null) {
        Layer城市燃气.hide();
    }
    if (Layer城市供水 != null) {
        Layer城市供水.hide();
    }
    if (Layer园林绿化 != null) {
        Layer园林绿化.hide();
    }
    if (Layer环境卫生 != null) {
        Layer环境卫生.hide();
    }
    if (Layer市容整治 != null) {
        Layer市容整治.hide();
    }

    if (Layer占用挖掘道路 != null) {
        Layer占用挖掘道路.hide();
    }
    if (Layer停车场分布 != null) {
        Layer停车场分布.hide();
    }
    if (Layer汽车场站分布 != null) {
        Layer汽车场站分布.hide();
    }
    if (Layer污水处理厂分布 != null) {
        Layer污水处理厂分布.hide();
    }
    if (Layer热电厂分布 != null) {
        Layer热电厂分布.hide();
    }
    if (Layer区域锅炉房分布 != null) {
        Layer区域锅炉房分布.hide();
    }
    if (Layer燃气供应企业分布 != null) {
        Layer燃气供应企业分布.hide();
    }
    if (Layer水源地分布 != null) {
        Layer水源地分布.hide();
    }
    if (Layer水厂分布 != null) {
        Layer水厂分布.hide();
    }
    if (Layer垃圾处理厂分布 != null) {
        Layer垃圾处理厂分布.hide();
    }
    if (Layer垃圾中转站分布 != null) {
        Layer垃圾中转站分布.hide();
    }
    if (Layer城市公园分布 != null) {
        Layer城市公园分布.hide();
    }
    if (Layer景区景点分布 != null) {
        Layer景区景点分布.hide();
    }
    if (Layer景观大道分布 != null) {
        Layer水厂分布.hide();
    }
    if (Layer古树名木分布 != null) {
        Layer古树名木分布.hide();
    }
    if (Layer绿化示范点分布 != null) {
        Layer绿化示范点分布.hide();
    }

}
//加载规划项目标记
function LoadProjectPoint() {
    dojo.xhrGet({ url: "../../GMap/main/getPrjG.aspx", handleAs: "json", preventCache: true, load: dojo.hitch(this, "OnProjectPointLoaded") });
}

function OnProjectPointLoaded(result) {

    Layer项目选址 = new esri.layers.GraphicsLayer();

    Layer用地规划 = new esri.layers.GraphicsLayer();

    Layer工程规划 = new esri.layers.GraphicsLayer();

    for (var i = 0; i < result.length; i++) {
        var geometry = Map.extent.getCenter();
        geometry.x = result[i].FLon;
        geometry.y = result[i].FLat;
        var strPic = "";
        var tempLayer = null;

        if (result[i].FType == "1") {
            strPic = '../../image/markBlue.png';
            tempLayer = Layer项目选址;
        }
        else if (result[i].FType == "2") {
            strPic = '../../image/markGreen.png';
            tempLayer = Layer用地规划;
        }
        else if (result[i].FType == "3") {
            strPic = '../../image/markRed.png';
            tempLayer = Layer工程规划;
        }
        if (tempLayer != null) {
            var attr = { "title": result[i].FPrjName };
            var symbol = new esri.symbol.PictureMarkerSymbol(strPic, 20, 20).setOffset(0, 10);
            var graphic = new esri.Graphic(geometry, symbol, attr);
            graphic.FID = result[i].FID;
            graphic.FPrjName = result[i].FPrjName;


            graphic.onClick = function(gra) {
                showWin("a", "../../GMap/main/PrjInfo.aspx?fid=" + gra.FID, gra.FPrjName, 500, 300, "50%", "70%");
            };
            tempLayer.add(graphic);
            tempLayer.hide();
            tempLayer.onClick = function(evt) {
                if (evt.graphic.onClick != null) {
                    evt.graphic.onClick(evt.graphic);
                }
            }

        }
    }
    Map.addLayer(Layer项目选址);
    Map.addLayer(Layer用地规划);
    Map.addLayer(Layer工程规划);
}
//加载城建项目
function LoadCSJS_Build() {

    dojo.xhrGet({ url: "../../GMap/main/getBuild.aspx", handleAs: "json", preventCache: true, load: dojo.hitch(this, "onCSJS_BuildLoaded") });
}
var Layer占用挖掘道路 = null;
var Layer停车场分布 = null;
var Layer汽车场站分布 = null;
var Layer污水处理厂分布 = null;
var Layer热电厂分布 = null;
var Layer区域锅炉房分布 = null;
var Layer燃气供应企业分布 = null;
var Layer水源地分布 = null;
var Layer水厂分布 = null;
var Layer垃圾处理厂分布 = null;
var Layer垃圾中转站分布 = null;
var Layer城市公园分布 = null;
var Layer景区景点分布 = null;
var Layer景观大道分布 = null;
var Layer古树名木分布 = null;
var Layer绿化示范点分布 = null;
var LayerLoadcount = 0;
function onCSJS_BuildLoaded(result) {
    Layer道路交通 = new esri.layers.GraphicsLayer();
    Layer城市照明 = new esri.layers.GraphicsLayer();
    Layer污水处理 = new esri.layers.GraphicsLayer();
    Layer集中供热 = new esri.layers.GraphicsLayer();
    Layer城市燃气 = new esri.layers.GraphicsLayer();
    Layer城市供水 = new esri.layers.GraphicsLayer();
    Layer园林绿化 = new esri.layers.GraphicsLayer();
    Layer环境卫生 = new esri.layers.GraphicsLayer();
    Layer市容整治 = new esri.layers.GraphicsLayer();


    Layer占用挖掘道路 = new esri.layers.GraphicsLayer();
    Layer停车场分布 = new esri.layers.GraphicsLayer();
    Layer汽车场站分布 = new esri.layers.GraphicsLayer();
    Layer污水处理厂分布 = new esri.layers.GraphicsLayer();
    Layer热电厂分布 = new esri.layers.GraphicsLayer();
    Layer区域锅炉房分布 = new esri.layers.GraphicsLayer();
    Layer燃气供应企业分布 = new esri.layers.GraphicsLayer();
    Layer水源地分布 = new esri.layers.GraphicsLayer();
    Layer水厂分布 = new esri.layers.GraphicsLayer();
    Layer垃圾处理厂分布 = new esri.layers.GraphicsLayer();
    Layer垃圾中转站分布 = new esri.layers.GraphicsLayer();
    Layer城市公园分布 = new esri.layers.GraphicsLayer();
    Layer景区景点分布 = new esri.layers.GraphicsLayer();
    Layer景观大道分布 = new esri.layers.GraphicsLayer();
    Layer古树名木分布 = new esri.layers.GraphicsLayer();
    Layer绿化示范点分布 = new esri.layers.GraphicsLayer();
    var font = new esri.symbol.Font("20px", esri.symbol.Font.STYLE_NORMAL, esri.symbol.Font.VARIANT_NORMAL, esri.symbol.Font.WEIGHT_BOLDER);
    for (var i = 0; i < result.length; i++) {
        var geometry = Map.extent.getCenter();
        geometry.x = result[i].FLon;
        geometry.y = result[i].FLat;
        var strPic = "";
        var tempLayer = null;
        var linkurl = "";
        if (result[i].FType == "112") {
            strPic = '../../image/05.gif';
            tempLayer = Layer道路交通;
            linkurl = "../../Government/CityBuild/RoadTrafficBuildEdit.aspx?FType=112&IsView=0"
        }
        else if (result[i].FType == "12") {
            strPic = '../../image/MapDig.png';
            tempLayer = Layer占用挖掘道路;
            linkurl = "../../Government/CityBuild/RoadWajueEdit.aspx?FType=12&IsView=0"
        }
        else if (result[i].FType == "132") {
            strPic = '../../image/MapStop.png';
            tempLayer = Layer停车场分布;
            linkurl = "../../Government/CityBuild/ParkFileEdit.aspx?FType=132&IsView=0"
        }
        else if (result[i].FType == "162") {
            strPic = '../../image/MapStop2.png';
            tempLayer = Layer汽车场站分布;
            linkurl = "../../Government/CityBuild/RoadTrfBusFileEdit.aspx?FType=162&IsView=0"
        }
        else if (result[i].FType == "23") {
            strPic = '../../image/06.gif';
            tempLayer = Layer城市照明;
        }

        else if (result[i].FType == "32") {
            strPic = '../../image/07.gif';
            tempLayer = Layer污水处理厂分布;
            linkurl = "../../Government/CitySlop/slopFileEdit.aspx?FType=32&IsView=0"
        }
        else if (result[i].FType == "42") {
            strPic = '../../image/08.gif';
            tempLayer = Layer集中供热;
        }
        else if (result[i].FType == "521" || result[i].FType == "522" || result[i].FType == "523") {
            strPic = '../../image/09.gif';
            tempLayer = Layer城市燃气;
        }
        else if (result[i].FType == "62" || result[i].FType == "64") {
            strPic = '../../image/12.gif';

            tempLayer = Layer水源地分布;

        }
        else if (result[i].FType == "64") {
            strPic = '../../image/12.gif';

            tempLayer = Layer水厂分布;

        }
        else if (result[i].FType == "64") {
            strPic = '../../image/12.gif';

            tempLayer = Layer水厂分布;

        }
        else if (result[i].FType == "72") {
            strPic = '../../image/11.gif';
            tempLayer = Layer城市公园分布;
        }
        else if (result[i].FType == "73") {
            strPic = '../../image/11.gif';
            tempLayer = Layer景区景点分布;
        }
        else if (result[i].FType == "74") {
            strPic = '../../image/11.gif';
            tempLayer = Layer景观大道分布;
        }
        else if (result[i].FType == "75" || result[i].FType == "76") {
            strPic = '../../image/11.gif';
            tempLayer = Layer古树名木分布;
        }
        else if (result[i].FType == "76") {
            strPic = '../../image/11.gif';
            tempLayer = Layer绿化示范点分布;
        }
        else if (result[i].FType == "82" || result[i].FType == "83") {
            strPic = '../../image/6.gif';
            tempLayer = Layer垃圾处理厂分布;
        }
        else if (result[i].FType == "83") {
            strPic = '../../image/6.gif';
            tempLayer = Layer垃圾中转站分布;
        }
        else if (result[i].FType == "91") {
            strPic = '../../image/6.gif';
            tempLayer = Layer市容整治;
        }
        if (tempLayer == null) {
            tempLayer = new esri.layers.GraphicsLayer();

        }
        var symbol = new esri.symbol.PictureMarkerSymbol(strPic, 20, 20);
        var graphic = new esri.Graphic(geometry, symbol);
        graphic.linkurl = linkurl;
        graphic.FID = result[i].FID;
        symbol.title = "a";
        //        var textSymbol = new esri.symbol.TextSymbol(result[i].FName
        //      ,
        //      font, new dojo.Color([0, 0, 0])
        //    );
        //        textSymbol.setAlign(esri.symbol.TextSymbol.ALIGN_START);
        //        textSymbol.xoffset = textSymbol.xoffset + 10;
        //        var labelPointGraphic = new esri.Graphic(geometry, textSymbol);
        //        tempLayer.add(labelPointGraphic);

        graphic.onClick = function() {
            showWin("a", this.linkurl + "&fid=" + this.FID, "城建项目", 500, 300, "50%", "70%");
        };
        tempLayer.add(graphic); tempLayer.hide();
        tempLayer.onClick = function(evt) {

            if (evt.graphic.onClick != null) {
                evt.graphic.onClick();
            }
        }

        tempLayer.add(graphic);
        if (i + 1 == result.length) {
            Map.addLayer(tempLayer);
        }
        else {
            if (result[i].FType != result[i + 1].FType) {
                Map.addLayer(tempLayer);
            }
        }
    }

    //    if (Layer污水处理) {
    //        Layer污水处理厂分布 = Layer污水处理;
    //        Map.addLayer(Layer污水处理厂分布);
    //    }
    //geometryService.labelPoints(Map.graphics.graphics, showLabel);

}

function getGraphicForFID(FID) {

    for (var i = 0; i < Map.graphics.graphics.length; i++) {
        var gra = Map.graphics.graphics[i];
        if (gra.FID && gra == FID) {
            return gra;
        }
    }


    return null;
}
//切换普通地图和卫星图
var IsMap2d = true;
function Mapctrls2d() {
    var span显示普通地图 = document.getElementById("span显示普通地图");
    var span显示卫星地图 = document.getElementById("span显示卫星地图");
    if (span显示普通地图 && span显示卫星地图) {

        if (IsMap2d) {
            span显示卫星地图.style.display = "";
            span显示普通地图.style.display = "none";
            IsMap2d = false;
            Map.addLayer(SatelliteLayer, 1);
        }
        else {
            Map.removeLayer(SatelliteLayer);
            span显示卫星地图.style.display = "none";
            span显示普通地图.style.display = "";
            IsMap2d = true;

        }
    }
}
//=========CAD图层显示专用=====star========//
//从名称得到图层ID
function getIDforName(Layer, name) {
    var n = "";
    if (Layer.layerInfos && Layer.layerInfos.length > 0) {
        for (var i = 0; i < Layer.layerInfos.length; i++) {
            var info = Layer.layerInfos[i];
            if (info.name == name) {
                n = info.id;
            }
        }
    }
    return n;
}
//显示cad图层，专题图子页面树调用。
function showCadMap(fList) {
    var hidd_CADLAYER = document.getElementById("hidd_CADLAYER");
    if (hidd_CADLAYER) {
        hidd_CADLAYER.value = fList;
    }
    //加载cad图
    Map.removeLayer(CadLayer);
    //设置要显示的图层
    if (CadLayer.loaded) {
        showCadMapLayer(CadLayer);
    } else {
        dojo.connect(CadLayer, "onLoad", showCadMapLayer);
    }

}
function showCadMapLayer(CadLayer) {
    var hidd_CADLAYER = document.getElementById("hidd_CADLAYER");
    if (hidd_CADLAYER) {
        fList = hidd_CADLAYER.value;
    }
    var v = fList.split(",");
    vis = [];
    for (var i = 0; i < v.length; i++) {
        var id = getIDforName(CadLayer, v[i]);
        if (id)
            vis.push(id);
    }

    CadLayer.setVisibleLayers(vis);
    Map.addLayer(CadLayer);
    return vis;
}
//=========CAD图层显示专用====end=========//
// 加载初始化
dojo.addOnLoad(Initialize);