InitHtmlDate();
function InitHtmlDate () {
    //Get Data
    var ext = "{YWFunc2[7500e11c-bb5c-4041-bc88-865930b24495].Run}";
    var table = LzHelper.ExeCommond(ext, null, "json");
    
    data = getData(table.value, "0932ddda-f7aa-4a72-92bf-09c4f13f7b73");
    data.items = [];
    for (var i = 0; i < data.length; i++) {
        data[i].items = getData(table.value, data[i].id);
        for (var j = 0; j < data[i].items.length; j++) {
            temp = data[i].items[j];
            temp.items = getData(table.value, data[i].items[j].id);
        }
    }
    //bindingDom
    for (var i = 0; i < data.length; i++) {
        for (var j = 0; j < data[i].items.length; j++) {
            var temp = data[i].items[j];
            $("#t_" + i ).append('<LI><A class=submenu1 href="java-script:;">' + temp.title + '</A> <DIV class=imsc><DIV style="TOP: -26px; LEFT: 190px" class=imsubc><UL id="ul_' + i + j + '"></UL></DIV></DIV></LI>');
            for (var k = 0; k < temp.items.length; k++) {
                var temp2 = temp.items[k];
                $("#ul_" + i + j).append('<LI><A class=submenu2   onclick=btn_click("' + temp2.id + '")>' + temp2.title + '<BR></A></LI>')
            }
        }
    }
}
//function btn_click(id) {
//    LzHelper.Open(LzParameterObj, id);
//}

function getData(data, parid) {
    var result = [];
    for (var item in data.Rows) {
        if (item == "length") {
            continue;
        }
        var temp = data.Rows[item];
        if (temp.parentid == parid) {
            result[result.length] = {
                id: temp.id,
                title: temp.name,
                parentid: temp.parentid
            };
            delete data.Rows[item];
        }
    }
    return result;
}
function MM_showHideLayers() { //v9.0
    var i, p, v, obj, args = MM_showHideLayers.arguments;
    for (i = 0; i < (args.length - 2); i += 3)
        with (document) if (getElementById && ((obj = getElementById(args[i])) != null)) {
            v = args[i + 2];
            if (obj.style) { obj = obj.style; v = (v == 'show') ? 'visible' : (v == 'hide') ? 'hidden' : v; }
            obj.visibility = v;
        }
}