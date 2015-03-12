
//所有页面要执行的代码 效果 。
try {
    $(document).ready(function() {
        //文本框样式
        txtCss();
        //列表指向样式
        DynamicGrid(".m_dg1_i");
        //自定义提示框
        tipAuto();
    });
}
catch (e) { }

//弹出帮助窗口
var winHelp;
function openHelp(number, type) {
    var w = 500, h = 500;
    url = "../../Common/help/help.aspx?FLinkNumber=" + number + "&FType=" + type;
    if (winHelp == null) {
        winHelp = window.open(url, '', 'fullscreen=0,toolbar=no,width=' + w + ',height=' + h + ',location=0,directories=0,status=0,menubar=no,scrollbars=1,resizable=0');
        winHelp.moveTo((screen.width - w) / 2, (screen.height - h) / 2);
    }
    else if (winHelp.closed) {
        winHelp = window.open(url, '', 'fullscreen=0,toolbar=no,width=' + w + ',height=' + h + ',location=0,directories=0,status=0,menubar=no,scrollbars=1,resizable=0');
        winHelp.moveTo((screen.width - w) / 2, (screen.height - h) / 2);
    }
    else {
        winHelp.location.replace(url);
    }
    winHelp.outerWidth = screen.availWidth;
    winHelp.outerHeight = screen.availHeight;
    winHelp.focus();
}

function SetUrl(obj, url) {
    obj.src = url;
    obj.scrolling = 'no';

}


String.prototype.endsWith = function(str) {
    return (this.substring(this.length - str.length) == str);
}
String.prototype.trim = function() {

    return this.replace(/(\s*$)|(^\s*)/g, "");
}

function fileTypecheck() {
    var uploadFilePath = document.forms[0].HtmlInputFile1.value.trim();
    if (uploadFilePath == "") {
        document.all.btnUpload.disabled = true;
        return false;
    }
    if (!uploadFilePath.toLowerCase().endsWith("jpg")) {
        msg.innerHTML = "请上传文件类型为jpg的文件！";
        document.all.btnUpload.disabled = true;
        return false;
    }
    else {
        msg.innerHTML = "";
        document.all.btnUpload.disabled = false;
    }
}

//数据验证
function ifnull(s) {
    if (s.value == "") {
        s.focus();
        alert("有重要数据项没有填写,请填写后在进行保存！");
        return false;
    }
}

function trim(str) {
    if (typeof (str) != "string") return "";
    var rexp = /\s*$/;
    var lexp = /^\s*/;
    return str.replace(rexp, "").replace(lexp, "");
}

function isInt(s, info)  //整数输入判断
{
    if (s.value == "") return true;
    var chk = parseInt(s.value, 10);
    if (chk != s.value || chk < 0) {
        if (info == null) {
            info = '该字段';
        }
        alert(info + "应是正整数！");
        s.select();
        return false;
    }
    return true;
}
//检查录入长度
function checkLength(s, num, str) {
    if (s.value.length > num) {
        alert(str + "的字数需要控制在" + num + "字以内");
    }
}
//联系电话格式
function isTel(str) {
    var reg = /^([0-9]|[-])/;
    if (str.value != "") {
        if (!reg.exec(str.value)) {
            alert("请输入正确的电话号码");
            str.focus();
            return false;
        }
    }
}


function isZH(bb) {
    var num = 0;
    var aa = bb.value.trim();
    if (aa == "") return true;
    /////////////
    var chk = parseInt(bb.value, 10);
    if (chk != bb.value || chk < 0) {
        alert("该字段应是正整数！");
        bb.focus();
        return false;
    }
    /////////////
    for (var i = 0; i < aa.length; i++) {
        if (aa.charCodeAt(i) > 255) {
            num = num + 2;
        }
        else {
            num = num + 1;
        }
    }
    if (num != 19) {
        alert("身份证证号码不正确，应为19位！")
        bb.focus();
        return false;
    }
    else {
        return true;
    }
}

function isAge(s)  //年龄输入判断!
{
    if (s.value == "") return true;
    var chk = parseInt(s.value, 10);
    if (chk != s.value || chk < 0 || chk > 100) {
        alert("年龄字段应是正整数！");
        s.select();
        return false;
    }
    return true;
}

function isFloat(s, str)  //浮点数输入判断
{
    if (s.value == "") return true;
    var chk = parseFloat(s.value);
    if (chk != s.value) {
        if (str != null && str != undefined)
            alert(str + "应是整数或小数！");
        else
            alert("该字段应是整数或小数！");
        s.select();
        return false;
    }
    return true;
}


function isDate(obj) //时间输入判断
{
    var m, str, exp, d;
    str = trim(obj.value);
    if (str == "") return true;
    str = str.replace(/-0/g, "-");
    exp = /\d{4}-\d{1,2}-\d{1,2}/;
    if (str.match(exp) != null) {
        m = str.split("-");
        d = new Date(Date.parse(m[1] + '-' + m[2] + '-' + m[0]));
        if (d >= new Date(1800, 1, 1) && d <= new Date(2070, 6, 6) && d.getFullYear() == parseInt(m[0]) && (d.getMonth() + 1) == parseInt(m[1]) && d.getDate() == parseInt(m[2])) {
            return true;
        }
    }
    alert("该字段应是正确的日期值：YYYY-MM-DD！");
    obj.focus();
    return false;
}


function getDate(obj) {
    var aa = window.showModalDialog("../script/calen.htm", null, "dialogwidth:140pt;dialogheight:110pt;status:0");

    if (aa == null) return;
    obj.value = aa;
    return;
}

function is4Forder(obj) {
    var forder = trim(obj.value)
    if (forder.length > 4) {
        alert('序号必须是4位！');
        obj.focus();
        return false;
    }
    return true;
}

