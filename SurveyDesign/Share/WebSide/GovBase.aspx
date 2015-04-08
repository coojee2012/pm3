<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GovBase.aspx.cs" Inherits="Share_Main_GovBase" %>

<!DOCTYPE html>
   <script src="../../script/jquery.js" type="text/javascript"></script>
<script src="../../script/lock.js"></script>
<script  type="text/javascript" >

    function mygetLockId(username, pageurl) {

        var keyvalue = getLockId();

        if (keyvalue === "" || keyvalue == undefined) {
            alert("请插入加密锁!");
            return;
        }

        //ytb 修改
        VerifyKey(encodeURI(username), keyvalue, pageurl);

    }
    //ytb 修改
    function VerifyKey(name, key, pageurl) {

        $.ajax({
            url: "GovBase.aspx",
            type: 'get',
            contenttype: "application/json; charset=utf-8",
            data: { 'username': name, 'method': 'verify', 'keyvalue': key, 'pageid': pageurl },
            success: function (data) {
                if (data === "true") {
                    window.location.href = (getRootPath() + "/Government/" + pageurl);
                } else {
                    alert("必须传递合法的用户帐号");
                }
            },
            error: function (er) {
                alert(er);
            }
        });
    };

    function getRootPath() {
        //获取当前网址，
        var curWwwPath = window.document.location.href;
        //获取主机地址之后的目录
        var pathName = window.document.location.pathname;
        var pos = curWwwPath.indexOf(pathName);
        //获取主机地址
        var localhostPaht = curWwwPath.substring(0, pos);

        return (localhostPaht);
    }



</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>

    <object id="ePass" style="left: 0px; top: 0px" height="0" width="0" classid="clsid:C7672410-309E-4318-8B34-016EE77D6B58"  name="ePass"></object>
<object id="ePass2" style="left: 0px; top: 0px" height="0" width="0" classid="clsid:e6bd6993-164f-4277-ae97-5eb4bab56443" name="ePass2"></object>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <input type="hidden" id="LabKey" runat="server" value="" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
