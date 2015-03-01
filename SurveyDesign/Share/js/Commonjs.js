//本脚本需要引用jQuery脚本库
//window.document.write("<script type='text/jscript' src='/Foundation/LzProduct/Commonjs/jquery-1.4.1.min.js'></script>");
function LoadJQuery() {
    var head = document.getElementsByTagName('head')[0];
    var scripts = document.getElementsByTagName('script');
    var bool = false;
    for (var i = 0; i < scripts.length; i++) {
        var src = scripts[i].getAttribute('src');
        if (src == '../js/jquery-1.4.1.min.js') {
            bool = true;
            break;
        }
    }

    if (!bool) {
        var script = document.createElement('script');
        script.setAttribute('type', 'text/javascript');
        head.appendChild(script);
        script.src = "../js/jquery-1.4.1.min.js";

    }
}
LoadJQuery();

var TempObj = {};
/********************
函数名称：Trim
函数功能：去除字符串两边的空格
函数参数：str,需要处理的字符串
********************/
function Trim(str) {
    return str.replace(/(^s*)|(s*$)/g, "");
}
var Url = {

    // public method for url encoding
    encode: function (string) {
        if (string != null) {
            return escape(this._utf8_encode(string));
        }
        else {
            return string;
        }
    },

    // public method for url decoding
    decode: function (string) {
        if (string != null) {
            return this._utf8_decode(unescape(string));
        }
        else {
            return null;
        }
    },

    // private method for UTF-8 encoding
    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }

        return utftext;
    },

    // private method for UTF-8 decoding
    _utf8_decode: function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;

        while (i < utftext.length) {

            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            }
            else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }

        }

        return string;
    }

}

//统一参数对象
function LzParameter(ParentParameterObj) {
    //---------------------------属性------------------------------------
    //this.ParentParameterObj   父参数对象
    //this.Values               参数值数组对象
    //this.disValues               被屏蔽的参数值数组对象
    //this.Id                   参数对象Id
    //this.UrlParms             Url QueryString参数对象集合
    //this.IsRoot               是否是根参数对象

    //---------------------------构造函数----------------------------------
    this.Values = new Object();
    this.disValues = new Object();
    if (ParentParameterObj)
        this.ParentParameterObj = ParentParameterObj;


}
LzParameter.prototype = {
    //Values:{},
    //设置值方法
    Set: function (key, value) {
        if (value == undefined || value == null) {
            this.disValues[key] = {};
        }
        else if (this.disValues.hasOwnProperty(key)) {
            delete this.disValues[key];
        }
        for (var keytemp in this.Values) {
            if (keytemp != null && key != null && keytemp.toLowerCase() == key.toLowerCase()) {
                delete this.Values[keytemp];
            }
        }
        this.Values[key] = value;

    },
    Get: function (key) {
        //判断是否是有opener窗口
        //if (this.ParentParameterObj == undefined) {
        //    if (this.IsRoot == undefined) {
        //        if (window.opener) {
        //            try {
        //                if (window.opener.LzParameterObj) {
        //                    this.Values = window.opener.LzParameterObj.CloneAllKeyValue(this.Values);
        //                    for (var keytemp in this.UrlParms) {
        //                        if (keytemp != null) {
        //                            this.Values[keytemp] = this.UrlParms[keytemp];
        //                        }
        //                    }
        //                }
        //            }
        //            catch (e) { }
        //        }
        //        this.IsRoot = true;
        //    }
        //}
        //屏蔽指定参数
        if (this.disValues.hasOwnProperty(key)) {
            return null;
        }
        //从自身取值
        if (this.Values.hasOwnProperty(key)) {
            return this.Values[key];
        }
        //如果参数对象是页面的根参数对象
        if (this.IsPageRoot) {
            //从QueryString中取值
            if (this.UrlParms == undefined) {
                this.UrlParms = this.GetUrlParms();
            }
            if (this.UrlParms && this.UrlParms.hasOwnProperty(key)) {

                this.Set(key, this.UrlParms[key]);
                return this.UrlParms[key];
            }
        }
        //从父对象中取值
        if (this.ParentParameterObj) {
            var returnvalue = this.ParentParameterObj.Get(key);
            if (returnvalue != null)
                return returnvalue;
        }
        //从QueryString中取值
        if (this.UrlParms && this.UrlParms.hasOwnProperty(key)) {
            this.Set(key, this.UrlParms[key]);
            return this.UrlParms[key];
        }
        //从parent中取值

        return null;
    },
    //得到所有KeyValue的副本
    //    CloneAllKeyValue: function (ValuesObj) {
    //        var NewValues = {};
    //        if (ValuesObj != undefined) {
    //            NewValues = ValuesObj;
    //        }
    //        for (var key in this.Values) {
    //            if (!NewValues.hasOwnProperty(key)) {
    //                if (!this.disValues.hasOwnProperty(key)) {
    //                    NewValues[key] = this.Values[key];
    //                }
    //            }
    //        }
    //        //从QueryString中取值
    //        if (this.UrlParms == undefined && this.IsPageRoot) {
    //            this.UrlParms = this.GetUrlParms();
    //        }
    //        for (var key in this.UrlParms) {
    //            if (!NewValues.hasOwnProperty(key)) {
    //                if (!this.disValues.hasOwnProperty(key)) {
    //                    NewValues[key] = this.UrlParms[key];
    //                }
    //            }
    //        }
    //        if (this.ParentParameterObj) {
    //           return this.ParentParameterObj.CloneAllKeyValue(NewValues);
    //           
    //        }
    //        return NewValues;
    //    },
    CloneAllKeyValue: function (ValuesObj) {

        var NewValues = {};
        var PValue = {};
        if (this.ParentParameterObj) {
            PValue = this.ParentParameterObj.CloneAllKeyValue(null);
        }
        for (var key in this.Values) {
            if (!this.disValues.hasOwnProperty(key)) {
                if (key != "") {
                    NewValues[key] = this.Values[key];
                }
            }
            else {
                if (NewValues.hasOwnProperty(key)) {
                    delete NewValues[key];
                }
                if (PValue.hasOwnProperty(key))
                {
                    delete PValue[key];
                }
            }
        }

        for (var key in PValue)
        {
            if (!NewValues.hasOwnProperty(key))
            {
                if (key != "") {
                    NewValues[key] = PValue[key];
                }
            }
        }
        //从QueryString中取值
        if (this.UrlParms == undefined && this.IsPageRoot) {
            this.UrlParms = this.GetUrlParms();
        }
        for (var key in this.UrlParms) {
            if (!this.disValues.hasOwnProperty(key)) {
                if (key != "") {
                    NewValues[key] = this.UrlParms[key];
                }
            }
            else {
                if (NewValues.hasOwnProperty(key)) {
                    delete NewValues[key];
                }
            }
        }
        if (ValuesObj != undefined) {
            for (var key in ValuesObj) {
                if (!this.disValues.hasOwnProperty(key)) {
                    if (key != "") {
                        NewValues[key] = ValuesObj[key];
                    }
                }
                else {
                    if (NewValues.hasOwnProperty(key)) {
                        delete NewValues[key];
                    }
                }
            }
        }
        return NewValues;
    },
    //序列化参数到字符串
    ToString: function () {
        var Values = this.CloneAllKeyValue(this.Values);
        for (var key in this.disValues) {
            if (Values.hasOwnProperty(key)) {
                delete Values[key];
            }
        }
        var paramstr = "";
        var tempValue;
        for (var key in Values) {
            if (key != "") {
                tempValue = Values[key];
                switch (typeof (tempValue)) {
                    case "number":
                        paramstr += "&" + key + "=" + Values[key];
                        break;
                    case "string":
                        paramstr += "&" + key + "=" + Url.encode(Values[key]);
                        break;
                    case "boolean":
                        paramstr += "&" + key + "=" + Values[key];
                        break;
                    default:
                        //obj 和函数对象不序列化
                        break;
                }
            }

        }
        paramstr = paramstr.substr(1, paramstr.length - 1);
        return paramstr;
    },
    //得到父参数对象
    GetParentObj: function () {
        if (window.parent) {
            if (window.parent != window) {
                this.ParentParameterObj = window.parent.window.LzParameterObj;
            }
        }
    },
    GetUrlParms: function (path, NotDecode) {
        var args = new Object();
        var query = location.search.substring(1);
        if (path != undefined) {
            var index1 = path.indexOf('?');
            var index2 = path.indexOf('=');
            if (index1 == -1) {
                if (index2 == -1) {
                    return args;
                }
                else {
                    query = path;
                }
            }
            else {
                query = path.substring(index1 + 1);
            }
        }

        var pairs = query.split("&");
        for (var i = 0; i < pairs.length; i++) {
            var pos = pairs[i].indexOf('=');
            if (pos == -1) continue;
            var argname = pairs[i].substring(0, pos);
            var value = pairs[i].substring(pos + 1);
            if (NotDecode) {
                args[argname] = value;
            }
            else {
                args[argname] = Url.decode(value);
            }
        }
        return args;
    }

}