function CheckSFZHM(bb) {
    var num = 0;
    var aa = bb.value.trim();
    if (aa == "") return true;
    for (var i = 0; i < aa.length; i++) {
        if (aa.charCodeAt(i) > 255) {
            num = num + 2;
        }
        else {
            num = num + 1;
        }
    }
    if (num != 15 && num != 18) {
        alert("身份证证号码不正确，应为15位或18位！")
        bb.focus();
        return false;
    }
    else {
        return true;
    }
}

//刷新左则树
function pageReload() {
    parent.frames["left"].document.location.reload();
}

var win = null;
function OpenWin(url) {
    if (win == null) {
        win = window.open(url, '', 'fullscreen=0,toolbar=no,width=' + (screen.availWidth - 10) + ',height=' + (screen.availHeight - 50) + ',location=0,directories=0,status=1,menubar=no,scrollbars=1,resizable=1');
    }
    else if (win.closed) {
        win = window.open(url, '', 'fullscreen=0,toolbar=no,width=' + (screen.availWidth - 10) + ',height=' + (screen.availHeight - 50) + ',location=0,directories=0,status=1,menubar=no,scrollbars=1,resizable=1');
    }
    else {
        win.location.replace(url);
    }
    win.moveTo(0, 0);
    win.outerWidth = screen.availWidth;
    win.outerHeight = screen.availHeight;
    win.focus();
}

function OpenWinSmall(url) {
    if (win == null) {
        win = window.open(url, '', 'fullscreen=0,toolbar=no,width=' + (screen.availWidth - 200) + ',height=' + (screen.availHeight - 100) + ',location=0,directories=0,status=1,menubar=no,scrollbars=1,resizable=1');
    }
    else if (win.closed) {
        win = window.open(url, '', 'fullscreen=0,toolbar=no,width=' + (screen.availWidth - 200) + ',height=' + (screen.availHeight - 100) + ',location=0,directories=0,status=1,menubar=no,scrollbars=1,resizable=1');
    }
    else {
        win.location.replace(url);
    }
    win.outerWidth = screen.availWidth;
    win.outerHeight = screen.availHeight;
    win.focus();
}


function OpenDown(url, width, height) {
    var down = window.open(url, '', 'fullscreen=0,toolbar=no,width=' + width + ',height=' + height + ',location=0,directories=0,status=1,menubar=no,scrollbars=1,resizable=1');
    down.moveTo(-100, -100);
}

//设页面全部文本框为只读
function changeReadOnly() {
    var form = document.forms[0];
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "text" || form.elements[i].type == "textarea") {
            var e = form.elements[i];
            e.readOnly = "disabled";
        }
        if (form.elements[i].type == "submit") {
            var e = form.elements[i];
            if (e.id.indexOf('Del') > -1 || e.id.indexOf('add') > -1 || e.id.indexOf('save') > -1) {
                e.disabled = true;
            }
        }
        if (form.elements[i].type == "button") {
            var e = form.elements[i];
            if (e.id.indexOf('Del') > -1 || e.id.indexOf('add') > -1 || e.id.indexOf('save') > -1) {
                e.disabled = true;
            }
        }
        if (form.elements[i].tagName == "SELECT") {
            var e = form.elements[i];
            e.disabled = true;
        }



    }
}
//清空页面
function clearPage() {
    var form = document.forms[0];
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "text" || form.elements[i].type == "textarea") {
            var e = form.elements[i];
            e.value = "";
        }

        if (form.elements[i].tagName == "SELECT") {

            var e = form.elements[i];
            e.selectedIndex = 0;
        }
    }
}
function checkAllByTag(chk, tag) {
    var form = chk.form;
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "checkbox" && !form.elements[i].disabled) {
            if (form.elements[i].id.indexOf(tag) > -1) {
                var e = form.elements[i];
                if (e.name != chk.name)
                    e.checked = chk.checked;
            }
        }
    }
}
function checkAll(chk) {
    var form = chk.form;
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "checkbox" && !form.elements[i].disabled) {
            if (form.elements[i].id.indexOf('CheckItem') > -1) {
                var e = form.elements[i];
                if (e.name != chk.name)
                    e.checked = chk.checked;
            }
        }
    }
}

function checkAll1(chk) {
    var form = chk.form;
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "checkbox" && !form.elements[i].disabled) {
            if (form.elements[i].id.indexOf('GridItem') > -1) {
                var e = form.elements[i];
                if (e.name != chk.name)
                    e.checked = chk.checked;
            }
        }
    }
}



var checkedCount = 0;
//统计选中项
function checkCount() {
    checkedCount = 0;
    var form = document.forms[0];
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "checkbox" && form.elements[i].checked == true) {
            if (form.elements[i].id.indexOf('CheckItem') > -1)
            { checkedCount++; }
        }
    }
    return checkedCount;
}
function mouseOver(con) {
    if (!con.contains(event.fromElement)) {
        con.bgColor = '#E5E7F7';
    }
}
function mouseOut(con) {

    con.bgColor = '#ffffff';

}
function setfo(x) {
    var range = x.createTextRange();
    range.moveStart("character", x.value.length);
    //range.moveEnd("character",0);
    range.select();
}


function hidControl(obj) {
    obj.style.display = "none";
}

function showControl(obj) {
    obj.style.display = "";
}


//关闭后不用刷新父页面的
function showApproveWindow(sUrl, width, height) {
    window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')
}


function showInputDataWindow(sUrl, width, height) {
    var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
    if (rv != null && rv == "1") {
        if (document.getElementById('btnSearch') != null) {
            document.getElementById('btnSearch').click();
        }
        if (document.getElementById('btnReload') != null) {
            document.getElementById('btnReload').click();
        }
    }
}

