<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GovBase.aspx.cs" Inherits="Share_Main_GovBase" %>
<object id="ePass" style="left: 0px; top: 0px" height="0" width="0" classid="clsid:C7672410-309E-4318-8B34-016EE77D6B58"  name="ePass"></object>
<object id="ePass2" style="left: 0px; top: 0px" height="0" width="0" classid="clsid:e6bd6993-164f-4277-ae97-5eb4bab56443" name="ePass2"></object>

<!DOCTYPE html>
<script  type="text/javascript" >
    function ReadU_KeyInfor() {
        var Device, DeviceSN, DeviceCount;
        Device = document.getElementById("ePass2");
        try {
            if (DeviceCount = Device.FindToken("483FEC71")) {
                if (!Device.OpenToken("483FEC71", 1)) {
                    DeviceSN = Device.GetSN();
                    alert(DeviceSN);
                    return DeviceSN;
                }
            }
        }
        catch (err) {

        }
    }

    function CheckKey() {
        //  return "42F81BC1722A9F26";
        var ss;
        try {
            ss = ePass.GetLibVersion();
            ss = ePass.OpenDevice(1, "");
            var results = ePass.GetStrProperty(7, 0, 0);
            //alert(results);
            if ((results == "42F81BC1844C0D6D") || (results == "42F81BC171EC0764") || (results == "42F81BC171EC1BDC") || (results == "42F81BC171C44E24") || (results == "42F81BC171EC01D4") || (results == "42F81BC1765AB00B") || (results == "42F81BC1765A403B") || (results == "42F81BC1765AB653") || (results == "42F81BC1788359B3") || (results == "42F81BC18443E30D") || (results == "42F81BC18473560D") || (results == "42F81BC18472D08D"))
                results = "42F81BC1844395A3";
            if ((results == "42F81BC175B2AEC3") || (results == "42F81BC175B2B98B"))
                results = "42F81BC171224894";
            if (results == "42F81BC18473CA3D")
                results = "42F81BC174EC0CC1";
            if (results == "42F81BC18473D625")
                results = "42F81BC175B2B693";
            return results;
        }
        catch (err) {

        }

    }

    function getLockId() {
        var result = ReadU_KeyInfor();

        if (result == "" || result == null) {
            result = CheckKey();
        }
        document.getElementById("LabKey").value =  "1" + result +"2";
        return result;
    }
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
