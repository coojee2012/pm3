<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GovBase.aspx.cs" Inherits="Share_Main_GovBase" %>

<!DOCTYPE html>
   <script src="../../script/jquery.js" type="text/javascript"></script>
<script src="../../script/lock.js"></script>
<script  type="text/javascript" >

    $(function() {
        LoadActiveX();
    });


    function mygetLockId(username, pageid) {
        
        var keyvalue = getLockId();
        
        if (keyvalue === "" || keyvalue == undefined) {
            alert("请插入加密锁!");
            return;
        }
 
        //ytb 修改
        VerifyKey(encodeURI(username), keyvalue, pageid);
      
    }
    //ytb 修改
    function VerifyKey(name, key, pageid) {

        $.ajax({
            url: "GovBase.aspx",
            type: 'get',
            contenttype: "application/json; charset=utf-8",
            data: { 'username': name, 'method': 'verify', 'keyvalue': key, 'pageid': pageid },
            success: function(data) {
                if (data === "true") {
                    window.location.href = "/Government/AppZBBA/"+pageid+".aspx?r="+Math.random();
                } else {
                    {
                        alert("必须传递合法的用户帐号");
                    }
                }
            },
            error: function (er) {
                alert(er);
            }
        });
    };


    
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
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