function showAddWindow_ru(sUrl, width, height, txtid, btnid) {
    var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
    if (rv != null) {
        if (document.getElementById(txtid))
            document.getElementById(txtid).value = rv;
        if (document.getElementById(btnid))
            document.getElementById(btnid).click();
        return rv;
    }
    return "";
}


//关闭后刷新父页面
function showAddWindow(sUrl, width, height, reloadCon) {
    var parm = "";
    var dialogHeight = "dialogHeight:" + height + "px";
    var dialogWidth = "dialogWidth:" + width + "px";

    //兼容火狐，在屏幕中间弹出对话框。
    if (navigator.userAgent.toLowerCase().indexOf('msie') > 0) {
        parm = dialogHeight + ";" + dialogWidth + ";center=yes;resizable:yes;";
    }
    else {
        var x = (window.screen.width / 2) - (width / 2) + "px";
        var y = (window.screen.height / 2) - (height / 2) + "px";
        parm = dialogHeight + ";" + dialogWidth + ";dialogLeft:" + x + ";dialogTop:" + y + ";";
    }

    var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', parm);
    if (rv != null && rv == "1") {
        if (reloadCon && reloadCon != undefined)
            reloadCon.click();
        else {
            if (document.getElementById('btnSearch') != null) {
                document.getElementById('btnSearch').click();
            }
            else if (document.getElementById('btnReload') != null) {
                document.getElementById('btnReload').click();
            }
            else if (document.getElementById('btnQuery') != null) {
                document.getElementById('btnQuery').click();
            }
            
        }
    }
}

//关闭后返回值
function showAddWindow_rv(sUrl, width, height) {
    var parm = "";
    var dialogHeight = "dialogHeight:" + height + "px";
    var dialogWidth = "dialogWidth:" + width + "px";

    //兼容火狐，在屏幕中间弹出对话框。
    if (navigator.userAgent.toLowerCase().indexOf('msie') > 0) {
        parm = dialogHeight + ";" + dialogWidth + ";center=yes;resizable:yes;";
    }
    else {
        var x = (window.screen.width / 2) - (width / 2) + "px";
        var y = (window.screen.height / 2) - (height / 2) + "px";
        parm = dialogHeight + ";" + dialogWidth + ";dialogLeft:" + x + ";dialogTop:" + y + ";";
    }

    return window.showModalDialog(sUrl + '&rid=' + Math.random(), '', parm);
}

//弹出窗口，有返回值
function showWinByReturn(sUrl, width, height) {
    return window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:yes; help:no;scroll:auto;');
}

//弹出窗口，有返回值，不可改变窗口大小
function showAddWindow_re(sUrl, width, height, reloadCon) {
    var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:no; status:no; help:no;scroll:auto;');

    if (rv != null && rv == "1") {

        reloadCon.click();
    }
}

function showUpPicWindow(sUrl, width, height) {
    if (document.getElementById('hidden_Fid').value == null || document.getElementById('hidden_Fid').value == "") {
        alert('请先保存');
        return false;
    }
    var idvalue = window.showModalDialog(sUrl + '?fid=' + document.getElementById('hidden_Fid').value + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:no;')

    if (idvalue != null && idvalue.indexOf('.') != -1) {
        document.all.img_EmpPic.src = idvalue;
    }
}


function showEntUpPicWindow(sUrl, sCon, width, height) {

    var idvalue = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:no;')

    if (idvalue != null && idvalue.indexOf('.') != -1) {
        sCon.style.display = "block";
        sCon.src = idvalue;
        //       document.all.img_EmpPic.src = idvalue;
    }
}



function showSearchWindow(obj, sUrl, width, height) {


    var idvalue = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

    if (idvalue != null && idvalue != "") {
        obj.value = idvalue;
    }
    else {
        obj.value = '';
    }
    document.all.btnSearch.click();
}

function retValue(obj) {
    window.returnValue = obj;
    window.close();
}

function showLeft() {
    if (parent.trleft.cols != "200,8,*") {
        parent.trleft.cols = "200,8,*";
    }

}

function hidLeft() {
    if (parent.trleft.cols == "200,8,*") {
        parent.trleft.cols = "0,8,*";
    }

}



function exitSystem() {
    document.all.btnExit.click();
}

function ShowQueryDiv(obj) {
    var cDiv = document.getElementById('Div_Query');
    if (cDiv == null) {
        return;
    }
    else {
        cDiv.style.display = "";
        cDiv.style.top = (document.body.clientHeight - parseInt(cDiv.style.height.replace("px", ""))) / 2;
        cDiv.style.left = (document.body.clientWidth - parseInt(cDiv.style.width.replace("px", ""))) / 2;
        cDiv.style.position = "absolute";
        //        cDiv.style.backgroundColor="Transparent";
        cDiv.style.backgroundColor = "#FFFFFF";


        with (document.getElementById('overDiv')) {
            style.display = "";
            style.width = document.body.clientWidth;
            style.height = document.body.clientHeight;
            style.top = 0;
            style.left = 0;
            style.position = "absolute";
            style.zIndex = 10;
            style.backgroundColor = "#0000FF";
        }
    }
    document.getElementById("hidden_type").value = obj.id;
}





function clen(con, len) {

    if (calculate_byte(con.value) > len) {
        con.focus();
        return false;
    }
    return true;
}


/////////////////////
//身份证格式验证
//-----------//
function isIdCard(con) {
    if (con.value.trim() == "") {
        alert("请输入身份证号码");
        con.select();
        return false;
    }
    if (con.value.length != 18 && con.value.length != 15) {
        alert("您填入的身份证号码有误！");
        con.select();
        return false;
    }
    if (!isIdCardNo(con.value)) {
        con.select();
        return false;
    }
    return true;
}


function isIdCardNo(num) {
    var factorArr = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1);
    var error;
    var varArray = new Array();
    var intValue;
    var lngProduct = 0;
    var intCheckDigit;
    var intStrLen = num.length;
    var idNumber = num;

    if ((intStrLen != 15) && (intStrLen != 18)) {
        error = "输入身份证号码长度不对！";
        alert(error);
        return false;
    }
    for (i = 0; i < intStrLen; i++) {
        varArray[i] = idNumber.charAt(i);
        if ((varArray[i] < '0' || varArray[i] > '9') && (i != 17)) {
            error = "错误的身份证号码！.";
            alert(error);
            return false;
        } else if (i < 17) {
            varArray[i] = varArray[i] * factorArr[i];
        }
    }
    if (intStrLen == 18) {
        var date8 = idNumber.substring(6, 14);
        if (checkDate(date8) == false) {
            error = "身份证中日期信息不正确！.";
            alert(error);
            return false;
        }
        for (i = 0; i < 17; i++) {
            lngProduct = lngProduct + varArray[i];
        }
        intCheckDigit = 12 - lngProduct % 11;
        switch (intCheckDigit) {
            case 10:
                intCheckDigit = 'X';
                break;
            case 11:
                intCheckDigit = 0;
                break;
            case 12:
                intCheckDigit = 1;
                break;
        }
        //        if (varArray[17].toUpperCase() != intCheckDigit) {
        //            error = "身份证效验位错误!...正确为： " + intCheckDigit + ".";
        //            alert(error);
        //            return false;
        //        }
    }
    else {
        var date6 = idNumber.substring(6, 12);
        if (checkDate(date6) == false) {
            alert("身份证日期信息有误！.");
            return false;
        }
    }
    return true;
}
function checkDate(date) {
    return true;
}
function getBrithByIdCard(con, IdCard) {
    var Ydar = IdCard.substring(6, 10);
    var Month = IdCard.substring(10, 12);
    var Day = IdCard.substring(12, 14);
    con.value = Ydar + '-' + Month + '-' + Day;
}


