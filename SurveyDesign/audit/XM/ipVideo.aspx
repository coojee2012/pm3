<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ipVideo.aspx.cs" Inherits="audit_XM_ipVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <base target="_self"></base>
    <script type="text/javascript" src="ydsp/jquery.js"></script>
    <script language="javascript" for="PlayViewOCX" event="FireCatchPic(szPath,iWindowID)" charset="gb2312">
        //抓图返回信息
        //路径 szPath
        //窗口 iWindowID   
    </script>
    <script language="javascript" charset="gb2312">
        window.attachEvent('onload', fnLoad);
        function fnLoad() {
            //登入CMS
            loginCMS();
            //设置本地参数
            SetlocalParam();
        }

        //登入CMS
        function loginCMS() {
            var userName = document.getElementById("hidden_UserName").value;
            var pw = document.getElementById("hidden_PassWord").value;
            var ipAdd = document.getElementById("hidden_IPAddress").value;
            var port = document.getElementById("hidden_Port").value;
            var dataFetchType = "0"; // 0 从CMS获取数据   1 从本地获取数据
            try {
                var OCXobj = document.getElementById("PlayViewOCX");
                var ret = OCXobj.Login_V11(userName, pw, ipAdd, port, dataFetchType);
                switch (ret) {
                    case 0:
                        //调用成功！
                        break;
                    case -1:
                        //调用失败！错误码:OCXobj.GetLastError_V11()
                        //alert("登入服务器失败，请检查视频运营商和供应商信息是否填写正确！");
                        break;
                    default:
                        break;
                }
            }
            catch (e) {
                alert("视频系统登陆失败");
            }
        }

        /*****************本地参数设置******************/
        function SetlocalParam() {
            //设置本地参数
            var PicType = 0;
            var PicPath = "C:\\capimages";
            var minSpace4Pic = "256";
            var RecordPath = "C:\\Record";
            var RecordSize = "2";
            var minSpace4Rec = "256";
            var OCXobj = document.getElementById("PlayViewOCX");
            //设置图片保存路径和格式
            var Ret = OCXobj.SetCapturParam(PicPath, PicType);
            if (Ret == 0) {
                //调用成功！
            }
            else {
                //调用失败！错误码:OCXobj.GetLastError_V11()
            }
            //设置保存图片磁盘空间最小值
            OCXobj.SetPicDiskMinSize(minSpace4Pic);
            //设置录像保存路径和文件大小
            var ret2 = OCXobj.SetRecordParam(RecordPath, RecordSize);
            if (ret2 == 0) {
                //调用成功
            }
            else {
                //调用失败！错误码:OCXobj.GetLastError_V11()
            }
            //设置录像磁盘空间最小值
            OCXobj.SetRecordDiskMinSize(minSpace4Rec);
        }


        /*****设置视频参数******/
        function SetVideoEffect() {
            var OCXobj = "";
            BrightValue = "";
            ContrastValue = "";
            SaturationValue = "";
            HueValue = "";
            var validateRes = validateIntegerRange(BrightValue, 1, 10, '亮度')
                                & validateIntegerRange(ContrastValue, 1, 10, '对比度')
                                & validateIntegerRange(SaturationValue, 1, 10, '饱和度')
                                & validateIntegerRange(HueValue, 1, 10, '色调');
            if (!validateRes) {
                return;
            }
            var ret = OCXobj.SetVideoEffect(parseInt(BrightValue), parseInt(ContrastValue), parseInt(SaturationValue), parseInt(HueValue));
            switch (ret) {
                case 0:
                    //调用成功！
                    break;
                case -1:
                    //调用失败！错误码:OCXobj.GetLastError_V11()
                    break;
                default:
                    break;
            }
        }

        function startvideo(address) {

            var OCXobj = document.getElementById("PlayViewOCX");
            try {
                var ret = OCXobj.StartTask_Preview_V11(address);
                switch (ret) {
                    case 0:
                        //调用成功！
                        break;
                    case -1:
                        //调用失败！错误码:OCXobj.GetLastError_V11()
                        break;
                    default:
                        break;
                }
            }
            catch (e) {
                alert("视频查看失败");
            }
        }

        function PTZControl(funcName) {
            var OCXobj = $("#PlayViewOCX").get(0);
            if (OCXobj == null) {
                //alert("控件未安装！");
                return;
            } else {
                var res = null;
                switch (funcName) {
                    case "PTZLeftUp":
                        res = OCXobj.StartTask_PTZ(25, 1); //云台：左上
                        break;
                    case "PTZUp":
                        res = OCXobj.StartTask_PTZ(21, 1); //云台：上
                        break;
                    case "PTZRightUp":
                        res = OCXobj.StartTask_PTZ(26, 1); //云台：右上
                        break;
                    case "PTZLeft":
                        res = OCXobj.StartTask_PTZ(23, 1); //云台：左
                        break;
                    case "PTZAuto":
                        res = OCXobj.StartTask_PTZ(29, 1); //云台：自转
                        break;
                    case "PTZRight":
                        res = OCXobj.StartTask_PTZ(24, 1); //云台：右
                        break;
                    case "PTZLeftDown":
                        res = OCXobj.StartTask_PTZ(27, 1); //云台：左下
                        break;
                    case "PTZDown":
                        res = OCXobj.StartTask_PTZ(22, 1); //云台：下
                        break;
                    case "PTZRightDown":
                        res = OCXobj.StartTask_PTZ(28, 1); //云台：右下
                        break;
                    case "PTZStop":
                        res = OCXobj.StartTask_PTZ(-21, 1); //云台：停止
                        break;
                    case "PTZAddTimes":
                        res = OCXobj.StartTask_PTZ(11, 1); //云台：焦距+
                        break;
                    case "PTZMinusTimes":
                        res = OCXobj.StartTask_PTZ(12, 1); //云台：焦距-
                        break;
                    case "PTZFarFocus":
                        res = OCXobj.StartTask_PTZ(13, 1); //云台：焦点+
                        break;
                    case "PTZNearFocus":
                        res = OCXobj.StartTask_PTZ(14, 1); //云台：焦点-
                        break;
                    case "PTZLargeAperture":
                        res = OCXobj.StartTask_PTZ(15, 1); //云台：光圈+
                        break;
                    case "PTZSmallAperture":
                        res = OCXobj.StartTask_PTZ(16, 1); //云台：光圈-
                        break;
                }

                if (res != null && res == 0) {
                    //showMethodInvokedInfo("StartTask_PTZ接口调用成功！");
                } else {
                    //showMethodInvokedInfo("StartTask_PTZ接口调用失败！错误码：" + OCXobj.GetLastError_V11());
                }
            }
        }

        function amousedown(obj) {
            obj.className = "selected";
            if (obj.getAttribute("X") == "up") {
                PTZControl('PTZUp');
            }
            if (obj.getAttribute("X") == "down") {
                PTZControl('PTZDown');
            }
            if (obj.getAttribute("X") == "left") {
                PTZControl('PTZLeft');
            }
            if (obj.getAttribute("X") == "right") {
                PTZControl('PTZRight');
            }
            if (obj.getAttribute("X") == "big") {
                PTZControl('PTZAddTimes');
            }
            if (obj.getAttribute("X") == "small") {
                PTZControl('PTZMinusTimes');
            }
        }
        function amouseup(obj) {
            obj.className = "";
            if (obj.getAttribute("X") == "up") {
                PTZControl('PTZStop');
            }
            if (obj.getAttribute("X") == "down") {
                PTZControl('PTZStop');
            }
            if (obj.getAttribute("X") == "left") {
                PTZControl('PTZStop');
            }
            if (obj.getAttribute("X") == "right") {
                PTZControl('PTZStop')
            }
            if (obj.getAttribute("X") == "big") {
                PTZControl('PTZStop');
            }
            if (obj.getAttribute("X") == "small") {
                PTZControl('PTZStop');
            }
        }

        function aclick(obj) {
            if (obj.getAttribute("X") == "big") {
                //PTZ_NEAR();
            }
            if (obj.getAttribute("X") == "small") {
                //PTZ_FAR();
            }
        }
    </script>
