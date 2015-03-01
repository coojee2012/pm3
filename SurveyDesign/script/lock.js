var flag = 0;
function LoadActiveX() {

    //ET99 白锁
    document.body.insertAdjacentHTML('beforeEnd', "<object id='ePass2' style='DISPLAY: none' classid='clsid:e6bd6993-164f-4277-ae97-5eb4bab56443' name='ePass2'></object>");
    //1000
    document.body.insertAdjacentHTML('beforeEnd', '<object id="ePass1" style="left: 0px; top: 0px" height="0" width="0" classid="clsid:C7672410-309E-4318-8B34-016EE77D6B58" name="ePass1"></object>');

    //BJCA
    try {
        //var oUtil = new ActiveXObject("BJCASecCOM.Util");
        document.body.insertAdjacentHTML('beforeEnd', "<OBJECT classid=\"clsid:FCAA4851-9E71-4BFE-8E55-212B5373F040\" height=1 id=bjcactrl  style=\"HEIGHT: 1px; LEFT: 10px; TOP: 28px; WIDTH: 1px\" width=1 VIEWASTEXT></OBJECT>");
        //var bjcactrl = new ActiveXObject("BJCASecCOM.BJCASecCOMV2");
        bjcactrl.GetUserListPnp();
    }
    catch (e) {
        //alert("请下载证书驱动程序并安装!");
    }
    try {
        PubWindow();
    }
    catch (ex) { }
}


function GetePass1() {

    var ePass1 = document.getElementById("ePass1");
    var str = "";
    if (ePass1) {
        ePass1.GetLibVersion();

        try {
            ePass1.OpenDevice(1, "");
            str = ePass1.GetStrProperty(7, 0, 0);
            ePass1.CloseDevice();
            // alert(ePass1.GetStrProperty(7,0,0));
        }
        catch (e) {
            ePass1.CloseDevice();

            return;
        }
    }
    return str;
}

function GetePass2() {
    var Device = document.getElementById("ePass2");
    if (Device) {
        try {
            if (DeviceN = Device.FindToken("483FEC71")) {
                if (!Device.OpenToken("483FEC71", 1)) {
                    return Device.GetSN();
                    // alert(Device.GetSN());
                }
            }
        }
        catch (e) {
            return "";
        }
    }
}
function getLockId() {


    var result = GetePass2();
    //只用ET99的锁
    if (result == "" || result == null) {
        result = GetePass1();
        //       if (result == "" || result == null) {
        //           //ET99 百色加密锁
        //           result = ReadePass1000();
        //           if (result == "" || result == "00" || result == null) {
        //               result = ReadET99Jx();
        //           }
        //           //result=ReadePass1000();
        //       }
    }
    return result;
}
function readLock(id) {
    var o = document.getElementById(id);
    if (o) {
        o.value = getLockId();
    }
}

function setLockId() {
    var o = document.getElementById("id");
    if (o) {
        o.value = getLockId();
    }
}


function fnQYLogin() {

    var sInfo = GetUserList_HT();
    //    if (sInfo == null || sInfo == "") {
    //        alert("请插入证书KEY！");
    //        return false;
    //    }
    strName = sInfo.substring(0, sInfo.indexOf("||"));
    strUniqueID = sInfo.substring(sInfo.indexOf("||") + 2, sInfo.indexOf("&&&"));

    var oCAID = document.getElementById("CaCerti");

    var UserCert = bjcactrl.ExportUserCert(strUniqueID);
    //var sUserCaId = bjcactrl.GetCertInfoByOid(UserCert, "1.2.86.11.7.1.8");
    //验证书
    //var oFrm = document.getElementById("hdFrm");


    if (oCAID != null && UserCert != null && UserCert != "") {
        oCAID.value = UserCert;
        //oUserCaId.value = sUserCaId;
    } //alert(oCAID.value);


}


function GetUserList_HT() {
    try {
        g_xmluserlist = bjcactrl.GetUserList();
    }
    catch (e) {
        g_xmluserlist = "";
        //	    alert(e.message);
    }
    return g_xmluserlist;
}
window.onload = LoadActiveX;