//必填项判断
function AutoCheckInfo() {
    //            alert($("span[innerHTML=*]").length);
    //只能用在文本框和下拉框的判定上面
    var isok = true;
    $("tt").parent().find("input[type=text],select,textarea,input[type=radio],input[type=checkbox]").each(function(i) {
        var msg;
        if ($(this).is("input[type=radio]")) {
            if (!$("input[id^=" + $(this).attr("name") + "]:checked").val()) isok = false;
            var s = $.trim($(this).parent().prev().text()).replace(":", "").replace("：", "");
            if (!s) s = $.trim($(this).parent().parent().prev().text()).replace(":", "").replace("：", "");
            msg = "请选择 " + s + "！";
        }
        else if ($(this).is("input[type=checkbox]")) {
            if ($(":checkbox[id^=" + $(this).attr("name").replace("$0", "") + "]").attr("id")) {
                if ($("input:checkbox[id^=" + $(this).attr("name").replace("$0", "") + "]:checked").length == 0) isok = false;
                var s = $.trim($(this).parent().prev().text()).replace(":", "").replace("：", "");
                if (!s) s = $.trim($(this).parent().parent().prev().text()).replace(":", "").replace("：", "");
                msg = "请至少选择一个 " + s + "！";
            }
        }
        else if ($(this).is("input") || $(this).is("textarea")) {//如果是input控件
            if (!$.trim($(this).val())) isok = false;
            msg = "请填写 " + $.trim($(this).parent().prev().text()).replace(":", "").replace("：", "") + "！";
        }
        else {//如果是select控件
            if (!$.trim($(this).val())) isok = false;
            msg = "请选择 " + $.trim($(this).parent().prev().text()).replace(":", "").replace("：", "") + "！";
        }
        if (!isok) {
            if ($(this).is("input") || $(this).is("textarea"))//应对日期控件未填时会赋值到按钮上。
                $(this).select();
            else
                $(this).focus();
            alert(msg);
            return false;
        }
    });

    if (!isok) {
        return false;
    }
    //其余的判定条件

    return true;
}

//验证复选框
function checkBoxList_Nocheck(name) {
    var checked = true;
    $("input[name^='" + name + "']").each(function() {
        if (this.checked) {
            checked = false;
        }
    });
    return checked;
}


//列表光标移动效果
function DynamicGrid(myclass) {
    var temp = ".m_dg1_i";
    if (myclass != "" && myclass != undefined) {
        temp = myclass;
    }
    $(temp).each(function() {
        $(this).bind("mouseover", function() {
            $(this).addClass("m_dg1_i_b"); //("background-color", "#E5F1FF");
        });
        $(this).bind("mouseout", function() {
            $(this).removeClass("m_dg1_i_b")
        });
    });
}

//textbox的特效
function txtCss() {
    $(":text[class=m_txt],textarea[class=m_txt]").focus(function() { $(this).removeClass("m_txt").removeClass("m_txt_hover").addClass("m_txt_focus"); });
    $(":text[class=m_txt],textarea[class=m_txt]").blur(function() { $(this).removeClass("m_txt_hover").removeClass("m_txt_focus").addClass("m_txt"); });
    $(":text[class=m_txt],textarea[class=m_txt]").mouseover(function() { $(this).removeClass("m_txt").addClass("m_txt_hover"); });
    $(":text[class=m_txt],textarea[class=m_txt]").mouseout(function() { $(this).removeClass("m_txt_hover").addClass("m_txt"); });
    $(":text[readonly='readonly'],textarea[readonly='readonly']").css("color", "#999999"); //只读文本框文字变灰
    $("[disabled='disabled']").css("color", "#999999"); //不可用按钮文字变灰
    //清空按钮
    $(":reset").click(function() {
        $(":text").attr("value", "");
        $("select").each(function() {
            if (!$(this).attr("onchange") && !$(this).is("[disabled]"))
                $(this).attr("value", "");
        });
        return false;
    });

}