</head>
<body style="margin: 0px;">
    <div>
        <table width="98%" align="center" class="m_title" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th colspan="5">视频信息
                </th>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="0" style="height: 510px; width: 100%">
            <tr>
                <td style="height: 50%; width: 20%; vertical-align: top;">
                    <table border="0" cellspacing="0" cellpadding="0" style="height: 95%; width: 95%; border: 1px solid #B9CAD8;">
                        <tr style="height: 40px;">
                            <td>&nbsp;<a href="#"><%=XMMC %></a>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 50px;">
                                <%=htmlSPSBinfo %>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" cellpadding="0" style="height: 95%; width: 95%; border: 1px solid #B9CAD8;">
                                    <tr>
                                        <td align="center">
                                            <table style="height: 220px; width: 100%" border="0">
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <table border="0" cellspacing="0" cellpadding="0" style="width: 130px; height: 130px; background-image: url(../images/all.jpg);">
                                                            <tr style="height: 44px">
                                                                <td style="width: 44px">&nbsp;
                                                                </td>
                                                                <td class="tdup">
                                                                    <a href="#" s="" x="up" onmousedown="amousedown(this)" onmouseup="amouseup(this);"></a>
                                                                </td>
                                                                <td style="width: 44px">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 44px">
                                                                <td class="tdleft">
                                                                    <a href="#" s="" x="left" onmousedown="amousedown(this)" onmouseup="amouseup(this);"></a>
                                                                </td>
                                                                <td style="width: 44px">&nbsp;
                                                                </td>
                                                                <td class="tdright">
                                                                    <a href="#" s="" x="right" onmousedown="amousedown(this)" onmouseup="amouseup(this);"></a>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 44px">
                                                                <td style="width: 44px">&nbsp;
                                                                </td>
                                                                <td class="tddown">
                                                                    <a href="#" s="" x="down" onmousedown="amousedown(this)" onmouseup="amouseup(this);"></a>
                                                                </td>
                                                                <td style="width: 44px">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr style="height: 70px">
                                                    <td style="width: 35px"></td>
                                                    <td valign="top">
                                                        <table border="0" cellspacing="0" cellpadding="0" style="width: 130px; height: 44px;">
                                                            <tr>
                                                                <td class="tdbig">
                                                                    <a href="#" s="" x="big" onclick="aclick(this)" onmousedown="amousedown(this)" onmouseup="amouseup(this);"></a>
                                                                </td>
                                                                <td style="width: 44px">&nbsp;
                                                                </td>
                                                                <td class="tdsmall">
                                                                    <a href="#" s="" x="small" onclick="aclick(this)" onmousedown="amousedown(this)" onmouseup="amouseup(this);"></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table border="0" cellspacing="0" cellpadding="0" style="height: 520px; width: 100%">
                        <tr>
                            <td style="height: 20px; width: 60%;"></td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <a href="drivers\OCX_HK.exe" align="right">视频播放驱动下载  </a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <object classid="clsid:D5E14042-7BF6-4E24-8B01-2F453E8154D7" id="PlayViewOCX" width="640px" height="500px" name="ocx">
                                </object>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <form id="form1" runat="server">
        <input id="hidden_IPAddress" type="hidden" runat="server" value="218.200.188.237" />
        <input id="hidden_Port" type="hidden" runat="server" value="80" />
        <input id="hidden_UserName" type="hidden" runat="server" value="admin" />
        <input id="hidden_PassWord" type="hidden" runat="server" value="12345" />
    </form>
</body>
</html>