var LzHelper = function () {
    var define = this.constructor.prototype;
    var browserVersion = null;
};
LzHelper.prototype = {
    //获取当前浏览器版本
    GetBrowserVersion: function () {
        if (this.browserVersion == null);
        {
            if (navigator.userAgent.indexOf("MSIE") > 0) {
                var b_version = navigator.appVersion
                var version = b_version.split(";");
                var trim_Version = version[1].replace(/[ ]/g, "");
                this.browserVersion = trim_Version;

            }
            if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
                this.browserVersion = "Firefox";
            }
            if (isSafari = navigator.userAgent.indexOf("Safari") > 0) {
                this.browserVersion = "Safari";
            }
            if (isCamino = navigator.userAgent.indexOf("Camino") > 0) {
                this.browserVersion = "Camino";
            }
            if (isMozilla = navigator.userAgent.indexOf("Gecko/") > 0) {
                this.browserVersion = "Gecko";
            }
        }
        return this.browserVersion;

    },
    //*****************
    //创建参数对象
    //*****************
    CreateParameterObj: function (ParentParameterObj) {
        return new LzParameter(ParentParameterObj);
    },
    InheritObj: function (A, B) {
        for (var key in B) {
            A[key] = B[key];
        }
    },
    InheritObjWeek: function (A, B) {
        for (var key in B) {
            if (!A.hasOwnProperty(key)) {
                A[key] = B[key];
            }
        }
    },
    UnionParameter: function (Path, ParameterObj) {
        var strPath = Path;
        if (Path.indexOf("?") == -1) {
            return Path + "?" + ParameterObj.ToString();
        }
        else {
            var tt = strPath.substring(strPath.indexOf("?") + 1, strPath.length).split("&");
            var newPObj = this.CreateParameterObj(ParameterObj);
            newPObj.Values = ParameterObj.GetUrlParms(Path);
            //this.InheritObjWeek(newPObj.Values, ParameterObj.Values);
            strPath = strPath.substring(0, strPath.indexOf("?")) + "?" + newPObj.ToString();
        }
        return strPath;
    },
    //*******打开窗口
    //StrParameter  参数对象字符串   XXX=value1&YYY=value2
    //UrlId         页面地址UrlGUID
    OpenGUID: function (UrlIdP, StrParameter, ClientConfigObj, ShortRe) {
        var NewPObj = this.CreateParameterObj(LzParameterObj);
        NewPObj.Values = NewPObj.GetUrlParms(StrParameter, true);
        if (typeof (ClientConfigObj) == "string") {
            ClientConfigObj = NewPObj.GetUrlParms(ClientConfigObj);
        }
        this.Open(NewPObj, UrlIdP, ClientConfigObj, ShortRe);
    },
    //刷新页面
    RefreshPage: function () {
        window.opener.location.href = window.opener.location.href.replace("#", "");
    },
    //*******打开窗口
    //ParameterObj  参数对象，可为空
    //UrlId         页面地址Url
    Open: function (ParameterObjP, UrlIdP, ClientConfigObj, ShortRe) {

        //处理参数
        var ParameterObj = ParameterObjP;
        var UrlId = UrlIdP;
        if (typeof (ParameterObjP) == "string") {
            UrlId = ParameterObjP;
            ParameterObj = UrlIdP;
        }
        if (ParameterObj == undefined || ParameterObj == null) {
            ParameterObj = LzParameterObj;
        }

        for (var key in ParameterObj.Values) {
            if (ParameterObj.Values[key] == null || ParameterObj.Values[key] == "") {
                ParameterObj.Set(key);
            }
        }
        //iframe方式
        if (ClientConfigObj != undefined && ClientConfigObj != null && ClientConfigObj.Function != null && ClientConfigObj.Function != undefined) {
            //获取Path
            var strPath = UrlId;
            if (strPath.indexOf("/") != -1 || strPath.indexOf("\\") != -1) {
                strPath = UrlId;
            }
            else {
                strPath = this.ExeCommond("{Url[" + UrlId + "].GetPath}", ParameterObj);
            }
            //计算参数
            var newPObj = this.CreateParameterObj(ParameterObj);
            newPObj.disValues = ParameterObj.disValues;
            newPObj.Values = ParameterObj.GetUrlParms(strPath);
            //debugger
            if (strPath.indexOf("?") == -1) {
                strPath = strPath + "?" + newPObj.ToString();
            }
            else {
                strPath = this.UnionParameter(strPath, newPObj);
            }

            var Exce = new Function(ClientConfigObj.Function + "('" + strPath + "')");

            Exce();
            return;
        }
        //iframe方式
        else if (ClientConfigObj != undefined && ClientConfigObj != null && ClientConfigObj.iframe != null && ClientConfigObj.iframe != undefined) {
            var FormObj = $("#" + ClientConfigObj.iframe);
            //ClientConfigObj.isAutoHeight
            //查找子框架
            if (FormObj != null) {
                FormObj = FormObj.get(0);
            }
            else {
                var win = window;
                while (FormObj == null && win.parent != null) {
                    win = win.parent;
                    FormObj = win.document.getElementById(ClientConfigObj.iframe);
                }
                if (FormObj == null || FormObj == undefined) {
                    alert("没有发现iframe ：" + ClientConfigObj.iframe);
                }
            }
            //获取Path

            var strPath = UrlId;
            if (strPath.indexOf("/") != -1 || strPath.indexOf("\\") != -1) {
                strPath = UrlId;
            }
            else {
                strPath = this.ExeCommond("{Url[" + UrlId + "].GetPath}", ParameterObj);
            }
            //计算参数
            var newPObj = this.CreateParameterObj(ParameterObj);
            newPObj.disValues = ParameterObj.disValues;
            newPObj.Values = ParameterObj.GetUrlParms(strPath);
            //debugger
            if (strPath.indexOf("?") == -1) {
                strPath = strPath + "?" + newPObj.ToString();
            }
            else {
                strPath = this.UnionParameter(strPath, newPObj);
            }

            var self = this;

            if (ClientConfigObj.isAutoHeight == true) {

                //  var loadFun = FormObj.onreadystatechange;

                FormObj.onreadystatechange = function (a, b, c) {
                    if (FormObj.readyState == "complete") {
                        //  if (loadFun != null) {
                        //      loadFun(a, b, c);
                        //   }
                        self.LayoutIframeAjust(FormObj);
                    }
                }
            }

            //            if (FormObj.onload == null) {

            //                FormObj.onload = function () {
            //                    //debugger;
            //                    debugger;
            //                    self.LayoutIframeAjust();
            //                }
            //            }
            //页面请求
            FormObj.src = strPath;
            return;
        }
        else {
            var strPath = UrlId;
            if (strPath.indexOf("/") != -1 || strPath.indexOf("\\") != -1) {
                strPath = this.UnionParameter(strPath, ParameterObj);
                var sName = "";
                if (ClientConfigObj && ClientConfigObj.sName) {
                    sName = ClientConfigObj.sName;
                }
                window.open(strPath, sName, "Height:768px,Width:1024px,resizable=yes,scrollbars=yes,status=yes"); //"about:blank"
                return;
            }
        }
        //获得UrlId对应的页面配置信息
        var commondstr = "{Url[" + UrlId + "].GetJsonConfig}";
        var ConfigObj = this.ExeCommond(commondstr, ParameterObj);
        //debugger
        if (commondstr == ConfigObj) {
            alert("页面配置信息出错：" + commondstr);
            return;
        }
        eval("ConfigObj = " + ConfigObj + ";");
        //兼容客户端配置信息
        if (ClientConfigObj != null) {
            this.InheritObj(ConfigObj, ClientConfigObj);
            //jQuery.extend(ConfigObj, ClientConfigObj);
        }
        //窗口打开方式
        var RequestType = ConfigObj.RequestType == "" ? "get" : ConfigObj.RequestType.toLowerCase();
        var ModeDialogbox = ConfigObj.ModeDialogbox == "" ? "no" : ConfigObj.ModeDialogbox.toLowerCase();
        //设置窗口特性
        if (ConfigObj.Proportion.toLowerCase() == "yes") {
            ConfigObj.width = window.screen.width;
            ConfigObj.height = window.screen.height;
            ConfigObj.top = "0px";
            ConfigObj.left = "0px";
        }
        var sFeatures = "channelmode=" + ConfigObj.channelmode
            + ",directories=" + ConfigObj.directories
            + ",channelmode=" + ConfigObj.channelmode
            + ",fullscreen=" + ConfigObj.fullscreen
            + (ConfigObj.left == "" ? "" : ",left=" + ConfigObj.left)
            + (ConfigObj.top == "" ? "" : ",top=" + ConfigObj.top)
            + (ConfigObj.width == "" ? "" : ",width=" + ConfigObj.width)
            + (ConfigObj.height == "" ? "" : ",height=" + ConfigObj.height)
            + ",location=" + ConfigObj.location
            + ",menubar=" + ConfigObj.menubar
            + ",resizable=" + ConfigObj.resizable
            + ",scrollbars=" + ConfigObj.scrollbars
            + ",status=" + ConfigObj.status
            + ",titlebar=" + ConfigObj.titlebar
            + ",toolbar=" + ConfigObj.toolbar;

        var windowName = UrlId ? UrlId : "LzOpenWindowName";
        var sName = ConfigObj.sName;
        //是否是参数补填框架过来？
        if (ShortRe) {
            sName = "_self";
        }
        //获得窗口地址
        var path = ConfigObj["sURL"];
        //获得新参数对象
        var NewParameterObj = this.CreateParameterObj(ParameterObj);
        this.InheritObj(NewParameterObj.Values, ConfigObj.Parameters); //jQuery.extend(NewParameterObj.Values, ConfigObj.Parameters);


        //创建\获取专用Form
        var FormObj = this.GetFormObj();
        //获得缺失参数集合
        var ShortParameters = {};
        if (ConfigObj.ShortParameters.length > 0) {
            $.each(ConfigObj.ShortParameters, function (i, item) {
                ShortParameters[item.ParameterKey] = item.ParameterName;
            });
            //如果存在缺失参数，则转向参数补填框架
            FormObj.item("LzPostShortParameters").value = jQuery.param(ShortParameters);
            //指定转向参数补填框架地址
            path = "/Foundation/LzProduct/UrlManager/ShortParameters.aspx";
            RequestType = "post";
        }

        if (RequestType == "post" && ModeDialogbox == "no") {
            //获得参数值
            var Value = NewParameterObj.ToString();
            FormObj.item("LzPostUrlId").value = UrlId;
            FormObj.item("LzPostParametersValue").value = Value;

            FormObj.action = path;
            FormObj.method = RequestType;
            FormObj.target = windowName;
            var NewWindowObj = window.open("", sName, sFeatures, ConfigObj.bReplace); //"about:blank"
            NewWindowObj.name = windowName;
            FormObj.submit();
        }
        else {
            //var Values = NewParameterObj.CloneAllKeyValue(NewParameterObj.Values);

            var ParamString = NewParameterObj.ToString();
            if (ParamString != "") {
                if (path.indexOf("?") == -1) {
                    path = path + "?" + ParamString;
                }
                else {
                    path = this.UnionParameter(path, NewParameterObj);
                    //path = path + "&" + NewParameterObj.ToString();
                }
            }
            if (ModeDialogbox == "no") {
                var NewWindowObj = window.open(path, sName, sFeatures, ConfigObj.bReplace); //"about:blank"
                NewWindowObj.name = windowName;
            }
            else {
                ConfigObj.dialogHeight = ConfigObj.dialogHeight == "100%" ? (window.screen.availHeight) : ConfigObj.dialogHeight;
                ConfigObj.dialogWidth = ConfigObj.dialogWidth == "100%" ? (window.screen.availWidth) : ConfigObj.dialogWidth;
                sFeatures = "center:" + ConfigObj.center
                            + ";dialogHide:" + ConfigObj.dialogHide
                            + (ConfigObj.dialogHeight == "" ? "" : ";dialogHeight:" + ConfigObj.dialogHeight)
                            + (ConfigObj.dialogLeft == "" ? "" : ";dialogLeft:" + ConfigObj.dialogLeft)
                            + (ConfigObj.dialogTop == "" ? "" : ";dialogTop:" + ConfigObj.dialogTop)
                            + (ConfigObj.dialogWidth == "" ? "" : ";dialogWidth:" + ConfigObj.dialogWidth)
                            + ";scroll:" + ConfigObj.scroll;
                var result = window.showModalDialog(path, ConfigObj, sFeatures);
                return result;
            }
        }

    },
    //创建专用的Form对象
    GetFormObj: function () {
        //查找Form
        var FormObj = $("#LzWindowOpenForm");
        if (FormObj[0] != undefined) {
            return FormObj[0];
        }
        else {
            var bodyDom = $("body");
            bodyDom.append('<form id="LzWindowOpenForm" action="#" method="post" name="LzWindowOpenForm">' +
            '<input id="LzPostShortParameters" type ="hidden" name="LzPostShortParameters" />' +
            '<input id="LzPostUrlId" type ="hidden" name="LzPostUrlId" />' +
            '<input id="LzPostParametersValue" type ="hidden" name="LzPostParametersValue" /></form>');
        }
        return $("#LzWindowOpenForm")[0];
    },

    //封装后台服务表达式执行
    //Expression        表达式字符串
    //ResultType        返回值类型,不传时默认 text     可以设置：xml\html\script\json\jsonp\text
    //Callback          当callback有数值时自动采用异步调用，否则同步执行
    ExeCommond: function (LzExpression, ParameterObjP, ResultType, Callback, SettingObj) {
        var browser = navigator.appName
        var b_version = navigator.appVersion
        var version = b_version.split(";");
        var trim_Version = version[1].replace(/[ ]/g, "");
        if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE6.0") {
            return this.ExeCommond2(LzExpression, ParameterObjP, ResultType, Callback);
        }

        var ParameterObj = ParameterObjP;
        if (ParameterObj == undefined || ParameterObj == null) {
            ParameterObj = LzParameterObj;
        }
        //var PostData = "LzPostExecExpression=" + LzExpression + "@LzExpressionSplit@" + ParameterObj.ToString();
        var PostData = jQuery.param({ LzPostExecExpression: LzExpression }) + "&" + ParameterObj.ToString();

        var ajaxObj = {
            type: "POST",
            url: "/Foundation/LzProduct/HttpER/HttpER.aspx",
            data: PostData
        }
        if (ResultType) {
            ajaxObj.dataType = ResultType;
        }
        else {
            ajaxObj.dataType = "text";
        }
        if (Callback) {
            ajaxObj.async = true;
            ajaxObj.success = function (msg) {
                Callback(msg);
            };
        }
        else {
            ajaxObj.async = false;
        }
        if (SettingObj) {
            jQuery.extend(ajaxObj, SettingObj);
        }
        if (ajaxObj.async) {
            $.ajax(ajaxObj);
        } else {
            if (ajaxObj.dataType == 'json') {
                return this.parseJSON($.ajax(ajaxObj).responseText);
            }
            else if (ajaxObj.dataType == "text") {
                return $.ajax(ajaxObj).responseText;
            }
            else {
                return jQuery.httpData($.ajax(ajaxObj), ajaxObj.dataType, ajaxObj);
            }
        }
    },
    //支持IE浏览器(通用Post请求)现在只是IE6再用
    ExeCommond2: function (LzExpression, ParameterObjP, ResultType, Callback) {

        var httpAdapter = window.ActiveXObject ? new ActiveXObject("Microsoft.XMLHTTP") : new httpAdapterRequest();

        if (httpAdapter) {
            var isAsync = false;
            if (Callback) {
                isAsync = true;
            }

            var ParameterObj = ParameterObjP;
            if (ParameterObj == undefined || ParameterObj == null) {
                ParameterObj = LzParameterObj;
            }
            //var PostData = "LzPostExecExpression=" + LzExpression + "@LzExpressionSplit@" + ParameterObj.ToString();
            var PostData = jQuery.param({ LzPostExecExpression: LzExpression }) + "&" + ParameterObj.ToString();

            httpAdapter.Open("POST", "/Foundation/LzProduct/HttpER/HttpER.aspx", isAsync);

            httpAdapter.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            httpAdapter.Send(PostData);

            if (isAsync) {
                httpAdapter.onreadystatechange = function () {
                    if (httpAdapter.readyState == 4) {
                        if (httpAdapter.status == 200) {
                            if (ResultType == 'json') {
                                Callback(this.parseJSON(httpAdapter.responseText));
                            }
                            Callback(httpAdapter.responseText);
                        }
                    }
                }

            } else {
                if (ResultType == 'json') {
                    return this.parseJSON(httpAdapter.responseText);
                }

                return httpAdapter.responseText;
            }

        } else {

        }



    },
    parseJSON: function (data) {
        if (typeof data !== "string" || !data) {
            return null;
        }
        if (window.JSON) {
            if (window.JSON.parse) {
                try {
                    return window.JSON.parse(data);
                } catch (e) {
                    return (new Function("return " + data))();
                }
            }
            else {
                return (new Function("return " + data))();
            }
        }
        else {
            return (new Function("return " + data))();
        }

    },
    OpenUrl: function (Url, ClientConfigObj) {
        if (ClientConfigObj != undefined && ClientConfigObj.iframe != null && ClientConfigObj.iframe != undefined) {
            var FormObj = $("#" + ClientConfigObj.iframe);
            //查找子框架
            if (FormObj != null) {
                FormObj = FormObj.get(0);
            }
            else {
                var win = window;
                while (FormObj == null && win.parent != null) {
                    win = win.parent;
                    FormObj = win.document.getElementById(ClientConfigObj.iframe);
                }
                if (FormObj == null || FormObj == undefined) {
                    alert("没有发现iframe ：" + ClientConfigObj.iframe); return;
                }
            }
            //页面请求
            FormObj.src = Url;
            return;
        }
        window.open(Url);
    },
    ListDel: function (TableName, KeyName) {
        var Str = document.getElementById('gvList').selectCheckBoxValues;

        if (Str != null && Str != "") {
            if (confirm("确定删除已选定的数据！")) {
                var sRtn = LzHelper.ExeCommond("{CS.DataBase.DeleteItems[LzProduct_App][" + TableName + "][" + KeyName + "][" + Str + "]}");
                if (sRtn) {
                    alert("删除成功！");
                    window.location.href = window.location.href.replace("#", "");

                }
                else {

                    alert("删除失败！");
                }
            }
        }
        else {
            alert("请选择要删除的数据！");

        }

    },
    LayoutIframeAjust: function (iframeObj) {
        if (iframeObj == undefined) {
            var num = 0;
            if (LzHelper.GetBrowserVersion() == "MSIE9.0") {

                var iframes = document.getElementsByTagName("iframe");
                for (var i = 0; i < iframes.length; i++) {
                    var iff = iframes[i];
                    try {
                        var Height = window.screen.availHeight;

                        var tempHeight = iff.contentWindow.document.body.scrollHeight;
                        var tempHeight2 = iff.contentWindow.document.documentElement.scrollHeight;
                        var tempHeight3 = Math.max(tempHeight, tempHeight2);
                        var iframeHeight = Math.max(Height, tempHeight3);
                        iff.setAttribute("height", iframeHeight);
                    }
                    catch (ex) {
                    }
                }
            }
            else {
                var iframes = document.getElementsByTagName("iframe");
                for (var i = 0; i < iframes.length; i++) {
                    var iff = iframes[i];
                    try {
                        var a = document.frames ? document.frames[iff.id].document : iff.contentDocument;
                        if (iff != null && a != null) {
                            iff.height = Math.max(iff.clientHeight, a.body.scrollHeight);
                        }
                        if (iff.src != "") {
                            var list = a.getElementById("divListContainer");
                            if (list) {
                                list.style.overflow = "";
                            }
                        }

                    }
                    catch (ex) { }
                }
            }
            document.body.setAttribute("height", num);
        } else {
            var iff = iframeObj;
            if (LzHelper.GetBrowserVersion() == "MSIE9.0") {
                var Height = window.screen.availHeight;

                var tempHeight = iff.contentWindow.document.body.scrollHeight;
                var tempHeight2 = iff.contentWindow.document.documentElement.scrollHeight;
                var tempHeight3 = Math.max(tempHeight, tempHeight2);
                var iframeHeight = Math.max(Height, tempHeight3);
                iff.setAttribute("height", iframeHeight);

            } else {
                var a = document.frames ? document.frames[iff.id].document : iff.contentDocument;
                if (iff != null && a != null) {
                    iff.height = Math.max(iff.clientHeight, a.body.scrollHeight);

                }
            }

        }

    },
    //参数验证,并处理自身参数对象 或返回新的参数对象 
    ParameterValidator: function (ObjectID, ParamterObjP, IsGetNewPara) {
        var NewParameterObj = ParamterObjP;
        if (IsGetNewPara) {
            NewParameterObj = LzHelper.CreateParameterObj(ParamterObjP);
        }

        var result = LzHelper.ExeCommond("{ParameterRestrain[" + ObjectID + "].Execute.ToJson}", NewParameterObj);

        var resultObj = (new Function("return " + result))();
        var resultObjArr = resultObj.ParameterRestrainItems;
        if (resultObjArr != null && resultObjArr.length > 0) {
            //2. 不完整时弹出页面补全参数
            //this.CreateTableByParameters(resultObjArr, KeyID, ParamterObjP);
            var dialogParametersObj = { Arr: resultObjArr, KeyID: ObjectID, Parameters: NewParameterObj };
            var returnObj;
            var hasResult = false;
            while (!hasResult) {
                returnObj = window.showModalDialog('/Foundation/LzProduct/Commonjs/Paremeter.html', dialogParametersObj, "dialogHeight:800px;dialogWidth:800px;");
                if (typeof returnObj !== 'undefined') {
                    hasResult = true;
                }
            }
            NewParameterObj = returnObj.ParameterObj;
            if (resultObj.ParameterObj.Parameters != null && resultObj.ParameterObj.Parameters.length > 0) {
                $.each(resultObj.ParameterObj.Parameters, function (i, n) {
                    NewParameterObj.Set(n.ParameterKey, n.ParameterValue)
                });
            }
        }
        else {
            if (resultObj.Parameters != null && resultObj.Parameters.length > 0) {
                $.each(resultObj.Parameters, function (i, n) {
                    NewParameterObj.Set(n.ParameterKey, n.ParameterValue)
                });
            }
        }
        return NewParameterObj;
    },
    //2012-2-22 syq 添加
    ExecuteJS: function (KeyID, ParamterObjP) {
        if (typeof ParamterObjP == 'undefined' || ParamterObjP == null) {
            return null;
        }
        var strParameters = KeyID.split('-');
        var strNewParameter = '';
        for (var j = 0; j < strParameters.length; j++) {
            strNewParameter += "_" + strParameters[j];
        }
        //1. 验证参数是否完整,返回必须且没有的参数数组
        var result = LzHelper.ExeCommond('{ParameterValidator[' + KeyID + ']}', ParamterObjP);
        var resultObj = eval('(' + result + ')');
        var resultObjArr = resultObj.Parameters;
        if (resultObjArr.length > 0) {
            //2. 不完整时弹出页面补全参数
            //this.CreateTableByParameters(resultObjArr, KeyID, ParamterObjP);
            var dialogParametersObj = { Arr: resultObjArr, KeyID: KeyID, Parameters: ParamterObjP };
            var returnObj;
            var hasResult = false;
            while (!hasResult) {
                returnObj = window.showModalDialog('/Foundation/LzProduct/Commonjs/Paremeter.html', dialogParametersObj, "dialogHeight:800px;dialogWidth:800px;");
                if (typeof returnObj !== 'undefined') {
                    hasResult = true;
                }
            }
            ParamterObjP = returnObj.ParameterObj;
        }
        if (typeof resultObj.FuncName == 'undefined' || resultObj.FuncName == null) {
            alert('改客户端脚本不支持此方法调用');
            return;
        }
        //加载完成后执行的方法
        var IsLoaded = false;
        //拼接加载完成自动运行的脚步方法
        var FuncName = "InIt2" + strNewParameter;
        TempObj[strNewParameter] = { Parameter: ParamterObjP };
        var funcStr = 'function ' + FuncName + '()';
        funcStr += ' {';
        funcStr += 'var obj = TempObj.' + strNewParameter + ';';
        funcStr += '    if (' + resultObj.IsAyc + ') {';
        funcStr += '        setTimeout(function(){' + resultObj.FuncName.InstanceName + '( obj.Parameter );}, 1);';
        funcStr += '    } else {';
        funcStr += '        ' + resultObj.FuncName.InstanceName + '(obj.Parameter);'; funcStr += '    }';
        funcStr += '}';
        var funcScript = document.createElement('script');
        funcScript.type = 'text/javascript';
        funcScript.text = funcStr;
        document.getElementsByTagName('head')[0].appendChild(funcScript);

        //3. 判断header里是否有当前GUID ? 有：直接执行方法5 ，没有：加载脚本 4
        var scripts = document.getElementsByTagName('script');
        var str = '';

        var currentSrc = '/Foundation/LzProduct/JSManager/JavaScript.ashx?AppID=' + KeyID;

        for (var i = 0; i < scripts.length; i++) {
            var src = scripts[i].getAttribute('src');
            if (src == currentSrc) { IsLoaded = true; break; }
        }
        if (IsLoaded) {
            //5.执行脚本方法
            eval(FuncName + '()');
        }
        else {
            //4. 获取脚本代码,动态添加到header标签中
            var head = document.getElementsByTagName('head')[0];
            var script = document.createElement('script');
            script.setAttribute('type', 'text/javascript');
            head.appendChild(script);
            script.src = currentSrc
        }
    },
    //获得Cookie对象
    GetParameterObjFromCookie: function () {
        var cookies = document.cookie.split("; ");
        var parameterObj = LzHelper.CreateParameterObj();
        $.each(cookies, function () {
            var str = this.split("=");
            if (str.length == 2) {
                parameterObj.Set(str[0], unescape(str[1]));
            }
        })

        return parameterObj;
    }, //存储对象到Cookie
    SaveParameterObjToCookie: function (ParameterObj) {
        for (var i in ParameterObj.Values) {
            document.cookie = (i + "=" + escape(ParameterObj.Values[i]));
        }
    },
    //得到表单中拓展表中的字段值
    GetDataRiverValue: function (sender, ColumnName) {
        var id = sender.id.split("_");
        id = id[id.length - 1];
        var obj = document.all["DataRiver_" + id + "@" + ColumnName];
        if (obj != null) {
            return $(obj).val();
        }
        return null;
    },
    //得到表单中拓展表中的字段值
    GetDataRiverObj: function (sender, ColumnName) {
        var id = sender.id.split("_");
        id = id[id.length - 1];
        var obj = document.all["DataRiver_" + id + "@" + ColumnName];

        return obj;
    },
    Alert: function (msg, _expired) {
        var expired = 1;
        if (_expired != undefined && isNaN(_expired)==false) {
            expired = _expired;
        }
        var DataRiver_isIe = (document.all) ? true : false;
        var DataRiver_DispMsgTimerId = 0;
        var DataRiver_HideMsgEventHandler = null;
        var DataRiver_HideDivDelegate = function (bgobj, msgobj, endInt) {
            return function () {
                DataRiver_hideBackground(bgobj, msgobj, endInt);
            }
        }
        var DataRiver_showBackground = function (obj, endInt, timerid, objContainer, expiredTime2Hide) {
            if ('undefined' != timerid && null != timerid) {
                window.clearTimeout(timerid);
            }
            if (null == obj) return;
            if (DataRiver_isIe) {
                obj.filters.alpha.opacity += 10;
                if (obj.filters.alpha.opacity < endInt) {
                    timerid = setTimeout(function () { DataRiver_showBackground(obj, endInt, timerid, objContainer, expiredTime2Hide) }, 20);
                }
                else if ('undefined' != typeof (expiredTime2Hide) && null != expiredTime2Hide && expiredTime2Hide > 0) {
                    DataRiver_DispMsgTimerId = window.setTimeout(function () { DataRiver_hideBackground(objContainer, obj, 5) }, expiredTime2Hide * 1000);
                }
            } else {
                var al = parseFloat(obj.style.opacity); al += 0.01;
                obj.style.opacity = al;
                if (al < (endInt / 100)) {
                    timerid = setTimeout(function () { DataRiver_showBackground(obj, endInt, timerid, objContainer, expiredTime2Hide) }, 20);
                }
                else if ('undefined' != typeof (expiredTime2Hide) && null != expiredTime2Hide && expiredTime2Hide > 0) {
                    DataRiver_DispMsgTimerId = window.setTimeout(function () { DataRiver_hideBackground(objContainer, obj, 5) }, expiredTime2Hide * 1000);
                }
            }
        }
        var DataRiver_hideBackground = function (bgobj, msgobj, endInt, timerid) {
            if ('undefined' != timerid && null != timerid) {
                window.clearTimeout(timerid);
            }
            if (null == msgobj) {
                if (null != bgobj) bgobj.style.display = "none";
                return;
            }
            if (DataRiver_isIe) {
                msgobj.filters.alpha.opacity -= 10;
                if (msgobj.filters.alpha.opacity > endInt) {
                    setTimeout(function () { DataRiver_hideBackground(bgobj, msgobj, endInt, timerid) }, 20);
                } else {
                    msgobj.style.display = "none";
                    if (null != bgobj) bgobj.style.display = "none";
                    window.clearTimeout(DataRiver_DispMsgTimerId);
                }
            } else {
                var al = parseFloat(msgobj.style.opacity); al -= 0.01;
                msgobj.style.opacity = al;
                if (al > (endInt / 100)) {
                    setTimeout(function () { DataRiver_hideBackground(bgobj, msgobj, endInt, timerid) }, 20);
                } else {
                    msgobj.style.display = "none";
                    if (null != bgobj) bgobj.style.display = "none";
                    window.clearTimeout(DataRiver_DispMsgTimerId);
                }
            }
        }




        var autoHide = false;
        if ('undefined' != typeof (expired) && null != expired && expired > 0) autoHide = true;
        var bgDiv = document.getElementById('DataRiver_BgDiv');
        if (null == bgDiv) {
            bgDiv = document.createElement('div');
            bgDiv.id = 'DataRiver_BgDiv';
            //        bgDiv.attachEvent('onclick', DataRiver_HideMsg);
            bgDiv.style.cssText = 'display: none; position: absolute; top: 0px; left: 0px; width: 0px; height: 0px;background-color: #c1c0c0; filter: alpha(opacity=1); z-index: 30000;';
            document.body.appendChild(bgDiv);
        }
        var msgDiv = document.getElementById('DataRiver_MsgDiv');
        if (null == msgDiv) {
            msgDiv = document.createElement('div');
            msgDiv.id = 'DataRiver_MsgDiv';
            //        msgDiv.attachEvent('onclick', DataRiver_HideMsg);
            msgDiv.align = "center";
            msgDiv.style.cssText = 'display: none; position: absolute; top: 20px; left: 300px; width: 240px;height: 80px; background-color: #d6e7fc; filter: alpha(opacity=0);z-index: 30001;';
            msgDiv.innerHTML = '<table style="border: 1px; border-color: #000000; border-collapse: collapse;height:100%"><tr style="height:100%"><td style="vAlign:middle;" align="center" id="DataRiver_MsgTd"></td></tr></table>';
            document.body.appendChild(msgDiv);
        }
        if (null == DataRiver_HideMsgEventHandler) {
            DataRiver_HideMsgEventHandler = new DataRiver_HideDivDelegate(bgDiv, msgDiv, 5);
        }
        if (autoHide) {
            bgDiv.attachEvent('onclick', DataRiver_HideMsgEventHandler);
            msgDiv.attachEvent('onclick', DataRiver_HideMsgEventHandler);
        }
        else {
            bgDiv.detachEvent('onclick', DataRiver_HideMsgEventHandler);
            msgDiv.detachEvent('onclick', DataRiver_HideMsgEventHandler);
        }
        var msgTd = document.getElementById('DataRiver_MsgTd');
        msgTd.innerText = msg;
        bgDiv.style.height = window.document.body.clientHeight;
        bgDiv.style.width = window.document.body.clientWidth;
        var bodyDiv = document.getElementById('InfoPathDataForm1_ViewBody');
        var containerWidth, containerHeigth;
        containerWidth = containerHeigth = 0;
        if (null != bodyDiv) {
            if (0 < bodyDiv.clientWidth) containerWidth = bodyDiv.clientWidth;
            else if (null != bodyDiv.offsetParent) containerWidth = bodyDiv.offsetParent.clientWidth;
            if (0 < bodyDiv.clientHeight) containerHeigth = bodyDiv.clientHeight;
            else if (null != bodyDiv.offsetParent) containerHeigth = bodyDiv.offsetParent.clientHeight;
        }
        if (0 >= containerHeigth) {
            containerHeigth = window.document.body.clientHeight;
        }
        if (0 >= containerWidth) {
            containerWidth = window.document.body.clientWidth;
        }
        if (containerWidth > 260) msgDiv.style.left = (containerWidth - 260) / 2;
        if (containerHeigth > 100) msgDiv.style.top = (containerHeigth - 100) / 2.6;
        bgDiv.style.display = "block";
        msgDiv.style.display = "block";
        DataRiver_showBackground(msgDiv, 100, null, bgDiv, expired);





    }
    ,
    TriggerEvent: function (EventID, ListenerGroupID, ParameterObj) {
        var Config = null;
        eval("Config=" + this.ExeCommond("{EventAction.GetListenerGrop[" + ListenerGroupID + "].GetJsonConfig}"));

        for (var i in Config.EventList) {
            if (Config.EventList[i].EventID == EventID) {
                var ListenerList = Config.EventList[i].ListenerList;
                var isHaveServer = false;
                //触发脚本功能
                for (var j in ListenerList) {
                    var Listener = ListenerList[j];
                    if (Listener.ListenerGroupID == ListenerGroupID) {
                        switch (Listener.ServerType) {
                            case "S":
                                //  this.ExeCommond("", ParameterObj);
                                isHaveServer = true;
                                break;
                            case "C":
                                this.ExecuteJS(Listener.ActionID, ParameterObj);
                                break;
                            default:
                        }
                    }
                }
                //促发后台服务
                if (isHaveServer) {
                    this.ExeCommond("{EventAction.GetListenerGrop[" + ListenerGroupID + "].Target[" + EventID + "]}", ParameterObj);
                }
                return;
            }
        }
    }
};



LzHelper = new LzHelper();
//创建全局参数对象
var LzParameterObj = LzHelper.CreateParameterObj();
LzParameterObj.IsPageRoot = true;
//设置参数对象
LzParameterObj.UrlParms = LzParameterObj.GetUrlParms();
//从parent中设置ParentObj
if (window.parent) {
    var ParentObj = LzParameterObj.GetParentObj();
    if (ParentObj != null) {
        LzParameterObj.ParentParameterObj = ParentObj;
    }
}