//加随机参
function addRan(url) {
    if (url.indexOf("?") < 0)
        url += "?";
    if (url.indexOf("&r=") > -1)
        url = url.replace(getRequest(url, "r"), Math.random().toString().substr(3, 8));
    else
        url += "&r=" + Math.random().toString().substr(3, 8);
    return url;
}

//从URL地址中取出指定参数的值
function getRequest(strHref, strName) {
    var intPos = strHref.indexOf("?");
    var strRight = strHref.substr(intPos + 1);
    var arrTmp = strRight.split("&");
    for (var i = 0; i < arrTmp.length; i++) {
        var arrTemp = arrTmp[i].split("=");
        if (arrTemp[0].toUpperCase() == strName.toUpperCase())
            return arrTemp[1];
    }
    return "";
}


//火狐、IE滚动条，主要用于左则菜单 
//<body style="height: 100%" onresize="AUTOoverflow('div_menu');" onload="AUTOoverflow('div_menu');">
function AUTOoverflow(objId) {
    var one = document.getElementById(objId); //需要滚动条的DIV高
    one.style.height = window.document.documentElement.offsetHeight - 30; //-40：可根据需要时调整
}


//得到元素left
function getAbsPoint_x(e) {
    var x = e.offsetLeft;
    while (e = e.offsetParent) {
        x += e.offsetLeft;
    }
    return x;
}

//得到元素top
function getAbsPoint_y(e) {
    var y = e.offsetTop;
    while (e = e.offsetParent) {
        y += e.offsetTop;
    }
    return y;
}


//得到元素的高
function getHeight(elem) {
    if (elem.style.display != "none") {
        return elem.offsetHeight || parseInt(cssY(elem));
    }
    var old = resetCSS(elem, {
        display: '',
        visibility: 'hidden',
        position: 'absolute'
    });
    var h = elem.clientHeight || parseInt(cssY(elem));
    restoreCSS(elem, old);
    return h;
}

//得到元素的宽
function getWidth(elem) {
    if (elem.style.display != "none") {
        return elem.offsetWidth || parseInt(cssX(elem));
    }
    var old = resetCSS(elem, {
        display: '',
        visibility: 'hidden',
        position: 'absolute'
    });
    var w = elem.clientWidth || parseInt(cssX(elem));
    restoreCSS(elem, old);
    return w;
}

//直接得到宽度
function cssX(elem) {
    if (elem.style.width) { return elem.style.width; }
    if (elem.currentStyle) return elem.currentStyle.width;
    if (document.defaultView && document.defaultView.getComputedStyle)
    { return document.defaultView.getComputedStyle(elem, "").getPropertyValue("width"); }
}
//直接得到高度
function cssY(elem) {
    if (elem.style.height) { return elem.style.height; }
    if (elem.currentStyle) return elem.currentStyle.height;
    if (document.defaultView && document.defaultView.getComputedStyle)
    { return document.defaultView.getComputedStyle(elem, "").getPropertyValue("height"); }
}
//设置一组CSS属性的函数
function resetCSS(elem, prop) {
    var old = {};
    //遍历每个属性
    for (var i in prop) {
        //记录旧的属性值
        old[i] = elem.style[i];
        //设置新的值
        elem.style[i] = prop[i];
    }
    return old;
}
//恢复原有CSS属性
function restoreCSS(elem, prop) {
    for (var i in prop)
        elem.style[i] = prop[i];
}



//多行文本专用字符串截取
function getLength(obj, length, name) {
    if (obj) {
        if (obj.value.length > length) {
            var mes = '该字段';
            if (name && name != "") {
                mes = name;
            }
            alert(mes + "信息长度不能超过" + length + "，请先截取！");
            obj.focus();
            return false;
        }
    }
    return true;
}


//企业打开业务时，采用当前页面跳转。
//双菜单形式业用。
function openApp(url) {
    //左边主菜单收起
    var fs = window.parent.document.getElementById("m_trleft");
    $("#t_menu", window.parent.frames["m_left"].document).hide("100", function() {
        fs.cols = "18,*"; $("#t_show", window.parent.frames["m_left"].document).show("100", function() { document.location = url; });
    });

    $("#t_menu", window.parent.frames["m_left"].document).mouseout(function() {
        $("#t_menu", window.parent.frames["m_left"].document).hide("100", function() {
            fs.cols = "18,*"; $("#t_show", window.parent.frames["m_left"].document).show("100");
        });
    });

}


///使用三级自定义菜单时，整理样式js：
function CollateMenu() {
    //整理按钮样式
    $(".o_m01_1").each(function() {//一级
        if ($(this).find("+div").find(":parent").length > 0) {
            $(this).find("+div:parent").hide();
            $(this).attr("class", "o_m01_3");
        }
        else {
            $(this).find("+div").hide();
        }
    });
    $(".o_m02_1").each(function() {//二级
        if ($(this).find("+div").find(":parent").length > 0) {
            $(this).find("+div:parent").hide();
            $(this).attr("class", "o_m02_3");
        }
    });
    //第一个有子菜单的展开
    $(".o_m01_3").first().find("+div:parent").show();
    $(".o_m01_3").first().attr("class", "o_m01_4");


    //子菜单展开和隐藏
    $(".o_m01_3,.o_m01_4").click(function() {//一级
        if ($(this).find("+div:parent").is(":hidden"))
            $(this).attr("class", "o_m01_4");
        else
            $(this).attr("class", "o_m01_3");
        $(this).blur(); //转移焦点
        $(this).find("+div:parent").slideToggle("fast");
    });
    $(".o_m02_3").click(function() {//二级
        $(this).find("+div:parent").slideToggle("fast");
        if ($(this).attr("class") == "o_m02_3")
            $(this).attr("class", "o_m02_4");
        else
            $(this).attr("class", "o_m02_3");
        $(this).blur(); //转移焦点
    });
    //点击没有子菜单
    $(".o_m01_1").click(function() {//一级
        $(".o_m01_2").attr("class", "o_m01_1");
        $(".o_m02_2").attr("class", "o_m02_1");
        $(".o_m03_2").attr("class", "o_m03_1");
        $(this).attr("class", "o_m01_2");
        $(this).blur(); //转移焦点
    });
    $(".o_m02_1").click(function() {//二级
        $(".o_m01_2").attr("class", "o_m01_1");
        $(".o_m02_2").attr("class", "o_m02_1");
        $(".o_m03_2").attr("class", "o_m03_1");
        $(this).attr("class", "o_m02_2");
        $(this).blur(); //转移焦点
    });
    $(".o_m03_1").click(function() {//三级
        $(".o_m01_2").attr("class", "o_m01_1");
        $(".o_m02_2").attr("class", "o_m02_1");
        $(".o_m03_2").attr("class", "o_m03_1");
        $(this).attr("class", "o_m03_2");
        $(this).blur(); //转移焦点
    });
}

//调用自定义提示tip
function tipAuto() {
    $("[tip],[title],[alt]").each(function() {
        var tip = $(this).attr("title");
        if (tip) {
            $(this).removeAttr("title");
            $(this).attr("tip", tip);
        }
        else {
            tip = $(this).attr("tip");
            if (!tip) {
                tip = $(this).attr("alt");
            }
        }
        $(this).hover(function() {
            if (tip)
                tooltip.show(tip);
        }, function() {
            tooltip.hide();
        });
    });
}

//自定义提示tip
var tooltip = function() {
    var id = 'tip_tt';
    var top = 3;
    var left = 3;
    var maxw = 300;
    var speed = 10;
    var timer = 20;
    var endalpha = 88;
    var alpha = 0;
    var tt, t, c, b, h;
    var ie = document.all ? true : false;
    return {
        show: function(v, w) {
            if (tt == null) {
                tt = document.createElement('div');
                tt.setAttribute('id', id);
                t = document.createElement('div');
                t.setAttribute('id', id + 'top');
                c = document.createElement('div');
                c.setAttribute('id', id + 'cont');
                b = document.createElement('div');
                b.setAttribute('id', id + 'bot');
                tt.appendChild(t);
                tt.appendChild(c);
                tt.appendChild(b);
                document.body.appendChild(tt);
                tt.style.opacity = 0;
                tt.style.filter = 'alpha(opacity=0)';
            }
            document.onmousemove = this.pos;
            tt.style.display = 'block';
            c.innerHTML = v;
            tt.style.width = w ? w + 'px' : 'auto';
            if (!w && ie) {
                t.style.display = 'none';
                b.style.display = 'none';
                tt.style.width = tt.offsetWidth;
                t.style.display = 'block';
                b.style.display = 'block';
            }
            if (tt.offsetWidth > maxw) { tt.style.width = maxw + 'px' }
            h = parseInt(tt.offsetHeight) + top;
            clearInterval(tt.timer);
            tt.timer = setInterval(function() { tooltip.fade(1) }, timer);
        },
        pos: function(e) {
            var u = ie ? event.clientY + document.documentElement.scrollTop : e.pageY;
            var l = ie ? event.clientX + document.documentElement.scrollLeft : e.pageX;

            tt.style.top = ((u - h) < 0 ? 0 : (u - h)) + 'px';
            tt.style.left = (l + left) + 'px';

        },
        fade: function(d) {
            var a = alpha;
            if ((a != endalpha && d == 1) || (a != 0 && d == -1)) {
                var i = speed;
                if (endalpha - a < speed && d == 1) {
                    i = endalpha - a;
                } else if (alpha < speed && d == -1) {
                    i = a;
                }
                alpha = a + (i * d);
                tt.style.opacity = alpha * .01;
                tt.style.filter = 'alpha(opacity=' + alpha + ')';
            } else {
                clearInterval(tt.timer);
                if (d == -1) { tt.style.display = 'none' }
            }
        },
        hide: function() {
            clearInterval(tt.timer);
            tt.timer = setInterval(function() { tooltip.fade(-1) }, timer);
        }
    };
} ();


//金额转换成大写  obj：要转换的对象(有val属性的)；unit：单位（人民币），可以没有

function moneyToCapital(obj, unit) {
    var num = $(obj).val();
    if (!/^\d*(\.\d*)?$/.test(num)) { alert("请输入有效的金额!"); obj.select(); return ""; }
    return Capital(num, unit);
}


function Capital(num, unit) {
    var AA = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖");
    var BB = new Array("", "拾", "佰", "仟", "万", "亿", "圆", "", "整");
    var CC = new Array("角", "分", "厘");

    var a = ("" + num).replace(/(^0*)/g, "").split("."), k = 0, re = "";
    for (var i = a[0].length - 1; i >= 0; i--)  //author: meizz
    {
        switch (k) {
            case 0: re = BB[7] + re; break;
            case 4: if (!new RegExp("0{4}\\d{" + (a[0].length - i - 1) + "}$").test(a[0]))
                    re = BB[4] + re; break;
            case 8: re = BB[5] + re; BB[7] = BB[5]; k = 0; break;
        }
        if (k % 4 == 2 && a[0].charAt(i) == "0" && a[0].charAt(i + 2) != "0") re = AA[0] + re;
        if (a[0].charAt(i) != 0) re = AA[a[0].charAt(i)] + BB[k % 4] + re; k++;
    }
    if (re) re += BB[6];
    if (a.length > 1) //加上小数部分(如果有小数部分)
    {
        for (var i = 0; i < a[1].length; i++) {
            re += AA[a[1].charAt(i)] + CC[i];
            if (i == 2) break;
        }
    }
    else if (re) { re += BB[8]; }
    if (re && unit) re += unit;
    return re;
}
function FIsApprove() {

    if (document.getElementById("btnSave") != null) {
        document.getElementById("btnSave").disabled = true;
    }
    if (document.getElementById("btnAdd") != null) {
        document.getElementById("btnAdd").disabled = true;
    }
    if (document.getElementById("btnAdd0") != null) {
        document.getElementById("btnAdd0").disabled = true;
    }
    if (document.getElementById("btnDel") != null) {
        document.getElementById("btnDel").disabled = true;
    }
    if (document.getElementById("btnMod") != null) {
        document.getElementById("btnMod").disabled = true;
    }
    if (document.getElementById("btnReport") != null) {
        document.getElementById("btnReport").disabled = true;
    }
    if (document.getElementById("btnInto") != null) {
        document.getElementById("btnInto").disabled = true;
    }
    if (document.getElementById("btnReload") != null) {
        document.getElementById("btnReload").disabled = true;
    }
    if (document.getElementById("btnOrder") != null) {
        document.getElementById("btnOrder").disabled = true;
    }
    if (document.getElementById("btnListOrder") != null) {
        document.getElementById("btnListOrder").disabled = true;
    }
    if (document.getElementById("btnReload1") != null) {
        document.getElementById("btnReload1").disabled = true;
    }

    if (document.getElementById("btnReload1") != null) {
        document.getElementById("btnReload1").disabled = true;
    }

    if (document.getElementById("btnModSFZ") != null) {
        document.getElementById("btnModSFZ").disabled = true;
    }

    if (document.getElementById("btnModXLZ") != null) {
        document.getElementById("btnModXLZ").disabled = true;
    }

    if (document.getElementById("btnModZCZ") != null) {
        document.getElementById("btnModZCZ").disabled = true;
    }

    if (document.getElementById("btnModPhoto") != null) {
        document.getElementById("btnModPhoto").disabled = true;
    }

    if (document.getElementById("aUpQualiPic") != null) {
        document.getElementById("aUpQualiPic").disabled = true;
        document.getElementById("aUpQualiPic").onclick = function() { }
    }
    if (document.getElementById("btnModZLHT") != null) {
        document.getElementById("btnModZLHT").disabled = true;
    }
    if (document.getElementById("btnModStructure") != null) {
        document.getElementById("btnModStructure").disabled = true;
    }
    if (document.getElementById("btnModStructure1") != null) {
        document.getElementById("btnModStructure1").disabled = true;
    }
    if (document.getElementById("btnModBXMC") != null) {
        document.getElementById("btnModBXMC").disabled = true;
    }
    if (document.getElementById("UploadLink") != null) {
        document.getElementById("UploadLink").style.display = "none";
    }

    // .attr("disabled", "disabled")
    //    $("td").each(function() {
    //       alert( $(this).text);
    //       return;
    //    }
}

/////#///上报后信息不让改脚本////////////
function btnEnable() {
    $("#btnAdd").remove(); //新增
    $("#btnSave").remove(); //保存
    $("#btnSelect").remove(); //选择
    $("#btnDel").remove(); //删除
    $("#btnClear").remove(); //清空
    $("#btnLoad").remove(); //导入
    $("#btnFOrder").remove(); //保存顺序
    $("#btnSelectEnt").remove(); //选择所在企业
    $("#btnMod").remove(); //更改照片
    $("#btnReport").remove(); //上报
    $("#btnUpload").remove(); //上传
    $("#btnUpFile,#btnSelectEmp,#btnSelEmpList").remove();
    $(":submit,:button[value^=选择],:button[value^=上传]").remove();
    $("input[id$=btnDelete],input[id$=lbtnDelete],input[id$=btnDel],input[id$=btnItemSave],input[id$=btnSave]").remove(); //button删除、保存
    $("a[id$=btnDel],a[id$=btnDelete],a[id$=lbtnDelete],a[id$=btnItemSave],a[id$=btnSave]").replaceWith("<a>--</a>"); //linkbuttom删除、保存

    //上报后查看或管理部门查看时填写内容变为纯文本
    changeWord();
}

//上报后查看或管理部门查看时填写内容变为纯文本
function changeWord() {
    //去掉红色的星号
    $("tt,font,span").each(function() {
        if ($(this).html().indexOf("*") >= 0 || $(this).text().indexOf("*") >= 0)
            $(this).replaceWith("");
    });
    //select转换为文字
    $("select").each(function() {
        var s = $(this).closest("table").find("tbody tr td input[type='submit'][value^='查询']");
        if (s.length < 1)
            $(this).replaceWith("<span>" + $("option:selected", this).text().replace("请选择", "").replace("----", "") + "</span>");
    });
    //input转换为文字
    $(":text[class^=m_txt]").each(function() {
        var s = $(this).closest("table").find("tbody tr td input[type='submit'][value^='查询']");
        if (s.length < 1)
            $(this).replaceWith(this.value);
    });
    //textarea转换为文字
    $("textarea[class^=m_txt]").each(function() {
        var val = this.value.replace(/\r\n/g, "<>").replace(/\s/g, "&nbsp;").replace(/<>/g, "<br /><br />");
        $(this).replaceWith("<span>" + val + "</span>");
    });
    //radio转换为文字
    $("input[type=radio]").each(function() {
        if ($(this).attr("checked")) { $(this).remove(); }
        else { $(this).next().remove(); $(this).remove(); }
    });
    //checkbox转换为文字
    $("input[type=checkbox]").each(function() {
        if (this.id == "t_FNation") {
            $(this).remove();
            $("label[for=" + this.id + "]").remove();
        }
        else if ($(this).attr("checked")) {
            var txt = $(this).next().text();
            if (txt == "")
                txt = "是";
            if (this.id.indexOf('CheckItem') == -1 &&
            this.id.indexOf('checkAll') == -1) {
                $("label[for=" + this.id + "]").remove();
                $(this).replaceWith(txt);
            } else {
                $(this).parent().hide();
                $(this).remove();
            }
        }
        else {
            var txt = $(this).next().text();
            $("label[for=" + this.id + "]").remove();
            if (txt == "")
                txt = "否";
            else
                txt = "";

            if (this.id.indexOf('CheckItem') == -1 &&
            this.id.indexOf('checkAll') == -1) {
                $(this).replaceWith(txt);
            } else {
                $(this).parent().hide();
                $(this).remove();
            }
        }
    });
    //移除只读属性，免的变灰
    $(":text[readonly='readonly'],textarea[readonly='readonly']").removeAttr("readonly");
    $("[disabled='disabled']").removeAttr("disabled");
    $("#a7,#jop3").hide();
    $("table#search_tab_Info").hide();
    $("span[id*=lReportTip]").html("<font color='red'>当前业务已经上报！</font>");
}

//设置Cookie
function setCookie(name, value) {
    var argv = setCookie.arguments;
    var argc = setCookie.arguments.length;
    var expires = (argc > 2) ? argv[2] : null;
    if (expires != null) {
        var LargeExpDate = new Date();
        LargeExpDate.setTime(LargeExpDate.getTime() + (expires * 1000 * 3600 * 24));
    }
    document.cookie = name + "=" + escape(value) + ((expires == null) ? "" : ("; expires=" + LargeExpDate.toGMTString()));
}

//读取Cookie
function getCookie(Name) //cookies读取JS 
{
    var search = Name + "="
    if (document.cookie.length > 0) {
        offset = document.cookie.indexOf(search)
        if (offset != -1) {
            offset += search.length
            end = document.cookie.indexOf(";", offset)
            if (end == -1) end = document.cookie.length
            return unescape(document.cookie.substring(offset, end))
        }
        else return "";
    }
}

function EntWinUnloadEvent(JudgeWay, ClearType) {
    var isClear = 0;
    var isIE = document.all ? true : false;
    if (isIE) {//IE浏览器  
        var n = window.event.screenX - window.screenLeft;
        var b = n > document.documentElement.scrollWidth - 20;
        if ((b && window.event.clientY < 0) || (window.event.clientY < 0 && window.event.clientX < 10) || window.event.altKey) {
            isClear = 1;
        }
    }
    else {//火狐浏览器 
        if (document.documentElement.scrollWidth != 0) {
        }
        else
            isClear = 1;
    }
    if (JudgeWay == "Enforce") {//强制清理
        isClear = 1;
    }
    if (isClear == 1) {
        $.ajax({
            url: "../../Common/ClearSession.ashx?Type=" + ClearType,
            type: "get",
            dataType: "html",
            timeout: 10000
        });
    }
}



function findDimensions() //函数：获取尺寸
{
    var winWidth = 0;

    var winHeight = 0;
    //获取窗口宽度

    if (window.innerWidth)

        winWidth = window.innerWidth;

    else if ((document.body) && (document.body.clientWidth))

        winWidth = document.body.clientWidth;

    //获取窗口高度

    if (window.innerHeight)

        winHeight = window.innerHeight;

    else if ((document.body) && (document.body.clientHeight))

        winHeight = document.body.clientHeight;

    //通过深入Document内部对body进行检测，获取窗口大小

    if (document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth) {

        winHeight = document.documentElement.clientHeight;

        winWidth = document.documentElement.clientWidth;

    }

    //结果输出至两个文本框
    var o = new Object();
    o.Height = winHeight;
    o.Width = winWidth;
    return o;
}
function CPos(x, y) {
    this.x = x;
    this.y = y;
}

function GetObjPos(ATarget) {
    var target = ATarget;
    var pos = new CPos(target.offsetLeft, target.offsetTop);

    var target = target.offsetParent;
    while (target) {
        pos.x += target.offsetLeft;
        pos.y += target.offsetTop;

        target = target.offsetParent
    }

    return pos;
}

function initUpdateProgress(control) {
    var UpdateProgress1 = document.getElementById("UpdateProgress1");
    if (control) {
        var table1 = document.getElementById(control);
        if (table1) {
            UpdateProgress1.style.left = GetObjPos(table1).x + "px";
            UpdateProgress1.style.top = GetObjPos(table1).y + "px";
            UpdateProgress1.style.width = getWidth(table1) + "px";
            UpdateProgress1.style.height = getHeight(table1) + "px";
        }
    }
    else {
        var o = findDimensions();
        UpdateProgress1.style.left = "0px";
        UpdateProgress1.style.top = "0px";
        UpdateProgress1.style.width = o.Width + "px";
        UpdateProgress1.style.height = o.Height + "px";
    }

}